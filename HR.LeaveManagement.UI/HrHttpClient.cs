using System.Net.Http.Json;
using HR.LeaveManagement.UI.Services.Base;
using Newtonsoft.Json;

namespace HR.LeaveManagement.UI;

public class HrHttpClient:IHrHttpClient
{
    private readonly HttpClient _client;
    private readonly string _baseUrl;
    public HrHttpClient(HttpClient client)
    {
        _client = client;
        this._baseUrl = "https://localhost:5001/";
    }

    public async Task<Response<T>> GetAsync<T>(string endpoint)
    {
        var response = await _client.GetAsync(_baseUrl + endpoint);
        var responseBody = await response.Content.ReadAsStringAsync();

        // Check if the response is successful
        if (response.IsSuccessStatusCode)
        {
            // Deserialize the response body into the generic type T
            var data = JsonConvert.DeserializeObject<T>(responseBody);

            // Create a new Response object with the deserialized data
            var result = new Response<T>
            {
                Message = "Success",
                Data = data
            };

            return result;
        }
        else
        {
            // If the response is not successful, deserialize the error message
            var error = JsonConvert.DeserializeObject<Response<object>>(responseBody);

            // Create a new Response object with the error message
            var result = new Response<T>
            {
                Message = error.Message,
                ValidationErrors = error.ValidationErrors
            };

            return result;
        }
    }
    
    public async Task<Response<T>> PostAsync<T>(string endpoint, object data)
    {
        var response = await _client.PostAsJsonAsync((_baseUrl + endpoint),data);
        var responseBody = await response.Content.ReadAsStringAsync();
        // Check if the response is successful
        if (response.IsSuccessStatusCode)
        {
            // Deserialize the response body into the generic type T
            var serialisedObj = JsonConvert.DeserializeObject<T>(responseBody);

            // Create a new Response object with the deserialized data
            var result = new Response<T>
            {
                Message = "Success",
                Data = serialisedObj
            };

            return result;
        }
        else
        {
            // If the response is not successful, deserialize the error message
            var error = JsonConvert.DeserializeObject<Response<object>>(responseBody);

            // Create a new Response object with the error message
            var result = new Response<T>
            {
                Message = error.Message,
                ValidationErrors = error.ValidationErrors
            };

            return result;
        }
    }
    
    public async Task<Response<T>> PutAsync<T>(string endpoint, object data)
    {
        var response = await _client.PutAsJsonAsync((_baseUrl + endpoint),data);
        var responseBody = await response.Content.ReadAsStringAsync();
        // Check if the response is successful
        if (response.IsSuccessStatusCode)
        {
            // Deserialize the response body into the generic type T
            var serialisedObj = JsonConvert.DeserializeObject<T>(responseBody);

            // Create a new Response object with the deserialized data
            var result = new Response<T>
            {
                Message = "Success",
                Data = serialisedObj
            };

            return result;
        }
        else
        {
            // If the response is not successful, deserialize the error message
            var error = JsonConvert.DeserializeObject<Response<object>>(responseBody);

            // Create a new Response object with the error message
            var result = new Response<T>
            {
                Message = error.Message,
                ValidationErrors = error.ValidationErrors
            };

            return result;
        }
    }
    
    public async Task<Response<T>> DeletAsync<T>(string endpoint)
    {
        var response = await _client.DeleteAsync(_baseUrl + endpoint);
        var responseBody = await response.Content.ReadAsStringAsync();
        // Check if the response is successful
        if (response.IsSuccessStatusCode)
        {
            // Deserialize the response body into the generic type T
            var serialisedObj = JsonConvert.DeserializeObject<T>(responseBody);

            // Create a new Response object with the deserialized data
            var result = new Response<T>
            {
                Message = "Success",
                Data = serialisedObj
            };

            return result;
        }
        else
        {
            // If the response is not successful, deserialize the error message
            var error = JsonConvert.DeserializeObject<Response<object>>(responseBody);

            // Create a new Response object with the error message
            var result = new Response<T>
            {
                Message = error.Message,
                ValidationErrors = error.ValidationErrors
            };

            return result;
        }
    }
}