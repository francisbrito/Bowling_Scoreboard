﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BowlingScoreboard.Core
{
    public interface IFileFormatValidator
    {
        bool IsValid(string input);
    }
}
