// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ILabelRepository.cs" company="Bridgelabz">
//   Copyright © 2021 Company="BridgeLabz"
// </copyright>
// <creator name="Rahul prabu"/>
// --------------------------------------------------------------------------------------------------------------------

namespace FundooRepository.Interface
{
    using FundooModels;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>
    /// interface ILabelRepository
    /// </summary>
    public interface ILabelRepository
    {
        /// <summary>
        /// Adds the label.
        /// </summary>
        /// <param name="labelModel">The label model.</param>
        /// <returns>returns string after successfully adding label without notesId</returns>
        Task<string> AddLabel(LabelModel labelModel);

        /// <summary>
        /// Deletes the label.
        /// </summary>
        /// <param name="labelId">The label identifier.</param>
        /// <returns>returns a string after deleting from home</returns>
        Task<string> DeleteLabel(int labelId);

        /// <summary>
        /// Edits the label.
        /// </summary>
        /// <param name="labelModel">The label model.</param>
        /// <returns>returns a string after editing label</returns>
        Task<string> EditLabel(LabelModel labelModel);

        /// <summary>
        /// Gets the label.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>returns a list for getting labels based on userID</returns>
        List<LabelModel> GetLabel(int userId);

        /// <summary>
        /// Adds the notes label.
        /// </summary>
        /// <param name="labelModel">The label model.</param>
        /// <returns>returns a string after adding label from notes</returns>
        Task<string> AddNotesLabel(LabelModel labelModel);

        /// <summary>
        /// Deletes the label from note.
        /// </summary>
        /// <param name="labelId">The label identifier.</param>
        /// <returns>returns a string after deleting a label from note</returns>
        Task<string> DeleteLabelFromNote(int labelId);

        /// <summary>
        /// Gets the label by note.
        /// </summary>
        /// <param name="notesId">The notes identifier.</param>
        /// <returns>returns a list of label by notes</returns>
        List<LabelModel> GetLabelByNote(int notesId);

        /// <summary>
        /// Gets the notes based on label.
        /// </summary>
        /// <param name="labelId">The label identifier.</param>
        /// <returns>returns a list of notes based on labe</returns>
        List<NotesModel> GetNotesBasedOnLabel(int labelId);
    }
}
