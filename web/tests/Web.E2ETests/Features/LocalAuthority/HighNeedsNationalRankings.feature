Feature: Local Authority high needs national rankings

    @HighNeedsFlagEnabled
    Scenario: National rankings displays data in table
        Given I am on local authority high needs national rankings for local authority with code '204'
        When I click on table view
        Then the national rankings table is displayed with the following values:
          | Name                   | Value  |
          | City of London         | 74.4%  |
          | Islington              | 83.8%  |
          | Greenwich              | 90.6%  |
          | Hammersmith and Fulham | 97.4%  |
          | Camden                 | 97.5%  |
          | Southwark              | 97.5%  |
          | Lambeth                | 98.5%  |
          | Kensington and Chelsea | 100.3% |
          | Lewisham               | 105.3% |
          | Hackney                | 109.1% |

    @HighNeedsFlagEnabled
    Scenario: Download national ranking chart
        Given I am on local authority high needs national rankings for local authority with code '204'
        When I click on save as image
        Then the chart image is downloaded

    @HighNeedsFlagEnabled
    Scenario: Copy national ranking chart
        Given I am on local authority high needs national rankings for local authority with code '204'
        When I click on copy image
        Then the chart image is copied