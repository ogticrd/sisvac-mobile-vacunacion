﻿using Akavache;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Threading.Tasks;

namespace SisVac.Framework.Services
{

    public class CacheService : ICacheService
    {
        public CacheService()
        {
            //BlobCache.ApplicationName = "SisVac";
            Registrations.Start("SisVac");
        }

        #region Local
        public async Task<T> GetLocalObject<T>(string key)
        {
            try
            {
                return await BlobCache.LocalMachine.GetObject<T>(key);
            }
            catch (KeyNotFoundException)
            {
                return default(T);
            }
        }

        public async Task InsertLocalObject<T>(string key, T value)
        {
            await BlobCache.LocalMachine.InsertObject(key, value);
        }

        public async Task RemoveLocalObject(string key)
        {
            await BlobCache.LocalMachine.Invalidate(key);
        }
        #endregion

        #region Secure
        public async Task<T> GetSecureObject<T>(string key)
        {
            try
            {
                return await BlobCache.Secure.GetObject<T>(key);
            }
            catch (KeyNotFoundException)
            {
                return default(T);
            }
        }

        public async Task InsertSecureObject<T>(string key, T value)
        {
            await BlobCache.Secure.InsertObject(key, value);
        }

        public async Task RemoveSecureObject(string key)
        {
            await BlobCache.Secure.Invalidate(key);
        }
        #endregion
    }

    public static class CacheKeyDictionary
    {
        public const string UserCredentials = "userCredentials";

        public const string NotificationPush = "notificationPush";
        public const string AutomaticLogin = "automaticLogin";

        public const string RecentTransactions = "recentTransactions";
    }

    public interface ICacheService : ILocalMachineCache, ISecureCache
    {

    }

    public interface ILocalMachineCache
    {
        Task<T> GetLocalObject<T>(string key);

        Task InsertLocalObject<T>(string key, T value);

        Task RemoveLocalObject(string key);
    }

    public interface ISecureCache
    {
        Task<T> GetSecureObject<T>(string key);

        Task InsertSecureObject<T>(string key, T value);

        Task RemoveSecureObject(string key);
    }

}