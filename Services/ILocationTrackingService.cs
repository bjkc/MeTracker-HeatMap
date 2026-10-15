using System;
using System.Threading.Tasks;
using MeTracker.Models;

namespace MeTracker.Services
{
    public interface ILocationTrackingService
    {
        event EventHandler<LocationModel>? LocationUpdated;
        Task StartTrackingAsync();
        Task SeedSampleRouteAsync();
        Task StartTracking();
        Task StopTracking();
    }
}
