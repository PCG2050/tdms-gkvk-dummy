namespace Application.Interface
{
    /// <summary>
    /// Marker interface for DTOs with an Id property
    /// </summary>
    public interface IBaseDto
    {
        int Id { get; set; }
    }

    /// <summary>
    /// Marker interface for Create DTOs
    /// </summary>
    public interface ICreateDto
    {
    }

    /// <summary>
    /// Marker interface for Update DTOs (must have Id property)
    /// </summary>
    public interface IUpdateDto
    {
        int Id { get; set; }
    }

    /// <summary>
    /// Marker interface for Complete DTOs (DTOs with nested collections)
    /// </summary>
    public interface ICompleteDto : IBaseDto
    {
    }
}
