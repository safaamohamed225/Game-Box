namespace GameBox.Attributes
{
    public class MaxFileSizeAttribute:ValidationAttribute
    {

        private readonly int fileSizeAllowed;

        public MaxFileSizeAttribute(int fileSizeAllowed)
        {
            this.fileSizeAllowed = fileSizeAllowed;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var file = value as IFormFile;

            if (file != null)
            {
               
                if (file.Length > fileSizeAllowed)
                {

                    return new ValidationResult($"Maximum allowed size is {fileSizeAllowed} Bytes");

                }

            }

            return ValidationResult.Success;
        }
    }
}
