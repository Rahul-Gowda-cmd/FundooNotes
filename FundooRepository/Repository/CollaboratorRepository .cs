using FundooModels;
using FundooRepository.Context;
using FundooRepository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FundooRepository.Repository
{
    public class CollaboratorRepository: ICollaboratorRepository
    {
        private readonly UserContext CollaboratorContext;

        public CollaboratorRepository(UserContext CollaboratorContext)
        {
            this.CollaboratorContext = CollaboratorContext;
        }
        public async Task<string> AddCollaborator(CollaboratorModel collaborator)
        {
            try
            {
                var owner = (from user in this.CollaboratorContext.Users
                             join notes in this.CollaboratorContext.Notes
                             on user.UserId equals notes.UserId
                             where notes.NoteId == collaborator.NoteId && user.Email == collaborator.SenderEmail
                             select new { userId = user.UserId }).SingleOrDefault();
                if (owner != null)
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
                return "Sender Email does not Match";
            }
            catch (ArgumentNullException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<CollaboratorModel> GetCollaborator(int noteId)
        {
            try
            {
                var Check = this.CollaboratorContext.Collaboratores.Any(e => e.NoteId == noteId);
                if (Check)
                {
                    var list = this.CollaboratorContext.Collaboratores.Where(e => e.NoteId == noteId && e.ReciverEmail != null).ToList();
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
