using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ParkAPI.Dtos;
using ParkAPI.Models;
using ParkAPI.Repository;

namespace ParkAPI.Controllers
{
    [Route("api/v{version:apiVersion}/nationalparks")]
    //[Route("api/nationalparks")]
    [ApiController]
    //[ApiExplorerSettings(GroupName= "NationalParksAPI")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class NationalParksController : ControllerBase
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
    {
        private readonly INationalParkRepository _nationalPark;
        private readonly IMapper _mapper;
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public NationalParksController(INationalParkRepository nationalPark, IMapper mapper)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        {
            _nationalPark = nationalPark;
            _mapper = mapper;
        }

        /// <summary>
        /// Get List of National Parks
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<NationalParkDto>))]
        [ProducesResponseType(400)]
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

        /// <summary>
        /// Get Individual National Park
        /// </summary>
        /// <param name="Id"> The Id of the National Park</param>
        /// <returns></returns>
        [ProducesResponseType(200, Type = typeof(List<NationalParkDto>))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesDefaultResponseType]
        [HttpGet("{Id}", Name ="GetNationalPark")]
        public IActionResult GetNationalPark(int Id)
        {
            var park = _nationalPark.GetNationalParks(Id);
            if(park == null)
            {
                return NotFound();
            }
            var parkDto = _mapper.Map<NationalParkDto>(park);
            /*Without AutoMapper*/
            /*var parksDto = new NationalParkDto()
            {
                Established = park.Established,
                Id = Id,
                Name = park.Name,
                State = park.State,
            };*/
            return Ok(parkDto);
        }

        /// <summary>
        /// Add a Park the List of National parks
        /// </summary>
        /// <param name="nationalParkDto"></param>
        /// <returns></returns>
        [ProducesResponseType(201, Type = typeof(List<NationalParkDto>))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

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

        /// <summary>
        /// Update National park using the Id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="nationalParkDto"></param>
        /// <returns></returns>
        [HttpPatch("{id}", Name="UpdatePark")]
        [ProducesResponseType(204)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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

        /// <summary>
        /// Delete a Park
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        [HttpDelete("{id}", Name = "DeletePark")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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