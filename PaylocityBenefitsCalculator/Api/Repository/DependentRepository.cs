using Api.Context;
using Api.Interface;
using Api.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Repository
{
    public class DependentRepository : IDependentRepository
    {
        private readonly SampleContext _sampleContext;
        public DependentRepository(SampleContext sampleContext)
        {
            _sampleContext = sampleContext;
        }
        public async Task<List<Dependent>> DeleteDependent(int Id)
        {
            var dependent = _sampleContext.Dependent.Find(Id);
            if (dependent != null)
            {
                _sampleContext.Entry(dependent).State = EntityState.Deleted;
                _sampleContext.SaveChanges();
            }
            return await _sampleContext.Dependent.ToListAsync();
        }

        public async Task<Dependent> GetDependentByID(int Id)
        {
            return await _sampleContext.Dependent.FirstOrDefaultAsync(m => m.Id == Id);
        }

        public async Task<List<Dependent>> GetDependents()
        {
            return await _sampleContext.Dependent.ToListAsync();
        }

        public async Task<List<Dependent>> InsertDependent(Dependent dependent)
        {
            await _sampleContext.Dependent.AddAsync(dependent);
            await _sampleContext.SaveChangesAsync();
            return await _sampleContext.Dependent.ToListAsync();
        }

        public bool DependentCanBeInserted(Dependent dependent)
        {
            var allDependentsForThisEmployee = new List<Dependent>();
            if (dependent.Id == 0) // add dependent scenario
            {
                allDependentsForThisEmployee = _sampleContext.Dependent.Where(m => m.EmployeeId == dependent.EmployeeId).ToList();
            }
            else if (dependent.Id != 0 && dependent.EmployeeId == 0)
            {
                var empIdObj = _sampleContext.Dependent.Where(m => m.Id == dependent.Id).FirstOrDefault();
                if (empIdObj == null)
                    return false;
                var empId = empIdObj.EmployeeId;
                allDependentsForThisEmployee = _sampleContext.Dependent.Where(m => m.EmployeeId == empId).ToList();
            }
            else
            {
                return false;
            }

            int count = 0;
            int databaseIdDependent = 0;
            foreach (var dep in allDependentsForThisEmployee)
            {
                if (dep.Relationship == Relationship.Spouse || dep.Relationship == Relationship.DomesticPartner)
                {
                    count++;
                    databaseIdDependent = dep.Id; // current record in database
                }
                    
            }
            if ((dependent.Relationship == Relationship.Spouse || dependent.Relationship == Relationship.DomesticPartner) && count > 0)
            {
                if (dependent.Id != 0 && databaseIdDependent != dependent.Id)
                return false;

            }
                    
            
            return true;
        }

        public async Task<Dependent> UpdateDependent(int Id, Dependent dependent)
        {
            var dependentDB = await _sampleContext.Dependent.FirstOrDefaultAsync(m => m.Id == Id);
            dependentDB.DateOfBirth = dependent.DateOfBirth;
            dependentDB.FirstName = dependent.FirstName;
            dependentDB.LastName = dependent.LastName;

            _sampleContext.Dependent.Update(dependentDB);
            await _sampleContext.SaveChangesAsync();
            return dependent;
        }
    }
}
