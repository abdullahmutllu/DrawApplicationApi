using NetTopologySuite.Geometries;

namespace StajApp.Models
{
    public class CoordinateGeometry
    {
        public int Id { get; set; }
        public string  Description { get; set; }
        public Geometry Geoloc { get; set; }
    }
}
