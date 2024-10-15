# ZAP Scanning Report

ZAP is supported by the [Crash Override Open Source Fellowship](https://crashoverride.com/?zap=rep).


## Summary of Alerts

| Risk Level | Number of Alerts |
| --- | --- |
| High | 0 |
| Medium | 2 |
| Low | 2 |
| Informational | 7 |




## Alerts

| Name | Risk Level | Number of Instances |
| --- | --- | --- |
| Content Security Policy (CSP) Header Not Set | Medium | 4 |
| Sub Resource Integrity Attribute Missing | Medium | 12 |
| Permissions Policy Header Not Set | Low | 4 |
| Strict-Transport-Security Header Not Set | Low | 4 |
| Base64 Disclosure | Informational | 4 |
| Information Disclosure - Suspicious Comments | Informational | 4 |
| Non-Storable Content | Informational | 4 |
| Sec-Fetch-Dest Header is Missing | Informational | 3 |
| Sec-Fetch-Mode Header is Missing | Informational | 3 |
| Sec-Fetch-Site Header is Missing | Informational | 3 |
| Sec-Fetch-User Header is Missing | Informational | 3 |




## Alert Detail



### [ Content Security Policy (CSP) Header Not Set ](https://www.zaproxy.org/docs/alerts/10038/)



##### Medium (High)

### Description

Content Security Policy (CSP) is an added layer of security that helps to detect and mitigate certain types of attacks, including Cross Site Scripting (XSS) and data injection attacks. These attacks are used for everything from data theft to site defacement or distribution of malware. CSP provides a set of standard HTTP headers that allow website owners to declare approved sources of content that browsers should be allowed to load on that page — covered types are JavaScript, CSS, HTML frames, fonts, images and embeddable objects such as Java applets, ActiveX, audio and video files.

* URL: https://s198d01-education-benchmarking-fd.azurefd.net/
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: ``
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-fd.azurefd.net/robots.txt
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: ``
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-fd.azurefd.net/school/135966
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: ``
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-fd.azurefd.net/sitemap.xml
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: ``
  * Other Info: ``

Instances: 4

### Solution

Ensure that your web server, application server, load balancer, etc. is configured to set the Content-Security-Policy header.

### Reference


* [ https://developer.mozilla.org/en-US/docs/Web/Security/CSP/Introducing_Content_Security_Policy ](https://developer.mozilla.org/en-US/docs/Web/Security/CSP/Introducing_Content_Security_Policy)
* [ https://cheatsheetseries.owasp.org/cheatsheets/Content_Security_Policy_Cheat_Sheet.html ](https://cheatsheetseries.owasp.org/cheatsheets/Content_Security_Policy_Cheat_Sheet.html)
* [ https://www.w3.org/TR/CSP/ ](https://www.w3.org/TR/CSP/)
* [ https://w3c.github.io/webappsec-csp/ ](https://w3c.github.io/webappsec-csp/)
* [ https://web.dev/articles/csp ](https://web.dev/articles/csp)
* [ https://caniuse.com/#feat=contentsecuritypolicy ](https://caniuse.com/#feat=contentsecuritypolicy)
* [ https://content-security-policy.com/ ](https://content-security-policy.com/)


#### CWE Id: [ 693 ](https://cwe.mitre.org/data/definitions/693.html)


#### WASC Id: 15

#### Source ID: 3

### [ Sub Resource Integrity Attribute Missing ](https://www.zaproxy.org/docs/alerts/90003/)



##### Medium (High)

### Description

The integrity attribute is missing on a script or link tag served by an external server. The integrity tag prevents an attacker who have gained access to this server from injecting a malicious content. 

* URL: https://s198d01-education-benchmarking-fd.azurefd.net/
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `<link
      rel="icon"
      type="image/x-icon"
      href="https://azurefrontdoorpages.azureedge.net/pages/PageNotFound_files/favicon.ico"
    />`
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-fd.azurefd.net/
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `<link
      rel="shortcut icon"
      type="image/x-icon"
      href="https://azurefrontdoorpages.azureedge.net/pages/PageNotFound_files/favicon.ico"
    />`
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-fd.azurefd.net/
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `<link
      rel="stylesheet"
      property="stylesheet"
      type="text/css"
      href="https://azurefrontdoorpages.azureedge.net/pages/PageNotFound_files/UxFxErrorCss_8097D4DBB3B4874308CB3816C1762BED98637360.css"
    />`
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-fd.azurefd.net/
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `<link
      rel="stylesheet"
      property="stylesheet"
      type="text/css"
      href="https://azurefrontdoorpages.azureedge.net/pages/PageNotFound_files/UxFxStableCssWesternEuropean_6724ABFCA058F28804A76FD40AD14C9D7A6031D9.css"
    />`
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-fd.azurefd.net/school/135966
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `<link
      rel="icon"
      type="image/x-icon"
      href="https://azurefrontdoorpages.azureedge.net/pages/PageNotFound_files/favicon.ico"
    />`
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-fd.azurefd.net/school/135966
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `<link
      rel="shortcut icon"
      type="image/x-icon"
      href="https://azurefrontdoorpages.azureedge.net/pages/PageNotFound_files/favicon.ico"
    />`
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-fd.azurefd.net/school/135966
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `<link
      rel="stylesheet"
      property="stylesheet"
      type="text/css"
      href="https://azurefrontdoorpages.azureedge.net/pages/PageNotFound_files/UxFxErrorCss_8097D4DBB3B4874308CB3816C1762BED98637360.css"
    />`
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-fd.azurefd.net/school/135966
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `<link
      rel="stylesheet"
      property="stylesheet"
      type="text/css"
      href="https://azurefrontdoorpages.azureedge.net/pages/PageNotFound_files/UxFxStableCssWesternEuropean_6724ABFCA058F28804A76FD40AD14C9D7A6031D9.css"
    />`
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-fd.azurefd.net/sitemap.xml
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `<link
      rel="icon"
      type="image/x-icon"
      href="https://azurefrontdoorpages.azureedge.net/pages/PageNotFound_files/favicon.ico"
    />`
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-fd.azurefd.net/sitemap.xml
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `<link
      rel="shortcut icon"
      type="image/x-icon"
      href="https://azurefrontdoorpages.azureedge.net/pages/PageNotFound_files/favicon.ico"
    />`
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-fd.azurefd.net/sitemap.xml
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `<link
      rel="stylesheet"
      property="stylesheet"
      type="text/css"
      href="https://azurefrontdoorpages.azureedge.net/pages/PageNotFound_files/UxFxErrorCss_8097D4DBB3B4874308CB3816C1762BED98637360.css"
    />`
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-fd.azurefd.net/sitemap.xml
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `<link
      rel="stylesheet"
      property="stylesheet"
      type="text/css"
      href="https://azurefrontdoorpages.azureedge.net/pages/PageNotFound_files/UxFxStableCssWesternEuropean_6724ABFCA058F28804A76FD40AD14C9D7A6031D9.css"
    />`
  * Other Info: ``

Instances: 12

### Solution

Provide a valid integrity attribute to the tag.

### Reference


* [ https://developer.mozilla.org/en-US/docs/Web/Security/Subresource_Integrity ](https://developer.mozilla.org/en-US/docs/Web/Security/Subresource_Integrity)


#### CWE Id: [ 345 ](https://cwe.mitre.org/data/definitions/345.html)


#### WASC Id: 15

#### Source ID: 3

### [ Permissions Policy Header Not Set ](https://www.zaproxy.org/docs/alerts/10063/)



##### Low (Medium)

### Description

Permissions Policy Header is an added layer of security that helps to restrict from unauthorized access or usage of browser/client features by web resources. This policy ensures the user privacy by limiting or specifying the features of the browsers can be used by the web resources. Permissions Policy provides a set of standard HTTP headers that allow website owners to limit which features of browsers can be used by the page such as camera, microphone, location, full screen etc.

* URL: https://s198d01-education-benchmarking-fd.azurefd.net/
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: ``
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-fd.azurefd.net/robots.txt
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: ``
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-fd.azurefd.net/school/135966
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: ``
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-fd.azurefd.net/sitemap.xml
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: ``
  * Other Info: ``

Instances: 4

### Solution

Ensure that your web server, application server, load balancer, etc. is configured to set the Permissions-Policy header.

### Reference


* [ https://developer.mozilla.org/en-US/docs/Web/HTTP/Headers/Permissions-Policy ](https://developer.mozilla.org/en-US/docs/Web/HTTP/Headers/Permissions-Policy)
* [ https://developer.chrome.com/blog/feature-policy/ ](https://developer.chrome.com/blog/feature-policy/)
* [ https://scotthelme.co.uk/a-new-security-header-feature-policy/ ](https://scotthelme.co.uk/a-new-security-header-feature-policy/)
* [ https://w3c.github.io/webappsec-feature-policy/ ](https://w3c.github.io/webappsec-feature-policy/)
* [ https://www.smashingmagazine.com/2018/12/feature-policy/ ](https://www.smashingmagazine.com/2018/12/feature-policy/)


#### CWE Id: [ 693 ](https://cwe.mitre.org/data/definitions/693.html)


#### WASC Id: 15

#### Source ID: 3

### [ Strict-Transport-Security Header Not Set ](https://www.zaproxy.org/docs/alerts/10035/)



##### Low (High)

### Description

HTTP Strict Transport Security (HSTS) is a web security policy mechanism whereby a web server declares that complying user agents (such as a web browser) are to interact with it using only secure HTTPS connections (i.e. HTTP layered over TLS/SSL). HSTS is an IETF standards track protocol and is specified in RFC 6797.

* URL: https://s198d01-education-benchmarking-fd.azurefd.net/
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: ``
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-fd.azurefd.net/robots.txt
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: ``
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-fd.azurefd.net/school/135966
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: ``
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-fd.azurefd.net/sitemap.xml
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: ``
  * Other Info: ``

Instances: 4

### Solution

Ensure that your web server, application server, load balancer, etc. is configured to enforce Strict-Transport-Security.

### Reference


* [ https://cheatsheetseries.owasp.org/cheatsheets/HTTP_Strict_Transport_Security_Cheat_Sheet.html ](https://cheatsheetseries.owasp.org/cheatsheets/HTTP_Strict_Transport_Security_Cheat_Sheet.html)
* [ https://owasp.org/www-community/Security_Headers ](https://owasp.org/www-community/Security_Headers)
* [ https://en.wikipedia.org/wiki/HTTP_Strict_Transport_Security ](https://en.wikipedia.org/wiki/HTTP_Strict_Transport_Security)
* [ https://caniuse.com/stricttransportsecurity ](https://caniuse.com/stricttransportsecurity)
* [ https://datatracker.ietf.org/doc/html/rfc6797 ](https://datatracker.ietf.org/doc/html/rfc6797)


#### CWE Id: [ 319 ](https://cwe.mitre.org/data/definitions/319.html)


#### WASC Id: 15

#### Source ID: 3

### [ Base64 Disclosure ](https://www.zaproxy.org/docs/alerts/10094/)



##### Informational (Medium)

### Description

Base64 encoded data was disclosed by the application/web server. Note: in the interests of performance not all base64 strings in the response were analyzed individually, the entire response should be looked at by the analyst/security team/developer(s).

* URL: https://s198d01-education-benchmarking-fd.azurefd.net/
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `net/pages/PageNotFound_files/UxFxErrorCss_8097D4DBB3B4874308CB3816C1762BED98637360`
  * Other Info: `������ځ�h�Z.��ߊW��LE�J뢰���4���w�;�}<�נ�סּ?|�~��`
* URL: https://s198d01-education-benchmarking-fd.azurefd.net/robots.txt
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `net/pages/PageNotFound_files/UxFxErrorCss_8097D4DBB3B4874308CB3816C1762BED98637360`
  * Other Info: `������ځ�h�Z.��ߊW��LE�J뢰���4���w�;�}<�נ�סּ?|�~��`
* URL: https://s198d01-education-benchmarking-fd.azurefd.net/school/135966
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `net/pages/PageNotFound_files/UxFxErrorCss_8097D4DBB3B4874308CB3816C1762BED98637360`
  * Other Info: `������ځ�h�Z.��ߊW��LE�J뢰���4���w�;�}<�נ�סּ?|�~��`
* URL: https://s198d01-education-benchmarking-fd.azurefd.net/sitemap.xml
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `net/pages/PageNotFound_files/UxFxErrorCss_8097D4DBB3B4874308CB3816C1762BED98637360`
  * Other Info: `������ځ�h�Z.��ߊW��LE�J뢰���4���w�;�}<�נ�סּ?|�~��`

Instances: 4

### Solution

Manually confirm that the Base64 data does not leak sensitive information, and that the data cannot be aggregated/used to exploit other vulnerabilities.

### Reference


* [ https://projects.webappsec.org/w/page/13246936/Information%20Leakage ](https://projects.webappsec.org/w/page/13246936/Information%20Leakage)


#### CWE Id: [ 200 ](https://cwe.mitre.org/data/definitions/200.html)


#### WASC Id: 13

#### Source ID: 3

### [ Information Disclosure - Suspicious Comments ](https://www.zaproxy.org/docs/alerts/10027/)



##### Informational (Medium)

### Description

The response appears to contain suspicious comments which may help an attacker. Note: Matches made within script blocks or files are against the entire content not only comments.

* URL: https://s198d01-education-benchmarking-fd.azurefd.net/
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `from`
  * Other Info: `The following pattern was used: \bFROM\b and was detected in the element starting with: "<!-- saved from url=(0058)https://df.onecloud.azure-test.net/Error/UE_404?shown=true -->", see evidence field for the suspicious comment/snippet.`
* URL: https://s198d01-education-benchmarking-fd.azurefd.net/robots.txt
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `from`
  * Other Info: `The following pattern was used: \bFROM\b and was detected in the element starting with: "<!-- saved from url=(0058)https://df.onecloud.azure-test.net/Error/UE_404?shown=true -->", see evidence field for the suspicious comment/snippet.`
* URL: https://s198d01-education-benchmarking-fd.azurefd.net/school/135966
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `from`
  * Other Info: `The following pattern was used: \bFROM\b and was detected in the element starting with: "<!-- saved from url=(0058)https://df.onecloud.azure-test.net/Error/UE_404?shown=true -->", see evidence field for the suspicious comment/snippet.`
* URL: https://s198d01-education-benchmarking-fd.azurefd.net/sitemap.xml
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `from`
  * Other Info: `The following pattern was used: \bFROM\b and was detected in the element starting with: "<!-- saved from url=(0058)https://df.onecloud.azure-test.net/Error/UE_404?shown=true -->", see evidence field for the suspicious comment/snippet.`

Instances: 4

### Solution

Remove all comments that return information that may help an attacker and fix any underlying problems they refer to.

### Reference



#### CWE Id: [ 200 ](https://cwe.mitre.org/data/definitions/200.html)


#### WASC Id: 13

#### Source ID: 3

### [ Non-Storable Content ](https://www.zaproxy.org/docs/alerts/10049/)



##### Informational (Medium)

### Description

The response contents are not storable by caching components such as proxy servers. If the response does not contain sensitive, personal or user-specific information, it may benefit from being stored and cached, to improve performance.

* URL: https://s198d01-education-benchmarking-fd.azurefd.net/
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `no-store`
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-fd.azurefd.net/robots.txt
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `no-store`
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-fd.azurefd.net/school/135966
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `no-store`
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-fd.azurefd.net/sitemap.xml
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `no-store`
  * Other Info: ``

Instances: 4

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

### [ Sec-Fetch-Dest Header is Missing ](https://www.zaproxy.org/docs/alerts/90005/)



##### Informational (High)

### Description

Specifies how and where the data would be used. For instance, if the value is audio, then the requested resource must be audio data and not any other type of resource.

* URL: https://s198d01-education-benchmarking-fd.azurefd.net/
  * Method: `GET`
  * Parameter: `Sec-Fetch-Dest`
  * Attack: ``
  * Evidence: ``
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-fd.azurefd.net/school/135966
  * Method: `GET`
  * Parameter: `Sec-Fetch-Dest`
  * Attack: ``
  * Evidence: ``
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-fd.azurefd.net/sitemap.xml
  * Method: `GET`
  * Parameter: `Sec-Fetch-Dest`
  * Attack: ``
  * Evidence: ``
  * Other Info: ``

Instances: 3

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

* URL: https://s198d01-education-benchmarking-fd.azurefd.net/
  * Method: `GET`
  * Parameter: `Sec-Fetch-Mode`
  * Attack: ``
  * Evidence: ``
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-fd.azurefd.net/school/135966
  * Method: `GET`
  * Parameter: `Sec-Fetch-Mode`
  * Attack: ``
  * Evidence: ``
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-fd.azurefd.net/sitemap.xml
  * Method: `GET`
  * Parameter: `Sec-Fetch-Mode`
  * Attack: ``
  * Evidence: ``
  * Other Info: ``

Instances: 3

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

* URL: https://s198d01-education-benchmarking-fd.azurefd.net/
  * Method: `GET`
  * Parameter: `Sec-Fetch-Site`
  * Attack: ``
  * Evidence: ``
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-fd.azurefd.net/school/135966
  * Method: `GET`
  * Parameter: `Sec-Fetch-Site`
  * Attack: ``
  * Evidence: ``
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-fd.azurefd.net/sitemap.xml
  * Method: `GET`
  * Parameter: `Sec-Fetch-Site`
  * Attack: ``
  * Evidence: ``
  * Other Info: ``

Instances: 3

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

* URL: https://s198d01-education-benchmarking-fd.azurefd.net/
  * Method: `GET`
  * Parameter: `Sec-Fetch-User`
  * Attack: ``
  * Evidence: ``
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-fd.azurefd.net/school/135966
  * Method: `GET`
  * Parameter: `Sec-Fetch-User`
  * Attack: ``
  * Evidence: ``
  * Other Info: ``
* URL: https://s198d01-education-benchmarking-fd.azurefd.net/sitemap.xml
  * Method: `GET`
  * Parameter: `Sec-Fetch-User`
  * Attack: ``
  * Evidence: ``
  * Other Info: ``

Instances: 3

### Solution

Ensure that Sec-Fetch-User header is included in user initiated requests.

### Reference


* [ https://developer.mozilla.org/en-US/docs/Web/HTTP/Headers/Sec-Fetch-User ](https://developer.mozilla.org/en-US/docs/Web/HTTP/Headers/Sec-Fetch-User)


#### CWE Id: [ 352 ](https://cwe.mitre.org/data/definitions/352.html)


#### WASC Id: 9

#### Source ID: 3


\newpage
