using PushoverClient;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace BuildTest.Services.Pushover
{
    public class PushoverService : IPushoverService
    {
        /// <summary>
        /// Sends a message via Pushover
        /// </summary>
        /// 
        /// <param name="appToken">Application token</param>
        /// <param name="userKey">User key</param>
        /// <param name="message">Message text</param>
        /// 
        /// <returns>
        ///     true if message has been sent successfully, false if Pushover service returned BadRequest
        /// </returns>
        /// 
        /// <remarks>
        /// It seems that the Pushover service returns BadRequest if either the application token or the user key provided is invalid
        /// </remarks>
        /// 
        public async Task<bool> Message(string appToken, string userKey, string message)
        {
            if (String.IsNullOrWhiteSpace(appToken))
                throw new ArgumentException($"{nameof(appToken)} must be specified");
            if (String.IsNullOrWhiteSpace(userKey))
                throw new ArgumentException($"{nameof(userKey)} must be specified");
            if (String.IsNullOrWhiteSpace(message))
                throw new ArgumentException($"{nameof(message)} must be specified");

            var pclient = new PushoverClient.Pushover(appToken);

            // Pushover.NET doesn't handle failed requests, so it's on us
            try
            {
                var response = await pclient.PushAsync(
                  String.Empty,
                  message,
                  userKey);
                return true;
            }
            catch (AggregateException e)
            {
                // Check if the request actually made it to the Pushover service
                // and return false as it's a problem with the message
                if (e.InnerException is WebException webException 
                    && webException.Response is HttpWebResponse httpWebResponse
                    && httpWebResponse.StatusCode == HttpStatusCode.BadRequest)
                {
                    return false;
                }

                throw;
            }
        }
    }
}
