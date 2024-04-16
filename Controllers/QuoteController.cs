using Microsoft.AspNetCore.Mvc;
using Quote.Models;
using System.Linq;

namespace Quote.Controllers
{
    [ApiController]
    [Route("api/quotes")]
    public class QuotesController : ControllerBase
    {

        private readonly QuotesContext _context;

        public QuotesController(QuotesContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult GetQuotes([FromQuery] string? author, string? tags, [FromQuery] string? text)
        {
            var query = _context.Quotes.AsQueryable();

            // Filter quotes based on search parameters
            if (!string.IsNullOrEmpty(author))
            {
                query = query.Where(q => q.Author.Contains(author, StringComparison.OrdinalIgnoreCase));
            }

            if (tags != null)
            {
                query = query.Where(q => q.Tags.Any(tag => tags.Contains(tag.ToString())));
            }

            if (!string.IsNullOrEmpty(text))
            {
                query = query.Where(q => q.QuoteText.Contains(text, StringComparison.OrdinalIgnoreCase));
            }

            var quotes = query.ToList();
            return Ok(quotes);
        }

        [HttpGet("{id}")]
        public Quotes? GetQuoteById(int id)
        {
            return _context.Quotes.Find(id);
        }
        [HttpPost]
        public void AddQuote(Quotes quote)
        {
            _context.Quotes.Add(quote);
            _context.SaveChanges();
        }
        [HttpPut("{id}")]
        public void UpdateQuote(int id, Quotes updatedQuote)
        {
            var existingQuote = GetQuoteById(id);
            if (existingQuote != null)
            {
                existingQuote.Author = updatedQuote.Author;
                existingQuote.Tags = updatedQuote.Tags;
                existingQuote.QuoteText = updatedQuote.QuoteText;

                _context.SaveChanges();
            }
        }
        [HttpDelete("{id}")]
        public bool DeleteQuote(int id)
        {
            var quote = GetQuoteById(id);
            if (quote != null)
            {
                _context.Quotes.Remove(quote);
                _context.SaveChanges();
                return true;
            }
            return false;
        }
    }
}