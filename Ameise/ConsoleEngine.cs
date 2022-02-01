using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Numerics;
using AStarSharp;
using System.Threading;
using System.Diagnostics;

namespace Ameise
{
  

    internal static class ConsoleEngine
    {
        //https://de.wikipedia.org/wiki/Unicodeblock_Geometrische_Formen
        //https://de.wikipedia.org/wiki/Codepage_850
        //https://docs.microsoft.com/de-de/dotnet/api/system.text.encoding?view=net-6.0
        private static char Wakable = '□';

        private static char NotWakable = '■';

        private static char Errot = 'X';

        private static char Start = '▣';

        private static char End = '▣';

        private static char Path = '▦';

        private static char[] zahlen = { '①', '②', '③', '④', '⑤', '⑥', '⑦', '⑧', '⑨', '⑩', '⑪', '⑫', '⑬', '⑭', '⑮', '⑯', '⑰', '⑱', '⑲', '⑳', '㉑', '㉒', '㉓', '㉔', '㉕', '㉖', '㉗', '㉘', '㉙', '㉚', '㉛', '㉜', '㉝', '㉞', '㉟', '㊱', '㊲', '㊳', '㊴', '㊵', '㊶', '㊷', '㊸', '㊹', '㊺', '㊻', '㊼', '㊽', '㊾', '㊿' };

        public static void Draw(List<List<Tile>> feld, Vector2 vec, FieldState fls)
        {
            feld[(int)vec.X][(int)vec.Y].State = fls;
            Draw(feld);
        }

        public static void Draw(List<List<Tile>> feld)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(NotWakable);
            for (int i = 0; i < feld[0].Count; i++)
            {
                //sb.Append(NotWakable);
                sb.Append(zahlen[i]);
                //Console.Write(NotWakable);
            }
            sb.Append(NotWakable);
            sb.Append('\n');
            //Console.Write('\n');
            for (int i = 0; i < feld.Count; i++)
            {
                sb.Append(zahlen[i]);

                //sb.Append(NotWakable);
                //Console.Write(NotWakable);
                for (int a = 0; a < feld[i].Count; a++)
                {
                    switch (feld[i][a].State)
                    {
                        case FieldState.Wakable:
                            sb.Append(Wakable);
                            //Console.Write(Wakable);
                            break;

                        case FieldState.NotWakable:
                            sb.Append(NotWakable);
                            //Console.Write(NotWakable);
                            break;

                        //case FieldState.Start:
                        //    sb.Append(Start);

                        //    //Console.Write(Start);
                        //    break;

                        //case FieldState.Ende:
                        //    sb.Append(End);

                        //    //Console.Write(End);
                        //    break;

                        //case FieldState.Path:
                        //    sb.Append(Path);

                        //    //Console.Write(Path);
                        //    break;

                        default:
                            sb.Append(Errot);
                            //Console.Write(Errot);
                            break;
                    }
                }
                sb.Append(NotWakable);
                //Console.Write(NotWakable);
                sb.Append('\n');
                //Console.Write('\n');
            }
            for (int i = 0; i < feld[0].Count + 2; i++)
            {
                sb.Append(NotWakable);
                //Console.Write(NotWakable);
            }
            //sb.Append("\n\n");
            //Console.Write('\n');

            //Console.Clear();
            Console.SetCursorPosition(0, 0);
            Console.CursorVisible = false;
            Console.WindowHeight = feld.Count + 2;
            Console.WindowWidth = /*2 **/ (feld[0].Count + 2);
            Console.OutputEncoding = Encoding.Unicode;
            Console.Write(sb.ToString());
            sb.Clear();
            //Thread.Sleep(25);
        }
    }
}