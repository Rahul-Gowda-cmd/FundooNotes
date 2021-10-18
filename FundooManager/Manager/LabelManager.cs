using FundooManager.Interface;
using FundooModels;
using FundooRepository.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FundooManager.Manager
{
    public class LabelManager : ILabelManager
    {
        private readonly ILabelRepository repositary;
        public LabelManager(ILabelRepository repositary)
        {
            this.repositary = repositary;

        }
        public Task<string> AddLabel(LabelModel labelModel)
        {
            try
            {
                return this.repositary.AddLabel(labelModel);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Task<string> AddNotesLabel(LabelModel labelModel)
        {
            try
            {
                return this.repositary.AddNotesLabel(labelModel);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Task<string> DeleteLabelFromNote(int labelId)
        {
            try
            {
                return this.repositary.DeleteLabelFromNote(labelId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Task<string> DeleteLabel(int userId, string labelName)
        {
            try
            {
                return this.repositary.DeleteLabel(userId, labelName);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<NotesModel> DisplayNotesBasedOnLabel(int userId, string labelName)
        {
            try
            {
                return this.repositary.DisplayNotesBasedOnLabel(userId, labelName);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Task<string> EditLabel(LabelModel labelModel)
        {
            try
            {
                return this.repositary.EditLabel(labelModel);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<string> GetLabel(int userId)
        {
            try
            {
                return this.repositary.GetLabel(userId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<LabelModel> GetLabelByNote(int notesId)
        {
            try
            {
                return this.repositary.GetLabelByNote(notesId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
