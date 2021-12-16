using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WBPlugin
{
    public interface IUserInputRetriever<T>
    {
        T getUserInput(String prompt);
    }
}
