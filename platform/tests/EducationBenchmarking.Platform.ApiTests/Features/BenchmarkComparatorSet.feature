Feature: Benchmark Comparator set Endpoint Testing

    Scenario: Create a comparator set successfully
        Given I have a valid comparator set request of size set to '7'
        When I submit the comparator set request
        Then a valid comparator set of size '7' should be returned
        Then the comparator result should be ok

    Scenario: Sending a bad comparator set request
        Given I have a invalid comparator set request
        When I submit the comparator set request
        Then the comparator result should be bad request

    Scenario: sending a valid comparator set characteristics request
        Given a valid comparator set characteristics request
        When I submit the comparator set characteristics request
        Then the comparator set characteristics result should be:
          | Code                                 | Description                                                                                                    |
          | LaCode                               | Local Authority Code                                                                                           |
          | NumberOfPupils                       | Number of pupils                                                                                               |
          | GenderOfPupils                       | Gender of pupils                                                                                               |
          | SchoolPhase                          | School phase                                                                                                   |
          | PeriodCoveredByReturn                | Period covered by return                                                                                       |
          | SchoolOverallPhase                   | School overall phase                                                                                           |
          | TypeofEstablishment                  | School type                                                                                                    |
          | UrbanRural                           | Urban/rural schools                                                                                            |
          | GovernmentOffice                     | Government office region                                                                                       |
          | LondonBorough                        | London borough                                                                                                 |
          | LondonWeighting                      | London weighting                                                                                               |
          | PercentageOfEligibleFreeSchoolMeals  | Eligibility for free school meals                                                                              |
          | PercentageOfPupilsWithStatementOfSen | Pupils with SEN who have statements or EHC plans                                                               |
          | PercentageOfPupilsOnSenRegister      | Pupils with special educational needs (SEN) who don't have statements or education and health care (EHC) plans |
          | PercentageOfPupilsWithEal            | Pupils with English as an additional language                                                                  |
          | PercentageBoarders                   | Boarders                                                                                                       |
          | AdmissionsPolicy                     | Admissions policy                                                                                              |
          | Pfi                                  | Part of a private finance initiative?                                                                          |
          | DoesTheSchoolHave6Form               | Does the school have a sixth form?                                                                             |
          | NumberIn6Form                        | Number in sixth form                                                                                           |
          | LowestAgePupils                      | Lowest age of pupils                                                                                           |
          | HighestAgePupils                     | Highest age of pupils                                                                                          |
          | PercentageQualifiedTeachers          | Percentage of teachers with qualified teacher status (full time equivalent)                                    |
          | FullTimeTa                           | Number of teaching assistants (full time equivalent)                                                           |
          | FullTimeOther                        | Number of non-classroom support staff – excluding auxiliary staff (full time equivalent)                       |
          | FullTimeAux                          | Number of Auxiliary staff (Full Time Equivalent)                                                               |
          | SchoolWorkforceFte                   | Number in the school workforce (full time equivalent)                                                          |
          | NumberOfTeachersFte                  | Number of teachers (full time equivalent)                                                                      |
          | SeniorLeadershipFte                  | Number in the senior leadership team (full time equivalent)                                                    |
          | OfstedRating                         | Ofsted rating                                                                                                  |
          | Ks2Actual                            | Key Stage 2 attainment                                                                                         |
          | Ks2Progress                          | Key Stage 2 progress                                                                                           |
          | AverageAttainment8                   | Average Attainment 8                                                                                           |
          | Progress8Measure                     | Progress 8 measure                                                                                             |
          | SpecificLearningDifficulty           | Specific learning difficulty                                                                                   |
          | ModerateLearningDifficulty           | Moderate learning difficulty                                                                                   |
          | SevereLearningDifficulty             | Severe learning difficulty                                                                                     |
          | ProfLearningDifficulty               | Profound and multiple learning difficulty                                                                      |
          | SocialHealth                         | Social, emotional and mental health                                                                            |
          | SpeechNeeds                          | Speech, language and communications needs                                                                      |
          | HearingImpairment                    | Hearing impairment                                                                                             |
          | VisualImpairment                     | Visual impairment                                                                                              |
          | MultiSensoryImpairment               | Multi-sensory impairment                                                                                       |
          | PhysicalDisability                   | Physical disability                                                                                            |
          | AutisticDisorder                     | Autistic spectrum disorder                                                                                     |
          | OtherLearningDiff                    | Other learning difficulty                                                                                      |
          | PerPupilExp                          | Expenditure per pupil                                                                                          |
          | PerPupilGf                           | Grant funding per pupil                                                                                        |
          | RrToIncome                           | RR to Income                                                                                                   |