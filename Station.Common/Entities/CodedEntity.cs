namespace Station.Common.Entities
{
    public abstract class CodedEntity<T> : Entity<T>
    {
        public virtual string Code { get; protected set; }
    }
}
