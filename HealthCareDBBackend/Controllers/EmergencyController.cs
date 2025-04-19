//using Microsoft.AspNetCore.Mvc;
//using EmergencyAlertAPI.Data;
//using EmergencyAlertAPI.Models;
//using Twilio;
//using Twilio.Rest.Api.V2010.Account;
//using Microsoft.EntityFrameworkCore;
//using System;
//using System.Threading.Tasks;

//namespace EmergencyAlertAPI.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class EmergencyController : ControllerBase
//    {
//        private readonly AppDbContext _context;

//        public EmergencyController(AppDbContext context)
//        {
//            _context = context;
//        }

//        [HttpPost("send-alert")]
//        public async Task<IActionResult> SendAlert([FromBody] EmergencyAlert alert)
//        {
//            if (!ModelState.IsValid)
//                return BadRequest(ModelState);

//            _context.EmergencyAlerts.Add(alert);
//            await _context.SaveChangesAsync();

//            // Send SMS using Twilio
//            string accountSid = "your_twilio_sid";
//            string authToken = "your_twilio_auth_token";
//            string twilioPhoneNumber = "+123456789"; // Replace with Twilio number

//            TwilioClient.Init(accountSid, authToken);
//            var message = await MessageResource.CreateAsync(
//                body: $"Emergency Alert: {alert.PatientName} triggered an emergency ({alert.EmergencyType}).",
//                from: new Twilio.Types.PhoneNumber(twilioPhoneNumber),
//                to: new Twilio.Types.PhoneNumber(alert.ContactNumber)
//            );

//            alert.Status = "Notified";
//            await _context.SaveChangesAsync();

//            return Ok(new { message = "Alert sent!", sid = message.Sid });
//        }
//    }
//}
