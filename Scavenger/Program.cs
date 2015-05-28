using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Scavenger
{
    class Program
    {
        static void Main(string[] args)
        {
            string sourcesFilename = args[0];
            string targetsFilename = args[1];
            string wildcards = args[2];

            List<string> sources = File.ReadLines(sourcesFilename).ToList();
            List<string> targets = File.ReadLines(targetsFilename).ToList();
            
            List<Tuple<string, DateTime, string>> items = new List<Tuple<string, DateTime, string>>();

            foreach (string source in sources)
            {
                List<string> allEntries = new List<string>();

                if (!Directory.Exists(source))
                {
                    throw new ApplicationException(String.Format("Source folder '{0}' does not exist", source));
                }

                foreach (string wildcard in wildcards.Split(new char[] {' '}, StringSplitOptions.RemoveEmptyEntries))
                {
                    string[] entries = Directory.GetFileSystemEntries(source, wildcard, SearchOption.AllDirectories);
                    allEntries.AddRange(entries);
                }              

                foreach (string entry in allEntries)
                {
                    FileInfo info = new FileInfo(entry);
                    items.Add(new Tuple<string, DateTime, string>(info.Name, info.LastWriteTimeUtc, entry));
                }
            }

            List<Tuple<string, DateTime, string>> filtered = new List<Tuple<string, DateTime, string>>();

            foreach (Tuple<string, DateTime, string> item in items)
            {
                Tuple<string, DateTime, string> filteredItem = filtered.SingleOrDefault(f => f.Item1 == item.Item1);
                if (filteredItem == null)
                {
                    filtered.Add(item);
                }
                else if (filteredItem.Item2.Ticks < item.Item2.Ticks)
                {
                    filtered.Remove(filteredItem);
                    filtered.Add(item);
                }
            }

            foreach (string target in targets)
            {
                foreach (Tuple<string, DateTime, string> item in filtered)
                {
                    Console.WriteLine("Copying {0} to {1}", item.Item1, target);
                    string targetPathname = Path.Combine(target, item.Item1);
                    File.Copy(item.Item3, targetPathname, true);
                }
            }
        }
    }
}