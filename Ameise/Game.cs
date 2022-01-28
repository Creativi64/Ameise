using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using AStarSharp;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.CompilerServices;

namespace Ameise
{
    public class Game
    {
        public static readonly bool FieldInitialized = false;

        // Needet ----------------

        public static readonly List<List<Tile>> Feld;

        // Display Parameter ---------

        public static int StartY;

        public static int StartX;

        public static readonly int Scaling = 1;

        public static readonly int Abstand = (1 * Scaling);

        public static readonly int Height_Y = (25 + Abstand) * Scaling;

        public static readonly int Height_X = Height_Y;

        public static readonly int With = 20;

        public static readonly int Height = With;

        public static readonly int MaxHeight_Y = With * (Height_Y + Abstand);

        public static readonly int MaxHeight_X = Height * (Height_X + Abstand);

        public static SmoothingMode GraficMode = SmoothingMode.Default;

        // Item ---------------------
        public static int Items;

        private static Stack<Point> notwa = new();

        private static Stack<Point> items = new();

        public static List<Nest> Nester = new List<Nest>();

       

        // Init -----------------
        private static readonly Color[] p = { Color.Red };

        /// <summary>
        /// NEED TO be done to WORK
        /// </summary>
        /// <param name="FeldGrafik">A grafic that should be drawn on</param>
        /// <param name="With">The With of the Form</param>
        /// <param name="Height">The Height of the Form</param>
        /// <param name="GraficMode">The Render Quality</param>
        public static void init(Graphics FeldGrafik, int With, int Height, SmoothingMode GraficMode, bool PlaceBlocksItems = true, bool genNest = true, int ErstelleteAmeisen = 1, int posNestX = 0, int posNestY = 0)
        {
            Engine.FeldGrafik = FeldGrafik;

            FeldGrafik.SmoothingMode = GraficMode;
            StartX = ((With - Height_X * Game.With) / 2);
            StartY = ((Height - Height_Y * Game.Height) / 2);
            FeldGrafik.TranslateTransform(StartX, StartY);

            Game.GraficMode = GraficMode;

            if (PlaceBlocksItems)
            {
                PlaceBocksItems();
            }

            Feld[posNestX][posNestY].State = FieldState.wakable;

            if (genNest)
            {
                foreach (var item in p)
                {
                    Nester.Add(new Nest(new Vector2(posNestX, posNestY), item, ErstelleteAmeisen));
                    Feld[posNestX][posNestY].State = FieldState.wakable;
                    Feld[posNestX][posNestY].Nest = Nester[Nester.Count - 1];
                }
            }

            //Engine observable = new Engine();
            //Observer observer = new Observer();
            //observable.SomethingHappened += observer.HandleEvent;

            //observable.DoSomething();
        }

        static Game()
        {
            Feld = new List<List<Tile>>();

            for (int i = 0; i < Game.With; i++)
            {
                Feld.Add(new List<Tile>());
                for (int a = 0; a < Height; a++)
                {
                    Feld[i].Add(new Tile(new Point(i * (Height_X + Abstand), a * (Height_Y + Abstand)), Height_X, Height_Y, Abstand, FieldState.wakable));
                }
            }
            StartX = 100;
            StartY = 100;
            FieldInitialized = true;
        }

        // Funktions ------------

        public static void ResetGame()
        {
            //RemoveBlocksItems();
            for (int i = 0; i < Feld.Count; i++)
            {
                for (int a = 0; a < Feld[i].Count; a++)
                {
                    Feld[a][i].Item.Clear();
                    Feld[a][i].State = FieldState.wakable;
                }
            }
            PlaceBocksItems();

            Feld[0][0].State = FieldState.wakable;
        }

        public static void RemoveBlocksItems()
        {
            for (int i = 0; i < Feld.Count; i++)
            {
                for (int a = 0; a < Feld[i].Count; a++)
                {
                    Feld[i][a].Item.Clear();
                    Feld[i][a].State = FieldState.wakable;
                }
            }
            Engine.Draw();
        }

        public static void HandelResizeEvent(Graphics GesamtFeld, int With, int Height, Color BackgroundColor)
        {
            if (Engine.FeldGrafik == null)
            {
                Engine.FeldGrafik = GesamtFeld;
            }
            StartX = ((With - Height_X * Game.With) / 2);
            StartY = ((Height - Height_Y * Game.Height) / 2);

            Engine.FeldGrafik.Clear(BackgroundColor);

            Engine.FeldGrafik = GesamtFeld;
            Engine.FeldGrafik.TranslateTransform(StartX, StartY);
            Engine.FeldGrafik.SmoothingMode = SmoothingMode.HighQuality;
            Engine.Draw();
        }

        public static void SetFieldState(int X, int Y, FieldState State)
        {
            Feld[X][X].State = State;
        }

