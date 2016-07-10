﻿using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Meowtrix.HentaiViewer.ViewModels
{
    class ViewPageSelector : DataTemplateSelector
    {
        public DataTemplate ListTemplate { get; set; }
        public DataTemplate DetailTemplate { get; set; }
        protected override DataTemplate SelectTemplateCore(object item, DependencyObject container)
        {
            if (item is ListPage) return ListTemplate;
            else if (item is DetailPage) return DetailTemplate;
            else throw new ArgumentException(nameof(item));
        }
    }
}