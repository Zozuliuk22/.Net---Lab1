namespace BuildingsCollection
{
    public class Block
    {
        public string Code { get; set; }

        public string Name { get; set; }

        public string AdministrationAddress { get; set; }

        public int InhabitantsNumber { get; set; }

        public double Area { get; set; }

        public override string ToString()
        {
            return $"{Code} - {Name} - {AdministrationAddress}";
        }
    }
}
