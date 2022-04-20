using Message.API.Data;
using Message.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Message.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MessagesController : Controller
    {
        private readonly MessageDbContext messageDbContext;
        
        public MessagesController(MessageDbContext messageDbContext)
        {
            this.messageDbContext = messageDbContext;
        }
        //////////////////////////////////////////////////////////////////////////////////
        // get all messages
        [HttpGet]
        public async Task<IActionResult> GetAllMessages()
        {
            var messages = await messageDbContext.Messages.ToListAsync();
            return Ok(messages);
        }

        //////////////////////////////////////////////////////////////////////////////////

        // get message
        [HttpGet]
        [Route("{id:int}")]
        [ActionName("GetMessage")]
        public async Task<IActionResult> GetMessage([FromRoute] int id)
        {
            var messages = await messageDbContext.Messages.FirstOrDefaultAsync(x => x.Id == id);
            if(messages != null)
            {
                return Ok(messages);
            }

            return NotFound("Message not found");
        }



        // add message
        [HttpPost]
        public async Task<IActionResult> AddMessage([FromBody] Messages message)
        {
            await messageDbContext.Messages.AddAsync(message);
            await messageDbContext.SaveChangesAsync();            
            return CreatedAtAction(nameof(GetMessage), new { id = message.Id}, message);
        }



        // update message
        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> UpdateMessage([FromRoute] int id, [FromBody] Messages message)
        {
            var existingMessage = await messageDbContext.Messages.FirstOrDefaultAsync(x => x.Id == id);
            if(existingMessage != null)
            {
                existingMessage.IdUser = message.IdUser;
                existingMessage.Message = message.Message;
                existingMessage.Theme = message.Theme;
                await messageDbContext.SaveChangesAsync();
                return Ok(existingMessage);


            }

            return NotFound("Message not found");
        }


        // update message
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> DeleteMessage([FromRoute] int id)
        {
            var existingMessage = await messageDbContext.Messages.FirstOrDefaultAsync(x => x.Id == id);
            if (existingMessage != null)
            {
                messageDbContext.Remove(existingMessage);
                await messageDbContext.SaveChangesAsync();
                return Ok(existingMessage);
            }

            return NotFound("Message not found");
        }
    }
}
