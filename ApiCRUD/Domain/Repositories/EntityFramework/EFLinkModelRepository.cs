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
    
    public async Task<IEnumerable<ClientInfoEntities>> clientListAsync(string sortBy, bool sortDir, int limit, int page, string search)
        {
           
            List<ClientInfoEntities> result = await _context.ClientEntity.ToListAsync();

            if (sortDir)
                result =  result.OrderBy(x => GetPropertyValue(x, sortBy)).ToList();
            else
                result = result.OrderByDescending(x => GetPropertyValue(x, sortBy)).ToList();
            

            return result.Take(10);
           
        }
        public async Task<Guid> clientCreateAsync(ClientInfoEntities client)
        {

                var currclient = await _context.ClientEntity.AddAsync(client);
                await _context.SaveChangesAsync();
                 return client.id;
        }
        public async Task<ClientInfoEntities> clientGetAsync(Guid id)
        {
            return await _context.ClientEntity.FirstOrDefaultAsync(x => x.id.Equals(id));
        }
        public async Task clientUpdateAsync(Guid id, ClientInfoEntities client)
        {
            _context.Entry<ClientInfoEntities>(client).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
        public async Task clientDeteleAsync(Guid id)
        {
            var product = new ClientInfoEntities() { id = id };
            _context.ClientEntity.Attach(product);
            _context.ClientEntity.Remove(product);
            await _context.SaveChangesAsync();
        }

    }
}
