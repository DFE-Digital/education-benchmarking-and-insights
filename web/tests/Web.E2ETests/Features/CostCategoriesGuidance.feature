Feature: Cost categories guidance

    Scenario: Sub categories are displayed on guidance page
        Given I am on cost categories guidance page
        Then the sub categories for each category are
          | Category                                   | Sub-category                                             |
          | Teaching and Teaching support staff        | teaching staff                                           |
          | Teaching and Teaching support staff        | supply teaching staff                                    |
          | Teaching and Teaching support staff        | agency supply teaching staff                             |
          | Teaching and Teaching support staff        | education support staff                                  |
          | Teaching and Teaching support staff        | educational consultancy                                  |
          | Non-educational support staff and services | administrative and clerical staff                        |
          | Non-educational support staff and services | auditor costs                                            |
          | Non-educational support staff and services | other staff                                              |
          | Non-educational support staff and services | professional services (non-curriculum)                   |
          | Educational supplies                       | examination fees                                         |
          | Educational supplies                       | learning resources (not ICT equipment)                   |
          | Educational ICT                            | ICT learning resources                                   |
          | Premises staff and services                | cleaning and caretaking                                  |
          | Premises staff and services                | maintenance of premises                                  |
          | Premises staff and services                | other occupation costs                                   |
          | Premises staff and services                | premises staff                                           |
          | Utilities                                  | energy                                                   |
          | Utilities                                  | water and sewerage                                       |
          | Administrative supplies                    | administrative supplies (non-educational)                |
          | Catering staff and supplies                | catering staff                                           |
          | Catering staff and supplies                | catering supplies                                        |
          | Other costs                                | direct revenue financing                                 |
          | Other costs                                | grounds maintenance                                      |
          | Other costs                                | indirect employee expenses                               |
          | Other costs                                | interest charges for loan and bank                       |
          | Other costs                                | other insurance premiums                                 |
          | Other costs                                | private finance initiative (PFI) charges                 |
          | Other costs                                | rent and rates                                           |
          | Other costs                                | special facilities                                       |
          | Other costs                                | staff development and training                           |
          | Other costs                                | staff-related insurance                                  |
          | Other costs                                | supply teacher insurance                                 |
          | Other costs                                | community focused school staff (maintained schools only) |
          | Other costs                                | community focused school costs (maintained schools only) |