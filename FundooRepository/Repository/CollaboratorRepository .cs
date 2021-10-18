using FundooModels;
using FundooRepository.Context;
using FundooRepository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FundooRepository.Repository
{
    public class CollaboratorRepository: ICollaboratorRepository
    {
        private readonly UserContext CollaboratorContext;

        public CollaboratorRepository(UserContext CollaboratorContext)
        {
            this.CollaboratorContext = CollaboratorContext;
        }
        public string AddCollaborator(CollaboratorModel collaborator)
        {
            try
            {
                string message = string.Empty;
                var owner = (from user in this.CollaboratorContext.Users
                             join notes in this.CollaboratorContext.Notes
                             on user.UserId equals notes.UserId
                             where notes.NoteId == collaborator.NoteId && user.Email == collaborator.SenderEmail
                             select new { userId = user.UserId }).SingleOrDefault();
                if (owner != null)
                {
                    var colExists = this.CollaboratorContext.Collaboratores.Where(x => x.ReciverEmail == collaborator.ReciverEmail).SingleOrDefault();
                    if (colExists == null)
                    {
                        this.CollaboratorContext.Add(collaborator);
                        this.CollaboratorContext.SaveChanges();
                        message = "Collaborator Added!";
                    }
                    else
                    {
                        message = "This email already exists";
                    }
                }
                return message= "This email Not exists in UserModel";
            }
            catch (ArgumentNullException ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
