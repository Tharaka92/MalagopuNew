using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GenericAPI.Models
{
    public class WebApiConfigModel
    {
        public int ServiceId { get; set; }
        public string RequestType { get; set; }
        public string ConnectionStringId { get; set; }
        public string ObjectToRetrieveData { get; set; }
        public string DbType { get; set; }
        public string ObjectToStoreData { get; set; }
        public string LatestBatchId { get; set; }
    }
}