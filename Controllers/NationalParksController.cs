using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ParkAPI.Dtos;
using ParkAPI.Models;
using ParkAPI.Repository;

namespace ParkAPI.Controllers
{
    [Route("api/nationalparks")]
    [ApiController]
    public class NationalParksController : ControllerBase
    {
        private readonly INationalParkRepository _nationalPark;
        private readonly IMapper _mapper;
        public NationalParksController(INationalParkRepository nationalPark, IMapper mapper)
        {
            _nationalPark = nationalPark;
            _mapper = mapper;
        }
        [HttpGet]
        public IActionResult GetNationalParks(){
            var parks = _nationalPark.GetNationalParks();
            if(parks == null){
                return NotFound();
            }

            var parkDto = new List<NationalParkDto>();

            foreach(var obj in parks)
            {
                parkDto.Add(_mapper.Map<NationalParkDto>(obj));
            }
            return Ok(parkDto);
        }
        [HttpGet("{Id}", Name ="GetNationalPark")]
        public IActionResult GetNationalPark(int Id)
        {
            var park = _nationalPark.GetNationalParks(Id);
            if(park == null)
            {
                return NotFound();
            }
            var parkDto = _mapper.Map<NationalParkDto>(park);
            return Ok(parkDto);
        }
        [HttpPost]
        public IActionResult CreateNationalPark([FromBody]NationalParkDto nationalParkDto)
        {
            if(nationalParkDto == null)
            {
                return BadRequest();
            }
            if( _nationalPark.NationalParkExists(nationalParkDto.Name))
            {
                ModelState.AddModelError("", "Parks already Exists in our database");
                return StatusCode(404, ModelState);
            }
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            var nationalParkObj = _mapper.Map<NationalPark>(nationalParkDto);

            if(!_nationalPark.CreateNationalPark(nationalParkObj))
            {
                ModelState.AddModelError("", $"Something went wrong saving the record {nationalParkObj.Name}");
                return StatusCode(500, ModelState);
            }
            return CreatedAtRoute("GetNationalPark", 
            new 
            {
                nationalParkId = nationalParkObj.Id
                }, 
                nationalParkObj);
        }
        [HttpPatch("{id}", Name="UpdatePark")]
        public IActionResult UpdatePark(int id, NationalParkDto nationalParkDto)
        {
            if(nationalParkDto == null || id != nationalParkDto.Id)
            {
                return BadRequest(ModelState);
            }
            var nationalParkObj = _mapper.Map<NationalPark>(nationalParkDto);
            if(!_nationalPark.UpdateNationalPark(nationalParkObj))
            {
                ModelState.AddModelError("", $"Something went wrong updating record {nationalParkObj.Name}");
                return StatusCode(500, ModelState);
            }
            //_nationalPark.Save();
            return Ok();
        }

        [HttpDelete("{id}", Name = "DeletePark")]
        public IActionResult DeletePark(int id)
        {
            if (!_nationalPark.NationalParkExists(id))
            {
                return NotFound();
            }
            var nationalParkObj = _nationalPark.GetNationalParks(id);
            if (!_nationalPark.DeleteNationalPark(nationalParkObj))
            {
                ModelState.AddModelError("", $"Something went wrong deleting record {nationalParkObj.Name}");
                return StatusCode(500, ModelState);
            }
            //_nationalPark.Save();
            return Ok();
        }
    }
}