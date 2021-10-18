using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FundooModels
{
    public class LabelModel
    {
        [Key]
        public int LabelId { get; set; }

        [ForeignKey("NotesModel")]
        public int? NoteId { get; set; }
        public virtual NotesModel NotesModel { get; set; }

        [ForeignKey("UserModel")]
        public int UserId { get; set; }
        public virtual UserModel UserModel { get; set; }

        [Required]
        public string LabelName { get; set; }
    }
}
