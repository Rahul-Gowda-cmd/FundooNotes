// <copyright file="ICollaboratorManager.cs" company="Bridgelabz">
//   Copyright © 2021 Company="BridgeLabz"
// </copyright>
// <creator name="Rahul prabu"/>
// --------------------------------------------------------------------------------------------------------------------

namespace FundooManager.Manager
{
    using FundooManager.Interface;
    using FundooModels;
    using FundooRepository.Interface;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>
    /// class LabelManager
    /// </summary>
    /// <seealso cref="FundooManager.Interface.ILabelManager" />
    public class LabelManager : ILabelManager
    {
        /// <summary>
        /// The repositary
        /// </summary>
        private readonly ILabelRepository repositary;

        /// <summary>
        /// Initializes a new instance of the <see cref="LabelManager"/> class.
        /// </summary>
        /// <param name="repositary">The repositary.</param>
        public LabelManager(ILabelRepository repositary)
        {
            this.repositary = repositary;

        }

        /// <summary>
        /// Adds the label.
        /// </summary>
        /// <param name="labelModel">The label model.</param>
        /// <returns>
        /// returns string after successfully adding label without notesId
        /// </returns>
        /// <exception cref="System.Exception"></exception>
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

        /// <summary>
        /// Adds the notes label.
        /// </summary>
        /// <param name="labelModel">The label model.</param>
        /// <returns>
        /// returns a string after adding label from notes
        /// </returns>
        /// <exception cref="System.Exception"></exception>
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

        /// <summary>
        /// Deletes the label from note.
        /// </summary>
        /// <param name="labelId">The label identifier.</param>
        /// <returns>
        /// returns a string after deleting a label from note
        /// </returns>
        /// <exception cref="System.Exception"></exception>
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

        /// <summary>
        /// Deletes the label.
        /// </summary>
        /// <param name="labelId">The label identifier.</param>
        /// <returns>
        /// returns a string after deleting from home
        /// </returns>
        /// <exception cref="System.Exception"></exception>
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
                return this.repositary.GetNotesBasedOnLabel(labelId);
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
