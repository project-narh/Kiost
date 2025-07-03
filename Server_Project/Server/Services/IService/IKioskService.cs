namespace Server.Services
{
    public interface IKioskService
    {
        string GetStatus();
        void SetStatus(string status);
    }
}
