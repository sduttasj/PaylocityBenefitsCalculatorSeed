using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Api.Dtos.Dependent;
using Api.Dtos.Employee;
using Api.Interface;
using Api.Models;
using Api.Context;

namespace Api.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly SampleContext _sampleContext;
        public EmployeeRepository(SampleContext sampleContext)
        {
            _sampleContext = sampleContext;
        }
        public async Task<List<Employee>> DeleteEmployee(int Id)
        {
            var employee = _sampleContext.Employee.Find(Id);
            if (employee != null)
            {
                _sampleContext.Entry(employee).State = EntityState.Deleted;
                _sampleContext.SaveChanges();
            }
            return await _sampleContext.Employee.ToListAsync();
        }

        public bool FindEmployee(int Id)
        {
            return _sampleContext.Employee.Any(x => x.Id == Id);

        }

        public async Task<List<Dependent>> GetDependentsByEmployeeID(int ID)
        {
            return await _sampleContext.Dependent.Where(m => m.EmployeeId == ID).ToListAsync();
        }

        public async Task<Employee> GetEmployeeByID(int Id)
        {
            return await _sampleContext.Employee.FirstOrDefaultAsync(m => m.Id == Id);
        }

        public async Task<List<Employee>> GetEmployees()
        {
            return await _sampleContext.Employee.Include(x => x.Dependents).ToListAsync();
        }

        public async Task<List<Employee>> InsertEmployee(Employee employee)
        {
            await _sampleContext.Employee.AddAsync(employee);
            await _sampleContext.SaveChangesAsync();
            return await _sampleContext.Employee.ToListAsync();
        }


        public async Task<Employee> UpdateEmployee(int Id, Employee Employee)
        {
            var employee = _sampleContext.Employee.Find(Id);
            if (employee != null)
            {
                employee.FirstName = Employee.FirstName;
                employee.LastName = Employee.LastName;
                employee.Salary = Employee.Salary;
                _sampleContext.Employee.Update(employee);
                await _sampleContext.SaveChangesAsync();
                return Employee;
            }
            else
            {
                return null;
            }

        }

    }
}
