﻿Feature: Establishment Comparators Endpoint Testing

    Scenario: Sending a valid comparator schools request
        Given a valid comparator schools request for school id '990000'
        When I submit the comparator schools request
        Then the comparator schools should total '234' and contain:
          | Urn    |
          | 990211 |
          | 990223 |
          | 990059 |
          | 990070 |
          | 990111 |
          | 990156 |
          | 990047 |
          | 777042 |
          | 990082 |
          | 990186 |
          | 990205 |
          | 990183 |
          | 990239 |
          | 990031 |
          | 990245 |
          | 990115 |
          | 990180 |
          | 990100 |
          | 990242 |
          | 990155 |
          | 990146 |
          | 990131 |
          | 990247 |
          | 990008 |
          | 990226 |
          | 990087 |
          | 990191 |
          | 990012 |
          | 990122 |

    Scenario: Sending a valid comparator trusts request
        Given a valid comparator trusts request for company number '10192252'
        When I submit the comparator trusts request
        Then the comparator trusts should total '25' and contain:
          | CompanyNumber |
          | 07185046      |
          | 07694547      |
          | 07930340      |
          | 07452885      |
          | 07695504      |
          | 07703941      |
          | 07751232      |
          | 09896221      |
          | 08085503      |