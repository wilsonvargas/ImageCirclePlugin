﻿using ImageCircle.Forms.Plugin.Abstractions;
using System;
using Xamarin.Forms;
using ImageCircle.Forms.Plugin.iOS;
using Xamarin.Forms.Platform.iOS;
using System.ComponentModel;
using System.Diagnostics;
using Foundation;
using CoreAnimation;
using CoreGraphics;

[assembly: ExportRenderer(typeof(CircleImage), typeof(ImageCircleRenderer))]
namespace ImageCircle.Forms.Plugin.iOS
{
    /// <summary>
    /// ImageCircle Implementation
    /// </summary>
    [Preserve(AllMembers = true)]
    public class ImageCircleRenderer : ImageRenderer
    {
        /// <summary>
        /// Used for registration with dependency service
        /// </summary>
        public async static void Init()
        {
            var temp = DateTime.Now;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnElementChanged(ElementChangedEventArgs<Image> e)
        {
            base.OnElementChanged(e);
            if (Element == null)
                return;
            CreateCircle();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            if (e.PropertyName == VisualElement.HeightProperty.PropertyName ||
                e.PropertyName == VisualElement.WidthProperty.PropertyName ||
              e.PropertyName == CircleImage.BorderColorProperty.PropertyName ||
              e.PropertyName == CircleImage.BorderThicknessProperty.PropertyName ||
              e.PropertyName == CircleImage.FillColorProperty.PropertyName)
            {
                CreateCircle();
            }
        }

        private void CreateCircle()
        {
            try
            {
                var min = Math.Min(Element.Width, Element.Height);
                Control.Layer.CornerRadius = ((CircleImage)Element).CornerRadius;
                Control.Layer.MasksToBounds = false;
                Control.BackgroundColor = ((CircleImage)Element).FillColor.ToUIColor();
                Control.ClipsToBounds = true;

                var borderThickness = ((CircleImage)Element).BorderThickness;
                var externalBorder = new CALayer();
                externalBorder.CornerRadius = Control.Layer.CornerRadius;
                externalBorder.Frame = new CGRect(-.5, -.5, min + 1, min + 1);
                externalBorder.BorderColor = ((CircleImage)Element).BorderColor.ToCGColor();
                externalBorder.BorderWidth = ((CircleImage)Element).BorderThickness;

                Control.Layer.AddSublayer(externalBorder);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Unable to create circle image: " + ex);
            }
        }
    }
}
