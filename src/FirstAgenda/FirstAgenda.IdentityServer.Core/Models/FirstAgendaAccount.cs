using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace FirstAgenda.IdentityServer.Core.Models
{
    public class FirstAgendaAccount
    {
        public FirstAgendaAccount() { }
        
        public int Id { get; set; }
        
        [MaxLength(200)]
        public string SubjectId { get; set; }
        
        [MaxLength(100)]
        public string LoginId { get; set; }

        public string PasswordHash { get; set; }

        [MaxLength(200)]
        public string ExternalProviderName { get; set; }

        [MaxLength(100)]
        public string ExternalProviderSubjectId { get; set; }

        public bool IsActive { get; set; }

        [MaxLength(100)]
        public string Salt { get; set; }

        public DateTimeOffset? LastPasswordChangeDateUtc { get; set; }

        public DateTimeOffset CreatedDateUtc { get; set; }
        public Guid Uid { get; set; }
        public bool PasswordExpired { get; set; }
        public int FailedLoginAttempts { get; set; }

        public AccountProfile Profile { get; set; }
        
        /// <summary>
        /// Is true, the user must use two factor authentication using SMS token when signing in using forms authentication
        /// </summary>
        public bool MustUseTwoFactorAuthentication { get; set; }

        /// <summary>
        /// Mobile number to send two factor authentication token to when presenting two factor authentication challenge
        /// </summary>
        [MaxLength(100)]
        public string TwoFactorAuthenticationMobilePhone { get; set; }
    }
}