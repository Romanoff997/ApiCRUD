using ApiCRUD.Domain.Repositories.Entities;
using ApiCRUD.Models.Client;
using ApiCRUD.Services;
using AutoMapper;

namespace ApiCRUD.Mappings
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // CreateRequest -> User
            CreateMap<ClientInfoViewModel, ClientInfoEntities>()
                .ForMember(x => x.children, opt =>
                opt.MapFrom(src => new JsonNewtonConverter().WriteJson(src.children)))
            .ForMember(x => x.jobs, opt =>
            opt.MapFrom(src => new JsonNewtonConverter().WriteJson(src.jobs)));

            CreateMap<ClientInfoEntities, ClientInfoViewModel>()
                .ForMember(x => x.children, opt =>
                opt.MapFrom(src => new JsonNewtonConverter().ReadJson<string[]>(src.children)))
                .ForMember(x => x.jobs, opt =>
                opt.MapFrom(src => new JsonNewtonConverter().ReadJson<string[]>(src.jobs)));

            // UpdateRequest -> User
            //CreateMap<ClientInfoViewModel, ClientInfoModel>()
            //    .ForAllMembers(x => x.Condition(
            //        (src, dest, prop) =>
            //        {
            //            // ignore both null & empty string properties
            //            if (prop == null) return false;
            //            return true;
            //        }
            //    ));
        }
    }
}
