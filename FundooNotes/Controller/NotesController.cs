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
        public async Task<IActionResult> NotetoTrash(int notesId)
        {
            try
            {
                
                bool resultMessage = await this.notesManager.NotetoTrash(notesId);
                if (resultMessage)
                {
                    return this.Ok(new ResponseModel<string>() { Status = true, Message = "Note is Stored in trash " });
                }
                else
                {
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "Invalid Id " });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }

        //[HttpPost]
        //[Route("api/Deletenote")]
        //public async Task<IActionResult> NotetoTrash(int notesId)
        //{
        //    try
        //    {

        //        bool resultMessage = await this.notesManager.NotetoTrash(notesId);
        //        if (resultMessage)
        //        {
        //            return this.Ok(new ResponseModel<string>() { Status = true, Message = "Note is Stored in trash " });
        //        }
        //        else
        //        {
        //            return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "Invalid Id " });
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
        //    }
        //}

        //[HttpPost]
        //[Route("api/Deletenote")]
        //public async Task<IActionResult> NotetoTrash(int notesId)
        //{
        //    try
        //    {

        //        bool resultMessage = await this.notesManager.NotetoTrash(notesId);
        //        if (resultMessage)
        //        {
        //            return this.Ok(new ResponseModel<string>() { Status = true, Message = "Note is Stored in trash " });
        //        }
        //        else
        //        {
        //            return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "Invalid Id " });
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
        //    }
        //}

        //[HttpPost]
        //[Route("api/Deletenote")]
        //public async Task<IActionResult> NotetoTrash(int notesId)
        //{
        //    try
        //    {

        //        bool resultMessage = await this.notesManager.NotetoTrash(notesId);
        //        if (resultMessage)
        //        {
        //            return this.Ok(new ResponseModel<string>() { Status = true, Message = "Note is Stored in trash " });
        //        }
        //        else
        //        {
        //            return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "Invalid Id " });
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
        //    }
        //}

        //[HttpPost]
        //[Route("api/Deletenote")]
        //public async Task<IActionResult> NotetoTrash(int notesId)
        //{
        //    try
        //    {

        //        bool resultMessage = await this.notesManager.NotetoTrash(notesId);
        //        if (resultMessage)
        //        {
        //            return this.Ok(new ResponseModel<string>() { Status = true, Message = "Note is Stored in trash " });
        //        }
        //        else
        //        {
        //            return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "Invalid Id " });
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
        //    }
        //}

        //[HttpPost]
        //[Route("api/Deletenote")]
        //public async Task<IActionResult> NotetoTrash(int notesId)
        //{
        //    try
        //    {

        //        bool resultMessage = await this.notesManager.NotetoTrash(notesId);
        //        if (resultMessage)
        //        {
        //            return this.Ok(new ResponseModel<string>() { Status = true, Message = "Note is Stored in trash " });
        //        }
        //        else
        //        {
        //            return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "Invalid Id " });
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
        //    }
        //}

        //[HttpPost]
        //[Route("api/Deletenote")]
        //public async Task<IActionResult> NotetoTrash(int notesId)
        //{
        //    try
        //    {

        //        bool resultMessage = await this.notesManager.NotetoTrash(notesId);
        //        if (resultMessage)
        //        {
        //            return this.Ok(new ResponseModel<string>() { Status = true, Message = "Note is Stored in trash " });
        //        }
        //        else
        //        {
        //            return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "Invalid Id " });
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
        //    }
        //}

        //[HttpPost]
        //[Route("api/Deletenote")]
        //public async Task<IActionResult> NotetoTrash(int notesId)
        //{
        //    try
        //    {

        //        bool resultMessage = await this.notesManager.NotetoTrash(notesId);
        //        if (resultMessage)
        //        {
        //            return this.Ok(new ResponseModel<string>() { Status = true, Message = "Note is Stored in trash " });
        //        }
        //        else
        //        {
        //            return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "Invalid Id " });
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
        //    }
        //}

        //[HttpPost]
        //[Route("api/Deletenote")]
        //public async Task<IActionResult> NotetoTrash(int notesId)
        //{
        //    try
        //    {

        //        bool resultMessage = await this.notesManager.NotetoTrash(notesId);
        //        if (resultMessage)
        //        {
        //            return this.Ok(new ResponseModel<string>() { Status = true, Message = "Note is Stored in trash " });
        //        }
        //        else
        //        {
        //            return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "Invalid Id " });
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
        //    }
        //}

        //    [HttpPost]
        //    [Route("api/Deletenote")]
        //    public async Task<IActionResult> NotetoTrash(int notesId)
        //    {
        //        try
        //        {

        //            bool resultMessage = await this.notesManager.NotetoTrash(notesId);
        //            if (resultMessage)
        //            {
        //                return this.Ok(new ResponseModel<string>() { Status = true, Message = "Note is Stored in trash " });
        //            }
        //            else
        //            {
        //                return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "Invalid Id " });
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
        //        }
        //    }
        
    }
}
