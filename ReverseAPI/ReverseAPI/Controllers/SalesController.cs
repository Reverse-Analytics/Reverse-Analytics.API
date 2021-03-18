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
    public class SalesController : Controller
    {
        private readonly IDbLayer _context;

        public SalesController(IDbLayer context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<ActionResult<ICollection<Sale>>> Get()
        {
            try
            {
                var payments = await _context.GetSales();

                // int g = 0;

                return Ok(payments);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Sale>> Get(int? id)
        {
            try
            {
                var sale = await _context.GetSale(id);

                // int g = 0;

                if (sale == null) return NotFound();

                return Ok(sale);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<Sale>> Add(Sale newSale)
        {
            try
            {
                var sale = await _context.AddSale(newSale);

                // int g = 0;

                return CreatedAtAction(nameof(Get), new { id = sale.SaleId }, newSale);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut]
        public async Task<ActionResult<Sale>> Update(Sale saleToUpdate)
        {
            try
            {
                var sale = await _context.UpdateSale(saleToUpdate);

                // int g = 0;

                return Ok(sale);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Sale>> Delete(int? id)
        {
            try
            {
                var sale = await _context.DeleteSale(id);

                // int g = 0;

                return Ok(sale);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
