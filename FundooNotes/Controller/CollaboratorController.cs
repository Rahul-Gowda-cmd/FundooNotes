using FundooManager.Interface;
using FundooModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FundooNotes.Controller
{
    //[Authorize]
    public class CollaboratorController : ControllerBase
    {
        private readonly ICollaboratorManager collaboratorManager;
        public CollaboratorController(ICollaboratorManager collaboratorManager)
        {
            this.collaboratorManager = collaboratorManager;

        }

        [HttpPost]
        [Route("api/addCollaborator")]
        public async Task<IActionResult> AddCollaborator([FromBody] CollaboratorModel collaboratorModel)
        {
            try
            {
                string result =await this.collaboratorManager.AddCollaborator(collaboratorModel);
                if (result == "Collaborator Added!")
                {
                    return this.Ok(new ResponseModel<string>() { Status = true, Message = result });
                }
                else if (result == "This email already exists")
                {
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = result });
                }
                else
                {
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = result });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }

        [HttpGet]
        [Route("api/getcollaboratornotes")]
        public IActionResult GetCollaborator(int noteId)
        {
            try
            {
                List<CollaboratorModel> data = this.collaboratorManager.GetCollaborator(noteId);
                if (data != null)
                {
                    return this.Ok(new { Status = true, Message = "Get Collaborator Notes Successfull", Data = data });
                }
                else
                {
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "Get Notes Failed" });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }

        [HttpPut]
        [Route("api/RemoveCollaborator")]
        public async Task<IActionResult> RemoveCollaborator(int CollaboratorId)
        {
            try
            {
                string result = await this.collaboratorManager.RemoveCollaborator(CollaboratorId);
                if (result == "Collaborator Removed Successfully")
                {
                    return this.Ok(new ResponseModel<string>() { Status = true, Message = result });
                }

                return this.BadRequest(new ResponseModel<string>() { Status = false, Message = result });
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }
    }
}
