﻿using MediaCollection2.Domain.Movie;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MediaCollection2.Data
{
    public class MediaCollectionContext : IdentityDbContext
    {
        public MediaCollectionContext(DbContextOptions Options) : base(Options)
        {

        }
        public DbSet<Movie> Movies { get; set; }
    }
}