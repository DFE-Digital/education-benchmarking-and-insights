Feature: High needs glossary

    Scenario: Terms displayed in the high needs glossary table
        Given I am on high needs glossary page
        Then there are 9 items in the high needs glossary
        
    Scenario: Terms displayed in the general glossary table
        Given I am on high needs glossary page
        Then there are 22 items in the general glossary