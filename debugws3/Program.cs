﻿using System;
using System.Collections;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;

namespace debugws3
{
  public struct Result
  {
    public double result;
    public string err;

    public Result(double _result, string _err)
    {
      result = _result;
      err = _err;
    }
  }

  public class Calc
  {
    private static void WelcomeMessage()
    {
      Console.WriteLine(
          "\n\n  --------------------------- Welcome to Calculator.NET! --------------------------" +
          "\n\n Enter any calculation you would like to perform using any of the four operators" +
          "\n\n\n\t\t\t\t `+` `-` `*` `/`                                      " +
          "\n" +
          "\n\tExamples: 45 + 16" +
          "\n\t          65 * 4 / 531" +
          "\n\t          49 - 2 + 134 * 648" +
          "\n\n" +
          "\t\t\t\t You can type `EXIT` any time to leave the application." +
          "\n\n");
    }

    //Method invoked as long as the user is running the calculator
    private static bool CalculatorOn()
    {
      var userInput = Console.ReadLine();

      if (String.IsNullOrEmpty(userInput))
      {
        const string message = "Type something! :)";
        Console.WriteLine(message);
        return true;
      }

      if (userInput.ToLower() == "exit")
        return false;

      var result = Calculate(userInput);

      if (result.err != "")
      {
        Console.WriteLine("Error: {0}", result.err);
      }
      else
      {
        Console.WriteLine("Result: {0}", result.result);
        Console.WriteLine("\nType 'Exit' to leave :( Or try another calculation :)\n");
      }

      return true;
    }

    public static Result Calculate(string input)
    {
      if (input == null)
      {
        return new Result(0, "Wrong entry. Try again using one or more operations");
      }

      //Getting numbers and math operators using Regex
      var numArray = Regex.Matches(input, "[0-9]+").Cast<Match>().Select(m => m.Value).ToArray();
      var opArray = Regex.Matches(input, @"[+-\/*]").Cast<Match>().Select(m => m.Value).ToArray();

      //Casting Arrays to the correct data type
      int[] numbersToBeCalculated = Array.ConvertAll(numArray, int.Parse);
      char[] operations = Array.ConvertAll(opArray, char.Parse);

      //Checking if math operators are equal or more than the numbers to be calculated
      if (operations.Length >= numbersToBeCalculated.Length)
      {
        return new Result(0, "Wrong entry. Try again using one or more operations");
      }

      double result = numbersToBeCalculated[0];

      Stack stack =new Stack();

      var j = 0;
      for (var i = 1; i < numbersToBeCalculated.Length; i++)
      {
        switch (operations[j])
        {
          case '+':
            {
              result += numbersToBeCalculated[i];
              break;
            }
          case '-':
            {
              result -= numbersToBeCalculated[i];
              break;
            }
          case '*':
            {
              result *= numbersToBeCalculated[i];
              break;
            }
          case '/':
            {
              result /= numbersToBeCalculated[i];
              break;
            }
          default:
            break;
        }
        j++;
      }

      DataTable t = new DataTable();
      result = Convert.ToDouble( t.Compute(input, null));

      return new Result(result, "");
    }

    public static void ExitAndThankYouMessage()
    {
      Console.Clear();
      Console.WriteLine("\n\n\n\n");
      Console.WriteLine(@"
                         _____  _   _  _____  _   _  _   _     _     _  _____  _   _
                        (_   _)( ) ( )(  _  )( ) ( )( ) ( )   ( )   ( )(  _  )( ) ( )
                          | |  | |_| || (_) || `\| || |/'/'   `\`\_/'/'| ( ) || | | |
                          | |  |  _  ||  _  || , ` || , <       `\ /'  | | | || | | |
                          | |  | | | || | | || |`\ || |\`\       | |   | (_) || (_) |
                          (_)  (_) (_)(_) (_)(_) (_)(_) (_)      (_)   (_____)(_____)
                                    |\
                            /    /\/o\_
                           (.-.__.(   __o
                        /\_(      .----'
                         .' \____/
                        /   /  / \
                    ___:____\__\__\__________________________________________________________");
    }

    private static void Main(string[] args)
    {
      WelcomeMessage();

      Console.WriteLine("-> Calculate ");

      bool calculatorOn = true;
      while (calculatorOn)
      {
        calculatorOn = CalculatorOn();
      }

      ExitAndThankYouMessage();
    }

  }
}