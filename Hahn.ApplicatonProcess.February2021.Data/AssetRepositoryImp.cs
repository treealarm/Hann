using Hahn.ApplicatonProcess.February2021.Domain;
using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Collections.Generic;

namespace Hahn.ApplicatonProcess.February2021.Data
{

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
            try
            {
                await _assetContext.SaveChangesAsync();
            }
            catch(Exception)
            {
                return default;
            }
            
            return new_db_row.Entity;
        }

        public async Task<EN_RETCODE> Delete(int id)
        {
            var db_row = await GetById(id);
            if (db_row == null)
            {
                return EN_RETCODE.FAILED;
            }
            _assetContext.Assets.Remove(db_row);
            await _assetContext.SaveChangesAsync();
            return EN_RETCODE.OK;
        }

        public Task<Asset> GetById(int id)
        {
            return  _assetContext.Assets.FirstOrDefaultAsync(t => t.ID == id);
        }

        public async Task<EN_RETCODE> Update(Asset item)
        {
            var db_row = await GetById(item.ID);
            if (db_row == null)
            {
                return EN_RETCODE.FAILED;
            }
            
            db_row.CopyFrom(item);
            await _assetContext.SaveChangesAsync();
            return EN_RETCODE.OK;
        }
        public async Task<IEnumerable<Asset>> GetAll()
        {
            var ret = await _assetContext.Assets.ToListAsync();
            return ret;
        }
    }
}
