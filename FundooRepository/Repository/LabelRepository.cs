using FundooModels;
using FundooRepository.Context;
using FundooRepository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FundooRepository.Repository
{
    public class LabelRepository : ILabelRepository
    {
        private readonly UserContext repositaryContext;

        public LabelRepository(UserContext repositaryContext)
        {
            this.repositaryContext = repositaryContext;
        }
        public async Task<string> AddLabel(LabelModel labelModel)
        {
            try
            {
                var exist = this.repositaryContext.Labels.Where(x => x.LabelName == labelModel.LabelName && x.UserId == labelModel.UserId && x.NoteId == null).SingleOrDefault();
                if (exist == null)
                {
                    this.repositaryContext.Labels.Add(labelModel);
                    await this.repositaryContext.SaveChangesAsync();
                    return "Added Label";
                }

                return "Label Already Exists";
            }
            catch (ArgumentNullException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<string> AddNotesLabel(LabelModel labelModel)
        {
            try
            {
                var exists = this.repositaryContext.Labels.Where(x => x.LabelName == labelModel.LabelName && labelModel.NoteId == x.NoteId).SingleOrDefault();
                if (exists == null)
                {
                    this.repositaryContext.Labels.Add(labelModel);
                    await this.repositaryContext.SaveChangesAsync();
                    labelModel.LabelId = 0;
                    labelModel.NoteId = null;
                    await this.AddLabel(labelModel);
                    return "Added Label To Note";
                }

                return "Invalid Id";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<string> DeleteLabelFromNote(int labelId)
        {
            try
            {
                var existsLabel = this.repositaryContext.Labels.Where(x => x.LabelId == labelId).SingleOrDefault();
                if (existsLabel != null)
                {
                    this.repositaryContext.Labels.Remove(existsLabel);
                    await this.repositaryContext.SaveChangesAsync();
                    return "Deleted Label From Note";
                }

                return "Failed";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<string> DeleteLabel(int labelId)
        {
            try
            {
                var label = this.repositaryContext.Labels.Where(x => x.LabelId == labelId).SingleOrDefault();
                var exists = this.repositaryContext.Labels.Where(x => x.LabelName == label.LabelName && x.UserId== label.UserId).ToList();
                if (exists.Count > 0 )
                {
                    this.repositaryContext.Labels.RemoveRange(exists);
                    await this.repositaryContext.SaveChangesAsync();
                    return "Deleted Label";
                }

                return "No Label Present";
            }
            catch (ArgumentNullException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<NotesModel> GetNotesBasedOnLabel(int labelId)
        {
            try
            {
                var existdata = this.repositaryContext.Labels.Where(x => x.LabelId == labelId).SingleOrDefault();
                var exists = (from label in this.repositaryContext.Labels
                              join notes in this.repositaryContext.Notes
                              on label.NoteId equals notes.NoteId
                              where label.UserId == existdata.UserId && label.LabelName == existdata.LabelName
                              select notes).ToList();
                if (exists.Count > 0)
                {
                    return exists;
                }

                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<string> EditLabel(LabelModel labelModel)
        {
            try
            {
                var exist = this.repositaryContext.Labels.Where(x => x.LabelId == labelModel.LabelId).Select(x => x.LabelName).SingleOrDefault();
                var existOldLabel = this.repositaryContext.Labels.Where(x => x.LabelName == exist && x.UserId == labelModel.UserId).ToList();
                if (existOldLabel.Count > 0)
                {                   
                    existOldLabel.ForEach(x => x.LabelName = labelModel.LabelName);
                    this.repositaryContext.Labels.UpdateRange(existOldLabel);
                    await this.repositaryContext.SaveChangesAsync();
                    return "Label is Updated";
                }
                return "Label doesnot Exist";
            }
            catch (ArgumentNullException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<LabelModel> GetLabel(int userId)
        {
            try
            {
                var list = this.repositaryContext.Labels.Where(e => e.UserId == userId).Distinct().ToList();
                if (list.Count > 0)
                { 
                    return list;
                }
                else
                {
                    return null;
                }
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
                var list = this.repositaryContext.Labels.Where(e => e.NoteId == notesId).ToList();
                if (list.Count > 0)
                {      
                    return list;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
