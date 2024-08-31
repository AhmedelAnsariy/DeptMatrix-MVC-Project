using System;

namespace MVCRev.PL.Services
{
    public class SingeltonServices : ISingletonServies
    {
        public Guid Guid { get; set; }




        public SingeltonServices()
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
