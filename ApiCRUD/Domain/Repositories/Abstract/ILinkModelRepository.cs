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
        public  Task<IEnumerable<ClientInfoEntities>> ClientListAsync(string sortBy, bool sortDir, int limit, int page, string search);
        public  Task<Guid> ClientCreateAsync(ClientInfoEntities client);
        public  Task<ClientInfoEntities> ClientGetAsync(Guid id);
        public  Task ClientUpdateAsync( ClientInfoEntities client);
        public  Task ClientDeteleAsync(Guid id);
        public  Task ClientSoftDeteleAsync(Guid id);

    }
}
