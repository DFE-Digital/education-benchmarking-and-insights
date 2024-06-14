IF EXISTS(SELECT *
          FROM INFORMATION_SCHEMA.TABLES
          WHERE table_name = 'Financial')
    BEGIN
       ALTER TABLE Financial ADD TargetedGrants decimal(16,2) NULL;
       ALTER TABLE Financial DROP COLUMN BreakdownEducationalSuppliesCosts;
       ALTER TABLE Financial DROP COLUMN BreakdownEducationalSuppliesCostsCS;
       ALTER TABLE Financial DROP COLUMN CommunityFocusedSchoolCostsCS;
       ALTER TABLE Financial DROP COLUMN CommunityFocusedSchoolStaffCS;
    END