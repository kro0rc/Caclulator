using System;
using System.Collections.Generic;
using Calculator.UserInteraction;
using Calculator.CalculationProcessor;
using Calculator.Parser;
using Calculator.Commands;
using Calculator.FileProcessor;
using System.Globalization;

namespace Calculator
{
    public class Client
    {
        private string _codeToExit = "exit";
        private string _mistake = " = mistake in expression";
        private string _signOfAssignment = " = ";
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
                    List<string> parsedExpression = RetrieveParsedExpression(userInput);
                    bool parsedExpressionIsValid = CheckParsedExpression(parsedExpression);

                    if(parsedExpressionIsValid)
                    {
                        double result = RunCalculator(parsedExpression);
                        ShowResponse(new ShowResponseCommand(this._interaction, result.ToString()));
                    }
                    else
                    {
                        ShowResponse(new ShowResponseCommand(this._interaction, MessagesTemplates.WarnIncorrectInput));
                    }
                }
                else
                {
                    ShowResponse(new ShowResponseCommand(this._interaction, MessagesTemplates.WarnIncorrectInput));
                }
            }

            StartConsoleProcess();
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
                    string expressionToModify = expressions[i];
                    int nestingLevel = GetNestingLevel(expressions[i]);

                    for (int k = 0; k <= nestingLevel; k ++)
                    {
                        int currentNestingLevel = nestingLevel - k;

                        if(currentNestingLevel == 0)
                        {
                            List<string> parsedExpression = RetrieveParsedExpression(expressionToModify);
                            bool parsedExpressionIsValid = CheckParsedExpression(parsedExpression);

                            if (parsedExpressionIsValid)
                            {
                                double result = RunCalculator(parsedExpression);
                                expressions[i] = expressions[i] + this._signOfAssignment + result.ToString(CultureInfo.InvariantCulture);
                            }
                            else
                            {
                                expressions[i] = expressions[i] + this._mistake;
                            }
                        }
                        else
                        {
                            expressionToModify = SimplifyNesting(expressionToModify, currentNestingLevel);
                        }
                    }  
                }
                else if(!expressionHasCorrectFormat)
                {
                    expressions[i] = expressions[i] + this._mistake;
                }
            }

            WriteResultToFile(expressions, this._pathToFile);
        }

        private string SimplifyNesting(string expression, int currentNestringLevel)
        {
            int[] indexes = this._parserMode.GetNestedExpressionIndexes(expression, currentNestringLevel);
            String expressionPart = expression.Substring(indexes[0], indexes[1] - indexes[0] + 1);
            double result = this._calculatorMode.SimpleCalculating(this.ParseString(this._parserMode.PrepareExpressionPart(expressionPart)));

            return expression.Replace(expressionPart, result.ToString(CultureInfo.InvariantCulture));
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
            return this._parserMode.CheckExpressionBeforeParsing(expression);
        }

        private bool CheckParsedExpression(List<string> expression)
        {
            int numbersCount = 0;
            int signsCount = 0;
            List<string> availableOperators = new List<string>() { "-", "+", "*", "/" };

            for (int i = 0; i < expression.Count; i++)
            {
                if(Double.TryParse(expression[i], NumberStyles.Number, CultureInfo.InvariantCulture, out double res))
                {
                    numbersCount++;
                }
                else if(availableOperators.Contains(expression[i]))
                {
                    signsCount++;
                }
            }

            if (numbersCount == 2 && signsCount > 0 && signsCount <= 3)
            {
                return true;
            }

            return false;
        }

        private int GetNestingLevel(string expression)
        {
            return this._parserMode.GetNestingLevel(expression);
        }

        private List<string> RetrieveParsedExpression(string expression)
        {
            return this._parserMode.ParseExpression(expression);
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
