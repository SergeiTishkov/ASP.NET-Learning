using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoMapper
{
    class Program
    {
        static void Main(string[] args)
        {
            var user = new User() //user = (User)
            {
                Id = 10,
                FirstName = "",
                LastName = ""
            };
            Mapper.Initialize(cfg => cfg.AddProfile(new UserProfile()));//может сам найти профили и добавить их

            var dbUser = Mapper.Map<DBUser>(user);
        }
    }
}

internal class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<User, DBUser>()
            .ForMember(m => m.Created, opt => opt.UseValue(DateTime.UtcNow))
            .ForMember(m => m.Modified, opt => opt.MapFrom(u => u.Id != default(int) ? DateTime.UtcNow : (DateTime?)null))
            .ReverseMap()
            .ForMember(d => d.Id, opt => opt.Ignore());
    }
}