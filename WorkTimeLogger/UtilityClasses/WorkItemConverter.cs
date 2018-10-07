using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Data;
using WorkTimeLogger;

namespace WorkTimeLogger.Converters
{
    /// <summary>
    /// Converter used when adding new work items to the workitem list.
    /// </summary>
    public class WorkItemConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is WorkItem)
                return value;
            return null;
        }
    }
}
