using AutoMapper;
using gRPCDotNetCoreClient.Data;
using gRPCDotNetCoreClient.Models;
using Microsoft.Extensions.DependencyInjection;

namespace gRPCDotNetCoreClient.Automapper
{
    public static class AutomapperExtentions
    {
        public static void AddAutomapperConfig(this IServiceCollection services) {
            
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<BookModel, BookDto>()
                .ReverseMap();
                cfg.CreateMap<Org.Example.NMVnext.Sevices.Book, BookDto>()
                .ReverseMap();
            });


            IMapper mapper = config.CreateMapper();
            
            services.AddSingleton<IMapper>(mapper);
        }
    }
}