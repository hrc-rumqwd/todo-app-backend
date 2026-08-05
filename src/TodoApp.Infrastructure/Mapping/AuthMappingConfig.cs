using Mapster;
using TodoApp.Application.Auth.Commands.Register;
using TodoApp.Domain.Entities;

namespace TodoApp.Infrastructure.Mapping
{
    internal class AuthMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<RegisterCommand, AppUser>()
                .Map(dest => dest.UserName, src => src.Email)
                .Map(dest => dest.Email, src => src.Email)
                .Map(dest => dest.FullName, src => src.FullName);
        }
    }
}
