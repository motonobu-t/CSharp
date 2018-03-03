using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.IO;


namespace ProjectCheck
{
    class Program
    {
        public static List<string> m_FileList;

        static void Main(string[] args)
        {
            string[] files = System.Environment.GetCommandLineArgs();

            Program.m_FileList = new List<string>();

            Console.WriteLine(files[1]);
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.Load(files[1]);
            XmlNodeList nodelist;
            XmlElement xmlRoot = xmldoc.DocumentElement;

            nodelist = xmlRoot.GetElementsByTagName(@"ItemGroup");
            //nodelist = xmlRoot.GetElementsByTagName(@"ClInclude");

            Console.WriteLine(nodelist.Count);
            

            foreach(XmlElement nd in nodelist)
            {
                int TargetIndex = 0;    // 文字列検索位置
                string Filestr = null;  // ファイル名文字列

                //Console.WriteLine(nd.InnerText);
                //Console.WriteLine(nd.Attributes.GetNamedItem("Include").InnerText);
                XmlNodeList CIInclude = nd.GetElementsByTagName("ClInclude");
                foreach(XmlNode Include in CIInclude)
                {
                    Filestr = Include.Attributes.GetNamedItem("Include").InnerText;
                    // ファイル名のみ抽出(ディレクトリパスを除去)
                    if (0 < (TargetIndex = Filestr.LastIndexOf(@"\"))) {
                        string str = Filestr.Substring(TargetIndex + 1);
                        Filestr = str;
                        TargetIndex = 0;
                    }
                    Console.WriteLine(Filestr);
                    Program.m_FileList.Add(Filestr);
                }

                XmlNodeList ClCompile = nd.GetElementsByTagName("ClCompile");
                foreach (XmlNode Include in ClCompile)
                {
                    Filestr = Include.Attributes.GetNamedItem("Include").InnerText;
                    // ファイル名のみ抽出(ディレクトリパスを除去)
                    if (0 < (TargetIndex = Filestr.LastIndexOf(@"\")))
                    {
                        string str = Filestr.Substring(TargetIndex + 1);
                        Filestr = str;
                        TargetIndex = 0;
                    }
                    Console.WriteLine(Include.Attributes.GetNamedItem("Include").InnerText);
                    Program.m_FileList.Add(Include.Attributes.GetNamedItem("Include").InnerText);
                }
            }
            Console.ReadLine();
        }
    }
}
