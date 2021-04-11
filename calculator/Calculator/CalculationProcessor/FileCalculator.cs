using System;
using System.Collections.Generic;
using System.Globalization;
using Calculator.UserInteraction;
using Calculator.Commands;
using Calculator.Parser;
using Calculator.FileProcessor;


namespace Calculator.CalculationProcessor
{
    class FileCalculator : CalculatorRealization
    {
        private string _pathToFile;
        private string _signOfAssignment = " = ";
        private string _mistake = " = error in expression";
        private IUserInteraction _userInteraction;
        private ExpressionParser _parser;
        private IFileHandler _fileHandler;

        public FileCalculator(string pathToFile)
        {
            this._pathToFile = pathToFile;
            this._userInteraction = new ConsoleInteraction();
            this._parser = new FileExpressionParser();
            this._fileHandler = new FileHandler();
        }

        public override void Run()
        {
            if (String.IsNullOrWhiteSpace(this._pathToFile))
            {
                this._pathToFile = GetPathToFile(new GetUserPathCommand(this._userInteraction));
            }

            string[] expressions = this._fileHandler.GetLinesFromFile(this._pathToFile);

            for (int i = 0; i < expressions.Length; i++)
            {
                bool expressionHasCorrectFormat = this._parser.CheckExpressionBeforeParsing(expressions[i]);

                if (expressionHasCorrectFormat)
                {
                    string expressionToModify = expressions[i];
                    int nestingLevel = this._parser.GetNestingLevel(expressions[i]);

                    for (int k = 0; k <= nestingLevel; k++)
                    {
                        int currentNestingLevel = nestingLevel - k;

                        if (currentNestingLevel == 0)
                        {
                            List<string> parsedExpression = this._parser.ParseExpression(expressionToModify);
                            bool parsedExpressionIsValid = CheckParsedExpression(parsedExpression);

                            if (parsedExpressionIsValid)
                            {
                                double result = base.SimpleCalculating(parsedExpression);
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
                else if (!expressionHasCorrectFormat)
                {
                    expressions[i] = expressions[i] + this._mistake;
                }
            }

            this._fileHandler.WriteDataToFile(expressions, this._pathToFile);
        }

        private string GetPathToFile(GetUserPathCommand command)
        {
            command.Execute();

            if (!String.IsNullOrWhiteSpace(command.UserInput) && this._fileHandler.CheckPath(command.UserInput))
            {
                return command.UserInput;
            }

            return GetPathToFile(command);
        }
        private string SimplifyNesting(string expression, int currentNestringLevel)
        {
            int[] indexes = this._parser.GetNestedExpressionIndexes(expression, currentNestringLevel);
            String expressionPart = expression.Substring(indexes[0], indexes[1] - indexes[0] + 1);
            double result = base.SimpleCalculating(this._parser.ParseExpression(this._parser.PrepareExpressionPart(expressionPart)));

            return expression.Replace(expressionPart, result.ToString(CultureInfo.InvariantCulture));
        }

    }
}
