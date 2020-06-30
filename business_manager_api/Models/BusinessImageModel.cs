using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace business_manager_api
{
    [Table(name: "BusinessImage")]
    public class BusinessImageModel
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        private long Id { get; set; }

        public long BusinessId { get; set; }

        public string ImageData { get; set; }
    }
}
