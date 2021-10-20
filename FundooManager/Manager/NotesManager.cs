// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NotesManager.cs" company="Bridgelabz">
//   Copyright © 2021 Company="BridgeLabz"
// </copyright>
// <creator name="Rahul prabu"/>
// --------------------------------------------------------------------------------------------------------------------
namespace FundooManager.Manager
{
    using FundooManager.Interface;
    using FundooModels;
    using FundooRepository.Interface;
    using Microsoft.AspNetCore.Http;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>
    /// Class NotesManager
    /// </summary>
    /// <seealso cref="FundooManager.Interface.INotesManager" />
    public class NotesManager : INotesManager
    {
        /// <summary>
        /// The notesrepositary
        /// </summary>
        private readonly INotesRepositary notesrepositary;

        /// <summary>
        /// Initializes a new instance of the <see cref="NotesManager"/> class.
        /// </summary>
        /// <param name="notesrepositary">The notesrepositary.</param>
        public NotesManager(INotesRepositary notesrepositary)
        {
            this.notesrepositary = notesrepositary;
        }

        /// <summary>
        /// Adds the notes.
        /// </summary>
        /// <param name="note">The note.</param>
        /// <returns></returns>
        /// <exception cref="System.Exception"></exception>
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

        /// <summary>
        /// Deletenotes the specified note identifier.
        /// </summary>
        /// <param name="noteID">The note identifier.</param>
        /// <returns>
        /// returns a string on successful delete
        /// </returns>
        /// <exception cref="System.Exception"></exception>
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

        /// <summary>
        /// Gets the notes.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>
        /// Returns a lit of retrieved notes
        /// </returns>
        /// <exception cref="System.Exception"></exception>
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

        /// <summary>
        /// Updates the note.
        /// </summary>
        /// <param name="updatenotes">The updatenotes.</param>
        /// <returns></returns>
        /// <exception cref="System.Exception"></exception>
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

        /// <summary>
        /// Restores the note.
        /// </summary>
        /// <param name="noteId">The note identifier.</param>
        /// <returns>
        /// returns a string on successful restore
        /// </returns>
        /// <exception cref="System.Exception"></exception>
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

        /// <summary>
        /// Gets the archive.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>
        /// returns list as result
        /// </returns>
        /// <exception cref="System.Exception"></exception>
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

        /// <summary>
        /// Gets the trash.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>
        /// returns list as result
        /// </returns>
        /// <exception cref="System.Exception"></exception>
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

        /// <summary>
        /// Pins the note.
        /// </summary>
        /// <param name="noteId">The note identifier.</param>
        /// <returns>
        /// returns a string after updating pin
        /// </returns>
        /// <exception cref="System.Exception"></exception>
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

        /// <summary>
        /// Uns the pin note.
        /// </summary>
        /// <param name="noteId">The note identifier.</param>
        /// <returns>
        /// returns a string after remove pin
        /// </returns>
        /// <exception cref="System.Exception"></exception>
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

        /// <summary>
        /// Archives the note.
        /// </summary>
        /// <param name="noteId">The note identifier.</param>
        /// <returns>
        /// returns a string after Archive note
        /// </returns>
        /// <exception cref="System.Exception"></exception>
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

        /// <summary>
        /// Uns the archive note.
        /// </summary>
        /// <param name="noteId">The note identifier.</param>
        /// <returns>
        /// returns a string after UnArchivenote
        /// </returns>
        /// <exception cref="System.Exception"></exception>
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

        /// <summary>
        /// Sets the remainder.
        /// </summary>
        /// <param name="noteId">The note identifier.</param>
        /// <param name="Time">The time.</param>
        /// <returns>
        /// returns a string after Set Time
        /// </returns>
        /// <exception cref="System.Exception"></exception>
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

        /// <summary>
        /// Removes the remainder.
        /// </summary>
        /// <param name="noteId">The note identifier.</param>
        /// <returns>
        /// returns a string after Remove Reminder
        /// </returns>
        /// <exception cref="System.Exception"></exception>
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

        /// <summary>
        /// Adds the color.
        /// </summary>
        /// <param name="noteId">The note identifier.</param>
        /// <param name="color">The color.</param>
        /// <returns>
        /// returns string on successful update of color
        /// </returns>
        /// <exception cref="System.Exception"></exception>
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

        /// <summary>
        /// Adds the image.
        /// </summary>
        /// <param name="noteId">The note identifier.</param>
        /// <param name="image">The image.</param>
        /// <returns>
        /// returns string after successfully adding image
        /// </returns>
        /// <exception cref="System.Exception"></exception>
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

        /// <summary>
        /// Removes the image.
        /// </summary>
        /// <param name="noteId">The note identifier.</param>
        /// <returns>
        /// returns string after successfully removing image
        /// </returns>
        /// <exception cref="System.Exception"></exception>
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
