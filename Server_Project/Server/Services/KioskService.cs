namespace Server.Services
{
    public class KioskService : IKioskService
    {
        private string _status = "on";

        public string GetStatus()
        {
            return _status;
        }

        public void SetStatus(string status)
        {
            _status = status;
        }
    }

}
