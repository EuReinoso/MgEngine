using MgEngine.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MgEngine.Interface
{
    public interface IUpdate
    {
        void Update(Inputter inputter, float dt);
    }
}
