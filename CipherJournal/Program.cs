using CipherJournal.Models;
using CipherJournal.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<CipherService>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowFrontend");

app.MapGet("/entries", (CipherService svc) => svc.GetAllSummaries());

app.MapGet("/entries/{id}", (int id, CipherService svc) =>
    svc.GetSummaryById(id) is { } entry ? Results.Ok(entry) : Results.NotFound());

app.MapGet("/entries/{id}/hint", (int id, CipherService svc) =>
    svc.GetHint(id) is { } hint ? Results.Ok(new { hint }) : Results.NotFound());

app.MapPost("/entries/{id}/attempt", (int id, AttemptRequest request, CipherService svc) =>
    Results.Ok(svc.CheckAttempt(id, request.Answer)));

app.Run();