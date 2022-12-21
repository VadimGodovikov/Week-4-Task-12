using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prog10._1
{
	class RegularEx
	{

		public RegularEx(string pattern, string txt)
		{
			r = new Regex(pattern.ToLower());
			text = txt;
		}

		// поля

		private Regex r;
		private string text;

		public bool Exist() // содержит ли текст фрагменты, соответствующие шаблону поля;
		{
			if (r.IsMatch(text.ToLower()))
			{
				Console.WriteLine("Содержит");
				return r.IsMatch(text);
			}
			else
			{
				Console.WriteLine("Не содержит");
				return false;
			}
		}

		public void ShowMatches() // •	вывести на экран все фрагменты текста, соответствующие шаблону поля;
		{
			MatchCollection m = r.Matches(text.ToLower());
			foreach (Match x in m)
			{
				Console.Write(x.Value + " ");
			}

		}

		public string DeleteMatches() // •	удалить из текста все фрагменты, соответствующие шаблону поля;
		{
			MatchCollection m = r.Matches(text.ToLower());
			string s = text.ToLower();
			foreach (Match x in m)
			{
				int i = s.IndexOf(x.Value);
				int l = x.Value.Length;
				s = s.Remove(i, l);
				text = text.Remove(i, l);

			}
			Console.WriteLine(text);
			return text;
		}

		public Regex R // •	св-во позволяющее установить или получить регулярное выражение, хранящееся в соответствующем поле класса (доступно для чтения и записи)
		{
			get { return r; }
			set { r = value; }
		}

		public string Text // •	св-во позволяющее установить или получить строковое поле класса (доступно для чтения и записи)
		{
			get { return text; }
			set { text = value; }
		}

		public object this[int n] // индексатор
		{
			get
			{
				if (n == 0)
				{
					return R;
				}
				else if (n == 1)
				{
					return Text;
				}
				else
				{
					Console.WriteLine("Ошибка, можно обратиться только к шаблону и тексту(0 и 1)");
					return null;
				}
			}
		}

		public static string operator -(RegularEx ex, Regex r) // bin -
		{
			MatchCollection m = r.Matches(ex.text.ToLower());
			string s = ex.text.ToLower();
			foreach (Match x in m)
			{
				int i = s.IndexOf(x.Value);
				int l = x.Value.Length;
				s = s.Remove(i, l);
				ex.text = ex.text.Remove(i, l);

			}
			return ex.text;
		}

		private bool CheckPer() // TRUE OR FALSE
		{
			if(text != string.Empty)
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		public static bool operator true(RegularEx ex) // TRUE
		{
			return ex.CheckPer();
		}
		public static bool operator false(RegularEx ex) // FALSE
		{
			return !ex.CheckPer();
		}

		public static string operator +(RegularEx ex, string str) // BIN +
		{
			return ex.Text + " " + str;
		}

		public static implicit operator string(RegularEx ex) // Regex в String
		{
			return ex.R.ToString();
		}
		public static implicit operator RegularEx(string Text) // String в Regex
		{
			RegularEx reg1 = new RegularEx(Text, "");
			return reg1;
		}

	}

	class Program
	{
		public static int readInt()
		{
			do
			{
				int res;
				bool check = int.TryParse(Console.ReadLine(), out res);
				if (check) return res;
				else Console.WriteLine("Введите корректное число");
			} while (true);
		}
		static void Main(string[] args)
		{
			while (true)
			{
				try
				{
					Console.WriteLine("Введите любой текст как с символами/словами, так и с цифрами: ");
					string text = Console.ReadLine();
					Console.Write("Введите шаблон поля регулярного выражения: ");
					string rex = Console.ReadLine();
					Console.WriteLine();
					RegularEx Reg = new RegularEx(rex, text);
					Regex vir = new Regex(rex);
					Reg.R = vir;
					Reg.Text = text;

					// INDEXATOR
					n1: Console.WriteLine("К чем хотите обратиться: \n0 - К шаблону регулярного выражения;\n1 - К тексту");
					int n = readInt();
					Console.WriteLine(Reg[n]);
					if (n != 0 && n != 1)
					{
						goto n1;
					}
					Console.WriteLine();

					//BIN -
					Console.WriteLine("Удаление из строки все фрагменты, соответствующие шаблону поля регулярного выражения");
					Regex regex = new Regex(rex);
					Console.WriteLine(Reg - regex);
					Console.WriteLine();

					// TRUE OR FALSE
					Console.WriteLine("\nЕсли поле Text пустое, то false, иначе true: ");
					if (Reg)
					{
						Console.WriteLine("True");
					}
					else Console.WriteLine("False");
					Console.WriteLine();

					// BIN +
					Console.WriteLine("Введите строку которую хотите добавить к своей строке: ");
					string str = Console.ReadLine();
					string binp = Reg + str;
					Console.WriteLine(binp);
					Console.WriteLine();

					//Преобразование Regex в строку и наоборот
					Console.WriteLine("Преобразование Regex в тип String и наоборот");
					string newOb = (RegularEx)Reg;
					Console.WriteLine(newOb);
					Reg = (string)newOb;
					Console.WriteLine(Reg.R);
					break;
				}
				catch
				{
					Console.WriteLine("Ошибка");
					break;
				}
			}
			Console.WriteLine();
		}
	}
}
