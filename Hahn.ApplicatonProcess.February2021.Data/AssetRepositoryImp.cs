using Hahn.ApplicatonProcess.February2021.Domain;
using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Hahn.ApplicatonProcess.February2021.Data
{
    public interface IRepository<T> where T :  class
    {
        Task<T> Create(T item);
        Task<T> GetById(int id);
        Task<bool> Delete(int id);
        Task<(bool, int)> Update(int id, T item);

    }

    public class AssetRepositoryImp : IRepository<Asset>
    {
        private readonly AssetContext _assetContext;

        public AssetRepositoryImp(AssetContext ContextIn)
        {
            this._assetContext = ContextIn;
        }

        public async Task<Asset> Create(Asset item)
        {
            var db_row = await GetById(item.ID);
            if (db_row != null)
            {
                return default;
            }
            var new_db_row = await _assetContext.Assets.AddAsync(item);
            await _assetContext.SaveChangesAsync();
            return new_db_row.Entity;
        }

        public async Task<bool> Delete(int id)
        {
            var db_row = await GetById(id);
            if (db_row == null)
            {
                return false;
            }
            _assetContext.Assets.Remove(db_row);
            await _assetContext.SaveChangesAsync();
            return true;
        }

        public Task<Asset> GetById(int id)
        {
            return  _assetContext.Assets.FirstOrDefaultAsync(t => t.ID == id);
        }

        public async Task<(bool, int)> Update(int id, Asset item)
        {
            var db_row = await GetById(id);
            if (db_row == null)
            {
                return (false, default);
            }
            
            db_row.CopyFrom(item);
            await _assetContext.SaveChangesAsync();
            return (true, db_row.ID);
        }
    }
}
