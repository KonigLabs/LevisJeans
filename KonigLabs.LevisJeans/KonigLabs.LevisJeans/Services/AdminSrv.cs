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

        public void Serialize(string path, string separator)
        {
            var entities = Mapper.Map<IEnumerable<Customer>, IEnumerable<CustomerAdminVm>>(
                _dataContext.GetQuery<Customer>()
                    .Where(p => p.Phrase != string.Empty)
                    .OrderBy(p => p.Id)
                ).ToList();

            var text = new StringBuilder($"Номер{separator}Имя{separator}Фамилия{separator}Отчество{separator}Email{separator}Телефон{separator}Отмечен\n");
            foreach (var item in entities)
            {
                text.Append($"{item.Id}{separator}{Escape(item.FirstName)}{separator}{Escape(item.LastName)}{separator}{Escape(item.MiddleName)}{separator}{Escape(item.Email)}{separator}{Escape(item.Phone)}{separator}{(item.Checked ? "Да" : "Нет")}\n");
            }
            
            File.WriteAllText(path, text.ToString(), Encoding.UTF8);
        }

        private string Escape(string str)
        {
            if (string.IsNullOrEmpty(str))
                return string.Empty;
            var tmp = str.Replace("\"", "\"\"");
            //separators for different locales
            if (tmp.Contains(",") || tmp.Contains(";"))
                tmp = $"\"{tmp}\"";
            return tmp;
        }
    }
}