using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using src.Data;
using src.Persistence.Models;
using src.Dto;
using AutoMapper;
using src.Services;

namespace src.Controllers
{
    [Route("api/plottable")]
    [ApiController]
    public class PlottableEntitiesController : ControllerBase
    {
        private readonly PlottableEntityService _plottableEntityService;
        private readonly NavigationPlatformService _navigationPlatformService;
        private readonly IMapper _mapper;

        public PlottableEntitiesController(PlottableEntityService plottableEntityService, NavigationPlatformService navigationPlatformService, IMapper mapper)
        {
            _plottableEntityService = plottableEntityService;
            _navigationPlatformService = navigationPlatformService;
            _mapper = mapper;
        }

        // GET: PlottableEntities
        [HttpGet]
        public async Task<List<PlottableEntityWithDistanceAndMoveableEntityDto>> GetPlottableEntitiesAsync()
        {
            List<PlottableEntity> plottables = await _plottableEntityService.GetAllPlottablesAsync();
            NavigationPlatformDto platformDto = _mapper.Map<NavigationPlatformDto>(await _navigationPlatformService.GetPlatform());
            List<PlottableEntityWithDistanceAndMoveableEntityDto> plottablesWithDistance = _plottableEntityService.GetDistances(platformDto, plottables);
            return plottablesWithDistance;
        }

        // GET: PlottableEntities/Details/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PlottableEntityWithDistanceAndMoveableEntityDto>> GetPlottableEntity(int id)
        {
            try
            {
                PlottableEntity plottableEntity = await _plottableEntityService.GetPlottableAsync(id);
                List<PlottableEntity> plottables = new List<PlottableEntity>();
                plottables.Add(plottableEntity);
                NavigationPlatformDto platformDto = _mapper.Map<NavigationPlatformDto>(await _navigationPlatformService.GetPlatform());
                List<PlottableEntityWithDistanceAndMoveableEntityDto> plottablesWithDistance = _plottableEntityService.GetDistances(platformDto, plottables);
                return plottablesWithDistance[0];
            }
            catch (InvalidOperationException)
            {
                PlottableEntityWithDistanceAndMoveableEntityDto noMatchingEntity = new PlottableEntityWithDistanceAndMoveableEntityDto();
                noMatchingEntity.Identifier = "";
                noMatchingEntity.Classification = "";
                noMatchingEntity.Subclassification = "";
                noMatchingEntity.Hostility = "";
                noMatchingEntity.MoveableEntity = new MoveableEntityDto();
                return noMatchingEntity;
            }
        }

        [HttpPut("{id}")]

        public async Task<int> UpdatePlottable(int id, string hostility, string classification, string subclassification, string identifier)
        {
            try
            {
                PlottableEntityDto plottableEntityDto = _mapper.Map<PlottableEntityDto>(await _plottableEntityService.GetPlottableAsync(id));

                plottableEntityDto.Classification = classification ?? plottableEntityDto.Classification;
                plottableEntityDto.Hostility = hostility ?? plottableEntityDto.Hostility;
                plottableEntityDto.Subclassification = subclassification ?? plottableEntityDto.Subclassification;
                plottableEntityDto.Identifier = identifier ?? plottableEntityDto.Identifier;

                return await _plottableEntityService.UpdatePlottableEntityAsync(plottableEntityDto);
            }
            catch (InvalidOperationException)
            {
                return -1;
            }
        }


        [HttpPost]
        public async Task<int> PostPlottableEntity(PlottableEntityWithVectorDto plottableEntityWithVectorDto)
        {
            try {
                PlottableEntity plottableEntity = _mapper.Map<PlottableEntity>(plottableEntityWithVectorDto);
                MoveableEntity moveableEntity = _mapper.Map<MoveableEntity>(plottableEntityWithVectorDto.MoveableEntity);
                plottableEntity.MoveableEntity = moveableEntity;
                return await _plottableEntityService.AddPlottableAsync(plottableEntity);
            }
            catch (DbUpdateException)
            {
                return -1;
            }
        }

        [HttpDelete("{id}")]
        public async Task<int> DeletePlottableEntity(int id)
        {
            try
            {
                return await _plottableEntityService.DeletePlottableByIdAsync(id);
            }
            catch (InvalidOperationException)
            {
                return -1;
            }
        }
    }
}
