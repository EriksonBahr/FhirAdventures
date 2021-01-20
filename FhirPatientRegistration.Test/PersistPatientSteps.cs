using FhirPatientRegistration.Core;
using Hl7.Fhir.Model;
using NUnit.Framework;
using System;
using System.Linq;
using TechTalk.SpecFlow;

namespace FhirPatientRegistration.Test
{
    [Binding]
    public class PersistPatientSteps
    {
        private FhirRepository repo;
        private Patient persistedPatient;

        [Given(@"an existing FHIR database '(.*)' new")]
        public void GivenAnExistingFHIRDatabaseNew(string p0)
        {
            repo = new FhirRepository(p0);
        }
        
        [When(@"a new '(.*)' patient named '(.*)' with dob of '(.*)' is created")]
        public void WhenANewPatientNamedWithDobOfIsCreated(string p0, string p1, string p2)
        {
            var pat = new Patient();
            pat.Gender = AdministrativeGender.Male;
            pat.Name.Add(new HumanName {
                Given = p1.Split(' ')
            });
            pat.BirthDate = DateTime.Parse(p2).ToString("yyyy-MM-dd");
            persistedPatient = repo.AddPatient(pat);
        }

        [When(@"the same patient's contact ""(.*)"" is defined as ""(.*)""")]
        public void WhenTheSamePatientSContactIsDefinedAs(string p0, string p1)
        {
            persistedPatient.Telecom.Add(new ContactPoint
            {
                System = ContactPoint.ContactPointSystem.Phone,
                Value = p1
            });
            persistedPatient = repo.Save(persistedPatient);
        }
        
        [Then(@"the patient '(.*)' shall contain the '(.*)' defined as '(.*)'")]
        public void ThenThePatientShallContainTheDefinedAs(string p0, string p1, string p2)
        {
           Assert.That(persistedPatient.Telecom.FirstOrDefault().Value, Is.EqualTo(p2));

            Assert.That(persistedPatient.Name.FirstOrDefault().Given,
                Is.EqualTo(repo.GetPatientsByName(p0).FirstOrDefault().Name.FirstOrDefault().Given));
        }
    }
}
