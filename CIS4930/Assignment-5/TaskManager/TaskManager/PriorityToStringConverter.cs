using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace TaskManager
{
    class PriorityToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType,
        object parameter, string language)
        {
            string priority;
            switch (value)
            {
                case 1:
                    priority = "Low";
                    break;
                case 2:
                    priority = "Medium";
                    break;
                case 3:
                    priority = "High";
                    break;
                default:
                    priority = "Low";
                    break;
            }
            // Return the value to pass to the target.
            return priority;
        }

        // ConvertBack is not implemented for a OneWay binding.
        public object ConvertBack(object value, Type targetType,
            object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
