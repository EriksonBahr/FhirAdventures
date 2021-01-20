using FhirPatientRegistration.Core;
using Hl7.Fhir.Model;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using TechTalk.SpecFlow;

namespace FhirPatientRegistration.Test
{
    [Binding]
    public class LocatePatientSteps
    {
        private FhirRepository repo;
        private IEnumerable<Patient> patients;

        [Given(@"an existing FHIR database '(.*)'")]
        public void GivenAnExistingFHIRDatabase(string p0)
        {
            repo = new FhirRepository(p0);
        }

        [When(@"I search for a patient named '(.*)'")]
        public void WhenISearchForAPatientNamed(string p0)
        {
            patients =  repo.GetPatientsByName(p0);
        }
        
        [Then(@"It should return at least (.*) patients")]
        public void ThenItShouldReturnAtLeastPatients(int p0)
        {
            Assert.That(patients.Count(), Is.GreaterThan(0));
        }

        [Then(@"The date of birth of the patient (.*) is '(.*)'")]
        public void ThenTheDateOfBirthOfThePatientIs(int p0, string p1)
        {
            var dob = DateTime.Parse(p1);
            Assert.That(dob, Is.EqualTo(DateTime.Parse(patients.ElementAt(p0).BirthDate)));
        }


    }
}
