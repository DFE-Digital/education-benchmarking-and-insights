--This script ensures company numbers are 8 digits

--Updates schools
UPDATE School
SET TrustCompanyNumber = REPLICATE('0', 8 - LEN(TrustCompanyNumber)) + TrustCompanyNumber
WHERE TrustCompanyNumber IS NOT NULL
  AND LEN(TrustCompanyNumber) < 8

--Removes duplicates trusts
DELETE FROM Trust
WHERE CompanyNumber IN (
    SELECT REPLICATE('0', 8 - LEN(CompanyNumber)) + CompanyNumber FROM Trust
    WHERE LEN(CompanyNumber) < 8)

--Update trusts
UPDATE Trust
SET CompanyNumber =  REPLICATE('0', 8 - LEN(CompanyNumber)) + CompanyNumber
WHERE CompanyNumber IS NOT NULL
  AND LEN(CompanyNumber) < 8