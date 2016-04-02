using System;
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

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace PayBay.Utilities.CustomControl
{
	public sealed partial class StarRating : UserControl 
	{
		public StarRating()
		{
			this.InitializeComponent();
		}
                
        #region StarBackground

        /// <summary>
        /// StarBackground Dependency Property
        /// </summary>
        public static readonly DependencyProperty StarBackgroundProperty =
			DependencyProperty.Register("StarBackground", typeof(SolidColorBrush), typeof(StarRating), 
				new PropertyMetadata(new SolidColorBrush(Windows.UI.Colors.Transparent), OnStarBackgroundChanged));

		/// <summary>
		/// Gets or sets the StarBackground property.  
		/// </summary>
		public SolidColorBrush StarBackground
		{
			get { return (SolidColorBrush)GetValue(StarBackgroundProperty); }
			set { SetValue(StarBackgroundProperty, value); }
		}

		/// <summary>
		/// Handles changes to the StarBackground property.
		/// </summary>
		private static void OnStarBackgroundChanged(DependencyObject d,
			DependencyPropertyChangedEventArgs e)
		{
			StarRating control = (StarRating)d;
			foreach (Star star in control.spStars.Children)
				star.StarBackground = (SolidColorBrush)e.NewValue;
		}
		#endregion

		#region StarForeground

		/// <summary>
		/// StarForeground Dependency Property
		/// </summary>
		public static readonly DependencyProperty StarForegroundProperty =
			DependencyProperty.Register("StarForeground", typeof(SolidColorBrush), typeof(StarRating),
				new PropertyMetadata((SolidColorBrush)new SolidColorBrush(Windows.UI.Colors.LightGray), OnStarForegroundChanged));

		/// <summary>
		/// Gets or sets the StarForeground property.  
		/// </summary>
		public SolidColorBrush StarForeground
		{
			get { return (SolidColorBrush)GetValue(StarForegroundProperty); }
			set { SetValue(StarForegroundProperty, value); }
		}

		/// <summary>
		/// Handles changes to the StarForeground property.
		/// </summary>
		private static void OnStarForegroundChanged(DependencyObject d,
			DependencyPropertyChangedEventArgs e)
		{
			StarRating control = (StarRating)d;
			foreach (Star star in control.spStars.Children)
				star.StarForeground = (SolidColorBrush)e.NewValue;

		}
		#endregion

		#region StarOutlineColor

		/// <summary>
		/// StarOutlineColor Dependency Property
		/// </summary>
		public static readonly DependencyProperty StarOutlineColorProperty =
			DependencyProperty.Register("StarOutlineColor", typeof(SolidColorBrush), typeof(StarRating),
				new PropertyMetadata(new SolidColorBrush(Windows.UI.Colors.Transparent), OnStarOutlineColorChanged));

		/// <summary>
		/// Gets or sets the StarOutlineColor property.  
		/// </summary>
		public SolidColorBrush StarOutlineColor
		{
			get { return (SolidColorBrush)GetValue(StarOutlineColorProperty); }
			set { SetValue(StarOutlineColorProperty, value); }
		}

		/// <summary>
		/// Handles changes to the StarOutlineColor property.
		/// </summary>
		private static void OnStarOutlineColorChanged(DependencyObject d,
			DependencyPropertyChangedEventArgs e)
		{
			StarRating control = (StarRating)d;
			foreach (Star star in control.spStars.Children)
				star.StarOutlineColor = (SolidColorBrush)e.NewValue;

		}
		#endregion

		#region Value

		/// <summary>
		/// Value Dependency Property
		/// </summary>
		public static readonly DependencyProperty ValueProperty =
			DependencyProperty.Register("Value", typeof(Double), typeof(StarRating),
				new PropertyMetadata((Double)0.0, OnValueChanged));

		/// <summary>
		/// Gets or sets the Value property.  
		/// </summary>
		public Double Value
		{
			get { return (Double)GetValue(ValueProperty); }
			set
			{
				Double newValue = CoerceValue(this, value);
				SetValue(ValueProperty, newValue);
			}
		}

		/// <summary>
		/// Handles changes to the Value property.
		/// </summary>
		private static void OnValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			StarRating starRating = d as StarRating;
			RefreshStar(starRating, e.NewValue);
		}
		#endregion

		#region Maximum
		/// <summary>
		/// Maximum Dependency Property
		/// </summary>
		public static readonly DependencyProperty MaximumProperty =
			DependencyProperty.Register("Maximum", typeof(Int32), typeof(StarRating),
				new PropertyMetadata((Int32)0, OnMaximumChanged));

		/// <summary>
		/// Gets or sets the Maximum property.  
		/// </summary>
		public Int32 Maximum
		{
			get { return (Int32)GetValue(MaximumProperty); }
			set { SetValue(MaximumProperty, value); }
		}

		public static void OnMaximumChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			StarRating starRating = (StarRating)d;
			SetupStars(starRating);
		}
		#endregion

		#region Private Helpers
		/// <summary>
		/// Coerces the Value value.
		/// </summary>
		private static Double CoerceValue(StarRating starRating, object value)
		{
			Double current = (Double)value;

			if (current < (Double)0.0)
				current = (Double)0.0;

			if (current > starRating.Maximum)
				current = starRating.Maximum;

			return current;
		}

		/// <summary>
		/// Sets up stars when Maximun properties changed
		/// Will only show up to the number of stars requested (up to Maximum)
		/// </summary>
		/// <param name="starRating"></param>
		private static void SetupStars(StarRating starRating)
		{
			Double value = starRating.Value;

			starRating.spStars.Children.Clear();
			for (int i = 0; i < starRating.Maximum; i++)
			{
				Star star = new Star();
				star.Tag = i;
				star.StarBackground = starRating.StarBackground;
				star.StarForeground = starRating.StarForeground;
				star.StarOutlineColor = starRating.StarOutlineColor;
				star.Value = value;

				starRating.spStars.Children.Insert(i, star);
			}
		}

		/// <summary>
		/// Refresh stars when Value property changed
		/// </summary>
		/// <param name="starRating"></param>
		private static void RefreshStar(StarRating starRating, object e)
		{
			Double value = (Double)e;

			foreach (Star star in starRating.spStars.Children)
			{
				if (value > 1)
					star.Value = 1.0;
				else if (value > 0)
				{
					star.Value = value;
				}
				else
				{
					star.Value = 0.0;
				}

				value -= 1.0;
			}
		}
        		
		#endregion

		private void spStars_Tapped(object sender, TappedRoutedEventArgs e)
		{
			Double pos = e.GetPosition(this).X;
			Value = Convert.ToDouble(Math.Ceiling((pos * Maximum) / ActualWidth));
		}

		private void spStars_PointerMoved(object sender, PointerRoutedEventArgs e)
		{
			Double pos = e.GetCurrentPoint(this).Position.X;
			RefreshStar(this, Convert.ToDouble(Math.Ceiling((pos * Maximum) / ActualWidth)));
		}

        private void spStars_PointerExited(object sender, PointerRoutedEventArgs e)
        {
			
            RefreshStar(this, Value);
        }

        private void spStars_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            StackPanel starContainer = (StackPanel)sender;
            foreach (Star star in starContainer.Children)
            {
                star.StarSize = starContainer.ActualHeight;
            }
        }
    }
}
