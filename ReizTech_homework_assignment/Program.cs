internal class Program
{
    private static void Main(string[] args)
    {
        FirstTask();
        SecondTask();
    }
    //--------------------------------------------first task starts here------------------------------------------------------------

    /// <summary>
    /// First (clock) task
    /// </summary>
    private static void FirstTask()
    {
        int hours, minutes;
        double hourDegrees, minuteDegrees, answerDegrees;
        Console.WriteLine("Enter hours, 1 to 12, whole number: "); //user must be able to add analogue clock hours which is 1 to 12
        try
        {
            hours = int.Parse(Console.ReadLine());
            if(hours < 1 || hours > 12)
            {
                HandleTimeError();
                return;
            }
        }
        catch
        {
            HandleTimeError();
            return;
        }
        Console.WriteLine("Enter minutes, 0 to 60, whole number: ");
        try
        {
            minutes = int.Parse(Console.ReadLine());
            if (minutes < 0 || minutes > 60)
            {
                HandleTimeError();
                return;
            }
        }
        catch
        {
            HandleTimeError();
            return;
        }

        minuteDegrees = minutes * 6; //1 minute = 6 degrees
        hourDegrees = hours * 30 + minutes * 0.5; //1 hour = 30 degrees and we must shift it by how many minutes have passed
                                                  //(1 minute is 0.5 degrees for the hour hand)

        switch(minuteDegrees > hourDegrees)
        {
            case true:
                answerDegrees = CalculateDegrees(minuteDegrees, hourDegrees);
                break;
            case false:
                answerDegrees = CalculateDegrees(hourDegrees, minuteDegrees);
                break;
        }
        Console.WriteLine("Lesser angle between the hour and minute hands is: \n" + answerDegrees + " degrees.");
    }


    /// <summary>
    /// Method finds the angle between the minute and hour hands by evaluating which hand is standing at a higher angle on the clock.
    /// </summary>
    /// <param name="high">larger degree value</param>
    /// <param name="low">smaller degree value</param>
    /// <returns></returns>
    private static double CalculateDegrees(double high, double low)
    {
        return high - low > 180 ? 360 - high + low : high - low;
    }
    /// <summary>
    /// Handles error when time is written incorrectly
    /// </summary>
    private static void HandleTimeError()
    {
        Console.WriteLine("Time format is incorrect.");
    }

    //--------------------------------------------second task starts here------------------------------------------------------------
    /// <summary>
    /// Second (tree) task
    /// </summary>
    private static void SecondTask()
    {
        Branch root = new Branch();
        FillTree(root);
        Console.WriteLine("Tree depth is: " + (CalculateTreeDepth(root))); 
    }
    /// <summary>
    /// Hard-codes data into tree (it aint purty but it works)
    /// </summary>
    /// <param name="root">root node</param>
    /// <returns></returns>
    public static void FillTree(Branch root)
    {
        Branch currBranch = root;

        for (int i = 0; i < 3; i++) root.branches.Add(new Branch()); // adds 3 branches to root
        currBranch = root.branches.First();

        for (int i = 0; i < 2; i++) currBranch.branches.Add(new Branch()); // adds 2 branches to 2nd layer 1st branch
        currBranch = currBranch.branches.First();

        for (int i = 0; i < 2; i++) currBranch.branches.Add(new Branch()); // adds 2 branches to 3rd layer 1st branch
        currBranch = root.branches[1];

        currBranch.branches.Add(new Branch());                             // adds 1 branch to 2nd layer 2nd branch
        currBranch = currBranch.branches.First();

        currBranch.branches.Add(new Branch());                             // adds 1 branch to 3rd layer 3rd branch
        currBranch = currBranch.branches.First();

        for (int i = 0; i < 2; i++) currBranch.branches.Add(new Branch()); // adds 2 layers to  4th layer 3rd branch
        currBranch = currBranch.branches.First();

        for (int i = 0; i < 2; i++) currBranch.branches.Add(new Branch()); // adds 2 layers to 5th layer 1st branch      
    }

    //For context - the tree looks like this
    //          O
    //        / | \
    //       O  O  O
    //      / \  \
    //     O   O  O  
    //    / \      \
    //   O   O      O
    //             / \
    //            O   O
    //           / \
    //          O   O 


    /// <summary>
    /// Method calculates tree depth by finding what is the depth below each node, recursively
    /// </summary>
    /// <param name="branches"></param>
    /// <returns></returns>
    private static int CalculateTreeDepth(Branch branches)
    {
        if (branches == null) return 0;

        List<int> branchDepths = new List<int>();

        foreach (Branch branch in branches.branches)
        {
            branchDepths.Add(CalculateTreeDepth(branch)); //adds all subtree max depths to a list
        }

        if (branches.branches.Count == 0) branchDepths.Add(CalculateTreeDepth(null)); //so that method would enter the null value below bottom nodes
                                                                                      //an alternative would be to add 1 to the returned value in call statement

        return branchDepths.Max() + 1;
    }
    /// <summary>
    /// Class for tree 
    /// </summary>
    public class Branch
    {
        public List<Branch> branches = new List<Branch>();
    }
}