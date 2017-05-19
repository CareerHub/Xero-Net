## Tests for the Core API
These are really code snippets in the form of runnable tests. The examples for each of the endpoints in [Developer](http://developer.xero.com/documentation/api/api-overview/) have been turned into individual tests.


## Getting the tests working
Add `appsettings.json` to the `Xero.Api.Tests` directory:

    {
        "Api": {
            "BaseUrl": "https://api.xero.com",
            "ConsumerKey": "MEQS3OLPBG4DKNI52N9U8Z5VYEXXXX",
            "ConsumerSecret": "6FJIDKHKKMTNOMR8EVYGKXYIXXXXXX",
            "SigningCertificatePath": "public_privatekey.pfx",
            "SigningCertificatePassword": ""
        }
    }

The config file will need to be edited for your certificate and token values for a private application.

These will run against the live API, so will cause your rates to be reached if all are run at once.
