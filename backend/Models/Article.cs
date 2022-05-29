using System;
using System.Collections.Generic;

namespace backendAg.Models
{
    public partial class Article
    {
        public int ArticleId { get; set; }
        public string Tittle { get; set; } = null!;
        public string Author { get; set; } = null!;
        public int AuthorId { get; set; }
        public string Content { get; set; } = null!;
        public string PublicationDate { get; set; } = null!;

        public virtual Administrator AuthorNavigation { get; set; } = null!;
    }
}
