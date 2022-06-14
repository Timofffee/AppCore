using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using UnityEngine;

namespace Krem.AppCore
{
    public class ClassFileDetails
    {
        public int OID { get; set; }
        public string className { get; set; }
        public string path { get; set; }
        public System.DateTime updateTime { get; set; }

        internal ClassFileDetails()
        {
        }

        internal ClassFileDetails(string setClassName, string setPath, System.DateTime setUpdateTime)
        {
            className = setClassName;
            path = setPath;
            updateTime = setUpdateTime;
        }
    }

    public static class ClassFileFinder
    {
        private const string ClassPattern = @"\bclass\b\s+\b{0}\b[\s:]";
        private const string NamespacePattern = @"\bnamespace\b\s+\b{0}\b[\s:]";
        
        static List<string> classFiles;

        public static ClassFileDetails FindClassFile(System.Type t)
        {
            return FindClassFile(t.Name, t.Namespace);
        }

        public static ClassFileDetails FindClassFile(string className, string classNamespace = null)
        {
            ClassFileDetails details = null;

            classFiles = new List<string>();
            FindAllScriptFiles(Application.dataPath);

            for (int i = 0; i < classFiles.Count; i++)
            {
                string codeFile = File.ReadAllText(classFiles[i]);
                String classMatchPattern = String.Format(ClassPattern, className);
                String namespaceMatchPattern = String.Format(NamespacePattern, classNamespace);
                
                if (Regex.Matches(codeFile, classMatchPattern, RegexOptions.IgnoreCase).Count == 1 && 
                    Regex.Matches(codeFile, namespaceMatchPattern, RegexOptions.IgnoreCase).Count == 1)
                {
                    details = new ClassFileDetails(className, classFiles[i],
                        File.GetLastAccessTimeUtc(classFiles[i]));
                }
            }

            if (details == null)
            {
                Debug.LogError("Failed to lookup class file for class " + className);
            }

            return details;
        }

        static void FindAllScriptFiles(string startDir)
        {
            try
            {
                foreach (string file in Directory.GetFiles(startDir))
                {
                    if (file.EndsWith(".cs"))
                        classFiles.Add(file.Replace("\\", "/"));
                }

                foreach (string dir in Directory.GetDirectories(startDir))
                {
                    FindAllScriptFiles(dir);
                }
            }
            catch (System.Exception ex)
            {
                Debug.LogException(ex);
            }
        }
    }
}