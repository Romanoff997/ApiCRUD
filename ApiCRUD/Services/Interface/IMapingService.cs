
using ApiCRUD.Domain.Repositories.Entities;
using ApiCRUD.Models.Client;

namespace ApiCRUD.Services.Interface
{
    public interface IMapingService
    {
        public IQueryable<ClientInfoViewModel> GetLinkViews(IQueryable<ClientInfoEntities> Links);
        public ClientInfoEntities MapEntity(ClientInfoViewModel client);
        public ClientInfoViewModel MapView(ClientInfoEntities client);
    }
}
