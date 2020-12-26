﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingHood
{
    class AhkRunner
    {
        public static AhkRunner Instance
        {
            get
            {
                if (ahkRunner == null) {
                    ahkRunner = new AhkRunner();
                }
                return ahkRunner;
            }
        }
        private static AhkRunner ahkRunner;

        public string AhkPath
        {
            get {
                return ConfigurationManager.AppSettings["AhkPath"];
            }
        }

        private AhkRunner() { }

        public void RunAhkScript(string scriptPath) {
            
            var ahkExePath = AhkPath;
            System.Diagnostics.Process.Start(
                ahkExePath, 
                scriptPath);

            CodeBow.Current.CloseWindow();
        }

    }
}