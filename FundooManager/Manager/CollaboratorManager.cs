// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CollaboratorManager.cs" company="Bridgelabz">
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
    ///  class CollaboratorManager
    /// </summary>
    /// <seealso cref="FundooManager.Interface.ICollaboratorManager" />
    public class CollaboratorManager : ICollaboratorManager
    {
        /// <summary>
        /// The collaborator repositary
        /// </summary>
        private readonly ICollaboratorRepository CollaboratorRepositary;

        /// <summary>
        /// Initializes a new instance of the <see cref="CollaboratorManager"/> class.
        /// </summary>
        /// <param name="CollaboratorRepositary">The collaborator repositary.</param>
        public CollaboratorManager(ICollaboratorRepository CollaboratorRepositary)
        {
            this.CollaboratorRepositary = CollaboratorRepositary;

        }

        /// <summary>
        /// Adds the collaborator.
        /// </summary>
        /// <param name="collaborator">The collaborator.</param>
        /// <returns>
        /// returns string after adding collaborator
        /// </returns>
        /// <exception cref="System.Exception"></exception>
        public Task<string> AddCollaborator(CollaboratorModel collaborator)
        {
            try
            {
                return this.CollaboratorRepositary.AddCollaborator(collaborator);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Gets the collaborator.
        /// </summary>
        /// <param name="noteId">The note identifier.</param>
        /// <returns>
        /// returns string after get collaborator
        /// </returns>
        /// <exception cref="System.Exception"></exception>
        public List<CollaboratorModel> GetCollaborator(int noteId)
        {
            try
            {
                return this.CollaboratorRepositary.GetCollaborator(noteId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Removes the collaborator.
        /// </summary>
        /// <param name="CollaboratorId">The collaborator identifier.</param>
        /// <returns>
        /// returns string after deleting collaborator
        /// </returns>
        /// <exception cref="System.Exception"></exception>
        public Task<string> RemoveCollaborator(int CollaboratorId)
        {
            try
            {
                return this.CollaboratorRepositary.RemoveCollaborator(CollaboratorId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
