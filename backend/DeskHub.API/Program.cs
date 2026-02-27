using DeskHub.API.Exceptions;
using DeskHub.Application;
using DeskHub.Infrastructure;
using Microsoft.OpenApi;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
var builder = WebApplication.CreateBuilder(args);

#region Services

builder.Services
    .AddControllers()
    .AddJsonOptions(options =>
        options.JsonSerializerOptions.Converters
            .Add(new JsonStringEnumConverter())
    );
builder.Services.AddOpenApi(options =>
{
    options.AddSchemaTransformer((schema, context, cancellationToken) =>
    {
        var type = context.JsonTypeInfo.Type;

        if (type.IsEnum)
        {
            schema.Type = JsonSchemaType.String;
            schema.Format = null;

            schema?.Enum?.Clear();

            foreach (var name in Enum.GetNames(type))
            {
                schema?.Enum?.Add(JsonValue.Create(name));
            }
        }

        return Task.CompletedTask;
    });
});

builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

#endregion

var app = builder.Build();

#region Middleware

app.UseExceptionHandler();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(options =>
        options.SwaggerEndpoint("/openapi/v1.json", "DeskHub API")
    );
}

app.UseHttpsRedirection();

//app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

#endregion

app.Run();
