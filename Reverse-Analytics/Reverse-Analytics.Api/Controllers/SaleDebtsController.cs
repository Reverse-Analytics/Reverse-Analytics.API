using Microsoft.AspNetCore.Mvc;
using ReverseAnalytics.Domain.DTOs.SaleDebt;
using ReverseAnalytics.Domain.Interfaces.Services;

namespace Reverse_Analytics.Api.Controllers
{
    [ApiController]
    [Route("api/debts/")]
    public class SaleDebtsController : ControllerBase
    {
        private readonly ISaleDebtService _saleDebtService;
        private readonly ISaleDebtService _supplyDebtService;

        public SaleDebtsController(ISaleDebtService saleDebtService, ISaleDebtService supplyDebtService)
        {
            ArgumentNullException.ThrowIfNull(saleDebtService);
            ArgumentNullException.ThrowIfNull(supplyDebtService);

            _saleDebtService = saleDebtService;
            _supplyDebtService = supplyDebtService;
        }

        [HttpGet("sales")]
        public async Task<ActionResult<IEnumerable<SaleDebtDto>>> GetSaleDebtsAsync()
        {
            var saleDebts = await _saleDebtService.GetAllSaleDebtsAsync();

            return Ok(saleDebts);
        }

        [HttpPut("sales/{id}/settle")]
        public async Task<ActionResult<SaleDebtDto>> SettleDebtAsync(int id)
        {
            var result = await _supplyDebtService.SettleDebtAsync(id);

            return Ok(result);
        }

        [HttpPut("sales/{id}/payment")]
        public async Task<ActionResult> PayDebtAsync(int id, DebtPaymentDto dto)
        {
            if (dto.Id != id)
            {
                return BadRequest($"Route id: {id} does not match with model id: {dto.Id}.");
            }

            var result = await _supplyDebtService.MakePaymentAsync(id, dto.Amount);

            return Ok(result);
        }
    }
}
