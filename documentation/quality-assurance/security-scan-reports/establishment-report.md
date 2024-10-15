# ZAP Scanning Report

ZAP is supported by the [Crash Override Open Source Fellowship](https://crashoverride.com/?zap=rep).


## Summary of Alerts

| Risk Level | Number of Alerts |
| --- | --- |
| High | 0 |
| Medium | 0 |
| Low | 1 |
| Informational | 6 |




## Alerts

| Name | Risk Level | Number of Instances |
| --- | --- | --- |
| Unexpected Content-Type was returned | Low | 2 |
| A Client Error response code was returned by the server | Informational | 90 |
| Sec-Fetch-Dest Header is Missing | Informational | 1 |
| Sec-Fetch-Mode Header is Missing | Informational | 1 |
| Sec-Fetch-Site Header is Missing | Informational | 1 |
| Sec-Fetch-User Header is Missing | Informational | 1 |
| Storable and Cacheable Content | Informational | 1 |




## Alert Detail



### [ Unexpected Content-Type was returned ](https://www.zaproxy.org/docs/alerts/100001/)



##### Low (High)

### Description

A Content-Type of text/html was returned by the server.
This is not one of the types expected to be returned by an API.
Raised by the 'Alert on Unexpected Content Types' script

* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/actuator/health
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `text/html`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/latest/meta-data/
  * Method: `GET`
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

A response code of 404 was returned by the server.
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
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/8638577003649186789
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
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/actuator/health
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 400`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/actuator/health
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
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/5589243290875860127
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/swagger
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/swagger/
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/swagger/.env
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/swagger/.htaccess
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/swagger/1228812262873263743
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/swagger/json
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/swagger/json/
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/swagger/json%3Fclass.module.classLoader.DefaultAssertionStatus=nonsense
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/swagger/json%3Fname=abc
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/swagger/trace.axd
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/swagger%3Fclass.module.classLoader.DefaultAssertionStatus=nonsense
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/swagger%3Fname=abc
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/trace.axd
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
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/clientaccesspolicy.xml
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
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/crossdomain.xml
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
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/latest/meta-data/
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
  * Method: `OPTIONS`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/swagger
  * Method: `OPTIONS`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/swagger/json
  * Method: `OPTIONS`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/swagger
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/swagger/json
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/swagger/json%3F-d+allow_url_include%253d1+-d+auto_prepend_file%253dphp://input
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `HTTP/1.1 404`
  * Other Info: ``
* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/swagger%3F-d+allow_url_include%253d1+-d+auto_prepend_file%253dphp://input
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

Instances: 90

### Solution



### Reference



#### CWE Id: [ 388 ](https://cwe.mitre.org/data/definitions/388.html)


#### WASC Id: 20

#### Source ID: 4

### [ Sec-Fetch-Dest Header is Missing ](https://www.zaproxy.org/docs/alerts/90005/)



##### Informational (High)

### Description

Specifies how and where the data would be used. For instance, if the value is audio, then the requested resource must be audio data and not any other type of resource.

* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/swagger/json
  * Method: `GET`
  * Parameter: `Sec-Fetch-Dest`
  * Attack: ``
  * Evidence: ``
  * Other Info: ``

Instances: 1

### Solution

Ensure that Sec-Fetch-Dest header is included in request headers.

### Reference


* [ https://developer.mozilla.org/en-US/docs/Web/HTTP/Headers/Sec-Fetch-Dest ](https://developer.mozilla.org/en-US/docs/Web/HTTP/Headers/Sec-Fetch-Dest)


#### CWE Id: [ 352 ](https://cwe.mitre.org/data/definitions/352.html)


#### WASC Id: 9

#### Source ID: 3

### [ Sec-Fetch-Mode Header is Missing ](https://www.zaproxy.org/docs/alerts/90005/)



##### Informational (High)

### Description

Allows to differentiate between requests for navigating between HTML pages and requests for loading resources like images, audio etc.

* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/swagger/json
  * Method: `GET`
  * Parameter: `Sec-Fetch-Mode`
  * Attack: ``
  * Evidence: ``
  * Other Info: ``

Instances: 1

### Solution

Ensure that Sec-Fetch-Mode header is included in request headers.

### Reference


* [ https://developer.mozilla.org/en-US/docs/Web/HTTP/Headers/Sec-Fetch-Mode ](https://developer.mozilla.org/en-US/docs/Web/HTTP/Headers/Sec-Fetch-Mode)


#### CWE Id: [ 352 ](https://cwe.mitre.org/data/definitions/352.html)


#### WASC Id: 9

#### Source ID: 3

### [ Sec-Fetch-Site Header is Missing ](https://www.zaproxy.org/docs/alerts/90005/)



##### Informational (High)

### Description

Specifies the relationship between request initiator's origin and target's origin.

* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/swagger/json
  * Method: `GET`
  * Parameter: `Sec-Fetch-Site`
  * Attack: ``
  * Evidence: ``
  * Other Info: ``

Instances: 1

### Solution

Ensure that Sec-Fetch-Site header is included in request headers.

### Reference


* [ https://developer.mozilla.org/en-US/docs/Web/HTTP/Headers/Sec-Fetch-Site ](https://developer.mozilla.org/en-US/docs/Web/HTTP/Headers/Sec-Fetch-Site)


#### CWE Id: [ 352 ](https://cwe.mitre.org/data/definitions/352.html)


#### WASC Id: 9

#### Source ID: 3

### [ Sec-Fetch-User Header is Missing ](https://www.zaproxy.org/docs/alerts/90005/)



##### Informational (High)

### Description

Specifies if a navigation request was initiated by a user.

* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/swagger/json
  * Method: `GET`
  * Parameter: `Sec-Fetch-User`
  * Attack: ``
  * Evidence: ``
  * Other Info: ``

Instances: 1

### Solution

Ensure that Sec-Fetch-User header is included in user initiated requests.

### Reference


* [ https://developer.mozilla.org/en-US/docs/Web/HTTP/Headers/Sec-Fetch-User ](https://developer.mozilla.org/en-US/docs/Web/HTTP/Headers/Sec-Fetch-User)


#### CWE Id: [ 352 ](https://cwe.mitre.org/data/definitions/352.html)


#### WASC Id: 9

#### Source ID: 3

### [ Storable and Cacheable Content ](https://www.zaproxy.org/docs/alerts/10049/)



##### Informational (Medium)

### Description

The response contents are storable by caching components such as proxy servers, and may be retrieved directly from the cache, rather than from the origin server by the caching servers, in response to similar requests from other users.  If the response data is sensitive, personal or user-specific, this may result in sensitive information being leaked. In some cases, this may even result in a user gaining complete control of the session of another user, depending on the configuration of the caching components in use in their environment. This is primarily an issue where "shared" caching servers such as "proxy" caches are configured on the local network. This configuration is typically found in corporate or educational environments, for instance.

* URL: https://s198d01-ebis-establishment-fa.azurewebsites.net/api/swagger/json
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: ``
  * Other Info: `In the absence of an explicitly specified caching lifetime directive in the response, a liberal lifetime heuristic of 1 year was assumed. This is permitted by rfc7234.`

Instances: 1

### Solution

Validate that the response does not contain sensitive, personal or user-specific information.  If it does, consider the use of the following HTTP response headers, to limit, or prevent the content being stored and retrieved from the cache by another user:
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
