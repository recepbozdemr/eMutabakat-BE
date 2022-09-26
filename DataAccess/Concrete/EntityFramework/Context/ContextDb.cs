using Core.Entities.Concrate;
using Entities.Concrate;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework.Context
{
    public class ContextDb:DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) //DB BAĞLANTISI
        {
            optionsBuilder.UseSqlServer(@"Server=DESKTOP-H1DE4VH\SQLEXPRESS;Database=eMutabakatDB;Integrated Security=true");
        }

        public DbSet<AccountReconciliationDetail> AccountReconciliationDetails { get; set; }

        public DbSet<AccountReconiliation> AccountReconiliations { get; set; }

        public DbSet<BaBsReconciliation> BaBsReconciliations { get; set; }

        public DbSet<BaBsReconciliationsDetail> BaBsReconciliationsDetail { get; set; }

        public DbSet<Company> Companies { get; set; }

        public DbSet<Currency> Currencies { get; set; }

        public DbSet<CurrencyAccount> CurrencyAccounts { get; set; }

        public DbSet<OperationsClaim> OperationsClaims { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<UserCompany> UserCompanies { get; set; }

        public DbSet<UserOperationClaim> UserOperationClaims { get; set; }
    }
}
