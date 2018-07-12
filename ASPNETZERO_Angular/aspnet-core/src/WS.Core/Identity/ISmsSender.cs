using System.Threading.Tasks;

namespace WS.Identity
{
    public interface ISmsSender
    {
        Task SendAsync(string number, string message);
    }
}