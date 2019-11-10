using AutoMapper;
using Desafio.Models;
using Desafio.ViewModel;

namespace Desafio.Configuration
{
    public class AutoMapperConfiguration : Profile
    {
        public AutoMapperConfiguration()
        {
            CreateMap<UserViewModel, User>().ReverseMap();
            CreateMap<CompanyViewModel, Company>().ReverseMap();
            CreateMap<PageViewModel, Page>().ReverseMap();
        }
    }
}
