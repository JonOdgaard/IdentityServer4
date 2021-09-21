using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection.XmlEncryption;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace EncryotedTokenDecoder
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            Decode();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context => { await context.Response.WriteAsync("Hello World!"); });
            });
        }

        private void Decode()
        {
            var rawToken = File.ReadAllText(@"C:\git\CoreMvcWithWsFederation\WebWithWs\decode_me.txt");

            var xml = new XmlDocument();
            xml.LoadXml(rawToken);

            var ntm = new XmlNamespaceManager(xml.NameTable);
            ntm.AddNamespace("xenc", "http://www.w3.org/2001/04/xmlenc#");
            ntm.AddNamespace("e", "http://www.w3.org/2001/04/xmlenc#");

            var cipherValueNodes = xml.DocumentElement.SelectNodes("//e:CipherData", ntm);
            var cipherValues = new List<string>();


            // var cert = X509Certificate2.CreateFromCertFile(@"C:\git\server-og-web\Documentation\FirstAgenda EncryptAndSignDev.pfx");
            var cert = new X509Certificate2(@"C:\git\server-og-web\Documentation\FirstAgenda EncryptAndSignDev.pfx",
                "Jg15moso");


            foreach (var cipherValueNode in cipherValueNodes)
            {
                var xe = cipherValueNode as XmlElement;
                var cv = xe.ChildNodes.Item(0);
                var cipherValue = cv.InnerText;

                Console.WriteLine($"Cipher value: {cipherValue}");

                cipherValues.Add(cipherValue);
            }

            var firstECipher = cipherValues.First();
            var secondXencCipher = cipherValues.Skip(1).Take(1).Single();

            var d = Decrypt2(cert, firstECipher);

            Console.WriteLine(d);

            // var dec = DecryptStringUsingSymmetricAes256(firstECipher, certPublicKeyString);

            // foreach (var cipherValue in cipherValues.Skip(1))
            // {
            //
            //     var dc = DecryptString(cipherValue, cert.GetPublicKeyString());
            //     
            //     Console.WriteLine(dc);
            //     
            // }
        }

        private static byte[] ExtractIVAndDecrypt(
            SymmetricAlgorithm algorithm,
            byte[] cipherText,
            int offset,
            int count)
        {
            byte[] rgbIV = new byte[algorithm.BlockSize / 8];
            if (cipherText.Length - offset < rgbIV.Length)
                throw new ApplicationException(); // DiagnosticUtility.ExceptionUtility.ThrowHelperError((Exception) new InvalidOperationException(SR.GetString("ID6019", (object) (cipherText.Length - offset), (object) rgbIV.Length)));
            Buffer.BlockCopy((Array) cipherText, offset, (Array) rgbIV, 0, rgbIV.Length);
            algorithm.Padding = PaddingMode.ISO10126;
            algorithm.Mode = CipherMode.CBC;
            ICryptoTransform cryptoTransform = (ICryptoTransform) null;
            try
            {
                cryptoTransform = algorithm.CreateDecryptor(algorithm.Key, rgbIV);
                return cryptoTransform.TransformFinalBlock(cipherText, offset + rgbIV.Length, count - rgbIV.Length);
            }
            finally
            {
                cryptoTransform?.Dispose();
            }
        }
        
        public static byte[] Decrypt2(X509Certificate2 cert, string encryptedString)
        {
            var encryptedData = Encoding.UTF8.GetBytes(encryptedString);
            
            var symmetricAlgorithm = SymmetricAlgorithm.Create("http://www.w3.org/2001/04/xmlenc#aes256-cbc");

            var x = ExtractIVAndDecrypt(symmetricAlgorithm, encryptedData, 0, encryptedData.Length);
            
            
            // var certRsaPrivateKey = cert.GetRSAPrivateKey();
            // var rsaCng = certRsaPrivateKey as RSACng;
            // var dec = rsaCng.Decrypt(encryptedData, RSAEncryptionPadding.OaepSHA256);

            return x;
        }

        private static bool _optimalAsymmetricEncryptionPadding = false;

        public static bool IsKeySizeValid(int keySize)
        {
            return keySize >= 384 &&
                   keySize <= 16384 &&
                   keySize % 8 == 0;
        }

        public static string DecryptStringUsingSymmetricAes256(string cipherText, string keyString)
        {
            var fullCipher = Convert.FromBase64String(cipherText);

            var iv = new byte[16];
            var cipher = new byte[fullCipher.Length - iv.Length];

            Buffer.BlockCopy(fullCipher, 0, iv, 0, iv.Length);
            Buffer.BlockCopy(fullCipher, iv.Length, cipher, 0, cipher.Length);
            var key = Encoding.UTF8.GetBytes(keyString);

            using (var aesAlg = SymmetricAlgorithm.Create("http://www.w3.org/2001/04/xmlenc#aes256-cbc"))
            {
                using (var decryptor = aesAlg.CreateDecryptor(key, iv))
                {
                    string result;

                    using (var msDecrypt = new MemoryStream(cipher))
                    {
                        using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                        {
                            using (var srDecrypt = new StreamReader(csDecrypt))
                            {
                                result = srDecrypt.ReadToEnd();
                            }
                        }
                    }

                    return result;
                }
            }
        }
    }
}