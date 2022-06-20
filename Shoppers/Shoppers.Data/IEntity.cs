namespace Shoppers.Data
{
    public interface IEntity<T>
    {
        public T Id { get; set; }
    }
}
