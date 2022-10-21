using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BugTrackerModel
{
    public class Person
    {
        [ForeignKey("Bug")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PersonId { get; set; }
        public string Name { get; set; }

    }
}
