using SQLite;
using MeTracker.Models;

namespace MeTracker.Repositories
{
    public class LocationRepository : ILocationRepository
    {
        private SQLiteAsyncConnection? _connection;

        private async Task InitAsync()
        {
            if (_connection != null)
                return;

            var dbPath = Path.Combine(FileSystem.AppDataDirectory, "locations.db");
            _connection = new SQLiteAsyncConnection(dbPath);
            await _connection.CreateTableAsync<LocationModel>();
        }

        public async Task<List<LocationModel>> GetAllLocationsAsync()
        {
            await InitAsync();
            return await _connection!.Table<LocationModel>().ToListAsync();
        }

        public async Task SaveLocationAsync(LocationModel location)
        {
            await InitAsync();
            await _connection!.InsertAsync(location);
        }

        public Task<List<LocationModel>> GetAllAsync() => GetAllLocationsAsync();
        public Task<List<LocationModel>> GetAll() => GetAllLocationsAsync();
        public Task SaveAsync(LocationModel location) => SaveLocationAsync(location);
        public Task Save(LocationModel location) => SaveLocationAsync(location);
    }
}
