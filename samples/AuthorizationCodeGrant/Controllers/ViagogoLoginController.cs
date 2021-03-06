﻿using GogoKit;
using GogoKit.Enumerations;
using System;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace AuthorizationCodeGrant.Controllers
{
    public class ViagogoLoginController : Controller
    {
        private const string ClientIdentifier = "YOUR_CLIENT_ID";
        private const string ClientSecret = "YOUR_CLIENT_SECRET";

        // This value must match the callback URL your application was registered
        // with. Note: Your callback url MUST be HTTPS
        private static readonly Uri RedirectUri = new Uri("https://localhost:44300/callback");

        private readonly IViagogoClient _viagogoClient;

        public ViagogoLoginController()
        {
            _viagogoClient = new ViagogoClient(
                                ClientIdentifier,
                                ClientSecret,
                                new ProductHeaderValue("GogoKit-Samples"),
                                new GogoKitConfiguration { ViagogoApiEnvironment = ApiEnvironment.Sandbox });
        }

        [Route("")]
        public ActionResult Index()
        {
            // The value set here will be passed back to your callback. Use this
            // to set state such as which user is logging in so that your application
            // can store the access token returned against that user.
            var state = "UserId or some other unique value";
            var authorizationUrl = _viagogoClient.OAuth2.GetAuthorizationUrl(RedirectUri, new[] { "read:user" }, state);

            // Redirect the user to the viagogo authorization page where
            // they can login and consent to your application acting on
            // their behalf with the requested access
            return Redirect(authorizationUrl.AbsoluteUri);
        }

        [Route("callback")]
        public async Task<ActionResult> Callback(string code, string state)
        {
            // Optional: Use state to verify that this is a valid callbck request
            // that your application is expecting

            var token = await _viagogoClient.OAuth2.GetAuthorizationCodeAccessTokenAsync(code, RedirectUri, new[] { "read:user" });

            // Save the token for making API requests later. 
            await _viagogoClient.TokenStore.SetTokenAsync(token);

            return Content(token.AccessToken);
        }
    }
}