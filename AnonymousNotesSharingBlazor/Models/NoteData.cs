using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AnonymousNotesSharingBlazor.Models
{
    public class NoteData
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? NoteMessage { get; set; }
        [Required]
        public DateTime CreatedDate { get; set; }
    }
}
