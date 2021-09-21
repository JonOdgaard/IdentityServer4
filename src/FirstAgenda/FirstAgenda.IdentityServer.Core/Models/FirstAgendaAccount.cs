using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FirstAgenda.IdentityServer.Core.Models
{
    public class FirstAgendaAccount
    {
        public int Id { get; set; }
        
        [MaxLength(200)]
        public string SubjectId { get; set; }

        [MaxLength(100)]
        public string UserName { get; set; }

        public string Password { get; set; }

        [MaxLength(200)]
        public string ExternalProviderName { get; set; }

        [MaxLength(100)]
        public string ExternalProviderSubjectId { get; set; }

        public bool IsActive { get; set; } = true;

        [MaxLength(100)]
        public string Salt { get; set; }

        public DateTimeOffset LastPasswordChangeDateUtc { get; set; }

        public DateTimeOffset CreatedDateUtc { get; set; }
        public Guid Uid { get; set; }
        public bool MustChangedPassword { get; set; }
        public int FailedLoginAttempts { get; set; }
        public int LanguageId { get; set; }
        [MaxLength(10)]
        public string TimeZoneId { get; set; }
        public bool HasProfilePicture { get; set; }
    }
}