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
            Page page = Page.Create("This is a good joke 0...",null);
            Page page1 = Page.Create("This is a good joke 1...",null);
            Page page2 = Page.Create("This is a good joke 2...",null);
            Chapter chapter1 = new Chapter("Chapter 1", new List<Page>() { page, page1, page2 });
            Chapter chapter2 = new Chapter("Chapter 2", new List<Page>() { page, page1, page2 });
            Epub epub = Epub.Create("My first epub.");
            epub.ChapterList.AddChapter(chapter1);
            epub.ChapterList.AddChapter(chapter2);
            EpubWriter writer = EpubWriter.Initialize(epub);
            writer.CreateEpub(@"", "epubtest");
            Console.WriteLine("Fin del aplicativo. Pulse una tecla para finalizar");
            Console.ReadLine();

        }
    }
}
