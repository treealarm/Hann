using Hahn.ApplicatonProcess.February2021.Data;
using Hahn.ApplicatonProcess.February2021.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hahn.ApplicatonProcess.February2021.Web.Controllers
{
    [ApiController]
    [Route("February2021Api")]
    public class AssetController : ControllerBase
    {

        private readonly ILogger<AssetController> _logger;
        private readonly IRepository<Asset> _repo;

        public AssetController(ILogger<AssetController> logger, IRepository<Asset> repo)
        {
            _logger = logger;
            _repo = repo;
        }

        [HttpGet]
        public IEnumerable<Asset> Get()
        {
            return  _repo.GetAll().Result;
        }

        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        //[Route("GetById/{id}")]
        
        [HttpGet ("{asset_id}", Name = "GetById")]
        public ActionResult<Asset> GetById(int asset_id)
        {
            Asset temp = _repo.GetById(asset_id).Result;
            if(temp == null)
            {
                return NotFound();
            }
            return Ok(temp);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Route("CreateAsset")]
        public ActionResult<Asset> CreateAsset([FromBody] Asset inAsset)
        {
            var retAsset = _repo.Create(inAsset).Result;
            if(retAsset == null)
            {
                return NotFound();
            }
            
            return CreatedAtAction(@"GetById", new { asset_id = retAsset.ID }, retAsset);
        }

    }
}
