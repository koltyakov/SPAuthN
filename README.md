# SPAuthN - SharePoint .Net auth via Node.js

The wrapper for [node-sp-auth](https://www.npmjs.com/package/node-sp-auth) and [node-sp-auth-config](https://www.npmjs.com/package/node-sp-auth-config) for usage in .Net assemblies.

Allows authenticating in SharePoint in whatever you need scenarios and provides a wizard-like approach for building and managing connection config files.

---

Hey! Attention please! On the first place, it is a crazy experiment which solves one of the our very specific task for a frontier technology stack with SharePoint/Node.js/.Net where we need running the same exactly auth mechanisms which we use in Node.js but in .Net applications. We know exactly what we're doing and why. Please use the lib only in case when native .Net Credentials strategies do not suite your app.

## For whom is this library?

For folks who used to create application for SharePoint with authentication level powered by `node-sp-auth-config` and `node-sp-auth-config` and who desire reuse authentication settings parameters and formats in .Net application.

For geeks from geeks passionated with fanky technology experiments on their way doing awesome stuff.

For the cases when one tool should rule *all possible authentication strategies in SharePoint.

And definitely not for the situations when these work for you:

- `context.Credentials = new SharePointOnlineCredentials("username", "securepass");`
- `context.Credentials = new NetworkCredential("username", "password", "domain");`
- Any other native authentication route.

## Supported SharePoint versions

- SharePoint Online
- SharePoint 2016
- SharePoint 2013

## Authentication strategies

- SharePoint Online:
  - Addin only permissions
  - SAML based with user credentials
  - ADFS user credentials
- SharePoint 2013, 2016:
  - Addin only permissions
  - User credentials through the http NTLM handshake
  - ADFS user credentials
  - Form-based authentication (FBA)
  - Forefront TMG authentication

## How to install

```powershell
Install-Package SPAuthN
```

## How to use

```csharp
Headers authHeaders = SPAuth.GetAuth();
```

That's it! Really!

Now `Headers` object contains Cookie or Authorization which can be injected to web requests.
This is a low level, session timeouts should be controlled manually.