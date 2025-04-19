using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HealthcareDBBackend.Data;
using HealthcareDBBackend.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

[Route("api/messages")]
[ApiController]
public class MessageController : ControllerBase
{
    private readonly UserInfoDBContext _context;

    public MessageController(UserInfoDBContext context)
    {
        _context = context;
    }


    // Fetch messages between two specific users
    [HttpGet("chat/{userId}/{chatWithUserId}/{pageNumber}/{pageSize}")]
    public async Task<IActionResult> GetMessagesBetweenUsers(int userId, int chatWithUserId, int pageNumber = 1, int pageSize = 20)
    {
        // 1. Verify the user exists
        var chatting_with = await _context.UserInfo
            .Where(u => u.UserId == chatWithUserId)
            .Select(u => new
            {
                u.FirstName,
                u.LastName,
                u.Role,
                Specialty = u.Role == "Doctor"
                    ? _context.Doctors
                        .Where(d => d.UserId == u.UserId)
                        .Select(d => d.Specialty)
                        .FirstOrDefault()
                    : null
            })
            .FirstOrDefaultAsync();


        if (chatting_with == null)
        {
            return NotFound("User not found.");
        }

        // 2. Check if a conversation exists
        var existingMessages = await _context.Messages
            .AnyAsync(m =>
                (m.SenderId == userId && m.ReceiverId == chatWithUserId) ||
                (m.SenderId == chatWithUserId && m.ReceiverId == userId));

        // 3. If no messages exist, insert a placeholder (optional)
        if (!existingMessages)
        {
            var placeholder = new Message
            {
                SenderId = userId,
                ReceiverId = chatWithUserId,
                MessageText = "[New chat started]",
                SentAt = DateTime.UtcNow,
                ReadAt = null
            };

            _context.Messages.Add(placeholder);
            await _context.SaveChangesAsync();
        }

        // 4. Now fetch messages like normal
        var messages = await _context.Messages
            .Where(m =>
                (m.SenderId == userId && m.ReceiverId == chatWithUserId) ||
                (m.SenderId == chatWithUserId && m.ReceiverId == userId))
            .OrderByDescending(m => m.SentAt)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return Ok(new
        {
            chatting_with_FirstName = chatting_with.FirstName,
            chatting_with_LastName = chatting_with.LastName,
            chatting_with_Role = chatting_with.Role,
            chatting_with_Specialty = chatting_with.Specialty,
            messages = messages
        });

    }





    // send message form user to designated user
    [HttpPost("send")]
    public async Task<IActionResult> SendMessage([FromBody] Message newMessage)
    {
        if (newMessage == null || string.IsNullOrEmpty(newMessage.MessageText))
        {
            return BadRequest("Message cannot be empty.");
        }

        newMessage.SentAt = DateTime.UtcNow;
        newMessage.ReadAt = null;

        _context.Messages.Add(newMessage);
        await _context.SaveChangesAsync();

        Console.WriteLine($"📩 New message sent: {newMessage.MessageText} from {newMessage.SenderId} to {newMessage.ReceiverId}");

        return Ok(new { message = "Message sent successfully!" });
    }

}


