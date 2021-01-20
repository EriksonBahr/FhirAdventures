namespace FhirPatientRegistration.Console
{
    using FhirPatientRegistration.Core;
    using Hl7.Fhir.Serialization;
    using System;
    using System.Linq;

    class Program
    {
        static void Main(string[] args)
        {
            var input = String.Empty;
            var repo = new FhirRepository("http://hapi.fhir.org/baseR4");

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
    }
}
