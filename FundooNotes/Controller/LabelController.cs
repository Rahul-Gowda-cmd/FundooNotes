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
    /// class LabelController 
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    [Authorize]
    public class LabelController : ControllerBase
    {
        /// <summary>
        /// The manager
        /// </summary>
        private readonly ILabelManager manager;

        /// <summary>
        /// Initializes a new instance of the <see cref="LabelController"/> class.
        /// </summary>
        /// <param name="manager">The manager.</param>
        public LabelController(ILabelManager manager)
        {
            this.manager = manager;
        }

        /// <summary>
        /// Adds the label.
        /// </summary>
        /// <param name="labelModel">The label model.</param>
        /// <returns>returns IActionResult Status Code after successfully adding label without notesId<</returns>
        [HttpPost]
        [Route("api/AddLabel")]
        public async Task<IActionResult> AddLabel([FromBody] LabelModel labelModel)
        {
            try
            {
                string result = await this.manager.AddLabel(labelModel);
                if (result == "Added Label")
                {
                    return this.Ok(new ResponseModel<string>() { Status = true, Message = result});
                }

                return this.BadRequest(new ResponseModel<string>() { Status = false, Message = result });
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }

        /// <summary>
        /// Adds the notes label.
        /// </summary>
        /// <param name="labelModel">The label model.</param>
        /// <returns>returns a IActionResult Status Code after adding label from notes</returns>
        [HttpPost]
        [Route("api/AddNotesLabel")]
        public async Task<IActionResult> AddNotesLabel([FromBody] LabelModel labelModel)
        {
            try
            {
                string result = await this.manager.AddNotesLabel(labelModel);
                if (result == "Added Label To Note")
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

        /// <summary>
        /// Deletes the label from note.
        /// </summary>
        /// <param name="labelId">The label identifier.</param>
        /// <returns>returns a string after deleting a label from note</returns>
        [HttpDelete]
        [Route("api/DeleteLabelFromNote")]
        public async Task<IActionResult> DeleteLabelFromNote(int labelId)
        {
            try
            {
                string result = await this.manager.DeleteLabelFromNote(labelId);
                if (result == "Deleted Label From Note")
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

        /// <summary>
        /// Deletes the label.
        /// </summary>
        /// <param name="labelId">The label identifier.</param>
        /// <returns>returns a IActionResult Status Code after deleting from home</returns>
        [HttpDelete]
        [Route("api/DeleteLabel")]
        public async Task<IActionResult> DeleteLabel(int labelId)
        {
            try
            {
                var result = await this.manager.DeleteLabel(labelId);
                if (result == "Deleted Label")
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

        /// <summary>
        /// Edits the label.
        /// </summary>
        /// <param name="labelModel">The label model.</param>
        /// <returns>returns a IActionResult Status Code after editing label</returns>
        [HttpPut]
        [Route("api/EditLabel")]
        public async Task<IActionResult> EditLabel([FromBody]LabelModel labelModel)
        {
            try
            {
                string result = await this.manager.EditLabel(labelModel);
                if (result == "Label is Updated")
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

        /// <summary>
        /// Gets the notes based on label.
        /// </summary>
        /// <param name="labelId">The label identifier.</param>
        /// <returns>return Display Notes Based on Lable</returns>
        [HttpGet]
        [Route("api/GetNotesBasedOnLabel")]
        public IActionResult GetNotesBasedOnLabel(int labelId)
        {
            try
            {
                var data = this.manager.GetNotesBasedOnLabel(labelId);
                if (data != null)
                {
                    return this.Ok(new { Status = true, Message = "Display Notes Based on Lable", Data = data });
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
        /// Gets the label by note identifier.
        /// </summary>
        /// <param name="notesId">The notes identifier.</param>
        /// <returns>returns a list of label by notes</returns>
        [HttpGet]
        [Route("api/GetLabelByNote")]
        public IActionResult GetLabelByNoteId(int notesId)
        {
            try
            {
                var result = this.manager.GetLabelByNote(notesId);
                if (result != null)
                {
                    return this.Ok(new ResponseModel<List<LabelModel>>() { Status = true, Message = "Retrieved Label", Data = result });
                }

                return this.BadRequest(new ResponseModel<List<string>>() { Status = false, Message = "Retrieved Label Failed" });
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }

        /// <summary>
        /// Gets the label.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>returns a IActionResult Status Code for getting labels based on userID</returns>
        [HttpGet]
        [Route("api/GetLabel")]
        public IActionResult GetLabel(int userId)
        {
            try
            {
                var result = this.manager.GetLabel(userId);
                if (result != null)
                {
                    return this.Ok(new { Status = true, Message = "Get label", Data = result });
                }

                return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "Get label Failed" });
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }
    }
}

