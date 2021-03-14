using Microsoft.AspNetCore.Mvc;
using ReverseAPI.DAL;
using ReverseAPI.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ReverseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private readonly IDbLayer _context;

        public ClientsController(IDbLayer context)
        {
            _context = context;
        }

        // GET: api/[controller]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Client>>> Get()
        {
            try
            {
                var clients = await _context.GetClients();

                int g = 0;

                return Ok(clients);
            }
            catch(Exception e)
            {
                return BadRequest(e);
            }
        }

        // GET: api/Clients/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Client>> Get(int id)
        {
            var client = await _context.GetClient(id);

            int g = 0;

            if (client == null) return NotFound();

            return Ok(client);
        }

        [HttpPost]
        public async Task<ActionResult<Client>> Create(Client newClient)
        {
            await _context.AddClient(newClient);

            int g = 0;

            return CreatedAtAction(nameof(Get), new { id = newClient.IdClient }, newClient);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Client clientToUpdate)
        {
            int g = 0;

            var client = await _context.GetClient(clientToUpdate.IdClient);

            if (client == null) return NotFound();

            client.FullName = clientToUpdate.FullName;
            client.Discount = clientToUpdate.Discount;

            await _context.UpdateClient(client);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var client = await _context.GetClient(id);

            int g = 0;

            if (client == null) return NotFound();

            await _context.DeleteClient(id);

            return NoContent();
        }
    }
}
