using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagement.InfraStructure.Data
{
    public interface IDbInitializer
    {
        void Initialize();
    }
}
