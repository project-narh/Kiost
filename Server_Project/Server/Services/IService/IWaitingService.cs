using Server.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Server.Services.IService
{
    public interface IWaitingService
    {
        Task<int> GetWaitTimeAsync(int people);
        Task<List<WaitingEntry>> GetWaitingListAsync();
    }
}