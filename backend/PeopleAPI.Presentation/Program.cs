using Microsoft.AspNetCore.Mvc.ApiExplorer;
using PeopleAPI.Presentation.Swagger;
using PeopleAPI.CrossCutting.IoC.Infrastructure; 
using PeopleAPI.CrossCutting.IoC.Application;
using PeopleAPI.Presentation.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerConfiguration();
builder.Services.AddInfrastructure();
builder.Services.AddApplication(builder.Configuration);

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(options =>
{
    var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();
    foreach (var description in provider.ApiVersionDescriptions)
    {
        options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
    }

    options.DefaultModelsExpandDepth(-1);
});

app.UseHttpsRedirection();

app.UseAuthentication(); 

app.UseAuthorization();

app.UseMiddleware(typeof(ErrorMiddleware)); 

app.MapControllers();

app.Run();
