Feature: Insights expenditure endpoints

    Scenario: Sending a valid school expenditure request with category and dimension
        Given a school expenditure request with urn '990000', category 'TotalExpenditure' and dimension 'Actuals'
        When I submit the insights expenditure request
        Then the school expenditure response should be ok, contain a JSON object and match the expected output of 'SchoolExpenditureTotalExpenditureActuals.json'

    Scenario: Sending a valid school expenditure request with dimension
        Given a school expenditure request with urn '990000', category '' and dimension 'Actuals'
        When I submit the insights expenditure request
        Then the school expenditure response should be ok, contain a JSON object and match the expected output of 'SchoolExpenditureNoCategoryActuals.json'

    Scenario: Sending a valid school expenditure request with bad URN
        Given a school expenditure request with urn '0000000', category '' and dimension 'Actuals'
        When I submit the insights expenditure request
        Then the school expenditure result should be not found

    Scenario: Sending an invalid school expenditure request
        Given a school expenditure request with urn '990000', category 'Invalid' and dimension ''
        When I submit the insights expenditure request
        Then the school expenditure result should be bad request

    Scenario: Sending a valid school expenditure history request
        Given a valid school expenditure history request with urn '990000'
        When I submit the insights expenditure request
        Then the school expenditure response should be ok, contain a JSON object and match the expected output of 'SchoolExpenditureHistory.json'
        
    Scenario: Sending a valid school expenditure query request with URNs
        Given a valid school expenditure query request with urns:
          | Urn    |
          | 990000 |
          | 990001 |
          | 990002 |
        When I submit the insights expenditure request
        Then the school expenditure response should be ok, contain a JSON array and match the expected output of 'SchoolExpenditureMultipleUrns.json'
        
    Scenario: Sending a valid school expenditure query request with company number and phase
        Given a valid school expenditure query request with company number '08104190' and phase 'Secondary'
        When I submit the insights expenditure request
        Then the school expenditure response should be ok, contain a JSON array and match the expected output of 'SchoolExpenditureTrustSecondary.json'
        
    Scenario: Sending a valid school expenditure query request with LA code and phase
        Given a valid school expenditure query request with LA code '205' and phase 'Secondary'
        When I submit the insights expenditure request
        Then the school expenditure response should be ok, contain a JSON array and match the expected output of 'SchoolExpenditureLASecondary.json'
        
    Scenario: Sending a valid trust expenditure request with category and dimension
        Given a trust expenditure request with company number '10192252', category 'TotalExpenditure' and dimension 'Actuals'
        When I submit the insights expenditure request
        Then the trust expenditure response should be ok, contain a JSON object and match the expected output of 'TrustExpenditureTotalExpenditureActuals.json'

    Scenario: Sending a valid trust expenditure request with category Utilities and dimension PerUnit
        Given a trust expenditure request with company number '10192252', category 'Utilities' and dimension 'PerUnit'
        When I submit the insights expenditure request
        Then the trust expenditure response should be ok, contain a JSON object and match the expected output of 'TrustExpenditureUtilitiesPerUnit.json'

    Scenario: Sending a valid trust expenditure request with category Utilities and dimension PercentExpenditure
        Given a trust expenditure request with company number '10192252', category 'Utilities' and dimension 'PercentExpenditure'
        When I submit the insights expenditure request
        Then the trust expenditure response should be ok, contain a JSON object and match the expected output of 'TrustExpenditureUtilitiesPercentExpenditure.json'

    Scenario: Sending a valid trust expenditure request with category Utilities and dimension PercentIncome
        Given a trust expenditure request with company number '10192252', category 'Utilities' and dimension 'PercentIncome'
        When I submit the insights expenditure request
        Then the trust expenditure response should be ok, contain a JSON object and match the expected output of 'TrustExpenditureUtilitiesPercentIncome.json'

    Scenario: Sending a valid trust expenditure request with dimension
        Given a trust expenditure request with company number '10192252', category '' and dimension 'Actuals'
        When I submit the insights expenditure request
        Then the trust expenditure response should be ok, contain a JSON object and match the expected output of 'TrustExpenditureActuals.json'

    Scenario: Sending a trust expenditure request with bad company number
        Given a trust expenditure request with company number '10000000', category '' and dimension 'Actuals'
        When I submit the insights expenditure request
        Then the trust expenditure result should be not found

    Scenario: Sending an invalid trust expenditure request
        Given a trust expenditure request with company number '10192252', category 'Invalid' and dimension ''
        When I submit the insights expenditure request
        Then the trust expenditure result should be bad request

    Scenario: Sending a valid trust expenditure history request
        Given a valid trust expenditure history request with company number '10192252'
        When I submit the insights expenditure request
        Then the trust expenditure response should be ok, contain a JSON object and match the expected output of 'TrustExpenditureHistory.json'
        
    Scenario: Sending a valid trust expenditure query request
        Given a valid trust expenditure query request with company numbers:
          | CompanyNumber |
          | 10249712      |
          | 10259334      |
          | 10264735      |
        When I submit the insights expenditure request
        Then the trust expenditure response should be ok, contain a JSON array and match the expected output of 'TrustsExpenditureQuery.json'

    Scenario: Sending a valid school average across comparator set expenditure history request with dimension Actuals
        Given a school average across comparator set expenditure history request with urn '990000' and dimension 'Actuals'
        When I submit the insights expenditure request
        Then the school expenditure response should be ok, contain a JSON object and match the expected output of 'SchoolExpenditureAverageAcrossComparatorSetActuals.json'
        
    Scenario: Sending a valid school average across comparator set expenditure history request with dimension PerUnit
        Given a school average across comparator set expenditure history request with urn '990000' and dimension 'PerUnit'
        When I submit the insights expenditure request
        Then the school expenditure response should be ok, contain a JSON object and match the expected output of 'SchoolExpenditureAverageAcrossComparatorSetPerUnit.json'
        
    Scenario: Sending a valid school average across comparator set expenditure history request with dimension PercentIncome
        Given a school average across comparator set expenditure history request with urn '990000' and dimension 'PercentIncome'
        When I submit the insights expenditure request
        Then the school expenditure response should be ok, contain a JSON object and match the expected output of 'SchoolExpenditureAverageAcrossComparatorSetPercentIncome.json'
        
    Scenario: Sending a valid school average across comparator set expenditure history request with dimension PercentExpenditure
        Given a school average across comparator set expenditure history request with urn '990000' and dimension 'PercentExpenditure'
        When I submit the insights expenditure request
        Then the school expenditure response should be ok, contain a JSON object and match the expected output of 'SchoolExpenditureAverageAcrossComparatorSetPercentExpenditure.json'
        
    Scenario: Sending an invalid school average across comparator set expenditure history request
        Given a school average across comparator set expenditure history request with urn '990000' and dimension 'invalid'
        When I submit the insights expenditure request
        Then the school expenditure result should be bad request

    Scenario: Sending a valid school national average expenditure history request with dimension Actuals, phase Primary, financeType Maintained
        Given a school national average expenditure history request with dimension 'Actuals', phase 'Primary', financeType 'Maintained'
        When I submit the insights expenditure request
        Then the school expenditure response should be ok, contain a JSON object and match the expected output of 'SchoolExpenditureNationalAvgHistoryActualsPrimaryMaintained.json'
        
    Scenario: Sending a valid school national average expenditure history request with dimension PerUnit, phase Primary, financeType Maintained
        Given a school national average expenditure history request with dimension 'PerUnit', phase 'Primary', financeType 'Maintained'
        When I submit the insights expenditure request
        Then the school expenditure response should be ok, contain a JSON object and match the expected output of 'SchoolExpenditureNationalAvgHistoryPerUnitPrimaryMaintained.json'
        
    Scenario: Sending a valid school national average expenditure history request with dimension PercentIncome, phase Primary, financeType Maintained
        Given a school national average expenditure history request with dimension 'PercentIncome', phase 'Primary', financeType 'Maintained'
        When I submit the insights expenditure request
        Then the school expenditure response should be ok, contain a JSON object and match the expected output of 'SchoolExpenditureNationalAvgHistoryPercentIncomePrimaryMaintained.json'
        
    Scenario: Sending a valid school national average expenditure history request with dimension PercentExpenditure, phase Primary, financeType Maintained
        Given a school national average expenditure history request with dimension 'PercentExpenditure', phase 'Primary', financeType 'Maintained'
        When I submit the insights expenditure request
        Then the school expenditure response should be ok, contain a JSON object and match the expected output of 'SchoolExpenditureNationalAvgHistoryPercentExpenditurePrimaryMaintained.json'
        
    Scenario: Sending a valid school national average expenditure history request with dimension Actuals, phase Secondary, financeType Academy
        Given a school national average expenditure history request with dimension 'Actuals', phase 'Secondary', financeType 'Academy'
        When I submit the insights expenditure request
        Then the school expenditure response should be ok, contain a JSON object and match the expected output of 'SchoolExpenditureNationalAvgHistoryActualsSecondaryAcademy.json'
        
    Scenario: Sending a valid school national average expenditure history request with dimension PerUnit, phase Special, financeType Academy
        Given a school national average expenditure history request with dimension 'PerUnit', phase 'Special', financeType 'Academy'
        When I submit the insights expenditure request
        Then the school expenditure response should be ok, contain a JSON object and match the expected output of 'SchoolExpenditureNationalAvgHistoryPerUnitSpecialAcademy.json'
        
    Scenario: Sending an invalid school national average expenditure history request with invalid dimension, phase Primary, financeType Maintained
        Given a school national average expenditure history request with dimension 'invalid', phase 'Primary', financeType 'Maintained'
        When I submit the insights expenditure request
        Then the school expenditure result should be bad request

    Scenario: Sending an invalid school national average expenditure history request with dimension Actuals, invalid phase, financeType Maintained
        Given a school national average expenditure history request with dimension 'Actuals', phase 'invalid', financeType 'Maintained'
        When I submit the insights expenditure request
        Then the school expenditure result should be bad request

    Scenario: Sending an invalid school national average expenditure history request with dimension Actuals, phase Primary, invalid financeType
        Given a school national average expenditure history request with dimension 'Actuals', phase 'Primary', financeType 'invalid'
        When I submit the insights expenditure request
        Then the school expenditure result should be bad request

    Scenario: Sending a valid multiple trust expenditure query request with category 'NonEducationalSupportStaff',  dimension 'Actuals' and excludeCentralServices 'true'
        Given a valid trust expenditure query request for category 'NonEducationalSupportStaff', dimension 'Actuals', excludeCentralServices 'true', with company numbers:
          | CompanyNumber |
          | 10249712      |
          | 10259334      |
          | 10264735      |
        When I submit the insights expenditure request
        Then the trust expenditure response should be ok, contain a JSON array and match the expected output of 'TrustsExpenditureNonEduStaffActualsExcludeCentral.json'

    Scenario: Sending a valid multiple trust expenditure query request with category 'NonEducationalSupportStaff', dimension 'PerUnit' and excludeCentralServices 'false'
        Given a valid trust expenditure query request for category 'NonEducationalSupportStaff', dimension 'PerUnit', excludeCentralServices 'false', with company numbers:
          | CompanyNumber |
          | 10249712      |
          | 10259334      |
          | 10264735      |
        When I submit the insights expenditure request
        Then the trust expenditure response should be ok, contain a JSON array and match the expected output of 'TrustsExpenditureNonEduStaffPerUnitIncludeCentral.json'

    Scenario: Sending a valid multiple trust expenditure query request with category 'NonEducationalSupportStaff', dimension 'PercentExpenditure' and excludeCentralServices 'true'
        Given a valid trust expenditure query request for category 'NonEducationalSupportStaff', dimension 'PercentExpenditure', excludeCentralServices 'true', with company numbers:
          | CompanyNumber |
          | 10249712      |
          | 10259334      |
          | 10264735      |
        When I submit the insights expenditure request
        Then the trust expenditure response should be ok, contain a JSON array and match the expected output of 'TrustsExpenditureNonEduStaffPercentExpenditureExcludeCentral.json'

    Scenario: Sending a valid multiple trust expenditure query request with category 'NonEducationalSupportStaff' and dimension 'PercentIncome' and excludeCentralServices 'false'
        Given a valid trust expenditure query request for category 'NonEducationalSupportStaff', dimension 'PercentIncome', excludeCentralServices 'false', with company numbers:
          | CompanyNumber |
          | 10249712      |
          | 10259334      |
          | 10264735      |
        When I submit the insights expenditure request
        Then the trust expenditure response should be ok, contain a JSON array and match the expected output of 'TrustsExpenditureNonEduStaffPercentIncomeIncludeCentral.json'

    Scenario: Sending a valid multiple trust expenditure query request with category 'EducationalIct',  dimension 'Actuals' and excludeCentralServices 'true'
        Given a valid trust expenditure query request for category 'EducationalIct', dimension 'Actuals', excludeCentralServices 'true', with company numbers:
          | CompanyNumber |
          | 10249712      |
          | 10259334      |
          | 10264735      |
        When I submit the insights expenditure request
        Then the trust expenditure response should be ok, contain a JSON array and match the expected output of 'TrustsExpenditureEducationalIctActualsExcludeCentral.json'

    Scenario: Sending a valid multiple trust expenditure query request with category 'EducationalIct',  dimension 'Actuals' and excludeCentralServices 'false'
        Given a valid trust expenditure query request for category 'EducationalIct', dimension 'Actuals', excludeCentralServices 'false', with company numbers:
          | CompanyNumber |
          | 10249712      |
          | 10259334      |
          | 10264735      |
        When I submit the insights expenditure request
        Then the trust expenditure response should be ok, contain a JSON array and match the expected output of 'TrustsExpenditureEducationalIctActualsIncludeCentral.json'