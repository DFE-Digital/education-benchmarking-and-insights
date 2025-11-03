DROP VIEW IF EXISTS VW_SchoolsWorkforceSummaryActual
    GO

CREATE VIEW VW_SchoolsWorkforceSummaryActual
AS
SELECT n.RunType,
       n.RunId,
       n.URN,
       n.TotalPupils,
       n.FinanceType,
       n.OverallPhase,
       IIF(n.TeachersFTE > 0.0, (n.TotalPupils / n.TeachersFTE), NULL)      AS PupilTeacherRatio,
       n.EHCPlan,
       n.SENSupport,
       s.SchoolName,
       s.LACode,
       s.TrustCompanyNumber,
       s.NurseryProvision,
       s.SpecialClassProvision,
       s.SixthFormProvision
FROM NonFinancial n
         LEFT JOIN School s on n.URN = s.URN
-- exclude non-lead federation schools
WHERE s.FederationLeadURN = s.URN -- federation lead
OR s.FederationLeadURN IS NULL -- not federated
    GO

DROP VIEW IF EXISTS VW_SchoolsWorkforceSummaryPercentPupil
    GO

CREATE VIEW VW_SchoolsWorkforceSummaryPercentPupil
AS
SELECT RunType,
       RunId,
       URN,
       TotalPupils,
       PupilTeacherRatio,
       IIF(TotalPupils > 0.0, (EHCPlan / TotalPupils) * 100, NULL)      AS EHCPlan,
       IIF(TotalPupils > 0.0, (SENSupport / TotalPupils) * 100, NULL)   AS SENSupport,
       FinanceType,
       OverallPhase,
       SchoolName,
       LACode,
       TrustCompanyNumber,
       NurseryProvision,
       SpecialClassProvision,
       SixthFormProvision
FROM VW_SchoolsWorkforceSummaryActual
    GO

DROP VIEW IF EXISTS VW_SchoolsWorkforceSummaryDefaultActual
    GO

CREATE VIEW VW_SchoolsWorkforceSummaryDefaultActual AS
SELECT RunId,
       URN,
       TotalPupils,
       PupilTeacherRatio,
       EHCPlan,
       SENSupport,
       FinanceType,
       OverallPhase,
       SchoolName,
       LACode,
       TrustCompanyNumber,
       NurseryProvision,
       SpecialClassProvision,
       SixthFormProvision
FROM VW_SchoolsWorkforceSummaryActual
WHERE RunType = 'default'
    GO

DROP VIEW IF EXISTS VW_SchoolsWorkforceSummaryDefaultPercentPupil
    GO

CREATE VIEW VW_SchoolsWorkforceSummaryDefaultPercentPupil AS
SELECT RunId,
       URN,
       TotalPupils,
       PupilTeacherRatio,
       EHCPlan,
       SENSupport,
       FinanceType,
       OverallPhase,
       SchoolName,
       LACode,
       TrustCompanyNumber,
       NurseryProvision,
       SpecialClassProvision,
       SixthFormProvision
FROM VW_SchoolsWorkforceSummaryPercentPupil
WHERE RunType = 'default'
    GO

DROP VIEW IF EXISTS VW_SchoolsWorkforceSummaryDefaultCurrentActual
    GO

CREATE VIEW VW_SchoolsWorkforceSummaryDefaultCurrentActual AS
SELECT URN,
       TotalPupils,
       PupilTeacherRatio,
       EHCPlan,
       SENSupport,
       FinanceType,
       OverallPhase,
       SchoolName,
       LACode,
       TrustCompanyNumber,
       NurseryProvision,
       SpecialClassProvision,
       SixthFormProvision
FROM VW_SchoolsWorkforceSummaryDefaultActual
WHERE RunId = (SELECT Value FROM Parameters WHERE Name = 'CurrentYear')
    GO

DROP VIEW IF EXISTS VW_SchoolsWorkforceSummaryDefaultCurrentPercentPupil
    GO

CREATE VIEW VW_SchoolsWorkforceSummaryDefaultCurrentPercentPupil AS
SELECT URN,
       TotalPupils,
       PupilTeacherRatio,
       EHCPlan,
       SENSupport,
       FinanceType,
       OverallPhase,
       SchoolName,
       LACode,
       TrustCompanyNumber,
       NurseryProvision,
       SpecialClassProvision,
       SixthFormProvision
FROM VW_SchoolsWorkforceSummaryDefaultPercentPupil
WHERE RunId = (SELECT Value FROM Parameters WHERE Name = 'CurrentYear')
    GO