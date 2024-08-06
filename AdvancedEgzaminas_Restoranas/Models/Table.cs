namespace AdvancedEgzaminas_Restoranas.Models
{
    public class Table
    {
        public int Number { get; set; }
        public int Seats { get; set; }
        public bool IsOccupied { get; set; } = false;

        public Table(int number, int seats)
        {
            Number = number;
            Seats = seats;
        }
    }
}
