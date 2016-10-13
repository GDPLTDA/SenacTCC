using Pathfinder.Abstraction;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Pathfinder
{
    public class FileTool
    {
        Settings settings;

        public FileTool()
        {
            settings = new Settings();
        }

        public string GetTextRepresentation(IMap map)
        {
            var ret = string.Empty;

            for (int i = 0; i < map.Height; i++)
            {
                for (int j = 0; j < map.Width; j++)
                {
                    char c = ' ';
                    var node = map[i, j];

                    if (node == map.StartNode)
                        c = settings.Start;
                    else if (node == map.EndNode)
                        c = settings.End;
                    else if (!node.Walkable)
                        c = settings.Wall;
                    else
                        c = settings.Empty;

                    ret += c.ToString();
                }
                ret += "\n";
            }

            ret = ret.Remove(ret.LastIndexOf("\n"));
            return ret;
        }

        public IMap ReadMapFromFile(string fileName)
        {

            int width = 0, height = 0;
            var nodes = new List<Node>();
            Node startNode = null;
            Node endNode = null;
            int x = 0, y = 0;
            byte dig;
            

            if (fileName == "")
                throw new Exception("fileName is empty!");

            using (var fs = new FileStream(fileName, FileMode.Open))
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


        public void SaveFileFromMap(IMap map, string filename = "")
        {
            var text = GetTextRepresentation(map);
            var folder = settings.FolderToSaveMaps;

            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);

            var now = DateTime.Now;
            if (string.IsNullOrEmpty(filename))
                filename = $"map_{map.Width}x{map.Height}_{now.Year}{now.Month}{now.Day}_{now.Hour}-{now.Minute}-{now.Second}.txt";
                       
            var newfileName = Path.Combine(folder, filename);

            File.WriteAllText(newfileName, text);

        }

    }
}
