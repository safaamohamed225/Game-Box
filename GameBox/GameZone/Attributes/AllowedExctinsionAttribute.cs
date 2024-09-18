using System.Xml.Serialization;

namespace GameBox.Attributes
{
    public class AllowedExctinsionAttribute:ValidationAttribute
    {
        private readonly string allowedExtensions;

        public AllowedExctinsionAttribute(string allowedExtensions)
        {
            this.allowedExtensions = allowedExtensions;
        }

        protected override ValidationResult? IsValid(object? value , ValidationContext validationContext)
        {
            var file = value as IFormFile; 
            
            if (file != null) 
            {
                var extension = Path.GetExtension(file.FileName);
                var isAllowed = allowedExtensions.Split(',').Contains(extension, StringComparer.OrdinalIgnoreCase);
                if (!isAllowed) {

                    return new ValidationResult($"Only {allowedExtensions} are allowed");
                
                }

            }

            return ValidationResult.Success;
        }
    }
}
