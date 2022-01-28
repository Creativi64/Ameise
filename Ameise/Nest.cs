using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using System.Drawing;

namespace Ameise
{
    public class Nest
    {
        public readonly Guid Idenifier;

        public readonly Vector2 posNest;

        public readonly Color Team;

        private Stack<Amei> ameisen = new Stack<Amei>();

        private List<Item> Inventar = new List<Item>();

        public Nest(Vector2 posNest, Color Team, int Ameisen)
        {
            Idenifier = Guid.NewGuid();
            this.posNest = posNest;
            this.Team = Team;
            for (int i = 0; i < Ameisen; i++)
            {
                ameisen.Push(new Amei(posNest, this.Team));
            }
        }

        /*

        BISSEL RIGGED

         */

        /// <summary>
        /// Deply a Amise
        /// </summary>
        /// <param name="IdentifierNest">The guid of the Nest</param>
        /// <returns>true if it deployed False if not</returns>
        public bool depolyAmeiseFromNest(Guid IdentifierNest)
        {
            //bool Deployed = false;

            //// Check Depoly pos
            //int[] directions = new int[4];
            //int i = 0;
            //Random rnd = new Random();
            //do
            //{
            //    switch (rnd.Next(1, 5))
            //    {
            //        case 1:
            if (posNest.X + 1 <= Game.Feld[0].Count - 1)
            {
                if (Game.Feld[(int)posNest.X + 1][(int)posNest.Y].State == FieldState.wakable && Game.Feld[(int)posNest.X + 1][(int)posNest.Y].Ameis == null)
                {
                    // Right

                    if (IdentifierNest != Guid.Empty)
                    {
                        // Chose The Specific
                        Game.Feld[(int)posNest.X][(int)posNest.Y].Ameis = Nest.getFirstAmeiseFromNest(IdentifierNest);
                        Game.Feld[(int)posNest.X][(int)posNest.Y].Ameis.Deployed = true;
                        Engine.Draw();
                        Game.Feld[(int)posNest.X][(int)posNest.Y].Ameis.MoveRight();
                    }
                    else
                    {
                        Game.Feld[(int)posNest.X][(int)posNest.Y].Ameis = Game.Nester[0].ameisen.Pop();
                        Game.Feld[(int)posNest.X][(int)posNest.Y].Ameis.Deployed = true;
                        Engine.Draw();
                        Game.Feld[(int)posNest.X][(int)posNest.Y].Ameis.MoveRight();
                    }
                    //i = 500;
                    //break;
                    return true;
                }
            }

            //directions[0]++;

            //    break;

            //case 2:
            if (posNest.Y - 1 >= 0)
            {
                if (Game.Feld[(int)posNest.X][(int)posNest.Y - 1].State == FieldState.wakable && Game.Feld[(int)posNest.X][(int)posNest.Y - 1].Ameis == null)
                {
                    //up
                    if (IdentifierNest != Guid.Empty)
                    {
                        // Chose The Specific
                        Game.Feld[(int)posNest.X][(int)posNest.Y].Ameis = Nest.getFirstAmeiseFromNest(IdentifierNest);
                        Game.Feld[(int)posNest.X][(int)posNest.Y].Ameis.Deployed = true;
                        Engine.Draw();
                        Game.Feld[(int)posNest.X][(int)posNest.Y].Ameis.MoveUp();
                    }
                    else
                    {
                        Game.Feld[(int)posNest.X][(int)posNest.Y].Ameis = Game.Nester[0].ameisen.Pop();
                        Game.Feld[(int)posNest.X][(int)posNest.Y].Ameis.Deployed = true;
                        Engine.Draw();
                        Game.Feld[(int)posNest.X][(int)posNest.Y].Ameis.MoveUp();
                    }
                    //i = 500;
                    //break;
                    return true;
                }
            }
            //directions[1]++;

            //    break;

            //case 3:
            if (posNest.Y + 1 <= Game.Feld.Count - 1)
            {
                if (Game.Feld[(int)posNest.X][(int)posNest.Y + 1].State == FieldState.wakable && Game.Feld[(int)posNest.X][(int)posNest.Y + 1].Ameis == null)
                {
                    //down
                    if (IdentifierNest != Guid.Empty)
                    {
                        // Chose The Specific
                        Game.Feld[(int)posNest.X][(int)posNest.Y].Ameis = Nest.getFirstAmeiseFromNest(IdentifierNest);
                        Game.Feld[(int)posNest.X][(int)posNest.Y].Ameis.Deployed = true;
                        Engine.Draw();
                        Game.Feld[(int)posNest.X][(int)posNest.Y].Ameis.MoveDown();
                    }
                    else
                    {
                        Game.Feld[(int)posNest.X][(int)posNest.Y].Ameis = Game.Nester[0].ameisen.Pop();
                        Game.Feld[(int)posNest.X][(int)posNest.Y].Ameis.Deployed = true;
                        Engine.Draw();
                        Game.Feld[(int)posNest.X][(int)posNest.Y].Ameis.MoveDown();
                    }
                    //i = 500;
                    //break;
                    return true;
                }
            }
            //directions[2]++;

            //    break;

            //case 4:
            if (posNest.X - 1 >= 0)
            {
                if (Game.Feld[(int)posNest.X - 1][(int)posNest.Y].State == FieldState.wakable && Game.Feld[(int)posNest.X - 1][(int)posNest.Y].Ameis == null)
                {
                    //left
                    if (IdentifierNest != Guid.Empty)
                    {
                        // Chose The Specific
                        Game.Feld[(int)posNest.X][(int)posNest.Y].Ameis = Nest.getFirstAmeiseFromNest(IdentifierNest);
                        Game.Feld[(int)posNest.X][(int)posNest.Y].Ameis.Deployed = true;
                        Engine.Draw();
                        Game.Feld[(int)posNest.X][(int)posNest.Y].Ameis.MoveLeft();
                    }
                    else
                    {
                        Game.Feld[(int)posNest.X][(int)posNest.Y].Ameis = Game.Nester[0].ameisen.Pop();
                        Game.Feld[(int)posNest.X][(int)posNest.Y].Ameis.Deployed = true;
                        Engine.Draw();
                        Game.Feld[(int)posNest.X][(int)posNest.Y].Ameis.MoveLeft();
                    }
                    //i = 500;
                    //break;
                    return true;
                }
            }

            //directions[3]++;

            //        break;
            //}

            //foreach (var item in directions)
            //{
            //    if (item > 0)
            //    {
            //        i++;
            //    }
            //}
            //} while (i < 4);
             
            return false;
        }

