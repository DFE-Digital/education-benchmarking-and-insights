DROP VIEW IF EXISTS VW_SchoolSummary
GO

CREATE VIEW VW_SchoolSummary
AS
    SELECT s.URN,
           s.SchoolName,
           s.AddressStreet,
           s.AddressLocality,
           s.AddressLine3,
           s.AddressTown,
           s.AddressCounty,
           s.AddressPostcode,
           s.OverallPhase,
           f.PeriodCoveredByReturn,
           CONVERT(float, f.TotalPupils) AS TotalPupils
    FROM School s
             LEFT JOIN CurrentDefaultFinancial f on f.URN = s.URN
GO