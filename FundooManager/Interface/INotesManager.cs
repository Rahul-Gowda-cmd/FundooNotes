using FundooModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FundooManager.Interface
{
    public interface INotesManager
    {
        string AddNotes(NotesModel notes);
        
        List<NotesModel> GetNotes(int userId);
        string UpdateNote(NotesModel updateNote, int NotesID);
        bool DeleteNote(int UserID, int noteID);
    }
}
