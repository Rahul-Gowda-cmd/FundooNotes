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

        public async Task<string> AddNotes(NotesModel note)
        {
            try
            {
                this.notesContext.Notes.Add(note);
                await this.notesContext.SaveChangesAsync();
                return "Note Added Successfully";
            }
            catch (ArgumentNullException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> NotetoTrash(int noteID)
        {
            try
            {
                if (notesContext.Notes.Any(n => n.NoteId == noteID))
                {
                    var note = notesContext.Notes.Find(noteID);
                    if (note.IsTrash)
                    {
                        notesContext.Entry(note).State = EntityState.Deleted;
                    }
                    else
                    {
                        note.IsTrash = true;
                        note.IsArchived = false;
                        note.IsPin = false;
                    }
                    await notesContext.SaveChangesAsync();
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
                var Check = this.notesContext.Notes.Any(e => e.UserId == userId);
                if (Check)
                { 
                    var list = this.notesContext.Notes.Where(e => e.UserId == userId && e.IsTrash == false && e.IsArchived == false).ToList();
                return list;
                }
                else
                {
                    return null;
                }
               
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<string> UpdateNote(NotesModel updateNote)
        {
            try
            {
                var userData = this.notesContext.Notes.Where(e => e.NoteId == updateNote.NoteId).SingleOrDefault();
                if(userData!=null)
                {
                    userData.Title = updateNote.Title;
                    userData.Body = updateNote.Body;
                    await this.notesContext.SaveChangesAsync();
                    return "Notes Updated";
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

        public Task<string> Delete(int noteId)
        {
            throw new NotImplementedException();
        }

        public Task<string> RestoreNote(int noteId)
        {
            throw new NotImplementedException();
        }

        public List<NotesModel> GetArchive(int userId)
        {
            throw new NotImplementedException();
        }

        public List<NotesModel> GetTrash(int userId)
        {
            throw new NotImplementedException();
        }

        public Task<string> PinNote(int noteId)
        {
            throw new NotImplementedException();
        }

        public Task<string> UnPinNote(int noteId)
        {
            throw new NotImplementedException();
        }

        public Task<string> ArchiveNote(int noteId)
        {
            throw new NotImplementedException();
        }

        public Task<string> UnArchiveNote(int noteId)
        {
            throw new NotImplementedException();
        }

        public Task<string> SetRemainder(int noteId, string Time)
        {
            throw new NotImplementedException();
        }

        public Task<string> RemoveRemainder(int noteId)
        {
            throw new NotImplementedException();
        }

        public Task<string> AddColor(int noteId, string color)
        {
            throw new NotImplementedException();
        }
    }
}
