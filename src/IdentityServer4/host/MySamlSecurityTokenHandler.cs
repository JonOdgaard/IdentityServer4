using System.Xml;
using Microsoft.IdentityModel.Tokens.Saml;

namespace IdentityServerHost
{
    public class MySamlSecurityTokenHandler : SamlSecurityTokenHandler
    {
        public override bool CanReadToken(string securityToken)
        {
            var canRead = base.CanReadToken(securityToken);
            return canRead;
        }

        public override bool CanReadToken(XmlReader reader)
        {
            return base.CanReadToken(reader);
        }
        
        
    }
}