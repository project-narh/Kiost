using System.Threading.Tasks;

namespace Server.Services.IService
{
    public interface IKioskService
    {
        Task<string> GetStatusAsync();
        Task SetStatusAsync(string status);
    }
}