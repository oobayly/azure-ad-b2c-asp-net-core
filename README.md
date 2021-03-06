# azure-ad-b2c-asp-net-core

A sample demonstrating how you can configure your ASP.NET Core applications to take advantage of [Azure AD B2C](https://azure.microsoft.com/en-us/services/active-directory-b2c/) to perform such tasks as:
- Authenticate users
- Protect Web APIs
- Redeem authorization code
- Call a protected Web API

Please find more information in this walk-through post:
[Setting up your ASP.NET Core apps and services for Azure AD B2C](https://dzimchuk.net/setting-up-your-aspnet-core-apps-and-services-for-azure-ad-b2c/)
You may also want to check [this](https://dzimchuk.net/azure-ad-b2c-user-profile-editing-issues-with-asp-net-core/) post describing issues with user profile editing.

# Configuration

## Web App

```
"Authentication": {
    "AzureAd": {
      "Instance": "e.g. https://login.microsoftonline.com/",
      "TenantId": "e.g. <your domain>.onmicrosoft.com>",
      "ClientId": "",
      "ClientSecret": "",
      "PostLogoutRedirectUri": "https://localhost:44397/",
      "B2C": {
        "SignInOrSignUpPolicy": "e.g B2C_1_TestSignUpAndSignInPolicy",
        "EditProfilePolicy": "e.g B2C_1_TestProfileEditPolicy"
      }
    }
  },
  "TestServiceOptions": {
    "BaseUrl": "https://localhost:44359/"
  } 
```

## TestService

```
"Authentication": {
    "AzureAd": {
      "Instance": "e.g. https://login.microsoftonline.com/",
      "TenantId": "e.g. <your domain>.onmicrosoft.com>",
      "Audience": "Use client Id of the common app",
      "SignInOrSignUpPolicy": "e.g B2C_1_TestSignUpAndSignInPolicy"
    }
  }
```
