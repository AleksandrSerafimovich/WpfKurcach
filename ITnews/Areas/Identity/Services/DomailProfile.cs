using AutoMapper;
using ITnews.Models;
using ITnews.Models.ManageViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITnews.Areas.Identity.Services
{
    public class DomainProfile : Profile
    {
        public DomainProfile()
        {
            CreateMap<IndexViewModel, ApplicationUser>();
            CreateMap<ApplicationUser, IndexViewModel>();
        }
    }
}
