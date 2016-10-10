using Pathfinder.Abstraction;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Pathfinder.MapGenerators
{
    public class FileMapGenerator : IMapGenerator
    {
        public IMap ReadMap(string argument)
        {
            int width = 0, height = 0;
            var nodes = new List<Node>();
            Node startNode = null;
            Node endNode = null;
            int x = 0, y = 0;
            byte dig;
            var settings = new Settings();

            if (argument == "")
                argument = "test.txt";

            using (var fs = new FileStream(argument, FileMode.Open))
            {
                var reader = new BinaryReader(fs);

                while (!(reader.BaseStream.Position == reader.BaseStream.Length))
                {
                    dig = reader.ReadByte();

                    if (dig == 13)
                        continue;

                    if (dig == 10)
                    {
                        y++;
                        x = 0;
                        continue;
                    }

                    var chrDig = (char)dig;

                    if (chrDig == settings.Start)
                        startNode = new Node(x, y);
                    else
                    if (chrDig == settings.End)
                        endNode = new Node(x, y);
                    else
                    if (chrDig == settings.Wall)
                        nodes.Add(new Node(x, y) { Walkable = false });
                    else
                    if (chrDig == settings.Empty)
                        nodes.Add(new Node(x, y) { Walkable = true });
                    else
                        throw new Exception("invalid character " + chrDig.ToString());

                    x++;
                }
                y++;


            }

            width = x;
            height = y;

            var ret = new Map(width, height);

            ret.StartNode = startNode;
            ret.EndNode = endNode;

            ret.DefineAllNodes(nodes);
            ret.DefineNode(ret.StartNode);
            ret.DefineNode(ret.EndNode);

            if (!ret.ValidMap())
                throw new Exception("Invalid map configuration");

            return ret;
            
        }
    }
}