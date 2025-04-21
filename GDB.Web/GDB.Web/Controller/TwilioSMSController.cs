using GDB.Web.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;
namespace GDB.Web.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class TwilioSMSController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public TwilioSMSController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost]
        [Route("SendSms")]
        public async Task<IActionResult> SendSms([FromBody] MessagesViewModel messages)
        {
            string accountSid = _configuration["Twilio:AccountSID"];
            string authToken = _configuration["Twilio:AuthToken"];
            string fromNumber = _configuration["Twilio:FromNumber"];
            try
            {
                TwilioClient.Init(accountSid, authToken);

                var message = MessageResource.Create(
                    to: new PhoneNumber(messages.PhoneNumber),
                    from: new PhoneNumber(fromNumber),
                    body: messages.Message
                );
                return Ok(new { message.Sid, Status = message.Status.ToString() });
            }
            catch (Exception ex)
            {
                string errorMessage = ex.Message;
                return null;
            }
        }
    }
}

    public class SmsRequest
    {
        public string To { get; set; }
        public string Message { get; set; }
    }
