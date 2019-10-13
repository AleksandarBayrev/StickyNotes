using StickyNotes.MainLogic.Contracts;
using StickyNotes.MainLogic.Modules;
using System;
using System.Collections.Generic;
using System.Text;

namespace StickyNotes.MainLogic
{
    public class StickyNotesManager
    {
        private static StickyNotesManager instance = null;
        private const string noNotes = "No notes";
        private IList<INote> notes;
        
        internal StickyNotesManager()
        {
            this.notes = new List<INote>();
        }
        internal StickyNotesManager(IList<INote> notes)
        {
            this.notes = notes;
        }
        public static StickyNotesManager GetInstance(IList<INote> notes)
        {
            if (instance == null)
            {
                instance = new StickyNotesManager(notes);
            }
            return instance;
        }

        public void AddNote(string Message)
        {
            this.notes.Add(new Note(Message));
        }

        public string RemoveNotes()
        {
            this.notes.Clear();
            return noNotes;
        }

        public string GetNotes()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var note in this.notes)
            {
                sb.Append(note.ToString());
            }

            if (sb.Length == 0)
            {
                sb.Append(noNotes);
            }

            return sb.ToString();
        }
    }
}
