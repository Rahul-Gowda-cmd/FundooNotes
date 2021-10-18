using FundooModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FundooManager.Interface
{
    public interface ILabelManager
    {
        Task<string> AddLabel(LabelModel labelModel);
        Task<string> DeleteLabel(int userId, string labelName);
        Task<string> EditLabel(LabelModel labelModel);
        List<string> GetLabel(int userId);
        Task<string> AddNotesLabel(LabelModel labelModel);
        Task<string> DeleteLabelFromNote(int labelId);
        List<LabelModel> GetLabelByNote(int notesId);
        List<NotesModel> DisplayNotesBasedOnLabel(int userId, string labelName);
    }
}
