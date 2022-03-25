namespace Core.Domain.Entities
{
    public class BaseEntity:BaseEntity<int>
    {
    }

    public class BaseEntity<T>:ISDeleted,IEntity
    {
        public bool IsDeleted { get ; set; }
        public T Id { get ; set ; }
    }
    public interface IEntity
    {}
}
