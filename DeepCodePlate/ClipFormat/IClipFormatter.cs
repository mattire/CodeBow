using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingHood.ClipFormat
{
    interface IClipFormatter
    {
        string Name { get; }
        //string FormatClip();
        string Format(string str);
    }
}
