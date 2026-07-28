namespace TodoApp.Shared.Commons.Abstractions
{
    public interface IEntityId<TKey>
    {
        TKey Id { get; set; }
    }
}
