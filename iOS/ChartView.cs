using System;
using System.Collections.Generic;
using CoreGraphics;
using UIKit;

namespace FlowerChart.iOS
{
	
	public class ChartView : UIView
	{
		#region -= constructors =-

		public ChartView() : base() { }

		#endregion

		// rect changes depending on if the whole view is being redrawn, or just a section
		public override void Draw(CGRect rect)
		{
			Console.WriteLine("Draw() Called");
			base.Draw(rect);

			using (CGContext context = UIGraphics.GetCurrentContext())
			{
				UIColor.White.SetFill();
				context.FillRect(rect);

				CGPoint startPoint= new CGPoint();
				startPoint.X = 150;
				startPoint.Y = 200;
				Double radius = 120;

				List<UIColor> colors = new List<UIColor>();
				colors.Add(UIColor.FromRGB(231, 218, 129));
				colors.Add(UIColor.FromRGB(215, 164, 127));
				colors.Add(UIColor.FromRGB(215, 129, 127));
				colors.Add(UIColor.FromRGB(215, 127, 177));
				colors.Add(UIColor.FromRGB(179, 127, 215));
				colors.Add(UIColor.FromRGB(127, 135, 215));
				colors.Add(UIColor.FromRGB(127, 177, 215));
				colors.Add(UIColor.FromRGB(140, 193, 202));
				colors.Add(UIColor.FromRGB(150, 222, 180));
				colors.Add(UIColor.FromRGB(166, 215, 127));

				int[] numbers = new int[10] { 100, 80, 100, 90, 120, 120, 85, 110, 120, 110};

				for (var i = 0; i < 10; i++)
				{
					UIColor color = colors[i];
					color.SetStroke();
					context.BeginPath();
					CGPoint startArc = new CGPoint();
					startArc.X = (nfloat)(startPoint.X + radius / 8 * Math.Cos(2 * Math.PI * i / 10 + 2 * Math.PI / 20 - Math.PI / 2));
					startArc.Y = (nfloat)(startPoint.Y + radius / 8 * Math.Sin(2 * Math.PI * i / 10 + 2 * Math.PI / 20 - Math.PI / 2));
					context.MoveTo(startArc.X , startArc.Y );
					context.AddArc(startArc.X, startArc.Y, (nfloat)numbers[i], (nfloat)(-Math.PI / 2 + 2 * Math.PI *i / 10) , (nfloat)(- Math.PI / 2 + Math.PI*2*(i+1)/10), false);
					context.ClosePath();
					color.SetFill();
					context.DrawPath(CGPathDrawingMode.FillStroke);
				}

				UIColor alpha_color = UIColor.FromRGBA(255, 255, 255, 80);
				alpha_color.SetStroke();
				context.BeginPath();
				context.AddArc(startPoint.X, startPoint.Y, (nfloat)radius * 5 / 8, 0, (nfloat)(2 * Math.PI), false);
				context.ClosePath();
				alpha_color.SetFill();
				context.DrawPath(CGPathDrawingMode.FillStroke);

				UIColor white_color = UIColor.FromRGBA(255, 255, 255, 255);
				white_color.SetStroke();
				context.BeginPath();
				context.AddArc(startPoint.X, startPoint.Y, (nfloat)radius * 3 / 8, 0, (nfloat)(2 * Math.PI), false);
				context.ClosePath();
				white_color.SetFill();
				context.DrawPath(CGPathDrawingMode.FillStroke);

				for (var i = 0; i < 10; i++)
				{
					CGPoint icon_center = new CGPoint();
					icon_center.X = (nfloat)(startPoint.X + radius / 2 * Math.Cos(2 * Math.PI * i / 10 + 2 * Math.PI / 20 - Math.PI / 2));
					icon_center.Y = (nfloat)(startPoint.Y + radius / 2 * Math.Sin(2 * Math.PI * i / 10 + 2 * Math.PI / 20 - Math.PI / 2));
					String filename = (i + 1).ToString() + ".png";
					CGImage image = UIImage.FromFile(filename).CGImage;
					nint icon_width = image.Width*2/3;
					nint icon_height = image.Height*2/3;
					CGRect icon_rect = new CGRect(icon_center.X - icon_width / 2, icon_center.Y - icon_height / 2, icon_width, icon_height);
					context.DrawImage(icon_rect, image);
				}

				int sum = 0;
				for (var i = 0; i < 10; i++)
				{
					sum += numbers[i];
				}
				int result = sum / 12;

				UIColor ring_color = UIColor.FromRGB(234, 235, 236);
				ring_color.SetStroke();
				context.BeginPath();
				context.AddArc(startPoint.X, startPoint.Y, (nfloat)radius * 5 / 16, 0, (nfloat)(2 * Math.PI), false);
				context.ClosePath();
				context.SetLineWidth(5);
				context.DrawPath(CGPathDrawingMode.Stroke);

				UIColor result_color = UIColor.FromRGB(172, 202, 78);
				result_color.SetStroke();
				context.BeginPath();
				context.AddArc(startPoint.X, startPoint.Y, (nfloat)radius * 5 / 16, (nfloat)(-Math.PI/2), (nfloat)(-Math.PI / 2+ result * 2 * Math.PI / 100), false);
				context.SetLineWidth(5);
				context.DrawPath(CGPathDrawingMode.Stroke);

				UIColor.Black.SetStroke();
				CGPoint fontPoint = new CGPoint();
				context.ScaleCTM(1, -1);
				fontPoint.X = startPoint.X - 17;
				fontPoint.Y = -startPoint.Y - 12;
				context.SetLineWidth(1);
				context.TextPosition = fontPoint;
				context.SelectFont("Helvetica", 32, CGTextEncoding.MacRoman);
				UIColor.Black.SetFill();
				context.SetTextDrawingMode(CGTextDrawingMode.FillStroke);
				context.ShowText(result.ToString());

				//// draw a rectangle using stroke rect

				//context.StrokeRect(new CGRect(10, 10, 200, 100));

				//// draw a rectangle using a path
				//context.BeginPath();
				//context.MoveTo(220, 10);
				//context.AddLineToPoint(420, 10);
				//context.AddLineToPoint(420, 110);
				//context.AddLineToPoint(220, 110);
				//context.ClosePath();
				//UIColor.DarkGray.SetFill();
				//context.DrawPath(CGPathDrawingMode.FillStroke);

				//// draw a rectangle using a path
				//CGPath rectPath = new CGPath();
				//rectPath.AddRect(new CGRect(new CGPoint(430, 10), new CGSize(200, 100)));
				//context.AddPath(rectPath);
				//context.DrawPath(CGPathDrawingMode.Stroke);

			}
		}
	}
}

