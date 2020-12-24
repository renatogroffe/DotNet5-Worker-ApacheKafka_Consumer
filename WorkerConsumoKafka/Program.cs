using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace WorkerConsumoKafka
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine(
                "Testando o consumo de mensagens com Apache Kafka");            
            
            if (args.Length != 3)
            {
                Console.WriteLine(
                    "Informe 3 parametros: " +
                    "no primeiro a conexao para testes com o Kafka, " +
                    "no segundo o Topic a ser utilizado no consumo das mensagens, " +
                    "no terceiro o Group Id da aplicacao...");
                return;
            }

            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddSingleton<ParametrosExecucao>(
                        new ParametrosExecucao()
                        {
                            BootstrapServers = args[0],
                            Topic = args[1],
                            GroupId = args[2]
                        });
                    services.AddHostedService<Worker>();
                });
    }
}