using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Matcher.DATA.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebApplication122.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatMessagesController : ControllerBase
    {
        private readonly MatcherDBContext _context;

        public ChatMessagesController(MatcherDBContext context)
        {
            _context = context;
        }

        // GET: api/ChatMessages
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ChatMessage>>> GetChatMessages()
        {
            return await _context.ChatMessages.ToListAsync();
        }

        // GET: api/ChatMessages/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ChatMessage>> GetChatMessage(int id)
        {
            var chatMessage = await _context.ChatMessages.FindAsync(id);

            if (chatMessage == null)
            {
                return NotFound();
            }

            return chatMessage;
        }

        // PUT: api/ChatMessages/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutChatMessage(int id, ChatMessage chatMessage)
        {
            if (id != chatMessage.Id)
            {
                return BadRequest();
            }

            _context.Entry(chatMessage).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ChatMessageExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/ChatMessages
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ChatMessage>> PostChatMessage(ChatMessage chatMessage)
        {
            _context.ChatMessages.Add(chatMessage);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetChatMessage", new { id = chatMessage.Id }, chatMessage);
        }

        // DELETE: api/ChatMessages/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteChatMessage(int id)
        {
            var chatMessage = await _context.ChatMessages.FindAsync(id);
            if (chatMessage == null)
            {
                return NotFound();
            }

            _context.ChatMessages.Remove(chatMessage);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ChatMessageExists(int id)
        {
            return _context.ChatMessages.Any(e => e.Id == id);
        }
    }
}
