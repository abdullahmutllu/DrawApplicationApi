using Microsoft.EntityFrameworkCore;
using StajApp.AppDbContext;
using StajApp.Models;

namespace StajApp.Services.Postgres
{
    public class PostgresServiceRawQuery : IPostgresServiceRawQuery
    {
        private readonly ApplicationDbContext _context;
        public PostgresServiceRawQuery(ApplicationDbContext context)
        {
            _context = context;
        }
        public Response AddCoordinate(Coordinate coordinate)
        {

            var sqlAddQuery = "INSERT INTO \"Coordinates\" (\"X\", \"Y\", \"Name\") VALUES (@p0, @p1, @p2)";

            _context.Database.ExecuteSqlRaw(sqlAddQuery, coordinate.X, coordinate.Y, coordinate.Name);
            return new Response { IsSuccess = true, Message = "Coordinate added successfully." };
        }

        public Response DeleteCoordinate(int id)
        {
            var sqlDeleteQuery = $"DELETE FROM public.\"Coordinates\"\r\nWHERE \"Id\"={id};";
            var result = _context.Database.ExecuteSqlRaw(sqlDeleteQuery);

            return new Response { IsSuccess = true, Message = "Delete is succesfull" };
        }

        public Response GetAll()
        {
            var query = "SELECT \"Id\", \"X\", \"Y\", \"Name\"\r\nFROM public.\"Coordinates\";";
            var dataList = _context.Coordinates.FromSqlRaw(query).ToList();
            return new Response { IsSuccess = true, Data = dataList, Message = "Coordinat GetAll" };
        }

        public Response GetById(int id)
        {
            var getByIdQuery = $"SELECT * FROM public.\"Coordinates\" WHERE \"Id\" = {id}";
            var data = _context.Coordinates.FromSqlRaw(getByIdQuery).FirstOrDefault();


            return new Response { Data = data, IsSuccess = false, Message = "Coordinate not found" };

        }

        public Response UpdateCoordinate(int id, Coordinate coordinate)
        {
            var query = "UPDATE public.\"Coordinates\" " +
                           "SET \"X\" = @p0, \"Y\" = @p1, \"Name\" = @p2 " +
                           "WHERE \"Id\" = @p3";
            _context.Database.ExecuteSqlRaw(query, coordinate.X, coordinate.Y, coordinate.Name, id);
            return new Response { IsSuccess = true, Message = "Coordinate updated successfully" };
        }

    }
}
