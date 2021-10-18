using FundooManager.Interface;
using FundooModels;
using FundooRepository.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace FundooManager.Manager
{
    public class CollaboratorManager : ICollaboratorManager
    {
        private readonly ICollaboratorRepository CollaboratorRepositary;
        public CollaboratorManager(ICollaboratorRepository CollaboratorRepositary)
        {
            this.CollaboratorRepositary = CollaboratorRepositary;

        }
        public string AddCollaborator(CollaboratorModel collaborator)
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
    }
}
