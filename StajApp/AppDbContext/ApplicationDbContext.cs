using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NetTopologySuite;
using NetTopologySuite.Geometries;
using NetTopologySuite.IO;
using StajApp.Models;

namespace StajApp.AppDbContext
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<Models.Coordinate> Coordinates { get; set; }
        public DbSet<CoordinateGeometry> CoordinateGeometries { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var geometryFactory = NtsGeometryServices.Instance.CreateGeometryFactory(srid: 4326);
            var geometryConverter = new ValueConverter<Geometry, string>(
                v => v.AsText(),
                v => new WKTReader(geometryFactory).Read(v));

            modelBuilder.Entity<CoordinateGeometry>()
                .Property(e => e.Geoloc)
                .HasConversion(geometryConverter);
        }


    }
}
