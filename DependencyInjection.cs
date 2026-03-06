using System.Text;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http;
using FluentValidation;
using FluentValidation.AspNetCore;
using GymManagementSystem.Data;
using GymManagementSystem.Exceptions;
using GymManagementSystem.Services;
using GymManagementSystem.Validation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace GymManagementSystem
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices (this IServiceCollection services , IConfiguration configuration)
        {
            services.AddGymDbContext(configuration)
                    .AddProblemDetails()
                    .AddFluentValidationAutoValidation()
                    .AddEndpointsApiExplorer()
                    .AddSwagger()
                    .AddScoped<AuthService>()
                    .AddJwtToken(configuration)
                    .AddScoped<JwtTokenProvider>()
                    .AddValidatorsFromAssemblyContaining<CreateMemberRequestValidator>()
                    .AddExceptionHandler<GlobalExceptionHandler>()
                    .AddControllersWithJson()
                    .AddScoped<IMemberService,MemberService>()
                    .AddInvalidResponse();

            return services;
        }
 

        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new()
    {
        Title = "Gym Management System API",
        Version = "v1",
        Description = "API for managing gym members, coaches, subscriptions, and authentication.",
        Contact = new()
        {
            Name = "Magdy Mahmoud",
        }
    });

    // JWT AUTH
    options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Name = "Authorization",
        Description = "Enter JWT Token like: Bearer {your token}",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT"
    });

    options.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

            return services;
        }
        public static IServiceCollection AddJwtToken(this IServiceCollection services , IConfiguration configuration)
        {
            var jwtSetting = configuration.GetSection("JwtSettings");
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true ,
                    ValidateAudience = true ,
                    ValidateLifetime = true ,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSetting["Issuer"],
                    ValidAudience = jwtSetting["Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(jwtSetting["SecretKey"]!)
                    )
                };
            });
            return services;
        }
        public static IServiceCollection AddControllersWithJson (this IServiceCollection services)
        {
            services.AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter(null , false));
                options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
            });

            return services;
        }
        public static IServiceCollection AddGymDbContext(this IServiceCollection services , IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });

            return services;
        }
        private static IServiceCollection AddInvalidResponse(this IServiceCollection services)
        {
            services.Configure<ApiBehaviorOptions>(options =>
            {
        options.InvalidModelStateResponseFactory = context =>
    {
        var errors = context.ModelState
            .Where(x => x.Value.Errors.Count > 0)
            .ToDictionary(
                x => x.Key,
                x => x.Value.Errors.First().ErrorMessage
            );

        var problemDetails = new ProblemDetails
        {
            Type = "https://tools.ietf.org/html/rfc9110#section-15.5.1",
            Title = "One or more validation errors occurred.",
            Status = StatusCodes.Status400BadRequest,
            Instance = context.HttpContext.Request.Path
        };

        return new BadRequestObjectResult(new
        {
            problemDetails.Type,
            problemDetails.Title,
            problemDetails.Status,
            problemDetails.Instance,
            Errors = errors,
            TraceId = context.HttpContext.TraceIdentifier
        });
            };
            });

            return services;
    }
        }
    }
