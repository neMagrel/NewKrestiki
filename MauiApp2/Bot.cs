using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp2
{
    public class Bot
    {
        public bool?[,] board;
        public bool botSign;
        public bool botDifficulty;
        public static Random random = new Random();
        public int[] BotMove(int[] move)
        {
            int[] botMove = new int[2];
            if (move[0] == 3)
            {
                board[1, 1] = true;
                return [1, 1];
            }
            board[move[0], move[1]] = botSign != true;
            if (botDifficulty == true)
            {
                int[] winMove = CheckWinMove(botSign);
                if (winMove.Length != 1)
                {
                    board[winMove[0], winMove[1]] = botSign;
                    return winMove;
                }
            }
            if (board[1, 1] == null)
            {
                board[1, 1] = botSign;
                return [1, 1];
            }
            else
            {
                botMove = RandomMoveToCorner();
                if (botMove[0] != 1)
                {
                    board[botMove[0], botMove[1]] = botSign;
                    return botMove;
                }
            }

            botMove = RandomMoveToSide();
            board[botMove[0], botMove[1]] = botSign;
            return botMove;
        }

        public int[] RandomMoveToCorner()
        {
            List<int[]> moves = new List<int[]>();

            if (board[0, 0] == null) moves.Add([0, 0]);
            if (board[0, 2] == null) moves.Add([0, 2]);
            if (board[2, 0] == null) moves.Add([2, 0]);
            if (board[2, 2] == null) moves.Add([2, 2]);

            int randomIndex;
            if (moves.Count != 0)
            {
                randomIndex = random.Next(moves.Count);
                return moves[randomIndex];
            }

            return [1, 1];

        }

        public int[] RandomMoveToSide()
        {
            List<int[]> moves = new List<int[]>();

            if (board[0, 1] == null) moves.Add([0, 1]);
            if (board[1, 0] == null) moves.Add([1, 0]);
            if (board[1, 2] == null) moves.Add([1, 2]);
            if (board[2, 1] == null) moves.Add([2, 1]);

            int randomIndex;
            if (moves.Count != 0)
            {
                randomIndex = random.Next(moves.Count);
                return moves[randomIndex];
            }

            return [1, 1];
        }

        public int[] CheckWinMove(bool botSign)
        {
            int[] botMove = new int[] { 3, 3 };
            int[] playerMove = new int[] { 3, 3 };
            int[] checkResult;
            checkResult = CheckMoveSubFunc([0, 0], [0, 1], [0, 2], botSign);
            if (checkResult[0] != -1)
            {

                if (checkResult[0] == 0)
                {
                    playerMove = [checkResult[1], checkResult[2]];
                }
                else
                {
                    botMove = [checkResult[1], checkResult[2]];
                }
            }

            checkResult = CheckMoveSubFunc([1, 0], [1, 1], [1, 2], botSign);
            if (checkResult[0] != -1)
            {

                if (checkResult[0] == 0)
                {
                    playerMove = [checkResult[1], checkResult[2]];
                }
                else
                {
                    botMove = [checkResult[1], checkResult[2]];
                }
            }

            checkResult = CheckMoveSubFunc([2, 0], [2, 1], [2, 2], botSign);
            if (checkResult[0] != -1)
            {

                if (checkResult[0] == 0)
                {
                    playerMove = [checkResult[1], checkResult[2]];
                }
                else
                {
                    botMove = [checkResult[1], checkResult[2]];
                }
            }
            checkResult = CheckMoveSubFunc([0, 0], [1, 0], [2, 0], botSign);
            if (checkResult[0] != -1)
            {

                if (checkResult[0] == 0)
                {
                    playerMove = [checkResult[1], checkResult[2]];
                }
                else
                {
                    botMove = [checkResult[1], checkResult[2]];
                }
            }

            checkResult = CheckMoveSubFunc([0, 1], [1, 1], [2, 1], botSign);
            if (checkResult[0] != -1)
            {

                if (checkResult[0] == 0)
                {
                    playerMove = [checkResult[1], checkResult[2]];
                }
                else
                {
                    botMove = [checkResult[1], checkResult[2]];
                }
            }

            checkResult = CheckMoveSubFunc([0, 2], [1, 2], [2, 2], botSign);
            if (checkResult[0] != -1)
            {

                if (checkResult[0] == 0)
                {
                    playerMove = [checkResult[1], checkResult[2]];
                }
                else
                {
                    botMove = [checkResult[1], checkResult[2]];
                }
            }

            checkResult = CheckMoveSubFunc([0, 0], [1, 1], [2, 2], botSign);
            if (checkResult[0] != -1)
            {

                if (checkResult[0] == 0)
                {
                    playerMove = [checkResult[1], checkResult[2]];
                }
                else
                {
                    botMove = [checkResult[1], checkResult[2]];
                }
            }

            checkResult = CheckMoveSubFunc([0, 2], [1, 1], [2, 0], botSign);
            if (checkResult[0] != -1)
            {

                if (checkResult[0] == 0)
                {
                    playerMove = [checkResult[1], checkResult[2]];
                }
                else
                {
                    botMove = [checkResult[1], checkResult[2]];
                }
            }

            if (botMove[0] != 3)
            {
                return botMove;
            }
            else if (playerMove[0] != 3)
            {
                return playerMove;
            }
            return [1];
        }

        public int[] CheckMoveSubFunc(int[] a, int[] b, int[] c, bool botSign)
        {
            if (board[a[0], a[1]] == board[b[0], b[1]] && board[a[0], a[1]] != null && board[c[0], c[1]] == null)
            {
                if (board[a[0], a[1]] == botSign)
                {
                    return [0, c[0], c[1]];
                }
                else
                {
                    return [1, c[0], c[1]];
                }
            }

            if (board[a[0], a[1]] == board[c[0], c[1]] && board[a[0], a[1]] != null && board[b[0], b[1]] == null)
            {
                if (board[a[0], a[1]] == botSign)
                {
                    return [0, b[0], b[1]];
                }
                else
                {
                    return [1, b[0], b[1]];
                }
            }
            if (board[c[0], c[1]] == board[b[0], b[1]] && board[a[0], a[1]] == null && board[c[0], c[1]] != null)
            {
                if (board[c[0], c[1]] == botSign)
                {
                    return [0, a[0], a[1]];
                }
                else
                {
                    return [1, a[0], a[1]];
                }
            }
            return [-1];
        }
        public static void ResetBotBoard()
        {
            bool?[,] board = new bool?[,]
            {
            { null, null, null },
            { null, null, null },
            { null, null, null }
            };
        }
    }
}
