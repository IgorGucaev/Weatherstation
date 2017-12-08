namespace Station.Common.Interfaces
{
    public interface IDeletable
    {
        bool IsDeleted { get; }
        void MarkDeleted();
    }
}
