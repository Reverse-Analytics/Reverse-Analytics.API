using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReverseAPI.DAL;
using Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReverseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : Controller
    {
        private readonly IDbLayer _context;

        public PaymentsController(IDbLayer context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<ActionResult<ICollection<Payment>>> Get()
        {
            try
            {
                var payments = await _context.GetPayments();

                // // int g = 0;

                return Ok(payments);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Payment>> Get(int? id)
        {
            try
            {
                var payment = await _context.GetPayment(id);

                // // int g = 0;

                if (payment == null) return NotFound();

                return Ok(payment);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<Payment>> Add(Payment newPayment)
        {
            try
            {
                var payment = await _context.AddPayment(newPayment);

                // // int g = 0;

                return CreatedAtAction(nameof(Get), new { id = payment.PaymentId }, newPayment);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut]
        public async Task<ActionResult<Payment>> Update(Payment paymentToUpdate)
        {
            try
            {
                var payment = await _context.UpdatePayment(paymentToUpdate);

                // // int g = 0;

                return Ok(payment);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Payment>> Delete(int? id)
        {
            try
            {
                var payment = await _context.DeletePayment(id);

                // // int g = 0;

                return Ok(payment);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
