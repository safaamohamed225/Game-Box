
namespace GameBox.Services
{
    public class DevicesService : IDevicesService
    {
        private readonly ApplicationDbContext context;

        public DevicesService(ApplicationDbContext context)
        {
            this.context = context;
        }
        public IEnumerable<SelectListItem> GetListDevices()
        {
            return context.Devices
                .Select(d => new SelectListItem { Value = d.Id.ToString(), Text = d.Name })
                .OrderBy(d => d.Text)
                .AsNoTracking()
                .ToList();
        }
    }
}
