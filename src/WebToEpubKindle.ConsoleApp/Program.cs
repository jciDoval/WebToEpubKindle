using System;
using System.Collections.Generic;
using WebToEpubKindle.Core.Domain;
using WebToEpubKindle.Core.Infrastructure;

namespace WebToEpubKindle.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Inicio del aplicativo....");
            Page page = new Page("This is a good joke 0...");
            Page page1 = new Page("This is a good joke 1...");
            Page page2 = new Page("This is a good joke 2...");
            Chapter chapter1 = new Chapter("Chapter 1", new List<Page>() { page, page1, page2 });
            Chapter chapter2 = new Chapter("Chapter 2", new List<Page>() { page, page1, page2 });
            Epub epub = Epub.Create(new List<Chapter>() { chapter1, chapter2 });
            EpubWriter writer = EpubWriter.Initialize(epub);
            writer.CreateEpub(@"", "epubtest");
            Console.WriteLine("Fin del aplicativo. Pulse una tecla para finalizar");
            Console.ReadLine();

        }
    }
}
