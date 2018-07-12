using System.ComponentModel.DataAnnotations;
using WS.Authorization.Users;

namespace WS.Configuration.Host.Dto
{
    public class SendTestEmailInput
    {
        [Required]
        [MaxLength(User.MaxEmailAddressLength)]
        public string EmailAddress { get; set; }
    }
}