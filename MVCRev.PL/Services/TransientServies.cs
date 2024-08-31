using System;

namespace MVCRev.PL.Services
{
    public class TransientServies : ITransientServies
    {
        public Guid Guid { get; set; }

        public TransientServies()
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
