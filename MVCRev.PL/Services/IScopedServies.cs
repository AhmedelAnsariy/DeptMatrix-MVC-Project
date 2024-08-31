using System;

namespace MVCRev.PL.Services
{
    public interface IScopedServies
    {
        public Guid Guid { get; set; }

        string GetGuid();
    }
}
