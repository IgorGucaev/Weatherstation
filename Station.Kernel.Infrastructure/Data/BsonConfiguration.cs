namespace Station.Kernel.Infrastructure.Data
{
    abstract public class BsonConfiguration<TEntity> where TEntity : class
    {
        abstract public void Configure();
    }
}