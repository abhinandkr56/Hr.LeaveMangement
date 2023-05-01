using HR.LeaveManagement.UI.Services.Base;

namespace HR.LeaveManagement.UI;

public interface IHrHttpClient
{
    Task<Response<T>> GetAsync<T>(string endpoint);
    Task<Response<T>> PostAsync<T>(string endpoint, object data);
    Task<Response<T>> PutAsync<T>(string endpoint, object data);
    Task<Response<T>> DeletAsync<T>(string endpoint);
}