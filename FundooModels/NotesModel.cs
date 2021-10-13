using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FundooModels
{
    public class NotesModel
    {
        [Key]
        public int NoteId { get; set; }

        [ForeignKey("UserModel")]
        public int UserId { get; set; }
        public virtual UserModel UserModel{get; set;}
        public string Title { get; set; }
        public string Body { get; set; }
        public string Reminder { get; set; }
        public string Color { get; set; }
        public string Image { get; set; }

        [DefaultValue(false)]
        public bool IsArchived { get; set; }

        [DefaultValue(false)]
        public bool IsTrash { get; set; }

        [DefaultValue(false)]
        public bool IsPin { get; set; }

    }
}
