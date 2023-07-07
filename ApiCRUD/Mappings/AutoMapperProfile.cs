using ApiCRUD.Models.Client;
using AutoMapper;

namespace ApiCRUD.Mappings
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // CreateRequest -> User
            CreateMap<ClientInfoViewModel, ClientInfoModel>().ForMember(x=>x._children, opt =>
                opt.MapFrom(src=>src.children)).
                ForMember(x=>x._jobs, opt =>
                opt.MapFrom(src=>src.jobs))
                .ForAllMembers(x => x.Condition(
                (src, dest, prop) =>
                {
                    // ignore both null & empty string properties
                    if (prop == null) return false;
                    if (prop.GetType() == typeof(string) && string.IsNullOrEmpty((string)prop)) return false;

                    // ignore null role
                    //if (x.DestinationMember.Name == "children") return false;
                    //if (x.DestinationMember.Name == "jobs") return false;

                    return true;
                }
            ));
            CreateMap<ClientInfoModel, ClientInfoViewModel>();

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
