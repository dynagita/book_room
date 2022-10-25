using AutoMapper;
using BookRoom.Service.Application;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace BookRoom.Service.Unit.Tests.Utils
{
    [ExcludeFromCodeCoverage]
    public static class MapperCreate
    {
        private static IServiceProvider _provider = null;
        public static IMapper CreateMappers()
        {
            IServiceCollection services = new ServiceCollection();
            services.AddProfiles();
            if(_provider == null)
                _provider = services.BuildServiceProvider();

            var mapper = _provider.GetService<IMapper>();

            if (mapper == null)
                throw new Exception("Impossible create mapper for tests.");

            return mapper;
        }
    }
}
