using FundooManager.Interface;
using FundooModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FundooNotes.Controller
{
    [Authorize]
    public class NotesController : ControllerBase
    {
        private readonly INotesManager notesManager;

        public NotesController(INotesManager notesManager)
        {
            this.notesManager = notesManager;
        }

        [HttpPost]
        [Route("api/addNotes")]
        public async Task<IActionResult> AddNotes([FromBody] NotesModel note)
        {
            try
            {
                string resultMessage =await this.notesManager.AddNotes(note);
                if (resultMessage.Equals("Note Added Successfully"))
                {
                    return this.Ok(new ResponseModel<string>() { Status = true, Message = resultMessage });
                }
                else
                {
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = resultMessage });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }

        [HttpGet]
        [Route("api/GetNotes")]
        public IActionResult GetNotes(int userId)
        {
            try
            {
                List<NotesModel> resultMessage = this.notesManager.GetNotes(userId);
                if (resultMessage!=null)
                {
                    return this.Ok(new  { Status = true, Message = "Get Successfully",Data= resultMessage });
                }
                else
                {
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message ="Get Failed",Data= null });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }

        [HttpPut]
        [Route("api/UpdateNotes")]
        public async Task<IActionResult> UpdateNote([FromBody]NotesModel updateNote)
        {
            try
            {
                string resultMessage =await this.notesManager.UpdateNote(updateNote);
                if (resultMessage == ("Notes Updated"))
                {
                    return this.Ok(new ResponseModel<string>() { Status = true, Message = resultMessage });
                }
                else
                {
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = resultMessage });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }

        [HttpPost]
        [Route("api/Deletenote")]
        public async Task<IActionResult> Deletenote(int notesId)
        {
            try
            {               
                bool resultMessage = await this.notesManager.Deletenote(notesId);
                if (resultMessage)
                {
                    return this.Ok(new ResponseModel<string>() { Status = true, Message = "Note is Deleted " });
                }
                else 
                {
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "Invalid Id" });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }

        [HttpPost]
        [Route("api/RestoreNote")]
        public async Task<IActionResult> RestoreNote(int notesId)
        {
            try
            {
                string resultMessage = await this.notesManager.RestoreNote(notesId);
                if (resultMessage == ("Notes is restored"))
                {
                    return this.Ok(new ResponseModel<string>() { Status = true, Message = resultMessage });
                }
                else
                {
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = resultMessage });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }

        [HttpGet]
        [Route("api/GetArchive")]
        public IActionResult GetArchive(int notesId)
        {
            try
            {

                List<NotesModel> resultMessage = this.notesManager.GetArchive(notesId);
                if (resultMessage != null)
                {
                    return this.Ok(new { Status = true, Message = "Get Archive", Data = resultMessage });
                }
                else
                {
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "Archive Not Found", Data = null });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }

        [HttpGet]
        [Route("api/GetTrash")]
        public IActionResult GetTrash(int userId)
        {
            try
            {
                List<NotesModel> resultMessage = this.notesManager.GetTrash(userId);
                if (resultMessage != null)
                {
                    return this.Ok(new { Status = true, Message = "Get Trash", Data = resultMessage });
                }
                else
                {
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "Trash Not Found", Data = null });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }


        [HttpPost]
        [Route("api/PinNote")]
        public async Task<IActionResult> PinNote(int notesId)
        {
            try
            {
                string resultMessage = await this.notesManager.PinNote(notesId);
                if (resultMessage == ("Pin Notes"))
                {
                    return this.Ok(new ResponseModel<string>() { Status = true, Message = resultMessage });
                }
                else
                {
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = resultMessage });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }

        [HttpPost]
        [Route("api/UnPinNote")]
        public async Task<IActionResult> UnPinNote(int notesId)
        {
            try
            {
                string resultMessage = await this.notesManager.UnPinNote(notesId);
                if (resultMessage == ("UnPin the Notes"))
                {
                    return this.Ok(new ResponseModel<string>() { Status = true, Message = resultMessage });
                }
                else
                {
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = resultMessage });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }

        [HttpPost]
        [Route("api/ArchiveNote")]
        public async Task<IActionResult> ArchiveNote(int notesId)
        {
            try
            {
                string resultMessage = await this.notesManager.ArchiveNote(notesId);
                if (resultMessage == ("Archive Note"))
                {
                    return this.Ok(new ResponseModel<string>() { Status = true, Message = resultMessage });
                }
                else
                {
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = resultMessage });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }

        [HttpPost]
        [Route("api/UnArchiveNote")]
        public async Task<IActionResult> UnArchiveNote(int notesId)
        {
            try
            {
                string resultMessage = await this.notesManager.UnArchiveNote(notesId);
                if (resultMessage == ("UnArchived Note"))
                {
                    return this.Ok(new ResponseModel<string>() { Status = true, Message = resultMessage });
                }
                else
                {
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = resultMessage });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }

        [HttpPost]
        [Route("api/SetRemainder")]
        public async Task<IActionResult> SetRemainder(int notesId, string Time)
        {
            try
            {

                string resultMessage = await this.notesManager.SetRemainder(notesId, Time);
                if (resultMessage == ("Reminder is sets Successfuly"))
                {
                    return this.Ok(new ResponseModel<string>() { Status = true, Message = resultMessage });
                }
                else
                {
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = resultMessage });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }

        [HttpPost]
        [Route("api/RemoveRemainder")]
        public async Task<IActionResult> RemoveRemainder(int notesId)
        {
            try
            {

                string resultMessage = await this.notesManager.RemoveRemainder(notesId);
                if (resultMessage == ("Reminder is removed"))
                {
                    return this.Ok(new ResponseModel<string>() { Status = true, Message = resultMessage });
                }
                else
                {
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = resultMessage });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }

        [HttpPost]
        [Route("api/AddColor")]
        public async Task<IActionResult> AddColor(int notesId, string color)
        {
            try
            {

                string resultMessage = await this.notesManager.AddColor(notesId,color);
                if (resultMessage == ("Color Added Successfuly"))
                {
                    return this.Ok(new ResponseModel<string>() { Status = true, Message = resultMessage });
                }
                else
                {
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = resultMessage });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }

        [HttpPut]
        [Route("api/addImage")]
        public async Task<IActionResult> AddImage(int noteId, int userId, IFormFile image)
        {
            try
            {
                string resultMessage = await this.notesManager.AddImage(noteId, userId, image);
                if (resultMessage == "Image added")
                {
                    return this.Ok(new ResponseModel<string>() { Status = true, Message = resultMessage });
                }

                return this.BadRequest(new ResponseModel<string>() { Status = false, Message = resultMessage });
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<string>() { Status = true, Message = ex.Message });
            }
        }

        [HttpPut]
        [Route("api/removeImage")]
        public async Task<IActionResult> RemoveImage(int noteId)
        {
            try
            {
                string resultMessage = await this.notesManager.RemoveImage(noteId);
                if (resultMessage == "Image removed")
                {
                    return this.Ok(new ResponseModel<string>() { Status = true, Message = resultMessage });
                }

                return this.BadRequest(new ResponseModel<string>() { Status = false, Message = resultMessage });
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<string>() { Status = true, Message = ex.Message });
            }
        }
    }
}
