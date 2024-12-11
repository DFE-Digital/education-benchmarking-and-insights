DROP VIEW IF EXISTS SchoolCensusHistoricWithNulls
GO

CREATE VIEW SchoolCensusHistoricWithNulls AS
  SELECT URN
       , Year
       , FinanceType
       , OverallPhase
       , TotalPupils
       , CASE
             WHEN WorkforceFTE IS NULL OR WorkforceFTE = 0 THEN NULL
             ELSE WorkforceFTE
         END AS WorkforceFTE
       , CASE
             WHEN WorkforceHeadcount IS NULL OR WorkforceHeadcount = 0 THEN NULL
             ELSE WorkforceHeadcount
         END AS WorkforceHeadcount
       , CASE
             WHEN TeachersFTE IS NULL OR TeachersFTE = 0 THEN NULL
             ELSE TeachersFTE
         END AS TeachersFTE
       , CASE
             WHEN TeachersHeadcount IS NULL OR TeachersHeadcount = 0 THEN NULL
             ELSE TeachersHeadcount
         END AS TeachersHeadcount
       , CASE
             WHEN SeniorLeadershipFTE IS NULL OR SeniorLeadershipFTE = 0 THEN NULL
             ELSE SeniorLeadershipFTE
         END AS SeniorLeadershipFTE
       , CASE
             WHEN SeniorLeadershipHeadcount IS NULL OR SeniorLeadershipHeadcount = 0 THEN NULL
             ELSE SeniorLeadershipHeadcount
         END AS SeniorLeadershipHeadcount
       , CASE
             WHEN TeachingAssistantFTE IS NULL OR TeachingAssistantFTE = 0 THEN NULL
             ELSE TeachingAssistantFTE
         END AS TeachingAssistantFTE
       , CASE
             WHEN TeachingAssistantHeadcount IS NULL OR TeachingAssistantHeadcount = 0 THEN NULL
             ELSE TeachingAssistantHeadcount
         END AS TeachingAssistantHeadcount
       , CASE
             WHEN NonClassroomSupportStaffFTE IS NULL OR NonClassroomSupportStaffFTE = 0 THEN NULL
             ELSE NonClassroomSupportStaffFTE
         END AS NonClassroomSupportStaffFTE
       , CASE
             WHEN NonClassroomSupportStaffHeadcount IS NULL OR NonClassroomSupportStaffHeadcount = 0 THEN NULL
             ELSE NonClassroomSupportStaffHeadcount
         END AS NonClassroomSupportStaffHeadcount
       , CASE
             WHEN AuxiliaryStaffFTE IS NULL OR AuxiliaryStaffFTE = 0 THEN NULL
             ELSE AuxiliaryStaffFTE
         END AS AuxiliaryStaffFTE
       , CASE
             WHEN AuxiliaryStaffHeadcount IS NULL OR AuxiliaryStaffHeadcount = 0 THEN NULL
             ELSE AuxiliaryStaffHeadcount
         END AS AuxiliaryStaffHeadcount
       , PercentTeacherWithQualifiedStatus
    FROM SchoolCensusHistoric
GO
