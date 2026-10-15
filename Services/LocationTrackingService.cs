using System;
using System.Threading.Tasks;
using MeTracker.Models;
using MeTracker.Repositories;

namespace MeTracker.Services
{
    public class LocationTrackingService : ILocationTrackingService
    {
        private readonly ILocationRepository _repository;

        public event EventHandler<LocationModel>? LocationUpdated;

        public LocationTrackingService(ILocationRepository repository)
        {
            _repository = repository;
        }

        public async Task StartTrackingAsync()
        {
            await SeedSampleRouteAsync();
        }

        public async Task SeedSampleRouteAsync()
        {
            try
            {
                var loc = new LocationModel
                {
                    Latitude = 37.7749,
                    Longitude = -122.4194,
                    Timestamp = DateTime.UtcNow
                };
                await _repository.SaveLocationAsync(loc);
                LocationUpdated?.Invoke(this, loc);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Tracking error: {ex.Message}");
            }
        }

        public Task StartTracking() => StartTrackingAsync();
        public Task StopTracking() => Task.CompletedTask;
    }
}
