# AADGen
AAD Token Generation Tool

Usage:

* `s2s`: Generate Service to Service Token

  * `--clientid`          Required. Client App Id

  * `--certthumbprint`    Required. Client App Certificate Thumbprint

  * `--scope`             Required. Scope for the token (Audience)

  * `--configname`        Required. Name of S2S Config Section in appsettings
  
* `onbehalf`: Exchange a User Token for a Client Application to call another API on behalf of the user
  * `--accesstoken`       A user access token for the trusted client app

  * `--clientid`          Required. The AppId Of the trusted client app

  * `--certthumbprint`    Required. Certificate thumbprint for the trusted client application

  * `--authority`         (Default: https://login.microsoftonline.com/72f988bf-86f1-41af-91ab-2d7cd011db47) The authority of the
                      trusted client application. (must match the authority that issued the original user token)

  * `--scope`            (Default: https://login.microsoftonline.com/72f988bf-86f1-41af-91ab-2d7cd011db47) The scope for the
                      target api. e.g. https://graph.microsoft.com/application.readwrite.all

* [WIP] `interactive`: Get a user access token for a specific resource using interactive sign-in

