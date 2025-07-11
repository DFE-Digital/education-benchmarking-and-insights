Feature: School find ways to spend less

    Scenario: View all resources
        Given I am on 'recommended' resources page for school with URN '777042'
        And the following priority categories are shown on the page
          | Name                                       | Priority        |
          | Teaching and Teaching support staff        | High priority   |
          | Non-educational support staff and services | High priority   |
          | Administrative supplies                    | High priority   |
          | Educational supplies                       | High priority   |
          | Catering staff and supplies                | High priority   |
          | Premises staff and services                | High priority   |
          | Utilities                                  | Medium priority |
        When I click on all resources
        Then all resources tab is displayed

    Scenario: Show all should expand all sections and show correct categories
        Given I am on 'all' resources page for school with URN '777042'
        When I click on show all sections
        Then all sections on the page are expanded
        And the show all text changes to hide all sections
        And all resource sub categories are displayed on the page
          | Name                                       |
          | Teaching and Teaching support staff        |
          | Non-educational support staff and services |
          | Auditor costs                              |
          | Professional services (non-curriculum)     |
          | Educational supplies                       |
          | Learning resources (not ICT equipment)     |
          | Educational ICT                            |
          | ICT learning resources                     |
          | Premises staff and services                |
          | Cleaning and caretaking                    |
          | Maintenance of premises                    |
          | Utilities                                  |
          | Energy                                     |
          | Administrative supplies                    |
          | Administrative supplies (non-educational)  |
          | Catering staff and supplies                |
          | Catering supplies                          |
          | Other costs                                |
          | Special facilities                         |
          | Staff development and training             |

    Scenario: Show all should expand all sections and show correct links
        Given I am on 'all' resources page for school with URN '777042'
        When I click on show all sections
        Then all sections on the page are expanded
        And all sub categories have correct links
          | Text                                                                                                      | Href                                                                                                                        | Target |
          | Audit and financial services                                                                              | https://get-help-buying-for-schools.education.gov.uk/solutions/audit-and-financial-services                                 | _blank |
          | HR, payroll and employee screening services                                                               | https://get-help-buying-for-schools.education.gov.uk/solutions/hr-payroll-and-employee-screening                            | _blank |
          | Specialist professional services                                                                          | https://get-help-buying-for-schools.education.gov.uk/solutions/specialist-professional-services                             | _blank |
          | Musical instruments, equipment and technology                                                             | https://get-help-buying-for-schools.education.gov.uk/solutions/musical-instruments-equipment-and-technology                 | _blank |
          | Print books                                                                                               | https://get-help-buying-for-schools.education.gov.uk/solutions/print-books                                                  | _blank |
          | Everything ICT                                                                                            | https://get-help-buying-for-schools.education.gov.uk/solutions/ict-procurement                                              | _blank |
          | G-Cloud 14                                                                                                | https://get-help-buying-for-schools.education.gov.uk/solutions/g-cloud                                                      | _blank |
          | IT Hardware                                                                                               | https://get-help-buying-for-schools.education.gov.uk/solutions/it-hardware                                                  | _blank |
          | Multifunctional devices and digital transformation solutions                                              | https://get-help-buying-for-schools.education.gov.uk/solutions/mfd-digi-transform                                           | _blank |
          | Multifunctional devices, print and digital workflow software services and managed print service provision | https://get-help-buying-for-schools.education.gov.uk/solutions/printing-services                                            | _blank |
          | Plan technology for your school                                                                           | https://www.gov.uk/guidance/plan-technology-for-your-school                                                                 | _blank |
          | Print Marketplace 2                                                                                       | https://get-help-buying-for-schools.education.gov.uk/solutions/print-marketplace-2                                          | _blank |
          | Software licenses and associated services for academies and schools                                       | https://get-help-buying-for-schools.education.gov.uk/solutions/software-licenses                                            | _blank |
          | Technology products & associated services 2                                                               | https://get-help-buying-for-schools.education.gov.uk/solutions/technology-products-and-associated-services-2                | _blank |
          | Building cleaning                                                                                         | https://get-help-buying-for-schools.education.gov.uk/solutions/building-cleaning                                            | _blank |
          | Cleaning services                                                                                         | https://get-help-buying-for-schools.education.gov.uk/solutions/cleaning-services                                            | _blank |
          | Total cleaning service solutions                                                                          | https://get-help-buying-for-schools.education.gov.uk/solutions/total-cleaning-service-solutions                             | _blank |
          | Air cleaning units for education and childcare settings                                                   | https://get-help-buying-for-schools.education.gov.uk/solutions/air-cleaning                                                 | _blank |
          | Building in use - support services                                                                        | https://get-help-buying-for-schools.education.gov.uk/solutions/fm-support-service                                           | _blank |
          | Facilities management and workplace services                                                              | https://get-help-buying-for-schools.education.gov.uk/solutions/fm-workplace-dps                                             | _blank |
          | Internal fit-out and maintenance                                                                          | https://get-help-buying-for-schools.education.gov.uk/solutions/internal-maintenance-ypo                                     | _blank |
          | LED Lighting                                                                                              | https://get-help-buying-for-schools.education.gov.uk/solutions/led-lights                                                   | _blank |
          | Debt resolution services                                                                                  | https://get-help-buying-for-schools.education.gov.uk/solutions/debt-resolution-services                                     | _blank |
          | Education decarbonisation                                                                                 | https://get-help-buying-for-schools.education.gov.uk/solutions/education-decarbonisation                                    | _blank |
          | Electricity (for supply during 2024-2028)                                                                 | https://get-help-buying-for-schools.education.gov.uk/solutions/electricity-espo                                             | _blank |
          | Energy cost recovery services for schools                                                                 | https://get-help-buying-for-schools.education.gov.uk/solutions/energy-cost-recovery-services                                | _blank |
          | Fixed and flexible electricity                                                                            | https://get-help-buying-for-schools.education.gov.uk/solutions/fixed-and-flexible-electricity                               | _blank |
          | Fixed and flexible gas                                                                                    | https://get-help-buying-for-schools.education.gov.uk/solutions/fixed-and-flexible-gas                                       | _blank |
          | Flexible electricity                                                                                      | https://get-help-buying-for-schools.education.gov.uk/solutions/flex-electricity                                             | _blank |
          | Flexible gas                                                                                              | https://get-help-buying-for-schools.education.gov.uk/solutions/flex-gas                                                     | _blank |
          | Liquid fuels                                                                                              | https://get-help-buying-for-schools.education.gov.uk/solutions/liquid-fuel-nepo                                             | _blank |
          | Liquid fuels (ESPO)                                                                                       | https://get-help-buying-for-schools.education.gov.uk/solutions/liquid-fuel-espo                                             | _blank |
          | Liquified petroleum gas and other liquified fuels                                                         | https://get-help-buying-for-schools.education.gov.uk/solutions/liquid-fuel-other                                            | _blank |
          | Mains gas 2023                                                                                            | https://get-help-buying-for-schools.education.gov.uk/solutions/mains-gas-espo                                               | _blank |
          | Supply of energy 2                                                                                        | https://get-help-buying-for-schools.education.gov.uk/solutions/energy-ancillary                                             | _blank |
          | Utilities supplies and services                                                                           | https://get-help-buying-for-schools.education.gov.uk/solutions/utilities-supply-services                                    | _blank |
          | Communications solutions                                                                                  | https://get-help-buying-for-schools.education.gov.uk/solutions/communications-solutions                                     | _blank |
          | Communications solutions and associated telephony services                                                | https://get-help-buying-for-schools.education.gov.uk/solutions/communications-telephony                                     | _blank |
          | Corporate software and related products and services                                                      | https://get-help-buying-for-schools.education.gov.uk/solutions/corporate-software                                           | _blank |
          | Cyber security services 3                                                                                 | https://get-help-buying-for-schools.education.gov.uk/solutions/cyber-security                                               | _blank |
          | Education management systems                                                                              | https://get-help-buying-for-schools.education.gov.uk/solutions/education-management-systems                                 | _blank |
          | Everything ICT                                                                                            | https://get-help-buying-for-schools.education.gov.uk/solutions/ict-procurement                                              | _blank |
          | G-Cloud 14                                                                                                | https://get-help-buying-for-schools.education.gov.uk/solutions/g-cloud                                                      | _blank |
          | ICT networking and storage solutions                                                                      | https://get-help-buying-for-schools.education.gov.uk/solutions/ict-storage-network                                          | _blank |
          | IT Hardware                                                                                               | https://get-help-buying-for-schools.education.gov.uk/solutions/it-hardware                                                  | _blank |
          | Multifunctional devices and digital transformation solutions                                              | https://get-help-buying-for-schools.education.gov.uk/solutions/mfd-digi-transform                                           | _blank |
          | Multifunctional devices, print and digital workflow software services and managed print service provision | https://get-help-buying-for-schools.education.gov.uk/solutions/printing-services                                            | _blank |
          | Print Marketplace 2                                                                                       | https://get-help-buying-for-schools.education.gov.uk/solutions/print-marketplace-2                                          | _blank |
          | Software licenses and associated services for academies and schools                                       | https://get-help-buying-for-schools.education.gov.uk/solutions/software-licenses                                            | _blank |
          | Stationery, paper and education supplies                                                                  | https://get-help-buying-for-schools.education.gov.uk/solutions/office-and-education-supplies                                | _blank |
          | Technology products & associated services 2                                                               | https://get-help-buying-for-schools.education.gov.uk/solutions/technology-products-and-associated-services-2                | _blank |
          | Catering services                                                                                         | https://get-help-buying-for-schools.education.gov.uk/solutions/outsourced-catering                                          | _blank |
          | Commercial catering equipment                                                                             | https://get-help-buying-for-schools.education.gov.uk/solutions/commercial-catering-equipment                                | _blank |
          | Facilities management and workplace services                                                              | https://get-help-buying-for-schools.education.gov.uk/solutions/fm-workplace-dps                                             | _blank |
          | Food and drink - dynamic purchasing system                                                                | https://get-help-buying-for-schools.education.gov.uk/solutions/food-and-drink-dynamic-purchasing-system-dps                 | _blank |
          | Food and drink - framework                                                                                | https://get-help-buying-for-schools.education.gov.uk/solutions/food-and-drink-framework                                     | _blank |
          | Grocery, fresh, chilled and frozen foods                                                                  | https://get-help-buying-for-schools.education.gov.uk/solutions/grocery-fresh-chilled-and-frozen-foods                       | _blank |
          | Outsourced catering services                                                                              | https://get-help-buying-for-schools.education.gov.uk/solutions/outsourced-catering-cpc                                      | _blank |
          | Sandwiches and food to go                                                                                 | https://get-help-buying-for-schools.education.gov.uk/solutions/sandwiches-and-food-to-go                                    | _blank |
          | Sports facilities installation, refurbishment and maintenance                                             | https://get-help-buying-for-schools.education.gov.uk/solutions/sports-facilities-installation-refurbishment-and-maintenance | _blank |
          | National professional qualification                                                                       | https://get-help-buying-for-schools.education.gov.uk/solutions/npq                                                          | _blank |