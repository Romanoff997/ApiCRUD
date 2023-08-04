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

            //.ForMember(x => x.children, opt =>
            //     opt.MapFrom(src => new JsonNewtonConverter().WriteJson(src.children)))
            .ForMember(x => x.jobs, opt =>
                  opt.MapFrom(src => new JsonNewtonConverter().WriteJson(src.jobs)))
            .ForMember(x => x.documentIds, opt =>
                 opt.MapFrom(src => new JsonNewtonConverter().WriteJson(src.documentIds)))
            .ForMember(x => x.communications, opt =>
                  opt.MapFrom(src => new JsonNewtonConverter().WriteJson(src.communications)));
            //.ForAllMembers(x => x.Condition(
            //    (src, dest, prop) =>
            //    {

            //        if (prop == null) return false;

            //        if (prop.GetType() == typeof(string[]) && (prop as Array).Length == 0) return false;

            //        return true;
            //    }
            //));
            //.ForMember(x => x.children, opt =>
            //     opt.MapFrom(src => (src.children.Length != 0 && src.children != null) ? new JsonNewtonConverter().WriteJson(src.children) : null))
            //.ForMember(x => x.jobs, opt =>
            //      opt.MapFrom(src => (src.jobs.Length != 0 && src.jobs != null) ? new JsonNewtonConverter().WriteJson(src.jobs) : null))
            //.ForMember(x => x.documentIds, opt =>
            //     opt.MapFrom(src => (src.documentIds.Length != 0 && src.documentIds != null) ? new JsonNewtonConverter().WriteJson(src.documentIds) : null))
            //.ForMember(x => x.communications, opt =>
            //      opt.MapFrom(src => (src.communications.Length != 0 && src.communications != null) ? new JsonNewtonConverter().WriteJson(src.communications) : null));


            CreateMap<ClientInfoEntities, ClientInfoViewModel>()
                    //.ForMember(x => x.children, opt =>
                    //     opt.MapFrom(src => new JsonNewtonConverter().ReadJson<string[]>(src.children)))
                    .ForMember(x => x.jobs, opt =>
                         opt.MapFrom(src => new JsonNewtonConverter().ReadJson<string[]>(src.jobs)))
                    .ForMember(x => x.documentIds, opt =>
                            opt.MapFrom(src => new JsonNewtonConverter().ReadJson<string[]>(src.documentIds)))
                    .ForMember(x => x.communications, opt =>
                         opt.MapFrom(src => new JsonNewtonConverter().ReadJson<string[]>(src.communications)));

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
