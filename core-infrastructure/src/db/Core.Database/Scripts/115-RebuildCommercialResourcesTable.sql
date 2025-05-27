IF EXISTS(
   SELECT * 
        FROM INFORMATION_SCHEMA.COLUMNS
            WHERE table_name = 'CommercialResources'
            AND column_name = 'Id'
   )
    BEGIN
        DROP TABLE [dbo].[CommercialResources]
    END;

GO

IF NOT EXISTS(
    SELECT *
        FROM INFORMATION_SCHEMA.TABLES
        WHERE table_name = 'CommercialResources')
    BEGIN
        CREATE TABLE [dbo].[CommercialResources]
        (
            Title       nvarchar(255)  NOT NULL,
            Url         nvarchar(2000)  NOT NULL,
            Category    nvarchar(max)   NOT NULL,
            SubCategory nvarchar(max)   NOT NULL,
            ValidFrom   datetimeoffset NOT NULL DEFAULT GETUTCDATE(),
            ValidTo     datetimeoffset NULL
        
            CONSTRAINT PK_CommercialResources PRIMARY KEY (Title),
        );
        INSERT INTO [dbo].[CommercialResources] (Category, SubCategory, Title, Url)
            VALUES
                (
                    '["Teaching and Teaching support staff"]',
                    '["Supply teaching staff"]',
                    'Supply teachers and temporary staffing (STaTS)',
                    'https://find-dfe-approved-framework.service.gov.uk/list/supply-teachers'
                ),
                (
                    '["Teaching and Teaching support staff"]',
                    '["Supply teaching staff"]',
                    'Temporary and permanent staffing',
                    'https://find-dfe-approved-framework.service.gov.uk/list/temporary-and-permanent-staffing'
                ),
                (
                    '["Non-educational support staff and services"]',
                    '["Auditor costs"]',
                    'Audit and financial services',
                    'https://find-dfe-approved-framework.service.gov.uk/list/audit-and-financial-services'
                ),
                (
                    '["Non-educational support staff and services"]',
                    '["Professional services (non-curriculum)"]',
                    'Specialist professional services',
                    'https://find-dfe-approved-framework.service.gov.uk/list/specialist-professional-services'
                ),
                (
                    '["Non-educational support staff and services"]',
                    '["Professional services (non-curriculum)"]',
                    'Legal services',
                    'https://find-dfe-approved-framework.service.gov.uk/list/legal'
                ),
                (
                    '["Non-educational support staff and services"]',
                    '["Professional services (non-curriculum)"]',
                    'HR, payroll and employee screening services',
                    'https://find-dfe-approved-framework.service.gov.uk/list/hr-payroll-and-employee-screening'
                ),
                (
                    '["Educational supplies"]',
                    '["Learning resources (not ICT equipment)"]',
                    'Print books framework',
                    'https://find-dfe-approved-framework.service.gov.uk/list/print-books'
                ),
                (
                    '["Educational supplies"]',
                    '["Learning resources (not ICT equipment)"]',
                    'Musical instruments, equipment and technology framework',
                    'https://find-dfe-approved-framework.service.gov.uk/list/musical-equipment'
                ),
                (
                    '["Educational ICT", "Administrative supplies"]',
                    '["ICT learning resources", "Administrative supplies (non-educational)"]',
                    'G-Cloud 14',
                    'https://find-dfe-approved-framework.service.gov.uk/list/g-cloud'
                ),
                (
                    '["Educational ICT", "Administrative supplies"]',
                    '["ICT learning resources", "Administrative supplies (non-educational)"]',
                    'Everything ICT',
                    'https://find-dfe-approved-framework.service.gov.uk/list/ict-procurement'
                ),
                (
                    '["Educational ICT", "Administrative supplies"]',
                    '["ICT learning resources", "Administrative supplies (non-educational)"]',
                    'IT hardware',
                    'https://find-dfe-approved-framework.service.gov.uk/list/it-hardware'
                ),
                (
                    '["Educational ICT", "Administrative supplies"]',
                    '["ICT learning resources", "Administrative supplies (non-educational)"]',
                    'Multi-functional devices and digital solutions',
                    'https://find-dfe-approved-framework.service.gov.uk/list/mfd-digital-solutions'
                ),
                (
                    '["Educational ICT", "Administrative supplies"]',
                    '["ICT learning resources", "Administrative supplies (non-educational)"]',
                    'Multifunctional devices, print and digital workflow software services and managed print service provision',
                    'https://find-dfe-approved-framework.service.gov.uk/list/printing-services'
                ),
                (
                    '["Educational ICT", "Administrative supplies"]',
                    '["ICT learning resources", "Administrative supplies (non-educational)"]',
                    'Outsourced ICT',
                    'https://find-dfe-approved-framework.service.gov.uk/list/outsourced-ict'
                ),
                (
                    '["Educational ICT", "Administrative supplies"]',
                    '["ICT learning resources", "Administrative supplies (non-educational)"]',
                    'Print marketplace',
                    'https://find-dfe-approved-framework.service.gov.uk/list/print-marketplace'
                ),
                (
                    '["Educational ICT", "Administrative supplies"]',
                    '["ICT learning resources", "Administrative supplies (non-educational)"]',
                    'Software licences and associated services for academies and schools',
                    'https://find-dfe-approved-framework.service.gov.uk/list/software-licenses'
                ),
                (
                    '["Educational ICT", "Administrative supplies"]',
                    '["ICT learning resources", "Administrative supplies (non-educational)"]',
                    'Technology products & associated services 2',
                    'https://find-dfe-approved-framework.service.gov.uk/list/technology-products-and-associated-services'
                ),
                (
                    '["Educational ICT"]',
                    '["ICT learning resources"]',
                    'Plan technology for your school',
                    'https://www.gov.uk/guidance/plan-technology-for-your-school'
                ),
                (
                    '["Premises staff and services"]',
                    '["Cleaning and caretaking"]',
                    'Building cleaning',
                    'https://find-dfe-approved-framework.service.gov.uk/list/building-cleaning'
                ),
                (
                    '["Premises staff and services"]',
                    '["Cleaning and caretaking"]',
                    'Cleaning services',
                    'https://find-dfe-approved-framework.service.gov.uk/list/cleaning-services'
                ),
                (
                    '["Premises staff and services", "Catering staff and supplies"]',
                    '["Maintenance of premises", "Catering supplies"]',
                    'Facilities management and workplace services',
                    'https://find-dfe-approved-framework.service.gov.uk/list/fm-workplace-dps'
                ),
                (
                    '["Premises staff and services"]',
                    '["Maintenance of premises"]',
                    'Internal fit-out and maintenance',
                    'https://find-dfe-approved-framework.service.gov.uk/list/internal-maintenance-ypo'
                ),
                (
                    '["Premises staff and services"]',
                    '["Maintenance of premises"]',
                    'LED lighting',
                    'https://find-dfe-approved-framework.service.gov.uk/list/led-lights'
                ),
                (
                    '["Premises staff and services"]',
                    '["Maintenance of premises"]',
                    'Air cleaning',
                    'https://find-dfe-approved-framework.service.gov.uk/list/air-cleaning'
                ),
                (
                    '["Premises staff and services"]',
                    '["Maintenance of premises"]',
                    'Building in use - support services',
                    'https://find-dfe-approved-framework.service.gov.uk/list/fm-support-service'
                ),
                (
                    '["Utilities"]',
                    '["Energy"]',
                    'Debt resolution services',
                    'https://find-dfe-approved-framework.service.gov.uk/list/debt-resolution-services'
                ),
                (
                    '["Utilities"]',
                    '["Energy"]',
                    'Electricity (for supply during 2020 - 2028)',
                    'https://find-dfe-approved-framework.service.gov.uk/list/electricity-espo'
                ),
                (
                    '["Utilities"]',
                    '["Energy"]',
                    'Energy cost recovery services',
                    'https://find-dfe-approved-framework.service.gov.uk/list/energy-cost-recovery-services'
                ),
                (
                    '["Utilities"]',
                    '["Energy"]',
                    'Flexible electricity',
                    'https://find-dfe-approved-framework.service.gov.uk/list/flex-electricity'
                ),
                (
                    '["Utilities"]',
                    '["Energy"]',
                    'Flexible gas',
                    'https://find-dfe-approved-framework.service.gov.uk/list/flex-gas'
                ),
                (
                    '["Utilities"]',
                    '["Energy"]',
                    'Liquid fuels - ESPO framework',
                    'https://find-dfe-approved-framework.service.gov.uk/list/liquid-fuel-espo'
                ),
                (
                    '["Utilities"]',
                    '["Energy"]',
                    'Liquid fuels - NEPO framework',
                    'https://find-dfe-approved-framework.service.gov.uk/list/liquid-fuel-nepo'
                ),
                (
                    '["Utilities"]',
                    '["Energy"]',
                    'Liquified petroleum gas and other liquified fuels',
                    'https://find-dfe-approved-framework.service.gov.uk/list/liquid-fuel-other'
                ),
                (
                    '["Utilities"]',
                    '["Energy"]',
                    'Mains gas 2023',
                    'https://find-dfe-approved-framework.service.gov.uk/list/mains-gas-espo'
                ),
                (
                    '["Utilities"]',
                    '["Energy"]',
                    'Supply of energy 2',
                    'https://find-dfe-approved-framework.service.gov.uk/list/energy-ancillary'
                ),
                (
                    '["Utilities"]',
                    '["Energy"]',
                    'Education decarbonisation',
                    'https://find-dfe-approved-framework.service.gov.uk/list/education-decarbonisation'
                ),
                (
                    '["Utilities"]',
                    '["Energy"]',
                    'Fixed and flexible electricity',
                    'https://find-dfe-approved-framework.service.gov.uk/list/fixed-and-flexible-electricity'
                ),
                (
                    '["Utilities"]',
                    '["Energy"]',
                    'Fixed and flexible gas',
                    'https://find-dfe-approved-framework.service.gov.uk/list/fixed-and-flexible-gas'
                ),
                (
                    '["Utilities"]',
                    '["Energy"]',
                    'Utilities supplies and services',
                    'https://find-dfe-approved-framework.service.gov.uk/list/utilities-supplies-and-services'
                ),
                (
                    '["Administrative supplies"]',
                    '["Administrative supplies (non-educational)"]',
                    'Communications solutions',
                    'https://find-dfe-approved-framework.service.gov.uk/list/communications-solutions'
                ),
                (
                    '["Administrative supplies"]',
                    '["Administrative supplies (non-educational)"]',
                    'Communications solutions and associated telephony services',
                    'https://find-dfe-approved-framework.service.gov.uk/list/communications-telephony'
                ),
                (
                    '["Administrative supplies"]',
                    '["Administrative supplies (non-educational)"]',
                    'Corporate software and related products and services',
                    'https://find-dfe-approved-framework.service.gov.uk/list/corporate-software'
                ),
                (
                    '["Administrative supplies"]',
                    '["Administrative supplies (non-educational)"]',
                    'Cyber security services 3',
                    'https://find-dfe-approved-framework.service.gov.uk/list/cyber-security'
                ),
                (
                    '["Administrative supplies"]',
                    '["Administrative supplies (non-educational)"]',
                    'Education management systems',
                    'https://find-dfe-approved-framework.service.gov.uk/list/education-management-systems'
                ),
                (
                    '["Administrative supplies"]',
                    '["Administrative supplies (non-educational)"]',
                    'ICT networking and storage solutions',
                    'https://find-dfe-approved-framework.service.gov.uk/list/ict-storage-network'
                ),
                (
                    '["Administrative supplies"]',
                    '["Administrative supplies (non-educational)"]',
                    'Stationery, paper and education supplies',
                    'https://find-dfe-approved-framework.service.gov.uk/list/office-and-education-supplies'
                ),
                (
                    '["Catering staff and supplies"]',
                    '["Catering supplies"]',
                    'Catering services - ESPO framework',
                    'https://find-dfe-approved-framework.service.gov.uk/list/outsourced-catering'
                ),
                (
                    '["Catering staff and supplies"]',
                    '["Catering supplies"]',
                    'Catering services - the EB framework',
                    'https://find-dfe-approved-framework.service.gov.uk/list/catering-services'
                ),
                (
                    '["Catering staff and supplies"]',
                    '["Catering supplies"]',
                    'Commercial catering equipment',
                    'https://find-dfe-approved-framework.service.gov.uk/list/commercial-catering-equipment'
                ),
                (
                    '["Catering staff and supplies"]',
                    '["Catering supplies"]',
                    'Grocery, fresh, chilled and frozen foods',
                    'https://find-dfe-approved-framework.service.gov.uk/list/grocery-fresh-chilled-and-frozen-foods'
                ),
                (
                    '["Catering staff and supplies"]',
                    '["Catering supplies"]',
                    'Outsourced catering services',
                    'https://find-dfe-approved-framework.service.gov.uk/list/outsourced-catering-cpc'
                ),
                (
                    '["Catering staff and supplies"]',
                    '["Catering supplies"]',
                    'Food and drink - dynamic purchasing system (DPS)',
                    'https://find-dfe-approved-framework.service.gov.uk/list/food-and-drink-dynamic-purchasing-system-dps'
                ),
                (
                    '["Catering staff and supplies"]',
                    '["Catering supplies"]',
                    'Food and drink',
                    'https://find-dfe-approved-framework.service.gov.uk/list/food-and-drink-framework'
                ),
                (
                    '["Catering staff and supplies"]',
                    '["Catering supplies"]',
                    'Sandwiches and food to go',
                    'https://find-dfe-approved-framework.service.gov.uk/list/sandwiches-and-food-to-go'
                ),
                (
                    '["Other costs"]',
                    '["Staff development and training"]',
                    'National professional qualification',
                    'https://find-dfe-approved-framework.service.gov.uk/list/npq'
                ),
                (
                    '["Other costs"]',
                    '["Special facilities"]',
                    'Sports facilities installation, refurbishment, and maintenance',
                    'https://find-dfe-approved-framework.service.gov.uk/list/sports-facilities-installation-refurbishment-and-maintenance'
                ),
                (
                    '["Other costs"]',
                    '["Other insurance premiums"]',
                    'Risk protection arrangement – alternative to commercial insurance',
                    'https://find-dfe-approved-framework.service.gov.uk/list/rpa'
                ),
                (
                    '["Other costs"]',
                    '["Supply teacher insurance"]',
                    'Staff absence protection and reimbursement',
                    'https://find-dfe-approved-framework.service.gov.uk/list/staff-absence'
                );
    END