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
            var rng = new Random();
            var data = Enumerable.Range(1, 5).Select(index => new Asset
            {
                ID = index,
                PurchaseDate = DateTime.Now
            })
            .ToArray();
            foreach(var item in data)
            {
                Assets.Add(item);
            }
            try
            {
                this.SaveChanges();
            }
            catch(Exception e)
            {

            }
        }
        public DbSet<Asset> Assets { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase("AssetDb");
        }

    }
}
