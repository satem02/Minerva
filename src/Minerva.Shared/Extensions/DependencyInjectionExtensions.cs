﻿using System;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Minerva.Shared.Data;
using Minerva.Shared.Data.Entities;
using Minerva.Shared.Repositories;
using Minerva.Shared.Repositories.Implementations;
using Minerva.Shared.Services;
using Minerva.Shared.Services.Implementations;

namespace Minerva.Shared.Extensions
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddRepositoryLayer(this IServiceCollection services)
        {
            services.AddScoped<IPostRepository, PostRepository>();
            return services;
        }

        public static IServiceCollection AddServiceLayer(this IServiceCollection services)
        {
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IPostService, PostService>();
            return services;
        }

        public static IServiceCollection AddCustomIdentity(this IServiceCollection services)
        {
            var configuration = services.BuildServiceProvider().GetRequiredService<IConfiguration>();
            services.AddDbContext<MinervaDbContext>(options =>
                options.UseNpgsql(
                    configuration.GetConnectionString("DbConnection")));

            services.AddIdentityCore<UserEntity>()
                .AddEntityFrameworkStores<MinervaDbContext>();

            services.Configure<IdentityOptions>(options =>
            {
                // Password settings.
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 1;

                // Lockout settings.
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                // User settings.
                options.User.AllowedUserNameCharacters =
                    "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = false;
            });

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(cfg =>
                {
                    cfg.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("bhl1lIyDBPPyeXj8TCLnHd1YI1NMTD6S")),

                        ValidateIssuer = true,
                        ValidIssuer = "MusicDb",

                        ValidateAudience = true,
                        ValidAudience = "MusicDb",

                        ValidateLifetime = true, //validate the expiration and not before values in the token

                        ClockSkew = TimeSpan.FromMinutes(30) //5 minute tolerance for the expiration date
                    };
                });

            return services;
        }

    }
}