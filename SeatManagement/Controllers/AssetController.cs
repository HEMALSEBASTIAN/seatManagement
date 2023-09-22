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
        private readonly IAssetService _repositary;

        public AssetController(IAssetService repositary)
        {
            _repositary=repositary;
        }
        [HttpGet] //Get all asset
        public IActionResult Get()
        {
            return Ok(_repositary.Get());
        }
        [HttpPost] //Add asset in bulk or single
        public IActionResult AddAsset(List<LookUpAssetDTO> AssetDTOList)
        {
            if(AssetDTOList.Count() == 1)
            {
               int AssetId= _repositary.Add(AssetDTOList[0]);
                return Ok(AssetId);
            }
            else
            {
                _repositary.Add(AssetDTOList);
                return Ok();
            }
        }
        
        
        
        
        //[HttpPost("BulkAdd")]
        //public IActionResult Post(List<LookUpAssetDTO> AssetDTOList)
        //{
        //    _repositary.Add(AssetDTOList);
        //    return Ok();
        //}
    }
}
