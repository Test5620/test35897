/*
 * Pushover Messenger
 *
 * No description provided (generated by Swagger Codegen https://github.com/swagger-api/swagger-codegen)
 *
 * OpenAPI spec version: 1.0.0
 * 
 * Generated by: https://github.com/swagger-api/swagger-codegen.git
 */

using BuildTest.Services.Pushover;
using IO.Swagger.Attributes;
using IO.Swagger.Models;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Threading.Tasks;

namespace IO.Swagger.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class MessageApiController : Controller
    {
        private readonly IPushoverService _pushService;

        public MessageApiController(IPushoverService pushService)
        {
            _pushService = pushService;
        }

        /// <summary>
        /// Send a message to a user via a Pushover app
        /// </summary>

        /// <param name="body">Message object that needs to be sent</param>
        /// <response code="200">Message sent successfully</response>
        /// <response code="404">An application token or a user token provided is invalid</response>
        [HttpPost]
        [Route("/v1/message")]
        [ValidateModelState]
        [SwaggerOperation("Message")]
        public async virtual Task<IActionResult> Message([FromBody]Message body)
        {
            var success = await _pushService.Message(body.AppToken, body.UserKey, body._Message);
            return success ? StatusCode(200) : StatusCode(404);
        }
    }
}
