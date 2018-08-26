using System;
using System.Net;
using System.Net.Sockets;

namespace lip
{
    class IpGet
    {
        public static string CheckLocalIP()
        {
            IPAddress[] localIPs = Dns.GetHostAddresses(Dns.GetHostName());
            foreach (IPAddress a in localIPs)
            {
                if (IPAddress.Parse(a.ToString()).AddressFamily == AddressFamily.InterNetwork)
                {
                    return a.ToString();
                }
            }
            return CheckLocalIP();
        }
        public static string CheckExternalIP()
        {
            try
            {
                using (WebClient webClient = new WebClient())
                {

                    webClient.CachePolicy = new System.Net.Cache.RequestCachePolicy(System.Net.Cache.RequestCacheLevel.NoCacheNoStore);
                    return webClient.DownloadString("http://icanhazip.com/");
                }
            }
            catch (Exception)
            {
                return CheckExternalIP();
            }
        }
        public bool CheckPort(int port)
        {
            if (port <= 0 || port >= 65535) { return false; }
            else
            {
                try
                {
                    using (TcpClient client = new TcpClient())
                    {
                        var cl = client.BeginConnect(IPAddress.Parse(CheckLocalIP()), port, null, null);
                        var kk = cl.AsyncWaitHandle.WaitOne(new TimeSpan(0, 0, 10));
                        if (!kk)
                        {
                            return false;
                        }
                        client.EndConnect(cl);
                    }
                }
                catch (SocketException)
                {
                    return false;
                }
                return true;
            }
        }
    }
}
