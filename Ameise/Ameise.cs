using AStarSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Numerics;
using System.Diagnostics;

namespace Ameise
{
    public class Brain
    {
        private List<Vector2> positionen;

        public Astar astar;

        private long timesearched;

        public Vector2 NestPos { get; }

        public long Timesearched
        {
            get { return timesearched; }
        }

        private int iNotMovedCountUp;

        private int iNotMovedCountLeft;

        private int iNotMovedCountRight;

        private int iNotMovedCountDown;

        public List<Vector2> Positionen
        {
            get { return positionen; }
            set { positionen = value; }
        }

        public Brain(Vector2 NestPos)
        {
            positionen = new List<Vector2>();
            iNotMovedCountUp = 0;
            iNotMovedCountLeft = 0;
            iNotMovedCountRight = 0;
            iNotMovedCountDown = 0;
            astar = new Astar(Game.ConstructGrid());
            this.NestPos = NestPos;
        }

        private int checksourroundings(Amei amei)
        {
            int surrounded = 0;

            // rechts
            if (amei.Pos.X + 1 >= Game.Feld[0].Count)
            {
                surrounded++;
                Console.WriteLine("Rightnone");
            }
            else
            {
                if (Game.Feld[(int)amei.Pos.X + 1][(int)amei.Pos.Y].State == FieldState.NotWakable)
                {
                    surrounded++;
                    Console.WriteLine("Right");
                }
            }

            //links
            if (amei.Pos.X - 1 < 0)
            {
                surrounded++;
                Console.WriteLine("leftnone");
            }
            else
            {
                if (Game.Feld[(int)amei.Pos.X - 1][(int)amei.Pos.Y].State == FieldState.NotWakable)
                {
                    surrounded++;
                    Console.WriteLine("left");
                }
            }

            // Oberhalb
            if (amei.Pos.Y - 1 < 0)
            {
                surrounded++;
                Console.WriteLine("Abovenone");
            }
            else
            {
                if (Game.Feld[(int)amei.Pos.X][(int)amei.Pos.Y - 1].State == FieldState.NotWakable)
                {
                    surrounded++;
                    Console.WriteLine("Above");
                }
            }

            // unterhalb
            if (amei.Pos.Y + 1 >= Game.Feld.Count)
            {
                surrounded++;
                Console.WriteLine("belownone");
            }
            else
            {
                if (Game.Feld[(int)amei.Pos.X][(int)amei.Pos.Y + 1].State == FieldState.NotWakable)
                {
                    surrounded++;
                    Console.WriteLine("below");
                }
            }

            Console.WriteLine("----");
            return surrounded;
        }

