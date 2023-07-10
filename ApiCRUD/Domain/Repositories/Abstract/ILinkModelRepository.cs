using ApiCRUD.Domain.Repositories.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiCRUD.Domain.Repositories.Abstract
{
    public interface IClientModelRepository
    {
        public  Task<IEnumerable<ClientInfoEntities>> clientListAsync(string sortBy, bool sortDir, int limit, int page, string search);
        public  Task<Guid> clientCreateAsync(ClientInfoEntities client);
        public  Task<ClientInfoEntities> clientGetAsync(Guid id);
        public  Task clientUpdateAsync( ClientInfoEntities client);
        public  Task clientDeteleAsync(Guid id);

    }
}
