namespace GameEngine.Framework.Interface
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.IO;

    public interface FileIO
    {
        Stream readFile(string fileName);
        Stream writeFile(string fileName);
    }
}
