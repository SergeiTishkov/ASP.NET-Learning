using AutoMapper;
using Microsoft.AspNet.Identity.EntityFramework;
using WasteProducts.Logic.Common.Models.Users;

namespace WasteProducts.Logic.Mappings.UserMappings
{
    public class UserLoginProfile : Profile
    {
        public UserLoginProfile()
        {
            CreateMap<UserLogin, IdentityUserLogin>().ReverseMap();
        }
    }
}
