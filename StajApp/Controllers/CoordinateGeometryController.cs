using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NetTopologySuite.Geometries;
using NetTopologySuite.IO;
using Newtonsoft.Json;
using StajApp.AppDbContext;
using StajApp.DTOs;
using StajApp.Models;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace StajApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoordinateGeometryController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CoordinateGeometryController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> PostCoordinateGeometry([FromBody] WktCoordinateModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var reader = new WKTReader();
            var geometry = reader.Read(model.Wkt);

            var coordinateGeometry = new CoordinateGeometry
            {
                Description = model.Description,
                Geoloc = geometry
            };

            _context.CoordinateGeometries.Add(coordinateGeometry);
            await _context.SaveChangesAsync();

            return Ok("Geometry saved successfully.");
        }

        [HttpGet]
        public IActionResult GetCoordinates()
        {
            var coordinates = _context.CoordinateGeometries.ToList();
            var geometries = new List<WktCoordinateModel>();

            foreach (var coordinate in coordinates)
            {
                var geometry = coordinate.Geoloc;
                geometries.Add(new WktCoordinateModel
                {
                    Wkt = geometry.AsText(),
                    Description = coordinate.Description,
                    Id = coordinate.Id // Eklenmiş
                });
            }

            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                IgnoreNullValues = true,
                WriteIndented = true,
                Converters =
                {
                    new JsonStringEnumConverter(JsonNamingPolicy.CamelCase),
                    new GeometryConverter()
                },
                ReferenceHandler = ReferenceHandler.Preserve,
                MaxDepth = 256
            };

            return Ok(System.Text.Json.JsonSerializer.Serialize(geometries, options));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCoordinateGeometry(int id, [FromBody] WktCoordinateModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var coordinateGeometry = await _context.CoordinateGeometries.FindAsync(id);
            if (coordinateGeometry == null)
            {
                return NotFound("Coordinate not found.");
            }

            var reader = new WKTReader();
            var geometry = reader.Read(model.Wkt);

            coordinateGeometry.Description = model.Description;
            coordinateGeometry.Geoloc = geometry;

            _context.CoordinateGeometries.Update(coordinateGeometry);
            await _context.SaveChangesAsync();

            return Ok("Geometry updated successfully.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCoordinateGeometry(int id)
        {
            var coordinateGeometry = await _context.CoordinateGeometries.FindAsync(id);
            if (coordinateGeometry == null)
            {
                return NotFound("Coordinate not found.");
            }

            _context.CoordinateGeometries.Remove(coordinateGeometry);
            await _context.SaveChangesAsync();

            return Ok("Geometry deleted successfully.");
        }

        public class Responsee
        {
            public object Data { get; set; }
        }

        public class GeometryConverter : System.Text.Json.Serialization.JsonConverter<Geometry>
        {
            public override Geometry Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            {
                throw new NotImplementedException();
            }

            public override void Write(Utf8JsonWriter writer, Geometry value, JsonSerializerOptions options)
            {
                writer.WriteStringValue(value.AsText());
            }
        }
    }
}