        public static void UnsetScan()
        {
            for (int i = 0; i < Feld.Count; i++)
            {
                for (int a = 0; a < Feld[i].Count; a++)
                {
                    Feld[i][a].Scanned = false;
                }
            }
            Engine.Draw();
        }

        public static void PlaceBocksItems()
        {
            //notwa.Push(new Point( HORIZINTAL,VERTIKAL));
            Random rnd = new();

            for (int c = 0; c < 6; c++)
            {
                int[] RandomX_Block = new int[With];
                int[] RandomY_Block = new int[Height];
                for (int i = 0; i < Feld.Count; i++)
                {
                    RandomX_Block[i] = rnd.Next(0, With);
                }

                for (int i = 0; i < Feld.Count; i++)
                {
                    RandomY_Block[i] = rnd.Next(0, Height);
                }

                for (int i = 0; i < With; i++)
                {
                    notwa.Push(new Point(RandomX_Block[i], RandomY_Block[i]));
                }
            }

            for (int c = 0; c < 1; c++)
            {
                int[] RandomX_Item = new int[With];
                int[] RandomY_Item = new int[Height];
                for (int i = 0; i < Feld.Count; i++)
                {
                    RandomX_Item[i] = rnd.Next(0, With);
                }

                for (int i = 0; i < Feld.Count; i++)
                {
                    RandomY_Item[i] = rnd.Next(0, Height);
                }

                for (int i = 0; i < With; i++)
                {
                    items.Push(new Point(RandomX_Item[i], RandomY_Item[i]));
                }
            }

            foreach (var item in notwa)
            {
                Feld[item.X][item.Y].State = FieldState.notWakable;
            }
            int ii = 0;
            foreach (var item in items)
            {
                if (Feld[item.X][item.Y].State != FieldState.notWakable)
                {
                    Feld[item.X][item.Y].Item.Push(new Item(new Vector2(item.X, item.Y), ii.ToString()));
                    ii++;
                }
            }
            Items = ii;
            notwa.Clear();
            items.Clear();
        }

        public static List<List<Node>> ConstructGrid()
        {
            List<List<Node>> temp = new();

            for (int i = 0; i < Feld.Count; i++)
            {
                temp.Add(new List<Node>());
                for (int j = 0; j < Feld[i].Count; j++)
                {
                    switch (Feld[i][j].State)
                    {
                        case FieldState.wakable:
                            temp[i].Add(new Node(new Vector2(i, j), true));
                            break;

                        case FieldState.notWakable:
                            temp[i].Add(new Node(new Vector2(i, j), false));
                            break;

                        case FieldState.Start:
                            temp[i].Add(new Node(new Vector2(i, j), true));
                            break;

                        case FieldState.Ende:
                            temp[i].Add(new Node(new Vector2(i, j), true));
                            break;

                        case FieldState.Path:
                            temp[i].Add(new Node(new Vector2(i, j), true));
                            break;

                        default:
                            temp[i].Add(new Node(new Vector2(i, j), false));
                            break;
                    }
                }
            }
            return temp;
        }
    }

    public enum FieldState
    {
        wakable,

        notWakable,

        Start,

        Ende,

        Path,

        Error
    }

    public class Tile
    {
        public class Markieurngen
        {
            public Markieurngen(string name, int wert)
            {
                Name = name;
                Wert = wert;
            }

            public string Name { get; }

            public int Wert { get; set; }
        }

        public Tile(Point PosLOben, int Height_X, int Height_Y, int abstand, FieldState State)
        {
            this.PosLOben = PosLOben;
            this.State = State;
            PosRUnten = new Point(this.PosLOben.X + Height_X + abstand, this.PosLOben.Y + Height_Y + abstand);
            Item = new Stack<Item>();
            height_x = Height_X;
            height_y = Height_Y;
            Scanned = false;
            Marks = new List<Markieurngen>();
            Marks.Add(new Markieurngen("pathed", 0));
        }

        public List<Markieurngen> Marks { get; }

        public bool Scanned { get; set; }

        public Stack<Item> Item { get; set; }

        public Amei Ameis { get; set; }

        public Nest Nest { get; set; }

        private int height_x;

        public int Height_X
        {
            get { return height_x; }
        }

        private int height_y;

        public int Height_Y
        {
            get { return height_y; }
        }

        public Point PosLOben { get; }

        public Point PosRUnten { get; }

        public FieldState State { get; set; }
    }

    public class Item
    {
        public Vector2 Pos { get; set; }

        public string Name { get; set; }

        public Item(Vector2 Position, string Name)
        {
            Pos = Position;
            this.Name = Name;
        }

        public override bool Equals(object obj)
        {
            return obj is Item item &&
                   Pos.Equals(item.Pos);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Pos);
        }
    }
}