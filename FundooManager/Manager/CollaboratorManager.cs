using FundooManager.Interface;
using FundooModels;
using FundooRepository.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FundooManager.Manager
{
    public class CollaboratorManager : ICollaboratorManager
    {
        private readonly ICollaboratorRepository CollaboratorRepositary;
        public CollaboratorManager(ICollaboratorRepository CollaboratorRepositary)
        {
            this.CollaboratorRepositary = CollaboratorRepositary;

        }
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
