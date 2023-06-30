using Microsoft.AspNetCore.Mvc;
using ReverseAnalytics.Domain.DTOs.Refund;
using ReverseAnalytics.Domain.Interfaces.Services;

namespace Reverse_Analytics.Api.Controllers
{
    [Route("api/refunds")]
    [ApiController]
    public class RefundController : ControllerBase
    {
        private readonly IRefundService _refundService;
        private readonly IRefundDetailService _refundDetailService;

        public RefundController(IRefundService refundService, IRefundDetailService refundDetailService)
        {
            _refundService = refundService;
            _refundDetailService = refundDetailService;
        }


        // GET: api/<RefundController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RefundDto>>> GetRefundsAsync()
        {
            var refunds = await _refundService.GetAllRefundsAsync();
            return Ok(refunds);
        }

        // GET api/<RefundController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RefundDto>> GetRefundByIdAsync(int id)
        {
            var refund = await _refundService.GetRefundByIdAsync(id);

            return refund is null ?
                NotFound($"Refund with id: {id} does not exist") :
                Ok(refund);
        }

        // POST api/<RefundController>
        [HttpPost]
        public async Task<ActionResult<RefundDto>> CreateRefundAsync([FromBody] RefundForCreateDto refundToCreate)
        {
            var createdRefund = await _refundService.CreateRefundAsync(refundToCreate);

            return Created($"refunds/{createdRefund.Id}", createdRefund);
        }

        // PUT api/<RefundController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateRefundAsync(int id, [FromBody] RefundForUpdateDto refundToUpdate)
        {
            if (id != refundToUpdate.Id)
            {
                return BadRequest($"Route id: {id} does not match with Refund id: {refundToUpdate.Id}");
            }

            await _refundService.UpdateRefundAsync(refundToUpdate);

            return NoContent();
        }

        // DELETE api/<RefundController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteRefundAsync(int id)
        {
            await _refundService.DeleteRefundAsync(id);

            return NoContent();
        }
    }
}
