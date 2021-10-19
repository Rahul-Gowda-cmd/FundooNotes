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

        public Task<string> DeleteLabel(int labelId)
        {
            try
            {
                return this.repositary.DeleteLabel(labelId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<NotesModel> GetNotesBasedOnLabel(int labelId)
        {
            try
            {
                return this.repositary.GetNotesBasedOnLabel(labelId);
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

        public List<LabelModel> GetLabel(int userId)
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
