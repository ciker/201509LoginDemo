using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Linq;
using System.Text;

using GMF.Component.Data;
using GMF.Demo.Core.Models.Account;


namespace GMF.Demo.Core.Data.Configurations
{
    public class MemberAddressConfiguration : ComplexTypeConfiguration<MemberAddress>, IEntityMapper
    {
        public MemberAddressConfiguration()
        {
            Property(m => m.Province).HasColumnName("Province");
            Property(m => m.City).HasColumnName("City");
            Property(m => m.County).HasColumnName("County");
            Property(m => m.Street).HasColumnName("Street");
        }

        public void RegistTo(ConfigurationRegistrar configurations)
        {
            configurations.Add(this);
        }
    }
}
