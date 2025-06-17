Feature: Local Authority high needs national rankings

    @HighNeedsFlagEnabled
    Scenario: National rankings displays data in table
        Given I am on local authority high needs national rankings for local authority with code '204'
        When I click on table view
        Then the national rankings table is displayed with the following values:
          | Name                   | Value  |
          | City of London         | 74.4%  |
          | Islington              | 92.7%  |
          | Southwark              | 102%   |
          | Greenwich              | 102.5% |
          | Camden                 | 104.1% |
          | Lambeth                | 104.4% |
          | Lewisham               | 106.2% |
          | Hackney                | 109.9% |
          | Kensington and Chelsea | 110.3% |
          | Hammersmith and Fulham | 114.1% |

    @HighNeedsFlagEnabled
    Scenario: Warning banner not displayed if current local authority is in rankings
        Given I am on local authority high needs national rankings for local authority with code '204'
        Then the missing ranking warning message should not be displayed

    @HighNeedsFlagEnabled
    Scenario: Warning banner displayed if current local authority is not in rankings
        Given I am on local authority high needs national rankings for local authority with code '211'
        Then the missing ranking warning message should be displayed

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