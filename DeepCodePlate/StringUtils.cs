﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingHood
{
    public static class StringUtils
    {
        public static List<int> AllIndexesOf(this string str, string value)
        {
            if (String.IsNullOrEmpty(value))
                throw new ArgumentException("the string to find may not be empty", "value");
            List<int> indexes = new List<int>();
            for (int index = 0; ; index += value.Length)
            {
                index = str.IndexOf(value, index);
                if (index == -1)
                    return indexes;
                indexes.Add(index);
            }
        }

        public static List<int> AllIndexesOf(this string str, char[] values)
        {
            //if (String.IsNullOrEmpty(value))
            //    throw new ArgumentException("the string to find may not be empty", "value");
            List<int> indexes = new List<int>();
            for (int index = 0; ; index += 1)
            {
                index = str.IndexOfAny(values, index);
                if (index == -1)
                    return indexes;
                indexes.Add(index);
            }
        }

        public static string SubWithStartEndPoints(this string str, int start, int end)
        {
            var lst = new List<string>();
            
            return str.Substring(start, end - start);

        }

        
    }
}
