ALTER VIEW [dbo].[SchoolCensus] AS
SELECT
   s.URN,
   s.SchoolName,
   s.SchoolType,
   s.OverallPhase,
   s.LACode,
   s.LAName,
   s.TrustCompanyNumber,
   nf.TotalPupils,
   nf.WorkforceFTE,
   nf.WorkforceHeadcount,
   nf.TeachersFTE,
   nf.TeachersHeadcount,
   nf.SeniorLeadershipFTE,
   nf.SeniorLeadershipHeadcount,
   nf.TeachingAssistantFTE,
   nf.TeachingAssistantHeadcount,
   nf.NonClassroomSupportStaffFTE,
   nf.NonClassroomSupportStaffHeadcount,
   nf.AuxiliaryStaffFTE,
   nf.AuxiliaryStaffHeadcount,
   nf.PercentTeacherWithQualifiedStatus
FROM
   School s
   LEFT JOIN CurrentDefaultNonFinancial nf ON nf.URN = s.URN