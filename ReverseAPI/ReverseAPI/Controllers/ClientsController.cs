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
        public async Task<ActionResult<List<Client>>> GetClients()
        {
            try
            {
                var clients = Task.Run(() => _context.GetClients());

                await Task.WhenAll(clients);

                return Ok(clients);
            }
            catch(Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpGet("{id:length(24)}", Name = "GetClient")]
        public async Task<ActionResult<Client>> Get(int id)
        {
            var client = await _context.GetClient(id);

            if (client == null) return NotFound();

            return client;
        }

        [HttpPost]
        public async Task<ActionResult<Client>> AddClient(Client newClient)
        {
            await _context.AddClient(newClient);

            return CreatedAtAction("GetClient", new { id = newClient.IdClient }, newClient);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<ActionResult<Client>> Update(Client clientToUpdate)
        {
            var client = await _context.GetClient(clientToUpdate.IdClient);

            if (client == null) return NotFound();

            await _context.UpdateClient(clientToUpdate);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<ActionResult<Client>> Delete(int id)
        {
            var client = await _context.GetClient(id);

            if (client == null) return NotFound();

            await _context.DeleteClient(id);

            return NoContent();
        }
    }
}
