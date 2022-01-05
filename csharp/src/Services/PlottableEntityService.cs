using AutoMapper;
using src.Dto;
using src.Persistence.Models;
using src.Persistence.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using src.Enums;

namespace src.Services
{
    public class PlottableEntityService
    {
        private readonly IRepository<PlottableEntity> _plottableRepository;
        private readonly NavigationPlatformService _navigationPlatformService;
        private readonly IMapper _mapper;
        public PlottableEntityService(IRepository<PlottableEntity> plottableRepository, NavigationPlatformService navigationPlatformService, IMapper mapper)
        {
            _plottableRepository = plottableRepository;
            _navigationPlatformService = navigationPlatformService;
            _mapper = mapper;
        }

        public async Task<List<PlottableEntity>> GetAllPlottablesAsync()
        {
            return await _plottableRepository.GetAllAsync(plottable => plottable.MoveableEntity);
        }

        public async Task<PlottableEntity> GetPlottableAsync(int id)
        {
            return await _plottableRepository.GetSingleAsync(plottable => plottable.Id == id, plottable => plottable.MoveableEntity);
        }

        public async Task<int> AddPlottableAsync(PlottableEntity plottableEntity)
        {
            Classification classification = new Classification();
            Hostility hostility = new Hostility();
            bool validClassification = false;
            bool validHostility = false;
            bool validSubclass = false;

            foreach (string qwerty in classification.EClassification.Keys)
            {
                
                if (plottableEntity.Classification.Equals(qwerty))
                {
                    validClassification = true;
                    break;
                }

            }

            if (validClassification == true)
            {
                
                foreach (string qwerty in classification.EClassification[plottableEntity.Classification].Keys)
                {
                    
                    if (plottableEntity.Subclassification.Equals(qwerty))
                    {
                        validSubclass = true;
                        break;
                    }

                }
            }
            foreach (string qwerty in hostility.EHostility.Keys)
            {
                
                if (plottableEntity.Hostility.Equals(qwerty))
                {
                    validHostility = true;
                    break;
                }

            }

            if(validHostility==true && validSubclass == true && validHostility == true)
            {                
                TimeSpan t = DateTime.UtcNow - new DateTime(1970, 1, 1);
                ulong now = (ulong)t.TotalMilliseconds;
                plottableEntity.LastUpdate = now;
                _plottableRepository.Add(plottableEntity);
                return await _plottableRepository.Save();
            }

            return 0;
        }

        public async Task<int> UpdatePlottableEntityAsync(PlottableEntityDto plottableEntityDto)
        {
            Classification classification = new Classification();
            Hostility hostility = new Hostility();
            PlottableEntity foundPlottable = await GetPlottableAsync(plottableEntityDto.Id);
            foundPlottable.Identifier = plottableEntityDto.Identifier;
            foreach (string qwerty in classification.EClassification.Keys)
            {
                if (plottableEntityDto.Classification.Equals(qwerty))
                {
                    foundPlottable.Classification = plottableEntityDto.Classification;
                    break;
                }
     
            }
            foreach (string qwerty in classification.EClassification[foundPlottable.Classification].Keys)
            {

                if (plottableEntityDto.Subclassification.Equals(qwerty))
                {
                    foundPlottable.Subclassification = plottableEntityDto.Subclassification;
                    break;
                }

            }
            foreach (string qwerty in hostility.EHostility.Keys)
            {
                if (plottableEntityDto.Hostility.Equals(qwerty))
                {
                    foundPlottable.Hostility = plottableEntityDto.Hostility;
                    break;
                }
            }

            if (foundPlottable.Classification.Equals(plottableEntityDto.Classification) && foundPlottable.Subclassification.Equals(plottableEntityDto.Subclassification) && foundPlottable.Hostility.Equals(plottableEntityDto.Hostility))
            {
                return await _plottableRepository.Save();
            }
            else
            {
                return 0;
            }
            
            
        }

        public async Task<int> DeletePlottableByIdAsync(int id)
        {
            PlottableEntity plottableEntity = await GetPlottableAsync(id);
            _plottableRepository.Delete(plottableEntity);
            return await _plottableRepository.Save();
        }
        public async Task<int> SavePlottableEntityAsync(PlottableEntity plottableEntity)
        {
            _plottableRepository.Update(plottableEntity);
            return await _plottableRepository.Save();
        }

        public List<PlottableEntityWithDistanceAndMoveableEntityDto> GetDistances(NavigationPlatformDto platform, List<PlottableEntity> plottables)
        {
            Distance distance = new Distance();
            List<PlottableEntityWithDistanceAndMoveableEntityDto> mappedPlottables = _mapper.Map<List<PlottableEntityWithDistanceAndMoveableEntityDto>>(plottables);
            foreach (PlottableEntityWithDistanceAndMoveableEntityDto plottable in mappedPlottables)
            {
                plottable.Distance = distance.GetDistanceIgnoringElevation(platform.Latitude, platform.Longitude, plottable.Latitude, plottable.Longitude);
            }
            return mappedPlottables;
        }
    }
}
