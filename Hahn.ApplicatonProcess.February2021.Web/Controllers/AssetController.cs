using Hahn.ApplicatonProcess.February2021.Data;
using Hahn.ApplicatonProcess.February2021.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace Hahn.ApplicatonProcess.February2021.Web.Controllers
{
    [ApiController]
    [Route("February2021Api")]
    [Consumes("application/json")]
    [Produces("application/json")]
    public class AssetController : ControllerBase
    {

        private readonly ILogger<AssetController> _logger;
        private readonly IUnitOfWork<Asset> _repo;

        public AssetController(ILogger<AssetController> logger, IUnitOfWork<Asset> repo)
        {
            _logger = logger;
            _repo = repo;
        }

        [HttpGet]
        [Route("GetOSs")]
        public string GetOperatingSystem()
        {

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                _logger.LogInformation("determine Linux");
                return "Linux";
            }

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                _logger.LogError("unable to create");
                _logger.LogInformation("determine Windows");
                return "Windows";
            }

            _logger.LogError("Cannot determine operating system!");
            return ("Cannot determine operating system!");
        }

        [HttpGet]
        [Route("GetAllAssets")]
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
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Route("CreateAsset")]
        public ActionResult<Asset> CreateAsset([FromBody] Asset inAsset)
        {
            if(inAsset.ID > 0)
            {
                var tempAsset = _repo.GetById(inAsset.ID).Result;
                if(tempAsset != null)
                {
                    return BadRequest("id already exist");
                }
            }
            

            var retAsset = _repo.Create(inAsset).Result;
            if(retAsset == null)
            {
                _logger.LogError("unable to create");
                return BadRequest("unable to create");
            }
            
            return CreatedAtAction(@"GetById", new { asset_id = retAsset.ID }, retAsset);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Route("UpdateAsset")]
        public ActionResult UpdateAsset(Asset inAsset)
        {
            var tempAsset = _repo.GetById(inAsset.ID).Result;
            if (tempAsset == null)
            {
                return BadRequest("id not exist");
            }

            var res = _repo.Update(inAsset).Result;
            if (res != EN_RETCODE.OK)
            {
                return BadRequest("update failed");
            }

            return Ok();
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Route("DeleteAsset/{idAsset}")]
        public ActionResult DeleteAsset(int idAsset)
        {
            var tempAsset = _repo.GetById(idAsset).Result;
            if (tempAsset == null)
            {
                return BadRequest("id not exist");
            }

            var res = _repo.Delete(idAsset).Result;
            if (res != EN_RETCODE.OK)
            {
                return BadRequest("delete failed");
            }

            return Ok();
        }
    }
}
