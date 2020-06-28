using System.ComponentModel.DataAnnotations;

namespace LetsCodeApp.Models.BaseModels
{
    public class DbEntityBase
    {
        [Key]
        public int Id { get; set; }
    }
}
