namespace CarMaintenanceTrackerServer.DTOs.Car.Response
{
    public class GetAllCarsResponseDto
    {
        public ICollection<GetCarResponseDto> Cars { get; set; } = [];
    }
}
