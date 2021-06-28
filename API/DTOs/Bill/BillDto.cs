namespace API.DTOs
{
    public class BillDto
    {
        public int Id { get; set; }
        public float Value { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public BillTypeDto BillType { get; set; }
        public string[] UserName { get; set; }
    }
}