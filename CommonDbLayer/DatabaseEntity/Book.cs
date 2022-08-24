using System;
using System.Collections.Generic;

namespace CommonDbLayer.DatabaseEntity
{
    public partial class Book
    {
        public long BookId { get; set; }
        public string? Logo { get; set; }
        public string Title { get; set; } = null!;
        public string? Category { get; set; }
        public decimal? Price { get; set; }
        public string? AuthorName { get; set; }
        public string? Publisher { get; set; }
        public DateTime? PublisedDate { get; set; }
        public string? Content { get; set; }
        public bool? Active { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
