using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp2
{
    public class Logic
    {
        public static bool blockButtonsFlag = false;
        public void FirstLogic()// метод для логики, ничего не передается
        {
            if (GlobalState.CurrentSettings.botType) // проверка переменной в классе настроек (выбор режима: бот против бота или бот против игрока), если бот против бота:
            {
                AutoModeLogic();
            }
            else // если игрок против бота:
            {
                Bot bot = new()
                {
                    board = board,
                    botSign =! GlobalState.CurrentSettings.playerSign,
                    botDifficulty = GlobalState.CurrentSettings.difficulty
                };
                
                if (!GlobalState.CurrentSettings.playerSign) //   проверка за кого играет игрок (нолик или крестик), если за нолик:
                {
                    bot.BotMove([3, 3]); // Вернешь от него ход
                }

            }
        }
        public void AutoModeLogic()
        {
            Bot botCross = new()
            {
                botSign = true,
                botDifficulty = true,
                board = board
            };
            Bot botZero = new()
            {
                botDifficulty = false,
                botSign = false,
            };
            while (GlobalState.CurrentSettings.start)
            {
                int[] botMove = botCross.BotMove(GlobalState.CurrentSettings.currentMove);
                SetTextOnCell(botMove, botCross.botSign);
                GlobalState.CurrentSettings.currentMove = botMove;

                if(CheckFinishGame() != 0)
                {
                    GlobalState.CurrentSettings.start = false;
                    break;
                }

                botMove = botCross.BotMove(GlobalState.CurrentSettings.currentMove);
                SetTextOnCell(botMove, botCross.botSign);
                GlobalState.CurrentSettings.currentMove = botMove;

                if (CheckFinishGame() != 0)
                {
                    GlobalState.CurrentSettings.start = false;
                    break;
                }
            }
        }

        public static void HandMode(int[] cellCoordinates)
        {
            
            //SetTextOnCell(cellCoordinates, playerSign);
            //if (CheckFinishGame != 0)
            //{
            //  int[] botMove = bot1.BotMove(cellCoordinates)
            //  SetTextOnCell(botMove, bot1.botSign);
            //}
        }
        public static string ToggleButtonState(string buttonName)
        {
            if (buttonName == "ButtonStart")
            {
                GlobalState.CurrentSettings.start = !GlobalState.CurrentSettings.start;
                blockButtonsFlag = !blockButtonsFlag;
                //MainLogic();
                return "Игра начата";
            }
            else if (buttonName == "ButtonDifficulty")
            {
                GlobalState.CurrentSettings.difficulty = !GlobalState.CurrentSettings.difficulty;
                return "Текущая сложность - " + (GlobalState.CurrentSettings.difficulty == false ? "Легко" : "Сложно");
            }
            else if (buttonName == "ButtonTypeOfBots")
            {
                GlobalState.CurrentSettings.botType = !GlobalState.CurrentSettings.botType;
                return "Текущий режим - " + (GlobalState.CurrentSettings.botType == false ? "Авто" : "Ручной");
            }
            else if (buttonName == "ButtonSide")
            {
                GlobalState.CurrentSettings.playerSign = !GlobalState.CurrentSettings.playerSign;
                return "Текущий знак - " + (GlobalState.CurrentSettings.playerSign == true ? "X" : "O");
            }
            throw new Exception("Ошибка: запрашиваемой кнопки не существует в текущем методе");
            
        }
        public void SetTextOnCell(int[] move, bool sign)
        {
            if (!Check(move))
            {
                throw new Exception("Ход уже занят, проверьте правильность кода");
            }
            board[move[0], move[1]] = sign;
            //Вызвать метод, который запишет ход на доску интерфейса
        }
        private static bool?[,] board = new bool?[,]
        {
            { null, null, null},
            { null, null, null},
            { null, null, null}
        };
        public void SetStartingSettings()
        {
            Settings settings = new();

            settings.playerSign = true;
            settings.botType = true;
            settings.start = false;
            settings.difficulty = false;

            GlobalState.CurrentSettings = settings;

        }
        public static bool Check(int[] args)
        {
            if (board[args[0], args[1]] == null)
            {
                return true;
            }
            return false;
        }

        public static int CheckFinishGame()
        {
            for (int i = 0; i < 3; i++)
            {
                if (board[i, 0] != null && board[i, 0] == board[i, 1] && board[i, 1] == board[i, 2])
                {
                    if (board[i, 0] == true)
                    {
                        return 1;
                    }
                    else
                    {
                        return 2;
                    }
                }
            }

            // Проверка столбцов
            for (int i = 0; i < 3; i++)
            {
                if (board[0, i] != null && board[0, i] == board[1, i] && board[1, i] == board[2, i])
                {
                    if (board[0, i] == true)
                    {
                        return 1;
                    }
                    else
                    {
                        return 2;
                    }
                }
            }

            // Проверка диагоналей
            if (board[0, 0] != null && board[0, 0] == board[1, 1] && board[1, 1] == board[2, 2])
            {
                if (board[0, 0] == true)
                {
                    return 1;
                }
                else
                {
                    return 2;
                }
            }
            if (board[0, 2] != null && board[0, 2] == board[1, 1] && board[1, 1] == board[2, 0])
            {
                if (board[0, 2] == true)
                {
                    return 1;
                }
                else
                {
                    return 2;
                }
            }

            int countEmpty = 0;

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (board[i, j] == null)
                    {
                        countEmpty++;
                    }
                }
            }

            if (countEmpty == 0)
            {
                return -1;
            }
            return 0;
        }

        public static void ResetLogicBoard()
        {
            board = new bool?[,]
            {
                {null, null, null},
                {null, null, null},
                {null, null, null}
            };
        }
    }

    public class Settings
    {
        public bool start;                      //Значение true - игра началась, значение false - игра не началась
        public bool difficulty;                 //Значение true - сложно, значение false - легко
        public bool botType;                    //Значение true - игрок против бота, значение false - бот против бота
        public bool playerSign;                 //Значение true - крестик, значение false - нолик

        public int[] currentMove;
    }

    public static class GlobalState
    {
        public static Settings CurrentSettings { get; set; }
    }

}
