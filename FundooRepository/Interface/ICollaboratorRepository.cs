// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ICollaboratorRepository.cs" company="Bridgelabz">
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
    /// interface ICollaboratorRepository
    /// </summary>
    public interface ICollaboratorRepository
    {
        /// <summary>
        /// Adds the collaborator.
        /// </summary>
        /// <param name="collaborator">The collaborator.</param>
        /// <returns>returns string after adding collaborator</returns>
        Task<string> AddCollaborator(CollaboratorModel collaborator);

        /// <summary>
        /// Removes the collaborator.
        /// </summary>
        /// <param name="CollaboratorId">The collaborator identifier.</param>
        /// <returns>returns string after deleting collaborator</returns>
        Task<string> RemoveCollaborator(int CollaboratorId);

        /// <summary>
        /// Gets the collaborator.
        /// </summary>
        /// <param name="noteId">The note identifier.</param>
        /// <returns>returns string after get collaborator</returns>
        List<CollaboratorModel> GetCollaborator(int noteId);
    }
}
