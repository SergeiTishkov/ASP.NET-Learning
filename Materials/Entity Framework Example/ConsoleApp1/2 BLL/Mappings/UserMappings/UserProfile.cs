using AutoMapper;
using Microsoft.AspNet.Identity.EntityFramework;
using WasteProducts.DataAccess.Common.Models.Users;
using WasteProducts.Logic.Common.Models.Users;

namespace WasteProducts.Logic.Mappings.UserMappings
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDB>()
                .ForMember(m => m.Created, opt => opt.Ignore())
                .ForMember(m => m.Modified, opt => opt.Ignore())
                .AfterMap((user, userDB) => 
                {
                    foreach (var item in userDB.Roles)
                    {
                        item.UserId = userDB.Id;
                    }
                })
                .ReverseMap();
        }
    }
}
