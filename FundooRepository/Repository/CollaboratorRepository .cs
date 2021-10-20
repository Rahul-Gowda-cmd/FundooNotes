// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CollaboratorRepository.cs" company="Bridgelabz">
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
    /// class CollaboratorRepository: ICollaboratorRepository
    /// </summary>
    /// <seealso cref="FundooRepository.Interface.ICollaboratorRepository" />
    public class CollaboratorRepository: ICollaboratorRepository
    {
        /// <summary>
        /// The collaborator context
        /// </summary>
        private readonly UserContext CollaboratorContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="CollaboratorRepository"/> class.
        /// </summary>
        /// <param name="CollaboratorContext">The collaborator context.</param>
        public CollaboratorRepository(UserContext CollaboratorContext)
        {
            this.CollaboratorContext = CollaboratorContext;
        }

        /// <summary>
        /// Adds the collaborator.
        /// </summary>
        /// <param name="collaborator">The collaborator.</param>
        /// <returns>
        /// returns string after adding collaborator
        /// </returns>
        /// <exception cref="System.Exception"></exception>
        public async Task<string> AddCollaborator(CollaboratorModel collaborator)
        {
            try
            {
                var colExists = this.CollaboratorContext.Collaboratores.Where(x => x.ReciverEmail == collaborator.ReciverEmail && x.NoteId == collaborator.NoteId).SingleOrDefault();
                if (colExists == null)
                {
                    this.CollaboratorContext.Add(collaborator);
                    await this.CollaboratorContext.SaveChangesAsync();
                    return "Collaborator Added!";
                }
                else
                {
                  return "This email already exists";
                }
            }
            catch (ArgumentNullException ex)
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
                var Check = this.CollaboratorContext.Collaboratores.Any(e => e.NoteId == noteId);
                if (Check)
                {
                    var list = this.CollaboratorContext.Collaboratores.Where(e => e.NoteId == noteId).ToList();
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
        /// Removes the collaborator.
        /// </summary>
        /// <param name="CollaboratorId">The collaborator identifier.</param>
        /// <returns>
        /// returns string after deleting collaborator
        /// </returns>
        /// <exception cref="System.ArgumentNullException"></exception>
        public async Task<string> RemoveCollaborator(int CollaboratorId)
        {
            try
            {
                var receiverEmailExist = this.CollaboratorContext.Collaboratores.Where(x => x.CollaboratorId == CollaboratorId).SingleOrDefault();
                if (receiverEmailExist != null)
                {
                    this.CollaboratorContext.Collaboratores.Remove(receiverEmailExist);
                    await this.CollaboratorContext.SaveChangesAsync();
                    return "Collaborator Removed Successfully";
                }
                return "Invalid CollaboratorId";
            }
            catch (ArgumentNullException ex)
            {
                throw new ArgumentNullException(ex.Message);
            }
        }
    }
}
