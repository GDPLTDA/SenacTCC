using Pathfinder.Abstraction;
using Pathfinder.Constants;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Pathfinder
{
    public class FileTool
    {
        

        public FileTool()
        {
            
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
                        c = Settings.Start;
                    else if (node == map.EndNode)
                        c = Settings.End;
                    else if (!node.Walkable)
                        c = Settings.Wall;
                    else
                        c = Settings.Empty;

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
            DiagonalMovement? d = null;

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
                    
                    if (chrDig == '?')
                    {
                        var line = new List<char>();
                        while (chrDig!=10)
                            line.Add(chrDig = reader.ReadChar());

                        ReadMapSettings(string.Join("",line), out d);
                        continue;
                    }

                    if (chrDig == Settings.Start)
                        startNode = new Node(x, y);
                    else
                    if (chrDig == Settings.End)
                        endNode = new Node(x, y);
                    else
                    if (chrDig == Settings.Wall)
                        nodes.Add(new Node(x, y) { Walkable = false });
                    else
                    if (chrDig == Settings.Empty)
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
            ret.AllowDiagonal = d;
            ret.DefineAllNodes(nodes);
            ret.DefineNode(ret.StartNode);
            ret.DefineNode(ret.EndNode);

            if (!ret.ValidMap())
                throw new Exception("Invalid map configuration");

            return ret;
        }

        private void ReadMapSettings(string line, out DiagonalMovement? d)
        {
            var diagvar = "diagonal=";
            d = null;

            if (line.Contains(diagvar))
            {
                string diag = line.Substring(line.IndexOf(diagvar)+ diagvar.Length);
                diag = diag.Substring(0, diag.IndexOf(";"));

                var diags = Enum.GetValues(typeof(DiagonalMovement));
                for (int i = 0; i < diags.Length; i++)
                    if (diag.Contains(((DiagonalMovement)diags.GetValue(i)).ToString()))
                    {
                        d = (DiagonalMovement)diags.GetValue(i);
                    }
            }
        }


        public void SaveFileFromMap(IMap map, string filename = "")
        {
            var text = GetTextRepresentation(map);
            var folder = Settings.FolderToSaveMaps;

            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);

            var now = DateTime.Now;
            if (string.IsNullOrEmpty(filename))
            {
                filename = $"map_{map.Width}x{map.Height}_{now.Year}{now.Month}{now.Day}_{now.Hour}-{now.Minute}-{now.Second}.txt";
                filename = Path.Combine(folder, filename);
            }

            if (map.AllowDiagonal!=null)
                text = $"?diagonal={map.AllowDiagonal};\n{text}";

            File.WriteAllText(filename, text);

        }

    }
}
