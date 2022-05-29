using System;
using System.Collections.Generic;

namespace backendAg.Models
{
    public partial class Administrator
    {
        public Administrator()
        {
            Articles = new HashSet<Article>();
        }

        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string? Token { get; set; }

        public virtual ICollection<Article> Articles { get; set; }
    }
}
