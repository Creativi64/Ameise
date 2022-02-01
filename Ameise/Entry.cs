using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using System.Drawing;

namespace Ameise
{
    public class Entry
    {
        public Entry(Guid idenifier, Vector2 pos, Color team, string name)
        {
            Idenifier = idenifier;
            Pos = pos;
            Team = team;
            Name = name;
        }

        public Guid Idenifier { get; }
        public Vector2 Pos { get; set; }
        public Color Team { get; }
        public string Name { get; }

        public override string ToString()
        {
            return $"{Idenifier.ToString()}-{Name}-{Team.ToString()}-{Pos.ToString()}-";
        }

    }
}