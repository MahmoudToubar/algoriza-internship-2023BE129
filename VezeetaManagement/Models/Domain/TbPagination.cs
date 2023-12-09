using System.ComponentModel.DataAnnotations;

namespace VezeetaManagement.Models.Domain
{
    public class TbPagination
    {
        [Key]
        public int Pagination { get; set; }
        public int Page { get; set; } 
        public int PageSize { get; set; }
    }
}
