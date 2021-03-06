﻿using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using FluentValidationApp.Web.DTOs;
using FluentValidationApp.Web.FluentValidators;
using FluentValidationApp.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FluentValidationApp.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersApiController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IValidator<Customer> _custamerValidator;
        private readonly IMapper _mapper;

        public CustomersApiController(AppDbContext context, IValidator<Customer> custamerValidator, IMapper mapper)
        {
            _context = context;
            _custamerValidator = custamerValidator;
            _mapper = mapper;
        }

        //[Route("Mapping")]
        //[HttpGet]
        //public IActionResult MappingOrnek()
        //{
        //    Customer customer = new Customer
        //    {
        //        Name = "Bayram Eren",
        //        Email = "bayram.eren@outlook.com.tr",
        //        Age = 28,
        //       // CreditCard = new CreditCard
        //        {
        //            Number = "2344",
        //            ValidDate = DateTime.Now
        //        }
        //    };

        //    return Ok(_mapper.Map<CustomerDto>(customer));
        //}

        // GET: api/CustomersApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerDto>>> GetCustomers()
        {
            var customer = await _context.Customers.ToListAsync();

            return _mapper.Map<List<CustomerDto>>(customer);
        }

        // GET: api/CustomersApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> GetCustomer(int id)
        {
            var customer = await _context.Customers.FindAsync(id);

            if (customer == null)
            {
                return NotFound();
            }

            return customer;
        }

        // PUT: api/CustomersApi/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomer(int id, Customer customer)
        {
            if (id != customer.Id)
            {
                return BadRequest();
            }

            _context.Entry(customer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/CustomersApi
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Customer>> PostCustomer(Customer customer)
        {
            var customerResult = _custamerValidator.Validate(customer);


            //AddressValidator validationRules = new AddressValidator();
            //ValidationResult result;
            //customer.Addresses.ToList().ForEach(x =>
            //{
            //    result =  validationRules.Validate(x);
            //});

            if (!customerResult.IsValid)
            {
                return BadRequest(customerResult.Errors.Select(x => new
                {
                    Property = x.PropertyName,
                    Message = x.ErrorMessage
                }));
            }

            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCustomer", new { id = customer.Id }, customer);
        }

        // DELETE: api/CustomersApi/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Customer>> DeleteCustomer(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }

            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();

            return customer;
        }

        private bool CustomerExists(int id)
        {
            return _context.Customers.Any(e => e.Id == id);
        }
    }
}