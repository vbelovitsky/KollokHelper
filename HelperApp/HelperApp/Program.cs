using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelperApp
{
	class Program
	{

		static string fileName = "../../../Opredelenia.txt";
		static string separator = "+++++";
		static Random rnd = new Random();

		static List<List<string>> ReadFile(string fn)
		{
			List<List<string>> definitions = new List<List<string>>();
			if (File.Exists(fn))
			{
				try
				{
					using(FileStream fs = new FileStream(fn, FileMode.Open))
					{
						StreamReader reader = new StreamReader(fs, Encoding.UTF8);
						int index = -1;
						while (!reader.EndOfStream)
						{
							string line = reader.ReadLine();
							if (line.StartsWith(separator))
							{
								definitions.Add(new List<string>());
								index++;
							}
							else
							{
								List<string> def = line.Split(' ').ToList();
								definitions[index].Add($"{index + 1}." +$"{def[0]}" + $"{(def[0].Length == 2?"  ":" ")}" + string.Join(" ", def.GetRange(1, def.Count - 1)));
							}
						}
					}
					
				}
				catch(IOException e)
				{
					definitions = null;
					Console.WriteLine(e.Message);
				}
				catch(Exception e)
				{
					definitions = null;
					Console.WriteLine(e.Message);
				}
			}
			return definitions;
		}

		static void PrintDefinitions(List<List<string>> definitions)
		{
			for(int i = 0; i < 5; i++)
			{
				int module = rnd.Next(0, definitions.Count);
				int index = rnd.Next(0, definitions[module].Count);
				Console.WriteLine(definitions[module][index]);
			}
		}

		static void Main(string[] args)
		{
			do
			{
				Console.Clear();
				List<List<string>> definitions = ReadFile(fileName);
				try
				{
					if (definitions != null)
					{
						do
						{
							Console.Clear();
							PrintDefinitions(definitions);

							Console.WriteLine();
							Console.WriteLine("Esc");
						}
						while (Console.ReadKey(true).Key != ConsoleKey.Escape);
					}
				}
				catch (Exception)
				{
					Console.WriteLine("Ой");
				}
				Console.WriteLine("Для выхода нажмите Esc: ");
			}
			while (Console.ReadKey(true).Key != ConsoleKey.Escape);
		}
	}
}
