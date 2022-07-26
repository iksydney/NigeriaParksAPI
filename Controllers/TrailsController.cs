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
    //[Route("api/Trails")]
    [Route("api/v{version:apiVersion}/trails")]
    [ApiController]
    //[ApiExplorerSettings(GroupName = "NationalParkTrails")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class TrailsController : ControllerBase
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
    {
        private readonly ITrailRepository _trailRepo;
        private readonly IMapper _mapper;
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public TrailsController(ITrailRepository trailRepo, IMapper mapper)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        {
            _trailRepo = trailRepo;
            _mapper = mapper;
        }

        /// <summary>
        /// Get List of Trails 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<TrailDto>))]
        [ProducesResponseType(400)]
        public IActionResult GetTrails(){
            var trail = _trailRepo.GetTrails();
            if(trail == null){
                return NotFound();
            }

            var trailDto = new List<TrailDto>();

            foreach(var obj in trail)
            {
                trailDto.Add(_mapper.Map<TrailDto>(obj));
            }
            return Ok(trailDto);
        }

        /// <summary>
        /// Get Individual Trail
        /// </summary>
        /// <param name="Id"> The Id of the Trail</param>
        /// <returns></returns>
        [ProducesResponseType(200, Type = typeof(List<TrailDto>))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesDefaultResponseType]
        [HttpGet("{Id}", Name ="GetTrail")]
        public IActionResult GetTrail(int Id)
        {
            var trail = _trailRepo.GetTrails(Id);
            if(trail == null)
            {
                return NotFound();
            }
            var trailDto = _mapper.Map<TrailDto>(trail);
            /*Without AutoMapper*/
            /*var parksDto = new NationalParkDto()
            {
                Established = park.Established,
                Id = Id,
                Name = park.Name,
                State = park.State,
            };*/
            return Ok(trailDto);
        }

        /// <summary>
        /// Add a Park the List of Trail
        /// </summary>
        /// <param name="trailDto"></param>
        /// <returns></returns>
        [ProducesResponseType(201, Type = typeof(List<TrailDto>))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        [HttpPost]
        public IActionResult CreateTrail([FromBody]TrailCreateDto trailDto)
        {
            if(trailDto == null)
            {
                return BadRequest();
            }
            if( _trailRepo.TrailExists(trailDto.Name))
            {
                ModelState.AddModelError("", "Trail already Exists in our database");
                return StatusCode(404, ModelState);
            }
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            var trailObj = _mapper.Map<Trail>(trailDto);

            if(!_trailRepo.CreateTrail(trailObj))
            {
                ModelState.AddModelError("", $"Something went wrong saving the record {trailObj.Name}");
                return StatusCode(500, ModelState);
            }
            return CreatedAtRoute("trailDto", 
            new 
            {
                trailId = trailObj.Id
                }, 
                trailObj);
        }

        /// <summary>
        /// Update trail using the Id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="TrailDto"></param>
        /// <returns></returns>
        [HttpPatch("{id}", Name="UpdateTrail")]
        [ProducesResponseType(204)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult UpdateTrail(int id, TrailUpdateDto trailDto)
        {
            if(trailDto == null || id != trailDto.Id)
            {
                return BadRequest(ModelState);
            }
            var trailObj = _mapper.Map<Trail>(trailDto);
            if(!_trailRepo.UpdateTrail(trailObj))
            {
                ModelState.AddModelError("", $"Something went wrong updating record {trailObj.Name}");
                return StatusCode(500, ModelState);
            }
            //_trailRepo.Save();
            return Ok();
        }

        /// <summary>
        /// Delete a Trail
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        [HttpDelete("{id}", Name = "DeleteTrail")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteTrail(int id)
        {
            if (!_trailRepo.TrailExists(id))
            {
                return NotFound();
            }
            var trailObj = _trailRepo.GetTrails(id);
            if (!_trailRepo.DeleteTrail(trailObj))
            {
                ModelState.AddModelError("", $"Something went wrong deleting record {trailObj.Name}");
                return StatusCode(500, ModelState);
            }
            //_trailRepo.Save();
            return Ok();
        }
    }
}