using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using KonigLabs.LevisJeans.Models;
using KonigLabs.LevisJeans.Models.Contexts;
using KonigLabs.LevisJeans.Models.Entities;
using AutoMapper;
using System.Text;

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
            return Mapper.Map<IEnumerable<Customer>, IEnumerable<CustomerAdminVm>>(
                _dataContext.GetQuery<Customer>()
                    .Where(p => p.Checked == check && p.Phrase != string.Empty)
                    .OrderBy(p => p.Id)
                ).ToList();
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
            _dataContext.Reseed("Customers");
            return string.Empty;
        }

        public void Serialize(string path)
        {
            var entities = Mapper.Map<IEnumerable<Customer>, IEnumerable<CustomerAdminVm>>(
                _dataContext.GetQuery<Customer>()
                    .Where(p => p.Phrase != string.Empty)
                    .OrderBy(p => p.Id)
                ).ToList();

            var text = new StringBuilder("Номер,Имя,Фамилия,Отчество,Email,Телефон,Отмечен\n");
            foreach (var item in entities)
            {
                text.Append($"{item.Id},{Escape(item.FirstName)},{Escape(item.LastName)},{Escape(item.MiddleName)},{Escape(item.Email)},{Escape(item.Phone)},{(item.Checked ? "Да" : "Нет")}\n");
            }
            
            File.WriteAllText(path, text.ToString(), Encoding.UTF8);
        }

        private string Escape(string str)
        {
            if (string.IsNullOrEmpty(str))
                return string.Empty;
            var tmp = str.Replace("\"", "\"\"");
            if (tmp.Contains(","))
                tmp = $"\"{tmp}\"";
            return tmp;
        }
    }
}