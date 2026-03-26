using Microsoft.EntityFrameworkCore;

namespace mytheme.Models
{
   
    public class PurchasesViewModel
    {
        public int Id { get; set; }
        public string ImageLink { get; set; }      // From product
        public string Name { get; set; }           // From product
        public string Description { get; set; }    // From product
        public int ProductId { get; set; }         // From product

        public string Message { get; set; }        // From card
        public string FinalImagePath { get; set; } // From card
        public string CreatedAt { get; set; }      // From card
    }
}
