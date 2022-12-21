using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Text.RegularExpressions;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace prog12form
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			
			try
			{
				ResultBox.Text = "Результат работы программы: " + Environment.NewLine;
				string text = textBox1.Text;
				string rex = textBox2.Text;
				RegularEx Reg = new RegularEx(rex, text);
				Regex reg = new Regex(rex);
				Reg.Text = text;
				Reg.R = reg;

				// INDEXATOR
				int n = int.Parse(numericUpDown1.Text);
				ResultBox.Text += $"{Reg[n]}" + Environment.NewLine;
				if (n != 0 && n != 1)
				{
					return;
				}
				ResultBox.Text += " " + Environment.NewLine;

				//BIN -
				ResultBox.Text += "Удаление из строки все фрагменты, соответствующие шаблону поля регулярного выражения" + Environment.NewLine;
				Regex regex = new Regex(rex);
				ResultBox.Text += $"{Reg - regex}" + Environment.NewLine;
				ResultBox.Text += " " + Environment.NewLine;

				// TRUE OR FALSE
				ResultBox.Text += "\nЕсли поле Text пустое, то false, иначе true: " + Environment.NewLine;
				if (Reg)
				{
					ResultBox.Text += "True" + Environment.NewLine;
				}
				else ResultBox.Text += "False" + Environment.NewLine;
				ResultBox.Text += "" + Environment.NewLine;

				// BIN +
				ResultBox.Text += "Введите строку которую хотите добавить к своей строке: " + Environment.NewLine;
				string str = textBox3.Text;
				string binp = Reg + str;
				ResultBox.Text += binp + Environment.NewLine;
				ResultBox.Text += "" + Environment.NewLine;

				//Преобразование Regex в строку и наоборот
				ResultBox.Text += "Преобразование Regex в тип String и наоборот" + Environment.NewLine;
				string newOb = (RegularEx)Reg;
				ResultBox.Text += newOb + Environment.NewLine;
				Reg = (string)newOb;
				ResultBox.Text += Reg.R;
			}
			catch
			{
				MessageBox.Show("Ошибка");
				return;
			}
		}
	}
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
					MessageBox.Show("Ошибка, можно обратиться только к шаблону и тексту(0 и 1)");
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
			if (text != string.Empty)
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
}
