using FluentValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;

namespace business_manager_api
{
    [Table(name:"business_image")]
    public class BusinessImage
    {
        [Index(IsUnique = true)]
        private long Id { get; set; }

        public long BusinessId { get; set; }

        public string ImageData { get; set; }
    }
    public class BusinessImageValidator : AbstractValidator<BusinessImage>
    {
        private readonly int sizeLimit = 3;
        public BusinessImageValidator()
        {
            RuleFor(x => (x.ImageData.Length <= sizeLimit * 1.37 * 1024 * 1024)).Equal(true);
        }
    }
}
