Feature: PersistPatient
	In order to be able to retrieve the patient at a different moment
	As a nurse
	I want to add, edit and delete a patient

@mytag
Scenario: Shall be able to CRUD patients
	Given an existing FHIR database 'http://hapi.fhir.org/baseR4' new
	When a new 'male' patient named 'Elliot Robot' with dob of '1980-oct-22' is created
	When the same patient's contact "phone" is defined as "+55 12 123123123123"
	Then the patient 'Elliot Robot' shall contain the 'phone' defined as '+55 12 123123123123'