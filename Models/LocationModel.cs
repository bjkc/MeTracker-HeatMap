using SQLite;

namespace MeTracker.Models
{
    // [Table("Locations")]: This attribute instructs SQLite to store objects of this class 
    // inside a dedicated table named "Locations".
    [Table("Locations")]
    public class LocationModel
    {
        // [PrimaryKey, AutoIncrement]: This code marks 'Id' as the primary key of the table
        // and tells SQLite to automatically generate a unique, incrementing number (1, 2, 3...)
        // for every recorded location point.
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        // This code stores the GPS Latitude coordinate (e.g., 37.3360).
        public double Latitude { get; set; }

        // This code stores the GPS Longitude coordinate (e.g., -122.0580).
        public double Longitude { get; set; }

        // This code stores the exact date and UTC time when this GPS position was recorded.
        public DateTime Timestamp { get; set; }
    }
}
