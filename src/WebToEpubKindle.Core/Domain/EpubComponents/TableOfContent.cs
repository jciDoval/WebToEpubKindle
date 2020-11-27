using System;
using System.Collections.Generic;
using System.Text;
using WebToEpubKindle.Core.Interfaces;

namespace WebToEpubKindle.Core.Domain.EpubComponents
{
    public class TableOfContent
    {
        private string _epubTitle;
        private Dictionary<string, string> _nodes;

        public string Title { get => _epubTitle; }
        public Dictionary<string,string> Nodes { get => _nodes; }

        public TableOfContent(string epubTitle)
        {
            _epubTitle = epubTitle;
            _nodes = new Dictionary<string, string>();
        }

        public void ChapterIndexer(Chapter chapter) => _nodes.Add(chapter.Abbreviation, chapter.Title);

    }
}