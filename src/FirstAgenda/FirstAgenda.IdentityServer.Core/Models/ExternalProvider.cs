using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace FirstAgenda.IdentityServer.Core.Models
{
    public class ExternalProvider
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string ProviderId { get; set; }

        public string HomeRealm { get; set; }

        public string MetadataUrl { get; set; }
    }
}