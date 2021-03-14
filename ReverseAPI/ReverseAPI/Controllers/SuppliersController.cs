using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReverseAPI.DAL;
using ReverseAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReverseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuppliersController : Controller
    {
        private readonly IDbLayer _context;
        public SuppliersController(IDbLayer context)
        {
            _context = context;
        }

        // GET: SuppliersController
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Supplier>>> Get()
        {
            try
            {
                var suppliers = await _context.GetSuppliers();

                return Ok(suppliers);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        // GET: SuppliersController/Details/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Supplier>> Get(int id)
        {
            var client = await _context.GetSupplier(id);

            int g = 0;

            if (client == null) return NotFound();

            return Ok(client);
        }

        // GET: SuppliersController/Create
        [HttpPost]
        public async Task<ActionResult<Supplier>> Create(Supplier newSupplier)
        {
            await _context.AddSupplier(newSupplier);

            int g = 0;

            return CreatedAtAction(nameof(Get), new { id = newSupplier.SupplierId }, newSupplier);
        }

        // GET: SuppliersController/Edit/5
        [HttpPut("{id}")]

        public async Task<ActionResult> Update(Supplier supplierToUpdate)
        {
            int g = 0;

            var supplier = await _context.GetSupplier(supplierToUpdate.SupplierId);

            if (supplier == null) return NotFound();

            supplier.FullName = supplierToUpdate.FullName;

            await _context.UpdateSupplier(supplier);

            return NoContent();
        }

        // GET: SuppliersController/Delete/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var supplier = await _context.GetSupplier(id);

            int g = 0;

            if (supplier == null) return NotFound();

            await _context.DeleteSupplier(id);

            return NoContent();
        }
    }
}
