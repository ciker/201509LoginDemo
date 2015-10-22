using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Data.Entity.ModelConfiguration.Conventions;
using Domain;

namespace Infrastructure
{
    public class CRMContext : DbContext
    {

        public DbSet<Account> Accounts { get; set; }
        public DbSet<Contact> Contacts { get; set; }


        public CRMContext()
        {
            this.Configuration.ProxyCreationEnabled = true;
            this.Configuration.AutoDetectChangesEnabled = true;
            Database.SetInitializer<CRMContext>(null);
        }

        public virtual void Commit()
        {
            base.SaveChanges();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Configure Code First to ignore PluralizingTableName convention
            // If you keep this convention then the generated tables will have pluralized names.
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        }
    }

    public class CRMContextInitializer : DropCreateDatabaseAlways<CRMContext>
    {
        //without using package manager console
        protected override void Seed(CRMContext context)
        {
            base.Seed(context);

            //Accounts Data
            var accounts = new List<Account>
                {
                    new Account()
                        {
                            Name = "User One",
                            AccountType = AccountTypeEnum.Customer,
                            AccountStatus = AccountStatusEnum.Active,
                         Contacts   = new List<Contact>()
                             {
                                 new Contact()
                        {
                            FirstName = "User One",
                            ContactType =ContactTypeEnum.Billing
                         
                        }
                             }
                        },
                         new Account()
                        {
                            Name = "User Two",
                            AccountType = AccountTypeEnum.Lead,
                            AccountStatus = AccountStatusEnum.Active,
                            Contacts = new List<Contact>()
                                {
                                        new Contact()
                        {
                            FirstName = "User Two",
                            ContactType =ContactTypeEnum.Main
                        }
                                }
                        }
                };
            accounts.ForEach(x => context.Accounts.AddOrUpdate(x));

        }
    }
}
