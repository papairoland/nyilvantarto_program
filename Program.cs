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
        }
    }
}



