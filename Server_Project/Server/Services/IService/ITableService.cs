namespace Server.Services.IService
{
    public interface ITableService
    {
        List<Table> GetAvailableTables();
        List<TableStatus> GetTableStatus();
        void EnterTable(TableEnterRequest request);
        void Order(TableOrderRequest request);
        void ExitTable(TableExitRequest request);
        void ForceExitTable(TableForceExitRequest request);

        // 설정
        List<Table> GetTableConfig();
        void AddTable(TableConfigRequest request);
        void UpdateTable(int id, TableConfigRequest request);
        void DeleteTable(int id);
    }

}
