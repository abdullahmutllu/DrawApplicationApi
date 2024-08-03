using StajApp.Models;

namespace StajApp.Services.Postgres
{
    public interface IPostgresServiceRawQuery
    {
        public Response GetAll();
        public Response GetById(int id);
        public Response AddCoordinate(Coordinate coordinate);
        public Response UpdateCoordinate(int id, Coordinate coordinate);
        public Response DeleteCoordinate(int id);
    }
}
