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
    [ApiVersion("2.0")]
    [ApiController]
    //[ApiExplorerSettings(GroupName= "NationalParksAPI")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class NationalParksV2Controller : ControllerBase
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
    {
        private readonly INationalParkRepository _nationalPark;
        private readonly IMapper _mapper;
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public NationalParksV2Controller(INationalParkRepository nationalPark, IMapper mapper)
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
            var parks = _nationalPark.GetNationalParks().FirstOrDefault();
            
            return Ok(_mapper.Map<NationalParkDto>(parks));
        }
    }
}