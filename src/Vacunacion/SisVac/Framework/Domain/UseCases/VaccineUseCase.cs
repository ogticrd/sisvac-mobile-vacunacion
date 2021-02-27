using SisVac.Framework.Domain.Dto;
using SisVac.Framework.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SisVac.Framework.Domain.UseCases
{
    public interface IVaccineUseCase
    {
        Task<Qualification> GetQualificationData(string document);
        Task<UserResponse> GetDocumentData(string document);
        Task<Consent> GetConsentData(string document);
        Task<VaccineApplication> GetVaccineApplicationData(string document);
    }

    public class VaccineUseCase : IVaccineUseCase
    {

        protected ICitizensApiClient _citizensApiClient;
        public VaccineUseCase(ICitizensApiClient citizensApiClient)
        {
            _citizensApiClient = citizensApiClient;
        }

        public Task<Consent> GetConsentData(string document)
        {
            var response = ProcessDocument(document);

            if (!response.isValid)
                return null;

            return _citizensApiClient.GetConsent(response.document);
        }

        public Task<UserResponse> GetDocumentData(string document)
        {
            var response = ProcessDocument(document);

            if (!response.isValid)
                return null;

            return _citizensApiClient.GetBasicData(response.document);
        }

        public Task<Qualification> GetQualificationData(string document)
        {
            var response = ProcessDocument(document);

            if (!response.isValid)
                return null;

            return _citizensApiClient.GetQualification(response.document);
        }

        public Task<VaccineApplication> GetVaccineApplicationData(string document)
        {
            var response = ProcessDocument(document);

            if (!response.isValid)
                return null;

            return _citizensApiClient.GetVaccineApplication(response.document);
        }

        private (string document, bool isValid) ProcessDocument(string document)
        {
            if (string.IsNullOrEmpty(document))
                return (document, false);

            if (document.Length == 13)
                document = document.Replace("-", "");

            return (document, true);
        }
    }
}
