using src.Persistence.Models;
using src.Persistence.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace src.Services
{
    public class NavigationPlatformService
    {
        private readonly IRepository<NavigationPlatform> _platformRepository;
        public NavigationPlatformService(IRepository<NavigationPlatform> platformRepository)
        {
            _platformRepository = platformRepository;
        }

        public async Task<NavigationPlatform> GetPlatform()
        {
            List<NavigationPlatform> platform = await _platformRepository.GetAllAsync();
            return platform[0];
        }
    }
}
