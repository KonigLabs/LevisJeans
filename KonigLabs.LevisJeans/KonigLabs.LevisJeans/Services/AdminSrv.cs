using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using KonigLabs.LevisJeans.Models;
using KonigLabs.LevisJeans.Models.Contexts;
using KonigLabs.LevisJeans.Models.Entities;

namespace KonigLabs.LevisJeans.Services
{
    public class AdminSrv : IAdminSrv
    {
        private readonly IDataContext _dataContext;

        public AdminSrv(IDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public IEnumerable<CustomerAdminVm> GetCustomers(bool check = false)
        {
            return from c in _dataContext.GetQuery<Customer>()
                join t in _dataContext.GetQuery<Test>() on c.Id equals t.Id
                where c.Checked == check && c.Phrase != string.Empty
                orderby c.Id
                select new CustomerAdminVm
                {
                    Id = c.Id,
                    FirstName = c.FirstName,
                    LastName = c.LastName,
                    MiddleName = c.MiddleName,
                    Email = c.Email,
                    Phone = c.Phone,
                    Phrase = c.Phrase,
                    Checked = c.Checked,
                    Answer1 = t.Answer1,
                    Answer2 = t.Answer2,
                    Answer3 = t.Answer3,
                    Answer4 = t.Answer4
                };
        }

        public string Check(int id)
        {
            var entity = _dataContext.GetQuery<Customer>().FirstOrDefault(p => p.Id == id);

            if (entity == null)
                return "Клиент не найден.";

            if (entity.Checked)
                return "Уже отмечен.";

            entity.Checked = true;

            _dataContext.Commit();
            return string.Empty;
        }

        public string UnCheck(int id)
        {
            var entity = _dataContext.GetQuery<Customer>().FirstOrDefault(p => p.Id == id);

            if (entity == null)
                return "Клиент не найден.";

            if (!entity.Checked)
                return "Уже не отмечен.";

            entity.Checked = false;

            _dataContext.Commit();
            return string.Empty;
        }

        public string TotalErase()
        {
            _dataContext.RemoveAll("Tests");
            _dataContext.RemoveAll("Customers");
            return string.Empty;
        }

        public void Serialize(string path)
        {
            var entities = (from c in _dataContext.GetQuery<Customer>()
                join t in _dataContext.GetQuery<Test>() on c.Id equals t.Id
                orderby c.Id
                select new CustomerAdminVm
                {
                    Id = c.Id,
                    FirstName = c.FirstName,
                    LastName = c.LastName,
                    MiddleName = c.MiddleName,
                    Email = c.Email,
                    Phone = c.Phone,
                    Phrase = c.Phrase,
                    Checked = c.Checked,
                    Answer1 = t.Answer1,
                    Answer2 = t.Answer2,
                    Answer3 = t.Answer3,
                    Answer4 = t.Answer4
                }).ToList();

            var formatter = new XmlSerializer(typeof(List<CustomerAdminVm>));
            
            using (var fs = new FileStream(path, FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, entities);
            }
        }
    }
}