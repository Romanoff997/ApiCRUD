using ApiCRUD.Domain.Repositories.Abstract;
using ApiCRUD.Domain.Repositories.Entities;
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
    
    public async Task<IEnumerable<ClientInfoEntities>> ClientListAsync(string sortBy, bool sortDir, int limit, int page, string search)
        {
           
            List<ClientInfoEntities> result = await _context.ClientEntity.ToListAsync();

            if (sortDir)
                result =  result.OrderBy(x => GetPropertyValue(x, sortBy)).ToList();
            else
                result = result.OrderByDescending(x => GetPropertyValue(x, sortBy)).ToList();
            

            return result.Take(limit);
           
        }
        public async Task<Guid> ClientCreateAsync(ClientInfoEntities client)
        {

                var currclient = await _context.ClientEntity.AddAsync(client);
                await _context.SaveChangesAsync();
                 return client.id;
        }
        public async Task<ClientInfoEntities> ClientGetAsync(Guid id)
        {
            return await _context.ClientEntity.FirstOrDefaultAsync(x => x.id.Equals(id));
        }
        public async Task ClientUpdateAsync(ClientInfoEntities client)
        {
            _context.Entry(client).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
        public async Task ClientDeteleAsync(Guid id)
        {
            var client = new ClientInfoEntities() { id = id };
            _context.ClientEntity.Attach(client);
            _context.ClientEntity.Remove(client);
            await _context.SaveChangesAsync();
        }
        public async Task ClientSoftDeteleAsync(Guid id)
        {
            var client = _context.ClientEntity.Where(x => x.id.Equals(id));
            if (client != null)
            {
                //client.Delete = true;
                _context.Entry(client).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
                
        }

    }
}
