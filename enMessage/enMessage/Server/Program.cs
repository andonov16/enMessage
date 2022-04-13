using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using enMessage.DataAccess;
using enMessage.DataAccess.Repositories;
using enMessage.Server;
using enMessage.Server.Hubs;
using Newtonsoft.Json;

var builder = WebApplication.CreateBuilder(args);


string connectionString = builder.Configuration.GetConnectionString("DefaultConnectionString");
builder.Services.AddDbContext<ChatContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddScoped<ChatRepository>();
builder.Services.AddScoped<MessageRepository>();
builder.Services.AddScoped<RoleRepository>();
builder.Services.AddScoped<UserRepository>();
builder.Services.AddScoped<RequestRepository>();


// Add services to the container.

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddSignalR();




//add swagger
builder.Services.AddSwaggerGen();

//add signalr
builder.Services.AddResponseCompression(opts =>
{
    opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
        new[] { "application/octet-stream" });
});

var json = new JsonSerializerSettings();
json.Formatting = Formatting.Indented;
json.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
JsonConvert.DefaultSettings = () => json;


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();


//app.MapBlazorHub();
app.MapHub<ChatHub>("/chathub");
app.UseRouting();

app.MapRazorPages();


app.MapControllers();

//add swagger
//accesed with https://localhost:7222/swagger/index.html
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Blazor API V1");
});

app.MapFallbackToFile("index.html");

app.Run();
