namespace Lista2.Model
{
    public class CspSolution<V, D>
    {
        public List<Dictionary<V, D>> Solutions { get; set; }
        public int Iterations { get; set; }
        public double TotalMiliseconds { get; set; }

        public CspSolution(List<Dictionary<V, D>> solutions, int iterations, double totalMiliseconds)
        {
            Solutions = solutions;
            Iterations = iterations;
            TotalMiliseconds = totalMiliseconds;
        }
    }
}
