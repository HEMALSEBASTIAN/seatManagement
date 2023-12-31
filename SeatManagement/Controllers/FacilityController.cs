﻿using Microsoft.AspNetCore.Mvc;
using SeatManagement.DTO;
using SeatManagement.Interface;
using SeatManagement.Models;

namespace SeatManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FacilityController : Controller
    {
        private readonly IFacilityService _repositary;

        public FacilityController(IFacilityService repositary)
        {
            _repositary = repositary;
        }
        [HttpGet]  //get all faciltiy details
        public IActionResult Get()
        {
            return Ok(_repositary.Get());
        }
        [HttpPost] //Add facility
        public IActionResult Post(FacilityDTO facilityDTO)
        {
            
            int FacilityId=_repositary.Add(facilityDTO);
            return Ok(FacilityId);
        }





        [Route("id")]
        [HttpGet] //get a faciltiy detail by id
        public IActionResult Get(int id)
        {
            var item= _repositary.GetById(id);
            if(item==null)
                return NotFound();
            return Ok(item);
        }
        [Route("id")]
        [HttpPatch] //update a facility
        public IActionResult Update(Facility facility)
        {
            var item=_repositary.Update(facility);
            if(item==null)
                return NotFound();
            return Ok();
        }

        //[HttpGet("view")]
        //public IActionResult View()
        //{
        //    var item = _repositary.GetView();
        //    return Ok(item);
        //}
    }
}
