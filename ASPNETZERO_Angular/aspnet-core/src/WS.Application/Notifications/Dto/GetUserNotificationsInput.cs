using Abp.Notifications;
using WS.Dto;

namespace WS.Notifications.Dto
{
    public class GetUserNotificationsInput : PagedInputDto
    {
        public UserNotificationState? State { get; set; }
    }
}