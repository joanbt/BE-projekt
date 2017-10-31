using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Net;
using HtmlAgilityPack;
using System.IO;
namespace Ikea_parser
{
	class Program
	{
		public static string getBetween(string strSource, string strStart, string strEnd)
		{
			int Start, End;
			if (strSource.Contains(strStart) && strSource.Contains(strEnd))
			{
				Start = strSource.IndexOf(strStart, 0) + strStart.Length;
				End = strSource.IndexOf(strEnd, Start);
				return strSource.Substring(Start, End - Start);
			}
			else
			{
				return "";
			}
		}

		public static void Parse(string PATHTOFILE,string FILENAME)
		{
			List<float> prices = new List<float>();
			List<string> names = new List<string>();
			List<string> imageLink = new List<string>();
			var doc = new HtmlDocument();
			doc.Load(PATHTOFILE + FILENAME);
			var htmlNodes = doc.DocumentNode.SelectNodes("//div[contains(@class,'price regularPrice')]");
			foreach (var node in htmlNodes)
			{
				var price = node.SelectSingleNode("//div[contains(@class,'price regularPrice')]");
				string curr = node.InnerText.ToString().Replace("&nbsp;", "").Replace("Cena za sztuke", "").Replace("PLN", "").Replace("	", "").Replace("\n", "").Replace("/2szt.", "");
				prices.Add(float.Parse(curr));
			}

			var htmlNodes1 = doc.DocumentNode.SelectNodes("//div[contains(@class,'productTitle')]");
			foreach (var node in htmlNodes1)
			{
				names.Add(node.InnerText.ToString());
			}

			var htmlImages = doc.DocumentNode.SelectNodes("//div[@class='image']");
			foreach (var node in htmlImages)
			{
				string val = node.InnerHtml.ToString();
				string data = getBetween(val, "./", "\"");
				imageLink.Add(data);
			}

			System.IO.Directory.CreateDirectory(PATHTOFILE + "IMAGES");
			
			for (int i = 0; i < imageLink.Count; i++)
			{
				using (StreamWriter sw = File.AppendText(PATHTOFILE+"parse.csv"))
				{
					sw.WriteLine(names[i].ToString() + "|" + prices[i].ToString() + "|" + getBetween(imageLink[i], "/", ".JPG"));
				}
				//Console.WriteLine(names[i].ToString() + "|" + prices[i].ToString() + "|" + getBetween(imageLink[i], "/", ".JPG"));
				if (System.IO.File.Exists(PATHTOFILE + imageLink[i]) && !System.IO.File.Exists(PATHTOFILE + "IMAGES//" + getBetween(imageLink[i], "/", ".JPG") + ".JPG"))
				{
					File.Copy(PATHTOFILE + imageLink[i], PATHTOFILE + "IMAGES//" + getBetween(imageLink[i], "/", ".JPG") + ".JPG");
				}
			}
		}

		static void Main(string[] args)
		{
			string PATH = "C:\\Users\\Kyrychok\\Desktop\\";//CHANGE IT

			File.Create(PATH+"parse.csv");

			string FILENAME1 = "Sofy - IKEA.html";
			Parse(PATH,FILENAME1);
			string FILENAME2 = "Stoliki kawowe - IKEA.html";
			Parse(PATH, FILENAME2);
			string FILENAME3 = "Stoliki RTV - Stoliki pod telewizor - IKEA.html";
			Parse(PATH, FILENAME3);



		}
	}
}
