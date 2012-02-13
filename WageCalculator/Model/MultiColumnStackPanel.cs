using System;
using System.Collections.ObjectModel;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace WageCalculator.Model
{
    public class MultiColumnStackPanel : StackPanel
    {
        public int NumberOfColumns { get; set; }

        public static readonly DependencyProperty ItemsProperty =
            DependencyProperty.Register("Items",
            typeof(Collection<UIElement>),
            typeof(MultiColumnStackPanel),
            new PropertyMetadata(new Collection<UIElement>()));

        public Collection<UIElement> Items
        {
            get { return (Collection<UIElement>)GetValue(ItemsProperty); }
        }

        public MultiColumnStackPanel()
        {
            Orientation = Orientation.Vertical;
            Loaded += (s, e) => LoadItems();
        }

        private void LoadItems()
        {
            Children.Clear();
            if (Items == null) return;
            StackPanel sp = CreateNewStackPanel();
            foreach (UIElement item in Items)
            {
                sp.Children.Add(item);
                if (sp.Children.Count == NumberOfColumns)
                {
                    Children.Add(sp);
                    sp = CreateNewStackPanel();
                }
            }
            if (sp.Children.Count > 0)
                Children.Add(sp);
        }

        private static StackPanel CreateNewStackPanel()
        {
            return new StackPanel() { Orientation = Orientation.Horizontal };
        }
    }
}
