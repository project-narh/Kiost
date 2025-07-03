using Server.Models;
using Server.Database;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Server.Services.IService
{
    public interface ITableService
    {
        // 테이블 운영
        Task<List<Table>> GetAvailableTablesAsync();
        Task<List<TableStatus>> GetTableStatusAsync();
        Task<bool> EnterTableAsync(TableEnterRequest request);
        Task<int> OrderAsync(TableOrderRequest request);
        Task<bool> ExitTableAsync(TableExitRequest request);
        Task ForceExitTableAsync(TableForceExitRequest request);

        // 테이블 설정 관리
        Task<List<Table>> GetTableConfigAsync();
        Task<int> AddTableAsync(TableConfigRequest request);
        Task<bool> UpdateTableAsync(int id, TableConfigRequest request);
        Task<bool> DeleteTableAsync(int id);
    }
}