        public void Collect(Amei amei, BackgroundWorker bw)
        {
            TimeSpan ts;
            string elapsedTime;
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            do
            {
                //GC.Collect(1,GCCollectionMode.Forced);
                GC.WaitForPendingFinalizers();

                if (bw.CancellationPending)
                {
                    stopWatch.Stop();
                    timesearched += stopWatch.ElapsedTicks;
                    Console.WriteLine("##########################");
                    ts = new TimeSpan(timesearched);
                    elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                    ts.Hours, ts.Minutes, ts.Seconds,
                    ts.Milliseconds / 10);
                    Console.WriteLine("Time Elapesed: " + elapsedTime);

                    Console.WriteLine("##########################");
                    break;
                }

                //Console.WriteLine(GC.GetTotalMemory(true));
                amei.Search();

                GC.Collect();

                if (Positionen.Count > 0)
                {
                    do
                    {
                        astar = new Astar(Game.ConstructGrid());
                        Stack<Node> path = astar.FindPath(amei.Pos, positionen[0]);

                        if (path != null)
                        {
                            //move
                            foreach (var pat in path)
                            {
                                //Thread.Sleep(250);
                                if (amei.Pos.X > pat.Position.X)
                                {
                                    amei.MoveLeft();
                                }
                                else if (amei.Pos.X < pat.Position.X)
                                {
                                    amei.MoveRight();
                                }
                                else if (amei.Pos.Y > pat.Position.Y)
                                {
                                    amei.MoveUp();
                                }
                                else if (amei.Pos.Y < pat.Position.Y)
                                {
                                    amei.MoveDown();
                                }
                            }
                            amei.TryCollect();
                        }
                        else
                        {
                            this.positionen.Clear();
                            Console.WriteLine("kein weg");

                            return;
                        }
                    } while (Positionen.Count > 0);
                }
                else
                {
                    Random rnd = new Random();

                    //Console.WriteLine($"curr {Feld[(int)amei.Pos.X][(int)amei.Pos.Y].Marks[0].Wert}");
                    //Console.WriteLine($"last {Feld[(int)amei.LastPos.X][(int)amei.LastPos.Y].Marks[0].Wert}");
                    switch (rnd.Next(1, 5))
                    {
                        case 1:
                            if (amei.Pos.X + 1 <= Game.Feld[0].Count - 1)
                            {
                                if ((iNotMovedCountRight > 5 && Game.Feld[(int)amei.Pos.X + 1][(int)amei.Pos.Y].Marks[0].Wert < 1000) || Game.Feld[(int)amei.Pos.X][(int)amei.Pos.Y].Marks[0].Wert >= Game.Feld[(int)amei.Pos.X + 1][(int)amei.Pos.Y].Marks[0].Wert)
                                {
                                    amei.MoveRight();
                                    iNotMovedCountRight = 0;

                                    if (checksourroundings(amei) >= 3)
                                    {
                                        Game.Feld[(int)amei.Pos.X][(int)amei.Pos.Y].Marks[0].Wert += 1000;
                                        Console.WriteLine("surrounded");
                                    }
                                    Console.WriteLine("----");
                                }
                                else
                                {
                                    iNotMovedCountRight++;
                                }
                            }

                            break;

                        case 2:
                            if (amei.Pos.Y - 1 >= 0)
                            {
                                if ((iNotMovedCountUp > 5 && Game.Feld[(int)amei.Pos.X][(int)amei.Pos.Y - 1].Marks[0].Wert < 1000) || Game.Feld[(int)amei.Pos.X][(int)amei.Pos.Y].Marks[0].Wert > Game.Feld[(int)amei.Pos.X][(int)amei.Pos.Y - 1].Marks[0].Wert)
                                {
                                    amei.MoveUp();
                                    iNotMovedCountUp = 0;

                                    if (checksourroundings(amei) >= 3)
                                    {
                                        Game.Feld[(int)amei.Pos.X][(int)amei.Pos.Y].Marks[0].Wert += 1000;
                                        Console.WriteLine("surrounded");
                                    }
                                    Console.WriteLine("----");
                                }
                                else
                                {
                                    iNotMovedCountUp++;
                                }
                            }

                            break;

                        case 3:
                            if (amei.Pos.Y + 1 <= Game.Feld.Count - 1)
                            {
                                if ((iNotMovedCountDown > 5 && Game.Feld[(int)amei.Pos.X][(int)amei.Pos.Y + 1].Marks[0].Wert < 1000) || Game.Feld[(int)amei.Pos.X][(int)amei.Pos.Y].Marks[0].Wert >= Game.Feld[(int)amei.Pos.X][(int)amei.Pos.Y + 1].Marks[0].Wert)
                                {
                                    amei.MoveDown();
                                    iNotMovedCountDown = 0;

                                    if (checksourroundings(amei) >= 3)
                                    {
                                        Game.Feld[(int)amei.Pos.X][(int)amei.Pos.Y].Marks[0].Wert += 1000;
                                        Console.WriteLine("surrounded");
                                    }
                                    Console.WriteLine("----");
                                }
                                else
                                {
                                    iNotMovedCountDown++;
                                }
                            }

                            break;

                        case 4:
                            if (amei.Pos.X - 1 >= 0)
                            {
                                if ((iNotMovedCountLeft > 5 && Game.Feld[(int)amei.Pos.X - 1][(int)amei.Pos.Y].Marks[0].Wert < 1000) || Game.Feld[(int)amei.Pos.X][(int)amei.Pos.Y].Marks[0].Wert > Game.Feld[(int)amei.Pos.X - 1][(int)amei.Pos.Y].Marks[0].Wert)
                                {
                                    amei.MoveLeft();
                                    iNotMovedCountLeft = 0;

                                    if (checksourroundings(amei) >= 3)
                                    {
                                        Game.Feld[(int)amei.Pos.X][(int)amei.Pos.Y].Marks[0].Wert += 1000;
                                        Console.WriteLine("surrounded");
                                    }
                                    Console.WriteLine("----");
                                }
                                else
                                {
                                    iNotMovedCountLeft++;
                                }
                            }
                            break;
                    }
                }

                if (amei.Inventar.Count > 2)
                {
                    if (amei.GotoHome())
                    {
                        Game.Nester[0].transfareItemsFromAmeise(amei);
                    }
                }

                // continue random Move
            } while (amei.Inventar.Count + Game.Nester[0].Inventar.Count < Game.Items);

