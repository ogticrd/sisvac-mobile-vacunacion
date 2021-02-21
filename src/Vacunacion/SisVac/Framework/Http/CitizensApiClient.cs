using System;
using System.Threading.Tasks;
using Refit;
using SisVac.Framework.Domain;

namespace SisVac.Framework.Http
{
    public interface ICitizensApiClient
    {
        [Get("/citizens/basic-data/")]
        Task<UserResponse> GetBasicData(string cedula);
    }
}
