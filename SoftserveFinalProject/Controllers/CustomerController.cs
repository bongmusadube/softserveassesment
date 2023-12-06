using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SoftserveFinalProject.Data;
using SoftserveFinalProject.Models;

namespace SoftserveFinalProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly CustomerAPIDbContext dbContext;
        public CustomersController(CustomerAPIDbContext dbContext)
        {
           this.dbContext = dbContext;
        }

        public CustomerAPIDbContext DbContext { get; }

        [HttpGet]
        public async Task<IActionResult> GetCustomers()
        {
            return Ok( await dbContext.Customers.ToListAsync());
          
        }
        
        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetCustomer([FromRoute] Guid id)
        {
            var customer = await dbContext.Customers.FindAsync(id);
            
            if(customer == null)
            {
                return NotFound();
            }
            return Ok(customer);
        }


        [HttpPost]
        public async Task<IActionResult> AddCustomer(AddCustomerRequest addCustomerRequest) 
        {
            var customer = new Customer()
            {
                Id = Guid.NewGuid(),
                FirstName = addCustomerRequest.FirstName,
                LastName = addCustomerRequest.LastName,
                UserName = addCustomerRequest.UserName,
                EmailAddress = addCustomerRequest.EmailAddress,
                DateOfBirth = addCustomerRequest.DateOfBirth,
                Age = addCustomerRequest.Age,
                DateCreated = new DateTime(),
                DateModified = new DateTime(),
                IsDeleted = false,



            };

              await dbContext.Customers.AddAsync(customer);
              await dbContext.SaveChangesAsync();

              return Ok(customer);
        }


        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateCustomer([FromRoute] Guid id, UpdateCustomerRequest updateCustomerRequest)
        {
            //Check if the record exists
            var customer = await dbContext.Customers.FindAsync(id);

            if (customer != null) 
            {
                customer.FirstName = updateCustomerRequest.FirstName;
                customer.LastName = updateCustomerRequest.LastName;
                customer.UserName = updateCustomerRequest.UserName;
                customer.EmailAddress = updateCustomerRequest.EmailAddress;
                customer.Age = updateCustomerRequest.Age;
                customer.DateModified = new DateTime();


                await dbContext.SaveChangesAsync();

                //Return updated customer record
                return Ok(customer);
               
            
            }

            return NotFound();
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteCustomer([FromRoute] Guid id)
        {
            //Find the route
            var customer = await dbContext.Customers.FindAsync(id);

            if(customer !=null)
            {
                dbContext.Remove(customer);
               await  dbContext.SaveChangesAsync();

                return Ok(customer);

            }

            return NotFound();

        }


    }
}
