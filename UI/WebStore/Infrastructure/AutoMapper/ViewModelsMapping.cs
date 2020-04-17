using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Domain.Entities.Identity;
using WebStore.ViewModels.Identity;

namespace WebStore.Infrastructure.AutoMapper
{
    public class ViewModelsMapping : Profile
    {
        public ViewModelsMapping()
        {
            CreateMap<RegisterUserViewModel, User>()
                .ForMember(user => user.UserName, opt => opt.MapFrom(model => model.UserName));
        }
    }
}
