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
    public class Paycheck : IPaycheck
    {
        private readonly SampleContext _sampleContext;
        public Paycheck(SampleContext sampleContext)
        {
            _sampleContext = sampleContext;
        }

        public decimal ViewPay(int id)
        {
            /*
             * 26 paychecks per year with deductions spread as evenly as possible on each
                paycheck
                o employees have a base cost of $1,000 per month (for benefits)
                o each dependent represents an additional $600 cost per month (for benefits)
                o employees that make more than $80,000 per year will incur an additional 2% of their
                yearly salary in benefits costs
                o dependents that are over 50 years old will incur an additional $200 per month
             */
            var employee = _sampleContext.Employee.Where(e=> e.Id == id).FirstOrDefault(); 
            employee.Dependents = _sampleContext.Dependent.Where(d=>d.EmployeeId == employee.Id).ToList();
            return CalculatePay(employee);

        }

        public decimal CalculatePay(Employee employee)
        {
            var actualPay = 0.0M;
            if (employee != null)
            {
                decimal baseCost = 1000;
                
                if (employee.Dependents != null && employee.Dependents.Count > 0)
                {
                    DateTime Now = DateTime.Now;
                    foreach (var dependent in employee.Dependents)
                    {
                        baseCost += 600;
                        int Years = new DateTime(DateTime.Now.Subtract(dependent.DateOfBirth).Ticks).Year - 1;
                        if (Years > 50)
                        {
                            baseCost += 200;
                        }
                    }
                }
                baseCost = baseCost * 12;
                if (employee.Salary > 80000)
                {
                    baseCost += Decimal.Multiply(employee.Salary, (decimal)0.02);
                }
                actualPay = (employee.Salary - baseCost) / 26;
            }
            return actualPay;
        }

       
    }
}
