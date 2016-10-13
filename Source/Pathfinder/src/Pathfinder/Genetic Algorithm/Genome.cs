using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Pathfinder.Constants;
using Pathfinder.Abstraction;

namespace Pathfinder
{
    public class Genome
    {
        public List<Node> ListNodes { get; set; }
        public double Fitness { get; set; }
        public Genome(IMap map)
        {
            ListNodes = RouteFinding(map);
        }

        public List<Node> RouteFinding(IMap map)
        {
            var listnode = new List<Node>();
            bool run = true;
            var node = map.StartNode;

            while (run)
            {
                // verfica se não está voltando para o mesmo no anterior
                if (!listnode.Exists(o => o.X == node.X && o.Y == node.Y))
                    listnode.Add(node);

                var dir = Settings.Random.Next(1, Enum.GetNames(typeof(DirectionMovement)).Length);
                var newnode = map.GetDirectionNode(node, (DirectionMovement)dir);

                // verifica se teve colisão ou se encontrou o fim
                run = newnode != null;

                if (newnode != null)
                {
                    newnode.ParentNode = node;
                    node = newnode;
                }
            }

            return listnode;
        }
    }
}
