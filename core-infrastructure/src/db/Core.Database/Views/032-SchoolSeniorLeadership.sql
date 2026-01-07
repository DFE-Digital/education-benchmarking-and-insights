DROP VIEW IF EXISTS VW_SchoolSeniorLeadershipActual
    GO

CREATE VIEW VW_SchoolSeniorLeadershipActual AS
SELECT f.RunId,
       f.RunType,
       f.URN,
       s.SchoolName,
       s.LAName,
       f.TotalPupils,
       f.SeniorLeadershipFTE AS 'SeniorLeadership',
       f.HeadTeacherFTE AS 'HeadTeacher',
       f.DeputyHeadTeacherFTE AS 'DeputyHeadTeacher',
       f.AssistantHeadTeacherFTE AS 'AssistantHeadTeacher',
       f.LeadershipNonTeacherFTE AS 'LeadershipNonTeacher'
FROM NonFinancial f
    LEFT JOIN School s ON s.URN = f.URN
    GO

DROP VIEW IF EXISTS VW_SchoolSeniorLeadershipPercentWorkforce
    GO

CREATE VIEW VW_SchoolSeniorLeadershipPercentWorkforce AS
SELECT f.RunId,
       f.RunType,
       f.URN,
       s.SchoolName,
       s.LAName,
       f.TotalPupils,
       IIF(WorkforceFTE > 0.0, (SeniorLeadershipFTE /  WorkforceFTE) * 100, NULL) AS 'SeniorLeadership',
       IIF(WorkforceFTE > 0.0, (HeadTeacherFTE /  WorkforceFTE) * 100, NULL) AS 'HeadTeacher',
       IIF(WorkforceFTE > 0.0, (DeputyHeadTeacherFTE /  WorkforceFTE) * 100, NULL) AS 'DeputyHeadTeacher',
       IIF(WorkforceFTE > 0.0, (AssistantHeadTeacherFTE /  WorkforceFTE) * 100, NULL) AS 'AssistantHeadTeacher',
       IIF(WorkforceFTE > 0.0, (LeadershipNonTeacherFTE /  WorkforceFTE) * 100, NULL) AS 'LeadershipNonTeacher'
FROM NonFinancial f
    LEFT JOIN School s ON s.URN = f.URN
    GO

DROP VIEW IF EXISTS VW_SchoolSeniorLeadershipDefaultActual
    GO

CREATE VIEW VW_SchoolSeniorLeadershipDefaultActual AS
SELECT RunId,
       URN,
       SchoolName,
       LAName,
       TotalPupils,
       SeniorLeadership,
       Headteacher,
       DeputyHeadTeacher,
       AssistantHeadTeacher,
       LeadershipNonTeacher
FROM VW_SchoolSeniorLeadershipActual
WHERE RunType = 'default'
    GO

DROP VIEW IF EXISTS VW_SchoolSeniorLeadershipDefaultPercentWorkforce
    GO

CREATE VIEW VW_SchoolSeniorLeadershipDefaultPercentWorkforce AS
SELECT RunId,
       URN,
       SchoolName,
       LAName,
       TotalPupils,
       SeniorLeadership,
       Headteacher,
       DeputyHeadTeacher,
       AssistantHeadTeacher,
       LeadershipNonTeacher
FROM VW_SchoolSeniorLeadershipPercentWorkforce
WHERE RunType = 'default'
    GO

DROP VIEW IF EXISTS VW_SchoolSeniorLeadershipCurrentDefaultActual
    GO

CREATE VIEW VW_SchoolSeniorLeadershipCurrentDefaultActual AS
SELECT URN,
       SchoolName,
       LAName,
       TotalPupils,
       SeniorLeadership,
       Headteacher,
       DeputyHeadTeacher,
       AssistantHeadTeacher,
       LeadershipNonTeacher
FROM VW_SchoolSeniorLeadershipDefaultActual
WHERE RunId = (SELECT Value FROM Parameters WHERE Name = 'CurrentYear')
    GO

DROP VIEW IF EXISTS VW_SchoolSeniorLeadershipCurrentDefaultPercentWorkforce
    GO

CREATE VIEW VW_SchoolSeniorLeadershipCurrentDefaultPercentWorkforce AS
SELECT URN,
       SchoolName,
       LAName,
       TotalPupils,
       SeniorLeadership,
       Headteacher,
       DeputyHeadTeacher,
       AssistantHeadTeacher,
       LeadershipNonTeacher
FROM VW_SchoolSeniorLeadershipDefaultPercentWorkforce
WHERE RunId = (SELECT Value FROM Parameters WHERE Name = 'CurrentYear')
    GO