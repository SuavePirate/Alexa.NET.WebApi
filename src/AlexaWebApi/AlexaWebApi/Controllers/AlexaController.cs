using Alexa.NET;
using Alexa.NET.Request;
using Alexa.NET.Request.Type;
using Alexa.NET.Response;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlexaWebApi.Controllers
{
    [Route("api/[controller]")]
    public class AlexaController : Controller
    {
        /// <summary>
        /// This is the entry point for the Alexa skill
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public SkillResponse HandleResponse([FromBody]SkillRequest input)
        {
            var requestType = input.GetRequestType();

            // return a welcome message
            if(requestType == typeof(LaunchRequest))
            {
                return ResponseBuilder.Ask("Welcome to animal facts, ask me about information on an animal", null);
            }

            // return information from an intent
            else if (requestType == typeof(IntentRequest))
            {
                // do some intent-based stuff
                var intentRequest = input.Request as IntentRequest;

                // check the name to determine what you should do
                if (intentRequest.Intent.Name.Equals("AnimalFactIntent"))
                {
                    // get the slots
                    var animal = intentRequest.Intent.Slots["Animal"].Value;
                    if(animal == null)
                        return ResponseBuilder.Ask("You forgot to ask about an animal! Please try again.", null);

                    return ResponseBuilder.Tell($"I would normally tell you facts about ${animal} but I'm not a real skill yet.");
                }
            }

            return ResponseBuilder.Ask("I didn't understand that, please try again!", null);
        }
    }
}
