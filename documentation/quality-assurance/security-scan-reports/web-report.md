# ZAP Scanning Report

ZAP by [Checkmarx](https://checkmarx.com/).


## Summary of Alerts

| Risk Level | Number of Alerts |
| --- | --- |
| High | 0 |
| Medium | 3 |
| Low | 6 |
| Informational | 9 |




## Alerts

| Name | Risk Level | Number of Instances |
| --- | --- | --- |
| Proxy Disclosure | Medium | 26 |
| Sub Resource Integrity Attribute Missing | Medium | 9 |
| XSLT Injection | Medium | 2 |
| Cookie with SameSite Attribute None | Low | 12 |
| Cross-Domain JavaScript Source File Inclusion | Low | 9 |
| Deprecated Feature Policy Header Set | Low | 11 |
| Private IP Disclosure | Low | 1 |
| Timestamp Disclosure - Unix | Low | 11 |
| X-Content-Type-Options Header Missing | Low | 9 |
| Cookie Slack Detector | Informational | 7 |
| Information Disclosure - Suspicious Comments | Informational | 12 |
| Modern Web Application | Informational | 1 |
| Non-Storable Content | Informational | 2 |
| Re-examine Cache-control Directives | Informational | 8 |
| Session Management Response Identified | Informational | 1 |
| Storable and Cacheable Content | Informational | 11 |
| User Agent Fuzzer | Informational | 194 |
| User Controllable HTML Element Attribute (Potential XSS) | Informational | 2 |




## Alert Detail



### [ Proxy Disclosure ](https://www.zaproxy.org/docs/alerts/40025/)



##### Medium (Medium)

### Description

2 proxy server(s) were detected or fingerprinted. This information helps a potential attacker to determine
- A list of targets for an attack against the application.
 - Potential vulnerabilities on the proxy servers that service the application.
 - The presence or absence of any proxy-based components that might cause attacks against the application to be detected, prevented, or mitigated.

* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net
  * Method: `GET`
  * Parameter: ``
  * Attack: `TRACE, OPTIONS methods with 'Max-Forwards' header. TRACK method.`
  * Evidence: ``
  * Other Info: `Using the TRACE, OPTIONS, and TRACK methods, the following proxy servers have been identified between ZAP and the application/web server:
- Unknown
- Unknown
The following web/application server has been identified:
- Unknown
`
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/
  * Method: `GET`
  * Parameter: ``
  * Attack: `TRACE, OPTIONS methods with 'Max-Forwards' header. TRACK method.`
  * Evidence: ``
  * Other Info: `Using the TRACE, OPTIONS, and TRACK methods, the following proxy servers have been identified between ZAP and the application/web server:
- Unknown
- Unknown
The following web/application server has been identified:
- Unknown
`
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/accessibility
  * Method: `GET`
  * Parameter: ``
  * Attack: `TRACE, OPTIONS methods with 'Max-Forwards' header. TRACK method.`
  * Evidence: ``
  * Other Info: `Using the TRACE, OPTIONS, and TRACK methods, the following proxy servers have been identified between ZAP and the application/web server:
- Unknown
- Unknown
The following web/application server has been identified:
- Unknown
`
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/assets
  * Method: `GET`
  * Parameter: ``
  * Attack: `TRACE, OPTIONS methods with 'Max-Forwards' header. TRACK method.`
  * Evidence: ``
  * Other Info: `Using the TRACE, OPTIONS, and TRACK methods, the following proxy servers have been identified between ZAP and the application/web server:
- Unknown
- Unknown
The following web/application server has been identified:
- Unknown
`
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/assets/images
  * Method: `GET`
  * Parameter: ``
  * Attack: `TRACE, OPTIONS methods with 'Max-Forwards' header. TRACK method.`
  * Evidence: ``
  * Other Info: `Using the TRACE, OPTIONS, and TRACK methods, the following proxy servers have been identified between ZAP and the application/web server:
- Unknown
- Unknown
The following web/application server has been identified:
- Unknown
`
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/assets/images/favicon.ico
  * Method: `GET`
  * Parameter: ``
  * Attack: `TRACE, OPTIONS methods with 'Max-Forwards' header. TRACK method.`
  * Evidence: ``
  * Other Info: `Using the TRACE, OPTIONS, and TRACK methods, the following proxy servers have been identified between ZAP and the application/web server:
- Unknown
- Unknown
The following web/application server has been identified:
- Unknown
`
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/assets/images/favicon.svg
  * Method: `GET`
  * Parameter: ``
  * Attack: `TRACE, OPTIONS methods with 'Max-Forwards' header. TRACK method.`
  * Evidence: ``
  * Other Info: `Using the TRACE, OPTIONS, and TRACK methods, the following proxy servers have been identified between ZAP and the application/web server:
- Unknown
- Unknown
The following web/application server has been identified:
- Unknown
`
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/assets/images/govuk-icon-180.png
  * Method: `GET`
  * Parameter: ``
  * Attack: `TRACE, OPTIONS methods with 'Max-Forwards' header. TRACK method.`
  * Evidence: ``
  * Other Info: `Using the TRACE, OPTIONS, and TRACK methods, the following proxy servers have been identified between ZAP and the application/web server:
- Unknown
- Unknown
The following web/application server has been identified:
- Unknown
`
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/assets/images/govuk-icon-mask.svg
  * Method: `GET`
  * Parameter: ``
  * Attack: `TRACE, OPTIONS methods with 'Max-Forwards' header. TRACK method.`
  * Evidence: ``
  * Other Info: `Using the TRACE, OPTIONS, and TRACK methods, the following proxy servers have been identified between ZAP and the application/web server:
- Unknown
- Unknown
The following web/application server has been identified:
- Unknown
`
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/assets/manifest.json
  * Method: `GET`
  * Parameter: ``
  * Attack: `TRACE, OPTIONS methods with 'Max-Forwards' header. TRACK method.`
  * Evidence: ``
  * Other Info: `Using the TRACE, OPTIONS, and TRACK methods, the following proxy servers have been identified between ZAP and the application/web server:
- Unknown
- Unknown
The following web/application server has been identified:
- Unknown
`
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/contact
  * Method: `GET`
  * Parameter: ``
  * Attack: `TRACE, OPTIONS methods with 'Max-Forwards' header. TRACK method.`
  * Evidence: ``
  * Other Info: `Using the TRACE, OPTIONS, and TRACK methods, the following proxy servers have been identified between ZAP and the application/web server:
- Unknown
- Unknown
The following web/application server has been identified:
- Unknown
`
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/cookies
  * Method: `GET`
  * Parameter: ``
  * Attack: `TRACE, OPTIONS methods with 'Max-Forwards' header. TRACK method.`
  * Evidence: ``
  * Other Info: `Using the TRACE, OPTIONS, and TRACK methods, the following proxy servers have been identified between ZAP and the application/web server:
- Unknown
- Unknown
The following web/application server has been identified:
- Unknown
`
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/cookies%3Fcookies-saved=true
  * Method: `GET`
  * Parameter: ``
  * Attack: `TRACE, OPTIONS methods with 'Max-Forwards' header. TRACK method.`
  * Evidence: ``
  * Other Info: `Using the TRACE, OPTIONS, and TRACK methods, the following proxy servers have been identified between ZAP and the application/web server:
- Unknown
- Unknown
The following web/application server has been identified:
- Unknown
`
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/css
  * Method: `GET`
  * Parameter: ``
  * Attack: `TRACE, OPTIONS methods with 'Max-Forwards' header. TRACK method.`
  * Evidence: ``
  * Other Info: `Using the TRACE, OPTIONS, and TRACK methods, the following proxy servers have been identified between ZAP and the application/web server:
- Unknown
- Unknown
The following web/application server has been identified:
- Unknown
`
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/css/front-end.css%3Fv=Fk9S3ib-0VugYDs4FpUNvvAm6JE5yD-W2AFg4drw694
  * Method: `GET`
  * Parameter: ``
  * Attack: `TRACE, OPTIONS methods with 'Max-Forwards' header. TRACK method.`
  * Evidence: ``
  * Other Info: `Using the TRACE, OPTIONS, and TRACK methods, the following proxy servers have been identified between ZAP and the application/web server:
- Unknown
- Unknown
The following web/application server has been identified:
- Unknown
`
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/css/main.css%3Fv=UVZegOuzG5lo6RmYvNHND4ImtgfVPUTnYytqhFE7IPk
  * Method: `GET`
  * Parameter: ``
  * Attack: `TRACE, OPTIONS methods with 'Max-Forwards' header. TRACK method.`
  * Evidence: ``
  * Other Info: `Using the TRACE, OPTIONS, and TRACK methods, the following proxy servers have been identified between ZAP and the application/web server:
- Unknown
- Unknown
The following web/application server has been identified:
- Unknown
`
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/find-organisation
  * Method: `GET`
  * Parameter: ``
  * Attack: `TRACE, OPTIONS methods with 'Max-Forwards' header. TRACK method.`
  * Evidence: ``
  * Other Info: `Using the TRACE, OPTIONS, and TRACK methods, the following proxy servers have been identified between ZAP and the application/web server:
- Unknown
- Unknown
The following web/application server has been identified:
- Unknown
`
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/js
  * Method: `GET`
  * Parameter: ``
  * Attack: `TRACE, OPTIONS methods with 'Max-Forwards' header. TRACK method.`
  * Evidence: ``
  * Other Info: `Using the TRACE, OPTIONS, and TRACK methods, the following proxy servers have been identified between ZAP and the application/web server:
- Unknown
- Unknown
The following web/application server has been identified:
- Unknown
`
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/js/front-end.js%3Fv=1GeIUpYa1r7xaoPXlZXIxaLMfkrs9kM5iUMQkRpAreQ
  * Method: `GET`
  * Parameter: ``
  * Attack: `TRACE, OPTIONS methods with 'Max-Forwards' header. TRACK method.`
  * Evidence: ``
  * Other Info: `Using the TRACE, OPTIONS, and TRACK methods, the following proxy servers have been identified between ZAP and the application/web server:
- Unknown
- Unknown
The following web/application server has been identified:
- Unknown
`
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/js/govuk-frontend.min.js
  * Method: `GET`
  * Parameter: ``
  * Attack: `TRACE, OPTIONS methods with 'Max-Forwards' header. TRACK method.`
  * Evidence: ``
  * Other Info: `Using the TRACE, OPTIONS, and TRACK methods, the following proxy servers have been identified between ZAP and the application/web server:
- Unknown
- Unknown
The following web/application server has been identified:
- Unknown
`
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/robots.txt
  * Method: `GET`
  * Parameter: ``
  * Attack: `TRACE, OPTIONS methods with 'Max-Forwards' header. TRACK method.`
  * Evidence: ``
  * Other Info: `Using the TRACE, OPTIONS, and TRACK methods, the following proxy servers have been identified between ZAP and the application/web server:
- Unknown
- Unknown
The following web/application server has been identified:
- Unknown
`
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/school
  * Method: `GET`
  * Parameter: ``
  * Attack: `TRACE, OPTIONS methods with 'Max-Forwards' header. TRACK method.`
  * Evidence: ``
  * Other Info: `Using the TRACE, OPTIONS, and TRACK methods, the following proxy servers have been identified between ZAP and the application/web server:
- Unknown
- Unknown
The following web/application server has been identified:
- Unknown
`
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/school/135966
  * Method: `GET`
  * Parameter: ``
  * Attack: `TRACE, OPTIONS methods with 'Max-Forwards' header. TRACK method.`
  * Evidence: ``
  * Other Info: `Using the TRACE, OPTIONS, and TRACK methods, the following proxy servers have been identified between ZAP and the application/web server:
- Unknown
- Unknown
The following web/application server has been identified:
- Unknown
`
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/sign-in%3FredirectUri=%252F
  * Method: `GET`
  * Parameter: ``
  * Attack: `TRACE, OPTIONS methods with 'Max-Forwards' header. TRACK method.`
  * Evidence: ``
  * Other Info: `Using the TRACE, OPTIONS, and TRACK methods, the following proxy servers have been identified between ZAP and the application/web server:
- Unknown
- Unknown
The following web/application server has been identified:
- Unknown
`
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/sitemap.xml
  * Method: `GET`
  * Parameter: ``
  * Attack: `TRACE, OPTIONS methods with 'Max-Forwards' header. TRACK method.`
  * Evidence: ``
  * Other Info: `Using the TRACE, OPTIONS, and TRACK methods, the following proxy servers have been identified between ZAP and the application/web server:
- Unknown
- Unknown
The following web/application server has been identified:
- Unknown
`
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/cookies
  * Method: `POST`
  * Parameter: ``
  * Attack: `TRACE, OPTIONS methods with 'Max-Forwards' header. TRACK method.`
  * Evidence: ``
  * Other Info: `Using the TRACE, OPTIONS, and TRACK methods, the following proxy servers have been identified between ZAP and the application/web server:
- Unknown
- Unknown
The following web/application server has been identified:
- Unknown
`

Instances: 26

### Solution

Disable the 'TRACE' method on the proxy servers, as well as the origin web/application server.
Disable the 'OPTIONS' method on the proxy servers, as well as the origin web/application server, if it is not required for other purposes, such as 'CORS' (Cross Origin Resource Sharing).
Configure the web and application servers with custom error pages, to prevent 'fingerprintable' product-specific error pages being leaked to the user in the event of HTTP errors, such as 'TRACK' requests for non-existent pages.
Configure all proxies, application servers, and web servers to prevent disclosure of the technology and version information in the 'Server' and 'X-Powered-By' HTTP response headers.


### Reference


* [ https://tools.ietf.org/html/rfc7231#section-5.1.2 ](https://tools.ietf.org/html/rfc7231#section-5.1.2)


#### CWE Id: [ 200 ](https://cwe.mitre.org/data/definitions/200.html)


#### WASC Id: 45

#### Source ID: 1

### [ Sub Resource Integrity Attribute Missing ](https://www.zaproxy.org/docs/alerts/90003/)



##### Medium (High)

### Description

The integrity attribute is missing on a script or link tag served by an external server. The integrity tag prevents an attacker who have gained access to this server from injecting a malicious content.

* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `<script type="text/javascript" src="https://js.monitor.azure.com/scripts/b/ext/ai.clck.2.min.js"></script>`
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/accessibility
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `<script type="text/javascript" src="https://js.monitor.azure.com/scripts/b/ext/ai.clck.2.min.js"></script>`
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/contact
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `<script type="text/javascript" src="https://js.monitor.azure.com/scripts/b/ext/ai.clck.2.min.js"></script>`
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/cookies
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `<script type="text/javascript" src="https://js.monitor.azure.com/scripts/b/ext/ai.clck.2.min.js"></script>`
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/cookies%3Fcookies-saved=true
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `<script type="text/javascript" src="https://js.monitor.azure.com/scripts/b/ext/ai.clck.2.min.js"></script>`
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/find-organisation
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `<script type="text/javascript" src="https://js.monitor.azure.com/scripts/b/ext/ai.clck.2.min.js"></script>`
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/robots.txt
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `<script type="text/javascript" src="https://js.monitor.azure.com/scripts/b/ext/ai.clck.2.min.js"></script>`
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/school/135966
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `<script type="text/javascript" src="https://js.monitor.azure.com/scripts/b/ext/ai.clck.2.min.js"></script>`
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/sitemap.xml
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `<script type="text/javascript" src="https://js.monitor.azure.com/scripts/b/ext/ai.clck.2.min.js"></script>`
  * Other Info: ``

Instances: 9

### Solution

Provide a valid integrity attribute to the tag.

### Reference


* [ https://developer.mozilla.org/en-US/docs/Web/Security/Subresource_Integrity ](https://developer.mozilla.org/en-US/docs/Web/Security/Subresource_Integrity)


#### CWE Id: [ 345 ](https://cwe.mitre.org/data/definitions/345.html)


#### WASC Id: 15

#### Source ID: 3

### [ XSLT Injection ](https://www.zaproxy.org/docs/alerts/90017/)



##### Medium (Medium)

### Description

Injection using XSL transformations may be possible, and may allow an attacker to read system information, read and write files, or execute arbitrary code.

* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/cookies
  * Method: `POST`
  * Parameter: `__RequestVerificationToken`
  * Attack: `<xsl:value-of select="system-property('xsl:vendor')"/>`
  * Evidence: `Microsoft`
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/cookies
  * Method: `POST`
  * Parameter: `AnalyticsCookiesEnabled`
  * Attack: `<xsl:value-of select="system-property('xsl:vendor')"/>`
  * Evidence: `Microsoft`
  * Other Info: ``

Instances: 2

### Solution

Sanitize and analyze every user input coming from any client-side.

### Reference


* [ https://www.contextis.com/blog/xslt-server-side-injection-attacks ](https://www.contextis.com/blog/xslt-server-side-injection-attacks)


#### CWE Id: [ 91 ](https://cwe.mitre.org/data/definitions/91.html)


#### WASC Id: 23

#### Source ID: 1

### [ Cookie with SameSite Attribute None ](https://www.zaproxy.org/docs/alerts/10054/)



##### Low (Medium)

### Description

A cookie has been set with its SameSite attribute set to "none", which means that the cookie can be sent as a result of a 'cross-site' request. The SameSite attribute is an effective counter measure to cross-site request forgery, cross-site script inclusion, and timing attacks.

* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/sign-in%3FredirectUri=%252F
  * Method: `GET`
  * Parameter: `.AspNetCore.Correlation.9tGXT24RoZ27bsvS--X-xjmpSiJSo3-SauJrCO4sT10`
  * Attack: ``
  * Evidence: `Set-Cookie: .AspNetCore.Correlation.9tGXT24RoZ27bsvS--X-xjmpSiJSo3-SauJrCO4sT10`
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/sign-in%3FredirectUri=%252F
  * Method: `GET`
  * Parameter: `.AspNetCore.OpenIdConnect.Nonce.CfDJ8Epa28AQ8o1JszkO6BYP-bqS4mcOJXgNsU7e4Ki-K3PqLlZv4wyJsEQg0IFUYuJRt_flNCyqhNjXEQCaIHplMLlA80rObAKbIn7O_I5CZdx_aqJNzhQBfoP6QmJ4dMZ_ApW-7Nuykyu4BylTpH8eLdb9-wjtx16oZ0xFySYO4XImgG3mobHOMXAp6FdTdaw8J1RMg2p1C1Im6AeeVXQdMh1xE8brol7nXCAMCqoqU99JNmCOZbdvD07J6KlVh3ubne2DFJW5YAmKNwFspv5DcDI`
  * Attack: ``
  * Evidence: `Set-Cookie: .AspNetCore.OpenIdConnect.Nonce.CfDJ8Epa28AQ8o1JszkO6BYP-bqS4mcOJXgNsU7e4Ki-K3PqLlZv4wyJsEQg0IFUYuJRt_flNCyqhNjXEQCaIHplMLlA80rObAKbIn7O_I5CZdx_aqJNzhQBfoP6QmJ4dMZ_ApW-7Nuykyu4BylTpH8eLdb9-wjtx16oZ0xFySYO4XImgG3mobHOMXAp6FdTdaw8J1RMg2p1C1Im6AeeVXQdMh1xE8brol7nXCAMCqoqU99JNmCOZbdvD07J6KlVh3ubne2DFJW5YAmKNwFspv5DcDI`
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/sign-in%3FredirectUri=%252Faccessibility
  * Method: `GET`
  * Parameter: `.AspNetCore.Correlation.Zd6QHXMh8jzGveik3AINl6QlPQJlAHJv4R4RFmht65g`
  * Attack: ``
  * Evidence: `Set-Cookie: .AspNetCore.Correlation.Zd6QHXMh8jzGveik3AINl6QlPQJlAHJv4R4RFmht65g`
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/sign-in%3FredirectUri=%252Faccessibility
  * Method: `GET`
  * Parameter: `.AspNetCore.OpenIdConnect.Nonce.CfDJ8Epa28AQ8o1JszkO6BYP-bptWEH414P7lyJw1vJgzg6M_TclZcIrrZqsM6YaBy23wsxgEJtKDSpmko2fUOZejQAuINHq1ypOlLTkE9gRfyxWLxKLAZQLf3uW_NyUICF6ngK9fyfAQqQDxqtMF7Xnu7jQZk0GkzE66Jy2P2XfWt9lKNaDcQ3Txkvwt5-sZ9V951m2pv24c7Gbn8loXh4jfvvso4OfXfYwNtDx0Nnk_deKFgXnV2uKdeCPh3Tlg0nB16crFTKju1t3E_gVLNR3aKE`
  * Attack: ``
  * Evidence: `Set-Cookie: .AspNetCore.OpenIdConnect.Nonce.CfDJ8Epa28AQ8o1JszkO6BYP-bptWEH414P7lyJw1vJgzg6M_TclZcIrrZqsM6YaBy23wsxgEJtKDSpmko2fUOZejQAuINHq1ypOlLTkE9gRfyxWLxKLAZQLf3uW_NyUICF6ngK9fyfAQqQDxqtMF7Xnu7jQZk0GkzE66Jy2P2XfWt9lKNaDcQ3Txkvwt5-sZ9V951m2pv24c7Gbn8loXh4jfvvso4OfXfYwNtDx0Nnk_deKFgXnV2uKdeCPh3Tlg0nB16crFTKju1t3E_gVLNR3aKE`
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/sign-in%3FredirectUri=%252Fcontact
  * Method: `GET`
  * Parameter: `.AspNetCore.Correlation.4S7Tu1N75AFpoOH-R5ffEksHFLeXyintN1JTNdzY_q8`
  * Attack: ``
  * Evidence: `Set-Cookie: .AspNetCore.Correlation.4S7Tu1N75AFpoOH-R5ffEksHFLeXyintN1JTNdzY_q8`
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/sign-in%3FredirectUri=%252Fcontact
  * Method: `GET`
  * Parameter: `.AspNetCore.OpenIdConnect.Nonce.CfDJ8Epa28AQ8o1JszkO6BYP-bpRu8uIYgZ0jRdnqd14hWS1TuiU8k3OBRlh-pkiyqfOYlqO5n2edTX8TxYfo1TZ0Ml-d6dYgQqBKPNNVIQMiqdeV771fbmyASSAJZojzwbePs9VhfGvBGUM42yMnpu_9WCQpOek4RMjcFGLqIHdqNC4W_kd3AFkKC9tCDzlcGY_B4z6DUmzbAP0gmSumKezaAi8YNpKbFzAWd5MfHXMxu1DXIlb8O9a6dhImsYXl-2weKpPZZm1Dfnf73VDC-TK4io`
  * Attack: ``
  * Evidence: `Set-Cookie: .AspNetCore.OpenIdConnect.Nonce.CfDJ8Epa28AQ8o1JszkO6BYP-bpRu8uIYgZ0jRdnqd14hWS1TuiU8k3OBRlh-pkiyqfOYlqO5n2edTX8TxYfo1TZ0Ml-d6dYgQqBKPNNVIQMiqdeV771fbmyASSAJZojzwbePs9VhfGvBGUM42yMnpu_9WCQpOek4RMjcFGLqIHdqNC4W_kd3AFkKC9tCDzlcGY_B4z6DUmzbAP0gmSumKezaAi8YNpKbFzAWd5MfHXMxu1DXIlb8O9a6dhImsYXl-2weKpPZZm1Dfnf73VDC-TK4io`
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/sign-in%3FredirectUri=%252Fcookies
  * Method: `GET`
  * Parameter: `.AspNetCore.Correlation.xf-4JgEOkgBqzXC3SGHe7ntjcmgIMTEH2AAOY5pGIM0`
  * Attack: ``
  * Evidence: `Set-Cookie: .AspNetCore.Correlation.xf-4JgEOkgBqzXC3SGHe7ntjcmgIMTEH2AAOY5pGIM0`
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/sign-in%3FredirectUri=%252Fcookies
  * Method: `GET`
  * Parameter: `.AspNetCore.OpenIdConnect.Nonce.CfDJ8Epa28AQ8o1JszkO6BYP-bp0D7-Y_LpM9dIxNOcZXsZilPU3dNG-HUzEe4EbqxJM-OGVjxEUFtwZwePAsc4O3ObOlnf29rtrA0TIL0oqxGB8GyHBRfgX9nalEmvJt7Zm2LRL-rAeDngsd7oVMYZtJsEO1-VtoH6UPJyWMm4jHNOsAJ5LQvOmozRvMni1PMW9UEKRuD0TLpYtWWu3FaTgL5IX5tanVEhvl0HTHKUl0T6lsr4pRY9PSxiuBUHclDnMiRTbkHXT2TpEgZC28Bh6IEg`
  * Attack: ``
  * Evidence: `Set-Cookie: .AspNetCore.OpenIdConnect.Nonce.CfDJ8Epa28AQ8o1JszkO6BYP-bp0D7-Y_LpM9dIxNOcZXsZilPU3dNG-HUzEe4EbqxJM-OGVjxEUFtwZwePAsc4O3ObOlnf29rtrA0TIL0oqxGB8GyHBRfgX9nalEmvJt7Zm2LRL-rAeDngsd7oVMYZtJsEO1-VtoH6UPJyWMm4jHNOsAJ5LQvOmozRvMni1PMW9UEKRuD0TLpYtWWu3FaTgL5IX5tanVEhvl0HTHKUl0T6lsr4pRY9PSxiuBUHclDnMiRTbkHXT2TpEgZC28Bh6IEg`
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/sign-in%3FredirectUri=%252Ferror%252F404
  * Method: `GET`
  * Parameter: `.AspNetCore.Correlation.iMbFuaYkj9sKAlVVxftBRCnHhMzF6MtrZhlLatNnCKk`
  * Attack: ``
  * Evidence: `Set-Cookie: .AspNetCore.Correlation.iMbFuaYkj9sKAlVVxftBRCnHhMzF6MtrZhlLatNnCKk`
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/sign-in%3FredirectUri=%252Ferror%252F404
  * Method: `GET`
  * Parameter: `.AspNetCore.OpenIdConnect.Nonce.CfDJ8Epa28AQ8o1JszkO6BYP-bra8z-eEevdkisRGR6w7bO24rhvQTlK-yYjvdlA8FghLDWz81HU-YKm5ApmtE6uZTYck4_O2Iz3aCOSNddTwTbEXII9kRerKpXZR5xJ-6HM8LgH3hPRxRE2lAF1uL1D_zeDuz_MliAP4xZHsvqDdSLj7YszDW4Rnmi5djXTVooL_FdT6NT0q-vbb8kfp6ai9FJB_q4BqSKcTJ9kb7KvQBvdpXUaJYUzS_OgaOEGrmwClBNTRLkv3b09RNnwBOsnSwI`
  * Attack: ``
  * Evidence: `Set-Cookie: .AspNetCore.OpenIdConnect.Nonce.CfDJ8Epa28AQ8o1JszkO6BYP-bra8z-eEevdkisRGR6w7bO24rhvQTlK-yYjvdlA8FghLDWz81HU-YKm5ApmtE6uZTYck4_O2Iz3aCOSNddTwTbEXII9kRerKpXZR5xJ-6HM8LgH3hPRxRE2lAF1uL1D_zeDuz_MliAP4xZHsvqDdSLj7YszDW4Rnmi5djXTVooL_FdT6NT0q-vbb8kfp6ai9FJB_q4BqSKcTJ9kb7KvQBvdpXUaJYUzS_OgaOEGrmwClBNTRLkv3b09RNnwBOsnSwI`
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/sign-in%3FredirectUri=%252Ffind-organisation
  * Method: `GET`
  * Parameter: `.AspNetCore.Correlation.lwQrB9GPKMvIlDtVj92AytimwksaQYDfrgnCIa_-W9c`
  * Attack: ``
  * Evidence: `Set-Cookie: .AspNetCore.Correlation.lwQrB9GPKMvIlDtVj92AytimwksaQYDfrgnCIa_-W9c`
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/sign-in%3FredirectUri=%252Ffind-organisation
  * Method: `GET`
  * Parameter: `.AspNetCore.OpenIdConnect.Nonce.CfDJ8Epa28AQ8o1JszkO6BYP-bqeLkXqHK7Mq6iz2B16k5ilroMqGiY50S47y22Q25Xl0F6kOyDhN6g9GiR9fYvznUBhbGsCPplXtxStA7-cApuLt_ixXzD17gJnW2lvMD5tv0PlLLN7_4WZOO1Ooi_fExVjH06eoais1espQwiKy4McxCoAO0Bix6tQKuVOZ08zWKRam7Zy07elbnKm3xr2SDpZkh9mvoIJx85d-_4V7LuS9OwCGsCEofrz0-CkuWs0x8CZNKSi73ZIPQYHZhHNB50`
  * Attack: ``
  * Evidence: `Set-Cookie: .AspNetCore.OpenIdConnect.Nonce.CfDJ8Epa28AQ8o1JszkO6BYP-bqeLkXqHK7Mq6iz2B16k5ilroMqGiY50S47y22Q25Xl0F6kOyDhN6g9GiR9fYvznUBhbGsCPplXtxStA7-cApuLt_ixXzD17gJnW2lvMD5tv0PlLLN7_4WZOO1Ooi_fExVjH06eoais1espQwiKy4McxCoAO0Bix6tQKuVOZ08zWKRam7Zy07elbnKm3xr2SDpZkh9mvoIJx85d-_4V7LuS9OwCGsCEofrz0-CkuWs0x8CZNKSi73ZIPQYHZhHNB50`
  * Other Info: ``

Instances: 12

### Solution

Ensure that the SameSite attribute is set to either 'lax' or ideally 'strict' for all cookies.

### Reference


* [ https://tools.ietf.org/html/draft-ietf-httpbis-cookie-same-site ](https://tools.ietf.org/html/draft-ietf-httpbis-cookie-same-site)


#### CWE Id: [ 1275 ](https://cwe.mitre.org/data/definitions/1275.html)


#### WASC Id: 13

#### Source ID: 3

### [ Cross-Domain JavaScript Source File Inclusion ](https://www.zaproxy.org/docs/alerts/10017/)



##### Low (Medium)

### Description

The page includes one or more script files from a third-party domain.

* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/
  * Method: `GET`
  * Parameter: `https://js.monitor.azure.com/scripts/b/ext/ai.clck.2.min.js`
  * Attack: ``
  * Evidence: `<script type="text/javascript" src="https://js.monitor.azure.com/scripts/b/ext/ai.clck.2.min.js"></script>`
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/accessibility
  * Method: `GET`
  * Parameter: `https://js.monitor.azure.com/scripts/b/ext/ai.clck.2.min.js`
  * Attack: ``
  * Evidence: `<script type="text/javascript" src="https://js.monitor.azure.com/scripts/b/ext/ai.clck.2.min.js"></script>`
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/contact
  * Method: `GET`
  * Parameter: `https://js.monitor.azure.com/scripts/b/ext/ai.clck.2.min.js`
  * Attack: ``
  * Evidence: `<script type="text/javascript" src="https://js.monitor.azure.com/scripts/b/ext/ai.clck.2.min.js"></script>`
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/cookies
  * Method: `GET`
  * Parameter: `https://js.monitor.azure.com/scripts/b/ext/ai.clck.2.min.js`
  * Attack: ``
  * Evidence: `<script type="text/javascript" src="https://js.monitor.azure.com/scripts/b/ext/ai.clck.2.min.js"></script>`
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/cookies%3Fcookies-saved=true
  * Method: `GET`
  * Parameter: `https://js.monitor.azure.com/scripts/b/ext/ai.clck.2.min.js`
  * Attack: ``
  * Evidence: `<script type="text/javascript" src="https://js.monitor.azure.com/scripts/b/ext/ai.clck.2.min.js"></script>`
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/find-organisation
  * Method: `GET`
  * Parameter: `https://js.monitor.azure.com/scripts/b/ext/ai.clck.2.min.js`
  * Attack: ``
  * Evidence: `<script type="text/javascript" src="https://js.monitor.azure.com/scripts/b/ext/ai.clck.2.min.js"></script>`
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/robots.txt
  * Method: `GET`
  * Parameter: `https://js.monitor.azure.com/scripts/b/ext/ai.clck.2.min.js`
  * Attack: ``
  * Evidence: `<script type="text/javascript" src="https://js.monitor.azure.com/scripts/b/ext/ai.clck.2.min.js"></script>`
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/school/135966
  * Method: `GET`
  * Parameter: `https://js.monitor.azure.com/scripts/b/ext/ai.clck.2.min.js`
  * Attack: ``
  * Evidence: `<script type="text/javascript" src="https://js.monitor.azure.com/scripts/b/ext/ai.clck.2.min.js"></script>`
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/sitemap.xml
  * Method: `GET`
  * Parameter: `https://js.monitor.azure.com/scripts/b/ext/ai.clck.2.min.js`
  * Attack: ``
  * Evidence: `<script type="text/javascript" src="https://js.monitor.azure.com/scripts/b/ext/ai.clck.2.min.js"></script>`
  * Other Info: ``

Instances: 9

### Solution

Ensure JavaScript source files are loaded from only trusted sources, and the sources can't be controlled by end users of the application.

### Reference



#### CWE Id: [ 829 ](https://cwe.mitre.org/data/definitions/829.html)


#### WASC Id: 15

#### Source ID: 3

### [ Deprecated Feature Policy Header Set ](https://www.zaproxy.org/docs/alerts/10063/)



##### Low (Medium)

### Description

The header has now been renamed to Permissions-Policy.

* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `Feature-Policy`
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/accessibility
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `Feature-Policy`
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/contact
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `Feature-Policy`
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/cookies
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `Feature-Policy`
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/cookies%3Fcookies-saved=true
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `Feature-Policy`
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/find-organisation
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `Feature-Policy`
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/js/front-end.js%3Fv=1GeIUpYa1r7xaoPXlZXIxaLMfkrs9kM5iUMQkRpAreQ
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `Feature-Policy`
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/js/govuk-frontend.min.js
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `Feature-Policy`
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/robots.txt
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `Feature-Policy`
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/school/135966
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `Feature-Policy`
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/sitemap.xml
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `Feature-Policy`
  * Other Info: ``

Instances: 11

### Solution

Ensure that your web server, application server, load balancer, etc. is configured to set the Permissions-Policy header instead of the Feature-Policy header.

### Reference


* [ https://developer.mozilla.org/en-US/docs/Web/HTTP/Headers/Permissions-Policy ](https://developer.mozilla.org/en-US/docs/Web/HTTP/Headers/Permissions-Policy)
* [ https://scotthelme.co.uk/goodbye-feature-policy-and-hello-permissions-policy/ ](https://scotthelme.co.uk/goodbye-feature-policy-and-hello-permissions-policy/)


#### CWE Id: [ 16 ](https://cwe.mitre.org/data/definitions/16.html)


#### WASC Id: 15

#### Source ID: 3

### [ Private IP Disclosure ](https://www.zaproxy.org/docs/alerts/2/)



##### Low (Medium)

### Description

A private IP (such as 10.x.x.x, 172.x.x.x, 192.168.x.x) or an Amazon EC2 private hostname (for example, ip-10-0-56-78) has been found in the HTTP response body. This information might be helpful for further attacks targeting internal systems.

* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/assets/images/favicon.svg
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `10.02.68.86`
  * Other Info: `10.02.68.86
`

Instances: 1

### Solution

Remove the private IP address from the HTTP response body. For comments, use JSP/ASP/PHP comment instead of HTML/JavaScript comment which can be seen by client browsers.

### Reference


* [ https://tools.ietf.org/html/rfc1918 ](https://tools.ietf.org/html/rfc1918)


#### CWE Id: [ 200 ](https://cwe.mitre.org/data/definitions/200.html)


#### WASC Id: 13

#### Source ID: 3

### [ Timestamp Disclosure - Unix ](https://www.zaproxy.org/docs/alerts/10096/)



##### Low (Low)

### Description

A timestamp was disclosed by the application/web server. - Unix

* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/css/main.css%3Fv=UVZegOuzG5lo6RmYvNHND4ImtgfVPUTnYytqhFE7IPk
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `1428571429`
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/js/front-end.js%3Fv=1GeIUpYa1r7xaoPXlZXIxaLMfkrs9kM5iUMQkRpAreQ
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `1433087999`
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/js/front-end.js%3Fv=1GeIUpYa1r7xaoPXlZXIxaLMfkrs9kM5iUMQkRpAreQ
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `1604231423`
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/js/front-end.js%3Fv=1GeIUpYa1r7xaoPXlZXIxaLMfkrs9kM5iUMQkRpAreQ
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `1687547391`
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/js/front-end.js%3Fv=1GeIUpYa1r7xaoPXlZXIxaLMfkrs9kM5iUMQkRpAreQ
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `1714657791`
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/js/front-end.js%3Fv=1GeIUpYa1r7xaoPXlZXIxaLMfkrs9kM5iUMQkRpAreQ
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `1724754687`
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/js/front-end.js%3Fv=1GeIUpYa1r7xaoPXlZXIxaLMfkrs9kM5iUMQkRpAreQ
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `1768516095`
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/js/front-end.js%3Fv=1GeIUpYa1r7xaoPXlZXIxaLMfkrs9kM5iUMQkRpAreQ
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `1784335871`
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/js/front-end.js%3Fv=1GeIUpYa1r7xaoPXlZXIxaLMfkrs9kM5iUMQkRpAreQ
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `1804477439`
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/js/front-end.js%3Fv=1GeIUpYa1r7xaoPXlZXIxaLMfkrs9kM5iUMQkRpAreQ
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `1887473919`
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/js/front-end.js%3Fv=1GeIUpYa1r7xaoPXlZXIxaLMfkrs9kM5iUMQkRpAreQ
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `2005441023`
  * Other Info: ``

Instances: 11

### Solution

Manually confirm that the timestamp data is not sensitive, and that the data cannot be aggregated to disclose exploitable patterns.

### Reference


* [ https://cwe.mitre.org/data/definitions/200.html ](https://cwe.mitre.org/data/definitions/200.html)


#### CWE Id: [ 200 ](https://cwe.mitre.org/data/definitions/200.html)


#### WASC Id: 13

#### Source ID: 3

### [ X-Content-Type-Options Header Missing ](https://www.zaproxy.org/docs/alerts/10021/)



##### Low (Medium)

### Description

The Anti-MIME-Sniffing header X-Content-Type-Options was not set to 'nosniff'. This allows older versions of Internet Explorer and Chrome to perform MIME-sniffing on the response body, potentially causing the response body to be interpreted and displayed as a content type other than the declared content type. Current (early 2014) and legacy versions of Firefox will use the declared content type (if one is set), rather than performing MIME-sniffing.

* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/assets/images/favicon.ico
  * Method: `GET`
  * Parameter: `x-content-type-options`
  * Attack: ``
  * Evidence: ``
  * Other Info: `This issue still applies to error type pages (401, 403, 500, etc.) as those pages are often still affected by injection issues, in which case there is still concern for browsers sniffing pages away from their actual content type.
At "High" threshold this scan rule will not alert on client or server error responses.`
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/assets/images/favicon.svg
  * Method: `GET`
  * Parameter: `x-content-type-options`
  * Attack: ``
  * Evidence: ``
  * Other Info: `This issue still applies to error type pages (401, 403, 500, etc.) as those pages are often still affected by injection issues, in which case there is still concern for browsers sniffing pages away from their actual content type.
At "High" threshold this scan rule will not alert on client or server error responses.`
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/assets/images/govuk-icon-180.png
  * Method: `GET`
  * Parameter: `x-content-type-options`
  * Attack: ``
  * Evidence: ``
  * Other Info: `This issue still applies to error type pages (401, 403, 500, etc.) as those pages are often still affected by injection issues, in which case there is still concern for browsers sniffing pages away from their actual content type.
At "High" threshold this scan rule will not alert on client or server error responses.`
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/assets/images/govuk-icon-mask.svg
  * Method: `GET`
  * Parameter: `x-content-type-options`
  * Attack: ``
  * Evidence: ``
  * Other Info: `This issue still applies to error type pages (401, 403, 500, etc.) as those pages are often still affected by injection issues, in which case there is still concern for browsers sniffing pages away from their actual content type.
At "High" threshold this scan rule will not alert on client or server error responses.`
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/assets/manifest.json
  * Method: `GET`
  * Parameter: `x-content-type-options`
  * Attack: ``
  * Evidence: ``
  * Other Info: `This issue still applies to error type pages (401, 403, 500, etc.) as those pages are often still affected by injection issues, in which case there is still concern for browsers sniffing pages away from their actual content type.
At "High" threshold this scan rule will not alert on client or server error responses.`
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/css/front-end.css%3Fv=Fk9S3ib-0VugYDs4FpUNvvAm6JE5yD-W2AFg4drw694
  * Method: `GET`
  * Parameter: `x-content-type-options`
  * Attack: ``
  * Evidence: ``
  * Other Info: `This issue still applies to error type pages (401, 403, 500, etc.) as those pages are often still affected by injection issues, in which case there is still concern for browsers sniffing pages away from their actual content type.
At "High" threshold this scan rule will not alert on client or server error responses.`
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/css/main.css%3Fv=UVZegOuzG5lo6RmYvNHND4ImtgfVPUTnYytqhFE7IPk
  * Method: `GET`
  * Parameter: `x-content-type-options`
  * Attack: ``
  * Evidence: ``
  * Other Info: `This issue still applies to error type pages (401, 403, 500, etc.) as those pages are often still affected by injection issues, in which case there is still concern for browsers sniffing pages away from their actual content type.
At "High" threshold this scan rule will not alert on client or server error responses.`
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/js/front-end.js%3Fv=1GeIUpYa1r7xaoPXlZXIxaLMfkrs9kM5iUMQkRpAreQ
  * Method: `GET`
  * Parameter: `x-content-type-options`
  * Attack: ``
  * Evidence: ``
  * Other Info: `This issue still applies to error type pages (401, 403, 500, etc.) as those pages are often still affected by injection issues, in which case there is still concern for browsers sniffing pages away from their actual content type.
At "High" threshold this scan rule will not alert on client or server error responses.`
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/js/govuk-frontend.min.js
  * Method: `GET`
  * Parameter: `x-content-type-options`
  * Attack: ``
  * Evidence: ``
  * Other Info: `This issue still applies to error type pages (401, 403, 500, etc.) as those pages are often still affected by injection issues, in which case there is still concern for browsers sniffing pages away from their actual content type.
At "High" threshold this scan rule will not alert on client or server error responses.`

Instances: 9

### Solution

Ensure that the application/web server sets the Content-Type header appropriately, and that it sets the X-Content-Type-Options header to 'nosniff' for all web pages.
If possible, ensure that the end user uses a standards-compliant and modern web browser that does not perform MIME-sniffing at all, or that can be directed by the web application/web server to not perform MIME-sniffing.

### Reference


* [ https://learn.microsoft.com/en-us/previous-versions/windows/internet-explorer/ie-developer/compatibility/gg622941(v=vs.85) ](https://learn.microsoft.com/en-us/previous-versions/windows/internet-explorer/ie-developer/compatibility/gg622941(v=vs.85))
* [ https://owasp.org/www-community/Security_Headers ](https://owasp.org/www-community/Security_Headers)


#### CWE Id: [ 693 ](https://cwe.mitre.org/data/definitions/693.html)


#### WASC Id: 15

#### Source ID: 3

### [ Cookie Slack Detector ](https://www.zaproxy.org/docs/alerts/90027/)



##### Informational (Low)

### Description

Repeated GET requests: drop a different cookie each time, followed by normal request with all cookies to stabilize session, compare responses against original baseline GET. This can reveal areas where cookie based authentication/attributes are not actually enforced.

* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/assets/images/favicon.svg
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: ``
  * Other Info: `Cookies that don't have expected effects can reveal flaws in application logic. In the worst case, this can reveal where authentication via cookie token(s) is not actually enforced.
These cookies affected the response: 
These cookies did NOT affect the response: .AspNetCore.Antiforgery.cdV5uW_Ejgc
`
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/cookies
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: ``
  * Other Info: `Dropping this cookie appears to have invalidated the session: [.AspNetCore.Mvc.CookieTempDataProvider] A follow-on request with all original cookies still had a different response than the original request.
`
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/cookies%3Fcookies-saved=true
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: ``
  * Other Info: `Cookies that don't have expected effects can reveal flaws in application logic. In the worst case, this can reveal where authentication via cookie token(s) is not actually enforced.
These cookies affected the response: cookie_policy
These cookies did NOT affect the response: .AspNetCore.Antiforgery.cdV5uW_Ejgc,.AspNetCore.Mvc.CookieTempDataProvider
`
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/js/front-end.js%3Fv=1GeIUpYa1r7xaoPXlZXIxaLMfkrs9kM5iUMQkRpAreQ
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: ``
  * Other Info: `Cookies that don't have expected effects can reveal flaws in application logic. In the worst case, this can reveal where authentication via cookie token(s) is not actually enforced.
These cookies affected the response: 
These cookies did NOT affect the response: .AspNetCore.Antiforgery.cdV5uW_Ejgc
`
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/js/govuk-frontend.min.js
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: ``
  * Other Info: `Cookies that don't have expected effects can reveal flaws in application logic. In the worst case, this can reveal where authentication via cookie token(s) is not actually enforced.
These cookies affected the response: 
These cookies did NOT affect the response: .AspNetCore.Antiforgery.cdV5uW_Ejgc
`
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/sign-in%3FredirectUri=%252F
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: ``
  * Other Info: `Cookies that don't have expected effects can reveal flaws in application logic. In the worst case, this can reveal where authentication via cookie token(s) is not actually enforced.
These cookies affected the response: 
These cookies did NOT affect the response: .AspNetCore.Correlation.9tGXT24RoZ27bsvS--X-xjmpSiJSo3-SauJrCO4sT10,.AspNetCore.OpenIdConnect.Nonce.CfDJ8Epa28AQ8o1JszkO6BYP-bqS4mcOJXgNsU7e4Ki-K3PqLlZv4wyJsEQg0IFUYuJRt_flNCyqhNjXEQCaIHplMLlA80rObAKbIn7O_I5CZdx_aqJNzhQBfoP6QmJ4dMZ_ApW-7Nuykyu4BylTpH8eLdb9-wjtx16oZ0xFySYO4XImgG3mobHOMXAp6FdTdaw8J1RMg2p1C1Im6AeeVXQdMh1xE8brol7nXCAMCqoqU99JNmCOZbdvD07J6KlVh3ubne2DFJW5YAmKNwFspv5DcDI
`
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/cookies
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: ``
  * Other Info: `Cookies that don't have expected effects can reveal flaws in application logic. In the worst case, this can reveal where authentication via cookie token(s) is not actually enforced.
These cookies affected the response: 
These cookies did NOT affect the response: .AspNetCore.Antiforgery.cdV5uW_Ejgc,cookie_policy
`

Instances: 7

### Solution



### Reference


* [ https://cwe.mitre.org/data/definitions/205.html ](https://cwe.mitre.org/data/definitions/205.html)


#### CWE Id: [ 205 ](https://cwe.mitre.org/data/definitions/205.html)


#### WASC Id: 45

#### Source ID: 1

### [ Information Disclosure - Suspicious Comments ](https://www.zaproxy.org/docs/alerts/10027/)



##### Informational (Low)

### Description

The response appears to contain suspicious comments which may help an attacker. Note: Matches made within script blocks or files are against the entire content not only comments.

* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/contact
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `from`
  * Other Info: `The following pattern was used: \bFROM\b and was detected in the element starting with: "<script type="module" nonce="CSC9QseIv6iaES/L">
        import { initAll } from '/js/govuk-frontend.min.js'
        initAll()
  ", see evidence field for the suspicious comment/snippet.`
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/js/front-end.js%3Fv=1GeIUpYa1r7xaoPXlZXIxaLMfkrs9kM5iUMQkRpAreQ
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `DB`
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/js/front-end.js%3Fv=1GeIUpYa1r7xaoPXlZXIxaLMfkrs9kM5iUMQkRpAreQ
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `debug`
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/js/front-end.js%3Fv=1GeIUpYa1r7xaoPXlZXIxaLMfkrs9kM5iUMQkRpAreQ
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `from`
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/js/front-end.js%3Fv=1GeIUpYa1r7xaoPXlZXIxaLMfkrs9kM5iUMQkRpAreQ
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `query`
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/js/front-end.js%3Fv=1GeIUpYa1r7xaoPXlZXIxaLMfkrs9kM5iUMQkRpAreQ
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `select`
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/js/front-end.js%3Fv=1GeIUpYa1r7xaoPXlZXIxaLMfkrs9kM5iUMQkRpAreQ
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `TODO`
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/js/front-end.js%3Fv=1GeIUpYa1r7xaoPXlZXIxaLMfkrs9kM5iUMQkRpAreQ
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `user`
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/js/front-end.js%3Fv=1GeIUpYa1r7xaoPXlZXIxaLMfkrs9kM5iUMQkRpAreQ
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `Where`
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/js/front-end.js%3Fv=1GeIUpYa1r7xaoPXlZXIxaLMfkrs9kM5iUMQkRpAreQ
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `XXX`
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/js/govuk-frontend.min.js
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `select`
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/school/135966
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `from`
  * Other Info: `The following pattern was used: \bFROM\b and was detected in the element starting with: "<script type="module" nonce="ZH9IJvWjBvIabi/O">
      import { initAll } from '/js/govuk-frontend.min.js'
      initAll()
    </", see evidence field for the suspicious comment/snippet.`

Instances: 12

### Solution

Remove all comments that return information that may help an attacker and fix any underlying problems they refer to.

### Reference



#### CWE Id: [ 200 ](https://cwe.mitre.org/data/definitions/200.html)


#### WASC Id: 13

#### Source ID: 3

### [ Modern Web Application ](https://www.zaproxy.org/docs/alerts/10109/)



##### Informational (Medium)

### Description

The application appears to be a modern web application. If you need to explore it automatically then the Ajax Spider may well be more effective than the standard one.

* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/find-organisation
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `<noscript>
    <div class="govuk-warning-text">
        <span class="govuk-warning-text__icon" aria-hidden="true">!</span>
        <strong class="govuk-warning-text__text">
            <span class="govuk-visually-hidden">Warning</span>
            Your browser does not meet the minimum requirements to use this service.
        </strong>
    </div>
    <ul class="govuk-list govuk-list--bullet">
        <li>
            Please ensure that JavaScript is enabled.
        </li>
    </ul>
</noscript>`
  * Other Info: ``

Instances: 1

### Solution

This is an informational alert and so no changes are required.

### Reference




#### Source ID: 3

### [ Non-Storable Content ](https://www.zaproxy.org/docs/alerts/10049/)



##### Informational (Medium)

### Description

The response contents are not storable by caching components such as proxy servers. If the response does not contain sensitive, personal or user-specific information, it may benefit from being stored and cached, to improve performance.

* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/sign-in%3FredirectUri=%252F
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `302`
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/sign-in%3FredirectUri=%252Ferror%252F404
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `302`
  * Other Info: ``

Instances: 2

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

* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/
  * Method: `GET`
  * Parameter: `cache-control`
  * Attack: ``
  * Evidence: ``
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/accessibility
  * Method: `GET`
  * Parameter: `cache-control`
  * Attack: ``
  * Evidence: ``
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/assets/manifest.json
  * Method: `GET`
  * Parameter: `cache-control`
  * Attack: ``
  * Evidence: `public,max-age=600`
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/contact
  * Method: `GET`
  * Parameter: `cache-control`
  * Attack: ``
  * Evidence: ``
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/cookies
  * Method: `GET`
  * Parameter: `cache-control`
  * Attack: ``
  * Evidence: `no-cache, no-store`
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/cookies%3Fcookies-saved=true
  * Method: `GET`
  * Parameter: `cache-control`
  * Attack: ``
  * Evidence: `no-cache, no-store`
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/find-organisation
  * Method: `GET`
  * Parameter: `cache-control`
  * Attack: ``
  * Evidence: ``
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/school/135966
  * Method: `GET`
  * Parameter: `cache-control`
  * Attack: ``
  * Evidence: ``
  * Other Info: ``

Instances: 8

### Solution

For secure content, ensure the cache-control HTTP header is set with "no-cache, no-store, must-revalidate". If an asset should be cached consider setting the directives "public, max-age, immutable".

### Reference


* [ https://cheatsheetseries.owasp.org/cheatsheets/Session_Management_Cheat_Sheet.html#web-content-caching ](https://cheatsheetseries.owasp.org/cheatsheets/Session_Management_Cheat_Sheet.html#web-content-caching)
* [ https://developer.mozilla.org/en-US/docs/Web/HTTP/Headers/Cache-Control ](https://developer.mozilla.org/en-US/docs/Web/HTTP/Headers/Cache-Control)
* [ https://grayduck.mn/2021/09/13/cache-control-recommendations/ ](https://grayduck.mn/2021/09/13/cache-control-recommendations/)


#### CWE Id: [ 525 ](https://cwe.mitre.org/data/definitions/525.html)


#### WASC Id: 13

#### Source ID: 3

### [ Session Management Response Identified ](https://www.zaproxy.org/docs/alerts/10112/)



##### Informational (Medium)

### Description

The given response has been identified as containing a session management token. The 'Other Info' field contains a set of header tokens that can be used in the Header Based Session Management Method. If the request is in a context which has a Session Management Method set to "Auto-Detect" then this rule will change the session management to use the tokens identified.

* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/cookies
  * Method: `GET`
  * Parameter: `.AspNetCore.Antiforgery.cdV5uW_Ejgc`
  * Attack: ``
  * Evidence: `CfDJ8Epa28AQ8o1JszkO6BYP-brdBALJVD_0pbc-M7sT0HlFOXXkb9MY6SY6oJcOVkGAQv5MNPwTVAKf7Yd_rvEopXtXlZZcrVAFEvmnLkQtLrSJCiixrhgfyzhjCiNAdBpU0ZM0PCeyPeWdTCxvylQWpho`
  * Other Info: `
cookie:.AspNetCore.Antiforgery.cdV5uW_Ejgc`

Instances: 1

### Solution

This is an informational alert rather than a vulnerability and so there is nothing to fix.

### Reference


* [ https://www.zaproxy.org/docs/desktop/addons/authentication-helper/session-mgmt-id ](https://www.zaproxy.org/docs/desktop/addons/authentication-helper/session-mgmt-id)



#### Source ID: 3

### [ Storable and Cacheable Content ](https://www.zaproxy.org/docs/alerts/10049/)



##### Informational (Medium)

### Description

The response contents are storable by caching components such as proxy servers, and may be retrieved directly from the cache, rather than from the origin server by the caching servers, in response to similar requests from other users. If the response data is sensitive, personal or user-specific, this may result in sensitive information being leaked. In some cases, this may even result in a user gaining complete control of the session of another user, depending on the configuration of the caching components in use in their environment. This is primarily an issue where "shared" caching servers such as "proxy" caches are configured on the local network. This configuration is typically found in corporate or educational environments, for instance.

* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: ``
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/accessibility
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: ``
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/assets/images/favicon.ico
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `max-age=600`
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/assets/images/govuk-icon-180.png
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `max-age=600`
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/assets/images/govuk-icon-mask.svg
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `max-age=600`
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/assets/manifest.json
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `max-age=600`
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/contact
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: ``
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/find-organisation
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: ``
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/robots.txt
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: ``
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/school/135966
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: ``
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/sitemap.xml
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: ``
  * Other Info: ``

Instances: 11

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

* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1)`
  * Evidence: ``
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 6.0)`
  * Evidence: ``
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 6.1)`
  * Evidence: ``
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Trident/7.0; rv:11.0) like Gecko`
  * Evidence: ``
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/75.0.3739.0 Safari/537.36 Edg/75.0.109.0`
  * Evidence: ``
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.124 Safari/537.36`
  * Evidence: ``
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:93.0) Gecko/20100101 Firefox/91.0`
  * Evidence: ``
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (compatible; Googlebot/2.1; +http://www.google.com/bot.html)`
  * Evidence: ``
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (compatible; Yahoo! Slurp; http://help.yahoo.com/help/us/ysearch/slurp)`
  * Evidence: ``
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (iPhone; CPU iPhone OS 8_0_2 like Mac OS X) AppleWebKit/600.1.4 (KHTML, like Gecko) Version/8.0 Mobile/12A366 Safari/600.1.4`
  * Evidence: ``
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (iPhone; U; CPU iPhone OS 3_0 like Mac OS X; en-us) AppleWebKit/528.18 (KHTML, like Gecko) Version/4.0 Mobile/7A341 Safari/528.16`
  * Evidence: ``
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `msnbot/1.1 (+http://search.msn.com/msnbot.htm)`
  * Evidence: ``
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1)`
  * Evidence: ``
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 6.0)`
  * Evidence: ``
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 6.1)`
  * Evidence: ``
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Trident/7.0; rv:11.0) like Gecko`
  * Evidence: ``
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/75.0.3739.0 Safari/537.36 Edg/75.0.109.0`
  * Evidence: ``
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.124 Safari/537.36`
  * Evidence: ``
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:93.0) Gecko/20100101 Firefox/91.0`
  * Evidence: ``
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (compatible; Googlebot/2.1; +http://www.google.com/bot.html)`
  * Evidence: ``
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (compatible; Yahoo! Slurp; http://help.yahoo.com/help/us/ysearch/slurp)`
  * Evidence: ``
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (iPhone; CPU iPhone OS 8_0_2 like Mac OS X) AppleWebKit/600.1.4 (KHTML, like Gecko) Version/8.0 Mobile/12A366 Safari/600.1.4`
  * Evidence: ``
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (iPhone; U; CPU iPhone OS 3_0 like Mac OS X; en-us) AppleWebKit/528.18 (KHTML, like Gecko) Version/4.0 Mobile/7A341 Safari/528.16`
  * Evidence: ``
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `msnbot/1.1 (+http://search.msn.com/msnbot.htm)`
  * Evidence: ``
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/accessibility
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1)`
  * Evidence: ``
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/accessibility
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 6.0)`
  * Evidence: ``
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/accessibility
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 6.1)`
  * Evidence: ``
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/accessibility
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Trident/7.0; rv:11.0) like Gecko`
  * Evidence: ``
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/accessibility
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/75.0.3739.0 Safari/537.36 Edg/75.0.109.0`
  * Evidence: ``
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/accessibility
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.124 Safari/537.36`
  * Evidence: ``
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/accessibility
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:93.0) Gecko/20100101 Firefox/91.0`
  * Evidence: ``
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/accessibility
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (compatible; Googlebot/2.1; +http://www.google.com/bot.html)`
  * Evidence: ``
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/accessibility
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (compatible; Yahoo! Slurp; http://help.yahoo.com/help/us/ysearch/slurp)`
  * Evidence: ``
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/accessibility
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (iPhone; CPU iPhone OS 8_0_2 like Mac OS X) AppleWebKit/600.1.4 (KHTML, like Gecko) Version/8.0 Mobile/12A366 Safari/600.1.4`
  * Evidence: ``
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/accessibility
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (iPhone; U; CPU iPhone OS 3_0 like Mac OS X; en-us) AppleWebKit/528.18 (KHTML, like Gecko) Version/4.0 Mobile/7A341 Safari/528.16`
  * Evidence: ``
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/accessibility
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `msnbot/1.1 (+http://search.msn.com/msnbot.htm)`
  * Evidence: ``
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/assets
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1)`
  * Evidence: ``
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/assets
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 6.0)`
  * Evidence: ``
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/assets
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 6.1)`
  * Evidence: ``
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/assets
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Trident/7.0; rv:11.0) like Gecko`
  * Evidence: ``
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/assets
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/75.0.3739.0 Safari/537.36 Edg/75.0.109.0`
  * Evidence: ``
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/assets
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.124 Safari/537.36`
  * Evidence: ``
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/assets
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:93.0) Gecko/20100101 Firefox/91.0`
  * Evidence: ``
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/assets
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (compatible; Googlebot/2.1; +http://www.google.com/bot.html)`
  * Evidence: ``
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/assets
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (compatible; Yahoo! Slurp; http://help.yahoo.com/help/us/ysearch/slurp)`
  * Evidence: ``
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/assets
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (iPhone; CPU iPhone OS 8_0_2 like Mac OS X) AppleWebKit/600.1.4 (KHTML, like Gecko) Version/8.0 Mobile/12A366 Safari/600.1.4`
  * Evidence: ``
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/assets
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (iPhone; U; CPU iPhone OS 3_0 like Mac OS X; en-us) AppleWebKit/528.18 (KHTML, like Gecko) Version/4.0 Mobile/7A341 Safari/528.16`
  * Evidence: ``
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/assets
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `msnbot/1.1 (+http://search.msn.com/msnbot.htm)`
  * Evidence: ``
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/assets/images
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1)`
  * Evidence: ``
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/assets/images
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 6.0)`
  * Evidence: ``
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/assets/images
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 6.1)`
  * Evidence: ``
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/assets/images
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Trident/7.0; rv:11.0) like Gecko`
  * Evidence: ``
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/assets/images
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/75.0.3739.0 Safari/537.36 Edg/75.0.109.0`
  * Evidence: ``
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/assets/images
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.124 Safari/537.36`
  * Evidence: ``
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/assets/images
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:93.0) Gecko/20100101 Firefox/91.0`
  * Evidence: ``
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/assets/images
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (compatible; Googlebot/2.1; +http://www.google.com/bot.html)`
  * Evidence: ``
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/assets/images
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (compatible; Yahoo! Slurp; http://help.yahoo.com/help/us/ysearch/slurp)`
  * Evidence: ``
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/assets/images
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (iPhone; CPU iPhone OS 8_0_2 like Mac OS X) AppleWebKit/600.1.4 (KHTML, like Gecko) Version/8.0 Mobile/12A366 Safari/600.1.4`
  * Evidence: ``
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/assets/images
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (iPhone; U; CPU iPhone OS 3_0 like Mac OS X; en-us) AppleWebKit/528.18 (KHTML, like Gecko) Version/4.0 Mobile/7A341 Safari/528.16`
  * Evidence: ``
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/assets/images
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `msnbot/1.1 (+http://search.msn.com/msnbot.htm)`
  * Evidence: ``
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/contact
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1)`
  * Evidence: ``
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/contact
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 6.0)`
  * Evidence: ``
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/contact
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 6.1)`
  * Evidence: ``
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/contact
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Trident/7.0; rv:11.0) like Gecko`
  * Evidence: ``
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/contact
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/75.0.3739.0 Safari/537.36 Edg/75.0.109.0`
  * Evidence: ``
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/contact
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.124 Safari/537.36`
  * Evidence: ``
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/contact
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:93.0) Gecko/20100101 Firefox/91.0`
  * Evidence: ``
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/contact
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (compatible; Googlebot/2.1; +http://www.google.com/bot.html)`
  * Evidence: ``
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/contact
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (compatible; Yahoo! Slurp; http://help.yahoo.com/help/us/ysearch/slurp)`
  * Evidence: ``
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/contact
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (iPhone; CPU iPhone OS 8_0_2 like Mac OS X) AppleWebKit/600.1.4 (KHTML, like Gecko) Version/8.0 Mobile/12A366 Safari/600.1.4`
  * Evidence: ``
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/contact
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (iPhone; U; CPU iPhone OS 3_0 like Mac OS X; en-us) AppleWebKit/528.18 (KHTML, like Gecko) Version/4.0 Mobile/7A341 Safari/528.16`
  * Evidence: ``
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/contact
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `msnbot/1.1 (+http://search.msn.com/msnbot.htm)`
  * Evidence: ``
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/cookies
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1)`
  * Evidence: ``
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/cookies
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 6.0)`
  * Evidence: ``
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/cookies
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 6.1)`
  * Evidence: ``
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/cookies
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Trident/7.0; rv:11.0) like Gecko`
  * Evidence: ``
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/cookies
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/75.0.3739.0 Safari/537.36 Edg/75.0.109.0`
  * Evidence: ``
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/cookies
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.124 Safari/537.36`
  * Evidence: ``
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/cookies
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:93.0) Gecko/20100101 Firefox/91.0`
  * Evidence: ``
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/cookies
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (compatible; Googlebot/2.1; +http://www.google.com/bot.html)`
  * Evidence: ``
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/cookies
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (compatible; Yahoo! Slurp; http://help.yahoo.com/help/us/ysearch/slurp)`
  * Evidence: ``
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/cookies
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (iPhone; CPU iPhone OS 8_0_2 like Mac OS X) AppleWebKit/600.1.4 (KHTML, like Gecko) Version/8.0 Mobile/12A366 Safari/600.1.4`
  * Evidence: ``
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/cookies
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (iPhone; U; CPU iPhone OS 3_0 like Mac OS X; en-us) AppleWebKit/528.18 (KHTML, like Gecko) Version/4.0 Mobile/7A341 Safari/528.16`
  * Evidence: ``
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/cookies
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `msnbot/1.1 (+http://search.msn.com/msnbot.htm)`
  * Evidence: ``
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/cookies%3Fcookies-saved=true
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1)`
  * Evidence: ``
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/cookies%3Fcookies-saved=true
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 6.0)`
  * Evidence: ``
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/cookies%3Fcookies-saved=true
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 6.1)`
  * Evidence: ``
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/cookies%3Fcookies-saved=true
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Trident/7.0; rv:11.0) like Gecko`
  * Evidence: ``
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/cookies%3Fcookies-saved=true
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/75.0.3739.0 Safari/537.36 Edg/75.0.109.0`
  * Evidence: ``
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/cookies%3Fcookies-saved=true
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.124 Safari/537.36`
  * Evidence: ``
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/cookies%3Fcookies-saved=true
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:93.0) Gecko/20100101 Firefox/91.0`
  * Evidence: ``
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/cookies%3Fcookies-saved=true
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (compatible; Googlebot/2.1; +http://www.google.com/bot.html)`
  * Evidence: ``
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/cookies%3Fcookies-saved=true
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (compatible; Yahoo! Slurp; http://help.yahoo.com/help/us/ysearch/slurp)`
  * Evidence: ``
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/cookies%3Fcookies-saved=true
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (iPhone; CPU iPhone OS 8_0_2 like Mac OS X) AppleWebKit/600.1.4 (KHTML, like Gecko) Version/8.0 Mobile/12A366 Safari/600.1.4`
  * Evidence: ``
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/cookies%3Fcookies-saved=true
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (iPhone; U; CPU iPhone OS 3_0 like Mac OS X; en-us) AppleWebKit/528.18 (KHTML, like Gecko) Version/4.0 Mobile/7A341 Safari/528.16`
  * Evidence: ``
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/cookies%3Fcookies-saved=true
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `msnbot/1.1 (+http://search.msn.com/msnbot.htm)`
  * Evidence: ``
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/css
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1)`
  * Evidence: ``
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/css
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 6.0)`
  * Evidence: ``
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/css
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 6.1)`
  * Evidence: ``
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/css
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Trident/7.0; rv:11.0) like Gecko`
  * Evidence: ``
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/css
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/75.0.3739.0 Safari/537.36 Edg/75.0.109.0`
  * Evidence: ``
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/css
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.124 Safari/537.36`
  * Evidence: ``
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/css
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:93.0) Gecko/20100101 Firefox/91.0`
  * Evidence: ``
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/css
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (compatible; Googlebot/2.1; +http://www.google.com/bot.html)`
  * Evidence: ``
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/css
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (compatible; Yahoo! Slurp; http://help.yahoo.com/help/us/ysearch/slurp)`
  * Evidence: ``
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/css
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (iPhone; CPU iPhone OS 8_0_2 like Mac OS X) AppleWebKit/600.1.4 (KHTML, like Gecko) Version/8.0 Mobile/12A366 Safari/600.1.4`
  * Evidence: ``
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/css
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (iPhone; U; CPU iPhone OS 3_0 like Mac OS X; en-us) AppleWebKit/528.18 (KHTML, like Gecko) Version/4.0 Mobile/7A341 Safari/528.16`
  * Evidence: ``
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/css
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `msnbot/1.1 (+http://search.msn.com/msnbot.htm)`
  * Evidence: ``
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/find-organisation
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1)`
  * Evidence: ``
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/find-organisation
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 6.0)`
  * Evidence: ``
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/find-organisation
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 6.1)`
  * Evidence: ``
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/find-organisation
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Trident/7.0; rv:11.0) like Gecko`
  * Evidence: ``
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/find-organisation
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/75.0.3739.0 Safari/537.36 Edg/75.0.109.0`
  * Evidence: ``
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/find-organisation
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.124 Safari/537.36`
  * Evidence: ``
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/find-organisation
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:93.0) Gecko/20100101 Firefox/91.0`
  * Evidence: ``
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/find-organisation
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (compatible; Googlebot/2.1; +http://www.google.com/bot.html)`
  * Evidence: ``
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/find-organisation
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (compatible; Yahoo! Slurp; http://help.yahoo.com/help/us/ysearch/slurp)`
  * Evidence: ``
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/find-organisation
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (iPhone; CPU iPhone OS 8_0_2 like Mac OS X) AppleWebKit/600.1.4 (KHTML, like Gecko) Version/8.0 Mobile/12A366 Safari/600.1.4`
  * Evidence: ``
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/find-organisation
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (iPhone; U; CPU iPhone OS 3_0 like Mac OS X; en-us) AppleWebKit/528.18 (KHTML, like Gecko) Version/4.0 Mobile/7A341 Safari/528.16`
  * Evidence: ``
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/find-organisation
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `msnbot/1.1 (+http://search.msn.com/msnbot.htm)`
  * Evidence: ``
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/js
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1)`
  * Evidence: ``
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/js
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 6.0)`
  * Evidence: ``
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/js
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 6.1)`
  * Evidence: ``
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/js
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Trident/7.0; rv:11.0) like Gecko`
  * Evidence: ``
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/js
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/75.0.3739.0 Safari/537.36 Edg/75.0.109.0`
  * Evidence: ``
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/js
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.124 Safari/537.36`
  * Evidence: ``
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/js
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:93.0) Gecko/20100101 Firefox/91.0`
  * Evidence: ``
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/js
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (compatible; Googlebot/2.1; +http://www.google.com/bot.html)`
  * Evidence: ``
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/js
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (compatible; Yahoo! Slurp; http://help.yahoo.com/help/us/ysearch/slurp)`
  * Evidence: ``
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/js
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (iPhone; CPU iPhone OS 8_0_2 like Mac OS X) AppleWebKit/600.1.4 (KHTML, like Gecko) Version/8.0 Mobile/12A366 Safari/600.1.4`
  * Evidence: ``
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/js
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (iPhone; U; CPU iPhone OS 3_0 like Mac OS X; en-us) AppleWebKit/528.18 (KHTML, like Gecko) Version/4.0 Mobile/7A341 Safari/528.16`
  * Evidence: ``
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/js
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `msnbot/1.1 (+http://search.msn.com/msnbot.htm)`
  * Evidence: ``
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/robots.txt
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1)`
  * Evidence: ``
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/robots.txt
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 6.0)`
  * Evidence: ``
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/robots.txt
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 6.1)`
  * Evidence: ``
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/robots.txt
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Trident/7.0; rv:11.0) like Gecko`
  * Evidence: ``
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/robots.txt
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/75.0.3739.0 Safari/537.36 Edg/75.0.109.0`
  * Evidence: ``
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/robots.txt
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.124 Safari/537.36`
  * Evidence: ``
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/robots.txt
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:93.0) Gecko/20100101 Firefox/91.0`
  * Evidence: ``
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/robots.txt
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (compatible; Googlebot/2.1; +http://www.google.com/bot.html)`
  * Evidence: ``
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/robots.txt
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (compatible; Yahoo! Slurp; http://help.yahoo.com/help/us/ysearch/slurp)`
  * Evidence: ``
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/robots.txt
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (iPhone; CPU iPhone OS 8_0_2 like Mac OS X) AppleWebKit/600.1.4 (KHTML, like Gecko) Version/8.0 Mobile/12A366 Safari/600.1.4`
  * Evidence: ``
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/robots.txt
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (iPhone; U; CPU iPhone OS 3_0 like Mac OS X; en-us) AppleWebKit/528.18 (KHTML, like Gecko) Version/4.0 Mobile/7A341 Safari/528.16`
  * Evidence: ``
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/robots.txt
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `msnbot/1.1 (+http://search.msn.com/msnbot.htm)`
  * Evidence: ``
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/school
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1)`
  * Evidence: ``
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/school
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 6.0)`
  * Evidence: ``
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/school
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 6.1)`
  * Evidence: ``
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/school
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Trident/7.0; rv:11.0) like Gecko`
  * Evidence: ``
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/school
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/75.0.3739.0 Safari/537.36 Edg/75.0.109.0`
  * Evidence: ``
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/school
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.124 Safari/537.36`
  * Evidence: ``
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/school
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:93.0) Gecko/20100101 Firefox/91.0`
  * Evidence: ``
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/school
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (compatible; Googlebot/2.1; +http://www.google.com/bot.html)`
  * Evidence: ``
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/school
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (compatible; Yahoo! Slurp; http://help.yahoo.com/help/us/ysearch/slurp)`
  * Evidence: ``
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/school
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (iPhone; CPU iPhone OS 8_0_2 like Mac OS X) AppleWebKit/600.1.4 (KHTML, like Gecko) Version/8.0 Mobile/12A366 Safari/600.1.4`
  * Evidence: ``
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/school
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (iPhone; U; CPU iPhone OS 3_0 like Mac OS X; en-us) AppleWebKit/528.18 (KHTML, like Gecko) Version/4.0 Mobile/7A341 Safari/528.16`
  * Evidence: ``
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/school
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `msnbot/1.1 (+http://search.msn.com/msnbot.htm)`
  * Evidence: ``
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/school/135966
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1)`
  * Evidence: ``
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/school/135966
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 6.0)`
  * Evidence: ``
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/school/135966
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 6.1)`
  * Evidence: ``
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/school/135966
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Trident/7.0; rv:11.0) like Gecko`
  * Evidence: ``
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/school/135966
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/75.0.3739.0 Safari/537.36 Edg/75.0.109.0`
  * Evidence: ``
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/school/135966
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.124 Safari/537.36`
  * Evidence: ``
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/school/135966
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:93.0) Gecko/20100101 Firefox/91.0`
  * Evidence: ``
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/school/135966
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (compatible; Googlebot/2.1; +http://www.google.com/bot.html)`
  * Evidence: ``
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/school/135966
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (compatible; Yahoo! Slurp; http://help.yahoo.com/help/us/ysearch/slurp)`
  * Evidence: ``
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/school/135966
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (iPhone; CPU iPhone OS 8_0_2 like Mac OS X) AppleWebKit/600.1.4 (KHTML, like Gecko) Version/8.0 Mobile/12A366 Safari/600.1.4`
  * Evidence: ``
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/school/135966
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (iPhone; U; CPU iPhone OS 3_0 like Mac OS X; en-us) AppleWebKit/528.18 (KHTML, like Gecko) Version/4.0 Mobile/7A341 Safari/528.16`
  * Evidence: ``
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/school/135966
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `msnbot/1.1 (+http://search.msn.com/msnbot.htm)`
  * Evidence: ``
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/sign-in%3FredirectUri=%252F
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (compatible; Googlebot/2.1; +http://www.google.com/bot.html)`
  * Evidence: ``
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/sign-in%3FredirectUri=%252F
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `msnbot/1.1 (+http://search.msn.com/msnbot.htm)`
  * Evidence: ``
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/sitemap.xml
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1)`
  * Evidence: ``
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/sitemap.xml
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 6.0)`
  * Evidence: ``
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/sitemap.xml
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 6.1)`
  * Evidence: ``
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/sitemap.xml
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Trident/7.0; rv:11.0) like Gecko`
  * Evidence: ``
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/sitemap.xml
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/75.0.3739.0 Safari/537.36 Edg/75.0.109.0`
  * Evidence: ``
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/sitemap.xml
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.124 Safari/537.36`
  * Evidence: ``
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/sitemap.xml
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:93.0) Gecko/20100101 Firefox/91.0`
  * Evidence: ``
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/sitemap.xml
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (compatible; Googlebot/2.1; +http://www.google.com/bot.html)`
  * Evidence: ``
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/sitemap.xml
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (compatible; Yahoo! Slurp; http://help.yahoo.com/help/us/ysearch/slurp)`
  * Evidence: ``
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/sitemap.xml
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (iPhone; CPU iPhone OS 8_0_2 like Mac OS X) AppleWebKit/600.1.4 (KHTML, like Gecko) Version/8.0 Mobile/12A366 Safari/600.1.4`
  * Evidence: ``
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/sitemap.xml
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (iPhone; U; CPU iPhone OS 3_0 like Mac OS X; en-us) AppleWebKit/528.18 (KHTML, like Gecko) Version/4.0 Mobile/7A341 Safari/528.16`
  * Evidence: ``
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/sitemap.xml
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `msnbot/1.1 (+http://search.msn.com/msnbot.htm)`
  * Evidence: ``
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/cookies
  * Method: `POST`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1)`
  * Evidence: ``
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/cookies
  * Method: `POST`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 6.0)`
  * Evidence: ``
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/cookies
  * Method: `POST`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 6.1)`
  * Evidence: ``
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/cookies
  * Method: `POST`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Trident/7.0; rv:11.0) like Gecko`
  * Evidence: ``
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/cookies
  * Method: `POST`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/75.0.3739.0 Safari/537.36 Edg/75.0.109.0`
  * Evidence: ``
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/cookies
  * Method: `POST`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.124 Safari/537.36`
  * Evidence: ``
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/cookies
  * Method: `POST`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:93.0) Gecko/20100101 Firefox/91.0`
  * Evidence: ``
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/cookies
  * Method: `POST`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (compatible; Googlebot/2.1; +http://www.google.com/bot.html)`
  * Evidence: ``
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/cookies
  * Method: `POST`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (compatible; Yahoo! Slurp; http://help.yahoo.com/help/us/ysearch/slurp)`
  * Evidence: ``
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/cookies
  * Method: `POST`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (iPhone; CPU iPhone OS 8_0_2 like Mac OS X) AppleWebKit/600.1.4 (KHTML, like Gecko) Version/8.0 Mobile/12A366 Safari/600.1.4`
  * Evidence: ``
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/cookies
  * Method: `POST`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (iPhone; U; CPU iPhone OS 3_0 like Mac OS X; en-us) AppleWebKit/528.18 (KHTML, like Gecko) Version/4.0 Mobile/7A341 Safari/528.16`
  * Evidence: ``
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/cookies
  * Method: `POST`
  * Parameter: `Header User-Agent`
  * Attack: `msnbot/1.1 (+http://search.msn.com/msnbot.htm)`
  * Evidence: ``
  * Other Info: ``

Instances: 194

### Solution



### Reference


* [ https://owasp.org/wstg ](https://owasp.org/wstg)



#### Source ID: 1

### [ User Controllable HTML Element Attribute (Potential XSS) ](https://www.zaproxy.org/docs/alerts/10031/)



##### Informational (Low)

### Description

This check looks at user-supplied input in query string parameters and POST data to identify where certain HTML attribute values might be controlled. This provides hot-spot detection for XSS (cross-site scripting) that will require further review by a security analyst to determine exploitability.

* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/cookies%3Fcookies-saved=true
  * Method: `GET`
  * Parameter: `cookies-saved`
  * Attack: ``
  * Evidence: ``
  * Other Info: `User-controlled HTML attribute values were found. Try injecting special characters to see if XSS might be possible. The page at the following URL:

https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/cookies?cookies-saved=true

appears to include user input in:
a(n) [input] tag [value] attribute

The user input found was:
cookies-saved=true

The user-controlled value was:
true`
* URL: https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/cookies%3Fcookies-saved=true
  * Method: `GET`
  * Parameter: `cookies-saved`
  * Attack: ``
  * Evidence: ``
  * Other Info: `User-controlled HTML attribute values were found. Try injecting special characters to see if XSS might be possible. The page at the following URL:

https://s198d01-education-benchmarking-d8a2cna4f9btahaz.a02.azurefd.net/cookies?cookies-saved=true

appears to include user input in:
a(n) [svg] tag [aria-hidden] attribute

The user input found was:
cookies-saved=true

The user-controlled value was:
true`

Instances: 2

### Solution

Validate all input and sanitize output it before writing to any HTML attributes.

### Reference


* [ https://cheatsheetseries.owasp.org/cheatsheets/Input_Validation_Cheat_Sheet.html ](https://cheatsheetseries.owasp.org/cheatsheets/Input_Validation_Cheat_Sheet.html)


#### CWE Id: [ 20 ](https://cwe.mitre.org/data/definitions/20.html)


#### WASC Id: 20

#### Source ID: 3


\newpage
