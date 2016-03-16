using System.Collections.Generic;
using KonigLabs.LevisJeans.Models;

namespace KonigLabs.LevisJeans.Services
{
    public interface IAdminSrv
    {
        IEnumerable<CustomerAdminVm> GetCustomers(bool check = false);
        string Check(int id);
        string UnCheck(int id);
        string TotalErase();
        void Serialize(string path);
    }
}