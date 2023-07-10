﻿using ApiCRUD.Domain.Repositories;
using ApiCRUD.Domain.Repositories.Entities;
using ApiCRUD.Models;
using ApiCRUD.Models.Client;
using ApiCRUD.Services;
using ApiCRUD.Services.Interface;
using AutoMapper;
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
        private readonly IMapingService _mapper;
        public clientsController(DataManager dataManager, JsonNewtonConverter converter, MappingServiceNative mapper)
        {
            _dataManager = dataManager;
            _converter = converter;
            _mapper=mapper; 
        }


        [HttpGet]
        public async Task<ActionResult> clientList(string sortBy = "createdAt", string sortDir = "asc", int limit = 10, int page = 1, string? search=null)
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
                    throw new ApplicationException(_converter.WriteJson(vlidationErrorMessage));
                }

                IEnumerable <ClientInfoEntities> arrayClient = await _dataManager.ClientRepository.clientListAsync(sortBy, (sortDir == "asc" ? true : false), limit, page, search);
                var data = _mapper.GetLinkViews(arrayClient.AsQueryable()).ToArray();

                return Ok(new PaginationResponseBody()
                {
                    total = 0,
                    page = page,
                    limit = limit, 
                    data = data
                });   
        }


        [HttpPost]
        public async Task<ActionResult> clientCreate([FromBody] ClientInfoViewModel ViewClient)
        {

                if (!ModelState.IsValid)
                {
                    throw new ApplicationException();
                }
                var client = _mapper.MapEntity(ViewClient);
                var id = await _dataManager.ClientRepository.clientCreateAsync(client);

                return Ok(new CreateClientResponce {id = id});
            }


        [HttpGet("{id}")]
        public async Task<ActionResult> clientGet(Guid id)
        {
            if (!ModelState.IsValid)
            {
                throw new ApplicationException();
            }

            var client = await _dataManager.ClientRepository.clientGetAsync(id);

            ClientInfoViewModel viewClient = _mapper.MapView(client);
            return Ok(viewClient);

            
        }


        [HttpPut("{id}")]
        public async Task<ActionResult> clientUpdate(Guid id, [FromBody] ClientInfoViewModel _client)
        {
            if (!ModelState.IsValid)
            {
                throw new ApplicationException();
            }
            ClientInfoEntities client = _mapper.MapEntity(_client);
            client.id = id;
            client.updatedAt = DateTime.UtcNow;
            await _dataManager.ClientRepository.clientUpdateAsync(client);
            return Ok("Данные клиента успешо обновленны");
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult> clientDelete(Guid id)
        {
            if (!ModelState.IsValid)
            {
                throw new ApplicationException();
            }

            await _dataManager.ClientRepository.clientDeteleAsync(id);
            return Ok("Клиент удален");


            
        }
    }
}
