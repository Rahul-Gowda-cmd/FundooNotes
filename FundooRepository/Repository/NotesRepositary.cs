using FundooModels;
using FundooRepository.Context;
using FundooRepository.Interface;
using System;
using System.Collections.Generic;
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

        public string AddNotes(NotesModel note)
        {
            try
            {
                this.notesContext.Notes.Add(note);
                this.notesContext.SaveChanges();
                return "Note Added Successfully";
            }
            catch (ArgumentNullException ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
