using Microsoft.AspNetCore.Mvc;
using SeatManagement.CustomException;
using SeatManagement.DTO;
using SeatManagement.Interface;
using SeatManagement.Models;
using System.ComponentModel;

namespace SeatManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssetController : Controller
    {
        private readonly IAssetService _assetService;

        public AssetController(IAssetService assetService)
        {
            this._assetService= assetService;
        }
        [HttpGet] //Get all asset
        public IActionResult Get()
        {
            return Ok(_assetService.Get());
        }
        [HttpPost] //Add asset 
        public IActionResult AddAsset(List<LookUpAssetDTO> AssetDTOList)
        {
            _assetService.Add(AssetDTOList);
            return Ok();
        }
    }
}

//[HttpPost("BulkAdd")]
//public IActionResult Post(List<LookUpAssetDTO> AssetDTOList)
//{
//    _repositary.Add(AssetDTOList);
//    return Ok();
//}
