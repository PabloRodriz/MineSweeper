using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication2
{
	class GameField
	{
		private bool[,] mines, observed;
		private int[,] minesAround, minesHelp;
		private int numberOfMines, sizeOfField = 10, numberOfFreePixels = 90, numberOfObservedPixels;
		private int[] positionsOfMines = new int[10];

		public GameField(int numberOfMines)
		{
			this.numberOfMines = numberOfMines;
			numberOfObservedPixels = 0;
			mines = new bool[sizeOfField, sizeOfField];
			observed = new bool[sizeOfField, sizeOfField];
			minesAround = new int[sizeOfField, sizeOfField];
			minesHelp = new int[sizeOfField + 2, sizeOfField + 2];
			GeneratedMines(numberOfMines, sizeOfField * sizeOfField);
			PlacementOfMines();
			MinesAroundDef();
		}

		public int getnumberOfMines(){
			
			return numberOfMines;
		}

		public int getNumber(int v1, int v2)
		{
			return minesAround[v1, v2];
		}
		public int getSizeOfField()
		{
			return sizeOfField;
		}
		public bool getObserved(int v1, int v2)
		{
			return observed[v1, v2];
		}

		private void MinesAroundDef()
		{
			for (int i = 0; i < sizeOfField; i++)
			{
				for (int j = 0; j < sizeOfField; j++)
				{
					minesAround[i, j] = minesHelp[i, j] + minesHelp[i + 1, j] + minesHelp[i + 2, j] + minesHelp[i, j + 1] + minesHelp[i + 1, j + 1] + minesHelp[i + 2, j + 1] + minesHelp[i, j + 2] + minesHelp[i + 1, j + 2] + minesHelp[i + 2, j + 2];
				}
			}
			Console.Clear();
			for (int i = 0; i < 10; i++)
			{
				for (int j = 0; j < 10; j++)
				{
					Console.Write(minesAround[i, j].ToString());
				}
				Console.WriteLine();
			}
			Console.WriteLine();
			for (int i = 1; i < 11; i++)
			{
				for (int j = 1; j < 11; j++)
				{
					Console.Write(minesHelp[i, j].ToString());
				}
				Console.WriteLine();
			}
			Console.Beep();
		}

		private void PlacementOfMines()
		{
			for (int i = 0; i < numberOfMines; i++)
			{
				int X = (int)(positionsOfMines[i] / 10);
				int Y = positionsOfMines[i] - (int)(positionsOfMines[i] / 10) * 10;
				mines[X, Y] = true;
				minesHelp[X + 1, Y + 1] = 1;
			}
			Console.Beep();
		}

		private void GeneratedMines(int v1, int v2)
		{
			int pos = 0;
			for (int i = 0; i < v1; i++)
			{
				Random rn = new Random();
				positionsOfMines[i] = rn.Next(0, sizeOfField * sizeOfField - 1);
				for (int j = 0; j < i; j++)
				{
					if (positionsOfMines[i] == positionsOfMines[j])
					{
						i--;
						break;
					}
				}
			}
			Console.Beep();
		}

		public int tryFild(int x, int y)
		{
			if (observed[x, y] == true)
			{
				return 0;//nothing
			}
			else
			{
				observed[x, y] = true;
				if (mines[x, y] == true)
				{
					return 1;//xplosion
				}
				else
				{
					numberOfObservedPixels++;
					if (numberOfFreePixels == numberOfObservedPixels)
					{
						return 2; // win
					}
					return 3;//mines aroun
				}
			}
		}
	}
}
