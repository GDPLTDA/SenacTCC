using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace xnafan.MazeCreator
{

    /// <summary>
    /// Class to create random mazes with tiles as walls
    /// Jakob Krarup (www.xnafan.net)
    /// Use, alter and redistribute this code freely,
    /// but please leave this comment :)
    /// </summary>
    public class MazeCreator
    {

        #region Events and related

        /// <summary>
        /// Event is fired whenever a tile is deleted in the maze
        /// </summary>
        public event EventHandler<PointEventArgs> MazeChanged;


        /// <summary>
        /// Called to fire an event whenever a tile is deleted in the maze
        /// </summary>
        /// <param name="p">The Point where the deleted tile was</param>
        protected void OnTileDeleted(Point p)
        {
            if (MazeChanged != null)
            {
                MazeChanged(this, new PointEventArgs(p));
            }
        }

        #endregion


        #region Variables and properties

        /// <summary>
        /// Defines whether all corridors should stay with one tile separation, keeping to neat horizontal and vertical lines
        /// </summary>
        public bool DiagonalTunnelingAllowed { get; private set; }

        /// <summary>
        /// The maze as it looks now. Empty Points are zeroes and walls are ones
        /// </summary>
        public byte[,] Maze { get; private set; }

        //The stack of points not tested yet
        private Stack<Point> _tiletoTry = new Stack<Point>();

        /// <summary>
        /// The list of offsets to use to get the tiles above, below, to the right and the left of a specific tile
        /// </summary>
        private List<Point> NSEW = new List<Point> { new Point(0, 1), new Point(0, -1), new Point(1, 0), new Point(-1, 0) };

        /// <summary>
        /// Used to generate random values
        /// </summary>
        static Random rnd = new Random();

        private int _longestPathSoFar;
        /// <summary>
        /// This variable stores the accessible point which is furthest from the starting point.
        /// This can be used to store the position to place the target object or goal, which will give the biggest challenge for a player
        /// </summary>
        public Point FurthestPoint { get; private set; }

        private int _width, _height;
        private Point _currentTile;

        /// <summary>
        /// The width of the maze
        /// </summary>
        public int Width
        {
            get { return _width; }
            private set
            {
                //must be larger than two
                if (value < 3)
                {
                    throw new ArgumentException("Width must be larger than two for the generator to work");
                }

                _width = value;
            }
        }

        /// <summary>
        /// The height of the maze
        /// </summary>
        public int Height
        {

            get { return _height; }
            private set
            {
                //must be larger than two
                if (value < 3)
                {
                    throw new ArgumentException("Height must be larger than two for the generator to work");
                }

                _height = value;

            }

        }

        /// <summary>
        /// The tile where development of the maze is currently at
        /// </summary>
        public Point CurrentTile
        {
            get { return _currentTile; }
            private set
            {
                if (value.X < 1 || value.X >= this.Width - 1 || value.Y < 1 || value.Y >= this.Height - 1)
                {
                    throw new ArgumentException("CurrentTile must be within the one tile border all around the maze");
                }
                if (value.X % 2 == 1 || value.Y % 2 == 1 || DiagonalTunnelingAllowed)
                { _currentTile = value; }
                else
                {
                    throw new ArgumentException("The current square must not be both on an even X-axis and an even Y-axis when DiagonalTunnelingAllowed is false, to ensure we can get walls around all tunnels");
                }
            }

        }

        #endregion


        #region Constructors

        /// <summary>
        /// Creates a new mazegenerator with mazes starting at (1,1), and no diagonal tunneling.
        /// </summary>
        /// <param name="width">The number of tiles across in the mazes to generate. Must include the two ekstra tiles for a wall in both sides.</param>
        /// <param name="height">The number of tiles from top to bottom in the mazes to generate. Must include the two ekstra tiles for a wall both at top and bottom.</param>
        public MazeCreator(int width, int height) : this(width, height, new Point(1, 1)) { }


        /// <summary>
        /// Creates a new mazegenerator with mazes starting at the requested startingPosition, and no diagonal tunneling.
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="startingPosition"></param>
        public MazeCreator(int width, int height, Point startingPosition) : this(width, height, startingPosition, false){}


        /// <summary>
        /// Creates a new mazegenerator with mazes starting at the requested startingPosition, and no diagonal tunneling.
        /// </summary>
        /// <param name="width">The number of tiles across in the mazes to generate. Must include the two ekstra tiles for a wall in both sides.</param>
        /// <param name="height">The number of tiles from top to bottom in the mazes to generate. Must include the two ekstra tiles for a wall both at top and bottom.</param>
        /// <param name="startingPosition">Where to start tunneling from</param>
        /// <param name="diagonalTunnelingAllowed">Whether the tunneling should allow diagonal routes, e.g. up, left, up, left</param>
        public MazeCreator(int width, int height, Point startingPosition, bool diagonalTunnelingAllowed)
        {

            this.Width = width;
            this.Height = height;

            DiagonalTunnelingAllowed = diagonalTunnelingAllowed;

            Maze = new byte[Width, Height];

            //initialize all fields as taken
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    Maze[x, y] = 1;
                }
            }

            //start the excavation from the current position
            CurrentTile = startingPosition;
            //add the beginning position to the tiles to try
            _tiletoTry.Push(CurrentTile);
        }

        #endregion


        #region CreateMaze

        /// <summary>
        /// Creates a new maze with the current size and starting position
        /// </summary>
        /// <returns>A freshly generated, random maze</returns>
        public byte[,] CreateMaze()
        {
            //local variable to store neighbors to the current square
            //as we work our way through the maze
            List<Point> neighbors;

            //as long as there are still tiles to try
            while (_tiletoTry.Count > 0)
            {
                //excavate the square we are on
                Maze[CurrentTile.X, CurrentTile.Y] = 0;
                //notify anyone interested that the tile was removed
                OnTileDeleted(CurrentTile);

                //get all valid neighbors for the new tile  
                neighbors = GetValidNeighbors(CurrentTile);

                //if there are any interesting looking neighbors
                if (neighbors.Count > 0)
                {
                    //remember this tile, by putting it on the stack
                    _tiletoTry.Push(CurrentTile);
                    //move on to a random of the neighboring tiles
                    CurrentTile = neighbors[rnd.Next(neighbors.Count)];
                }
                else
                {
                    //if there were no neighbors to try, we are at a dead-end
                    //test to see if this dead end will be the furthest point in the maze
                    UpdateFurthestPoint();
                    //toss this tile out 
                    //(thereby returning to a previous tile in the list to check).
                    CurrentTile = _tiletoTry.Pop();
                }
            }

            return Maze;
        }

        private void UpdateFurthestPoint()
        {
            if (_tiletoTry.Count > _longestPathSoFar)
            {
                _longestPathSoFar = _tiletoTry.Count;
                FurthestPoint = CurrentTile;
            }
        }

        #endregion


        #region Helpermethods

        /// <summary>
        /// Get all the prospective neighboring tiles
        /// </summary>
        /// <param name="centerTile">The tile to test</param>
        /// <returns>All and any valid neighbors</returns>
        private List<Point> GetValidNeighbors(Point centerTile)
        {

            List<Point> validNeighbors = new List<Point>();

            //Check all four directions around the tile
            foreach (var offset in NSEW)
            {
                //find the neighbor's position
                Point toCheck = new Point(centerTile.X + offset.X, centerTile.Y + offset.Y);

                //make sure the tile is not on both an even X-axis and an even Y-axis
                //to ensure we can get walls around all tunnels
                if (toCheck.X % 2 == 1 || toCheck.Y % 2 == 1 || DiagonalTunnelingAllowed)
                {
                    //if the potential neighbor is unexcavated (==1)
                    //and still has three walls intact (new territory)
                    if (Maze[toCheck.X, toCheck.Y] == 1 && HasThreeWallsIntact(toCheck))
                    {
                        //add the neighbor
                        validNeighbors.Add(toCheck);
                    }
                }
            }

            return validNeighbors;
        }


        /// <summary>
        /// Counts the number of intact walls around a tile
        /// </summary>
        /// <param name="pointToCheck">The coordinates of the tile to check</param>
        /// <returns>Whether there are three intact walls (the tile has not been dug into earlier.</returns>
        private bool HasThreeWallsIntact(Point pointToCheck)
        {
            int intactWallCounter = 0;

            //Check all four directions around the tile
            foreach (var offset in NSEW)
            {
                //find the neighbor's position
                Point neighborToCheck = new Point(pointToCheck.X + offset.X, pointToCheck.Y + offset.Y);

                //make sure it is inside the maze, and it hasn't been dug out yet
                if (IsInside(neighborToCheck) && Maze[neighborToCheck.X, neighborToCheck.Y] == 1)
                {
                    intactWallCounter++;
                }
            }

            //tell whether three walls are intact
            return intactWallCounter == 3;

        }

        /// <summary>
        /// Find out whether a tile is inside the maze
        /// </summary>
        /// <param name="p">The coordinates of the tile to check</param>
        /// <returns>Whether the tile is inside the maze</returns>
        private bool IsInside(Point p)
        {
            return p.X >= 0 && p.Y >= 0 && p.X < Width && p.Y < Height;
        }

        #endregion


        #region ToString
		
        /// <summary>
        /// Returns a textmap of the maze.
        /// </summary>
        /// <returns>A textual representation of the maze</returns>
        public override string ToString()
        {
            string representation = " ";
            StringBuilder sb = new StringBuilder();
            for (int y = Height - 1; y>= 0 ; y--)
            {
                for (int x = 0; x < Width; x++)
                {
                    if (Maze[x, y] == 0)
                    {
                        //if it is the current tile, represent it with an "O"
                        if (CurrentTile.X == x && CurrentTile.Y == y)
                        {
                            representation = "O";
                        }
                        else
                        {
                            //if it is still one to test
                            if (_tiletoTry.Contains(new Point(x, y)))
                            {
                                representation = ".";
                            }
                            else
                            {
                                //been there - nothing to see
                                representation = " ";
                            }
                            if (FurthestPoint.X == x && FurthestPoint.Y == y)
                            {
                                representation = "*";
                            }
                        }
                    }
                    else
                    //it is unexcavated (wall)
                    {
                        representation = "X";
                    }
                    sb.Append(representation);
                }
                sb.AppendLine();
            }
            return sb.ToString();
        }

	    #endregion    


        #region ToBitmap

        public Bitmap ConvertToBitmap(int tileSizeInPixels)
        {
            //create a new bitmap with a pixel extra for each tile +1 for drawing a grid around all tiles
            Bitmap bmp = new Bitmap(1 + (tileSizeInPixels + 1) * this.Width, 1 + (tileSizeInPixels + 1) * this.Height);
            Graphics g = Graphics.FromImage(bmp);

            //fill the grapics with grey
            g.FillRectangle(Brushes.DarkGray, new Rectangle(0, 0, bmp.Width, bmp.Height));

            //get the current maze as a string, and replace any newlines
            string mazeString = this.ToString().Replace(Environment.NewLine, "");

            //store a fillBrush as white
            Brush fillBrush = Brushes.White;

            //for each tile, get a brush that is the correct color
            for (int y = 0; y < this.Height; y++)
            {
                for (int x = 0; x < this.Width; x++)
                {

                    switch (mazeString[x + y * this.Width])
                    {

                        case 'X': fillBrush = Brushes.Black;
                            break;
                        case 'O':
                            fillBrush = Brushes.Red;
                            break;
                        case '.':
                            fillBrush = Brushes.CornflowerBlue;
                            break;
                        case ' ':
                            fillBrush = Brushes.White;
                            break;
                        case '*':
                            fillBrush = Brushes.LightGreen;
                            break;
                    }
                    //draw the tile in
                    g.FillRectangle(fillBrush, 1 + x * (tileSizeInPixels + 1), 1 + y * (tileSizeInPixels + 1), tileSizeInPixels, tileSizeInPixels);

                }
            }
            //dispose of the Graphics object
            g.Dispose();
            return bmp;
        }

        #endregion

    }
}
