namespace Assets.Scripts.Placholders
{
    class ServerRequest
    {
        private string avalIP;
        public ServerRequest(string avalIP)
        {
            this.avalIP = avalIP;
        }
        public string getAvalIP()
        {
            return avalIP;
        }
    }
}