using System;
using System.Collections.Generic;
using Calculator.UserInteraction;
using Calculator.CalculationProcessor;
using Calculator.Parser;
using Calculator.Commands;
using Calculator.FileProcessor;

namespace Calculator
{
    public class Client
    {
        private string _codeToExit = "exit";
        private string _pathToFile;
        private IUserInteraction _interaction;
        private IFileHandler _fileHandler;
        private CalculatorRealization _calculatorMode;
        private ExpressionParser _parserMode;
        
        public Client(IUserInteraction interaction, string pathToFile)
        {
            this._interaction = interaction;
            this._pathToFile = pathToFile;
        }

        public void Init()
        {
            if (String.IsNullOrWhiteSpace(this._pathToFile))
            {
                SetCalcuratorType(new GetUserCalculatorTypeCommand(this._interaction));

                if (this._parserMode is ConsoleParser)
                {
                    StartConsoleProcess();
                }
                else
                {
                    StartFileProcess();
                }
            }
            else
            {
                this._calculatorMode = new FileCalculator();
                this._parserMode = new FileExpressionParser();
                this._fileHandler = new FileHandler();
                StartFileProcess();
            }
        }

        private void StartConsoleProcess()
        {
            string userInput = GetUserExpression(new GetUserInputCommand(this._interaction));
            
            if (userInput != this._codeToExit)
            {
                bool expressionHasCorrectFormat = CheckExpression(userInput);

                if (expressionHasCorrectFormat)
                {
                    List<string> parsedexpression = ParseString(userInput);
                    double result = RunCalculator(parsedexpression);
                    ShowResponse(new ShowResponseCommand(this._interaction, result.ToString()));
                }
            }
        }


        private void StartFileProcess()
        {
            if (String.IsNullOrWhiteSpace(this._pathToFile))
            {
                this._pathToFile = GetPathToFile(new GetUserInputCommand(this._interaction));
            }

            string[] expressions = GetExpressionsFromFile(this._pathToFile);

            for (int i = 0; i < expressions.Length; i++)
            {
                bool expressionHasCorrectFormat = CheckExpression(expressions[i]);

                if (expressionHasCorrectFormat)
                {
                    List<string> parsedexpression = ParseString(expressions[i]);
                    double result = RunCalculator(parsedexpression);
                    expressions[i] = expressions[i] + " = " + result.ToString();
                }
                else if(!expressionHasCorrectFormat)
                {
                    expressions[i] = expressions[i] + " = mistake in expression";
                }
            }

            WriteResultToFile(expressions, this._pathToFile);
        }

        private void SetCalcuratorType(GetUserCalculatorTypeCommand command)
        {
            command.Execute();

            this._calculatorMode = command.CalculatorType;
            this._parserMode = command.ParserType;

            if(command.ParserType is FileExpressionParser)
            {
                this._fileHandler = new FileHandler();
            }
        }

        private string GetUserExpression(GetUserInputCommand command)
        {
            command.Execute();

            if (!String.IsNullOrWhiteSpace(command.UserInput))
            {
                return command.UserInput;
            }

            return GetUserExpression(command);
        }

        private bool CheckExpression(string expression)
        {
            return this._parserMode.CheckExpression(expression);
        }

        private List<string> ParseString(string expression)
        {
            return this._parserMode.Parse(expression);
        }

        private double RunCalculator(List<string> expression)
        {
            return this._calculatorMode.SimpleCalculating(expression);

        }

        private string GetPathToFile(GetUserInputCommand command)
        {
            command.Execute();

            if (!String.IsNullOrWhiteSpace(command.UserInput) || !this._fileHandler.CheckPath(command.UserInput))
            {
                return command.UserInput;
            }

            return GetPathToFile(command);
        }

        private string[] GetExpressionsFromFile(string path)
        {
            return this._fileHandler.GetLinesFromFile(path);
        }

        private void WriteResultToFile(string[] resultArray, string path)
        {
            this._fileHandler.WriteDataToFile(resultArray, path);
        }

        private void ShowResponse(ICommand command)
        {
            command.Execute();
        }

    }
}
