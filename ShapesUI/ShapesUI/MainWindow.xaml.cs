using ShapesUI.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace ShapesUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private ObservableCollection<bool> showInputs = new() { true, false, false };
        private ObservableCollection<ShapeEntity> shapes = new();

        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<ShapeEntity> Shapes
        {
            get => shapes;
            set
            {
                shapes = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Shapes)));
            }
        }

        public static IEnumerable<ShapeEntityType> AvailableShapes => Enum.GetValues(typeof(ShapeEntityType)).Cast<ShapeEntityType>();

        public double ShapeAreaSum => Shapes.Sum(s => s.Area);

        public ObservableCollection<bool> ShowInputs
        {
            get => showInputs;
            set
            {
                showInputs = value;
                PropertyChanged?.Invoke(ShowInputs, null);
            }
        }

        public double InputA { get; set; }
        public double InputB { get; set; }


        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            /*
            Shapes.Add(new ShapeEntity(ShapeEntityType.Circle, "r=25", Math.PI * 25 * 25));
            Shapes.Add(new ShapeEntity(ShapeEntityType.Triangle, "a=25, h=10", 25 * 10 / 2));
            Shapes.Add(new ShapeEntity(ShapeEntityType.Triangle, "a=25, h=10", 25 * 10 / 2));
            Shapes.Add(new ShapeEntity(ShapeEntityType.Rectangle, "a=25, b=10", 25 * 10));
            Shapes.Add(new ShapeEntity(ShapeEntityType.Rectangle, "a=25, b=10", 25 * 10));
            Shapes.Add(new ShapeEntity(ShapeEntityType.Rectangle, "a=25, b=10", 25 * 10));
            Shapes.Add(new ShapeEntity(ShapeEntityType.Triangle, "a=25, h=10", 25 * 10 / 2));
            */


        }

        private void ShapeSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Debug.WriteLine("SelectionChanged");
            switch (e.AddedItems.Cast<ShapeEntityType>().FirstOrDefault())
            {
                case ShapeEntityType.Circle:
                    ShowInputs[1] = false;
                    ShowInputs[2] = false;
                    ShowInputs[0] = true;
                    break;
                case ShapeEntityType.Rectangle:
                    ShowInputs[0] = false;
                    ShowInputs[2] = false;
                    ShowInputs[1] = true;
                    break;
                case ShapeEntityType.Triangle:
                    ShowInputs[0] = false;
                    ShowInputs[1] = false;
                    ShowInputs[2] = true;
                    break;
                default:
                    ShowInputs[0] = false;
                    ShowInputs[1] = false;
                    ShowInputs[2] = false;
                    break;
            }
        }

        private void AddShapeClicked(object sender, RoutedEventArgs e)
        {
            var newShape = new ShapeEntity();
            if (ShowInputs[0] is true)
            {
                newShape.Type = ShapeEntityType.Circle;
                newShape.Values = $"r={InputA}";
                newShape.Area = CalculateCircleArea(InputA);
            }
            else if (ShowInputs[1] is true)
            {
                newShape.Type = ShapeEntityType.Rectangle;
                newShape.Values = $"a={InputA},b={InputB}";
                newShape.Area = CalculateRectangleArea(InputA, InputB);
            }
            else if (ShowInputs[2] is true)
            {
                newShape.Type = ShapeEntityType.Triangle;
                newShape.Values = $"a={InputA},h={InputB}";
                newShape.Area = CalculateTriangleArea(InputA, InputB);
            }

            Shapes.Add(newShape);
            //PropertyChanged?.Invoke(ShapeAreaSum, null);
        }

        private double CalculateCircleArea(double radius) => Math.PI * Math.Pow(radius, 2);
        private double CalculateRectangleArea(double a, double b) => a * b;
        private double CalculateTriangleArea(double a, double h) => a * h / 2;
    }
}
