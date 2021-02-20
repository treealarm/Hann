using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hahn.ApplicatonProcess.February2021.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AssetController : ControllerBase
    {

        private readonly ILogger<AssetController> _logger;

        public AssetController(ILogger<AssetController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<Asset> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new Asset
            {
                ID = index
            })
            .ToArray();
        }
    }
}
