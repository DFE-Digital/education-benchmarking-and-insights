# Cookie Documentation

## .AspNetCore.Antiforgery.RtGCWVXC8-4

This cookie is related to ASP.NET Core's Anti-forgery (CSRF) protection feature. When you enable anti-forgery protection in an ASP.NET Core application, the framework automatically generates tokens to prevent cross-site request forgery (CSRF) attacks. The `.AspNetCore.Antiforgery.RtGCWVXC8-4` cookie is part of that mechanism.


More details about ASP.NET Core Anti-forgery can be found [here](https://learn.microsoft.com/en-us/aspnet/core/security/anti-request-forgery?view=aspnetcore-8.0).

---

## .AspNetCore.Session

ASP.NET Core maintains session state by providing a cookie to the client that contains a session ID. This cookie:
- Is sent to the app with each request.
- Is used by the app to fetch the session data.
  The `.AspNetCore.Session` cookie is an essential part of managing user sessions in ASP.NET Core applications, facilitating the persistence of session data across multiple HTTP requests.

More details about ASP.NET Core session state can be found [here](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/app-state?view=aspnetcore-8.0#session-options).

---

## ai_session

The `ai_session` cookie is used by Application Insights to track user sessions and monitor user interactions within a web application. It helps in understanding how users navigate through and interact with your application.

More details about the `ai_session` cookie and Application Insights can be found [here](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/app-state?view=aspnetcore-8.0#session-options).

---

## ai_user

The `ai_user` cookie is utilized by Application Insights to identify unique users across their different sessions or visits to a web application. It helps in distinguishing one user from another and tracking their activities over time.

More details about the `ai_user` cookie and Application Insights can be found [here](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/app-state?view=aspnetcore-8.0#session-options).
