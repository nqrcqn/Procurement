using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using POEApi.Model;
using System.Windows.Documents;
using Procurement.ViewModel;
using System.Windows.Media;

namespace Procurement.View
{
    public class ItemPropertyToFormattedRunConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            Property property = value as Property;

            var fd = new FlowDocument(DisplayModeFactory.Create(property).Get());

            Style style = new Style(typeof(Paragraph));
            style.Setters.Add(new Setter(Block.MarginProperty, new Thickness(0)));
            fd.Resources.Add(typeof(Paragraph), style);

            return fd;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
