namespace TodoApp.Shared.Commons.Abstractions
{
    public interface IRemovableEntity
    {
        bool IsRemove { get; set; }
        bool IsActive { get; set; }
    }
}
