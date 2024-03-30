namespace GameEngine.Framework.Implementation
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.IO;

    public class FileIO : Interface.FileIO
    {
        Stream Interface.FileIO.readFile(string fileName)
        {
            if (fileName.StartsWith(@"\"))
            {
                fileName = fileName.Substring(1, fileName.Length - 1);
            }
            return System.IO.File.OpenRead(fileName);
        }

        Stream Interface.FileIO.writeFile(string fileName)
        {
            throw new NotImplementedException();
        }
    }
}
