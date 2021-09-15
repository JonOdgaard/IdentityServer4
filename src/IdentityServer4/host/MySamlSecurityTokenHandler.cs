using System;
using System.Diagnostics;
using System.IO;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Xml;
using Microsoft.IdentityModel.Tokens;
using Microsoft.IdentityModel.Tokens.Saml;
using Microsoft.IdentityModel.Xml;

namespace IdentityServerHost
{
    public class EncryptedSecurityToken : SecurityTokenHandler
    {
        public override bool CanReadToken(string tokenString)
        {
            var canRead = base.CanReadToken(tokenString);
            
            var canRead2 = base.CanReadToken(new XmlTextReader(new StringReader(tokenString)));


            return true;
            // return canRead;
        }

        public override bool CanReadToken(XmlReader reader)
        {
            return base.CanReadToken(reader);
        }

        public override ClaimsPrincipal ValidateToken(string securityToken, TokenValidationParameters validationParameters,
            out SecurityToken validatedToken)
        {
            var token = ReadToken(new XmlTextReader(new StringReader(securityToken)));

            // if (ContainingCollection != null)
            // {
            //     var identities = ContainingCollection.ValidateToken(validatedToken);
            //     var principal = new ClaimsPrincipal(identities.First());
            //
            //     return principal;
            // }
            //
            // return new ClaimsPrincipal(base.ValidateToken(validatedToken));            
            //
            
            return base.ValidateToken(securityToken, validationParameters, out validatedToken);
        }

        public override ClaimsPrincipal ValidateToken(XmlReader reader, TokenValidationParameters validationParameters,
            out SecurityToken validatedToken)
        {
            return base.ValidateToken(reader, validationParameters, out validatedToken);
        }

        public override bool CanValidateToken => true;

        public override void WriteToken(XmlWriter writer, SecurityToken token)
        {
            throw new NotImplementedException();
        }

        public override SecurityToken ReadToken(XmlReader reader, TokenValidationParameters validationParameters)
        {
            throw new NotImplementedException();
        }

        public override Type TokenType
        {
            get { return typeof(EncryptedSecurityToken); }
        }
    }
}