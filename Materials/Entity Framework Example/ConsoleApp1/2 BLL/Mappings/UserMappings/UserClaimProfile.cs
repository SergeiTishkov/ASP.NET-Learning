using AutoMapper;
using Microsoft.AspNet.Identity.EntityFramework;
using WasteProducts.Logic.Common.Models.Users;

namespace WasteProducts.Logic.Mappings.UserMappings
{
    public class UserClaimProfile : Profile
    {
        public UserClaimProfile()
        {
            CreateMap<UserClaim, IdentityUserClaim>().ReverseMap();
        }
    }
}
