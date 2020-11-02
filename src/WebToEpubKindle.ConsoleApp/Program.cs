using System;
using WebToEpubKindle.Core.Domain;
using WebToEpubKindle.Core.Infrastructure;

namespace WebToEpubKindle.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Inicio del aplicativo....");
            Epub epub = Epub.Create(null);
            EpubWriter.WriteToDisk(epub, @"D:\", "epubtest.txt");
            Console.WriteLine("Fin del aplicativo. Pulse una tecla para finalizar");
            Console.ReadLine();

        }
    }
}
