using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassicArchitecture.Core.CrossCuttingConcerns.Logging.Elasticsearch
{
    public class ElasticEnvironmentManager
    {
        public static string ElasticUrl
        {
            get { return Environment.GetEnvironmentVariable("ELASTIC_URL") ?? ""; }
        }

        public static string ElasticPassword
        {
            get { return Environment.GetEnvironmentVariable("ELASTIC_PASSWORD") ?? ""; }
        }

        public static string ElasticUser
        {
            get { return Environment.GetEnvironmentVariable("ELASTIC_USER") ?? ""; }
        }

        public static string IndexFormat
        {
            get { return Environment.GetEnvironmentVariable("ELASTIC_INDEX_FORMAT") ?? ""; }
        }
    }
}
