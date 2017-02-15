using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CaptureMyCurrentIp.Models
{
    public class WeavedConnectRequest
    {
        public string deviceaddress { get; set; }
        public string hostip { get; set; }
        public string wait { get; set; }
    }
    public class WeavedLoginResponse
    {
        public string status { get; set; }
        public string token { get; set; }
        public string email { get; set; }
        public string guid { get; set; }
        public string service_token { get; set; }
        public string service_level { get; set; }
        public string storage_plan { get; set; }
        public string secondary_auth { get; set; }
        public string apikey { get; set; }
        public string auth_token { get; set; }
        public int auth_expiration { get; set; }
        public string service_authhash { get; set; }
        public string developer_plan { get; set; }
        public string portal_plan { get; set; }
        public string portal_plan_expires { get; set; }
        public string service_features { get; set; }
    }

    public class WeavedDeviceListResponse
    {
        public string status { get; set; }
        public List<Device> devices { get; set; }
    }

    public class WeavedConnectResponse
    {
        public string status { get; set; }
        public Connection connection { get; set; }
    }

    public class Connection
    {
        public string deviceaddress { get; set; }
        public string expirationsec { get; set; }
        public string imageintervalms { get; set; }
        public string proxy { get; set; }
        public string requested { get; set; }
        public string status { get; set; }
        public List<object> streamscheme { get; set; }
        public List<object> streamuri { get; set; }
        public List<object> url { get; set; }
    }

    public class Device
    {
        public string deviceaddress { get; set; }
        public string devicealias { get; set; }
        public string ownerusername { get; set; }
        public string devicetype { get; set; }
        public string devicestate { get; set; }
        public string devicelastip { get; set; }
        public string lastinternalip { get; set; }
        public string servicetitle { get; set; }
        public string webenabled { get; set; }
        public List<object> weburi { get; set; }
        public string localurl { get; set; }
        public List<object> webviewerurl { get; set; }
    }   
}