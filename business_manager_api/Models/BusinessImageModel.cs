using FluentValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;

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
    public class BusinessImageValidator : AbstractValidator<BusinessImageModel>
    {
        private readonly int sizeLimit = 3;
        public BusinessImageValidator()
        {
            RuleFor(x => (x.ImageData.Length <= sizeLimit * 1.37 * 1024 * 1024)).Equal(true);
        }
    }
}
