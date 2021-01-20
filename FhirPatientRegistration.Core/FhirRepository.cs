using Hl7.Fhir.Model;
using Hl7.Fhir.Rest;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FhirPatientRegistration.Core
{
    public class FhirRepository
    {
        private string v;
        private FhirClient fhirClient;

        public FhirRepository(string v)
        {
            this.v = v;
            this.fhirClient = new FhirClient(v);
            fhirClient.PreferredFormat = ResourceFormat.Json;
            fhirClient.UseFormatParam = true;
        }

        public IEnumerable<Patient> GetPatientsByName(string p0)
        {
            //https://docs.fire.ly/fhirnetapi/client/search.html
            var param = new SearchParams();

            var names = p0.Split(' ');

            if (names.Count() == 0)
            {
                p0 += " ";
                names = p0.Split(' ');
            }

            foreach (var name in names)
            {
                param.Add("given", name);
            }

            param.Add(SearchParams.SEARCH_PARAM_SORT, "_id"); 
            var ret = fhirClient.Search<Patient>(param);
            var patients = new List<Patient>();
            patients = ret.Entry.Select(p => (Patient)p.Resource).ToList();

            return patients;
        }

        public Patient AddPatient(Patient pat)
        {
            return fhirClient.Create<Patient>(pat);
        }

        public Patient Save(Patient persistedPatient)
        {
            return fhirClient.Update<Patient>(persistedPatient);
        }
    }
}