using src.Persistence.Models;
using src.Persistence.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace src.Services
{
    public class MoveableEntityService
    {
        private readonly IRepository<PlottableEntity> _plottableRepository;
        private readonly IRepository<MoveableEntity> _moveableRepository;
        public MoveableEntityService(IRepository<PlottableEntity> plottableRepository, IRepository<MoveableEntity> moveableRepository)
        {
            _plottableRepository = plottableRepository;
            _moveableRepository = moveableRepository;
        }

        public async Task<MoveableEntity> AddMoveableEntityAsync(MoveableEntity moveableEntity)
        {
            _moveableRepository.Add(moveableEntity);
            await _moveableRepository.Save();
            return moveableEntity;
        }

        public async Task<MoveableEntity> GetVectorDataAsync(int plottableEntityId)
        {
            // TODO Need to do Eager loading because with lazy loading the plottable.MoveableEntity is going to return null
            PlottableEntity plottable = await _plottableRepository.GetSingleAsync(plottableEntityId);
            return plottable.MoveableEntity;
        }
    }
}
