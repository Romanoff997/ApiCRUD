
using ApiCRUD.Models.Client;

namespace ApiCRUD.Services.Interface
{
    public interface IMapingService
    {
        public IQueryable<ClientInfoViewModel> GetLinkViews(IQueryable<ClientInfoModel> Links);
        public ClientInfoModel Map(ClientInfoViewModel client);
    }
}
