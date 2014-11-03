using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace INSAttack
{
    public interface Factory<T>
    {
        T make();
    }
}
