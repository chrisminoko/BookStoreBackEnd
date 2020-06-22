﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreBackEnd.Models
{
    public class AuthorDto
    {
        public AuthorDto() 
        {
        }
        public long Id { get; set; }
        public string Name { get; set; }
        public  AuthorContactDto AuthorContact { get; set; }

    }
}
