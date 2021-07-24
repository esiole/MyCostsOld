using ChartJSCore.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Drawing;

namespace MyCosts.ViewModels.Statistics
{
    public class ChartJSDataset
    {
        public string Title { get; init; }
        public ImmutableList<double?> Values { get; private set; }
        public List<ChartColor> Colors { get; init; }
        public Color Color  { get; init; }

        public ChartJSDataset(string title)
        {
            if (title is null) throw new ArgumentNullException(nameof(title));
            Title = title;
            Values = ImmutableList<double?>.Empty;
            Colors = new List<ChartColor>();
        }

        public ChartJSDataset(string title, Color color) : this(title)
        {
            Color = color;
        }

        public ChartJSDataset(string title, List<double?> values, List<ChartColor> colors)
        {
            if (title is null) throw new ArgumentNullException(nameof(title));
            if (values is null) throw new ArgumentNullException(nameof(values));
            if (colors is null) throw new ArgumentNullException(nameof(colors));
            if (values.Count != colors.Count) 
                throw new ArgumentException($"{nameof(values)} и {nameof(colors)} должны быть одинаковой длины");
            if (values.Count == 0 || colors.Count == 0)
                throw new ArgumentException($"{nameof(values)} и {nameof(colors)} должны содержать хотя бы один элемент");
            Title = title;
            Values = Values.AddRange(values);
            Colors = colors;
        }

        public void Add(double? value, ChartColor color)
        {
            Values = Values.Add(value);
            Colors.Add(color);
        }

        public void AddValue(double? value)
        {
            Values = Values.Add(value);
        }
    }
}
