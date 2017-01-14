using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel;

namespace KMeans
{
    class Coordinates : List<double>, INotifyPropertyChanged
    {
        public Coordinates() : base() { }

        public new double this[int i]
        {
            get
            {
                return base[i];
            }
            set
            {
                base[i] = value;
                PropretyChanged();
            }
        }

        private void PropretyChanged()
        {
            throw new NotImplementedException();
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
