using ApiCRUD.Models.Client;
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
        public  Task<IEnumerable<ClientInfoModel>> clientListAsync(string sortBy, bool sortDir, int limit, int page, string search);
        public  Task<Guid> clientCreateAsync(ClientInfoModel client);
        public  Task<ClientInfoModel> clientGetAsync(Guid id);
        public  Task clientUpdateAsync(Guid id, ClientInfoModel client);
        public  Task clientDeteleAsync(Guid id);

    }
}
