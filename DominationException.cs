using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Domination_Game
{
    internal class DominationException : Exception
    {
        public DominationException(string message):base(message) 
        { 
        }
    }
}
