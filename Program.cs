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
            Console.WriteLine("Hello, World!");
        }
    }
}


