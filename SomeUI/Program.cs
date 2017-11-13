using SamuraiApp.Data;
using SamuraiApp.Domain;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System;
using Microsoft.EntityFrameworkCore;

namespace SomeUI
{
    internal class Program
    {
        private static SamuraiContext _context = new SamuraiContext();

        private static void Main(string[] args)
        {
            _context.GetService<ILoggerFactory>().AddProvider(new MyLoggerProvider());

            //InsertSamurai();
            //InsertMultipleSamurais();
            //DeleteWhileTrack();
            SimpleSamuraiQuery();
            FirtsOrDefault();
            RawSqlQuery();
        }

        private static void InsertSamurai()
        {
            var samurai = new Samurai
            {
                Name = "Julie"
            };

            _context.Samurais.Add(samurai);
            _context.SaveChanges();
        }

        private static void InsertMultipleSamurais()
        {
            var samurai = new Samurai
            {
                Name = "Satoshi Nakamoto"
            };

            var samurai2 = new Samurai
            {
                Name = "Marcus Flavius Teilus"
            };

            _context.Samurais.AddRange(new List<Samurai> { samurai, samurai2 });
            _context.SaveChanges();
        }

        private static void SimpleSamuraiQuery()
        {
            var name = "Julie";
            var samurais = _context.Samurais.ToList();

            foreach (var samurai in _context.Samurais.Where(s => s.Name == name).ToList())
            {
                Console.WriteLine(samurai.Name);
            }
        }

        private static void FirtsOrDefault()
        {
            var name = "Julie";
            var samurai = _context.Samurais.FirstOrDefault(s => s.Name == name);

            Console.WriteLine("Name " + samurai.Name + ", ID " + samurai.Id);
        }

        private static void DeleteWhileTrack()
        {
            var name = "Satoshi Nakamoto";
            var samurai = _context.Samurais.FirstOrDefault(s => s.Name == name);

            _context.Samurais.Remove(samurai);
            _context.SaveChanges();
        }

        private static void RawSqlQuery()
        {
            var query = "SELECT * FROM Samurais";
            var samurais = _context.Samurais.FromSql(query).ToList();

            samurais.ForEach(s => Console.WriteLine(s.Name));
            Console.WriteLine();
        }
    }
}