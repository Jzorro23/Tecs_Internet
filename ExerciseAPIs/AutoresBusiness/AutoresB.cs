using Models;
using AutoresData;

namespace AutoresBusiness
{
    public class AutoresB
    {
        public AutoresB()
        {
            
        }

        public List<Autores> GetAutoresFromFile()
        {
            return new AutoresD().GetAutoresFromFile();
        }

        public List<Autores> GetAutoresByName(string name)
        {
            return new AutoresD().GetAutoresByName(name);
        }
    }
}
