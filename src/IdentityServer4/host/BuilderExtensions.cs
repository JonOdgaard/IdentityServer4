using System.Security.Cryptography.X509Certificates;
using IdentityModel;
using IdentityServer4;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace IdentityServerHost
{
    public static class BuilderExtensions
    {
        public static IIdentityServerBuilder AddSigningCredential(this IIdentityServerBuilder builder)
        {
            // create random RS256 key
            //builder.AddDeveloperSigningCredential();

            // use an RSA-based certificate with RS256
            var rsaCert = new X509Certificate2("./keys/identityserver.test.rsa.p12", "changeit");
            builder.AddSigningCredential(rsaCert, "RS256");

            // ...and PS256
            builder.AddSigningCredential(rsaCert, "PS256");

            // or manually extract ECDSA key from certificate (directly using the certificate is not support by Microsoft right now)
            var ecCert = new X509Certificate2("./keys/identityserver.test.ecdsa.p12", "changeit");
            var key = new ECDsaSecurityKey(ecCert.GetECDsaPrivateKey())
            {
                KeyId = CryptoRandom.CreateUniqueId(16, CryptoRandom.OutputFormat.Hex)
            };

            // var store = new X509Store(StoreLocation.LocalMachine);
            // var certs = store.Certificates.Find(X509FindType.FindByThumbprint,
            //     "c38af9dcfc03a573d0c83355e08dc5b13d18590a", false);
            // var cert = certs[0];
            //
            // builder.AddSigningCredential(cert);

            return builder.AddSigningCredential(key, IdentityServerConstants.ECDsaSigningAlgorithm.ES256);
        }

        // use this for persisted grants store
        // public static void InitializePersistedGrantsStore(this IApplicationBuilder app)
        // {
        //     using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
        //     {
        //         serviceScope.ServiceProvider.GetRequiredService<PersistedGrantDbContext>().Database.Migrate();
        //     }
        // }
    }
}