using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuildOnlyDependency.Consumer
{
    public class Foo
    {
        public Foo()
        {
            Utils.DoMagic();
            SuperUtils.DoSuperMagic();
        }
    }
}
