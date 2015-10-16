using System;
using System.Data.Entity.ModelConfiguration;
using System.Data.Entity.ModelConfiguration.Configuration;

using GMF.Component.Data;
using GMF.Demo.Core.Models;


namespace GMF.Demo.Core.Data.Configurations.Account
{
    partial class LoginLogConfiguration
    {
        partial void LoginLogConfigurationAppend()
        {
            HasRequired(m => m.Member).WithMany(n => n.LoginLogs);
        }
    }
}