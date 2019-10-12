using StickyNotes.MainLogic.Contracts;
using System;

namespace StickyNotes.MainLogic.Modules
{
    public class Note : INote
    {
        private string message;
        private DateTime dateAdded;
        public string Message { get => this.message; set => this.message = value; }
        public DateTime DateAdded { get => this.dateAdded; set => this.dateAdded = value; }

        public Note()
        {
            this.message = "";
            this.dateAdded = DateTime.UtcNow;
        }

        public Note(string Message)
        {
            this.message = Message;
            this.dateAdded = DateTime.UtcNow;
        }

        public override string ToString()
        {
            return $"Note: {this.Message}\nDate added: {this.DateAdded.ToString()}\n\n";
        }
    }
}