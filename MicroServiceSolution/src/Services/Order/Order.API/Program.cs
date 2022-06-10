using MassTransit;
using Microsoft.EntityFrameworkCore;
using Order.DataInftrastructure;
using Order.Application.Consumers;

namespace Order.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDbContext<OrderDbContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("db")));
            builder.Services.AddMassTransit(configure => {

                configure.AddConsumer<CreateOrderMessageCommandConsumer>();
                configure.UsingRabbitMq((context, config) =>
                {
                    config.Host(builder.Configuration["RabbitMQUrl"], hostConfigurator =>
                    {
                        hostConfigurator.Username("guest");
                        hostConfigurator.Password("guest");
                    });

                    config.ReceiveEndpoint("create-order", endpointConfigurator =>
                    {
                        endpointConfigurator.ConfigureConsumer<CreateOrderMessageCommandConsumer>(context);
                    });


                });

            });

            //builder.Services.AddMassTransitHostedService();
                

            

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}