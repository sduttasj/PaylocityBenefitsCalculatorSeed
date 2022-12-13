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
    public class DependentsController : ControllerBase
    {
        private readonly IDependentRepository _dependent;
        private readonly IEmployeeRepository _employee;
        private readonly IMapper _mapper;
        public DependentsController(IDependentRepository dependent, IMapper mapper, IEmployeeRepository employee)
        {
            _dependent = dependent ??
                throw new ArgumentNullException(nameof(dependent));
            _employee = employee ??
                throw new ArgumentNullException(nameof(employee));
            _mapper = mapper;
        }
        [SwaggerOperation(Summary = "Get dependent by id")]
        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<GetDependentDto>>> Get(int id)
        {
            var dependent = await _dependent.GetDependentByID(id);
            //used automapper to copy objects
            var getDep = _mapper.Map<Dependent, GetDependentDto>(dependent);
            var result = new ApiResponse<GetDependentDto>
            {
                Data = getDep,
                Success = true
            };

            return result;
        }

        [SwaggerOperation(Summary = "Get all dependents")]
        [HttpGet("")]
        public async Task<ActionResult<ApiResponse<List<GetDependentDto>>>> GetAll()
        {
            var depList = await _dependent.GetDependents();
            var getDep = _mapper.Map<List<Dependent>, List<GetDependentDto>>(depList);
            var result = new ApiResponse<List<GetDependentDto>>
            {
                Data = getDep,
                Success = true
            };

            return result;
        }

        [SwaggerOperation(Summary = "Add dependent")]
        [HttpPost]
        public async Task<ActionResult<ApiResponse<List<AddDependentWithEmployeeIdDto>>>> AddDependent(AddDependentWithEmployeeIdDto newDependent)
        {
            var validEmployee = _employee.FindEmployee(newDependent.EmployeeId);
            var getDepList = new List<AddDependentWithEmployeeIdDto>();
            var msg = "failed to add dependent, spouse or deomestic parter present in the system";
            var dependent = new Dependent
            {
                EmployeeId = newDependent.EmployeeId,
                FirstName = newDependent.FirstName,
                LastName = newDependent.LastName,
                DateOfBirth = newDependent.DateOfBirth,
                Relationship = newDependent.Relationship
            };
            if (validEmployee &&  _dependent.DependentCanBeInserted(dependent))
            {
                List<Dependent>  listOfDependents = await _dependent.InsertDependent(dependent);            
                getDepList = _mapper.Map<List<Dependent>, List<AddDependentWithEmployeeIdDto>>(listOfDependents);
                msg = "success";
            }
            var result = new ApiResponse<List<AddDependentWithEmployeeIdDto>>
            {
                Data = getDepList,
                Success = true,
                Message = msg
            };

            return result;

        }

        [SwaggerOperation(Summary = "Update dependent")]
        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponse<GetDependentDto>>> UpdateDependent(int id, UpdateDependentDto updatedDependent)
        {
            var getDep = _mapper.Map<UpdateDependentDto, Dependent> (updatedDependent);
            getDep.Id= id;
            var dep = new Dependent();
            var msg = "Failed to update dependent, spouse or deomestic partner present in the system";
            if (_dependent.DependentCanBeInserted(getDep))
            {
                await _dependent.UpdateDependent(id, getDep);
                msg= "success";
            }
            var getDep2 = _mapper.Map<Dependent, GetDependentDto> (dep);
            var result = new ApiResponse<GetDependentDto>
            {
                Data = getDep2,
                Success = true,
                Message = msg
            };

            return result;
        }

        [SwaggerOperation(Summary = "Delete dependent")]
        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse<List<GetDependentDto>>>> DeleteDependent(int id)
        {
            var depList = await _dependent.DeleteDependent(id);
            var getDep = _mapper.Map<List<Dependent>, List<GetDependentDto>>(depList);
            var result = new ApiResponse<List<GetDependentDto>>
            {
                Data = getDep,
                Success = true
            };

            return result;
        }
    }
}
