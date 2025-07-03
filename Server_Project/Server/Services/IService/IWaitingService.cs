namespace Server.Services.IService
{
    public interface IWaitingService
    {
        int GetWaitTime(int people);
        List<WaitingEntry> GetWaitingList();
    }

}
