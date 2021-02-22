using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using SisVac.Framework.Domain;
using SQLite;

namespace SisVac.Framework.Data
{
    public class LocalDatabase
    {
        static SQLiteAsyncConnection _db;

        public SQLiteAsyncConnection Connection
        {
            get
            {
                return _db;
            }
        }

        public async Task Initialize()
        {
            if(_db == null)
            {
                var databasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "LocalDatabase.db");

                _db = new SQLiteAsyncConnection(databasePath);

                await _db.CreateTableAsync<ClinicLocation>();
                await LoadSeeds();
            }
        }

        async Task LoadSeeds()
        {
            if(await _db.Table<ClinicLocation>().CountAsync() == 0)
            {
                var locationsScript = ReadResourceFile("SisVac.Framework.Data.Scripts.ClinicLocations.sql");
                await _db.ExecuteAsync(locationsScript);
            }
        }

        private string ReadResourceFile(string filename)
        {
            var thisAssembly = Assembly.GetExecutingAssembly();
            using (var stream = thisAssembly.GetManifestResourceStream(filename))
            {
                using (var reader = new StreamReader(stream))
                {
                    return reader.ReadToEnd();
                }
            }
        }
    }
}
