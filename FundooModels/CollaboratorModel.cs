using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FundooModels
{
    public class CollaboratorModel
    {
        [Key]
        public int CollaboratorId { get; set; }

        [ForeignKey("NotesModel")]
        public int NoteId { get; set; }
        public virtual NotesModel NotesModel { get; set; }

        [Required]
        [RegularExpression(@"^[A-Za-z0-9]+([\.+\-_][A-Za-z0-9]+)*@[a-zA-Z0-9]+\.?[A-Za-z]+\.?[A-Za-z]{2,}$", ErrorMessage = "Email is not valid, Enter valid Email")]
        public string CollaboratorEmail { get; set; }
    }
}
