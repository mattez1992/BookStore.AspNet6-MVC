using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreWeb.Models.DbModels
{
    public class BaseDbModel
    {
        [Key]
        public int Id { get; set; }
    }
}
