using Microsoft.AspNetCore.Mvc;
using ReverseAnalytics.Domain.Entities;
using ReverseAnalytics.Infrastructure.Persistence;

namespace Reverse_Analytics.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class HomeController(ApplicationDbContext context) : CommonControllerBase
{
    private readonly ApplicationDbContext _context = context ?? throw new ArgumentNullException(nameof(context));

    // GET: api/<HomeController>
    [HttpGet]
    public IEnumerable<string> Get()
    {
        return new string[] { "value1", "value2" };
    }

    // GET api/<HomeController>/5
    [HttpGet("{id}")]
    public string Get(int id)
    {
        return "value";
    }

    // POST api/<HomeController>
    [HttpPost]
    public void Post([FromBody] string value)
    {
        var sale = new Sale()
        {
            TotalDue = 1000,
            TotalPaid = 5000,
            TotalDiscount = 2000,
            Currency = ReverseAnalytics.Domain.Enums.CurrencyType.USD,
            Date = DateTime.Now,
            Comments = "No comments",
            CustomerId = 1
        };

        _context.Sales.Add(sale);
        _context.SaveChanges();
    }

    // PUT api/<HomeController>/5
    [HttpPut("{id}")]
    public void Put(int id, [FromBody] string value)
    {
    }

    // DELETE api/<HomeController>/5
    [HttpDelete("{id}")]
    public void Delete(int id)
    {
    }
}
