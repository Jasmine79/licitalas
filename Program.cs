using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace AukcioProjekt
{
    class Festmeny
    {
        public static string cim;
        public static string festo;
        public static string stilus;
        
        public static int legmagasabbLicit = 100;
        public static DateTime legmagasabbLicitIdeje;
        public static bool elkelt = false;
        public Festmeny(string festo,string cim,string stilus)
        /* az UML diagramban Festmeny(cim, festo, stilus) sorrendben szerepel,
           de a minta alapján az általam beírt sorrend a helyes */
        {
            Festmeny.cim = cim;
            Festmeny.festo = festo;
            Festmeny.stilus = stilus;
        }
        public string getFesto()
        {
            return festo;
        }
        public string getStilus()
        {
            return stilus;
        }
        public static int LicitelesekSzama = 0;
        public static int getLicitelesekSzama(int LicitelesekSzama)
        {
            Console.WriteLine("Kérem adja meg a licitek számát.");
            LicitelesekSzama=int.Parse(Console.ReadLine());
            return LicitelesekSzama;
        }
        public static int licitekSzama = 0;
        static void Licit() //-- Az 1. feladat
        {
            for (int i = 0; i < getLicitelesekSzama(LicitelesekSzama); i++)
            {
                if (licitekSzama == getLicitelesekSzama(LicitelesekSzama))
                {
                    elkelt = true;
                    Console.WriteLine("Ez a festmény már elkelt. Nem lehet rá licitálni.");
                }
                else
                {
                    legmagasabbLicit = legmagasabbLicit * 110 / 100;
                    licitekSzama++;
                    legmagasabbLicitIdeje = DateTime.Now;
                }
            }
            Console.WriteLine($"A legmagasabb licit értéke: {legmagasabbLicit}");
            Console.WriteLine($"A legmagasabb licit időpontja: {legmagasabbLicitIdeje}");
        }
        static void Licit(int mertek) //-- A 2. feladat
        {
            Console.WriteLine("Kérem adja meg, hogy a licitelések alkalmával " +
                "mennyivel emelkedjen a licitek értéke.(10 és 100 között)");
            int.TryParse(Console.ReadLine(), out mertek);
            while (mertek < 10 || mertek > 100)
            {
                Console.WriteLine("Hibás értéket adott meg.");
                int.TryParse(Console.ReadLine(), out mertek);
            }
            for (int i = 0; i < getLicitelesekSzama(LicitelesekSzama); i++)
            {
                if (licitekSzama == getLicitelesekSzama(LicitelesekSzama))
                {
                    Console.WriteLine("Ez a festmény már elkelt. Nem lehet rá licitálni.");
                }
                else if (licitekSzama == 0)
                {
                    legmagasabbLicit = legmagasabbLicit * (mertek + 100) / 100;
                    licitekSzama++;
                    legmagasabbLicitIdeje = DateTime.Now;
                    Console.WriteLine($"A legmagasabb licit értéke: {legmagasabbLicit}");
                    Console.WriteLine($"A legmagasabb licit időpontja: {legmagasabbLicitIdeje}");
                }
                else
                {
                    Licit();
                }
            }

        }
        public override string ToString()
        {
            
            if (elkelt==true)
            {
                return String.Format($"{festo} : {cim} ({stilus})\n" +
                    $"Elkelt.\n{legmagasabbLicit} $ - {legmagasabbLicitIdeje}" +
                    $" (összesen: {licitekSzama} db)");
            }
            else
            {
                return String.Format($"{festo} : {cim} ({stilus})\n" +
                    $"{legmagasabbLicit} $ - {legmagasabbLicitIdeje}" +
                    $" (összesen: {licitekSzama} db)");
            }
        }
    }
    class Program
    {
        static List<Festmeny> festmenyek = new List<Festmeny>();
        static void Main(string[] args)
        {
            festmenyek.Add(new Festmeny("Barcsay Jenő", "Házak", 
                "konstruktivizmus"));
            festmenyek.Add(new Festmeny("Kassák Lajos", "Egyensúly", 
                "konstruktivizmus"));
            Hozzafuz();
            Beolvas();
            /* a festmény azonosításához használt sorszámok meghatározásához 
               segítségként.*/
            Console.WriteLine($"A listában {festmenyek.Count} festmény szerepel.");



            Console.WriteLine("Program vége.");
            Console.ReadKey();
        }
        public static int tovabbiakSzama;
        static void Hozzafuz()
        {
            Console.Write("Kérem adja meg, hogy hány festmény adatait " +
                "kívánja a listához hozzáadni: ");
            int.TryParse(Console.ReadLine(), out tovabbiakSzama);
            for (int i = 0; i < tovabbiakSzama; i++)
            {
                Console.WriteLine($"Kérem adja meg a(z) {i}. mű festőjét:");
                Festmeny.festo = Console.ReadLine();
                Console.WriteLine($"Kérem adja meg a(z) {i}. mű címét:");
                Festmeny.cim = Console.ReadLine();
                Console.WriteLine($"Kérem adja meg a(z) {i}. mű stilusát:");
                Festmeny.stilus = Console.ReadLine();
                festmenyek.Add(new Festmeny(Festmeny.festo,Festmeny.cim,
                    Festmeny.stilus));
            }
        }
        static void Beolvas()
        {
            StreamReader sr = new StreamReader(@"festmenyek.csv");
            while (!sr.EndOfStream)
            {
                string[] sor = sr.ReadLine().Split(';');
                festmenyek.Add(new Festmeny(sor[0], sor[1], sor[2]));
            }
            sr.Close();
        }
    }
}
