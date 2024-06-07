IF EXISTS(SELECT *
          FROM INFORMATION_SCHEMA.TABLES
          WHERE table_name = 'NonFinancial')
    BEGIN
        ALTER TABLE dbo.NonFinancial
            DROP COLUMN
                WorkforceHeadcountPerFTE,
                WorkforcePercentTotalWorkforce,
                WorkforcePerPupil,
                TeachersHeadcountPerFTE,
                TeachersPercentTotalWorkforce,
                TeachersPerPupil,
                SeniorLeadershipHeadcountPerFTE,
                SeniorLeadershipPercentTotalWorkforce,
                SeniorLeadershipPerPupil,
                TeachingAssistantHeadcountPerFTE,
                TeachingAssistantPercentTotalWorkforce,
                TeachingAssistantPerPupil,
                NonClassroomSupportStaffHeadcountPerFTE,
                NonClassroomSupportStaffPercentTotalWorkforce,
                NonClassroomSupportStaffPerPupil,
                AuxiliaryStaffHeadcountPerFTE,
                AuxiliaryStaffPercentTotalWorkforce,
                AuxiliaryStaffPerPupil;
    END;