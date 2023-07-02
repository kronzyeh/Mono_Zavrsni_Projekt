﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Model
{
    public class PublicationGet
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Edition { get; set; }
        public DateTime DatePublished { get; set; }
        public int Quantity { get; set; }
        public int NumberOfPages { get; set; }
        public string Language { get; set; }
        public string Type { get; set; }
        public string Genre { get; set; }
        public string Publisher { get; set; }

        public string Author { get; set; }
    }
}
