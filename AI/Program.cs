using AI.Messages;
using AI.Messages.ToExplorer;
using AI.Nodes;
using System;
using System.Threading;

namespace AI
{
    public class TestObj
    {
        public int Id { get; set; }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            SequenceNode node = new SequenceNode();
            var childNode = new ParallelNode();
            node.Children.Add(childNode);
            node.Children.Add(new LeafNode());
            node.Children.Add(new LeafNode());
            node.Children.Add(new LeafNode());
            childNode.Children.Add(new LeafNode());
            childNode.Children.Add(new LeafNode());
            childNode.Children.Add(new LeafNode());

            AIRunner runner = new AIRunner(new TestObj());
            runner.Initialize(node);
            for (int i = 0; i < 10; i ++)
            {
                runner.Run(50);
                Thread.Sleep(250);
                ////Console.WriteLine($"{i} th iteration");
            }
        }
    }
}
