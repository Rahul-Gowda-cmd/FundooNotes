// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ICollaboratorManager.cs" company="Bridgelabz">
//   Copyright © 2021 Company="BridgeLabz"
// </copyright>
// <creator name="Rahul prabu"/>
// --------------------------------------------------------------------------------------------------------------------

namespace FundooRepository.Repository
{
    using FundooModels;
    using FundooRepository.Context;
    using FundooRepository.Interface;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    /// <summary>
    /// class LabelRepository : ILabelRepository
    /// </summary>
    /// <seealso cref="FundooRepository.Interface.ILabelRepository" />
    public class LabelRepository : ILabelRepository
    {
        /// <summary>
        /// The repositary context
        /// </summary>
        private readonly UserContext repositaryContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="LabelRepository"/> class.
        /// </summary>
        /// <param name="repositaryContext">The repositary context.</param>
        public LabelRepository(UserContext repositaryContext)
        {
            this.repositaryContext = repositaryContext;
        }

        /// <summary>
        /// Adds the label.
        /// </summary>
        /// <param name="labelModel">The label model.</param>
        /// <returns>
        /// returns string after successfully adding label without notesId
        /// </returns>
        /// <exception cref="System.Exception"></exception>
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

        /// <summary>
        /// Adds the notes label.
        /// </summary>
        /// <param name="labelModel">The label model.</param>
        /// <returns>
        /// returns a string after adding label from notes
        /// </returns>
        /// <exception cref="System.Exception"></exception>
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

        /// <summary>
        /// Deletes the label from note.
        /// </summary>
        /// <param name="labelId">The label identifier.</param>
        /// <returns>
        /// returns a string after deleting a label from note
        /// </returns>
        /// <exception cref="System.Exception"></exception>
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

        /// <summary>
        /// Deletes the label.
        /// </summary>
        /// <param name="labelId">The label identifier.</param>
        /// <returns>
        /// returns a string after deleting from home
        /// </returns>
        /// <exception cref="System.Exception"></exception>
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

        /// <summary>
        /// Gets the notes based on label.
        /// </summary>
        /// <param name="labelId">The label identifier.</param>
        /// <returns>
        /// returns a list of notes based on labe
        /// </returns>
        /// <exception cref="System.Exception"></exception>
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

        /// <summary>
        /// Edits the label.
        /// </summary>
        /// <param name="labelModel">The label model.</param>
        /// <returns>
        /// returns a string after editing label
        /// </returns>
        /// <exception cref="System.Exception"></exception>
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

        /// <summary>
        /// Gets the label.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>
        /// returns a list for getting labels based on userID
        /// </returns>
        /// <exception cref="System.Exception"></exception>
        public List<LabelModel> GetLabel(int userId)
        {
            try
            {
                var list = this.repositaryContext.Labels.Where(e => e.UserId == userId).Distinct().ToList();
                //var list= await (from user in repositaryContext.Labels where user.UserId == userId 
                //                 && user.NoteId == null select user ).ToListAsync();
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

        /// <summary>
        /// Gets the label by note.
        /// </summary>
        /// <param name="notesId">The notes identifier.</param>
        /// <returns>
        /// returns a list of label by notes
        /// </returns>
        /// <exception cref="System.Exception"></exception>
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
