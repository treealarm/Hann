using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hahn.ApplicatonProcess.February2021.Domain
{
    public class UnitOfWork : IUnitOfWork<Asset>
    {
        private readonly IRepository<Asset> assetRepository;

        public UnitOfWork(IRepository<Asset> inRepository)
        {
            this.assetRepository = inRepository;
        }
        public Task<Asset> Create(Asset item)
        {
            return assetRepository.Create(item);
        }

        public Task<EN_RETCODE> Delete(int id)
        {
            return assetRepository.Delete(id);
        }

        public Task<IEnumerable<Asset>> GetAll()
        {
            return assetRepository.GetAll();
        }

        public Task<Asset> GetById(int id)
        {
            return assetRepository.GetById(id);
        }

        public Task<EN_RETCODE> Update(Asset item)
        {
            return assetRepository.Update(item);
        }
    }
}
