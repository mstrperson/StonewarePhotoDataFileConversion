using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Csv;

namespace StonewarePhotoDataFileConversion
{
    class Program
    {
        static void Main(string[] args)
        {
            CSV datafile = new CSV(new FileStream("C:\\Users\\jcox\\Documents\\Student_Photos\\datafile.csv", FileMode.Open));
            if (File.Exists("C:\\Users\\jcox\\Documents\\Student_Photos\\datafile.json")) File.Delete("C:\\Users\\jcox\\Documents\\Student_Photos\\datafile.json");
            StreamWriter writer = new StreamWriter(new FileStream("C:\\Users\\jcox\\Documents\\Student_Photos\\datafile.json", FileMode.CreateNew));
            writer.WriteLine("var studentData = [");
            int rowCount = 0;
            foreach (Row row in datafile)
            {
                writer.WriteLine("\t{");
                for (int i = 0; i < row.Keys.Count; i++)
                {
                    string header = row.Keys.ToList()[i];
                    int j;
                    string value = row[header];
                    if (!int.TryParse(value, out j))
                    {
                        value = string.Format("\"{0}\"", value);
                    }
                    writer.WriteLine("\t\t{0} : {1}{2}", header, value, i + 1 < row.Keys.Count ? "," : "");
                }

                writer.Write("\t}");
                writer.WriteLine("{0}", ++rowCount < datafile.RowCount ? "," : "");
            }

            writer.WriteLine("];");
            writer.Flush();
            writer.Close();
        }
    }
}
