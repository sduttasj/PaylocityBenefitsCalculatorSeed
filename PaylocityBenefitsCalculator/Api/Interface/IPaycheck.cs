using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Api.Models;

namespace Api.Interface
{
    public interface IPaycheck
    {
        decimal ViewPay(int id);
        decimal CalculatePay(Employee employee);
    }
}
