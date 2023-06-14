namespace TestKSK.Data.Interfaces
{
    public interface IEntityWithTypedId<ID> : IEntity
    {
        public ID Id { get; set; }
    }
}
