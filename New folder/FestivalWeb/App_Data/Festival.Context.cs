﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FestivalWeb.App_Data
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class FestivalEntities : DbContext
    {
        public FestivalEntities()
            : base("name=FestivalEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<Bands> Bands { get; set; }
        public DbSet<Gebruikers> Gebruikers { get; set; }
        public DbSet<Genres> Genres { get; set; }
        public DbSet<Tickets> Tickets { get; set; }
    }
}