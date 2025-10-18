using Models;

namespace AutoresData
{
    public class AutoresD
    {

        public AutoresD() 
        {

        }

        public List<Autores> GetAutoresFromFile()
        {
            //string[] lines = File.ReadAllLines(@"C:\Users\Jzorro\Desktop\Tecs_Inter\ExerciseAPIs\AutoresAPI\Autores.txt");
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "txt", "Autores.txt");
            string[] lines = File.ReadAllLines(path);
            int a = 0;
            var ListAutores = new List<Autores>();
            foreach (string line in lines)
            {
                a++;
                ListAutores.Add(new Autores()
                {
                    ID = a,
                    Name = line
                });
            }
            return ListAutores;
        }

        public List<Autores> GetAutoresByName(string name)
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "txt", "Autores.txt");
            string[] lines = File.ReadAllLines(path);
            int a = 0;
            var MatchAutores = new List<Autores>();

            foreach (string line in lines)
            {
                a++;
                if (line == name)
                {
                    MatchAutores.Add(new Autores()
                    {
                        ID = a,
                        Name = line
                    });
                }
            }

            if (MatchAutores.Count == 0)
            {
                return new List<Autores>
                {
                    new Autores()
                    {
                        ID = a,
                        Name = "El nombre del autor no se encuentra en la lista"
                    }
                };
            }

            return MatchAutores;
        }
    }
}
