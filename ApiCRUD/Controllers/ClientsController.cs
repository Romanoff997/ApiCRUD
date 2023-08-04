using ApiCRUD.Domain.Repositories;
using ApiCRUD.Domain.Repositories.Entities;
using ApiCRUD.Models;
using ApiCRUD.Models.Client;
using ApiCRUD.Services;
using ApiCRUD.Services.Interface;
using CRUD_Сlients_API.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ApiCRUD.Controllers
{
    [Route("api/clients")]
    [ApiController]
    public class СlientsController : ControllerBase
    {

        private readonly DataManager _dataManager;
       // private readonly IJsonConverter _converter;
        private readonly IMapingService _mapper;
        public СlientsController(DataManager dataManager, MappingServiceNative mapper, IJsonConverter converter)
        {
            _dataManager = dataManager;
            //_converter = converter;
            _mapper=mapper; 
        }


        [HttpGet]
        public async Task<ActionResult> ClientList(string sortBy , string sortDir, int limit, int page, string search)
        {

                bool validation = true;
                Dictionary<string, string> vlidationErrorMessage = new Dictionary<string, string>();
                if (TypeVisorService.GetTypeField(sortBy, typeof(ClientInfoEntities)) == null)
                {
                    validation = false;
                    vlidationErrorMessage.Add( "sortBy",  $"not find field" );
                }

                if (limit <= 0)
                {
                    validation = false;
                    vlidationErrorMessage.Add("limit", $"get it limit>0");
                }

                if (!(sortDir.Equals("asc") || sortDir.Equals("desc")))
                {
                    validation = false;
                    vlidationErrorMessage.Add("sortDir", $"get it 'asc'|'desc");
                }

                if (!validation)
                {
                    ErrorClientResponseModel errorResponse = new ErrorClientResponseModel();
                    errorResponse.status = (int)HttpStatusCode.BadRequest;
                    errorResponse.code = "APPLICATION_EXCEPTION";//"VALIDATION_EXCEPTION";
                    foreach (var curr in vlidationErrorMessage)
                    {
                        errorResponse.exception.Add(new ValidationExceptions() { field = curr.Key, message = curr.Value });
                    }
                    return BadRequest(errorResponse);
                }

                IEnumerable <ClientInfoEntities> arrayClient = await _dataManager.ClientRepository.ClientListAsync(sortBy, (sortDir == "asc" ? true : false), limit, page, search);
                var data = _mapper.GetLinkViews(arrayClient.AsQueryable()).ToArray();

                return Ok(
                            new PaginationResponseBody()
                            {
                                total = 0,
                                page = page,
                                limit = limit, 
                                data = data
                            }
                        );   
        }


        [HttpPost]
        public async Task<ActionResult> ClientCreate([FromBody] ClientInfoViewModel ViewClient)
        {
                if (!ModelState.IsValid)
                {
                    throw new ApplicationException();
                }
                var client = _mapper.MapEntity(ViewClient);
                var id = await _dataManager.ClientRepository.ClientCreateAsync(client);

                return Ok(new CreateClientResponce {id = id});
            }


        [HttpGet("{id}")]
        public async Task<ActionResult> ClientGet(Guid id)
        {
            var client = await _dataManager.ClientRepository.ClientGetAsync(id);

            ClientInfoViewModel viewClient = _mapper.MapView(client);
            return Ok(viewClient); 
        }


        [HttpPut("{id}")]
        public async Task<ActionResult> ClientUpdate(Guid id, [FromBody] ClientInfoViewModel _client)
        {
            ClientInfoEntities client = _mapper.MapEntity(_client);
            client.id = id;
            client.updatedAt = DateTime.UtcNow;
            await _dataManager.ClientRepository.ClientUpdateAsync(client);
            return Ok("Данные клиента успешо обновленны");
        }


    [HttpDelete("{id}")]
        public async Task<ActionResult> ClientDelete(Guid id)
        {
            if (!ModelState.IsValid)
            {
                throw new ApplicationException();
            }

            await _dataManager.ClientRepository.ClientDeteleAsync(id);
            return Ok("Клиент удален");
        }

        //[HttpDelete("{id}")]
        //public async Task<ActionResult> ClientSoftDelete(Guid id)
        //{
        //    await _dataManager.ClientRepository.ClientLightDeteleAsync(id);
        //    return Ok("Клиент удален");
        //}
    }
}
