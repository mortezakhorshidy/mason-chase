using Application.CommandHandlers;
using Application.Interfaces;
using Infrastructure.EventStore;
using Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddSingleton<IEventStore, InMemoryEventStore>();

builder.Services.AddScoped<CreateCustomerCommandHandler>();
builder.Services.AddScoped<UpdateCustomerCommandHandler>();
builder.Services.AddScoped<DeleteCustomerCommandHandler>();
builder.Services.AddSingleton<ICustomerUniquenessChecker, InMemoryCustomerUniquenessChecker>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();
app.Run();