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
			GameField gf;



			DRAW_INTERFACE DI = new DRAW_INTERFACE();
			string klavesa = "0";
			string ts = DateTime.Now.ToString();


			Console.WriteLine("Introduce number of mines");
			int numberOfmines = Int32.Parse(Console.ReadLine());
			gf = new GameField(numberOfmines);

			Console.WriteLine(gf.getnumberOfMines());


			Timer tim2 = new Timer(1000);
			tim2.Elapsed += Timer;

			tim2.Start();
			do
			{
				
				DI.clearConsole();
				printTimer();
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
							//DI.printASCII(ASCIIcisla + gf.getNumber(j, i), j, i, 23, 2);
							if(gf.getNumber(j, i) == 0){
								DI.printASCII(ASCIIcisla + gf.getNumber(j, i), j, i, 23, 2);
								/*
								for (int hi = 0; hi < 2; hi++)
								{
									
									DI.printASCII(ASCIIcisla + gf.getNumber(j, i), j, i, 23, 2);
								}
								*/
							}else{
								DI.printASCII(ASCIIcisla + gf.getNumber(j, i), j, i, 23, 2);
							}

						}
						else
						{
							DI.printASCII(ASCIIpismena + 23, j, i, 23, 2);
						}
						//
						DI.setColors(ConsoleColor.White, ConsoleColor.DarkBlue);


					}
				}



				Console.SetCursorPosition(63, 1);
				Console.WriteLine("Pos left: {0}" ,gf.getNumberOfObservedPixels());
				Console.WriteLine("Press k to finish game");




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
						Console.SetCursorPosition(31,10);
						Console.WriteLine("YOU WIN!");
						break;
					}

					if(gf.getNumber(x, y) == 0){
						// implementation: calculate with stack
						int [,] fieldStack = new int[2, gf.getNumberOfObservedPixels()];
						int numberOfFieldsStack = 0;
						setUpStack(ref fieldStack, ref numberOfFieldsStack, x, y, gf);

						while (numberOfFieldsStack != 0)
						{
							x = fieldStack[0, numberOfFieldsStack - 1];
							y = fieldStack[1, numberOfFieldsStack - 1];
							putput = gf.tryFild(x, y);
							numberOfFieldsStack--;
							if(putput == 2){
								Console.SetCursorPosition(31, 10);
								Console.WriteLine("YOU WIN!");
								break;
							}
							setUpStack(ref fieldStack, ref numberOfFieldsStack, x, y, gf);
						}

					}
				}

				Console.BackgroundColor = ConsoleColor.Black;



			} while (hS.Key != ConsoleKey.K);
			tim2.Stop();
			Console.SetCursorPosition(63, 0);
			Console.BackgroundColor = ConsoleColor.Red;
			Console.WriteLine("Final time: {0}", cont);

			Timer tim = new Timer(1000);
			tim.Elapsed += Tim_Elapsed;
			tim.Start();
			//DI.WaitingForEnd();
			//DI.BOOM();
			DI.WaitingForEnd();
		}
		private static void setUpStack(ref int[,] fieldStack, ref int numberOfFieldsStack,int x, int y, GameField gf ){
			if (gf.getNumber(x, y) == 0)
			{

				for (int i = -1; i < 2; i++)
				{
					for (int j = -1; j < 2; j++)
					{
						if (j + x < 0 || i + y < 0 || i + y > 9 || j + x > 9)
						{

						}
						else
						{
							if (gf.getObserved(x + j, y + i) == false)
							{
								if (gf.getIsInStack(x + j, y + i) == false)
								{

									fieldStack[0, numberOfFieldsStack] = x + j;
									fieldStack[1, numberOfFieldsStack] = y + i;
									numberOfFieldsStack++;
									gf.setIsInStack(x + j, y + i);
								}

							}


						}

					}

				}
			}

		}

		private static void Tim_Elapsed(object sender, ElapsedEventArgs e)
		{
			Console.Beep();

		}

		private static void Timer(object sender, ElapsedEventArgs e)
		{
			cont++;
			Console.SetCursorPosition(63, 0);
			Console.WriteLine("{0} seconds",cont);
			/*
			Console.SetCursorPosition(63, 2);
			Console.WriteLine("Casillas");
			*/

		}

		private static void printTimer(){
			Console.SetCursorPosition(63, 0);
			Console.BackgroundColor = ConsoleColor.Red;
			Console.ForegroundColor = ConsoleColor.White;
			Console.WriteLine("{0} seconds", cont);
		}
	}
}
