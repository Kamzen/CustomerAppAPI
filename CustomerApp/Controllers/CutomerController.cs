using CustomerApp.Data;
using CustomerApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace CustomerApp.Controllers
{
    [ApiController]
    //[Route("/[controller]")]
    [Route("/Customers")]
    public class CutomerController : Controller
    {

        private readonly CustomerAPIDbContext _dbContext;

        public CutomerController(CustomerAPIDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCustomers()
        {
            return Ok(await this._dbContext.Customers.Where(x => !x.IsDeleted).ToListAsync());
        }

        [HttpPost]
        public async Task<IActionResult> CreateCustomer(AddCustomerRequest addCustomerRequest)
        {
            var customer = new Customer()
            {
                CustomerID = Guid.NewGuid(),
                FirstName = addCustomerRequest.FirstName,
                LastName = addCustomerRequest.LastName,
                UserName = $"{addCustomerRequest.FirstName} {addCustomerRequest.LastName}",
                EmailAddress = addCustomerRequest.EmailAddress,
                DateOfBirth = addCustomerRequest.DateOfBirth,
                Age = DateTime.Now.Year - Int32.Parse(DateTime.Parse(addCustomerRequest.DateOfBirth.ToString()).Year.ToString()),
                DateCreated = DateTime.Now
            };

            await this._dbContext.Customers.AddAsync(customer);
            await this._dbContext.SaveChangesAsync();

            return Ok(customer);
        }


        [HttpGet]
        [Route("{Id:Guid}")]
        public async Task<IActionResult> GetCustomerById([FromRoute] Guid Id)
        {
            var customer = await this._dbContext.Customers.FindAsync(Id);

            if(customer == null)
            {
                return NotFound();
            }

            return Ok(customer);
        }


        [HttpPut]
        [Route("{Id:Guid}")]
        public async Task<IActionResult> UpdateCustomer([FromRoute] Guid Id, UpdateContactRequest updateContactRequest)
        {

            var customer = await this._dbContext.Customers.FindAsync(Id);
            if(customer == null)
            {
                return NotFound();
            }

            customer.FirstName = updateContactRequest.FirstName;
            customer.LastName = updateContactRequest.LastName;
            customer.UserName = $"{updateContactRequest.FirstName} {updateContactRequest.LastName}";
            customer.EmailAddress = updateContactRequest.EmailAddress;
            customer.DateOfBirth = updateContactRequest.DateOfBirth;
            customer.Age = DateTime.Now.Year - Int32.Parse(DateTime.Parse(updateContactRequest.DateOfBirth.ToString()).Year.ToString());
            customer.DateEdited = DateTime.Now;

            return Ok(customer);
        }


        [HttpDelete]
        [Route("{Id:Guid}")]

        public async Task<IActionResult> DeleteCustomer([FromRoute] Guid Id)
        {
            var customer = await this._dbContext.Customers.FindAsync(Id);
            if (customer == null)
            {
                return NotFound();
            }

            customer.IsDeleted = true;
            return Ok(customer);
        }
    }
}
