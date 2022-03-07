using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

namespace Management
{
    public static class File_Manager
    {
        public static void File_Out(string _File_Name, List<string> _FileInfo)
        {
            using (StreamWriter File = new StreamWriter(@"V:\Work\Projects\Project CS\Save\" + _File_Name))
            {
                for (int i = 0; i < _FileInfo.Count; i++)
                {
                    File.WriteLine(_FileInfo[i]);
                }
                Debug.Log("Writing!");
            }
        }

        public static string Add_to_Line(string _Line, string _Text)
        {
            if (_Line == "")
            {
                return _Text;
            }
            return _Line + ", " + _Text;
        }

        public static string Add_to_Line(string _Line, float _Text)
        {
            if (_Line == "")
            {
                return _Text + "";
            }
            return _Line + ", " + _Text;
        }
    }

}