        public static Amei getFirstAmeiseFromNest(Guid IdenifierNest)
        {
            foreach (var Nester in Game.Nester)
            {
                if (Nester.Idenifier == IdenifierNest)
                {
                    if (Nester.ameisen.Count > 0)
                    {
                        return Nester.ameisen.Pop();
                    }
                }
            }

            return null;
        }

        public static Amei peekFirstAmeiseFromNest(Guid IdenifierNest)
        {
            foreach (var Nester in Game.Nester)
            {
                if (Nester.Idenifier == IdenifierNest)
                {
                    if (Nester.ameisen.Count > 0)
                    {
                        return Nester.ameisen.Peek();
                    }
                }
            }

            return null;
        }

        public static Amei getAmeiseInField(Guid IdenifierAmeise)
        {
            for (int i = 0; i < Game.Feld.Count; i++)
            {
                for (int a = 0; a < Game.Feld[i].Count; a++)
                {
                    if (Game.Feld[a][i].Ameis != null)
                    {
                        if (Game.Feld[a][i].Ameis.Deployed == true && Game.Feld[a][i].Ameis.Idenifier == IdenifierAmeise)
                        {
                            return Game.Feld[a][i].Ameis;
                        }
                    }
                }
            }
            return null;
        }

        public static Vector2[] getAllAmeiseInField(Color Team)
        {
            List<Vector2> posAmeisen = new List<Vector2>();
            for (int i = 0; i < Game.Feld.Count; i++)
            {
                for (int a = 0; a < Game.Feld[i].Count; a++)
                {
                    if (Game.Feld[a][i].Ameis != null)
                    {
                        posAmeisen.Add(Game.Feld[a][i].Ameis.Pos);
                    }
                }
            }
            return posAmeisen.ToArray();
        }

        private void pickupSurroundingAmeise()
        {
            //set own possition
            const int SearchRaius = 1;
            int X = (int)posNest.X;
            int Y = (int)posNest.Y;
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
                        Game.Feld[X][Y].Scanned = true;
                        if (Engine.Scan)
                        {
                            Engine.Draw();
                        }
                        if (Game.Feld[X][Y].Ameis != null)
                        {
                            this.ameisen.Push(Game.Feld[X][Y].Ameis);
                            Game.Feld[X][Y].Ameis = null;
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
                        Game.Feld[X][Y].Scanned = true;
                        if (Engine.Scan)
                        {
                            Engine.Draw();
                        }
                        if (Game.Feld[X][Y].Ameis != null)
                        {
                            this.ameisen.Push(Game.Feld[X][Y].Ameis);
                            Game.Feld[X][Y].Ameis = null;
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
                        Game.Feld[X][Y].Scanned = true;
                        if (Engine.Scan)
                        {
                            Engine.Draw();
                        }
                        if (Game.Feld[X][Y].Ameis != null)
                        {
                            this.ameisen.Push(Game.Feld[X][Y].Ameis);
                            Game.Feld[X][Y].Ameis = null;
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
                        Game.Feld[X][Y].Scanned = true;
                        if (Engine.Scan)
                        {
                            Engine.Draw();
                        }
                        if (Game.Feld[X][Y].Ameis != null)
                        {
                            this.ameisen.Push(Game.Feld[X][Y].Ameis);
                            Game.Feld[X][Y].Ameis = null;
                        }
                    }
                }
            }

            Game.UnsetScan();
        }

        public void RecallAmeis()
        {
            //get pos of ameisese
            Vector2[] posAmeisen = Nest.getAllAmeiseInField(this.Team);

            foreach (var item in posAmeisen)
            {
                Game.Feld[(int)item.X][(int)item.Y].Ameis.GotoHome();
                // ameise go home
                //ameise.Pickup
                pickupSurroundingAmeise();
            }
        }
    }
}