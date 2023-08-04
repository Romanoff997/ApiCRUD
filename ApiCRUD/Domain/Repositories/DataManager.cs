using ApiCRUD.Domain.Repositories.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiCRUD.Domain.Repositories
{
    public class DataManager
    {
        public IClientModelRepository ClientRepository { get; set; }

        public DataManager(IClientModelRepository linkRepository)
        {
            ClientRepository = linkRepository;
        }
    }
    
}
