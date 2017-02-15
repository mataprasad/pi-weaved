using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json;
using CaptureMyCurrentIp.Models;

namespace CaptureMyCurrentIp.Controllers
{
    public class PiWeavedController : ApiController
    {
        public System.Net.Http.HttpResponseMessage Get(string id, string pwd, string device)
        {
            Device obj_device = null;
            //Login to get token
            var loginResponse = JsonConvert.DeserializeObject<WeavedLoginResponse>(LoadHttpResponse(String.Format("https://api.weaved.com/v22/api/user/login/{0}/{1}", id, pwd)));

            if (loginResponse == null || string.IsNullOrWhiteSpace(loginResponse.token))
            {
                return new HttpResponseMessage(HttpStatusCode.InternalServerError) { Content = new StringContent("Login failed.") };
            }

            //Get device list
            var listResponse = JsonConvert.DeserializeObject<WeavedDeviceListResponse>(LoadHttpResponse("https://api.weaved.com/v22/api/device/list/all", token: loginResponse.token));

            if (listResponse != null && listResponse.status == "true" && listResponse.devices != null && listResponse.devices.Count > 0)
            {
                obj_device = listResponse.devices.Where(P => String.Equals(P.devicealias, device, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
                if (obj_device == null)
                {
                    return new HttpResponseMessage(HttpStatusCode.InternalServerError) { Content = new StringContent("No device found with alias - " + device) };
                }
            }
            else
            {
                return new HttpResponseMessage(HttpStatusCode.InternalServerError) { Content = new StringContent("No device found.") };
            }

            var connectRequest = new WeavedConnectRequest();
            connectRequest.wait = "true";
            connectRequest.deviceaddress = obj_device.deviceaddress;
            connectRequest.hostip = obj_device.devicelastip;

            //connect to device
            var connectResponse = JsonConvert.DeserializeObject<WeavedConnectResponse>(LoadHttpResponse("https://api.weaved.com/v22/api/device/connect", token: loginResponse.token, method: "POST", data: JsonConvert.SerializeObject(connectRequest)));

            return new HttpResponseMessage(HttpStatusCode.OK) { Content = new ObjectContent<WeavedConnectResponse>(connectResponse, new System.Net.Http.Formatting.JsonMediaTypeFormatter()) };
        }

        private string LoadHttpResponse(string url, string method = "GET", string token = "", string data = "")
        {
            var response = string.Empty;
            using (var client = new System.Net.WebClient())
            {
                client.Headers.Add("User-Agent", "Mozilla/5.0 (Windows NT 6.3) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/56.0.2924.87 Safari/537.36 DotNet/4.0");
                client.Headers.Add("Content-Type", "application/json");
                client.Headers.Add("apikey", "WeavedDemoKey$2015");
                if (!String.IsNullOrWhiteSpace(token))
                {
                    client.Headers.Add("token", token);
                }

                if (method == "POST")
                {
                    response = client.UploadString(url, "POST", data);
                }
                else
                {
                    response = client.DownloadString(url);
                }
            }

            return response;
        }
    }
}
