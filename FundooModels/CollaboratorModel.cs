// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CollaboratorModel.cs" company="Bridgelabz">
//   Copyright © 2021 Company="BridgeLabz"
// </copyright>
// <creator name="Rahul prabu"/>
// --------------------------------------------------------------------------------------------------------------------

namespace FundooModels
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// class CollaboratorModel
    /// </summary>
    public class CollaboratorModel
    {
        /// <summary>
        /// Gets or sets the collaborator identifier.
        /// </summary>
        /// <value>
        /// The collaborator identifier.
        /// </value>
        [Key]
        public int CollaboratorId { get; set; }

        /// <summary>
        /// Gets or sets the note identifier.
        /// </summary>
        /// <value>
        /// The note identifier.
        /// </value>
        [ForeignKey("NotesModel")]
        public int NoteId { get; set; }
        
        /// <summary>
        /// Gets or sets the notes model.
        /// </summary>
        /// <value>
        /// The notes model.
        /// </value>
        public virtual NotesModel NotesModel { get; set; }

        /// <summary>
        /// Gets or sets the sender email.
        /// </summary>
        /// <value>
        /// The sender email.
        /// </value>
        [Required]
        [RegularExpression(@"^[A-Za-z0-9]+([\.+\-_][A-Za-z0-9]+)*@[a-zA-Z0-9]+\.?[A-Za-z]+\.?[A-Za-z]{2,}$", ErrorMessage = "Email is not valid, Enter valid Email")]
        public string SenderEmail { get; set; }

        /// <summary>
        /// Gets or sets the reciver email.
        /// </summary>
        /// <value>
        /// The reciver email.
        /// </value>
        [Required]
        [RegularExpression(@"^[A-Za-z0-9]+([\.+\-_][A-Za-z0-9]+)*@[a-zA-Z0-9]+\.?[A-Za-z]+\.?[A-Za-z]{2,}$", ErrorMessage = "Email is not valid, Enter valid Email")]
        public string ReciverEmail { get; set; }

    }
}
