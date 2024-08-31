using System;

namespace MVCRev.PL.Services
{
    public class ScopedServies : IScopedServies
    {
        public Guid Guid { get; set ; }


        public ScopedServies()
        {
            Guid = Guid.NewGuid();
        }




        public string GetGuid()
        {
            return Guid.ToString();
        }

       
        public override string ToString()
        {
            return Guid.ToString();
        }
    }
}
