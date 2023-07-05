using ApiCRUD.Domain.Repositories;
using ApiCRUD.Models;
using ApiCRUD.Models.Client;
using ApiCRUD.Services;
using CRUD_Сlients_API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Schema;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Model;

namespace ApiCRUD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class clientsController : ControllerBase
    {

        private readonly DataManager _dataManager;
        private readonly IJsonConverter _converter;
        public clientsController(DataManager dataManager)
        {
            _dataManager = dataManager;
            _converter = new JsonNewtonConverter();
        }
        // GET api/clients
        [HttpGet]
        public async Task<ActionResult> clientList(string sortBy = "createdAt", string sortDir = "asc", int limit = 10, int page = 1, string? search=null)
        {
            //try
            //{
                bool validation = true;
                Dictionary<string, string> vlidationErrorMessage = new Dictionary<string, string>();
                if (TypeVisorService.GetTypeField(sortBy, typeof(ClientInfoModel)) == null)
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
                    throw new ApplicationException(_converter.WriteJson(vlidationErrorMessage));
                }

                var arrayClient = await _dataManager.ClientRepository.clientListAsync(sortBy, (sortDir == "asc" ? true : false), limit, page, search);
                    
                return Ok(new ClientResponseModel()
                {
                    total = 0,
                    page = page,
                    limit = limit, 
                    data = arrayClient.ToArray(),
                });   
        }


        [HttpPost]
        public async Task<ActionResult> clientCreate([FromBody] ClientInfoViewModel ViewClient)
        {

                if (!ModelState.IsValid)
                {
                    throw new ApplicationException();
                }
                ClientInfoModel client = new ClientInfoModel() {
                    id = ViewClient.id, 
                    dob = ViewClient.dob, 
                    name = ViewClient.name,
                    patronymic = ViewClient.patronymic,
                    surname = ViewClient.surname,
                    сhildren = JsonSerializer.Serialize(ViewClient.сhildren),
                    //jobs = JsonSerializer.Serialize(ViewClient.jobs)
                };
                var id = await _dataManager.ClientRepository.clientCreateAsync(client);
                //if (id == null)
                   // throw new Exception("клиент не создан");
                return Ok(new CreateClientResponce {id= new Guid(id.ToString()) });
            }

        // POST api/clients
        [HttpGet("{id}")]
        public async Task<ActionResult> clientGet(Guid id)
        {
            if (!ModelState.IsValid)
            {
                ErrorClientResponseModel errorClientResponseModel = new ErrorClientResponseModel();
                errorClientResponseModel.status = 422;
                errorClientResponseModel.code = "VALIDATION_EXCEPTION";
                return BadRequest(errorClientResponseModel);
            }

            var client = await _dataManager.ClientRepository.clientGetAsync(id);

            if (client != null)
                return Ok(client);

            return BadRequest(new ErrorClientResponseModel()
            {
                status = 500,
                code = "INTERNAL_SERVER_ERROR"
            });
            
        }

        // PUT api/clients/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> clientUpdate(Guid id, [FromBody] ClientInfoModel client)
        {
            if (!ModelState.IsValid)
            {
                ErrorClientResponseModel errorClientResponseModel = new ErrorClientResponseModel();
                errorClientResponseModel.status = 422;
                errorClientResponseModel.code = "VALIDATION_EXCEPTION";
                return BadRequest(errorClientResponseModel);
            }
            try
            {

                await _dataManager.ClientRepository.clientUpdateAsync(new Guid(id.ToString()), client);
                return Ok("Данные клиента успешо обновленны");
            }
            catch (Exception ex) 
            {
                return BadRequest(new ErrorClientResponseModel()
                {
                    status = 500,
                    code = "INTERNAL_SERVER_ERROR"
                });
            }
        }

        // DELETE api/clients/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> clientDelete(Guid id)
        {
            if (!ModelState.IsValid)
            {
                ErrorClientResponseModel errorClientResponseModel = new ErrorClientResponseModel();
                errorClientResponseModel.status = 422;
                errorClientResponseModel.code = "VALIDATION_EXCEPTION";
                return BadRequest(errorClientResponseModel);
            }
            try
            {

                await _dataManager.ClientRepository.clientDeteleAsync(id);
                return Ok("Клиент удален");
            }
            catch (Exception ex)
            {
                return BadRequest(new ErrorClientResponseModel()
                {
                    status = 500,
                    code = "INTERNAL_SERVER_ERROR"
                });
            }
            
        }
    }
}
