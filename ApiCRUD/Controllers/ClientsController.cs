using ApiCRUD.Domain.Repositories;
using ApiCRUD.Models;
using ApiCRUD.Models.Client;
using CRUD_Сlients_API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        public clientsController(DataManager dataManager)
        {
            _dataManager = dataManager;
        }
        // GET api/clients
        [HttpGet]
        public async Task<ActionResult> clientList(string sortBy = "createdAt", string sortDir = "asc", int limit = 10, int page = 1, string? search=null)
        {
            try
            {
                ErrorClientResponseModel errorClientResponseModel = new ErrorClientResponseModel();
                bool validation = true;
                Type myType = typeof(ClientInfoModel);
                var myField = myType.GetProperty(sortBy);
               // FieldInfo field = myField.Where(x => x.Name.Equals(sortBy)).FirstOrDefault();
                if (myField == null)
                {
                    validation = false;
                    errorClientResponseModel.exception.Add(new ExceptionClientResponse() { field = "sortBy", message = $"not find field{sortBy}" }); ;
                }

                if (limit <= 0)
                {
                    validation = false;
                    errorClientResponseModel.exception.Add(new ExceptionClientResponse() { field = "limit", message = "get it limit>0" });
                }
                if (!(sortDir.Equals("asc") || sortDir.Equals("desc")))
                {
                    validation = false;
                    errorClientResponseModel.exception.Add(new ExceptionClientResponse() { field = "sortDir", message = "get it 'asc'|'desc'" });
                }

                if (!validation)
                {
                    errorClientResponseModel.status = 422;
                    errorClientResponseModel.code = "VALIDATION_EXCEPTION";
                    return BadRequest(errorClientResponseModel);
                }
                else
                {
                    var arrayClient = await _dataManager.ClientRepository.clientListAsync(sortBy, (sortDir == "asc" ? true : false), limit, page, search);
                    
                    return Ok(new ClientResponseModel()
                    {
                        total = 0,
                        page = page,
                        limit = limit, 
                        data = arrayClient.ToArray(),
                        //page
                    });

                }
            }
            catch (Exception ex)
            {
                ErrorClientResponseModel errorClientResponseModel = new ErrorClientResponseModel();
                errorClientResponseModel.status = 500;
                errorClientResponseModel.code = "INTERNAL_SERVER_ERROR";
                return BadRequest(errorClientResponseModel);
            }
        }

        // GET api/clients/{id}

        [HttpPost]
        public async Task<ActionResult> clientCreate([FromBody] ClientInfoViewModel ViewClient)
        {
            try
            { 
                if (!ModelState.IsValid)
                {
                    ErrorClientResponseModel errorClientResponseModel = new ErrorClientResponseModel();
                    errorClientResponseModel.status = 422;
                    errorClientResponseModel.code = "VALIDATION_EXCEPTION";
                    return BadRequest(errorClientResponseModel);
                }
                ClientInfoModel client = new ClientInfoModel() {
                    id = ViewClient.id, 
                    dob = ViewClient.dob, 
                    name = ViewClient.name,
                    patronymic = ViewClient.patronymic,
                    surname = ViewClient.surname,
                    сhildren = JsonSerializer.Serialize(ViewClient.сhildren)
                };
                await _dataManager.ClientRepository.clientCreateAsync(client);
                return Ok(new CreateClientResponce {id= client.id });
            }
            catch (Exception ex)
            {
                ErrorClientResponseModel errorClientResponseModel = new ErrorClientResponseModel();
                errorClientResponseModel.code = "INTERNAL_SERVER_ERROR";
                errorClientResponseModel.status = 500;
                return BadRequest(errorClientResponseModel);
            }
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
