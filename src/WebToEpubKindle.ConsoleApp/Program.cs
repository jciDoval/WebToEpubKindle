using System;
using System.Collections.Generic;
using System.Globalization;
using WebToEpubKindle.Core.Domain;
using WebToEpubKindle.Core.Domain.Enum;
using WebToEpubKindle.Core.Domain.EpubComponents;
using WebToEpubKindle.Core.Infrastructure;
using WebToEpubKindle.Core.Interfaces;

namespace WebToEpubKindle.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            const string _contentpage = @"Lorem ipsum dolor sit amet, consectetur adipiscing elit. Maecenas quis condimentum justo. Duis quis blandit dui. Suspendisse ornare maximus magna, nec egestas nunc molestie ac. Vestibulum vel malesuada magna, a ornare felis. Cras dapibus nibh at metus rhoncus gravida. Duis sodales finibus ipsum sed lobortis. Cras nec diam turpis. Suspendisse odio elit, cursus in elementum nec, posuere ut ipsum. Fusce vitae nisl sem. Sed nec suscipit turpis. Integer nec euismod nulla.

In eget tortor venenatis lectus semper feugiat. Sed eleifend leo sit amet lobortis laoreet. Donec dignissim non massa id tincidunt. Aliquam mattis placerat vulputate. Vestibulum id risus varius, consequat urna nec, condimentum turpis. Maecenas lacinia ac augue ut mollis. Pellentesque consequat turpis lacus, non placerat nisi rhoncus id.

Donec quis mi non nulla maximus finibus. Duis in quam ac ex convallis ornare. Proin fringilla gravida purus, vel fermentum orci rutrum sodales. Vivamus aliquam est ut aliquet cursus. Nunc vulputate laoreet auctor. Proin in velit non urna egestas accumsan eu vitae neque. Sed feugiat, tellus sagittis rhoncus tristique, lectus nunc pretium erat, in tristique est odio vel lorem. Nunc bibendum convallis dui in egestas. In sed dui eget erat ornare cursus a vitae felis.

Nunc efficitur pharetra turpis. Integer dictum metus rutrum, vehicula neque ut, sodales ante. Pellentesque sodales rutrum velit, non mattis nisi ornare id. Mauris non orci nec magna auctor rhoncus sit amet vel neque. Aenean cursus ullamcorper quam, eget semper nulla aliquet sit amet. Sed nec varius orci. Morbi eget maximus lorem. Mauris et odio sit amet urna egestas varius. Cras libero neque, tempor eu ultrices at, vehicula ac diam. Sed ut nulla nibh. Donec lobortis lorem eu nunc fringilla, sed bibendum ante aliquet. Suspendisse et elit eleifend quam mattis molestie nec a augue. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia curae; Nullam mi libero, fringilla eget ipsum a, pulvinar imperdiet ipsum. Nullam dictum accumsan velit, ut eleifend neque facilisis a. Sed quis metus sagittis, bibendum nisl nec, tincidunt elit.

Nam iaculis est id accumsan tempus. Quisque in ornare sapien, id ornare tortor. Morbi tristique, neque non maximus sagittis, velit massa lobortis augue, vel lobortis dolor eros nec nisl. Integer vulputate in est molestie rutrum. Ut ac erat sit amet sem egestas ultricies. Curabitur consequat massa vel vulputate porttitor. In hac habitasse platea dictumst. In erat metus, dapibus eu massa aliquam, auctor malesuada lectus. Pellentesque dui nisi, faucibus vitae mi vel, consequat aliquam lacus.

Nunc molestie ligula ante, ac accumsan nibh ultricies in. Etiam sagittis purus quis rhoncus fermentum. Vestibulum hendrerit, orci ut aliquam iaculis, felis dolor aliquam quam, at facilisis mi arcu in risus. Donec posuere sem vel fermentum tincidunt. Phasellus ligula ante, congue et euismod pulvinar, volutpat et nunc. Etiam eu mauris sit amet nulla molestie imperdiet eget in nisl. Etiam vel lacus justo. Proin eget ante vitae nulla consectetur vulputate lacinia in nunc. Duis ligula elit, lacinia eu dui facilisis, aliquam ultrices lorem.

Aliquam porta, diam ac imperdiet fringilla, ex mi euismod nibh, et consequat nulla nisi at massa. Duis bibendum sagittis ornare. Aenean quis eleifend lorem. Nulla sed libero ultrices, sollicitudin eros id, placerat nisi. Nullam in velit tortor. Maecenas euismod ex non tellus scelerisque auctor. Praesent accumsan neque ac pellentesque eleifend. Pellentesque eu velit nec mauris accumsan vulputate eu et sapien. In viverra est dolor, vel vulputate orci mattis at. Nam vel tincidunt lacus, sit amet varius purus. Nam mollis nulla sem, vitae finibus lorem aliquam et. Sed interdum nisl at sollicitudin interdum.

Nulla consectetur lectus non rutrum vestibulum. In justo mauris, porttitor sit amet neque ac, pulvinar finibus orci. Etiam eget neque dolor. Donec urna ante, commodo id pulvinar sed, aliquam at dui. Integer lobortis augue eu ligula luctus, sit amet sagittis ex mollis. Maecenas lacinia magna ut facilisis tincidunt. Integer sed aliquet tellus. Maecenas eget luctus lectus, eget vulputate ex. Fusce porttitor varius tortor, vestibulum lacinia lorem tempor vel. Nullam mi orci, fermentum nec mi eu, vestibulum luctus magna.

