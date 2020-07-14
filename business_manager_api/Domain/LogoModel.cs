using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FluentValidation;

namespace business_manager_api
{
    [Table(name: "LogoModel")]
    public class LogoModel
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public long EntityId { get; set; }
        public string ImageData { get; set; }

        public LogoModel(long EntityId, string ImageData)
        {
            this.EntityId = EntityId;
            this.ImageData = ImageData;
        }
    }
    public class LogoValidator : AbstractValidator<LogoModel>
    {
        private readonly int sizeLimit = 1;
        public LogoValidator()
        {
            RuleFor(x => (x.ImageData.Length <= sizeLimit * 1.37 * 1024 * 1024)).Equal(true);
        }
    }
}
