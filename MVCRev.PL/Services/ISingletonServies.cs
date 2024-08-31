using System;

namespace MVCRev.PL.Services
{
    public interface ISingletonServies
    {
        public Guid Guid { get; set; }

        string GetGuid();
    }
}
