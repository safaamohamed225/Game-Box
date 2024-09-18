namespace GameBox.ViewModel
{
    public class GameFormViewModel
    {
        [MaxLength(200)]
        public string Name { get; set; } = string.Empty;

        [Display(Name = "Category")]
        public int CategoryId { get; set; }
        public IEnumerable<SelectListItem> Categories { get; set; } = Enumerable.Empty<SelectListItem>();

        [Display(Name = "Devices")]
        public List<int> SelectedDevices { get; set; } = default!;//new List<int>();
        public IEnumerable<SelectListItem> Devices { get; set; } = Enumerable.Empty<SelectListItem>();

        [MaxLength(2000)]
        public string Description { get; set; } = string.Empty;
   
    }
}
