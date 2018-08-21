using AutoMapper;
using Microsoft.AspNet.Identity.EntityFramework;
using WasteProducts.Logic.Common.Models.Users;

namespace WasteProducts.Logic.Mappings.UserMappings
{
    public class UserRoleProfile : Profile
    {
        public UserRoleProfile()
        {
            CreateMap<UserRole, IdentityUserRole>()
                .ForMember(r => r.UserId, opt => opt.Ignore())
                .ReverseMap();
        }
    }
}
