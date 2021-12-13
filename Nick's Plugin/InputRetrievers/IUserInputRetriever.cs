using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nick_s_Plugin
{
    public interface IUserInputRetriever<T>
    {
        T getUserInput(String prompt, T defaultValue);
    }
}
