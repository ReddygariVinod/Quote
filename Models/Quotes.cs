using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace Quote.Models
{
    //[Table("dbo.Quotes")]
    public class Quotes
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Author { get; set; }

        [Required]
        public string QuoteText { get; set; }

        
        public string Tags { get; set; }
        

        //[NotMapped]
        //public string[] TagsJson
        //{
        //    get
        //    {
               
        //        return TagsJson != null ? JsonSerializer.Deserialize<string[]>(Tags) : new string[0];
        //    }
        //    set
        //    {
               
        //        TagsJson = JsonSerializer.Serialize(value);
        //    }
        //}
    }
}
