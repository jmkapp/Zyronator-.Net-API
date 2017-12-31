using System;
using System.Web.Configuration;
using ZyronatorShared.ConfigurationProperties;

namespace Zyronator.ConfigurationProperties
{
    public class ConfigurationProperties : IConfigurationProperties
    {
        private readonly string _connectionString;

        public ConfigurationProperties()
        {
            _connectionString = WebConfigurationManager.ConnectionStrings["DatabaseConnection"].ConnectionString;
        }
        public string ConnectionString
        {
            get
            {
                return _connectionString;
            }
        }

        public DateTime CurrentDate
        {
            get
            {
                return DateTime.Now;
            }
        }
    }
}