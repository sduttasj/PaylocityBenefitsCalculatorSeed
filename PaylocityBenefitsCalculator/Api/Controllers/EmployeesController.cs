using Api.Dtos.Dependent;
using Api.Dtos.Employee;
using Api.Interface;
using Api.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeRepository _employee;
        private readonly IMapper _mapper;
        public EmployeesController(IEmployeeRepository employee, IMapper mapper)
        {
            _employee = employee ??
                throw new ArgumentNullException(nameof(employee));
            _mapper = mapper;
        }
        [SwaggerOperation(Summary = "Get employee by id")]
        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<GetEmployeeDto>>> Get(int id)
        {
            var employee = await _employee.GetEmployeeByID(id);
            var getEmp = _mapper.Map<Employee,GetEmployeeDto>(employee);
            var result = new ApiResponse<GetEmployeeDto>
            {
                Data = getEmp,
                Success = true
            };

            return result;
        }
        
        [SwaggerOperation(Summary = "Get dependents by employee id")]
        [HttpGet("Dependents/{id}")]
        public async Task<ActionResult<ApiResponse<List<GetDependentDto>>>> GetDependentsByEmployee(int id)
        {
            var dependents = await _employee.GetDependentsByEmployeeID(id);
            var getDeps = _mapper.Map<List<Dependent>, List<GetDependentDto>>(dependents);
            var result = new ApiResponse<List<GetDependentDto>>
            {
                Data = getDeps,
                Success = true
            };

            return result;
        }
        
        [SwaggerOperation(Summary = "Get all employees")]
        [HttpGet("")]
        public async Task<ActionResult<ApiResponse<List<GetEmployeeDto>>>> GetAll()
        {
            var employeeList = await _employee.GetEmployees();
            var getEmp = _mapper.Map<List<Employee>, List<GetEmployeeDto>>(employeeList);
            var result = new ApiResponse<List<GetEmployeeDto>>
            {
                Data = getEmp,
                Success = true
            };

            return result;
        }

        [SwaggerOperation(Summary = "Add employee")]
        [HttpPost]
        public async Task<ActionResult<ApiResponse<List<AddEmployeeDto>>>> AddEmployee(AddEmployeeDto newEmployee)
        {
            var getEmp = _mapper.Map<AddEmployeeDto, Employee>(newEmployee);
            var employeeList = await _employee.InsertEmployee(getEmp);
            var getEmpList = _mapper.Map<List<Employee>, List<AddEmployeeDto>>(employeeList);
            var result = new ApiResponse<List<AddEmployeeDto>>
            {
                Data = getEmpList,
                Success = true
            };

            return result;
        }

        [SwaggerOperation(Summary = "Update employee")]
        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponse<GetEmployeeDto>>> UpdateEmployee(int id, UpdateEmployeeDto updatedEmployee)
        {
            var getEmp = _mapper.Map<UpdateEmployeeDto, Employee>(updatedEmployee);            
            var employee = await _employee.UpdateEmployee(id, getEmp);
            var getEmpDto = _mapper.Map<Employee, GetEmployeeDto>(employee);
            var result = new ApiResponse<GetEmployeeDto>
            {
                Data = getEmpDto,
                Success = true
            };

            return result;
        }

        [SwaggerOperation(Summary = "Delete employee")]
        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse<List<GetEmployeeDto>>>> DeleteEmployee(int id)
        {
            var depList = await _employee.DeleteEmployee(id);
            var getList = _mapper.Map<List<Employee>, List<GetEmployeeDto>>(depList);
            var result = new ApiResponse<List<GetEmployeeDto>>
            {
                Data = getList,
                Success = true
            };

            return result;
        }
    }
}
