using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using KonigLabs.LevisJeans.Models;
using KonigLabs.LevisJeans.Models.Contexts;
using KonigLabs.LevisJeans.Models.Entities;

namespace KonigLabs.LevisJeans.Services
{
    public class TestSrv : ITestSrv
    {
        private readonly IDataContext _dataContext;

        public TestSrv(IDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public NumberVm SaveCustomer(CustomerVm model)
        {
            var entity = Mapper.Map<CustomerVm, Customer>(model);
            _dataContext.Add(entity);
            _dataContext.Commit();

            var saved =
                _dataContext.GetQuery<Customer>()
                    .Where(p =>
                        p.FirstName == entity.FirstName &&
                        p.LastName == entity.LastName &&
                        p.MiddleName == entity.MiddleName &&
                        p.Email == entity.Email &&
                        p.Phone == entity.Phone)
                    .OrderByDescending(p => p.Id)
                    .FirstOrDefault();

            if (saved == null)
                return new NumberVm {Id = -1};

            return new NumberVm {Id = saved.Id};
        }

        public void SaveTest(TestVm model)
        {
            var entity = Mapper.Map<TestVm, Test>(model);

            var old = _dataContext.GetQuery<Test>().FirstOrDefault(p => p.Id == entity.Id);
            if (old != null)
                _dataContext.Remove(old);

            _dataContext.Add(entity);
            _dataContext.Commit();
        }

        public void AddPhrase(ChoosePhraseVm model)
        {
            var entity = _dataContext.GetQuery<Customer>().FirstOrDefault(p => p.Id == model.CustomerId);

            if (entity == null)
                return;

            entity.Phrase = model.Phrase;
            _dataContext.Commit();
        }

        public string[] GetPhrases(int customerId)
        {
            var entity = _dataContext.GetQuery<Customer>().FirstOrDefault(p => p.Id == customerId);

            if (entity?.Answers == null)
                return null;

            var parts = new[]
            {
                entity.Answers.Answer1 ? "Сам подворот не подвернётся" : "Лучше ногу подверну, чем джинсы",
                entity.Answers.Answer2 ? "Заправляю даже кровать" : "Заправляю салаты, а не рубашки",
                entity.Answers.Answer3 ? "Глажу всё, что под руку попадётся" : "Не глажу даже котиков",
                entity.Answers.Answer4 ? "Ремень пристёгиваю даже в машинах" : "Ремней боюсь ещё с детства"
            };

            return new[]
            {
                $"{parts[0]} и {parts[1]}",
                $"{parts[0]} и {parts[2]}",
                $"{parts[1]} и {parts[3]}",
                $"{parts[3]} и {parts[0]}",
            };
        }

        public string GetStrAns(int customerId)
        {
            var entity = _dataContext.GetQuery<Test>().FirstOrDefault(p => p.Id == customerId);

            if (entity == null)
                return null;

            return (entity.Answer1 ? "d" : "n") +
                   (entity.Answer2 ? "d" : "n") +
                   (entity.Answer3 ? "d" : "n") +
                   (entity.Answer4 ? "d" : "n");
        }
    }
}