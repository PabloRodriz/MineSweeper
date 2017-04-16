using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication2
{
	class DRAW_INTERFACE
	{
		public DRAW_INTERFACE()
		{

		}
		public void clearConsole()
		{
			Console.Clear();
		}

		/// <summary>
		/// Setting colors in console
		/// </summary>
		/// <param name="ForeColor">Text</param>
		/// <param name="BackGround">Background</param>
		public void setColors(ConsoleColor ForeColor, ConsoleColor BackGround)
		{
			Console.ForegroundColor = ForeColor;
			Console.BackgroundColor = BackGround;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="ASCII code">znak</param>
		/// <param name="souradniceX">X axis</param>
		/// <param name="souradniceY">Y axis</param>
		public void printASCII(int cislo, int souradniceX, int souradniceY, int shiftX, int shiftY)
		{
			int x = souradniceX * 4 + shiftX; int y = souradniceY * 2 + shiftY;
			Console.SetCursorPosition(x, y);
			int cis = cislo;
			char znak = (char)(cis);
			Console.Write(znak.ToString());

		}

		internal void BOOM()
		{
			Console.BackgroundColor = ConsoleColor.Black;
			//clearConsole();
			Console.BackgroundColor = ConsoleColor.Red;
			for (int i = 0; i < 7; i++)
			{
				Console.SetCursorPosition(30, 9 + i);
				for (int j = 0; j < 23; j++)
				{
					Console.Write(" ");
				}
			}
			//Print of letters
			TiskniB(31, 10);
			TiskniO(36, 10);
			TiskniO(41, 10);
			TiskniM(47, 10);
		}

		private void TiskniM(int x, int y)
		{
			for (int i = 0; i < 5; i++)
			{
				Console.BackgroundColor = ConsoleColor.Yellow;
				Console.SetCursorPosition(x, y + i);
				Console.Write(" ");
				Console.SetCursorPosition(x + 4, y + i);
				Console.Write(" ");
				if (i == 1)
				{
					Console.SetCursorPosition(x + 1, y + i);
					Console.Write(" ");
					Console.SetCursorPosition(x + 3, y + i);
					Console.Write(" ");
				}
				if (i == 2)
				{
					Console.SetCursorPosition(x + 2, y + i);
					Console.Write(" ");
				}
			}
		}

		private void TiskniO(int x, int y)
		{
			for (int i = 0; i < 5; i++)
			{
				Console.BackgroundColor = ConsoleColor.Yellow;
				//Console.SetCursorPosition(x, y + i);
				//Console.Write(" ");
				if (i == 1 || i == 2 || i == 3)
				{
					Console.SetCursorPosition(x + 1, y + i);
					Console.Write(" ");
					Console.SetCursorPosition(x + 4, y + i);
					Console.Write(" ");
				}
				if (i == 0 || i == 4)
				{
					Console.SetCursorPosition(x + 2, y + i);
					Console.Write(" ");
					Console.SetCursorPosition(x + 3, y + i);
					Console.Write(" ");
				}
			}
		}

		private void TiskniB(int x, int y)
		{
			for (int i = 0; i < 5; i++)
			{
				Console.BackgroundColor = ConsoleColor.Yellow;
				Console.SetCursorPosition(x, y + i);
				Console.Write(" ");
				if (i == 0 || i == 2 || i == 4)
				{
					Console.SetCursorPosition(x + 1, y + i);
					Console.Write(" ");
					Console.SetCursorPosition(x + 2, y + i);
					Console.Write(" ");
					Console.SetCursorPosition(x + 3, y + i);
					Console.Write(" ");
				}
				if (i == 1 || i == 3)
				{
					Console.SetCursorPosition(x + 4, y + i);
					Console.Write(" ");
				}
			}
		}

		public void WaitingForEnd()
		{
			setColors(ConsoleColor.Red, ConsoleColor.Yellow);
			int x = 30; int y = 23;
			Console.SetCursorPosition(x, y);
			Console.Write("Press any button ...");
			string pS = Console.ReadKey().ToString();

		}
	}
}
