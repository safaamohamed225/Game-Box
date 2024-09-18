namespace GameBox.Services
{
    public interface IDevicesService
    {

        IEnumerable<SelectListItem> GetListDevices();
    }
}
