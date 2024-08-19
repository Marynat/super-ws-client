using super_ws.api.Services;
using super_ws.database.Extensions;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("appsettings.Development.json", true);
// Add services to the container.
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
builder.Services.AddSuperDbContext(builder.Configuration);
builder.Services.AddTransient<IQuoteModelService, QuoteModelService>();
builder.Services.AddRazorPages();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();
app.MapControllers();

await app.RunAsync();
