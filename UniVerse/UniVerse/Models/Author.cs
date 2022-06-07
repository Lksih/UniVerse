using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace UniVerse.Models
{
    [Table("Author")]
    public class Author
    {
        [PrimaryKey, AutoIncrement, NotNull, Unique]
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
