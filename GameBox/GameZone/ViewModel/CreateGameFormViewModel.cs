

using GameBox.Attributes;

namespace GameBox.ViewModel
{
    public class CreateGameFormViewModel:GameFormViewModel
    {

        [AllowedExctinsion(FileSettings.AllowedExtentions),
       MaxFileSize(FileSettings.MaxFileSizeInBytes)]
        public IFormFile Cover { get; set; } = default!;

    }
}
