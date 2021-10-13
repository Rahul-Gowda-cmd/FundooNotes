using FundooModels;
using FundooRepository.Context;
using FundooRepository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FundooRepository.Repository
{
    public class NotesRepositary : INotesRepositary
    {
        private readonly UserContext notesContext;

        public NotesRepositary( UserContext notesContext)
        {
            this.notesContext = notesContext;
        }

        public string AddNotes(NotesModel note)
        {
            try
            {
                this.notesContext.Notes.Add(note);
                this.notesContext.SaveChanges();
                return "Note Added Successfully";
            }
            catch (ArgumentNullException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool DeleteNote(int UserID, int noteID)
        {
            try
            {
                if (notesContext.Notes.Any(n => n.NoteId == noteID && n.UserId == UserID))
                {
                    var note = notesContext.Notes.Find(noteID);
                    if (note.IsTrash)
                    {
                        notesContext.Entry(note).State = EntityState.Deleted;
                    }
                    else
                    {
                        note.IsTrash = true;
                        note.IsPin = false;
                        note.IsArchived = false;
                    }
                    notesContext.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<NotesModel> GetNotes(int userId)
        {
            try
            {
                var list = this.notesContext.Notes.Where(e => e.NoteId == userId).ToList();
                if (list.Count != 0)
                {
                    return list;
                }
                return null;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public string UpdateNote(NotesModel updateNote, int NotesID)
        {
            try
            {
                var userData = this.notesContext.Notes.Where(e => e.NoteId == NotesID).SingleOrDefault();
                if(userData!=null)
                {
                    this.notesContext.Notes.Add(updateNote);
                    this.notesContext.SaveChanges();
                    return "Note Added Successfully";
                }
                else
                {
                    return "Inavalid Id";
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
