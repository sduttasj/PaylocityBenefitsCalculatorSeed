using Api.Context;
using Api.Controllers;
using Api.Dtos.Dependent;
using Api.Interface;
using Api.Models;
using Api.Repository;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ApiTests
{
    public class PaycheckTest
    {
        
        private readonly SampleContext _sampleContext;
        private readonly IPaycheck _unitTestingPaycheckEmp = null;
        public PaycheckTest()
        {
            _sampleContext = new SampleContext();
            _unitTestingPaycheckEmp = new Paycheck(_sampleContext);
        }
        [Fact]
        public void GetPaycheck_EmployeePaycheckValue_EmployeeWithDependents()
        {
            //arrange
            var employee = GetSampleEmployee1();
            var expected = 2189.15M;
            //act
            var actual = Math.Round(_unitTestingPaycheckEmp.CalculatePay(employee), 2);
            //Assert
           Assert.Equal(expected, actual);
        }
        [Fact]
        public void GetPaycheck_EmployeePaycheckValue_EmployeeWithoutDependents()
        {
            //arrange
            var employee = GetSampleEmployee2();
            var expected = 7076.92m;
            //act
            var actual = Math.Round(_unitTestingPaycheckEmp.CalculatePay(employee), 2);
            //Assert
            Assert.Equal(expected, actual);
        }
        [Fact]
        public void GetPaycheck_EmployeePaycheckValue_EmployeeWithoutDependents_Below80k()
        {
            //arrange
            var employee = GetSampleEmployee3();
            var expected = 2461.57M;
            //act
            var actual = Math.Round(_unitTestingPaycheckEmp.CalculatePay(employee), 2);
            //Assert
            Assert.Equal(expected, actual);
        }
        [Fact]
        public void GetPaycheck_EmployeePaycheckValue_EmployeeDependentsAboveage50()
        {
            //arrange
            var employee = GetSampleEmployee4();
            var expected = 2096.84M;
            //act
            var actual = Math.Round(_unitTestingPaycheckEmp.CalculatePay(employee), 2);
            //Assert
            Assert.Equal(expected, actual);
        }
        private Employee GetSampleEmployee1()
        {
            var output =
            new Employee
            {
                Id = 2,
                FirstName = "Ja",
                LastName = "Morant",
                Salary = 92365.22m,
                DateOfBirth = new DateTime(1999, 8, 10),
                Dependents = new List<Dependent>
                {
                    new Dependent
                    {
                        Id = 1,
                        FirstName = "Spouse",
                        LastName = "Morant",
                        Relationship = Relationship.Spouse,
                        DateOfBirth = new DateTime(1998, 3, 3)
                    },
                    new()
                    {
                        Id = 2,
                        FirstName = "Child1",
                        LastName = "Morant",
                        Relationship = Relationship.Child,
                        DateOfBirth = new DateTime(2020, 6, 23)
                    },
                    new()
                    {
                        Id = 3,
                        FirstName = "Child2",
                        LastName = "Morant",
                        Relationship = Relationship.Child,
                        DateOfBirth = new DateTime(2021, 5, 18)
                    }
                }
            };
            return output;
        }
        private Employee GetSampleEmployee2()
        {
            var output =

                new Employee
                {
                    FirstName = "John",
                    LastName = "Doe",
                    DateOfBirth = DateTime.Parse("1992-12-10"),
                    Salary = 200000,
                    Id = 1
                };
            return output;
        }
        private Employee GetSampleEmployee3()
        {
            var output =

                new Employee
                {
                    FirstName = "John",
                    LastName = "Doe",
                    DateOfBirth = DateTime.Parse("1992-12-10"),
                    Salary = 76000.89m,
                    Id = 1
                };
            return output;
        }
        private Employee GetSampleEmployee4()
        {
            var output =
            new Employee
            {
                Id = 4,
                FirstName = "Jash",
                LastName = "Moranty",
                Salary = 92365.22m,
                DateOfBirth = new DateTime(1959, 8, 10),
                Dependents = new List<Dependent>
                {
                    new Dependent
                    {
                        Id = 1,
                        FirstName = "Spouse",
                        LastName = "Moranty",
                        Relationship = Relationship.Spouse,
                        DateOfBirth = new DateTime(1958, 3, 3)
                    },
                    new Dependent
                    {
                        Id = 2,
                        FirstName = "Child1",
                        LastName = "Moranty",
                        Relationship = Relationship.Child,
                        DateOfBirth = new DateTime(1985, 6, 23)
                    },
                    new Dependent
                    {
                        Id = 3,
                        FirstName = "Child2",
                        LastName = "Moranty",
                        Relationship = Relationship.Child,
                        DateOfBirth = new DateTime(1990, 5, 18)
                    }
                }
            };
            return output;
        }

    }
}
