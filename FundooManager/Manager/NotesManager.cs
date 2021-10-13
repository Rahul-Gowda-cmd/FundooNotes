using FundooManager.Interface;
using FundooModels;
using FundooRepository.Interface;
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

        public string AddNotes(NotesModel note)
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
    }
}
