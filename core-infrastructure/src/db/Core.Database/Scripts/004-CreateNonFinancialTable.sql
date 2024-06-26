IF NOT EXISTS(SELECT *
              FROM INFORMATION_SCHEMA.TABLES
              WHERE table_name = 'NonFinancial')
    BEGIN
        CREATE TABLE dbo.NonFinancial
        (
            RunType                                       nvarchar(50) NOT NULL,
            RunId                                         nvarchar(50) NOT NULL,
            URN                                           nvarchar(6)  NOT NULL,
            EstablishmentType                             nvarchar(50) NULL,
            TotalInternalFloorArea                        decimal      NULL,
            BuildingAverageAge                            decimal      NULL,
            TotalPupils                                   decimal      NULL,
            TotalPupilsSixthForm                          decimal      NULL,
            TotalPupilsNursery                            decimal      NULL,
            WorkforceHeadcount                            decimal      NULL,
            WorkforceFTE                                  decimal      NULL,
            WorkforceHeadcountPerFTE                      decimal      NULL,
            WorkforcePercentTotalWorkforce                decimal      NULL,
            WorkforcePerPupil                             decimal      NULL,
            TeachersHeadcount                             decimal      NULL,
            TeachersFTE                                   decimal      NULL,
            TeachersHeadcountPerFTE                       decimal      NULL,
            TeachersPercentTotalWorkforce                 decimal      NULL,
            TeachersPerPupil                              decimal      NULL,
            SeniorLeadershipHeadcount                     decimal      NULL,
            SeniorLeadershipFTE                           decimal      NULL,
            SeniorLeadershipHeadcountPerFTE               decimal      NULL,
            SeniorLeadershipPercentTotalWorkforce         decimal      NULL,
            SeniorLeadershipPerPupil                      decimal      NULL,
            TeachingAssistantHeadcount                    decimal      NULL,
            TeachingAssistantFTE                          decimal      NULL,
            TeachingAssistantHeadcountPerFTE              decimal      NULL,
            TeachingAssistantPercentTotalWorkforce        decimal      NULL,
            TeachingAssistantPerPupil                     decimal      NULL,
            NonClassroomSupportStaffHeadcount             decimal      NULL,
            NonClassroomSupportStaffFTE                   decimal      NULL,
            NonClassroomSupportStaffHeadcountPerFTE       decimal      NULL,
            NonClassroomSupportStaffPercentTotalWorkforce decimal      NULL,
            NonClassroomSupportStaffPerPupil              decimal      NULL,
            AuxiliaryStaffHeadcount                       decimal      NULL,
            AuxiliaryStaffFTE                             decimal      NULL,
            AuxiliaryStaffHeadcountPerFTE                 decimal      NULL,
            AuxiliaryStaffPercentTotalWorkforce           decimal      NULL,
            AuxiliaryStaffPerPupil                        decimal      NULL,
            PercentTeacherWithQualifiedStatus             decimal      NULL,
            PercentFreeSchoolMeals                        decimal      NULL,
            PercentSpecialEducationNeeds                  decimal      NULL,
            PercentWithEducationalHealthCarePlan          decimal      NULL,
            PercentWithoutEducationalHealthCarePlan       decimal      NULL,
            KS2Progress                                   decimal      NULL,
            KS4Progress                                   decimal      NULL,
            PredictedPercentChangePupils3To5Years         decimal      NULL,
            PercentWithVI                                 decimal      NULL,
            PercentWithSPLD                               decimal      NULL,
            PercentWithSLD                                decimal      NULL,
            PercentWithSLCN                               decimal      NULL,
            PercentWithSEMH                               decimal      NULL,
            PercentWithPMLD                               decimal      NULL,
            PercentWithPD                                 decimal      NULL,
            PercentWithOTH                                decimal      NULL,
            PercentWithMSI                                decimal      NULL,
            PercentWithMLD                                decimal      NULL,
            PercentWithHI                                 decimal      NULL,
            PercentWithASD                                decimal      NULL,

            CONSTRAINT PK_NonFinancial PRIMARY KEY (RunType, RunId, URN)
        );
    END;