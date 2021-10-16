using FundooModels;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FundooManager.Interface
{
    public interface INotesManager
    {
        Task<string> AddNotes(NotesModel notes);
        List<NotesModel> GetNotes(int userId);
        Task<string> UpdateNote(NotesModel updateNote);
        Task<bool> Deletenote(int noteID);
        Task<string> RestoreNote(int noteId);
        List<NotesModel> GetArchive(int userId);
        List<NotesModel> GetTrash(int userId);

        Task<string> PinNote(int noteId);
        Task<string> UnPinNote(int noteId);

        Task<string> ArchiveNote(int noteId);
        Task<string> UnArchiveNote(int noteId);

        Task<string> SetRemainder(int noteId, string Time);
        Task<string> RemoveRemainder(int noteId);

        Task<string> AddColor(int noteId, string color);

        Task<string> AddImage(int noteId, IFormFile image);
        Task<string> RemoveImage(int noteId);

    }
}
