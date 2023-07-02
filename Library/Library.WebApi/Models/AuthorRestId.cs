﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Library.WebApi.Models
{
    public class AuthorRestId
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Nationality { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime? DateOfDeath { get; set; }

    }
}