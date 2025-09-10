IF EXISTS (
    SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS
    WHERE TABLE_NAME = 'TrustFinancial' AND COLUMN_NAME = 'DirectRevenueFinancingCosts'
)
BEGIN
ALTER TABLE TrustFinancial
DROP COLUMN DirectRevenueFinancingCosts;
END;

IF EXISTS (
    SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS
    WHERE TABLE_NAME = 'TrustFinancial' AND COLUMN_NAME = 'DirectRevenueFinancingCostsCS'
)
BEGIN
    ALTER TABLE TrustFinancial
    DROP COLUMN DirectRevenueFinancingCostsCS;
END;
