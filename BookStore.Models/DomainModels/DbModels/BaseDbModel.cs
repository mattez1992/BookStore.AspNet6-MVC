using System.ComponentModel.DataAnnotations;

namespace BookStore.Models.DomainModels.DbModels
{
    public class BaseDbModel
    {
        [Key]
        public int Id { get; set; }
    }
}