Donec a justo diam. Praesent sit amet magna egestas, dapibus est at, porttitor nisl. Aliquam quis purus arcu. Pellentesque blandit felis ante. Mauris eleifend suscipit orci vitae cursus. Praesent finibus ultrices mi non pretium. Curabitur elementum risus sit amet dui feugiat pharetra. Sed vehicula quis ex a rhoncus. Aenean varius purus erat, vel imperdiet lectus condimentum in. Vivamus risus velit, imperdiet a consequat sed, pulvinar a velit.

Fusce lobortis, orci a lacinia tempus, turpis lectus eleifend justo, a sagittis massa nisi tincidunt nibh. Nullam vehicula erat ac nibh venenatis scelerisque. Pellentesque dictum dui sed justo venenatis vulputate. In euismod arcu et justo venenatis, in auctor augue pulvinar. Fusce vehicula consectetur leo, sit amet auctor nulla lacinia ut. Donec eu enim nec libero faucibus ultrices et nec nunc. Vestibulum porta nisl risus, id sagittis lacus hendrerit nec. Proin placerat imperdiet neque, id finibus lectus aliquet tincidunt. Proin in erat leo. Aliquam erat volutpat. Praesent suscipit nec nisl at posuere. Nullam ligula urna, consectetur vel iaculis gravida, tristique et lacus.

Ut at nulla pulvinar, commodo tellus nec, blandit mauris. Aliquam sed aliquet nisl, eu auctor lectus. Nulla id lectus elementum, luctus urna eu, tempus eros. Vestibulum blandit pretium ante ut eleifend. Aliquam nec pharetra libero. Donec in luctus justo, vitae dapibus nulla. Cras efficitur, mi et aliquet tristique, purus leo pretium lectus, a rutrum nunc tellus vitae tortor. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia curae; Etiam eu efficitur quam. Aliquam condimentum felis eget laoreet tempor.

Suspendisse sodales tellus elit, sit amet sollicitudin ligula elementum in. Fusce et urna nec nibh congue tristique eu at eros. Donec vitae cursus purus. Quisque vitae sodales risus. Nullam sollicitudin lacus nec mattis rutrum. Sed ac augue ipsum. Etiam blandit nulla tincidunt fermentum tristique. Maecenas at felis vestibulum, vestibulum tellus eu, porta turpis. Aliquam interdum eget orci eget sagittis. Nulla volutpat orci eget libero cursus, at porttitor velit varius. Aenean ut arcu eu sem fringilla ullamcorper et sed urna. Aenean semper aliquet diam, non blandit dui eleifend et. Praesent imperdiet nisi ac ligula malesuada aliquam. Quisque eleifend posuere leo ac dictum. Ut hendrerit urna non massa tristique faucibus.

Cras fringilla, risus vel cursus blandit, libero augue feugiat ante, et sagittis dolor leo nec dui. Cras consequat diam quis convallis ultricies. Sed nec luctus magna, vitae auctor augue. Aliquam erat volutpat. Vivamus malesuada venenatis lorem, in consequat neque venenatis eget. Donec id pellentesque eros. Etiam quis nulla consectetur, volutpat nisi ac, sollicitudin arcu. Etiam in mi eleifend, consequat tellus et, tincidunt libero. Vivamus sed nulla sed metus vulputate condimentum quis at arcu. Aliquam tempus neque at quam placerat gravida. Nunc aliquet nibh hendrerit hendrerit faucibus. Morbi posuere et sapien ut congue.

Maecenas pellentesque dolor eget lacus bibendum suscipit. Donec a vehicula ipsum, a viverra ex. Aliquam varius posuere nisl id sagittis. Donec eu purus scelerisque, laoreet ex vel, bibendum nunc. Etiam id arcu purus. Donec eget ex gravida, cursus mi ac, congue mi. Etiam placerat libero et dui consequat fringilla. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia curae; Suspendisse varius sagittis porttitor. Nunc ullamcorper a tortor placerat tempus. Vestibulum vitae tortor lorem. Mauris rhoncus in ante sit amet mollis.

Nunc finibus mi euismod malesuada ullamcorper. Cras imperdiet, diam eu tincidunt ultrices, felis massa congue ante, id porttitor felis ex vel sapien. Nam ultrices sem sed molestie pretium. Integer at lorem at purus efficitur accumsan. Quisque vitae convallis arcu. Cras justo est, vulputate quis aliquet et, fringilla viverra nibh. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia curae; Vestibulum consequat semper orci tincidunt mollis. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos.";

            Console.WriteLine("Inicio del aplicativo....");

            Page page = Page.Create(_contentpage, null);
            Page page1 = Page.Create(_contentpage, null);
            Page page2 = Page.Create(_contentpage, null);

            Chapter chapter1 = new Chapter("Chapter 1", new List<Page>() { page, page1, page2 });
            Chapter chapter2 = new Chapter("Chapter 2", new List<Page>() { page, page1, page2 });


            Epub epub = EpubFactory.Initialize(EpubVersion.V3_0, "versioning", new CultureInfo("en-EN"))
                                   .BuildInstance();

            epub.ChapterList.AddChapter(chapter1);
            epub.ChapterList.AddChapter(chapter2);

            IFileCreator creator = FileEpubCreator.Initialize(EpubVersion.V3_0, epub)
                                                  .BuildCreator();

            creator.Create("", "My first epub with versioning");

            Console.WriteLine("Fin del aplicativo. Pulse una tecla para finalizar");
            Console.ReadLine();

        }
    }
}
