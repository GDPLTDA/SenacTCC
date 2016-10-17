using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Pathfinder.Constants;
using Pathfinder.Abstraction;
using System.Reflection;

namespace Pathfinder
{
    public class Genome : IGenome
    {
        public IMap Map { get; set; }
        public List<Node> ListNodes { get; set; }
        public double Fitness { get; set; }

        public Genome()
        {
        }

        public Genome(IGenome genome)
        {
            Map = genome.Map;
            ListNodes = Copy(genome.ListNodes);
        }

        public Genome(IMap map)
        {
            Map = map;
            ListNodes = RouteFinding();
        }

        public List<Node> RouteFinding()
        {
            var listnode = new List<Node>();
            bool run = true;
            var node = new Node(Map.StartNode);

            while (run)
            {
                // verfica se não está voltando para o mesmo no anterior
                if (!listnode.Exists(o => o.X == node.X && o.Y == node.Y))
                    listnode.Add(node);

                var dir = Settings.Random.Next(1, Enum.GetNames(typeof(DirectionMovement)).Length);
                var newnode = Map.GetDirectionNode(node, (DirectionMovement)dir);

                // verifica se teve colisão ou se encontrou o fim
                run = newnode != null;

                if (newnode != null)
                {
                    node = new Node(newnode, node);
                }
            }

            return listnode;
        }

        public bool IsEqual(IGenome genome)
        {
            if (ListNodes.Count != genome.ListNodes.Count)
                return false;

            for (int i = 0; i < ListNodes.Count; i++)
            {
                if (ListNodes[i] != genome.ListNodes[i])
                    return false;
            }
            return true;
        }

        private List<Node> Copy(List<Node> listnode)
        {
            var returnnode = new List<Node>();

            foreach (var item in listnode)
                returnnode.Add(new Node(item));

            return returnnode;
        }
    }
}
