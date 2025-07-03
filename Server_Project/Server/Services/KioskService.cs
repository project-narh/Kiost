using Server.Services.IService;
using System.Threading.Tasks;

namespace Server.Services
{
    public class KioskService : IKioskService
    {
        private string _status = "on";

        public async Task<string> GetStatusAsync()
        {
            return await Task.FromResult(_status);
        }

        public async Task SetStatusAsync(string status)
        {
            _status = status;
            await Task.CompletedTask;
        }
    }
}