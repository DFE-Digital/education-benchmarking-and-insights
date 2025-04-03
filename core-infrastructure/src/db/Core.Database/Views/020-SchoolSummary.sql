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
           f.PeriodCoveredByReturn
    FROM School s
             LEFT JOIN CurrentDefaultFinancial f on f.URN = s.URN
GO