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
        static string fajlnev = "adatok.txt";
        static void Main(string[] args)
        {
            int valasztas = 0;
            while (valasztas != 5){
                Console.Clear();
                Console.WriteLine("=== DOLGOZÓI NYILVÁNTARTÓ RENDSZER ===");
                Console.WriteLine("1. Listázás\n2. Felvétel\n3. Törlés\n4. Statisztika\n5. Kilépés");
                Console.Write("Válasszon: ");
                if (!int.TryParse(Console.ReadLine(), out valasztas)) continue;
            }

            static void Felvetel(){
                Console.WriteLine("\n--- ÚJ DOLGOZÓ ---");
                Dolgozo uj = new Dolgozo();
                Console.Write("Név: "); uj.Nev = Console.ReadLine();
                Console.Write("Részleg: "); uj.Reszleg = Console.ReadLine();
                Console.Write("Fizetés: "); uj.Fizetes = int.Parse(Console.ReadLine());
                dolgozok.Add(uj);
                Mentes();
                Console.WriteLine("Sikeresen mentve a fájlba!");
            }

            static void Listazas(){
                if (dolgozok.Count == 0) Console.WriteLine("Üres az adatbázis.");
                Console.WriteLine("\n--- LISTA ---");
                for (int i = 0; i < dolgozok.Count; i++){
                Console.WriteLine($"{i + 1}. {dolgozok[i].Nev} [{dolgozok[i].Reszleg}] - {dolgozok[i].Fizetes} Ft");
                }
                Console.ReadKey();
                }
            static void Mentes(){
                using (StreamWriter sw = new StreamWriter(fajlnev)){
                    for (int i = 0; i < dolgozok.Count; i++){
                        sw.WriteLine($"{dolgozok[i].Nev};{dolgozok[i].Reszleg};{dolgozok[i].Fizetes}");
                    }
                }
            }
         }
    }
}







