using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities
{
    public class IRFileLogger
    {
        /// <summary>
        /// Writes an entry to the log using default parameters.
        /// </summary>
        /// <param name="entry">What to write to the log file.</param>
        public static void WriteEntry(string entry)
        {
            try
            {
                WriteEntry("DataLoader", entry);
            }
            catch { }
        }

        /// <summary>
        /// Writes an entry to the log file.
        /// </summary>
        /// <param name="filename">The full path and file name of the log file.</param>
        /// <param name="entry">What to write to the log file.</param> 
        public static void WriteEntry(string filenamePrefix, string entry)
        {
            string filename = "";
            try
            {
                filename = System.IO.Path.Combine(GetLogPath(), string.Format("DataLoader_{0}.Log", DateTime.Today.ToString("MMddyyyy")));
                entry += filenamePrefix;
                entry += Environment.NewLine;
                entry += "------------------------------------------------------------------------";
                entry += Environment.NewLine;
                try
                {
                    string sPath = System.IO.Path.GetDirectoryName(filename);
                    if (!Directory.Exists(sPath))
                    {
                        Directory.CreateDirectory(sPath);
                    }
                    try
                    {
                        entry = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt") + " : " + entry;
                        using (FileStream fs = new FileStream(filename, FileMode.Append, FileAccess.Write))
                        {
                            try
                            {
                                fs.Write(System.Text.Encoding.ASCII.GetBytes(entry), 0, entry.Length);
                                fs.Flush();
                            }
                            catch { }
                            finally
                            {
                                if (null != fs)
                                    try
                                    {
                                        fs.Close();
                                    }
                                    catch { }
                            }
                        }
                    }
                    catch { }
                }
                catch { }
            }
            catch { }
            finally { }
        }

        public static string GetLogPath()
        {
            return GetLogPath("bin", "Logs");
        }

        /// <summary>
        /// Constructs a path to the log file, excluding the log filename.
        /// </summary>
        /// <param name="siblingFolder">The name of a sibling folder to logFolder (typically the assembly's \bin directory).</param>
        /// <param name="logFolder">The name of the log folder. This folder will be a sibling of what is supplied in siblingFolder.</param>
        /// <returns></returns>
        public static string GetLogPath(string siblingFolder, string logFolder)
        {
            string thePath = "";

            try
            {
                System.Reflection.Assembly ass = System.Reflection.Assembly.GetExecutingAssembly();
                thePath = System.IO.Path.GetDirectoryName(ass.CodeBase);
                if (thePath.LastIndexOf(siblingFolder) > -1)
                    thePath = thePath.Substring(0, thePath.IndexOf(siblingFolder));
                if (thePath.IndexOf(@"file:\") == 0)
                    thePath = thePath.Substring((@"file:\").Length);
                thePath = thePath.Replace(@"file:///", "");
                thePath = System.IO.Path.Combine(thePath, logFolder);
            }
            catch { }
            finally { }

            return thePath;
        }
    }
}
