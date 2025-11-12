using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EQ.Domain.Interface
{
    public interface IIoController
    {
        bool Init(string configPath);
        bool ReadInput(int address);
        void WriteOutput(int address, bool value);
        void Close();
    }
}
