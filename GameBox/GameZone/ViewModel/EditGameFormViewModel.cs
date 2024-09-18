using GameBox.Attributes;

namespace GameBox.ViewModel
{
    public class EditGameFormViewModel:GameFormViewModel
    {
        public int Id { get; set; }
        public string? CurrentCover { get; set; }

        [AllowedExctinsion(FileSettings.AllowedExtentions),
     MaxFileSize(FileSettings.MaxFileSizeInBytes)]
        public IFormFile? Cover { get; set; } = default!;
    }
}
