using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using SisVac.Framework.Domain;
using SQLite;

namespace SisVac.Framework.Data
{
    public class LocalDatabase
    {
        static SQLiteAsyncConnection _db;

        public static SQLiteAsyncConnection Connection
        {
            get
            {
                return _db;
            }
        }

        public async Task Initialize()
        {
            var databasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "LocalDatabase.db");
            _db = new SQLiteAsyncConnection(databasePath);

            await _db.CreateTableAsync<ClinicLocation>();


            await LoadSeeds();
        }

        async Task LoadSeeds()
        {
            if(await _db.Table<ClinicLocation>().CountAsync() == 0)
            {
                await _db.InsertAllAsync(SeedData.GetClinicLocations());
            }
        }
    }
}
