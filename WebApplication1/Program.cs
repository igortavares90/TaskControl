using Microsoft.AspNetCore.Cors.Infrastructure;
using System.Text.Json.Serialization;
using TaskControl.Application.Services;
using TaskControl.Domain.Interfaces.Database;
using TaskControl.Domain.Interfaces.Repository;
using TaskControl.Domain.Interfaces.Service;
using TaskControl.Infrastructure.Data.Repositories;
using TaskControl.Infrastructure.Uow;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
                .AddJsonOptions(options =>
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
builder.Services.AddTransient<IProjectService, ProjectService>();
builder.Services.AddTransient<IProjectRepository, ProjectRepository>();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<ITaskService, TaskService>();
builder.Services.AddTransient<ITaskRepository, TaskRepository>();
builder.Services.AddTransient<ITaskHistoryService, TaskHistoryService>();
builder.Services.AddTransient<ITaskHistoryRepository, TaskHistoryRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
