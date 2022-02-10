using System;
using System.Drawing.Drawing2D;
using AmeisenGame;
using System.Drawing;
using System.Drawing.Imaging;
using System.ComponentModel;
using System.Threading;
using System.Collections.Generic;

using System.Numerics;
using System.Runtime.CompilerServices;
using BenchmarkDotNet.Running;
using BenchmarkDotNet.Attributes;

namespace TestConsole
{
    public class Program
    {
        public static Guid AktiveAmeise { get; set; }

        public static Guid AktivesNest { get; set; }


        public static BackgroundWorker bw;

        private static void Main(string[] args)
        {
            var summary = BenchmarkRunner.Run<Program>();
            Console.WriteLine("Hello World!");
        }

        [Benchmark]
        public void start()
        {
            var bmp = new Bitmap(700, 700);
            var gr = Graphics.FromImage(bmp);

            Game.init(gr, 700, 700, SmoothingMode.HighQuality, true, true, 3);

            AktivesNest = Game.Nester[0].Idenifier;
            AktiveAmeise = Nest.peekFirstAmeiseFromNest(AktivesNest).Idenifier;
            Game.Nester[Nest.getActiveNest(AktivesNest)].depolyAmeiseFromNest(AktivesNest);

            //var path = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyPictures), ($"Example{Engine.Draws}.png"));
            //bmp.Save(path);

            bw = new BackgroundWorker();
            bw.WorkerSupportsCancellation = true;
            Nest.getAmeiseInField(AktiveAmeise).StartCollect(bw);
            Thread.Sleep(500);
            bw.CancelAsync();

            //Engine.Draw();
            //var path1 = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyPictures), ($"Example{Engine.Draws}.png"));
            //bmp.Save(path1);
            //Console.ReadLine();
        }
    }
}