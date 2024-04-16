using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Quote.Models
{
    public class QuotesContext: DbContext
    {

        public QuotesContext(DbContextOptions<QuotesContext> options)
           : base(options)
        {
        }

        public DbSet<Quotes> Quotes { get; set; }
    }

}
