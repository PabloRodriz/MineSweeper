using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;



namespace ConsoleApplication2
{
	class Program
	{
		GameField gF;
		static int cont;
		static void Main(string[] args)
		{
			int x = 0, y = 0;
			int ASCIIcisla = 48;
			int ASCIIpismena = 65;
			ConsoleKeyInfo hS;
			GameField gf = new GameField();

			DRAW_INTERFACE DI = new DRAW_INTERFACE();
			string klavesa = "0";
			string ts = DateTime.Now.ToString();


			Timer tim2 = new Timer(1000);
			tim2.Elapsed += Timer;
			tim2.Start();

			do
			{
				DI.clearConsole();
				DI.setColors(ConsoleColor.White, ConsoleColor.DarkBlue);
				for (int i = 0; i < 10; i++)
				{
					for (int j = 0; j < 10; j++)
					{
						if (j == 0)
						{
							DI.printASCII(ASCIIpismena + i, j, i, 20, 2);
						}
						if (i == 0)
						{
							DI.printASCII(ASCIIcisla + j, j, i, 23, 0);
						}
						DI.setColors(ConsoleColor.White, ConsoleColor.Blue);

						if (gf.getObserved(j, i))
						{
							DI.printASCII(ASCIIcisla + gf.getNumber(j, i), j, i, 23, 2);
						}
						else
						{
							DI.printASCII(ASCIIpismena + 23, j, i, 23, 2);
						}
						//
						DI.setColors(ConsoleColor.White, ConsoleColor.DarkBlue);


					}
				}




				//user interface
				Console.SetCursorPosition(23 + 4 * x, 2 + 2 * y);
				Console.BackgroundColor = ConsoleColor.Red;
				if (gf.getObserved(x, y))
				{
					DI.printASCII(ASCIIcisla + gf.getNumber(x, y), x, y, 23, 2);
				}
				else
				{
					Console.Write((char)(Int32.Parse((ASCIIpismena + 23).ToString())));
				}
				Console.SetCursorPosition(23 + 4 * x, 2 + 2 * y);

				hS = Console.ReadKey();

				//y positions
				if (hS.Key == ConsoleKey.DownArrow)
				{
					y++;
					if (gf.getSizeOfField() - 1 < y)
					{
						y = 0;
					}
				}
				if (hS.Key == ConsoleKey.UpArrow)
				{
					y--;
					if (y < 0)
					{
						y = gf.getSizeOfField() - 1;
					}
				}

				//x positions
				if (hS.Key == ConsoleKey.RightArrow)
				{
					x++;
					if (gf.getSizeOfField() - 1 < x)
					{
						x = 0;
					}
				}
				if (hS.Key == ConsoleKey.LeftArrow)
				{
					x--;
					if (x < 0)
					{
						x = gf.getSizeOfField() - 1;
					}
				}

				//enter for observation
				if (hS.Key == ConsoleKey.Enter)
				{
					int putput = gf.tryFild(x, y);
					if (putput == 1)
					{
						DI.BOOM();
						break;
					}
					if (putput == 2)
					{
						Console.WriteLine("YOU WIN!");
						break;
					}
				}

				Console.BackgroundColor = ConsoleColor.Black;



			} while (hS.Key != ConsoleKey.K);

			Timer tim = new Timer(1000);
			tim.Elapsed += Tim_Elapsed;
			tim.Start();
			//DI.WaitingForEnd();
			//DI.BOOM();
			DI.WaitingForEnd();
		}

		private static void Tim_Elapsed(object sender, ElapsedEventArgs e)
		{
			Console.Beep();

		}

		private static void Timer(object sender, ElapsedEventArgs e)
		{
			cont++;
			Console.SetCursorPosition(65, 0);
			Console.WriteLine("{0} seconds",cont);

		}
	}
}
