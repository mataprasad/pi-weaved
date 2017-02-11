using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CaptureMyCurrentIp.Models
{
    public class Db
    {
        public bool AddLog(string ip, Guid guid)
        {
            using (var db = new DbIpLogDataContext())
            {
                var obj = db.DtIpLogs.Where(P => P.Guid == guid).FirstOrDefault();
                if (obj != null)
                {
                    obj.Ip = ip;
                    obj.RecordedAt = DateTime.UtcNow;
                }
                else
                {
                    db.DtIpLogs.InsertOnSubmit(new DtIpLog() { Guid = guid, Ip = ip, RecordedAt = DateTime.UtcNow });
                }
                db.SubmitChanges();
            }
            return true;
        }

        public string Get(Guid guid)
        {
            using (var db = new DbIpLogDataContext())
            {
                var obj = db.DtIpLogs.Where(P => P.Guid == guid).OrderByDescending(P => P.Id).FirstOrDefault();
                if (obj != null)
                {
                    return obj.Ip;
                }
            }
            return null;
        }
    }
}