using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace InterviewApp.Interfaces
{
    public interface IDependency
    {
        MemoryStream DrawImageFromNameToArray(string fileName);
    }
}
