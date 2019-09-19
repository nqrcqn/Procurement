using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using POEApi.Model;
using System.Windows.Documents;
using Procurement.ViewModel;
using System.Windows.Media;
using System.Windows;

namespace Procurement.View
{
    public class ItemPropertyToFormattedRunConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            Property property = value as Property;
            var paragraph = DisplayModeFactory.Create(property).Get();
            paragraph.Margin = new Thickness(0);
            paragraph.LineHeight = 0;
            FlowDocument flowDoc = new FlowDocument(paragraph);
            flowDoc.PagePadding = new Thickness(0);
            return flowDoc;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
