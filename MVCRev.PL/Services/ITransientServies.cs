using System;

namespace MVCRev.PL.Services
{
    public interface ITransientServies
    {
        public Guid Guid { get; set; }

        string GetGuid();
    }
}
