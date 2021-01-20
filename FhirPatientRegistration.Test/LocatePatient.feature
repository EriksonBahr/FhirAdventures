Feature: LocatePatient
	In order to be able to locate already admitted patients
	As a nurse
	I want to be able to search for patients based on its name

@mytag
Scenario: ShallBeAbleToRetrieveAPatientFromTheServerCaseInsensitive
	Given an existing FHIR database 'http://hapi.fhir.org/baseR4'
	When I search for a patient named 'Donald Simon'
	Then It should return at least 1 patients
	When I search for a patient named 'donald simon'
	Then It should return at least 1 patients
	And The date of birth of the patient 1 is '1982-jan-23'

Scenario: ShallBeAbleToRetrieveAPatientUsingFirstNameOnly
	Given an existing FHIR database 'http://hapi.fhir.org/baseR4'
	When I search for a patient named 'Donald'
	Then It should return at least 1 patients