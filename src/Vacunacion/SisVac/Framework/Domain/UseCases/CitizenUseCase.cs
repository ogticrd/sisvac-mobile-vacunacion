using SisVac.Framework.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SisVac.Framework.Domain.UseCases
{
    public interface ICitizenUseCase
    {
        Task<UserResponse> GetDocumentData(string document);
    }

    public class CitizenUseCase : ICitizenUseCase
    {
        protected ICitizensApiClient _citizensApiClient;
        public CitizenUseCase(ICitizensApiClient citizensApiClient)
        {
            _citizensApiClient = citizensApiClient;
        }

        public Task<UserResponse> GetDocumentData(string document)
        {
            if (string.IsNullOrEmpty(document))
                return null;

            if (document.Length == 13)
                document = document.Replace("-", "");

            return _citizensApiClient.GetBasicData(document);
        }
    }

}
