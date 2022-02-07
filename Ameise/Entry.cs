using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using System.Drawing;
using System.ComponentModel;

using System.Data;
using JetBrains.Annotations;

using System.Windows.Forms;
using JetBrains.Annotations;
using System.Runtime.CompilerServices;

namespace Ameise
{
    public class Entry : INotifyPropertyChanged
    {
        public Entry(Guid idenifier, Vector2 pos, Color team, string name)
        {
            Idenifier = idenifier;
            Pos = pos;
            Team = team;
            Name = name;
        }

        public Guid Idenifier { get; }
        //public Vector2 Pos { get; set; }

        private Vector2 pos;

        public Vector2 Pos
        {
            get { return pos; }
            set
            {
                pos = value;
                OnPropertyChanged();

            }
        }

        public Color Team { get; }
        public string Name { get; }

        public override string ToString()
        {
            return $"{Team.ToString()}-{Pos.ToString()}-{Idenifier.ToString()}-{Name}";
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}