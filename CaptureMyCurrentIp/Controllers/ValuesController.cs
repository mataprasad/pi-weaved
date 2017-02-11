using CaptureMyCurrentIp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CaptureMyCurrentIp.Controllers
{
    public class ValuesController : ApiController
    {
        private Db _db = new Db();
        public ip_upload_reponse Get(string ip, Guid guid)
        {
            ip_upload_reponse result = null;
            try
            {
                _db.AddLog(ip, guid);
                result = new ip_upload_reponse() { message = "Current ip captured successfully." };
            }
            catch (Exception ex)
            {
                result = new ip_upload_reponse() { message = ex.Message };
            }
            return result;
        }

        public string Get(Guid guid)
        {
            return _db.Get(guid);
        }
    }
}