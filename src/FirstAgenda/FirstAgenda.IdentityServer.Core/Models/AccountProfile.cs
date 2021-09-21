using System.ComponentModel.DataAnnotations;

namespace FirstAgenda.IdentityServer.Core.Models
{
    public class AccountProfile
    {
        public int Id { get; set; }
        
        [MaxLength(300)]
        public string UserFullName { get; set; }
        
        public int LanguageId { get; set; }
        
        [MaxLength(10)]
        public string TimeZoneId { get; set; }
        
        public string SmsNotificationMobileNumber { get; set; }
        
        public bool HasProfilePicture { get; set; }
        public string ProfilePictureStorageBucket { get; set; }
        public string ProfilePictureStorageKey { get; set; }

    }
}