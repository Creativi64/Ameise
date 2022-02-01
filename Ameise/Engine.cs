using System;
using System.Collections.Generic;
using System.Drawing;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace Ameise
{
    public class Engine
    {
        public static Graphics FeldGrafik = null;

        public static int Draws = 0;

        [Serializable]
        public class GameNotInitializedException : Exception
        {
            public GameNotInitializedException()
            { }

            public GameNotInitializedException(string message) : base(message)
            {
            }

            public GameNotInitializedException(string message, Exception inner) : base(message, inner)
            {
            }

            protected GameNotInitializedException(
              System.Runtime.Serialization.SerializationInfo info,
              System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
        }

        public class TilesBitmaps
        {
            public Bitmap FeldVoll;

            public Bitmap FeldLeer;

            public Bitmap FeldX;

            //public Bitmap FeldPath;

            //public Bitmap FeldStart;

            //public Bitmap FeldZiel;

            public Bitmap Item;

            public Bitmap Scan;

            public Bitmap Nest;

            public Bitmap Ameise;

            public TilesBitmaps(int X, int Y)
            {
                //Felder
                FeldVoll = new Bitmap(X, Y);
                Graphics G_FeldVoll = Graphics.FromImage(FeldVoll);
                G_FeldVoll.SmoothingMode = Game.GraficMode;
                G_FeldVoll.FillRectangle(Brushes.Black, new Rectangle(0, 0, X, Y));

                FeldLeer = new Bitmap(X, Y);
                Graphics G_FeldLeer = Graphics.FromImage(FeldLeer);
                G_FeldLeer.SmoothingMode = Game.GraficMode;
                Pen pen = new(Color.Black, 2);
                pen.Alignment = System.Drawing.Drawing2D.PenAlignment.Inset;
                pen.EndCap = System.Drawing.Drawing2D.LineCap.Flat;
                pen.LineJoin = System.Drawing.Drawing2D.LineJoin.Miter;
                G_FeldLeer.FillRectangle(Brushes.White, new Rectangle(0, 0, X, Y));
                G_FeldLeer.DrawRectangle(pen, new Rectangle(0, 0, X, Y));

                FeldX = new Bitmap(X, Y);
                Graphics G_FeldX = Graphics.FromImage(FeldX);
                G_FeldX.SmoothingMode = Game.GraficMode;
                G_FeldX.FillRectangle(Brushes.Red, new Rectangle(0, 0, X, Y));

                //FeldPath = new Bitmap(X, Y);
                //Graphics G_FeldPath = Graphics.FromImage(FeldPath);
                //G_FeldPath.SmoothingMode = Game.GraficMode;
                //G_FeldPath.FillRectangle(Brushes.Blue, new Rectangle(0, 0, X, Y));

                //FeldStart = new Bitmap(X, Y);
                //Graphics G_FeldStart = Graphics.FromImage(FeldStart);
                //G_FeldStart.SmoothingMode = Game.GraficMode;
                //G_FeldStart.FillRectangle(Brushes.Green, new Rectangle(0, 0, X, Y));

                //FeldZiel = new Bitmap(X, Y);
                //Graphics G_FeldZiel = Graphics.FromImage(FeldZiel);
                //G_FeldZiel.SmoothingMode = Game.GraficMode;
                //G_FeldZiel.FillRectangle(Brushes.Yellow, new Rectangle(0, 0, X, Y));

                Item = new Bitmap(X, Y);
                Graphics G_Item = Graphics.FromImage(Item);
                G_Item.SmoothingMode = Game.GraficMode;
                G_Item.FillEllipse(Brushes.Cyan, new Rectangle(X / 4, Y / 4, X / 2, Y / 2));

                Scan = new Bitmap(X, Y);
                Graphics G_Scan = Graphics.FromImage(Scan);
                G_Scan.SmoothingMode = Game.GraficMode;
                G_Scan.DrawEllipse(Pens.Cyan, new Rectangle(X / 8, Y / 8, X - 2 * (X / 8) - 1/*Abstand*/, Y - 2 * (Y / 8) - 1/*Abstand*/));

                Ameise = new Bitmap(X, Y);
                Graphics G_Ameise = Graphics.FromImage(Ameise);
                G_Ameise.SmoothingMode = Game.GraficMode;
                pen = new(Color.Brown, 2);
                Pen pen2 = new(Color.Black, 1);
                int b = 3;
                G_Ameise.DrawLine(pen, new Point(0 + b, 0 + b), new Point(X / 3, Y / 3));
                G_Ameise.DrawLine(pen, new Point(X - b, Y - b), new Point(X - X / 3, Y - Y / 3));
                G_Ameise.DrawLine(pen, new Point(X / 3, Y - Y / 3), new Point(0 + b, Y - b));
                G_Ameise.DrawLine(pen, new Point(X - b, 0 + b), new Point(X - X / 3, Y / 3));
                G_Ameise.DrawEllipse(pen, new Rectangle(X / 4, Y / 4, X / 2, Y / 2));
                G_Ameise.DrawLine(pen2, new Point(X, Y / 2), new Point(X - X / 3, Y / 2));

                Nest = new Bitmap(X, Y);
                Graphics G_Nest = Graphics.FromImage(Nest);
                G_Nest.SmoothingMode = Game.GraficMode;
                G_Nest.DrawEllipse(Pens.BurlyWood, new Rectangle(X / 4, Y / 4, X / 2, Y / 2));

                G_Nest.DrawEllipse(Pens.BurlyWood, new Rectangle(X / 3, Y / 5, X / 2, Y / 2));
                G_Nest.DrawEllipse(Pens.BurlyWood, new Rectangle(X / 5, Y / 3, X / 2, Y / 2));

                G_Nest.DrawEllipse(Pens.BurlyWood, new Rectangle(X / 2 - X / 5, Y / 3, X / 2, Y / 2));
                G_Nest.DrawEllipse(Pens.BurlyWood, new Rectangle((X / 3), Y / 2 - Y / 5, X / 2, Y / 2));

                G_Nest.DrawEllipse(Pens.BurlyWood, new Rectangle(X / 3 - X / 6, Y / 6, X / 2, Y / 2));
                G_Nest.DrawEllipse(Pens.BurlyWood, new Rectangle((X / 6), Y / 3 - Y / 6, X / 2, Y / 2));

                G_Ameise.Dispose();
                G_FeldLeer.Dispose();
                //G_FeldPath.Dispose();
                //G_FeldStart.Dispose();
                //G_FeldZiel.Dispose();
                G_FeldVoll.Dispose();
                G_FeldX.Dispose();

                G_Item.Dispose();
                G_Scan.Dispose();
                G_Nest.Dispose();
                pen.Dispose();
                pen2.Dispose();
            }
        }

        public static bool Marks;

        public static bool Scan;

        public static void Draw(Vector2 vec, FieldState fls)
        {
            Game.Feld[(int)vec.X][(int)vec.Y].State = fls;
            Draw();
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static void Draw(bool Optimesed = true)
        {
            if (FeldGrafik == null)
            {
                throw new GameNotInitializedException("Game Was not initilized, try Game.Init() to fix");
            }

            TilesBitmaps til = new TilesBitmaps(Game.Height_X, Game.Height_Y);

            int width = Game.Feld[^1][^1].PosRUnten.X;
            int height = Game.Feld[^1][^1].PosRUnten.Y;
            Bitmap Feld = new(width, height);

            Graphics G_Feld = Graphics.FromImage(Feld);

            G_Feld.SmoothingMode = Game.GraficMode;

            FeldGrafik.DrawImage(til.FeldVoll, new Point(-40, 50));
            FeldGrafik.DrawImage(til.FeldLeer, new Point(-40, 100));
            FeldGrafik.DrawImage(til.FeldX, new Point(-40, 150));
            //Game.GesamtFeld.DrawImage(til.FeldPath, new Point(-40, 200));
            //Game.GesamtFeld.DrawImage(til.FeldZiel, new Point(-40, 250));
            //Game.GesamtFeld.DrawImage(til.FeldStart, new Point(-40, 300));

            for (int i = 0; i < Game.Feld.Count; i++)
            {
                for (int a = 0; a < Game.Feld[i].Count; a++)
                {
                    switch (Game.Feld[a][i].State)
                    {
                        case FieldState.Blocked:
                        case FieldState.Wakable:

                            G_Feld.DrawImage(til.FeldLeer, Game.Feld[a][i].PosLOben);
                            if (Game.Feld[a][i].Item.Count != 0)
                            {
                                G_Feld.DrawImage(til.Item, Game.Feld[a][i].PosLOben);
                            }
                            if (Game.Feld[a][i].Nest != null)
                            {
                                Bitmap b = new(til.Ameise.Width, til.Ameise.Height);
                                Graphics gr = Graphics.FromImage(b);
                                gr.SmoothingMode = Game.GraficMode;

                                gr.DrawImage(til.Nest, new Point(0, 0));

                                SolidBrush p = new(Game.Feld[a][i].Nest.Team);

                                float ofset = (float)3 / (float)8;

                                gr.FillEllipse(p, new RectangleF((float)Game.Height_X * (float)ofset, (float)Game.Height_Y * (float)ofset, Game.Height_X / 4, Game.Height_Y / 4));
                                G_Feld.DrawImage(b, Game.Feld[a][i].PosLOben);
                            }
                            if (Game.Feld[a][i].Ameis != null)
                            {
                                Bitmap b = new(til.Ameise.Width, til.Ameise.Height);

                                Graphics gr = Graphics.FromImage(b);
                                gr.SmoothingMode = Game.GraficMode;
                                gr.TranslateTransform((float)til.Ameise.Width / 2, (float)til.Ameise.Height / 2);
                                gr.RotateTransform(Game.Feld[a][i].Ameis.Rotation);
                                gr.TranslateTransform(-(float)til.Ameise.Width / 2, -(float)til.Ameise.Height / 2);

                                gr.DrawImage(til.Ameise, new Point(0, 0));
                                SolidBrush p = new(Game.Feld[a][i].Ameis.Team);

                                float ofset = (float)3 / (float)8;

                                gr.FillEllipse(p, new RectangleF((float)Game.Height_X * (float)ofset, (float)Game.Height_Y * (float)ofset, Game.Height_X / 4, Game.Height_Y / 4));

                                G_Feld.DrawImage(b, Game.Feld[a][i].PosLOben);
                                b.Dispose();
                                p.Dispose();
                                gr.Dispose();
                            }
                            if (Engine.Scan)
                            {
                                if (Game.Feld[a][i].Scanned == true)
                                {
                                    G_Feld.DrawImage(til.Scan, Game.Feld[a][i].PosLOben);
                                }
                            }
                            if (Engine.Marks)
                            {
                                SolidBrush sbr;
                                if (Game.Feld[a][i].Marks[0].Wert * 10 <= 255)
                                {
                                    sbr = new(Color.FromArgb(255 - 10 * Game.Feld[a][i].Marks[0].Wert, 0, 0));
                                }
                                else
                                {
                                    sbr = new(Color.FromArgb(0, 0, 0));
                                }

                                G_Feld.DrawString(Game.Feld[a][i].Marks[0].Wert.ToString(), new Font(FontFamily.GenericMonospace, 10), sbr, Game.Feld[a][i].PosLOben);
                                sbr.Dispose();
                            }

                            break;

                        case FieldState.NotWakable:

                            G_Feld.DrawImage(til.FeldVoll, Game.Feld[a][i].PosLOben);
                            if (Game.Feld[a][i].Item.Count != 0)
                            {
                                G_Feld.DrawImage(til.Item, Game.Feld[a][i].PosLOben);
                            }
                            if (Game.Feld[a][i].Scanned == true)
                            {
                                G_Feld.DrawImage(til.Scan, Game.Feld[a][i].PosLOben);
                            }
                            if (Game.Feld[a][i].Ameis != null)
                            {
                                G_Feld.DrawImage(til.Ameise, Game.Feld[a][i].PosLOben);
                            }
                            break;

                        default:

                            G_Feld.DrawImage(til.FeldX, Game.Feld[a][i].PosLOben);
                            if (Game.Feld[a][i].Item.Count != 0)
                            {
                                G_Feld.DrawImage(til.Item, Game.Feld[a][i].PosLOben);
                            }
                            if (Game.Feld[a][i].Scanned == true)
                            {
                                G_Feld.DrawImage(til.Scan, Game.Feld[a][i].PosLOben);
                            }
                            if (Game.Feld[a][i].Ameis != null)
                            {
                                G_Feld.DrawImage(til.Ameise, Game.Feld[a][i].PosLOben);
                            }
                            break;
                    }
                }
            }

            FeldGrafik.DrawImage(Feld, 0, 0);
            Feld.Dispose();
            G_Feld.Dispose();
            Draws++;
            //Console.WriteLine(Draws);
            //GC.Collect();
        }
    }
}