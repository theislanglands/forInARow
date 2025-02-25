﻿using System.Drawing;
using System.Numerics;

namespace ForInARow

{
    public class Board
    {
        int width = 7;
        int height = 6;

        int[,] grid;

        public Board()
        {
            // empty = 0, player1 = 1, player2 = 2
            grid = new int[height, width];
            FillWithNumber(0);
        }

        public int getHeight() {
            return height;
        }

        public int getWidth() {
            return width;
        }


        public void FillWithNumber(int value)
        {
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    grid[i, j] = value;
                }
            }
        }

        public int getValueAtPoint(int row, int slot) {

            return grid[row, slot];
        }
        public void Clear()
        {
            Array.Clear(grid, 0, grid.Length);
        }

        public bool HasAvailableSlots()
        {
            for (int i = 0; i < width; i++)
            {
                if (grid[0,i] == 0) return true;
            }
            return false;
        }


        public bool CheckVictory(int noInRow, int playerId)
        {
            for (int row = 0; row < height; row++)
            {
                for (int col = 0; col < width; col++)
                {
                    int basevalue = grid[row, col];
                    if (basevalue == 0 || basevalue != playerId) continue;
                    if (ChechXInARow(row, col, noInRow) == true)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private bool ChechXInARow(int row, int col, int NoInRow)
        {
            Point[] directionVectors = GetDirectionVectors();
            int dx, dy;

            foreach (Point vector in directionVectors)
            {
                int countNoInRow = 1;
                

                for (int i = 1; i < NoInRow; i++) // chceking x away from basevalue
                {
                    dx = col + vector.X * i;
                    dy = row + vector.Y * i;
                    if (dx < 0 || dy < 0 || dx == width || dy == height) break; // outside grid
                    if (grid[dy, dx] != grid[row, col]) break; // wrong value
                    else
                    {
                        countNoInRow++;
                        if (countNoInRow == NoInRow)
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
                    
        }

        private static Point[] GetDirectionVectors()
        {
            return new Point[]
            {
                    new Point(0, -1), // up
                    new Point(1, -1), // right-up
                    new Point(1, 0), // right
                    new Point(1, 1), // right-down
                    new Point(0, 1), // down
                    new Point(-1, 1), // down left
                    new Point(-1, 0), // left
                    new Point(-1, -1), // left up
            };
        }

        public bool AddToSlot(int playerNumber, int col) {

            if (col >= width || col<0) throw new ArgumentException("column input exceedes board size", nameof(col));
            if (playerNumber < 1 || playerNumber > 2) throw new ArgumentException("player number should be 1 or 2", nameof(playerNumber));


            // initial value = empty column
            int index = height - 1;
            bool success;

            // finding correct index
            for (int row = 0; row < height; row++)
            {
                if (grid[row, col] != 0)
                {
                    index = row - 1;
                    break;
                }
            }

            // if found index less than zero, col is full
            if (index < 0) success = false;
            else
            {
                grid[index, col] = playerNumber;
                success = true;
            }

            return success;
        }

        public override string ToString()
        {
			string playingBoard = "";
            playingBoard += GetGritString();
            playingBoard += GetColNumberString();
            return playingBoard;
        }

		private string GetGritString()
		{
			string gritString = "";

            for (int row = 0; row < height; row++)
            {
                gritString += " | ";

                for (int col = 0; col < width; col++)
                {
                    if (grid[row, col] != 0)
                    {
                        gritString += $"{grid[row, col]} | ";
                    }
                    else
                    {
                        gritString += "  | ";
                    }

                }

                gritString += "\n";
            }

            return gritString;
		}

        private string GetColNumberString() {
            // drawing seperator
            string colNumberString = " ";
            for (int col = 0; col < width; col++)
            {
                colNumberString += "====";
            }
            colNumberString += "=\n";

            // drawing col numbers
            colNumberString += " ";
            for (int col = 0; col < width; col++)
            {
                colNumberString += $"  {col + 1} ";
            }

            return colNumberString;
        }

        internal object getGrid()
        {
            throw new NotImplementedException();
        }
    }
}