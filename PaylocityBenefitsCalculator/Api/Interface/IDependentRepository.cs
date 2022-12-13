using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Api.Models;

namespace Api.Interface
{
    public interface IDependentRepository
    {
        Task<List<Dependent>> GetDependents();
        Task<Dependent> GetDependentByID(int ID);
        Task<List<Dependent>> InsertDependent(Dependent dependent);
        Task<Dependent> UpdateDependent(int ID, Dependent dependent);
        Task<List<Dependent>> DeleteDependent(int ID);
        bool DependentCanBeInserted(Dependent dependent);
    }
}
