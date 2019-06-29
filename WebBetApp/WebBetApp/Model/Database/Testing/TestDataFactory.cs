using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebBetApp.Model.Database.DatabaseModel;

namespace WebBetApp.Model.Database.Testing
{
    public class TestDataFactory
    {
        public static void Fill(WebBetDbContext db)
        {
            if (!db.Sports.Any() || !db.Matches.Any())
            {
                InsertSports(db);
                InsertMatches(db);

                
            }

            if (!db.Wallet.Any())
            {
                 var initalBalance = new Wallet { Balance = 0 };
                 db.Wallet.Add(initalBalance);

                 db.SaveChanges();
            }
        }

        private static void InsertMatches(WebBetDbContext db)
        {
            new[]
            {
                new Match
                {
                    Pair = "Španjolska U21 - Francuska U21",
                    Type1 = 2.20,
                    TypeX = 3.50,
                    Type2 = 3.80,
                    Type1X = 1.40,
                    TypeX2 = 1.85,
                    Type12 = 1.40, 
                    IsPartOfTopOffer = true,
                    SportId = 1
                },
                new Match
                {
                    Pair = "Njemačka U21 - Rumunjska U21",
                    Type1 = 1.60,
                    TypeX = 4.60,
                    Type2 = 3.80,
                    Type1X = 1.40,
                    TypeX2 = 1.85,
                    Type12 = 1.30,
                    IsPartOfTopOffer = true,
                    SportId = 1
                },
                new Match
                {
                    Pair = "Brazil - Paragvaj",
                    Type1 = 1.20,
                    TypeX = 8.00,
                    Type2 = 25.00,
                    Type1X = 1.05,
                    TypeX2 = 6.00,
                    Type12 = 1.15,
                    IsPartOfTopOffer = true,
                    SportId = 1
                },
                new Match
                {
                    Pair = "Madagaskar-Burundi",
                    Type1 = 2.80,
                    TypeX = 2.90,
                    Type2 = 2.70,
                    Type1X = 1.45,
                    TypeX2 = 1.40,
                    Type12 = 1.40,
                    IsPartOfTopOffer = false,
                    SportId = 1
                },
                new Match
                {
                    Pair = "MiKi Mikkeli-JIPPO Joensuu",
                    Type1 = 2.40,
                    TypeX = 3.20,
                    Type2 = 2.50,
                    Type1X = 1.35,
                    TypeX2 = 1.40,
                    Type12 = 1.20,
                    IsPartOfTopOffer = false,
                    SportId = 1
                },
                new Match
                {
                    Pair = "Instituto-S.LorenAlm",
                    Type1 = 1.80,
                    TypeX = 13.00,
                    Type2 = 2.10,
                    Type1X = 1.60,
                    TypeX2 = 1.80,
                    Type12 = null,
                    IsPartOfTopOffer = false,
                    SportId = 2
                },
                new Match
                {
                    Pair = "Los Angeles-Las Vegas",
                    Type1 = 1.90,
                    TypeX = 13.00,
                    Type2 = 2.00,
                    Type1X = 1.65,
                    TypeX2 = 1.70,
                    Type12 = null,
                    IsPartOfTopOffer = true,
                    SportId = 2
                },
                new Match
                {
                    Pair = "Francuska - Češka",
                    Type1 = 1.10,
                    TypeX = 18.00,
                    Type2 = 3.80,
                    Type1X = null,
                    TypeX2 = 5.00,
                    Type12 = null,
                    IsPartOfTopOffer = false,
                    SportId = 2
                },
                new Match
                {
                    Pair = "Mannarino A.-Sonego L.",
                    Type1 = 1.40,
                    TypeX = null,
                    Type2 = 2.50,
                    Type1X = null,
                    TypeX2 = null,
                    Type12 = null,
                    IsPartOfTopOffer = false,
                    SportId = 4
                },
                 new Match
                {
                    Pair = "Tomic B.-Carreno Busta P.",
                    Type1 = 1.60,
                    TypeX = null,
                    Type2 = 2.10,
                    Type1X = null,
                    TypeX2 = null,
                    Type12 = null,
                    IsPartOfTopOffer = true,
                    SportId = 4
                },
                 new Match
                {
                    Pair = "Kina - Argentina",
                    Type1 = 3.60,
                    TypeX = null,
                    Type2 = 1.20,
                    Type1X = null,
                    TypeX2 = null,
                    Type12 = null,
                    IsPartOfTopOffer = false,
                    SportId = 5
                },
                  new Match
                {
                    Pair = "Rumunjska - Slovenija",
                    Type1 = 1.85,
                    TypeX = null,
                    Type2 = 1.75,
                    Type1X = null,
                    TypeX2 = null,
                    Type12 = null,
                    IsPartOfTopOffer = false,
                    SportId = 5
                },
            }.ToList().ForEach(match => db.Matches.Add(match));

            db.SaveChanges();
        }

        private static void InsertSports(WebBetDbContext db)
        {
            new[]
            {
                new Sport
                {
                    Name = "Nogomet"

                },
                new Sport
                {
                    Name = "Košarka"

                },
                new Sport
                {
                    Name = "Rukomet"

                },
                new Sport
                {
                    Name = "Tenis"

                },
                new Sport
                {
                    Name = "Odbojka"

                }
            }.ToList().ForEach(sport => db.Sports.Add(sport));

            db.SaveChanges();
        }
    }
}
