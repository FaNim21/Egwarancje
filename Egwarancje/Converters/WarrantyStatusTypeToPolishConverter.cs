using System;
using System.Globalization;
using Microsoft.Maui.Controls;
using EgwarancjeDbLibrary.Models;

namespace Egwarancje.Converters
{
    public class WarrantyStatusTypeToPolishConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is WarrantyStatusType status)
            {
                switch (status)
                {
                    case WarrantyStatusType.Awaitng:
                        return "Oczekuje";
                    case WarrantyStatusType.Accepted:
                        return "Zaakceptowane";
                    case WarrantyStatusType.Declined:
                        return "Odrzucone";
                }
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
