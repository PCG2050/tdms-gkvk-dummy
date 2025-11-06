namespace Application.Models
{
    public class ContainerOperationInfo
    {
        public bool HasError { get; set; }
        public string Message { get; set; }
        public object ContainerInfo { get; set; }
    }
}
