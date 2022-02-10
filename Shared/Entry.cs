using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using System.Drawing;
 

namespace AmeisenGame
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

        private Vector2 pos;

        public Vector2 Pos
        {
            get { return pos; }
            set
            {
                pos = value;
            }
        }

        public Color Team { get; }
        public string Name { get; }

        public override string ToString()
        {
            return $"{Team.ToString()}-{Pos.ToString()}-{Idenifier.ToString()}-{Name}";
        }
    }
}