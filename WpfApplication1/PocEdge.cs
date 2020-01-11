using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Media;
using DM.WPF_Tree;
using QuickGraph;

namespace DM.WPF_Tree
{
    /// <summary>
    /// A simple identifiable edge.
    /// </summary>
    [DebuggerDisplay("{Source.ID} -> {Target.ID}")]
    public class PocEdge : Edge<PocVertex>, INotifyPropertyChanged
    {
        private string id;
        public string ID
        {
            get { return id; }
            set
            {
                id = value;
                NotifyPropertyChanged("ID");
            }
        }

        public Brush EdgeColor { get; set; }

        public PocEdge(string id, PocVertex source, PocVertex target, Brush Color)
            : base(source, target)
        {
            ID = id;
            EdgeColor = Color;
        }




        #region INotifyPropertyChanged Implementation

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        #endregion
    }
}
