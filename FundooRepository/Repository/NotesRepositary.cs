using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using FundooModels;
using FundooRepository.Context;
using FundooRepository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
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

        private readonly IConfiguration Configuration;

        public NotesRepositary( UserContext notesContext, IConfiguration Configuration)
        {
            this.notesContext = notesContext;
            this.Configuration = Configuration;
        }

        public async Task<string> AddNotes(NotesModel note)
        {
            try
            {
                if (note.Title != null || note.Body != null || note.Reminder != null)
                {
                    this.notesContext.Notes.Add(note);
                    await this.notesContext.SaveChangesAsync();
                    return "Note Added Successfully";
                }
                else
                {
                    return "Invalid Id";
                }
            }
            catch (ArgumentNullException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> Deletenote(int noteID)
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

        public async Task<string> RestoreNote(int noteId)
        {
            try
            {
                var userData = this.notesContext.Notes.Where(e => e.NoteId == noteId).SingleOrDefault();
                if (userData.IsTrash==true)
                {
                    userData.IsTrash = false;
                    await notesContext.SaveChangesAsync();
                    return "Notes is restored";
                }
                else
                {
                    return "Invalid Id";
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<NotesModel> GetArchive(int userId)
        {
            try
            {
                var Check = this.notesContext.Notes.Any(e => e.UserId == userId);
                if (Check)
                {          
                    var list = this.notesContext.Notes.Where(e => e.UserId == userId && e.IsTrash == false && e.IsArchived == true).ToList();
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

        public List<NotesModel> GetTrash(int userId)
        {
            try
            {
                var Check = this.notesContext.Notes.Any(e => e.UserId == userId && e.IsTrash == true);
                if (Check)
                {
                    var list = this.notesContext.Notes.Where(e => e.UserId == userId &&  e.IsArchived == false).ToList();
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

        public async Task<string> PinNote(int noteId)
        {
            try
            {
                var userData = this.notesContext.Notes.Where(e => e.NoteId == noteId).SingleOrDefault();
                if (userData != null)
                {
                    userData.IsPin = true;
                    await this.notesContext.SaveChangesAsync();
                    return "Pin Notes";
                }
                else
                {
                    return "Invalid Id";
                }

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<string> UnPinNote(int noteId)
        {
            var userData = this.notesContext.Notes.Where(e => e.NoteId == noteId).SingleOrDefault();
            if (userData != null && userData.IsPin == true)
            {
                userData.IsPin = false;
                await this.notesContext.SaveChangesAsync();
                return "UnPin the Notes";
            }
            else
            {
                return "Invalid Id";
            }
        }

        public async Task<string> ArchiveNote(int noteId)
        {
            try
            {
                var userData = this.notesContext.Notes.Where(e => e.NoteId == noteId).SingleOrDefault();
                if (userData != null)
                {
                    userData.IsArchived = true;
                    await notesContext.SaveChangesAsync();
                    return "Archive Note";
                }
                else
                {
                    return "Invalid Id";
                }

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<string> UnArchiveNote(int noteId)
        {
            try
            {
                var userData = this.notesContext.Notes.Where(e => e.NoteId == noteId).SingleOrDefault();
                if (userData != null && userData.IsArchived == true)
                {
                    userData.IsArchived = false;
                    await notesContext.SaveChangesAsync();
                    return "UnArchived Note";
                }
                else
                {
                    return "Invalid Id";
                }

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<string> SetRemainder(int noteId, string Time)
        {
            try
            {
                var userData = this.notesContext.Notes.Where(e => e.NoteId == noteId).SingleOrDefault();
                if(userData != null)
                {
                    userData.Reminder = Time;
                    await notesContext.SaveChangesAsync();
                    return "Reminder is sets Successfuly";
                }
                else 
                {
                    return "Invalid Id";
                }

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public  async Task<string> RemoveRemainder(int noteId)
        {
            try
            {
                var userData = this.notesContext.Notes.Where(e => e.NoteId == noteId).SingleOrDefault();
                if (userData != null)
                {
                    userData.Reminder = null; ;
                    this.notesContext.Notes.Update(userData);
                    await notesContext.SaveChangesAsync();
                    return "Reminder is removed";
                }
                else
                {
                    return "Invalid Id";
                }

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<string> AddColor(int noteId, string color)
        {
            try
            {
                var userData = this.notesContext.Notes.Where(e => e.NoteId == noteId).SingleOrDefault();
                if (userData != null)
                {
                    userData.Color = color;
                    await notesContext.SaveChangesAsync();
                    return "Color Added Successfuly";
                }
                else
                {
                    return "Invalid Id";
                }

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<string> AddImage(int noteId, IFormFile image)
        {
            try
            {
                var userexist = this.notesContext.Notes.Find(noteId);
                Account account = new Account(
                        this.Configuration["CloudinaryAccount:CloudName"],
                        this.Configuration["CloudinaryAccount:APIKey"],
                        this.Configuration["CloudinaryAccount:APISecret"]);
                Cloudinary cloudinary = new Cloudinary(account);
                var uploadFile = new ImageUploadParams()
                {
                    File = new FileDescription(image.FileName, image.OpenReadStream())
                };
                var uploadResult = cloudinary.Upload(uploadFile);
                var uploadedImage = uploadResult.Url.ToString();
                if (userexist != null)
                {
                    userexist.Image = uploadedImage;
                    this.notesContext.Notes.Update(userexist);
                    await this.notesContext.SaveChangesAsync();
                    return "Image added";
                }
                else
                {
                    return "Invalid Id";
                }
            }
            catch (ArgumentNullException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<string> RemoveImage(int noteId)
        {
            try
            {
                var userexist = this.notesContext.Notes.Find(noteId);
                if (userexist != null)
                {
                    userexist.Image = null;
                    this.notesContext.Notes.Update(userexist);
                    await this.notesContext.SaveChangesAsync();
                    return "Image removed";
                }

                return "Invalid Id";
            }
            catch (ArgumentNullException ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
