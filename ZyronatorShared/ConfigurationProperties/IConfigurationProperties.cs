using System;

namespace ZyronatorShared.ConfigurationProperties
{
    public interface IConfigurationProperties
    {
        string ConnectionString { get; }
        DateTime CurrentDate {get;}
    }
}
