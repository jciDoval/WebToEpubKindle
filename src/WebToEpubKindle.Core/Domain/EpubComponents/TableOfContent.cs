using System;
using System.Collections.Generic;
using System.Text;
using WebToEpubKindle.Core.Interfaces;

namespace WebToEpubKindle.Core.Domain.EpubComponents
{
    public class TableOfContent
    {
        private string _epubTitle;
        private List<TableOfContentItem> _nodes;

        public string Title { get => _epubTitle; }
        public List<TableOfContentItem> Nodes { get => _nodes; }

        public TableOfContent(string epubTitle)
        {
            _epubTitle = epubTitle;
            _nodes = new List<TableOfContentItem>();
        }

        public void ChapterIndexer(Chapter chapter) => _nodes.Add(new TableOfContentItem(chapter.Abbreviation, 
                                                                                         chapter.FileName,
                                                                                         chapter.Title));

    }
}