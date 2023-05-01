using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using HR.LeaveManagement.UI;
using HR.LeaveManagement.UI.Contracts;
using HR.LeaveManagement.UI.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddScoped<IHrHttpClient, HrHttpClient>();
builder.Services.AddScoped<ILeaveTypeRepository, LeaveTypeRepository>();
await builder.Build().RunAsync();