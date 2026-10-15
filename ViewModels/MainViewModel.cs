using System.Collections.ObjectModel;
using MeTracker.Models;
using MeTracker.Repositories;

namespace MeTracker.ViewModels
{
    public class MainViewModel : BindableObject
    {
        private bool _isBusy;
        public bool IsBusy
        {
            get => _isBusy;
            set
            {
                _isBusy = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<LocationModel> Locations { get; } = new();

        private readonly ILocationRepository _repository;

        public MainViewModel(ILocationRepository repository)
        {
            _repository = repository;
        }

        public async Task LoadLocationsAsync()
        {
            try
            {
                IsBusy = true;
                var points = await _repository.GetAllAsync();
                Locations.Clear();
                foreach (var p in points)
                {
                    Locations.Add(p);
                }

                if (Locations.Count == 0)
                {
                    var p1 = new LocationModel { Latitude = 37.7749, Longitude = -122.4194, Timestamp = DateTime.Now };
                    var p2 = new LocationModel { Latitude = 37.7790, Longitude = -122.4150, Timestamp = DateTime.Now.AddMinutes(5) };
                    var p3 = new LocationModel { Latitude = 37.7830, Longitude = -122.4100, Timestamp = DateTime.Now.AddMinutes(10) };
                    await _repository.SaveAsync(p1);
                    await _repository.SaveAsync(p2);
                    await _repository.SaveAsync(p3);
                    Locations.Add(p1);
                    Locations.Add(p2);
                    Locations.Add(p3);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading points: {ex.Message}");
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
