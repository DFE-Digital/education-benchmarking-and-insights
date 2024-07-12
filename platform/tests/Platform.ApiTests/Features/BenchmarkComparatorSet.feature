Feature: Benchmark Comparator set Endpoint Testing

    Scenario: Getting a default comparator set's buildings successfully
        Given I have a valid default comparator set get request for school id '990000'
        When I submit the default comparator set request
        Then the default comparator set result should contain comparator buildings:
          | Urn    |  
          | 990683 |
          | 990545 |
          | 990518 |
          | 990283 |
          | 990027 |
          | 990716 |
          | 990442 |
          | 990108 |
          | 990280 |
          | 990626 |
          | 990079 |
          | 990075 |
          | 990023 |
          | 990362 |
          | 990107 |
          | 990465 |
          | 990562 |
          | 990059 |
          | 990540 |
          | 990014 |
          | 990461 |
          | 990072 |
          | 990184 |
          | 990432 |
          | 990037 |
          | 990534 |
          | 990016 |
          | 990250 |
          | 990083 |
          | 990576 |
          
    Scenario: Getting a default comparator set's pupils successfully
        Given I have a valid default comparator set get request for school id '990000'
        When I submit the default comparator set request
        Then the default comparator set result should contain comparator pupils:
          | Urn    |  
          | 990708 |
          | 990512 |
          | 990257 |
          | 990137 |
          | 990750 |
          | 990068 |
          | 990062 |
          | 990531 |
          | 990461 |
          | 990521 |
          | 990407 |
          | 990144 |
          | 990570 |
          | 990662 |
          | 990601 |
          | 990279 |
          | 990742 |
          | 990743 |
          | 990333 |
          | 990359 |
          | 990536 |
          | 990561 |
          | 990386 |
          | 990220 |
          | 990712 |
          | 990745 |
          | 990351 |
          | 990075 |
          | 990533 |
          | 990086 |
    
    # skip due to data pipeline dependency
    @ignore  
    Scenario: Getting a user defined comparator set's buildings successfully
        Given I have a valid user defined comparator set get request for school id '990000' containing:
          | Urn    |  
          | 990683 |
          | 990545 |
          | 990518 |
        When I submit the user defined comparator set request
        Then the user defined comparator set result should contain comparator buildings:
          | Urn    |  
          | 990683 |
          | 990545 |
          | 990518 |
          | 990000 |

    Scenario: Create a user defined comparator set successfully
        Given I have a valid user defined comparator set request for school id '990000'
        When I submit the user defined comparator set request
        Then the comparator result should be accepted

    Scenario: Sending a bad user defined comparator set request
        Given I have an invalid user defined comparator set request for school id '990000'
        When I submit the user defined comparator set request
        Then the comparator result should be bad request
        
    Scenario: Deleting a user defined comparator set that does not exist
        Given I have an invalid delete user defined comparator set request for school id '990000'
        When I submit the user defined comparator set request
        Then the comparator result should be not found
        
    Scenario: Getting a user defined trust comparator set successfully
        Given I have a valid user defined comparator set get request for trust id '10192252' containing:
          | CompanyNumber |  
          | 10249712      |
          | 10259334      |
          | 10264735      |
        When I submit the user defined trust comparator set request
        Then the user defined trust comparator set result should contain comparators:
          | CompanyNumber |  
          | 10249712      |
          | 10259334      |
          | 10264735      |
          | 10192252      |

    Scenario: Create a user defined trust comparator set successfully
        Given I have a valid user defined comparator set request for trust id '10192252'
        When I submit the user defined trust comparator set request
        Then the trust comparator result should be accepted

    Scenario: Sending a bad user defined trust comparator set request
        Given I have an invalid user defined comparator set request for trust id '10192252'
        When I submit the user defined trust comparator set request
        Then the trust comparator result should be bad request
        
    Scenario: Deleting a user defined trust comparator set that does not exist
        Given I have an invalid delete user defined comparator set request for trust id '10192252'
        When I submit the user defined trust comparator set request
        Then the trust comparator result should be not found
        
    Scenario: Deleting a user defined comparator set that does exist
        Given I have a valid delete user defined comparator set get request for trust id '10192252' containing:
          | CompanyNumber |  
          | 10249712      |
          | 10259334      |
          | 10264735      |
        When I submit the user defined trust comparator set request
        Then the trust comparator result should be ok