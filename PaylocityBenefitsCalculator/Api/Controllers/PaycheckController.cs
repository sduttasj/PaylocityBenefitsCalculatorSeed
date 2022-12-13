using Api.Dtos.Dependent;
using Api.Dtos.Employee;
using Api.Interface;
using Api.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class PaycheckController : ControllerBase
    {
        //private readonly IEmployeeRepository _employee;

        private readonly IPaycheck _paycheck;
        public PaycheckController(IPaycheck paycheck)
        {
           // _employee = employee ??
           //     throw new ArgumentNullException(nameof(employee));
            _paycheck = paycheck;
        }
        [SwaggerOperation(Summary = "View Paycheck")]
        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<Decimal>>> Paycheck(int id)
        {
            var paycheck =  _paycheck.ViewPay(id);
            var result = new ApiResponse<Decimal>
            {
                Data = paycheck,
                Success = true
            };

            return result;
        }

        [SwaggerOperation(Summary = "Calculate Paycheck")]
        [HttpGet("Calculate")]
        public async Task<ActionResult<ApiResponse<Decimal>>> calculatePaycheck([FromQuery]Employee employee)
        {
            var paycheck = _paycheck.CalculatePay(employee);
            var result = new ApiResponse<Decimal>
            {
                Data = paycheck,
                Success = true
            };

            return result;
        }

    }
}
