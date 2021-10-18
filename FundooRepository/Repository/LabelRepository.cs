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

        public async Task<string> DeleteLabel(int userId, string labelName)
        {
            try
            {
                var exists = this.repositaryContext.Labels.Where(x => x.LabelName == labelName && x.UserId == userId).ToList();
                if (exists.Count > 0)
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

        public List<NotesModel> DisplayNotesBasedOnLabel(int userId, string labelName)
        {
            try
            {
                var exists = (from notes in this.repositaryContext.Notes
                              join label in this.repositaryContext.Labels
                              on notes.NoteId equals label.NoteId
                              where userId == label.UserId && label.LabelName == labelName
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
                string message = "Label not present";
                var exist = this.repositaryContext.Labels.Where(x => x.LabelId == labelModel.LabelId).Select(x => x.LabelName).SingleOrDefault();
                var existOldLabel = this.repositaryContext.Labels.Where(x => x.LabelName == exist && x.UserId == labelModel.UserId).ToList();
                var labelExists = this.repositaryContext.Labels.Where(x => x.LabelName == labelModel.LabelName && x.UserId == labelModel.UserId && x.NoteId == null).SingleOrDefault();
                if (existOldLabel.Count > 0)
                {
                    message = "Updated Label";
                    if (labelExists != null)
                    {
                        this.repositaryContext.Labels.Remove(labelExists);
                        await this.repositaryContext.SaveChangesAsync();
                        message = "Exist label is deleted";
                    }

                    existOldLabel.ForEach(x => x.LabelName = labelModel.LabelName);
                    this.repositaryContext.Labels.UpdateRange(existOldLabel);
                    this.repositaryContext.SaveChanges();
                }
                return message;
            }
            catch (ArgumentNullException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<string> GetLabel(int userId)
        {
            try
            {
                var Check = this.repositaryContext.Labels.Any(e => e.UserId == userId);
                if (Check)
                {
                    var list = this.repositaryContext.Labels.Where(e => e.UserId == userId).Select(x => x.LabelName).Distinct().ToList();
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
                var Check = this.repositaryContext.Labels.Any(e => e.NoteId == notesId);
                if (Check)
                {
                    var list = this.repositaryContext.Labels.Where(e => e.NoteId == notesId).ToList();
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
