using Api.Dtos.Employee;
using Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Interface
{
    public interface IEmployeeRepository
    {
        Task<List<Employee>> GetEmployees();
        Task<Employee> GetEmployeeByID(int ID);
        Task<List<Dependent>> GetDependentsByEmployeeID(int ID);
        Task<List<Employee>> InsertEmployee(Employee employee);
        Task<Employee> UpdateEmployee(int ID, Employee employee);
        Task<List<Employee>> DeleteEmployee(int ID);
        bool FindEmployee(int Id);
    }
}
