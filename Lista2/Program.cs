// See https://aka.ms/new-console-template for more information
using Lista2.Model;

var westernAustralia = "Western Australia";
var northernTerritory = "Northern Territory";
var southAustralia = "South Australia";
var queensland = "Queensland";
var newSouthWales = "New South Wales";
var victoria = "Victoria";
var tasmania = "Tasmania";

var variables = new List<string> { westernAustralia, northernTerritory, southAustralia, 
    queensland, newSouthWales, victoria, tasmania};

var domains = new Dictionary<string, List<string>>();
foreach (var variable in variables)
{
    domains.Add(variable, new List<string> { "red", "green", "blue"});
}
var csp = new CSP<string, string>(variables, domains);
csp.AddConstraint(new MapColoringConstraint(westernAustralia, northernTerritory));
csp.AddConstraint(new MapColoringConstraint(westernAustralia, southAustralia));
csp.AddConstraint(new MapColoringConstraint(southAustralia, northernTerritory));
csp.AddConstraint(new MapColoringConstraint(queensland, northernTerritory));
csp.AddConstraint(new MapColoringConstraint(queensland, southAustralia));
csp.AddConstraint(new MapColoringConstraint(queensland, newSouthWales));
csp.AddConstraint(new MapColoringConstraint(newSouthWales, southAustralia));
csp.AddConstraint(new MapColoringConstraint(victoria, southAustralia));
csp.AddConstraint(new MapColoringConstraint(victoria, newSouthWales));
csp.AddConstraint(new MapColoringConstraint(victoria, tasmania));

var solution = csp.Backtracking(new Dictionary<string, string>());
if (solution is null)
{
    Console.WriteLine("No solution");
}
else
{
    Console.WriteLine("Solution found!");
    foreach (var item in solution.Keys)
    {
        Console.WriteLine($"{item}: {solution[item]}");
    }
}