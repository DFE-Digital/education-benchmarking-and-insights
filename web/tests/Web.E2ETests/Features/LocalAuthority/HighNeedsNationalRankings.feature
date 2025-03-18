Feature: Local Authority high needs national rankings

    @HighNeedsFlagEnabled
    Scenario: National rankings displays stubbed data in table
        Given I am on local authority high needs national rankings for local authority with code '204'
        When I click on table view
        Then the national rankings table is displayed with the following values:
          | Name                   | Value  |
          | Kensington and Chelsea | 81.7%  |
          | Lambeth                | 89.7%  |
          | Hammersmith and Fulham | 90.5%  |
          | Islington              | 91.6%  |
          | Southwark              | 93.7%  |
          | Greenwich              | 97%    |
          | Lewisham               | 102.5% |
          | Hackney                | 103%   |
          | Camden                 | 105.2% |
          | City of London         | 148.5% |

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