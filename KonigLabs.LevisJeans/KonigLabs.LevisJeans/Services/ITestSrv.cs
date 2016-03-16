using KonigLabs.LevisJeans.Models;

namespace KonigLabs.LevisJeans.Services
{
    public interface ITestSrv
    {
        NumberVm SaveCustomer(CustomerVm model);
        void SaveTest(TestVm model);
        void AddPhrase(ChoosePhraseVm model);
        string[] GetPhrases(int customerId);
    }
}