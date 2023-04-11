﻿using System;
using System.Collections.Generic;

namespace Assignment.Models
{
    public partial class Genre
    {
        public Genre()
        {
            Movies = new HashSet<Movie>();
        }

        public int GenreId { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Movie> Movies { get; set; }
    }
}
