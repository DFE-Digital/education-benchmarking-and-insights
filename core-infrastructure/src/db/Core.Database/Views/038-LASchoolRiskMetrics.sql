DROP VIEW IF EXISTS VW_LASchoolRiskMetricsDefault
    GO

CREATE VIEW VW_LASchoolRiskMetricsDefault AS
SELECT RunId
     , URN
     , RiskGroup
     , RiskIndicator
     , RiskIndicatorValue
     , RiskIndicatorValueFormatting
     , RiskIndicatorFlag
     , RiskIndicatorContribution
     , RiskIndicatorContributionMax
FROM LASchoolRiskIndicators
    GO

DROP VIEW IF EXISTS VW_LASchoolRiskMetricsDefaultCurrent
    GO

CREATE VIEW VW_LASchoolRiskMetricsDefaultCurrent AS
SELECT URN
     , RiskGroup
     , RiskIndicator
     , RiskIndicatorValue
     , RiskIndicatorValueFormatting
     , RiskIndicatorFlag
     , RiskIndicatorContribution
     , RiskIndicatorContributionMax
FROM VW_LASchoolRiskMetricsDefault
WHERE RunId = (SELECT Value FROM Parameters WHERE Name = 'CurrentYear')
    GO
