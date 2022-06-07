using SQLite;
using System.Collections.Generic;

namespace UniVerse.Models
{
    [Table("Tag")]
    public class Tag
    {
        [PrimaryKey, AutoIncrement, NotNull, Unique]
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
