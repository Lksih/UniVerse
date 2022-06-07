using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using UniVerse.Models;
using Xamarin.Forms;

namespace UniVerse
{
    public class DataBaseAccess
    {
        public const string DATABASE_NAME = "universe.db";
        SQLiteConnection db;

        public DataBaseAccess()
        {
            string dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), DATABASE_NAME);
            db = new SQLiteConnection(dbPath);
            db.CreateTable<Author>();
            db.CreateTable<Tag>();
            db.CreateTable<Verse>();
        }

        public List<Author> GetAllAuthors()
        {
            return db.Table<Author>().ToList();
        }

        public List<Tag> GetAllTags()
        {
            return db.Table<Tag>().ToList();
        }

        public List<Verse> GetAllVerses()
        {
            return db.Table<Verse>().ToList();
        }

        public Author GetAuthor(int id)
        {
            return db.Get<Author>(id);
        }

        public Tag GetTag(int id)
        {
            return db.Get<Tag>(id);
        }

        public Verse GetVerse(int id)
        {
            return db.Get<Verse>(id);
        }

        public int AddAuthor(string name)
        {
            Author res = db.FindWithQuery<Author>("SELECT * FROM Author WHERE Name = ?", name);
            if (res == null)
            {
                Author author = new Author();
                author.Name = name;
                db.Insert(author);
                return author.Id;
            }
            else
            {
                return res.Id;
            }
        }

        public int AddTag(string name)
        {
            Tag res = db.FindWithQuery<Tag>("SELECT * FROM Tag WHERE Name = ?", name);
            if (res == null)
            {
                Tag tag = new Tag();
                tag.Name = name;
                db.Insert(tag);
                return tag.Id;
            }
            else
            {
                return res.Id;
            }
        }

        public void AddVerse(string name, string text, string author, string[] tags)
        {
            Verse verse = new Verse();
            verse.Name = name;
            verse.Text = text;
            verse.AuthorId = AddAuthor(author);
            verse.Favourited = 0;
            db.Insert(verse);
            foreach(string tag in tags)
            {
                Verse_Tag verse_Tag = new Verse_Tag();
                verse_Tag.VerseId = verse.ID;
                verse_Tag.TagId = AddTag(tag);
                db.Insert(verse_Tag);
            }
        }

        public List<Verse_Tag> GetTagsForVerse(int verseId)
        {
            return db.Query<Verse_Tag>("SELECT * FROM Verse_Tag WHERE VerseId = ?", verseId);
        }

        public List<Verse> GetVersesOfAuthor(Author author)
        {
            return db.Query<Verse>("SELECT * FROM Verse WHERE AuthorId = ?", author.Id);
        }

        public List<Verse> GetVersesOfTag(Tag tag)
        {
            List<Verse_Tag> verse_tags = db.Query<Verse_Tag>("SELECT * FROM Verse_Tag WHERE TagId = ?", tag.Id);
            List<Verse> res = new List<Verse>();
            foreach(var verse_tag in verse_tags)
            {
                res.Add(GetVerse(verse_tag.VerseId));
            }
            return res;
        }

        public void UpdateLike(Verse verse)
        {
            db.Update(verse);
        }

        public List<Verse> GetLikedVerses()
        {
            return db.Query<Verse>("SELECT * FROM Verse WHERE Favourited = ?", 1);
        }
    }
}
