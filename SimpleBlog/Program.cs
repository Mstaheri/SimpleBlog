using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using SimpleBlog.Features.Posts.GetPostByIds;
using SimpleBlog.Features.Posts.CreatePosts;
using SimpleBlog.Infrastructure;
using Swashbuckle.Swagger;
using SimpleBlog.Features.Posts.EditPosts;
using SimpleBlog.Features.Posts.DeletePosts;
using SimpleBlog.Features.Comments.CreateComments;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//Database
string connection = builder.Configuration.GetValue<string>("ConnectionString:sqlServer");
builder.Services.AddSqlServer<BlogDbContext>(connection);

builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
//Post
//CreatePost
builder.Services.AddScoped<AppServicePostCreate>();
builder.Services.AddScoped<IValidator<InputPostCreate>, ValidatorPostCreate>();
//PostByIds
builder.Services.AddScoped<AppServiceGetPostByIds>();
//EditPosts
builder.Services.AddScoped<AppServicePostEdit>();
builder.Services.AddScoped<IValidator<InputPostEdit>, ValidatorPostEdit>();
//DeletePosts
builder.Services.AddScoped<AppServicePostDelete>();
//Comments
//CreateComments
builder.Services.AddScoped<AppServiceCreateComment>();
builder.Services.AddScoped<IValidator<InputCreateComment>, ValidatorCreateComment>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    
}

app.UseHttpsRedirection();

app.Run();