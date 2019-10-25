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
        private string cim;
        private string festo;
        private string stilus;

        private int legmagasabbLicit = 100;
        private DateTime legmagasabbLicitIdeje;
        private bool elkelt = false;
        public Festmeny(string festo, string cim, string stilus)
        /* az UML diagramban Festmeny(cim, festo, stilus) sorrendben szerepel,
           de a minta alapján az általam beírt sorrend a helyes */
        {
            this.cim = cim;
            this.festo = festo;
            this.stilus = stilus;
        }
        public string getFesto()
        {
            return festo;
        }
        public string getStilus()
        {
            return stilus;
        }
        public string getCim()
        {
            return cim;
        }
        public static int licitekSzama = 0;
        public bool Elkelt()
        {
            return elkelt;
        }
        public int getLegmagasabbLicit()
        {
            return legmagasabbLicit;
        }
        
        public void Licit() //-- Az 1. feladat
        {
            if (elkelt)
            {
                //elkelt = true;
                Console.WriteLine("Ez a festmény már elkelt. Nem lehet rá licitálni.");
            }
            else
            {
                legmagasabbLicit = legmagasabbLicit * 110 / 100;
                licitekSzama++;
                legmagasabbLicitIdeje = DateTime.Now;
                if ((DateTime.Now - legmagasabbLicitIdeje).Minutes > 2)
                {
                    elkelt = true;
                }
            }

            Console.WriteLine($"A legmagasabb licit értéke: {legmagasabbLicit}");
            Console.WriteLine($"A legmagasabb licit időpontja: {legmagasabbLicitIdeje}");
        }
        public void Licit(int merek) //-- A 2. feladat
        {

            while (merek < 10 || merek > 100)
            {
                Console.WriteLine("Hibás értéket adott meg.");
                int.TryParse(Console.ReadLine(), out merek);
            }
            
            if (elkelt)
            {
                Console.WriteLine("Ez a festmény már elkelt. Nem lehet rá licitálni.");
                elkelt = true;
            }
            else if (licitekSzama == 0)
            {
                legmagasabbLicit = legmagasabbLicit * (merek + 100) / 100;
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
        public override string ToString()
        {
            if (elkelt == true)
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
        public static void Main(string[] args)
        {
            festmenyek.Add(new Festmeny("Barcsay Jenő", "Házak",
              "konstruktivizmus"));
            festmenyek.Add(new Festmeny("Kassák Lajos", "Egyensúly",
              "konstruktivizmus"));
            Hozzafuz();
            Beolvas();
            foreach (Festmeny item in festmenyek)
            {
                int merek;
                Console.WriteLine($"Kérem adja meg, " +
                $"hogy milyen mértékben emelkedjen a licitek értéke " +
                $"{item.getFesto()} festőnek a(z) " +
                $"{item.getCim()} c. képére licitálás esetében (10 és 100 között)");
                int.TryParse(Console.ReadLine(), out merek);
                while (merek < 10 || merek > 100)
                {
                    Console.WriteLine("Hibás adatot adott meg. " +
                        "Adjon meg másik értéket, kérem.");
                    int.TryParse(Console.ReadLine(), out merek);
                } 
                item.Licit(merek);
            }
            foreach (Festmeny item in festmenyek)
            {
                item.Licit();
            }
            int kivalasztasKep;
            do
            {
                try //-- itt a hibakezelést próbálom végrehajtatni
                {
                     /*itt próbálom meghatározni, hogy nem megfelelő szám
                    esetén mit csináljon a gép*/
                    
                        int.TryParse(Console.ReadLine(), out kivalasztasKep);
                        while (festmenyek[kivalasztasKep].Elkelt())
                        {
                            Console.WriteLine("Ez a kép már elkelt. Kérem adja meg, " +
                            "hogy melyik képre akar licitálni.");
                            int.TryParse(Console.ReadLine(), out kivalasztasKep);
                        }
                        while (kivalasztasKep < 0 || kivalasztasKep > festmenyek.Count)
                        {
                            Console.WriteLine("Ilyen sorszámú kép nem létezik.");
                            int.TryParse(Console.ReadLine(), out kivalasztasKep);
                        }
                    /* a felhasználó licitálásához itt kell meghívni az eljárást szerintem*/
                    felhasznaloLicitalasa();
                }
                catch (InvalidDataException)
                {
                    Console.WriteLine("Hibás adatforma.");
                    throw;
                }
            } while (kivalasztasKep != 0);
            
            Console.WriteLine($"A legdrágábban elkelt festmény adatai:\n" +
               $"{festmenyek.Max(x => x.getLegmagasabbLicit()).ToString()}");
            /* a festmény azonosításához használt sorszámok meghatározásához 
               segítségként.*/
            Console.WriteLine($"A listában {festmenyek.Count} festmény szerepel.");
            for (int j = 0; j < 20; j++)
            {
                Random random = new Random();
                int kep = random.Next(0, (festmenyek.Count));
                int ertek = random.Next(10, 101);
                festmenyek[kep].Licit(ertek);
                Console.WriteLine(festmenyek[kep].ToString());
            }
            int ElkeltekSzama=0, NemElkeltekSzama=0 ;
            foreach (Festmeny item in festmenyek)
            {
                if (Festmeny.licitekSzama > 0)
                {
                    item.Elkelt();
                    Console.WriteLine($"{item.getFesto()} festőnek a(z) " +
                $"{item.getCim()} c. képe elkelt.");
                    ElkeltekSzama++;
                }
                else
                {
                    Console.WriteLine($"{item.getFesto()} festőnek a(z) " +
                $"{item.getCim()} c. képe még nem kelt el.");
                    NemElkeltekSzama++;
                }                    
            }
            Console.WriteLine($"{NemElkeltekSzama} db festmény " +
                $"nem kelt el az aukción.");
            List<Festmeny> csokkenessel = new List<Festmeny>();
            int[] csokkeno = new int[festmenyek.Count];
            for (int i = 0; i < festmenyek.Count; i++)
            {
                csokkeno[i]=festmenyek[i].getLegmagasabbLicit();
            }
            var csokkenosor= csokkeno.OrderByDescending(item => item);
            foreach (Festmeny item in festmenyek)
            {
                Console.WriteLine(item.ToString());
                csokkenessel.Add(item);
            }
            
            StreamWriter sw = null;
            try
            {
                using (sw=new StreamWriter(@"festmenyek_rendezett.csv", true, Encoding.Default))
                {
                    for (int i = 0; i < festmenyek.Count; i++)
                    {
                        sw.WriteLine(csokkenessel[i]);
                    }
                }
            }
            catch(IOException e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
            finally
            {
                sw.Close();
                sw.Dispose();
            }
            Console.WriteLine("Program vége.");
            Console.ReadKey();
        }
        static void felhasznaloLicitalasa()
        {
            int kivalasztasKep = 1;
            Console.WriteLine("Kérem adja meg, hogy melyik képre akar " +
              "licitálni");
            while (festmenyek[kivalasztasKep].Elkelt())
            {
                Console.WriteLine("Ez a kép már elkelt. Kérem adja meg, " +
                  "hogy melyik képre akar licitálni.");
                int.TryParse(Console.ReadLine(), out kivalasztasKep);
            } 
            int.TryParse(Console.ReadLine(), out kivalasztasKep);
            int kivalasztasErtek = 1;
            Console.WriteLine("kérem adja meg, hogy mekkora összeggel akar licitálni");
            int.TryParse(Console.ReadLine(), out kivalasztasErtek);
            festmenyek[kivalasztasKep - 1].Licit(kivalasztasErtek);
        }
        public static int tovabbiakSzama;
        static void Hozzafuz()
        {
            Console.Write("Kérem adja meg, hogy hány festmény adatait " +
              "kívánja a listához hozzáadni: ");
            int.TryParse(Console.ReadLine(), out tovabbiakSzama);
            for (int i = 0; i < tovabbiakSzama; i++)
            {
                Console.WriteLine($"Kérem adja meg a(z) {i+1}. mű festőjét:");
                string ujfesto = Console.ReadLine();
                Console.WriteLine($"Kérem adja meg a(z) {i+1}. mű címét:");
                string ujcim = Console.ReadLine();
                Console.WriteLine($"Kérem adja meg a(z) {i+1}. mű stilusát:");
                string ujstilus = Console.ReadLine();
                festmenyek.Add(new Festmeny(ujfesto, ujcim,
                  ujstilus));
            }
        }
        static void Beolvas()
        {
         StreamReader sr = new StreamReader(@"C:\Users\VidaZsuzsa\source\repos\AukcioProjekt\AukcioProjekt\festmenyek.csv"); //-- <--ez az otthoni elérési út
        
         /*StreamReader sr = new StreamReader(@"Y:\ESZF\orai_feladatok\
          * Bedando\festmenyek.csv");  <-- ez a benti cím*/
            while (!sr.EndOfStream)
            {
                string[] sor = sr.ReadLine().Split(';');
                festmenyek.Add(new Festmeny(sor[0], sor[1], sor[2]));
            }
            sr.Close();
        }
    }
}
