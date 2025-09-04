Feature: News

    Scenario: Go to news listing
        Given I am on home page
        When I click on the news link
        Then the news page is displayed with '2' articles

    Scenario: Go to news article
        Given I am on news page
        When I click on the '<title>' article link
        Then the news article is displayed with the heading '<heading>'

    Examples:
      | title     | heading        |
      | Published | Published body |