using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class AddUpdateItemViewModel
    {

        public int BranchID { get; set; }
        public int OldItemID { get; set; }
        public List<string>ItemNames { get; set; }
        [Required()]
        public List<int>ItemIds { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Please enter valid integer Number be carful not zero")]
        [Required()]
        public int Quantity { get; set; }
    }
}
