using FundooManager.Interface;
using FundooModels;
using FundooRepository.Interface;
using FundooRepository.Repository;
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

        public bool DeleteNote(int UserID, int noteID)
        {
            try
            {
                bool result= notesrepositary.DeleteNote(UserID, noteID);
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

        public string UpdateNote(NotesModel updateNote, int NotesID)
        {
            try
            {
                return this.notesrepositary.UpdateNote(updateNote, NotesID);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
