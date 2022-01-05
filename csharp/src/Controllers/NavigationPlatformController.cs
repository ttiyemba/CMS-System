using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using src.Dto;
using AutoMapper;
using src.Services;
using Microsoft.AspNetCore.Mvc;
using src.Persistence.Models;

namespace src.Controllers
{
    [Route("api/platform")]
    [ApiController]
    public class NavigationPlatformController : ControllerBase
    {
        private readonly NavigationPlatformService _navigationPlatformService;
        private readonly IMapper _mapper;

        public NavigationPlatformController(NavigationPlatformService navigationPlatformService, IMapper mapper)
        {
            _navigationPlatformService = navigationPlatformService;
            _mapper = mapper;
        }

        // GET: api/platform
        [HttpGet]
        public async Task<NavigationPlatformDto> GetPlatform()
        {
            NavigationPlatformDto platformDto = _mapper.Map<NavigationPlatformDto>(await _navigationPlatformService.GetPlatform());
            return platformDto;
        }
    }
}
