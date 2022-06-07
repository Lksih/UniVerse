using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace UniVerse.Models
{
    [Table("Verse_Tag")]
    public class Verse_Tag
    {
        [PrimaryKey, AutoIncrement, NotNull, Unique]
        public int ID { get; set; }

        public int VerseId { get; set; }
        public int TagId { get; set; }
    }
}
