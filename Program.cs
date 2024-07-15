using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace PigDiceGame
{
    internal class Program
    {
        static Random random = new Random();
        static void Main(string[] args)
        {
            Console.WriteLine("Let's Play PIG!" +
                "\n\nSee how many turns it takes you to get to 20." +
                "\nTurn ends when you hold or roll a 1." +
                "\nIf you roll a 1, you lose all points for the turn." +
                "\nIf you hold, you save all points for the turn.");

            int numberOfTurns = 0;
            int totalScore = 0;
            int turnScore = 0;
            bool gameOver = false;

            while (!gameOver)
            {
                numberOfTurns++;
                Console.WriteLine($"\nTURN {numberOfTurns}");
                turnScore = PlayTurn(totalScore);
                totalScore += turnScore;

                CheckWin(ref totalScore,ref numberOfTurns, ref gameOver);

            }

            Console.WriteLine("\nGame Over!!!");
        }

        static void CheckWin(ref int totalScore,ref int numberOfTurns, ref bool gameOver)
        {
            if (totalScore >= 20)
            {
                Console.WriteLine($"You Win! You finished in {numberOfTurns} turns!");
                gameOver = PlayAgain(ref totalScore, ref numberOfTurns);
            }
        }


        static int PlayTurn(int totalScore)
        {
            int turnScore = 0;
            bool turnOver = false;

            while (!turnOver)
            {
                Console.WriteLine("Enter 'r' to roll again, 'h' to hold.");
                char action = Console.ReadLine()[0];
                bool winWithoutHold = false;

                winWithoutHold = PerformAction(action, ref turnOver, totalScore , ref turnScore );
                if(winWithoutHold == true)
                    break;
               
            }
            return turnScore;
        }

        static bool PerformAction(char action, ref bool turnOver, int totalScore, ref int turnScore)
        {
            if (action == 'r')
            {
                int diceNumber = random.Next(1, 7);
                Console.WriteLine($"You Rolled : {diceNumber}");
                if (diceNumber == 1)
                {
                    Console.WriteLine($"Turn over. No Score.\n");
                    turnScore = 0;
                    turnOver = true;
                }
                else
                {
                    turnScore += diceNumber;
                    if (totalScore + turnScore >= 20)
                    {
                        Console.WriteLine($"Your turn score is {turnScore} and " +
                            $"your total score is {totalScore + turnScore}");
                        return true;
                    }


                    Console.WriteLine($"Your turn score is {turnScore} and your " +
                        $"total score is {totalScore}");


                    Console.WriteLine($"If you hold, you will have {totalScore + turnScore} points.");
                }

            }
            else
            {
                Console.WriteLine($"\nYour turn score is {turnScore} and your total score " +
                    $"is {totalScore + turnScore}\n");
                turnOver = true;
                return false;
            }
            return false;
        }


        static bool PlayAgain(ref int totalScore, ref int numberOfTurns)
        {


            Console.WriteLine("\nDo You want to play again ? Yes or No");
            string playChoice = Console.ReadLine();

            if (playChoice.ToLower() == "yes")
            {
                totalScore = 0;
                numberOfTurns = 0;
                return false;
            }
            else
                return true;

        }



    }
}
