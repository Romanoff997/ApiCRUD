using ApiCRUD.Domain.Repositories.Abstract;
using ApiCRUD.Models.Client;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ApiCRUD.Domain.Repositories.EntityFramework
{
    public class EFClientModelRepository : IClientModelRepository
    {
        private readonly MyDbContext _context;
        public EFClientModelRepository(MyDbContext context)
        {
            _context = context;
        }
        public static object GetPropertyValue(object obj, string propertyName)
        {
            var propertyInfo = obj.GetType().GetProperty(propertyName);
            return propertyInfo.GetValue(obj);
        }
    
    public async Task<IEnumerable<ClientInfoModel>> clientListAsync(string sortBy, bool sortDir, int limit, int page, string search)
        {
           
            List<ClientInfoModel> result = await _context.ClientEntity.ToListAsync();

            if (sortDir)
                result =  result.OrderBy(x => GetPropertyValue(x, sortBy)).ToList();
            else
                result = result.OrderByDescending(x => GetPropertyValue(x, sortBy)).ToList();
            

            return result.Take(10);
           
        }
        public async Task<Guid> clientCreateAsync(ClientInfoModel client)
        {

                var currclient = await _context.ClientEntity.AddAsync(client);
                _context.SaveChanges();
                return client.id;
        }
        public async Task<ClientInfoModel> clientGetAsync(Guid id)
        {
            return await _context.ClientEntity.FirstOrDefaultAsync(x => x.id.Equals(id));
        }
        public async Task clientUpdateAsync(Guid id, ClientInfoModel client)
        {
            ClientInfoModel _client = await _context.ClientEntity.FirstOrDefaultAsync(x => x.id.Equals(id));
            if (_client != null)
            {
                //_client.communications = client.communications;
                //_client.jobs = client.jobs;
                //_client.name = client.name;
                //_client.surname = client.surname;
                //_client.passport = client.passport;
                //_client.curWorkExp = client.curWorkExp;
                //_client.сhildren = client.сhildren;
                //_client.dob = client.dob;
                
                _context.SaveChanges();
            }

            
        }
        public async Task clientDeteleAsync(Guid id)
        {
            var product = new ClientInfoModel() { id = id };
            _context.ClientEntity.Attach(product);
            _context.ClientEntity.Remove(product);
            await _context.SaveChangesAsync();
        }

    }
}
