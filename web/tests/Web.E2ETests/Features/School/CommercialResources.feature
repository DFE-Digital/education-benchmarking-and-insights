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
          | Supply teaching staff                      |
          | Non-educational support staff and services |
          | Audit costs                                |
          | Professional services (non-curriculum)     |
          | Educational supplies                       |
          | Learning resources (not ICT)               |
          | Educational ICT                            |
          | ICT learning resources                     |
          | Premises staff and services                |
          | Cleaning and caretaking                    |
          | Maintenance of premises                    |
          | Premises staff                             |
          | Utilities                                  |
          | Energy                                     |
          | Water and Sewerage                         |
          | Administrative supplies                    |
          | Administrative supplies (non-educational)  |
          | Catering staff and supplies                |
          | Catering supplies                          |
          | Other costs                                |
          | Staff development and training             |

    Scenario: Show all should expand all sections and show correct links
        Given I am on 'all' resources page for school with URN '777042'
        When I click on show all sections
        Then all sections on the page are expanded
        And all sub categories have correct links
          | Text                                                                                                      | Href                                                                                                | Target |
          | find a framework agreement for goods or services                                                          | https://www.gov.uk/guidance/find-a-dfe-approved-framework-for-your-school                           | _blank |
          | Supply teachers and temporary staffing (STaTS)                                                            | https://find-dfe-approved-framework.service.gov.uk/list/supply-teachers                             | _blank |
          | Temporary and permanent staffing                                                                          | https://find-dfe-approved-framework.service.gov.uk/list/temporary-and-permanent-staffing            | _blank |
          | Audit services                                                                                            | https://find-dfe-approved-framework.service.gov.uk/list/audit                                       | _blank |
          | Specialist professional services                                                                          | https://find-dfe-approved-framework.service.gov.uk/list/specialist-professional-services            | _blank |
          | Legal services                                                                                            | https://find-dfe-approved-framework.service.gov.uk/list/legal                                       | _blank |
          | HR, payroll and employee screening services                                                               | https://find-dfe-approved-framework.service.gov.uk/list/hr-payroll-and-employee-screening           | _blank |
          | Books for schools                                                                                         | https://find-dfe-approved-framework.service.gov.uk/list/books-for-schools                           | _blank |
          | Digital marketplace (G-Cloud 13)                                                                          | https://find-dfe-approved-framework.service.gov.uk/list/digital-marketplace                         | _blank |
          | Everything ICT                                                                                            | https://find-dfe-approved-framework.service.gov.uk/list/ict-procurement                             | _blank |
          | IT hardware                                                                                               | https://find-dfe-approved-framework.service.gov.uk/list/it-hardware                                 | _blank |
          | Multi-functional devices and digital solutions                                                            | https://find-dfe-approved-framework.service.gov.uk/list/mfd-digital-solutions                       | _blank |
          | Multifunctional devices and digital transformation solutions                                              | https://find-dfe-approved-framework.service.gov.uk/list/mfd-digi-transform                          | _blank |
          | Multifunctional devices, print and digital workflow software services and managed print service provision | https://find-dfe-approved-framework.service.gov.uk/list/printing-services                           | _blank |
          | Outsourced ICT                                                                                            | https://find-dfe-approved-framework.service.gov.uk/list/outsourced-ict                              | _blank |
          | Print marketplace                                                                                         | https://find-dfe-approved-framework.service.gov.uk/list/print-marketplace                           | _blank |
          | Software licences and associated services for academies and schools                                       | https://find-dfe-approved-framework.service.gov.uk/list/software-licenses                           | _blank |
          | Technology products & associated services 2                                                               | https://find-dfe-approved-framework.service.gov.uk/list/technology-products-and-associated-services | _blank |
          | Building cleaning                                                                                         | https://find-dfe-approved-framework.service.gov.uk/list/building-cleaning                           | _blank |
          | Cleaning services                                                                                         | https://find-dfe-approved-framework.service.gov.uk/list/cleaning-services                           | _blank |
          | Total cleaning service solutions                                                                          | https://find-dfe-approved-framework.service.gov.uk/list/total-cleaning                              | _blank |
          | Facilities management and workplace services                                                              | https://find-dfe-approved-framework.service.gov.uk/list/fm-workplace-dps                            | _blank |
          | Internal fit-out and maintenance                                                                          | https://find-dfe-approved-framework.service.gov.uk/list/internal-maintenance-ypo                    | _blank |
          | Building in use - support services                                                                        | https://find-dfe-approved-framework.service.gov.uk/list/fm-support-service                          | _blank |
          | Debt resolution services                                                                                  | https://find-dfe-approved-framework.service.gov.uk/list/debt-resolution-services                    | _blank |
          | Electricity (for supply during 2020 - 2028)                                                               | https://find-dfe-approved-framework.service.gov.uk/list/electricity-espo                            | _blank |
          | Energy cost recovery services                                                                             | https://find-dfe-approved-framework.service.gov.uk/list/energy-cost-recovery-services               | _blank |
          | Flexible electricity                                                                                      | https://find-dfe-approved-framework.service.gov.uk/list/flex-electricity                            | _blank |
          | Flexible gas                                                                                              | https://find-dfe-approved-framework.service.gov.uk/list/flex-gas                                    | _blank |
          | Liquid fuels                                                                                              | https://find-dfe-approved-framework.service.gov.uk/list/liquid-fuel-espo                            | _blank |
          | Liquid fuels                                                                                              | https://find-dfe-approved-framework.service.gov.uk/list/liquid-fuel-nepo                            | _blank |
          | Liquified petroleum gas and other liquified fuels                                                         | https://find-dfe-approved-framework.service.gov.uk/list/liquid-fuel-other                           | _blank |
          | Mains gas 2023                                                                                            | https://find-dfe-approved-framework.service.gov.uk/list/mains-gas-espo                              | _blank |
          | Supply of energy 2                                                                                        | https://find-dfe-approved-framework.service.gov.uk/list/energy-ancillary                            | _blank |
          | Water, wastewater and ancillary services                                                                  | https://find-dfe-approved-framework.service.gov.uk/list/water                                       | _blank |
          | Water, wastewater and ancillary services                                                                  | https://find-dfe-approved-framework.service.gov.uk/list/water-ancillary                             | _blank |
          | Communications solutions                                                                                  | https://find-dfe-approved-framework.service.gov.uk/list/communications-solutions                    | _blank |
          | Communications solutions and associated telephony services                                                | https://find-dfe-approved-framework.service.gov.uk/list/communications-telephony                    | _blank |
          | Corporate software and related products and services                                                      | https://find-dfe-approved-framework.service.gov.uk/list/corporate-software                          | _blank |
          | Cyber security services 3                                                                                 | https://find-dfe-approved-framework.service.gov.uk/list/cyber-security                              | _blank |
          | Digital marketplace (G-Cloud 13)                                                                          | https://find-dfe-approved-framework.service.gov.uk/list/digital-marketplace                         | _blank |
          | Education management systems                                                                              | https://find-dfe-approved-framework.service.gov.uk/list/education-management-systems                | _blank |
          | Everything ICT                                                                                            | https://find-dfe-approved-framework.service.gov.uk/list/ict-procurement                             | _blank |
          | ICT networking and storage solutions                                                                      | https://find-dfe-approved-framework.service.gov.uk/list/ict-storage-network                         | _blank |
          | IT hardware                                                                                               | https://find-dfe-approved-framework.service.gov.uk/list/it-hardware                                 | _blank |
          | Multi-functional devices and digital solutions                                                            | https://find-dfe-approved-framework.service.gov.uk/list/mfd-digital-solutions                       | _blank |
          | Multifunctional devices and digital transformation solutions                                              | https://find-dfe-approved-framework.service.gov.uk/list/mfd-digi-transform                          | _blank |
          | Multifunctional devices, print and digital workflow software services and managed print service provision | https://find-dfe-approved-framework.service.gov.uk/list/printing-services                           | _blank |
          | Outsourced ICT                                                                                            | https://find-dfe-approved-framework.service.gov.uk/list/outsourced-ict                              | _blank |
          | Print marketplace                                                                                         | https://find-dfe-approved-framework.service.gov.uk/list/print-marketplace                           | _blank |
          | Software licences and associated services for academies and schools                                       | https://find-dfe-approved-framework.service.gov.uk/list/software-licenses                           | _blank |
          | Technology products & associated services 2                                                               | https://find-dfe-approved-framework.service.gov.uk/list/technology-products-and-associated-services | _blank |
          | Catering services                                                                                         | https://find-dfe-approved-framework.service.gov.uk/list/outsourced-catering                         | _blank |
          | Catering services                                                                                         | https://find-dfe-approved-framework.service.gov.uk/list/catering-services                           | _blank |
          | Commercial catering equipment                                                                             | https://find-dfe-approved-framework.service.gov.uk/list/catering-equipment                          | _blank |
          | Grocery, fresh, chilled and frozen foods                                                                  | https://find-dfe-approved-framework.service.gov.uk/list/grocery-fresh-chilled-and-frozen-foods      | _blank |
          | Outsourced catering services                                                                              | https://find-dfe-approved-framework.service.gov.uk/list/outsourced-catering-cpc                     | _blank |
          | Facilities management and workplace services                                                              | https://find-dfe-approved-framework.service.gov.uk/list/fm-workplace-dps                            | _blank |