namespace PeopleAPI.Domain.Entities.Base; 

public class BaseEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public DateTimeOffset? CreateTime { get; set; }
    public DateTimeOffset? UpdateTime { get; set; }
}
