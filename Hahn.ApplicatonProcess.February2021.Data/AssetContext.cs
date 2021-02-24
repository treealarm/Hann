using Hahn.ApplicatonProcess.February2021.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hahn.ApplicatonProcess.February2021.Data
{
    public class AssetContext : DbContext
    {
        public AssetContext()
        {

        }
        DbSet<Asset> _Assets = null;
        
        public DbSet<Asset> Assets
        {
            get
            {
                return _Assets;
            }
            set
            {
                _Assets = value;
            }
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase("AssetDb");
        }

    }
}
