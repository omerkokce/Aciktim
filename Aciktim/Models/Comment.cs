using System;
using System.Collections.Generic;

namespace Aciktim.Models
{
    public partial class Comment
    {
        public int CommentId { get; set; }
        public int ClientId { get; set; }
        public int RestaurantId { get; set; }
        public DateTime? Date { get; set; }
        public string Text { get; set; } = null!;

        public virtual Client Client { get; set; } = null!;
        public virtual Restaurant Restaurant { get; set; } = null!;
    }
}
