
namespace GameBox.Controllers
{
    public class GamesController : Controller
    {
        //private readonly ApplicationDbContext context;
        private readonly ICategoriesService categoriesService;
        private readonly IDevicesService devicesService;
        private readonly IGamesService gamesService;

    
        public GamesController(/*ApplicationDbContext context , */ICategoriesService categoriesService, IDevicesService devicesService, IGamesService gamesService)
        {
            //this.context = context;
            this.categoriesService = categoriesService;
            this.devicesService = devicesService;
            this.gamesService = gamesService;
        }
        public IActionResult Index()
        {
            var games = gamesService.GetAll();
            return View(games);
        }

        public IActionResult Details(int id)
        {
            var game = gamesService.GetGameById(id);
            if (game is null)
                return NotFound();

            return View(game);
        }

        [HttpGet]
        public IActionResult Create()
        {
            CreateGameFormViewModel viewModel = new()
            {
                Categories = categoriesService.GetSelectList(),

                Devices =devicesService.GetListDevices(),
            };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task <IActionResult> Create(CreateGameFormViewModel model)
        {

            if(! ModelState.IsValid)
            {

                model.Categories = categoriesService.GetSelectList();
                model.Devices = devicesService.GetListDevices();
                return View(model);
            }

            await gamesService.Create(model);
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var game= gamesService.GetGameById(id);

            if (game is null)
                return NotFound();

            EditGameFormViewModel viewModel = new()
            {
                Id =id,
                Name=game.Name,
                Description=game.Description,
                CategoryId=game.CategoryId,
                SelectedDevices=game.Devices.Select(g=>g.DeviceId).ToList(),
                Categories=categoriesService.GetSelectList(),
                Devices=devicesService.GetListDevices(),
                CurrentCover=game.Cover
            };
            return View(viewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditGameFormViewModel model)
        {

            if (!ModelState.IsValid)
            {

                model.Categories = categoriesService.GetSelectList();
                model.Devices = devicesService.GetListDevices();
                return View(model);
            }

            var game = await gamesService.Edit(model);
            if (game is null)
                return BadRequest();


            return RedirectToAction(nameof(Index));
          
    
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            //return BadRequest();
            var isDeleted = gamesService.Delete(id);

            return isDeleted ? Ok() : BadRequest();
        }

     
    }
}
