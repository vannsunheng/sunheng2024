using APIBackend.Extensions;
using APIBackend.Mideleware;
using Core.Entities.Identity;
using Infrastructure.Data;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddIdentityService(builder.Configuration);
builder.Services.AddSwaggerDocumentation();
var app = builder.Build();
app.UseMiddleware<ExceptionMiddleware>();
app.UseStatusCodePagesWithReExecute("/errors/{0}");
app.UseSwaggerDocumentation();
app.UseStaticFiles();

app.UseCors("CorsPolicy");

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();
using var scope =app.Services.CreateScope();
var services=scope.ServiceProvider;
var context=services.GetRequiredService<StoreContext>();
var Identitycontext=services.GetRequiredService<AppIdentityDBContext>();
var userManager=services.GetRequiredService<UserManager<AppUser>>();
var loger=services.GetRequiredService<ILogger<Program>>();
try
{
    await context.Database.MigrateAsync();
    await Identitycontext.Database.MigrateAsync();
    await StoreContextSeed.SeedAsync(context);
    await AppIdentityDBContextSeed.SeedUserAsync(userManager);
}
catch(Exception ex)
{
     loger.LogError(ex.ToString(),"An error occured during migrations");
}
app.Run();
