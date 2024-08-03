using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Validations;
using StajApp.AppDbContext;
using StajApp.Models;
using StajApp.Services;
using StajApp.Services.Map;
using StajApp.Services.Postgres;

namespace StajApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoordinateController : ControllerBase
    {

        private readonly IMapControlService _mapControlService;
         //private readonly IPostgresServiceRawQuery _postgresService;

        public CoordinateController(/* IPostgresServiceRawQuery postgresService,*/IMapControlService mapControlService)
        {

            //_postgresService = postgresService;
            _mapControlService = mapControlService;
  
        }
        [HttpGet]
        public Response GetCoordinate()
        {
         IQueryable<Coordinate> data = _mapControlService.GetAll();

            return new Response { Data = data ,IsSuccess=true,Message="Coordina add succesfull"};
            //  return _postgresService.GetAll();
            //return _service.GetCoordinate();

        }
        [HttpGet("{page}/{pageSize}")]
        public Response GetCoordinate(int page,int pageSize)
        {
          var data =   _mapControlService.GetAllCoordinates(page,pageSize);
            return new Response { Data = data ,Message="get by id is succesfull",IsSuccess=true};
            //return _postgresService.GetById(id);
           //return  _service.GetCoordinate(id);
        }
        [HttpPost]
        public Response PostCoordinate(Coordinate coordinate)
        {
           _mapControlService.Add(coordinate);
            return new Response { Message = "added" };
            //return _postgresService.AddCoordinate(coordinate);
            //return _service.PostCoordinate(coordinate);
        }
        [HttpPut("{id}")]
        public Response PutCoordinate(int id ,Coordinate coordinate)
        {
            _mapControlService.Update(id,coordinate);
            return new Response {Data = coordinate, Message="is success"};
            //return _postgresService.UpdateCoordinate(id,coordinate);
            //return _service.PutCoordinate(id,coordinate);

        }
        [HttpDelete("{id}")]
        public Response DeleteCoordinate(int id)
        {
            _mapControlService.Delete(id);
            return new Response { Message="delelte is succsesfull"};
            //return _postgresService.DeleteCoordinate(id);
            //return _service.DeleteCoordinate(id);
          
        }
    }
}
