namespace AutorModels
{
    public class Autor
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string FullName
        {
            get { return FirstName + " " + LastName; }
        }

        public int Edad
        {
            get { return new DateTime(DateTime.Today.Subtract(BirthDate).Ticks).Year - 1; }
        }

        public string Direccion { get; set; }
    }
}
