
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;


var builder=new HostBuilder();
builder.ConfigureWebJobs(p => { p.AddAzureStorageCoreServices(); p.AddAzureStorage(); });

builder.ConfigureLogging((q, a) => {

    a.AddConsole();
    // string key = q.Configuration["AzureKey"];
    string key = q.Configuration["APPINSIGHTS_INSTRUMENTATIONKEY"];

    a.AddApplicationInsightsWebJobs(p => p.InstrumentationKey = key);

});

var host = builder.Build();



using (host)
{
    host.Run();
}
