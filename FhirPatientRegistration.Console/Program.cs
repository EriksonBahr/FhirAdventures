namespace FhirPatientRegistration.Console
{
    using FhirPatientRegistration.Core;
    using Hl7.Fhir.Serialization;
    using Microsoft.Extensions.Http.Logging;
    using Microsoft.Extensions.Logging;
    using System;
    using System.Linq;
    using System.Net.Http;

    class Program
    {
        static void Main(string[] args)
        {
            ILogger logger = BuildLogger(LogLevel.Information);
            LoggingHttpMessageHandler handler = BuildLoggingHttpHandler(logger);
            var repo = new FhirRepository("http://hapi.fhir.org/baseR4", handler);

            string input;
            do
            {
                Console.WriteLine("Search for a patient (or Q to quit): ");
                input = Console.ReadLine();
                if (input.ToLower() == "q") { break; };

                var patients = repo.GetPatientsByName(input);
                Console.WriteLine($"Found {patients.Count()} patients with the name {input}. Showing the first.");
                if (patients.Count() > 0)
                    Console.WriteLine(patients.FirstOrDefault().ToJson(new FhirJsonSerializationSettings { Pretty = true}));


            } while (input.ToLower() != "q");
        }

        private static LoggingHttpMessageHandler BuildLoggingHttpHandler(ILogger logger)
        {
            var handler = new LoggingHttpMessageHandler(logger);
            handler.InnerHandler = new HttpClientHandler();
            return handler;
        }

        private static ILogger BuildLogger(LogLevel loglevel)
        {
            using var loggerFactory = LoggerFactory.Create(builder =>
            {
                builder
                    .AddFilter("*", loglevel)
                    .AddConsole();
            });

            return loggerFactory.CreateLogger<Program>();
        }
    }
}