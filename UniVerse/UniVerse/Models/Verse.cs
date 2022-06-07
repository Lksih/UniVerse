using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace UniVerse.Models
{
    [Table("Verse")]
    public class Verse
    {
        [PrimaryKey, AutoIncrement, NotNull, Unique]
        public int ID { get; set; }

        public string Name { get; set; }
        public string Text { get; set; }
        public int AuthorId { get; set; }
        public int Favourited { get; set; }
    }
}
