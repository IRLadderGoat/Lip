using System;
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace lip
{
    class IpGet
    {
        public string LocalIP, ExternalIP;
        private Random random = new Random();

        public IpGet(){
            CheckIPs();
        }
        public void CheckIPs()
        {
            IPAddress[] localIPs = Dns.GetHostAddresses(Dns.GetHostName());
            foreach (IPAddress a in localIPs)
            {
                if (IPAddress.Parse(a.ToString()).AddressFamily == AddressFamily.InterNetwork)
                {
                    LocalIP = a.ToString();
                }
            }
            
            using (WebClient webClient = new WebClient())
            {
                try
                {
                    webClient.CachePolicy = new System.Net.Cache.RequestCachePolicy(System.Net.Cache.RequestCacheLevel.NoCacheNoStore);
                    ExternalIP = webClient.DownloadString("http://icanhazip.com/");

                }catch (WebException e)
                {
                    System.Windows.Forms.MessageBox.Show("Try again in a second \n\n Error Message: \n" + e.Message);
                }
            }
        }
        public bool CheckPort(string port)
        {
            if(port == "")
            {
                return false;
            }
            using (TcpClient client = new TcpClient())
            {
                try
                {
                    client.Connect(IPAddress.Loopback, Convert.ToInt32(port));
                }
                catch (SocketException)
                {
                    return false;
                }
                client.Close();
                return true;
            }
        }
    }
}
