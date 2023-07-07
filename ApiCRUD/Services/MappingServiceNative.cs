using AutoMapper.QueryableExtensions;
using AutoMapper;
using ApiCRUD.Models.Client;
using ApiCRUD.Services.Interface;

namespace ApiCRUD.Services
{
    public class MappingServiceNative : IMapingService
    {
        private readonly IMapper _mapper;
        public MappingServiceNative(IMapper mapper)
        {
            _mapper = mapper;

        }

        public IQueryable<ClientInfoViewModel> GetLinkViews(IQueryable<ClientInfoModel> Links)
        {
            return Links.ProjectTo<ClientInfoViewModel>(_mapper.ConfigurationProvider);
        }
        public ClientInfoModel Map(ClientInfoViewModel client)
        {
            return _mapper.Map<ClientInfoModel>(client);
        }
    }
}
