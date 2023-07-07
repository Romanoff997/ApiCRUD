using AutoMapper.QueryableExtensions;
using AutoMapper;
using ApiCRUD.Models.Client;
using ApiCRUD.Services.Interface;
using ApiCRUD.Domain.Repositories.Entities;

namespace ApiCRUD.Services
{
    public class MappingServiceNative : IMapingService
    {
        private readonly IMapper _mapper;
        public MappingServiceNative(IMapper mapper)
        {
            _mapper = mapper;

        }

        public IQueryable<ClientInfoViewModel> GetLinkViews(IQueryable<ClientInfoEntities> Links)
        {
            return Links.ProjectTo<ClientInfoViewModel>(_mapper.ConfigurationProvider);
        }
        public ClientInfoEntities Map(ClientInfoViewModel client)
        {
            return _mapper.Map<ClientInfoEntities>(client);
        }
    }
}
