using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BugTrackerModel
{
    public class Bug
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BugId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public BugState State { get; set; } = BugState.Open;

        [DataType(DataType.Date)]
        public DateTime ReportedDate { get; set; }

        public int? PersonId { get; set; }
        public virtual Person? Person { get; set; }

    }
    public enum BugState
    {
        Open,
        Closed
    }

}
