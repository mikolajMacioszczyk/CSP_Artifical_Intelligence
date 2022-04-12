namespace Lista2.Model
{
    public class Field
    {
        public int Row { get; set; }
        public int Column { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is Field field &&
                   Row == field.Row &&
                   Column == field.Column;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Row, Column);
        }
    }
}
