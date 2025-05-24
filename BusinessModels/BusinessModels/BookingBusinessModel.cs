namespace BusinessModels
{
    public class BookingBusinessModel
    {
        public int Id { get; set; }
        public int RoomId { get; set; }
        public string VisitorName { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public List<int> ActivityIds { get; set; } = new();
    }
}
