// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ICollaboratorManager.cs" company="Bridgelabz">
//   Copyright © 2021 Company="BridgeLabz"
// </copyright>
// <creator name="Rahul prabu"/>
// --------------------------------------------------------------------------------------------------------------------

namespace FundooRepository.Interface
{
    using FundooModels;
    using Microsoft.AspNetCore.Http;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>
    ///  interface INotesRepositary
    /// </summary>
    public interface INotesRepositary
    {
        /// <summary>
        /// Adds the notes.
        /// </summary>
        /// <param name="notes">The notes.</param>
        /// <returns>returns a string when data added successful</returns>
        Task<string> AddNotes(NotesModel notes);

        /// <summary>
        /// Gets the notes.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>Returns a lit of retrieved notes</returns>
        List<NotesModel> GetNotes(int userId);

        /// <summary>
        /// Updates the note.
        /// </summary>
        /// <param name="updateNote">The update note.</param>
        /// <returns>returns string on successful update of data for title or Note</returns>
        Task<string> UpdateNote(NotesModel updateNote);

        /// <summary>
        /// Deletenotes the specified note identifier.
        /// </summary>
        /// <param name="noteID">The note identifier.</param>
        /// <returns>returns a string on successful delete</returns>
        Task<bool> Deletenote(int noteID);

        /// <summary>
        /// Restores the note.
        /// </summary>
        /// <param name="noteId">The note identifier.</param>
        /// <returns>returns a string on successful restore</returns>
        Task<string> RestoreNote(int noteId);

        /// <summary>
        /// Gets the archive.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>returns list as result</returns>
        List<NotesModel> GetArchive(int userId);

        /// <summary>
        /// Gets the trash.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>returns list as result</returns>
        List<NotesModel> GetTrash(int userId);

        /// <summary>
        /// Pins the note.
        /// </summary>
        /// <param name="noteId">The note identifier.</param>
        /// <returns>returns a string after updating pin</returns>
        Task<string> PinNote(int noteId);

        /// <summary>
        /// Uns the pin note.
        /// </summary>
        /// <param name="noteId">The note identifier.</param>
        /// <returns>returns a string after remove pin</returns>
        Task<string> UnPinNote(int noteId);

        /// <summary>
        /// Archives the note.
        /// </summary>
        /// <param name="noteId">The note identifier.</param>
        /// <returns>returns a string after Archive note</returns>
        Task<string> ArchiveNote(int noteId);

        /// <summary>
        /// Uns the archive note.
        /// </summary>
        /// <param name="noteId">The note identifier.</param>
        /// <returns>returns a string after UnArchivenote</returns>
        Task<string> UnArchiveNote(int noteId);

        /// <summary>
        /// Sets the remainder.
        /// </summary>
        /// <param name="noteId">The note identifier.</param>
        /// <param name="Time">The time.</param>
        /// <returns>returns a string after Set Time</returns>
        Task<string> SetRemainder(int noteId, string Time);

        /// <summary>
        /// Removes the remainder.
        /// </summary>
        /// <param name="noteId">The note identifier.</param>
        /// <returns>returns a string after Remove Reminder</returns>
        Task<string> RemoveRemainder(int noteId);

        /// <summary>
        /// Adds the color.
        /// </summary>
        /// <param name="noteId">The note identifier.</param>
        /// <param name="color">The color.</param>
        /// <returns>returns string on successful update of color</returns>
        Task<string> AddColor(int noteId, string color);

        /// <summary>
        /// Adds the image.
        /// </summary>
        /// <param name="noteId">The note identifier.</param>
        /// <param name="image">The image.</param>
        /// <returns>returns string after successfully adding image</returns>
        Task<string> AddImage(int noteId, IFormFile image);

        /// <summary>
        /// Removes the image.
        /// </summary>
        /// <param name="noteId">The note identifier.</param>
        /// <returns>returns string after successfully removing image</returns>
        Task<string> RemoveImage(int noteId);
    }
}