            if (amei.GotoHome())
            {
                Game.Nester[0].transfareItemsFromAmeise(amei);
            }

            stopWatch.Stop();
            timesearched += stopWatch.ElapsedTicks;

            Console.WriteLine("Alles einegesammelt oder Beendet");
            Console.WriteLine("##########################");

            ts = new TimeSpan(timesearched);
            elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}", ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);

            Console.WriteLine("Time Elapesed: " + elapsedTime);
            Console.WriteLine("##########################");
        }
    }

    public class Amei
    {
        public static int AnzahlAmeisen = 0;

        public Guid Idenifier { get; }

        public Color Team { get; }

        public bool Deployed = false;

        private Vector2 pos;

        public Vector2 LastPos { get; private set; }

        private int rotation;

        private Brain brn;

        private Stack<Item> inventar = new();

        public Stack<Item> Inventar
        {
            get { return inventar; }
        }

        public Vector2 Pos
        {
            get { return pos; }
        }

        public int Rotation
        {
            get { return rotation; }
        }

        public Amei(Vector2 Position, Color Col)
        {
            Idenifier = Guid.NewGuid();
            brn = new Brain(Position);
            pos = Position;
            rotation = 0;
            Team = Col;

            //Game.Feld[(int)Position.X][(int)Position.Y].Ameis = this;
            Amei.AnzahlAmeisen++;
        }

        public void StartCollect(BackgroundWorker bw)
        {
            if (Deployed == true)
            {
                brn.Collect(this, bw);
                if (bw.CancellationPending)
                {
                    return;
                }
            }
            else
            {
                bw.CancelAsync();
            }
        }

        public void Search(int radius = 2)
        {
            //own possition Check
            int X = (int)pos.X;
            int Y = (int)pos.Y;
            for (int j = 0; j < radius; j++)
            {
                Y += 1;

                // unten
                for (int x = 0; x < 1 + (2 * j); x++)
                {
                    if (x != 0)
                    {
                        X += 1;
                    }

                    //Console.Write(X + "<>" + (Y) + ";");
                    if (X >= 0 && Y >= 0 && X <= Game.Feld[0].Count - 1 && Y <= Game.Feld.Count - 1)
                    {
                        if (Engine.Scan)
                        {
                            Game.Feld[X][Y].Scanned = true;
                            Engine.Draw();
                        }
                        if (Game.Feld[X][Y].Item.Count > 0)
                        {
                            for (int i = 0; i < Game.Feld[X][Y].Item.Count; i++)
                            {
                                brn.Positionen.Add(Game.Feld[X][Y].Item.Peek().Pos);
                            }
                        }
                    }
                }

                //Console.Write("\n");
                X += 1;

                // rechts
                for (int y = 0; y < 3 + (2 * j); y++)
                {
                    if (y != 0)
                    {
                        Y -= 1;
                    }

                    //Console.Write(X + "<>" + (Y) + ";");
                    if (X >= 0 && Y >= 0 && X <= Game.Feld[0].Count - 1 && Y <= Game.Feld.Count - 1)
                    {
                        if (Engine.Scan)
                        {
                            Game.Feld[X][Y].Scanned = true;
                            Engine.Draw();
                        }
                        for (int i = 0; i < Game.Feld[X][Y].Item.Count; i++)
                        {
                            brn.Positionen.Add(Game.Feld[X][Y].Item.Peek().Pos);
                        }
                    }
                }

                //Console.Write("\n");
                X -= 1;

                //Oben
                for (int x = 0; x < 1 + (2 * j); x++)
                {
                    if (x != 0)
                    {
                        X -= 1;
                    }

                    //Console.Write(X + "<>" + (Y) + ";");
                    if (X >= 0 && Y >= 0 && X <= Game.Feld[0].Count - 1 && Y <= Game.Feld.Count - 1)
                    {
                        if (Engine.Scan)
                        {
                            Game.Feld[X][Y].Scanned = true;
                            Engine.Draw();
                        }
                        for (int i = 0; i < Game.Feld[X][Y].Item.Count; i++)
                        {
                            brn.Positionen.Add(Game.Feld[X][Y].Item.Peek().Pos);
                        }
                    }
                }

                //Console.Write("\n");
                X -= 1;

                //Links

                for (int y = 0; y < 3 + (2 * j); y++)
                {
                    if (y != 0)
                    {
                        Y += 1;
                    }

                    //Console.Write(X + "<>" + (Y) + ";");
                    if (X >= 0 && Y >= 0 && X <= Game.Feld[0].Count - 1 && Y <= Game.Feld.Count - 1)
                    {
                        if (Engine.Scan)
                        {
                            Game.Feld[X][Y].Scanned = true;
                            Engine.Draw();
                        }
                        for (int i = 0; i < Game.Feld[X][Y].Item.Count; i++)
                        {
                            brn.Positionen.Add(Game.Feld[X][Y].Item.Peek().Pos);
                        }
                    }
                }
            }
            if (Engine.Scan)
            {
                Game.UnsetScan();
            }
        }

        public void BackHome()
        {
            if (this.Deployed == false)
            {
                this.pos = brn.NestPos;
                this.LastPos = brn.NestPos;
            }
        }

        public void MoveUp(bool mark = true)
        {
            if (Deployed == true)
            {
                LastPos = pos;
                int Y = (int)pos.Y;
                rotation = 270;

                if (--Y >= 0)
                {
                    if (Game.Feld[(int)Pos.X][Y].State == FieldState.Wakable && Game.Feld[(int)Pos.X][Y].Ameis == null && Game.Feld[(int)Pos.X][Y].Nest == null)
                    {
                        pos.Y = Y;
                        Game.Feld[(int)LastPos.X][(int)LastPos.Y].Ameis = null;
                        Game.Feld[(int)Pos.X][(int)Pos.Y].Ameis = this;

                        if (mark)
                        {
                            Game.Feld[(int)Pos.X][(int)Pos.Y].Marks[0].Wert++;
                        }

                        Engine.Draw();
                    }
                }
            }
        }

        public void MoveDown(bool mark = true)
        {
            if (Deployed == true)
            {
                LastPos = pos;
                int Y = (int)pos.Y;
                rotation = 90;

                if (++Y <= Game.Feld.Count - 1)
                {
                    if (Game.Feld[(int)Pos.X][Y].State == FieldState.Wakable && Game.Feld[(int)Pos.X][Y].Ameis == null && Game.Feld[(int)Pos.X][Y].Nest == null)
                    {
                        pos.Y = Y;
                        Game.Feld[(int)LastPos.X][(int)LastPos.Y].Ameis = null;
                        Game.Feld[(int)Pos.X][(int)Pos.Y].Ameis = this;

                        if (mark)
                        {
                            Game.Feld[(int)Pos.X][(int)Pos.Y].Marks[0].Wert++;
                        }

                        Engine.Draw();
                    }
                }
            }
        }

        public void MoveRight(bool mark = true)
        {
            if (Deployed == true)
            {
                LastPos = pos;
                int X = (int)pos.X;
                rotation = 0;

                if (++X <= Game.Feld[0].Count - 1)
                {
                    if (Game.Feld[X][(int)Pos.Y].State == FieldState.Wakable && Game.Feld[X][(int)Pos.Y].Ameis == null && Game.Feld[X][(int)Pos.Y].Nest == null)
                    {
                        pos.X = X;
                        Game.Feld[(int)LastPos.X][(int)LastPos.Y].Ameis = null;
                        Game.Feld[(int)Pos.X][(int)Pos.Y].Ameis = this;

                        if (mark)
                        {
                            Game.Feld[(int)Pos.X][(int)Pos.Y].Marks[0].Wert++;
                        }

                        Engine.Draw();
                    }
                }
            }
        }

        public void MoveLeft(bool mark = true)
        {
            if (Deployed == true)
            {
                LastPos = pos;
                int X = (int)pos.X;
                rotation = 180;

                if (--X >= 0)
                {
                    if (Game.Feld[X][(int)Pos.Y].State == FieldState.Wakable && Game.Feld[X][(int)Pos.Y].Ameis == null && Game.Feld[X][(int)Pos.Y].Nest == null)
                    {
                        pos.X = X;
                        Game.Feld[(int)LastPos.X][(int)LastPos.Y].Ameis = null;
                        Game.Feld[(int)Pos.X][(int)Pos.Y].Ameis = this;

                        //Feld[(int)LastPos.X][(int)LastPos.Y].Ameis.Dispose();
                        if (mark)
                        {
                            Game.Feld[(int)Pos.X][(int)Pos.Y].Marks[0].Wert++;
                        }

                        Engine.Draw();
                    }
                }
            }
        }

        public void TryCollect()
        {
            if (Deployed == true)
            {
                if (Game.Feld[(int)Pos.X][(int)Pos.Y].Item.Count > 0)
                {
                    inventar.Push(Game.Feld[(int)Pos.X][(int)Pos.Y].Item.Pop());

                    if (brn.Positionen.Count > 0)
                    {
                        int i = brn.Positionen.IndexOf(pos);
                        brn.Positionen.RemoveAt(i);
                    }

                    Engine.Draw();
                }
                else
                {
                    Console.WriteLine("hier ist Nix");
                }
            }
        }

        public void TryPlaceBlock()
        {
            if (Deployed == true)
            {
                int X = (int)pos.X;
                int Y = (int)pos.Y;
                switch (rotation)
                {
                    case 0:

                        if (++X <= Game.Feld[0].Count - 1)
                        {
                            if (Game.Feld[X][(int)Pos.Y].State == FieldState.NotWakable)
                            {
                                Game.Feld[X][(int)Pos.Y].State = FieldState.Wakable;
                            }
                            else
                            {
                                Game.Feld[X][(int)Pos.Y].State = FieldState.NotWakable;
                            }
                        }

                        break;

                    case 90:

                        if (++Y <= Game.Feld.Count - 1)
                        {
                            if (Game.Feld[(int)Pos.X][Y].State == FieldState.NotWakable)
                            {
                                Game.Feld[(int)Pos.X][Y].State = FieldState.Wakable;
                            }
                            else
                            {
                                Game.Feld[(int)Pos.X][Y].State = FieldState.NotWakable;
                            }
                        }
                        break;

                    case 180:

                        if (--X >= 0)
                        {
                            if (Game.Feld[X][(int)Pos.Y].State == FieldState.NotWakable)
                            {
                                Game.Feld[X][(int)Pos.Y].State = FieldState.Wakable;
                            }
                            else
                            {
                                Game.Feld[X][(int)Pos.Y].State = FieldState.NotWakable;
                            }
                        }
                        break;

                    case 270:

                        if (--Y >= 0)
                        {
                            if (Game.Feld[(int)Pos.X][Y].State == FieldState.NotWakable)
                            {
                                Game.Feld[(int)Pos.X][Y].State = FieldState.Wakable;
                            }
                            else
                            {
                                Game.Feld[(int)Pos.X][Y].State = FieldState.NotWakable;
                            }
                        }
                        break;
                }
            }
        }

        public bool GotoHome()
        {
            List<Vector2> LandingPosition = new List<Vector2>();
            const int SearchRaius = 1;
            int X = (int)brn.NestPos.X;
            int Y = (int)brn.NestPos.Y;
            for (int j = 0; j < SearchRaius; j++)
            {
                Y += 1;

                // unten
                for (int x = 0; x < 1 + (2 * j); x++)
                {
                    if (x != 0)
                    {
                        X += 1;
                    }

                    //Console.Write(X + "<>" + (Y) + ";");
                    if (X >= 0 && Y >= 0 && X <= Game.Feld[0].Count - 1 && Y <= Game.Feld.Count - 1)
                    {
                        if (Engine.Scan)
                        {
                            Game.Feld[X][Y].Scanned = true;
                            Engine.Draw();
                        }
                        if (Game.Feld[X][Y].State == FieldState.Wakable)
                        {
                            LandingPosition.Add(new Vector2(X, Y));
                        }
                    }
                }

                //Console.Write("\n");
                X += 1;

                // rechts
                for (int y = 0; y < 3 + (2 * j); y++)
                {
                    if (y != 0)
                    {
                        Y -= 1;
                    }

                    //Console.Write(X + "<>" + (Y) + ";");
                    if (X >= 0 && Y >= 0 && X <= Game.Feld[0].Count - 1 && Y <= Game.Feld.Count - 1)
                    {
                        if (Engine.Scan)
                        {
                            Game.Feld[X][Y].Scanned = true;
                            Engine.Draw();
                        }
                        if (Game.Feld[X][Y].State == FieldState.Wakable)
                        {
                            LandingPosition.Add(new Vector2(X, Y));
                        }
                    }
                }

                //Console.Write("\n");
                X -= 1;

                //Oben
                for (int x = 0; x < 1 + (2 * j); x++)
                {
                    if (x != 0)
                    {
                        X -= 1;
                    }

                    //Console.Write(X + "<>" + (Y) + ";");
                    if (X >= 0 && Y >= 0 && X <= Game.Feld[0].Count - 1 && Y <= Game.Feld.Count - 1)
                    {
                        if (Engine.Scan)
                        {
                            Game.Feld[X][Y].Scanned = true;
                            Engine.Draw();
                        }
                        if (Game.Feld[X][Y].State == FieldState.Wakable)
                        {
                            LandingPosition.Add(new Vector2(X, Y));
                        }
                    }
                }

                //Console.Write("\n");
                X -= 1;

                //Links

                for (int y = 0; y < 3 + (2 * j); y++)
                {
                    if (y != 0)
                    {
                        Y += 1;
                    }

                    //Console.Write(X + "<>" + (Y) + ";");
                    if (X >= 0 && Y >= 0 && X <= Game.Feld[0].Count - 1 && Y <= Game.Feld.Count - 1)
                    {
                        if (Engine.Scan)
                        {
                            Game.Feld[X][Y].Scanned = true;
                            Engine.Draw();
                        }
                        if (Game.Feld[X][Y].State == FieldState.Wakable)
                        {
                            LandingPosition.Add(new Vector2(X, Y));
                        }
                    }
                }
            }
            if (Engine.Scan)
            {
                Game.UnsetScan();
            }

            foreach (var item in LandingPosition)
            {
                brn.astar = new Astar(Game.ConstructGrid());
                Stack<Node> path = brn.astar.FindPath(Pos, item);

                if (path != null)
                {
                    Console.WriteLine($"Pos {item.ToString()} nutzbar");
                    //move
                    foreach (var pat in path)
                    {
                        //Thread.Sleep(250);
                        if (Pos.X > pat.Position.X)
                        {
                            MoveLeft();
                        }
                        else if (Pos.X < pat.Position.X)
                        {
                            MoveRight();
                        }
                        else if (Pos.Y > pat.Position.Y)
                        {
                            MoveUp();
                        }
                        else if (Pos.Y < pat.Position.Y)
                        {
                            MoveDown();
                        }
                    }
                    return true;
                }

                Console.WriteLine($"Pos {item.ToString()} nicht nutzbar");
            }
            Console.WriteLine("Keinene Weg nach hause Gefunden");
            return false;
        }

        private static void PossitionAmeise(Amei Ameise)
        {
            Game.Feld[(int)Ameise.Pos.X][(int)Ameise.Pos.Y].Ameis = Ameise;
        }

        public override bool Equals(object obj)
        {
            return obj is Amei amei &&
                   Pos.Equals(amei.Pos);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Pos);
        }
    }
}