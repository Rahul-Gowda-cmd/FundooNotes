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
                var emailExists = this.CollaboratorContext.Users.Where(x => x.Email == collaborator.ReciverEmail).FirstOrDefault();

                var owner = (from user in this.CollaboratorContext.Users
                             join notes in this.CollaboratorContext.Notes
                             on user.UserId equals notes.UserId
                             where notes.NoteId == collaborator.NoteId && user.Email == collaborator.ReciverEmail
                             select new { userId = user.UserId }).SingleOrDefault();
                if (owner == null)
                {
                    var colExists = this.CollaboratorContext.Collaboratores.Where(x => x.ReciverEmail == collaborator.ReciverEmail && x.NoteId == collaborator.NoteId).SingleOrDefault();
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
                else
                {
                    message = "This email already exists";
                }

                return message;
            }
            catch (ArgumentNullException ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
