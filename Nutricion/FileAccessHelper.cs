﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nutricion
{
    public class FileAccessHelper
    {
        public static string GetLocationFile(String FileName) {
            return System.IO.Path.Combine(FileSystem.AppDataDirectory,FileName);
        }
    }
}
