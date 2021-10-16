using FundooManager.Interface;
using FundooModels;
using FundooRepository.Interface;
using FundooRepository.Repository;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FundooManager.Manager
{
    public class NotesManager : INotesManager
    {
        private readonly INotesRepositary notesrepositary;
        public NotesManager(INotesRepositary notesrepositary)
        {
            this.notesrepositary = notesrepositary;
        }

        public Task<string> AddNotes(NotesModel note)
        {
            try
            {
                return this.notesrepositary.AddNotes(note);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Task<bool> Deletenote(int noteID)
        {
            try
            {
                var result= notesrepositary.Deletenote(noteID);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<NotesModel> GetNotes(int userId)
        {

            try
            {
                return this.notesrepositary.GetNotes(userId);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public Task<string> UpdateNote(NotesModel updatenotes)
        {
            try
            {
                return this.notesrepositary.UpdateNote(updatenotes);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Task<string> RestoreNote(int noteId)
        {
            try
            {
                return this.notesrepositary.RestoreNote(noteId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<NotesModel> GetArchive(int userId)
        {
            try
            {
                return this.notesrepositary.GetArchive(userId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<NotesModel> GetTrash(int userId)
        {
            try
            {
                return this.notesrepositary.GetTrash(userId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Task<string> PinNote(int noteId)
        {
            try
            {
                return this.notesrepositary.PinNote(noteId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Task<string> UnPinNote(int noteId)
        {
            try
            {
                return this.notesrepositary.UnPinNote(noteId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Task<string> ArchiveNote(int noteId)
        {
            try
            {
                return this.notesrepositary.ArchiveNote(noteId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Task<string> UnArchiveNote(int noteId)
        {
            try
            {
                return this.notesrepositary.UnArchiveNote(noteId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Task<string> SetRemainder(int noteId, string Time)
        {
            try
            {
                return this.notesrepositary.SetRemainder(noteId,Time);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Task<string> RemoveRemainder(int noteId)
        {
            try
            {
                return this.notesrepositary.RemoveRemainder(noteId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Task<string> AddColor(int noteId, string color)
        {
            try
            {
                return this.notesrepositary.AddColor(noteId,color);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Task<string> AddImage(int noteId, IFormFile image)
        {
            try
            {
                return this.notesrepositary.AddImage(noteId, image);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Task<string> RemoveImage(int noteId)
        {
            try
            {
                return this.notesrepositary.RemoveImage(noteId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
