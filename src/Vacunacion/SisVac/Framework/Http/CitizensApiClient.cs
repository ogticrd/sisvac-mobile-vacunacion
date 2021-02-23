﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Refit;
using SisVac.Framework.Domain;

namespace SisVac.Framework.Http
{
    public interface ICitizensApiClient
    {
        [Get("/citizens/basic-data/")]
        Task<UserResponse> GetBasicData(string id);

        [Multipart]
        [Post("/citizens/vaccination-consent")]
        Task<dynamic> PostConsent(
            [AliasAs("cedula")] string cedula, [AliasAs("hasCovid")]bool hasCovid,
            [AliasAs("isPregnant")] bool isPregnant, [AliasAs("hadFever")] bool hadFever,
            [AliasAs("isVaccinated")] bool isVaccinated, [AliasAs("hadReactions")] bool hadReactions,
            [AliasAs("isAllergic")] bool isAllergic, [AliasAs("isMedicated")] bool isMedicated,
            [AliasAs("hasTransplant")] bool hasTransplant, [AliasAs("signature")] StreamPart Signature);

        [Get("/citizens/vaccination-consent")]
        Task<Consent> GetConsent(string id);

        [Get("/citizens/qualification")]
        Task<Qualification> GetQualification(string id);
    }
}