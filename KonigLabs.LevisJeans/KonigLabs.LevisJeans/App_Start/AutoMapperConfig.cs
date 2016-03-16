using AutoMapper;
using KonigLabs.LevisJeans.Models;
using KonigLabs.LevisJeans.Models.Entities;

namespace KonigLabs.LevisJeans
{
    public class AutoMapperConfig
    {
        public static void RegisterMappings()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Customer, CustomerVm>();
                cfg.CreateMap<CustomerVm, Customer>();
                cfg.CreateMap<CustomerVm, CustomerRedirectTermVm>();
                cfg.CreateMap<CustomerRedirectTermVm, CustomerVm>();
                cfg.CreateMap<Test, TestVm>();
                cfg.CreateMap<TestVm, Test>()
                    .ForMember(p => p.Id, opt => opt.MapFrom(src => src.CustomerId));
            });
        }
    }
}