/*
 * July 2025 pass on Commercial Resources due to move from `find-dfe-approved-framework.service.gov.uk` 
 * to `get-help-buying-for-schools.education.gov.uk` as per work item 266678.
 */

BEGIN TRANSACTION

TRUNCATE TABLE [CommercialResources]

INSERT INTO [data].[dbo].[CommercialResources]
    ([Title], [Url], [Category], [SubCategory])
VALUES
    ('Air cleaning units for education and childcare settings', 'https://get-help-buying-for-schools.education.gov.uk/solutions/air-cleaning', '["Premises staff and services"]', '["Maintenance of premises"]'),
    ('Audit and financial services', 'https://get-help-buying-for-schools.education.gov.uk/solutions/audit-and-financial-services', '["Non-educational support staff and services"]', '["Auditor costs"]'),
    ('Building cleaning', 'https://get-help-buying-for-schools.education.gov.uk/solutions/building-cleaning', '["Premises staff and services"]', '["Cleaning and caretaking"]'),
    ('Building in use - support services', 'https://get-help-buying-for-schools.education.gov.uk/solutions/fm-support-service', '["Premises staff and services"]', '["Maintenance of premises"]'),
    ('Catering services', 'https://get-help-buying-for-schools.education.gov.uk/solutions/outsourced-catering', '["Catering staff and supplies"]', '["Catering supplies"]'),
    ('Cleaning services', 'https://get-help-buying-for-schools.education.gov.uk/solutions/cleaning-services', '["Premises staff and services"]', '["Cleaning and caretaking"]'),
    ('Commercial catering equipment', 'https://get-help-buying-for-schools.education.gov.uk/solutions/commercial-catering-equipment', '["Catering staff and supplies"]', '["Catering supplies"]'),
    ('Communications solutions', 'https://get-help-buying-for-schools.education.gov.uk/solutions/communications-solutions', '["Administrative supplies"]', '["Administrative supplies (non-educational)"]'),
    ('Communications solutions and associated telephony services', 'https://get-help-buying-for-schools.education.gov.uk/solutions/communications-telephony', '["Administrative supplies"]', '["Administrative supplies (non-educational)"]'),
    ('Corporate software and related products and services', 'https://get-help-buying-for-schools.education.gov.uk/solutions/corporate-software', '["Administrative supplies"]', '["Administrative supplies (non-educational)"]'),
    ('Cyber security services 3', 'https://get-help-buying-for-schools.education.gov.uk/solutions/cyber-security', '["Administrative supplies"]', '["Administrative supplies (non-educational)"]'),
    ('Debt resolution services', 'https://get-help-buying-for-schools.education.gov.uk/solutions/debt-resolution-services', '["Utilities"]', '["Energy"]'),
    ('Education decarbonisation', 'https://get-help-buying-for-schools.education.gov.uk/solutions/education-decarbonisation', '["Utilities"]', '["Energy"]'),
    ('Education management systems', 'https://get-help-buying-for-schools.education.gov.uk/solutions/education-management-systems', '["Administrative supplies"]', '["Administrative supplies (non-educational)"]'),
    ('Electricity (for supply during 2024-2028)', 'https://get-help-buying-for-schools.education.gov.uk/solutions/electricity-espo', '["Utilities"]', '["Energy"]'),
    ('Energy cost recovery services for schools', 'https://get-help-buying-for-schools.education.gov.uk/solutions/energy-cost-recovery-services', '["Utilities"]', '["Energy"]'),
    ('Everything ICT', 'https://get-help-buying-for-schools.education.gov.uk/solutions/ict-procurement', '["Administrative supplies", "Educational ICT"]', '["Administrative supplies (non-educational)", "ICT learning resources"]'),
    ('Facilities management and workplace services', 'https://get-help-buying-for-schools.education.gov.uk/solutions/fm-workplace-dps', '["Catering staff and supplies", "Premises staff and services"]', '["Catering supplies", "Maintenance of premises"]'),
    ('Fixed and flexible electricity', 'https://get-help-buying-for-schools.education.gov.uk/solutions/fixed-and-flexible-electricity', '["Utilities"]', '["Energy"]'),
    ('Fixed and flexible gas', 'https://get-help-buying-for-schools.education.gov.uk/solutions/fixed-and-flexible-gas', '["Utilities"]', '["Energy"]'),
    ('Flexible electricity', 'https://get-help-buying-for-schools.education.gov.uk/solutions/flex-electricity', '["Utilities"]', '["Energy"]'),
    ('Flexible gas', 'https://get-help-buying-for-schools.education.gov.uk/solutions/flex-gas', '["Utilities"]', '["Energy"]'),
    ('Food and drink - dynamic purchasing system', 'https://get-help-buying-for-schools.education.gov.uk/solutions/food-and-drink-dynamic-purchasing-system-dps', '["Catering staff and supplies"]', '["Catering supplies"]'),
    ('Food and drink - framework', 'https://get-help-buying-for-schools.education.gov.uk/solutions/food-and-drink-framework', '["Catering staff and supplies"]', '["Catering supplies"]'),
    ('G-Cloud 14', 'https://get-help-buying-for-schools.education.gov.uk/solutions/g-cloud', '["Administrative supplies", "Educational ICT"]', '["Administrative supplies (non-educational)", "ICT learning resources"]'),
    ('Grocery, fresh, chilled and frozen foods', 'https://get-help-buying-for-schools.education.gov.uk/solutions/grocery-fresh-chilled-and-frozen-foods', '["Catering staff and supplies"]', '["Catering supplies"]'),
    ('HR, payroll and employee screening services', 'https://get-help-buying-for-schools.education.gov.uk/solutions/hr-payroll-and-employee-screening', '["Non-educational support staff and services"]', '["Professional services (non-curriculum)"]'),
    ('ICT networking and storage solutions', 'https://get-help-buying-for-schools.education.gov.uk/solutions/ict-storage-network', '["Administrative supplies"]', '["Administrative supplies (non-educational)"]'),
    ('Internal fit-out and maintenance', 'https://get-help-buying-for-schools.education.gov.uk/solutions/internal-maintenance-ypo', '["Premises staff and services"]', '["Maintenance of premises"]'),
    ('IT Hardware', 'https://get-help-buying-for-schools.education.gov.uk/solutions/it-hardware', '["Administrative supplies", "Educational ICT"]', '["Administrative supplies (non-educational)", "ICT learning resources"]'),
    ('LED Lighting', 'https://get-help-buying-for-schools.education.gov.uk/solutions/led-lights', '["Premises staff and services"]', '["Maintenance of premises"]'),
    ('Liquid fuels', 'https://get-help-buying-for-schools.education.gov.uk/solutions/liquid-fuel-nepo', '["Utilities"]', '["Energy"]'),
    ('Liquid fuels (ESPO)', 'https://get-help-buying-for-schools.education.gov.uk/solutions/liquid-fuel-espo', '["Utilities"]', '["Energy"]'),
    ('Liquified petroleum gas and other liquified fuels', 'https://get-help-buying-for-schools.education.gov.uk/solutions/liquid-fuel-other', '["Utilities"]', '["Energy"]'),
    ('Mains gas 2023', 'https://get-help-buying-for-schools.education.gov.uk/solutions/mains-gas-espo', '["Utilities"]', '["Energy"]'),
    ('Multifunctional devices and digital transformation solutions', 'https://get-help-buying-for-schools.education.gov.uk/solutions/mfd-digi-transform', '["Administrative supplies", "Educational ICT"]', '["Administrative supplies (non-educational)", "ICT learning resources"]'),
    ('Multifunctional devices, print and digital workflow software services and managed print service provision', 'https://get-help-buying-for-schools.education.gov.uk/solutions/printing-services', '["Administrative supplies", "Educational ICT"]', '["Administrative supplies (non-educational)", "ICT learning resources"]'),
    ('Musical instruments, equipment and technology', 'https://get-help-buying-for-schools.education.gov.uk/solutions/musical-instruments-equipment-and-technology', '["Educational supplies"]', '["Learning resources (not ICT equipment)"]'),
    ('National professional qualification', 'https://get-help-buying-for-schools.education.gov.uk/solutions/npq', '["Other costs"]', '["Staff development and training"]'),
    ('Outsourced catering services', 'https://get-help-buying-for-schools.education.gov.uk/solutions/outsourced-catering-cpc', '["Catering staff and supplies"]', '["Catering supplies"]'),
    ('Plan technology for your school', 'https://www.gov.uk/guidance/plan-technology-for-your-school', '["Educational ICT"]', '["ICT learning resources"]'),
    ('Print books', 'https://get-help-buying-for-schools.education.gov.uk/solutions/print-books', '["Educational supplies"]', '["Learning resources (not ICT equipment)"]'),
    ('Print Marketplace 2', 'https://get-help-buying-for-schools.education.gov.uk/solutions/print-marketplace-2', '["Administrative supplies", "Educational ICT"]', '["Administrative supplies (non-educational)", "ICT learning resources"]'),
    ('Risk protection arrangement - alternative to commercial insurance', 'https://get-help-buying-for-schools.education.gov.uk/solutions/rpa', '["Other costs"]', '["Other insurance premiums "]'),
    ('Sandwiches and food to go', 'https://get-help-buying-for-schools.education.gov.uk/solutions/sandwiches-and-food-to-go', '["Catering staff and supplies"]', '["Catering supplies"]'),
    ('Software licenses and associated services for academies and schools', 'https://get-help-buying-for-schools.education.gov.uk/solutions/software-licenses', '["Administrative supplies", "Educational ICT"]', '["Administrative supplies (non-educational)", "ICT learning resources"]'),
    ('Specialist professional services', 'https://get-help-buying-for-schools.education.gov.uk/solutions/specialist-professional-services', '["Non-educational support staff and services"]', '["Professional services (non-curriculum)"]'),
    ('Sports facilities installation, refurbishment and maintenance', 'https://get-help-buying-for-schools.education.gov.uk/solutions/sports-facilities-installation-refurbishment-and-maintenance', '["Other costs"]', '["Special facilities "]'),
    ('Stationery, paper and education supplies', 'https://get-help-buying-for-schools.education.gov.uk/solutions/office-and-education-supplies', '["Administrative supplies"]', '["Administrative supplies (non-educational)"]'),
    ('Supply of energy 2', 'https://get-help-buying-for-schools.education.gov.uk/solutions/energy-ancillary', '["Utilities"]', '["Energy"]'),
    ('Supply teachers and temporary staffing', 'https://get-help-buying-for-schools.education.gov.uk/solutions/supply-teachers', '["Teaching and Teaching support staff"]', '["Supply Teaching Staff"]'),
    ('Technology products & associated services 2', 'https://get-help-buying-for-schools.education.gov.uk/solutions/technology-products-and-associated-services-2', '["Administrative supplies", "Educational ICT"]', '["Administrative supplies (non-educational)", "ICT learning resources"]'),
    ('Temporary and permanent staffing', 'https://get-help-buying-for-schools.education.gov.uk/solutions/temporary-and-permanent-staffing', '["Teaching and Teaching support staff"]', '["Supply Teaching Staff"]'),
    ('Total cleaning service solutions', 'https://get-help-buying-for-schools.education.gov.uk/solutions/total-cleaning-service-solutions', '["Premises staff and services"]', '["Cleaning and caretaking"]'),
    ('Utilities supplies and services', 'https://get-help-buying-for-schools.education.gov.uk/solutions/utilities-supply-services', '["Utilities"]', '["Energy", "Water and Sewerage"]')

COMMIT TRANSACTION