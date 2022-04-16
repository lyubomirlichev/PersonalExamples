using System.Collections.Generic;

//my solution to https://www.codewars.com/kata/52a89c2ea8ddc5547a000863
public class KataLoop
{
    public static int GetLoopSize(Node startNode)
    {
        Dictionary<Node, int> dPrevNodes = new Dictionary<Node, int>();

        int steps = 0;

        Node nextNode = startNode;

        while (!dPrevNodes.ContainsKey(nextNode))
        {
            dPrevNodes.Add(nextNode, steps);
            nextNode = nextNode.next;
            steps++;
        }

        return dPrevNodes.Count - dPrevNodes[nextNode];
    }
}

public class Node
{
    private Node next;

    public Node GetNExt()
    {
        return this.next;
    }
}