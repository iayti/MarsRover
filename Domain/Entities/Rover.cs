namespace Domain.Entities
{
    public class Rover
    {
        public string Direction { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public string LocationDirection { get; set; }

        public Rover(int x, int y, string direction)
        {
            X = x;
            Y = y;
            Direction = direction;
            LocationDirection = x + " " + y + " " + direction;
        }
    }
}