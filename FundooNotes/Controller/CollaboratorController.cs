// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ICollaboratorManager.cs" company="Bridgelabz">
//   Copyright © 2021 Company="BridgeLabz"
// </copyright>
// <creator name="Rahul prabu"/>
// --------------------------------------------------------------------------------------------------------------------

namespace FundooNotes.Controller
{
    using FundooManager.Interface;
    using FundooModels;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>
    /// class CollaboratorController : ControllerBase
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    [Authorize]
    public class CollaboratorController : ControllerBase
    {
        /// <summary>
        /// The collaborator manager
        /// </summary>
        private readonly ICollaboratorManager collaboratorManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="CollaboratorController"/> class.
        /// </summary>
        /// <param name="collaboratorManager">The collaborator manager.</param>
        public CollaboratorController(ICollaboratorManager collaboratorManager)
        {
            this.collaboratorManager = collaboratorManager;

        }

        /// <summary>
        /// Adds the collaborator.
        /// </summary>
        /// <param name="collaboratorModel">The collaborator model.</param>
        /// <returns>returns IActionResult status code after adding collaborator</returns>
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

        /// <summary>
        /// Gets the collaborator.
        /// </summary>
        /// <param name="noteId">The note identifier.</param>
        /// <returns>returns IActionResult status code after get collaboratorreturns>
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

        /// <summary>
        /// Removes the collaborator.
        /// </summary>
        /// <param name="CollaboratorId">The collaborator identifier.</param>
        /// <returns>returns IActionResult status code after deleting collaborator</returns>
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
