Feature: Local Authority high needs national rankings

    @HighNeedsFlagEnabled
    Scenario: National rankings displays data in table for funding
        Given I am on local authority high needs national rankings for local authority with code '204'
        When I click on tab 'funding'
        And I click on table view for 'funding'
        Then the national rankings table for 'funding' is displayed with the following values:
          | Name                   | Value  |
          | Camden                 | 93.6%  |
          | Islington              | 95.7%  |
          | Greenwich              | 101.9% |
          | Lambeth                | 104.6% |
          | Southwark              | 107.5% |
          | Hammersmith and Fulham | 110.4% |
          | Kensington and Chelsea | 110.9% |
          | Hackney                | 111.8% |
          | Lewisham               | 113%   |
          
    @HighNeedsFlagEnabled
    Scenario: National rankings displays data in table for planned expenditure
        Given I am on local authority high needs national rankings for local authority with code '204'
        When I click on tab 'planned-expenditure'
        And I click on table view for 'planned-expenditure'
        Then the national rankings table for 'planned-expenditure' is displayed with the following values:
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
        When I click on tab 'funding'
        Then the missing ranking warning message should not be displayed for 'funding'

    @HighNeedsFlagEnabled
    Scenario: Warning banner displayed if current local authority is not in rankings
        Given I am on local authority high needs national rankings for local authority with code '211'
        When I click on tab 'funding'
        Then the missing ranking warning message should be displayed for 'funding'

    @HighNeedsFlagEnabled
    Scenario: Download national ranking chart for funding
        Given I am on local authority high needs national rankings for local authority with code '204'
        When I click on tab 'funding'
        And I click on save as image for 'funding'
        Then the chart image is downloaded for 'funding'
        
    @HighNeedsFlagEnabled
    Scenario: Download national ranking chart for planned expenditure
        Given I am on local authority high needs national rankings for local authority with code '204'
        When I click on tab 'planned-expenditure'
        And I click on save as image for 'planned-expenditure'
        Then the chart image is downloaded for 'planned-expenditure'

    @HighNeedsFlagEnabled
    Scenario: Copy national ranking chart for funding
        Given I am on local authority high needs national rankings for local authority with code '204'
        When I click on tab 'funding'
        And I click on copy image for 'funding'
        Then the chart image is copied
        
    @HighNeedsFlagEnabled
    Scenario: Copy national ranking chart for planned expenditure
        Given I am on local authority high needs national rankings for local authority with code '204'
        When I click on tab 'planned-expenditure'
        And I click on copy image for 'planned-expenditure'
        Then the chart image is copied