namespace HR.LeaveManagement.UI.Services.Base;

public class Response<T>
{
    public string Message { get; set; }
    public string ValidationErrors { get; set; }
    public T Data { get; set; }
}