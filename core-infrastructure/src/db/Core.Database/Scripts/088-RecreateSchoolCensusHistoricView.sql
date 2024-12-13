DROP VIEW IF EXISTS SchoolCensusHistoric
GO

CREATE VIEW SchoolCensusHistoric AS
  SELECT s.URN
       , s.Year
       , s.FinanceType
       , s.OverallPhase
       , nf.TotalPupils
       , nf.WorkforceFTE
       , nf.WorkforceHeadcount
       , nf.TeachersFTE
       , nf.TeachersHeadcount
       , nf.SeniorLeadershipFTE
       , nf.SeniorLeadershipHeadcount
       , nf.TeachingAssistantFTE
       , nf.TeachingAssistantHeadcount
       , nf.NonClassroomSupportStaffFTE
       , nf.NonClassroomSupportStaffHeadcount
       , nf.AuxiliaryStaffFTE
       , nf.AuxiliaryStaffHeadcount
       , nf.PercentTeacherWithQualifiedStatus
    FROM (
           SELECT URN
                , Year
                , FinanceType
                , OverallPhase
             FROM School
                , AARHistoryYears
         ) s
    LEFT
   OUTER
    JOIN NonFinancial nf
      ON s.Year = nf.RunId
     AND nf.URN = s.URN
     AND nf.RunType = 'default'
   WHERE s.FinanceType = 'Academy'
   UNION
     ALL
  SELECT s.URN
       , s.Year
       , s.FinanceType
       , s.OverallPhase
       , nf.TotalPupils
       , nf.WorkforceFTE
       , nf.WorkforceHeadcount
       , nf.TeachersFTE
       , nf.TeachersHeadcount
       , nf.SeniorLeadershipFTE
       , nf.SeniorLeadershipHeadcount
       , nf.TeachingAssistantFTE
       , nf.TeachingAssistantHeadcount
       , nf.NonClassroomSupportStaffFTE
       , nf.NonClassroomSupportStaffHeadcount
       , nf.AuxiliaryStaffFTE
       , nf.AuxiliaryStaffHeadcount
       , nf.PercentTeacherWithQualifiedStatus
    FROM (
           SELECT URN
                , Year
                , FinanceType
                , OverallPhase
             FROM School
                , CFRHistoryYears
         ) s
    LEFT
   OUTER
    JOIN NonFinancial nf
      ON s.Year = nf.RunId
     AND nf.URN = s.URN
     AND nf.RunType = 'default'
   WHERE s.FinanceType = 'Maintained'
GO
