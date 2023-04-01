namespace VSPWebApi.API.DTO
{
    public class carClassDTO
    {
        public string? Marka { get; set; }
        public string? Model { get; set; }
        public string? Plaka { get; set; }
        public string? KM { get; set; }
        public string? SatisFiyat { get; set; }
        public string? AlisFiyat { get; set; }
        public string? MotorHacmi { get; set; }
    }
    public class deleteClassDTO
    {
        public string? Plaka { get; set; }
    }
    public class CarClassGetCar
    {
        public string? Plaka { get; set; }
    }
}
