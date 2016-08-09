using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Gif.Components;
using xnafan.MazeCreator;
using System.Drawing;
using System.Diagnostics;

namespace TestingMazeCreator
{

    /// <summary>
    /// Class to demonstrate creating random mazes with the MazeCreator
    /// Jakob Krarup (www.xnafan.net)
    /// Use, alter and redistribute this code freely,
    /// but please leave this comment :)
    /// </summary>

    class Program
    {

        private static MazeCreator _creator;            //the MazeCreator
        private static AnimatedGifEncoder _gifEncoder;  //the GIF encoder

        static void Main(string[] args)
        {
            //create mazes of sizes 11 through 22
            for (int size = 10; size < 12; size++)
            {

                //make a new mazecreator
                _creator = new MazeCreator(size, size, new Point(1, 1));

                //figure out a savepath with a logical name containing width and height
                string gifPath = string.Format("c:\\teste\\maze_{0:00}x{0:00}.gif", size);

                //output status to console
                Console.WriteLine("Creating " + gifPath);

                //subscribe to the mazechanged event
                //to get a fresh bitmap on every new tile excavated
                _creator.MazeChanged += new EventHandler<PointEventArgs>(CreatorMazeChanged);

                //create the animated GIF
                _gifEncoder = new AnimatedGifEncoder();
                _gifEncoder.SetDelay(100);  //in milliseconds
                _gifEncoder.SetRepeat(0);   //yes - repeat (-1 is NO)
                _gifEncoder.SetQuality(1); //for generating palette - 1 is best, but slowest
                _gifEncoder.Start(gifPath); //where to save the maze
                var maze = _creator.CreateMaze();   //create a maze
                _gifEncoder.Finish();               //end

                //show the animated gif    
                Process.Start(gifPath);
                Console.WriteLine("Furthest point: " + _creator.FurthestPoint);
            }

            Console.WriteLine("Enter to exit!");
            Console.ReadLine();
        }


        static void CreatorMazeChanged(object sender, PointEventArgs e)
        {
            //get a Bitmap representation of the maze with each tile taking up six pixels
            Bitmap bmp = ((MazeCreator)sender).ConvertToBitmap(6);
            //add it as the next image to the encoder
            _gifEncoder.AddFrame(bmp);

            //output the maze as string
            Console.WriteLine(((MazeCreator)sender).ToString());
            Console.WriteLine("Enter to continue processing maze...");

            //wait for input
            Console.ReadLine();
        }
    }
}
