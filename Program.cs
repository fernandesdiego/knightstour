using System;
using System.Collections.Generic;
using System.Threading;
// return(x + MoveX[i] >= 0 && x + MoveX[i] < boardSize && y + MoveY[i] >= 0 && y + MoveY[i] < boardSize)

namespace knightstour
{
    class Program
    {       
        static int boardSize = 8;
        static int[,] board = new int[boardSize, boardSize];
        static int startX = 0;
        static int startY = 0;
        public static void Main(string[] args)
        {           
            SolveTour();
            Console.Read();
        }

        static void PrintBoard(int[,] board, int moveN = 0)
        {
            Console.Clear();
            for (int i = 0; i < boardSize; i++)
            {
                for (int j = 0; j < boardSize; j++)
                {    
                    if (board[i, j] == -1) {
                        Console.Write($"\t{board[i, j]}", Console.ForegroundColor = ConsoleColor.Cyan);
                    }              
                    else if (board[i, j] == moveN)
                    {
                        Console.Write($"\t{board[i, j]}", Console.ForegroundColor = ConsoleColor.Green);
                        Console.ResetColor();                        
                    }
                    else
                    {
                        Console.Write($"\t{board[i, j]}", Console.ForegroundColor = ConsoleColor.DarkBlue);
                    }
                }
                Console.WriteLine();
            }
            Thread.Sleep(300);
        }

        static bool SolveTour()
        {
            
            int[] moveX = {2, 1, -1, -2, -2, -1, 1, 2};
            int[] moveY = {1, 2, 2, 1, -1, -2, -2, -1};

            InitializeBoard();

            board[startX, startY] = 0;

            if (!SolveRecursively(startX, startY, 1, board, moveX, moveY))
            {
                Console.WriteLine("No solution found");
                return false;
            }
            else
            {
                PrintBoard(board);
            }

            return true;
        }

        static bool SolveRecursively(int x, int y, int moveN, int[,] board, int[] MoveX, int[] MoveY)
        {            
            int k, nextX, nextY;
            if(moveN == boardSize * boardSize)
            {
                Console.WriteLine("Traveled through all positions :)");
                return true;
            }

            for (k = 0; k < 8; k++)
            {
                nextX = x+ MoveX[k];
                nextY = y+ MoveY[k];
                if (SafeMove(nextX, nextY, board))
                {                                        
                    board[nextX, nextY] = moveN;
                    PrintBoard(board, moveN);               
                    if (SolveRecursively(nextX, nextY, moveN + 1, board, MoveX, MoveY))
                    {                        
                        return true;
                    }
                    else
                    {                        
                        board[nextX, nextY] = -1;
                    }
                }                
                
            }
            return false;
        }
         
        static void InitializeBoard()
        {
            for (int i = 0; i < boardSize; i++)
            {
                for (int j = 0; j < boardSize; j++)
                {
                    board[i, j] = -1;
                }
            }
        }
        static bool SafeMove(int x, int y, int[,] board)
        {
            return (x >= 0 && y >= 0 && x < boardSize && y < boardSize && board[x, y] == -1);      
        }
    }
}

