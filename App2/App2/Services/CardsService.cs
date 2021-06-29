using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using App2.Models;
using App2.Services;
using SQLite;
using Xamarin.Essentials;
using Xamarin.Forms;

[assembly: Dependency(typeof(CardsService))]
namespace App2.Services
{
    public static class CardsService 
    {
       static  SQLiteAsyncConnection db;
        static async Task Init()
            
        {
            if (db != null)
                return;
            var databasePath = Path.Combine(FileSystem.AppDataDirectory, "MyData.db");

            db = new SQLiteAsyncConnection(databasePath);

            await db.CreateTableAsync<Cards>();
        }

        public static async Task AddCards(string name, string group)
        {
            await Init();
            var image = "onmoderation.png";
            var cards = new Cards
            {
                Name = name,
                Group = group,
                Image = image
            };
        }

        public static async Task RemoveCards(int id)
        {
            await Init();
            await db.DeleteAsync<Cards>(id);
        }

        public static async Task<IEnumerable<Cards>> GetCards()
        {
            await Init();

           var cards =  await db.Table<Cards>().ToListAsync();
           return cards;
        }
    }
}
