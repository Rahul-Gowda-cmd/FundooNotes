using FundooManager.Interface;
using FundooModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FundooNotes.Controller
{
    public class NotesController : ControllerBase
    {
        private readonly INotesManager notesManager;

        public NotesController(INotesManager notesManager)
        {
            this.notesManager = notesManager;
        }

        [HttpPost]
        [Route("api/addNotes")]
        public IActionResult AddNotes([FromBody] NotesModel note)
        {
            try
            {
                string resultMessage =this.notesManager.AddNotes(note);
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
    }
}
