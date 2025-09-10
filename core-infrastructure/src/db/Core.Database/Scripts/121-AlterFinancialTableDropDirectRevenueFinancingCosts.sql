IF EXISTS (
    SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS
    WHERE TABLE_NAME = 'Financial' AND COLUMN_NAME = 'DirectRevenueFinancingCosts'
)
BEGIN
    ALTER TABLE Financial
    DROP COLUMN DirectRevenueFinancingCosts;
END;

IF EXISTS (
    SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS
    WHERE TABLE_NAME = 'Financial' AND COLUMN_NAME = 'DirectRevenueFinancingCostsCS'
)
BEGIN
    ALTER TABLE Financial
    DROP COLUMN DirectRevenueFinancingCostsCS;
END;
