using System;
using System.Collections.Generic;
using System.IO;

namespace ConsoleApp2
{
    class Dolgozo
    {
        public string Nev { get; set; }
        public string Reszleg { get; set; }
        public int Fizetes { get; set; }
    }

    internal class Program
    {
        static List<Dolgozo> dolgozok = new List<Dolgozo>();
        static string fajlnev = "adatok.txt"; // A fájl neve egy helyen tárolva

        static void Main(string[] args)
        {
            // 1. BEOLVASÁS INDÍTÁSKOR
            Beolvasas();

            int valasztas = 0;
            while (valasztas != 5)
            {
                Console.Clear();
                Console.WriteLine("=== DOLGOZÓI NYILVÁNTARTÓ RENDSZER ===");
                Console.WriteLine($"Jelenleg {dolgozok.Count} dolgozó van az adatbázisban.");
                Console.WriteLine("--------------------------------------");
                Console.WriteLine("1. Dolgozók listázása");
                Console.WriteLine("2. Új dolgozó felvétele (Azonnali mentés)");
                Console.WriteLine("3. Dolgozó törlése (Azonnali mentés)");
                Console.WriteLine("4. Statisztikák");
                Console.WriteLine("5. Kilépés");
                Console.WriteLine("--------------------------------------");
                Console.Write("Válasszon menüpontot: ");

                if (!int.TryParse(Console.ReadLine(), out valasztas)) continue;

                switch (valasztas)
                {
                    case 1: Listazas(); break;
                    case 2: Felvetel(); break;
                    case 3: Torles(); break;
                    case 4: Statisztika(); break;
                }
            }
        }

        static void Beolvasas()
        {
            if (File.Exists(fajlnev))
            {
                try
                {
                    using (StreamReader sr = new StreamReader(fajlnev))
                    {
                        while (!sr.EndOfStream)
                        {
                            string sor = sr.ReadLine();
                            if (string.IsNullOrEmpty(sor)) continue;

                            // Pontosvessző mentén darabolunk
                            string[] adatok = sor.Split(';');

                            Dolgozo d = new Dolgozo();
                            d.Nev = adatok[0];
                            d.Reszleg = adatok[1];
                            d.Fizetes = int.Parse(adatok[2]);

                            dolgozok.Add(d);
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Hiba a beolvasáskor: " + e.Message);
                    Console.ReadKey();
                }
            }
        }

        static void Mentes()
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(fajlnev))
                {
                    for (int i = 0; i < dolgozok.Count; i++)
                    {
                        sw.WriteLine($"{dolgozok[i].Nev};{dolgozok[i].Reszleg};{dolgozok[i].Fizetes}");
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("\nMentési hiba: " + e.Message);
            }
        }

        static void Listazas()
        {
            Console.WriteLine("\n--- LISTA ---");
            if (dolgozok.Count == 0) Console.WriteLine("Üres az adatbázis.");
            for (int i = 0; i < dolgozok.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {dolgozok[i].Nev} [{dolgozok[i].Reszleg}] - {dolgozok[i].Fizetes} Ft");
            }
            Console.WriteLine("\nNyomjon egy gombot...");
            Console.ReadKey();
        }

        static void Felvetel()
        {
            Console.WriteLine("\n--- ÚJ DOLGOZÓ ---");
            Dolgozo uj = new Dolgozo();
            Console.Write("Név: "); uj.Nev = Console.ReadLine();
            Console.Write("Részleg: "); uj.Reszleg = Console.ReadLine();
            Console.Write("Fizetés: "); uj.Fizetes = int.Parse(Console.ReadLine());

            dolgozok.Add(uj);

            // VALÓS IDEJŰ MENTÉS
            Mentes();

            Console.WriteLine("Sikeresen mentve a fájlba!");
            Console.ReadKey();
        }

        static void Torles()
        {
            Console.Write("\nTörlendő sorszáma: ");
            if (int.TryParse(Console.ReadLine(), out int id) && id > 0 && id <= dolgozok.Count)
            {
                dolgozok.RemoveAt(id - 1);

                // VALÓS IDEJŰ MENTÉS
                Mentes();

                Console.WriteLine("Törölve és frissítve a fájl.");
            }
            else Console.WriteLine("Nincs ilyen sorszám.");
            Console.ReadKey();
        }

        static void Statisztika()
        {
            if (dolgozok.Count == 0)
            {
                Console.WriteLine("\nNincs adat a statisztikához.");
                Console.ReadKey();
                return;
            }

            Console.Clear();
            Console.WriteLine("=== RÉSZLETES STATISZTIKA ===");

            // 1. Átlag
            double osszeg = 0;
            for (int i = 0; i < dolgozok.Count; i++) osszeg += dolgozok[i].Fizetes;
            Console.WriteLine($"Átlagfizetés: {osszeg / dolgozok.Count:0.00} Ft");

            // 2. Legnagyobb fizetés
            int maxI = 0;
            for (int i = 1; i < dolgozok.Count; i++)
            {
                if (dolgozok[i].Fizetes > dolgozok[maxI].Fizetes) maxI = i;
            }
            Console.WriteLine($"Legjobban fizetett: {dolgozok[maxI].Nev} ({dolgozok[maxI].Fizetes} Ft) a(z) {dolgozok[maxI].Reszleg} részlegen");

            // 3. Legkisebb fizetés
            int minI = 0;
            for (int i = 1; i < dolgozok.Count; i++)
            {
                if (dolgozok[i].Fizetes < dolgozok[minI].Fizetes) minI = i;
            }
            Console.WriteLine($"Legkevesebbet keres: {dolgozok[minI].Nev} ({dolgozok[minI].Fizetes} Ft) {dolgozok[maxI].Reszleg} részlegen");

            // 4. Egy konkrét részleg statisztikája
            Console.WriteLine("------------------------------");
            Console.Write("Adja meg a részleget a szűréshez: ");
            string r = Console.ReadLine();
            int rDb = 0;
            for (int i = 0; i < dolgozok.Count; i++)
            {
                if (dolgozok[i].Reszleg.Equals(r, StringComparison.OrdinalIgnoreCase)) rDb++;
            }
            Console.WriteLine($"A(z) {r} részlegen dolgozik: {rDb} fő.");

            Console.WriteLine("\nNyomjon egy gombot a visszatéréshez...");
            Console.ReadKey();
        }
    }
}
