// See https://aka.ms/new-console-template for more information
using Lista2.Heuristics.Values;
using Lista2.Heuristics.Variables;
using Lista2.Model;
using Lista2.Services;

IValueHeuristic<int> GetValueHeuristic()
{
    return new NoHeuristic<int>();
}
IVariableHeuristic<Field, int> GetVariableHeuristic()
{
    return new SmallerDomainHeuristic<Field, int>();
}
int minSolutions = 5;
int maxSolutions = 8;

var binaryInputs = new string[] { "binary_6x6", "binary_8x8", "binary_10x10" };
//var binaryInputs = new string[] { };
var futoshikiInputs = new string[] { "futoshiki_4x4", "futoshiki_5x5", "futoshiki_6x6" };


var fileSerializer = new FileSerializer();
var solutionsWithMetrics = new List<CspSolution<Field, int>>();

for (int solutionsCount = minSolutions; solutionsCount <= maxSolutions; solutionsCount++)
{
    Console.WriteLine();
    Console.WriteLine("===============================================");
    Console.WriteLine($"Solutions count = {solutionsCount}");
    Console.WriteLine();

    foreach (var binaryInput in binaryInputs)
    {
        var binary = fileSerializer.ReadBinary(binaryInput);

        var solutionsBacktracking = binary.SolveBacktracking(GetValueHeuristic(), GetVariableHeuristic(), solutionsCount);
        solutionsBacktracking.ProblemName = binaryInput;
        solutionsWithMetrics.Add(solutionsBacktracking);

        var solutionsForwardChecking = binary.SolveForwardChecking(GetValueHeuristic(), GetVariableHeuristic(), solutionsCount);
        solutionsForwardChecking.ProblemName = binaryInput;
        solutionsWithMetrics.Add(solutionsForwardChecking);

        LoggerService.PrintBinarySolutions(solutionsBacktracking, binary.Variables, binary.Size);
        LoggerService.PrintBinarySolutions(solutionsForwardChecking, binary.Variables, binary.Size);
    }

    foreach (var input in futoshikiInputs)
    {
        var futhosiki = fileSerializer.ReadFutoshiki(input);

        var futoshikiSolutions = futhosiki.SolveBacktracking(GetValueHeuristic(), GetVariableHeuristic(), solutionsCount);
        futoshikiSolutions.ProblemName = input;
        solutionsWithMetrics.Add(futoshikiSolutions);

        var futoshikiSolutions2 = futhosiki.SolveForwardChecking(GetValueHeuristic(), GetVariableHeuristic(), solutionsCount);
        futoshikiSolutions2.ProblemName = input;
        solutionsWithMetrics.Add(futoshikiSolutions2);

        LoggerService.PrintFutoshikiSolutions(futoshikiSolutions, futhosiki.Variables, futhosiki.Inequalities, futhosiki.Size);
        LoggerService.PrintFutoshikiSolutions(futoshikiSolutions2, futhosiki.Variables, futhosiki.Inequalities, futhosiki.Size);
    }
}

fileSerializer.SaveSolutionsToFile(solutionsWithMetrics);

//var hardcodedValues = new List<FieldHardcodedValue> { 
//    new FieldHardcodedValue { Row = 0, Column = 0, Value = 1 },
//    new FieldHardcodedValue { Row = 0, Column = 3, Value = 0 },
//    new FieldHardcodedValue { Row = 1, Column = 2, Value = 0 },
//    new FieldHardcodedValue { Row = 1, Column = 3, Value = 0 },
//    new FieldHardcodedValue { Row = 1, Column = 5, Value = 1 },
//    new FieldHardcodedValue { Row = 2, Column = 1, Value = 0 },
//    new FieldHardcodedValue { Row = 2, Column = 2, Value = 0 },
//    new FieldHardcodedValue { Row = 2, Column = 5, Value = 1 },
//    new FieldHardcodedValue { Row = 4, Column = 0, Value = 0 },
//    new FieldHardcodedValue { Row = 4, Column = 1, Value = 0 },
//    new FieldHardcodedValue { Row = 4, Column = 3, Value = 1 },
//    new FieldHardcodedValue { Row = 5, Column = 1, Value = 1 },
//    new FieldHardcodedValue { Row = 5, Column = 4, Value = 0 },
//    new FieldHardcodedValue { Row = 5, Column = 5, Value = 0 },
//};

//var futhoshikiHardcodedValues = new List<FieldHardcodedValue>()
//{
//    new FieldHardcodedValue {Row = 0, Column = 2, Value = 3},
//    new FieldHardcodedValue {Row = 1, Column = 1, Value = 3},
//    new FieldHardcodedValue {Row = 3, Column = 3, Value = 3},
//};

//var futhoshikiInequalities = new List<InequalityConstraintModel>()
//{
//    new InequalityConstraintModel {Variable1Row = 1, Variable1Column = 0, Variable2Row = 2, Variable2Column = 0, Operator = Lista2.Enums.InequalityOperator.GreaterThan},
//    new InequalityConstraintModel {Variable1Row = 1, Variable1Column = 1, Variable2Row = 2, Variable2Column = 1, Operator = Lista2.Enums.InequalityOperator.LessThan},
//    new InequalityConstraintModel {Variable1Row = 1, Variable1Column = 2, Variable2Row = 2, Variable2Column = 2, Operator = Lista2.Enums.InequalityOperator.GreaterThan},
//    new InequalityConstraintModel {Variable1Row = 2, Variable1Column = 0, Variable2Row = 3, Variable2Column = 0, Operator = Lista2.Enums.InequalityOperator.GreaterThan},
//    new InequalityConstraintModel {Variable1Row = 3, Variable1Column = 0, Variable2Row = 3, Variable2Column = 1, Operator = Lista2.Enums.InequalityOperator.GreaterThan},
//    new InequalityConstraintModel {Variable1Row = 3, Variable1Column = 2, Variable2Row = 3, Variable2Column = 3, Operator = Lista2.Enums.InequalityOperator.GreaterThan},
//};