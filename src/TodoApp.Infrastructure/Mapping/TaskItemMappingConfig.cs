using Mapster;

namespace TodoApp.Infrastructure.Mapping
{
    public class TaskItemMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            //config.NewConfig<TaskItemDto, TaskItem>()
        }
    }
}
