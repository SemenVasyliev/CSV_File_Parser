using Microsoft.VisualBasic.FileIO;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.Generic;
using System.Linq;

namespace TestTask.InterLink
{
    class Program
    {
        static void Main(string[] args)
        {
            string tempDate;
            string tempHour;
            List<string> hours = new List<string>();
            using StreamWriter sw = new StreamWriter(@"C:\Users\semen\Desktop\text2.csv", true);

            List<string> names = new List<string>();
            names.Add(" ");

            List<string> resultDate = new List<string>();
            List<string> resultDate2 = new List<string>();

            List<List<string>> list = new List<List<string>>();

            string[] s = File.ReadAllLines(@"C:\Users\semen\Desktop\acme_worksheet.csv");
            for (int i = 1; i < s.Length; i++)
            {
                string[] words = s[i].Split(new char[] { ',' });
                names.Add(words[0]);
                resultDate.Add(words[1] + ",");
                resultDate2.Add(DateTime.Parse(words[1]).ToString());                                             
            }
            List<string> uniqueDate = resultDate.Distinct().ToList();
            List<string> uniqueDate2 = resultDate2.Distinct().ToList();
            List<string> uniqueName = names.Distinct().ToList();
            sw.Write("Name / Date,");
            for (int i = 0; i < uniqueDate2.Count; i++)
            {
                sw.Write(uniqueDate2[i].Remove(10, 8) + ",");
            }

            for (int i = 1; i < uniqueName.Count; i++)
            {  
                for (int j = 0; j < uniqueDate.Count; j++)
                { 
                    tempDate = uniqueDate[j];
                    for (int k = 1; k < s.Length; k++)
                    {
                        string[] words = s[k].Split(new char[] { ',' });
                        if (tempDate.Remove(tempDate.Length - 1) == words[1] && uniqueName[i] == words[0])
                        {
                            tempHour = words[2] + ",";
                            hours.Add(tempHour);
                        }
                    }
                    if (hours.Count < j+1)
                    {
                        hours.Add("0,");
                    }
                }
                hours.Insert(0, Environment.NewLine + uniqueName[i] + ",");
                hours.ForEach(sw.Write);
                hours.Clear();
            }         
        }
    }
}
