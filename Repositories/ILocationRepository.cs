using MeTracker.Models;

namespace MeTracker.Repositories
{
    public interface ILocationRepository
    {
        Task<List<LocationModel>> GetAllLocationsAsync();
        Task SaveLocationAsync(LocationModel location);
        Task<List<LocationModel>> GetAllAsync();
        Task SaveAsync(LocationModel location);
        Task<List<LocationModel>> GetAll();
        Task Save(LocationModel location);
    }
}
