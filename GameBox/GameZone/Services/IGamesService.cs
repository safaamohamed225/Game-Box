namespace GameBox.Services
{
    public interface IGamesService
    {

         IEnumerable<Game> GetAll();
        Game? GetGameById(int id); 
        Task Create(CreateGameFormViewModel gameFormViewModel);
        Task <Game?> Edit(EditGameFormViewModel gameFormViewModel);
        bool Delete(int id);

    }
}
