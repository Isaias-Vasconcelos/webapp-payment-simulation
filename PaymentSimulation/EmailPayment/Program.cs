﻿using EmailPayment.Consumer;
using MassTransit;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Sinks.PostgreSQL;

namespace EmailPayment;

public static class Program
{
    public static async Task Main(string[] args)
    {
        var columnWriters = new Dictionary<string, ColumnWriterBase>
        {
            {"message", new RenderedMessageColumnWriter()},
            {"message_template", new MessageTemplateColumnWriter()},
            {"level", new LevelColumnWriter()},
            {"timestamp", new TimestampColumnWriter()},
            {"exception", new ExceptionColumnWriter()},
            {"log_event", new LogEventSerializedColumnWriter()}
        };

        Log.Logger = new LoggerConfiguration()
            .WriteTo.PostgreSQL(
                connectionString: Environment.GetEnvironmentVariable("DATABASE_URL"),
                tableName: "log_service_email_payment",
                needAutoCreateTable: true,
                columnOptions: columnWriters
            )
            .CreateLogger();
        
        try
        {
            Log.Information("[MICROSERVICE] -> Started EmailPayment...");

            await Host.CreateDefaultBuilder(args)
                .UseSerilog()
                .ConfigureServices(services =>
                {
                    services.AddMassTransit(x =>
                    {
                        x.AddConsumer<PaymentProcessedConsumer>();

                        x.UsingRabbitMq((context, cfg) =>
                        {
                            cfg.Host("rabbitmq", "/", h =>
                            {
                                h.Username("guest");
                                h.Password("guest");
                            });

                            cfg.ReceiveEndpoint("payment-processed-email-queue", e =>
                            {
                                e.ConfigureConsumer<PaymentProcessedConsumer>(context);
                            });
                        });
                    });
                })
                .Build()
                .RunAsync();
        }
        catch (Exception ex)
        {
            Log.Fatal(ex, "Erro fatal ao iniciar o serviço");
        }
        finally
        {
            await Log.CloseAndFlushAsync();
        }
    }
}