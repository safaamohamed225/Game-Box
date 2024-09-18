


using GameBox.ViewModel;

namespace GameBox.Services
{
    public class GamesService : IGamesService
    {
        private readonly ApplicationDbContext context;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly string imagesPath;

        public GamesService(ApplicationDbContext context , IWebHostEnvironment webHostEnvironment)
        {
            this.context = context;
            this.webHostEnvironment = webHostEnvironment;
            imagesPath = $"{webHostEnvironment.WebRootPath}{FileSettings.ImagesPath}";
        }

        public IEnumerable<Game> GetAll()
        {
            return context.Games
                .Include(c=>c.Category)
                .Include(d=>d.Devices)
                .ThenInclude(d=>d.Device)
                .AsNoTracking()
                .ToList();
        }

        public Game? GetGameById(int id)
        {
            return context.Games
                 .Include(c => c.Category)
                 .Include(d => d.Devices)
                 .ThenInclude(d => d.Device)
                 .AsNoTracking()
                 .SingleOrDefault(g => g.Id == id);
        }
        public async Task Create(CreateGameFormViewModel gameFormViewModel)
        {
            var coverName = await SaveCover(gameFormViewModel.Cover);
            //stream.Dispose();

            Game game = new()
            {
                Name = gameFormViewModel.Name,
                CategoryId = gameFormViewModel.CategoryId,
                Description = gameFormViewModel.Description,
                Cover = coverName,
                Devices = gameFormViewModel.SelectedDevices.Select(d => new GameDevice { DeviceId = d }).ToList()

            };

            context.Add(game);
            context.SaveChanges();


        }

        public async Task<Game?> Edit(EditGameFormViewModel gameFormViewModel)
        {
            var game = context.Games    //.Find(gameFormViewModel.Id);
                .Include(g=> g.Devices)
                 .SingleOrDefault(g => g.Id == gameFormViewModel.Id);
            if (game is null)
                return null;

            var hasNewCover=gameFormViewModel.Cover is not null;

            var oldCover = game.Cover;

            game.Name = gameFormViewModel.Name;
            game.Description = gameFormViewModel.Description;
            game.CategoryId=gameFormViewModel.CategoryId;
            game.Devices = gameFormViewModel.SelectedDevices.Select(d => new GameDevice { DeviceId = d }).ToList();

            if(hasNewCover)
            {
                game.Cover = await SaveCover(gameFormViewModel.Cover!);
            }

            var effectedRows = context.SaveChanges();

            if(effectedRows>0)
            {
                if(hasNewCover)
                {
                    var cover = Path.Combine(imagesPath, oldCover);
                    File.Delete(cover);
                } 
                return game;
            }
            else
            {
                var cover = Path.Combine(imagesPath, game.Cover);
                File.Delete(cover);
                return null;
            }
       
        }

        public bool Delete(int id)
        {
            var isDeleted = false;

            var game = context.Games.Find(id);
            if (game is null)
                return isDeleted;
            context.Remove(game);


            var effectedRows = context.SaveChanges();
            if(effectedRows>0)
            {
                isDeleted = true;
                var cover = Path.Combine(imagesPath, game.Cover);
                File.Delete(cover);
            }

            return isDeleted;
        }
        private async Task<string> SaveCover(IFormFile cover)
        {
            var coverName = $"{Guid.NewGuid()}{Path.GetExtension(cover.FileName)}";

            var path = Path.Combine(imagesPath, coverName);

            using var stream = File.Create(path);

            await cover.CopyToAsync(stream);

            return coverName;
        }

     
    }
}
 