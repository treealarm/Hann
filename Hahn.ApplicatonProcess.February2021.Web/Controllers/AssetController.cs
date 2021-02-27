using Hahn.ApplicatonProcess.February2021.Data;
using Hahn.ApplicatonProcess.February2021.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
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
        private readonly IConfiguration _config;

        public AssetController(ILogger<AssetController> logger, IUnitOfWork<Asset> repo, IConfiguration configuration)
        {
            _logger = logger;
            _repo = repo;
            _config = configuration;
        }

        
        string GetLocalizedString(string str_id)
        {
            return AssetValidator.GetLocalizedString(str_id, _config);
        }
        [HttpGet]
        [Route("GetOSs")]
        public string GetOperatingSystem()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                return "Linux";
            }

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                return "Windows";
            }

            return GetLocalizedString("unknown_system");
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
        public ActionResult<Asset> CreateAsset([FromBody] Asset inAsset)
        {
            if(inAsset.ID > 0)
            {
                var tempAsset = _repo.GetById(inAsset.ID).Result;
                if(tempAsset != null)
                {
                    return BadRequest(GetLocalizedString("id_already_exists"));
                }
            }
            

            var retAsset = _repo.Create(inAsset).Result;
            if(retAsset == null)
            {
                _logger.LogError("unable to create");
                return BadRequest(GetLocalizedString("unable_to_create"));
            }
            
            return CreatedAtAction(@"GetById", new { asset_id = retAsset.ID }, retAsset);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult UpdateAsset(Asset inAsset)
        {
            var tempAsset = _repo.GetById(inAsset.ID).Result;
            if (tempAsset == null)
            {
                _logger.LogError("id_not_exist");
                return BadRequest(GetLocalizedString("id_not_exist"));
            }

            var res = _repo.Update(inAsset).Result;
            if (res != EN_RETCODE.OK)
            {
                _logger.LogError("update failed");
                return BadRequest(GetLocalizedString("update_failed"));
            }

            return Ok();
        }

        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpDelete("{idAsset}", Name = "DeleteAsset")]
        public ActionResult DeleteAsset(int idAsset)
        {
            var tempAsset = _repo.GetById(idAsset).Result;
            if (tempAsset == null)
            {
                _logger.LogError("id not exist");
                return BadRequest(GetLocalizedString("id_not_exist"));
            }

            var res = _repo.Delete(idAsset).Result;
            if (res != EN_RETCODE.OK)
            {
                _logger.LogError("delete failed");
                return BadRequest(GetLocalizedString("delete_failed"));
            }

            return Ok();
        }
    }
}
