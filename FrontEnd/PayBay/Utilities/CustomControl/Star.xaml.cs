﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Shapes;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace PayBay.Utilities.CustomControl
{
	public sealed partial class Star : UserControl
	{
		public Star()
		{
			this.DataContext = this;
			this.InitializeComponent();
		}

        #region StarSize
        /// <summary>
        /// Value Dependency Property
        /// </summary>
        public static readonly DependencyProperty StarSizeProperty =
            DependencyProperty.Register("StarSize", typeof(Double), typeof(Star),
                new PropertyMetadata((Double)(0.0), OnStarSizeChanged));

        /// <summary>
        /// Gets or sets the Value property.  
        /// </summary>
        public Double StarSize
        {
            get { return (Double)GetValue(StarSizeProperty); }
            set { SetValue(StarSizeProperty, value); }
        }

        /// <summary>
        /// Handles changes to the Value property.
        /// </summary>
        private static void OnStarSizeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Star star = (Star)d;
            Double newSize = (Double)e.NewValue;

            star.gridStar.Width = newSize;
            star.gridStar.Height = newSize;
            star.gridStar.Clip = new RectangleGeometry
            {
                Rect = new Rect(0, 0, newSize, newSize)
            };

			ClipForeground(star);
        }

        #endregion

        #region StarBackground
        /// <summary>
        /// StarBackground Dependency Property
        /// </summary>
        public static readonly DependencyProperty StarBackgroundProperty =
			DependencyProperty.Register("StarBackground", typeof(SolidColorBrush), typeof(Star),
				new PropertyMetadata(new SolidColorBrush(Windows.UI.Colors.Gray), OnStarBackgroundChanged));

		/// <summary>
		/// Gets or sets the StarBackground property
		/// </summary>
		public SolidColorBrush StarBackground
		{
			get { return (SolidColorBrush)GetValue(StarBackgroundProperty); }
			set { SetValue(StarBackgroundProperty, value); }
		}

		/// <summary>
		/// Handles changes to the StarBackground property
		/// </summary>
		/// <param name="d"></param>
		/// <param name="e"></param>
		public static void OnStarBackgroundChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			SolidColorBrush newBackground = (SolidColorBrush)e.NewValue;
			Star star = (Star)d;

			star.starBackground.Fill = newBackground;
		}
		#endregion

		#region StarForeground
		/// <summary>
		/// StarForeground Dependency Property
		/// </summary>
		public static readonly DependencyProperty StarForegroundProperty =
			DependencyProperty.Register("StarForeground", typeof(SolidColorBrush), typeof(Star),
				new PropertyMetadata(new SolidColorBrush(Windows.UI.Colors.Transparent), OnStarForegroundChanged));

		/// <summary>
		/// Gets or sets the StarForeground property
		/// </summary>
		public SolidColorBrush StarForeground
		{
			get { return (SolidColorBrush)GetValue(StarForegroundProperty); }
			set { SetValue(StarForegroundProperty, value); }
		}

		/// <summary>
		/// Handles changes to the StarForeground property
		/// </summary>
		/// <param name="d"></param>
		/// <param name="e"></param>
		public static void OnStarForegroundChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			((Star)d).starForeground.Fill = (SolidColorBrush)e.NewValue;
		}
		#endregion

		#region StarOutlineColor
		/// <summary>
		/// StarOutlineColor Dependency Property
		/// </summary>
		public static readonly DependencyProperty StarOutlineColorProperty =
			DependencyProperty.Register("StarOutlineColor", typeof(SolidColorBrush), typeof(Star),
				new PropertyMetadata(new SolidColorBrush(Windows.UI.Colors.Transparent), OnStarOutlineColorChanged));

		/// <summary>
		/// Gets or sets the StarOutlineColor property
		/// </summary>
		public SolidColorBrush StarOutlineColor
		{
			get { return (SolidColorBrush)GetValue(StarOutlineColorProperty); }
			set { SetValue(StarOutlineColorProperty, value); }
		}

		/// <summary>
		/// Handles changes to the StarOutlineColor property
		/// </summary>
		/// <param name="d"></param>
		/// <param name="e"></param>
		public static void OnStarOutlineColorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			((Star)d).starOutline.Stroke = (SolidColorBrush)e.NewValue;
		}
		#endregion

		#region Value
		/// <summary>
		/// Value Dependency Property
		/// </summary>
		public static readonly DependencyProperty ValueProperty =
			DependencyProperty.Register("Value", typeof(Double), typeof(Star),
				new PropertyMetadata((Double)(0.0), OnValueChanged));

		/// <summary>
		/// Gets or sets the Value property.  
		/// </summary>
		public Double Value
		{
			get { return (Double)GetValue(ValueProperty); }
			set { SetValue(ValueProperty, value); }
		}

		/// <summary>
		/// Handles changes to the Value property.
		/// </summary>
		private static void OnValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			Star star = d as Star;
			star.Value = Math.Min(1, Math.Max(0, (double)e.NewValue));
			ClipForeground(star);
		}
		#endregion

		private static void ClipForeground(DependencyObject d)
		{
			Star star = d as Star;
			Int32 marginLeftOffset = (Int32)(star.Value * star.StarSize);
			star.vbForeground.Clip = new RectangleGeometry { Rect = new Rect(0, 0, marginLeftOffset, star.StarSize) };
		}

		#region Eventhandling

		#endregion

		private void Star_Loaded(object sender, RoutedEventArgs e)
		{
			Star star = sender as Star;
			star.StarSize = star.ActualHeight;
		}
	}
}
