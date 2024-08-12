using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;
using Talabat.APIs.Errors;
using Talabat.APIs.Helpers;
using Talabat.APIs.Middlewares;
using Talabat.Core;
using Talabat.Core.Entities.Identity;
using Talabat.Core.IRepository;
using Talabat.Core.Services;
using Talabat.Repository;
using Talabat.Repository.Data;
using Talabat.Repository.Identity;
using Talabat.Service;

namespace Talabat.APIs
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            string? redisConnectionString = builder.Configuration["RedisConnection"];

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            //Configure options
            //Store Context
            builder.Services.AddDbContext<StoreContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            //Identity Context
            builder.Services.AddDbContext<AppIdentityDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection")));

            //Creat obj in Controller from IGeneric
            builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            //Allow DI For BasketRepository
            builder.Services.AddScoped(typeof(IBasketRepository), typeof(BasketRepository));

            //Unit of Work
            builder.Services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));

            //Order Service
            builder.Services.AddScoped(typeof(IOrderService), typeof(OrderService));

            //Auto Mapper
            builder.Services.AddAutoMapper(typeof(MappingProfiles));

            //Configure Redis
            builder.Services.AddSingleton<IConnectionMultiplexer>(x =>
                ConnectionMultiplexer.Connect(redisConnectionString!));

            //Allow DI for Identity Service
            builder.Services.AddIdentity<AppUser, IdentityRole>(options =>
            {

            }).AddEntityFrameworkStores<AppIdentityDbContext>();

            //Validation Error
            builder.Services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = (actionContext) =>
                {
                    string[] errors = actionContext.ModelState.Where(p => p.Value.Errors.Count() > 0)
                                                         .SelectMany(p => p.Value.Errors)
                                                         .Select(e => e.ErrorMessage)
                                                         .ToArray();
                    var validationErrorResponse = new ApiValidationErrorResponse()
                    {
                        Errors = errors
                    };

                    return new BadRequestObjectResult(validationErrorResponse);
                };
            });

            

            var app = builder.Build();

            app.UseMiddleware<ExceptionMiddleware>();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            //Update-Database Automatically while runnig app
            using var scope = app.Services.CreateScope();
            var services = scope.ServiceProvider;
            var _dbContext = services.GetRequiredService<StoreContext>();
            var _IdentityDbContext = services.GetRequiredService<AppIdentityDbContext>();

            var loggerfactory = services.GetRequiredService<ILoggerFactory>();

            try
            {
                await _dbContext.Database.MigrateAsync(); //Updating database
                await StoreContextSeed.SeedAsync(_dbContext); //Data Seeding

                await _IdentityDbContext.Database.MigrateAsync(); //Updating Identity database

                //Identity Seed
                UserManager<AppUser> _userManager = services.GetRequiredService<UserManager<AppUser>>();
                await AppIdentityDbContextSeed.SeedUserAsync(_userManager);
            }
            catch (Exception ex)
            {
                var logger = loggerfactory.CreateLogger<Program>();
                logger.LogError(ex, "An Error has been occured during applying migration");
            }

            app.Run();
        }
    }
}
