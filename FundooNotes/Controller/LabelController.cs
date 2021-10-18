using FundooManager.Interface;
using FundooModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FundooNotes.Controller
{
    public class LabelController : ControllerBase
    {
        private readonly ILabelManager manager;
        public LabelController(ILabelManager manager)
        {
            this.manager = manager;
        }

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

        [HttpDelete]
        [Route("api/DeleteLabel")]
        public async Task<IActionResult> DeleteLabel(int userId, string labelName)
        {
            try
            {
                string result = await this.manager.DeleteLabel(userId, labelName);
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

        [HttpPost]
        [Route("api/EditLabel")]
        public async Task<IActionResult> EditLabel([FromBody]LabelModel labelModel)
        {
            try
            {
                string result = await this.manager.EditLabel(labelModel);
                if (result == "Updated Label")
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


        [HttpGet]
        [Route("api/DisplayNotesBasedOnLabel")]
        public IActionResult DisplayNotesBasedOnLabel(int userId, string labelName)
        {
            try
            {
                var data = this.manager.DisplayNotesBasedOnLabel(userId, labelName);
                if (data != null)
                {
                    return this.Ok(new { Status = true, Message = "Display Notes Based on Table", Data = data });
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

        [HttpPut]
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

