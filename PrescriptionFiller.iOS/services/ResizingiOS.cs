using System;
using System.Drawing;
using CoreGraphics;
using PrescriptionFiller.interfaces;
using PrescriptionFiller.iOS.services;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(ResizingiOS))]
namespace PrescriptionFiller.iOS.services
{
    public class ResizingiOS : IResizing
	{
		public byte[] ResizeImage(byte[] imageData, float width, float height)
		{
			UIImage originalImage = ImageFromByteArray(imageData);

			//create a 24bit RGB image
			using (CGBitmapContext context = new CGBitmapContext(IntPtr.Zero,
			(int)width, (int)height, 8,
			(int)(4 * width), CGColorSpace.CreateDeviceRGB(),
			CGImageAlphaInfo.PremultipliedFirst))
			{

				RectangleF imageRect = new RectangleF(0, 0, width, height);

				// draw the image
				context.DrawImage(imageRect, originalImage.CGImage);

				UIKit.UIImage resizedImage = UIKit.UIImage.FromImage(context.ToImage());

				// save the image as a jpeg
				return resizedImage.AsJPEG().ToArray();
			}
		}

		public static UIKit.UIImage ImageFromByteArray(byte[] data)
		{
			if (data == null)
			{
				return null;
			}

			UIKit.UIImage image;
			try
			{
				image = new UIKit.UIImage(Foundation.NSData.FromArray(data));
			}
			catch (Exception e)
			{
				Console.WriteLine("Image load failed: " + e.Message);
				return null;
			}
			return image;
		}

		public byte[] ResizeImage(byte[] imageData, float sizeDesired)
		{
			UIImage originalImage = ImageFromByteArray(imageData);

			int width = 0, height = 0;

			if (originalImage.Size.Width > originalImage.Size.Height)
			{
				height = (int)(originalImage.Size.Height * sizeDesired / originalImage.Size.Width);
				width = (int)sizeDesired;
			}
			else
			{
				width = (int)(originalImage.Size.Width * sizeDesired / originalImage.Size.Height);
				height = (int)sizeDesired;
			}

			Console.WriteLine("@@@@@@@@@@@@#################" + originalImage.Size.Height + " " + originalImage.Size.Width);
			Console.WriteLine("@@@@@@@@@@@@#################" + height + " " + width);

			//create a 24bit RGB image
			//using (CGBitmapContext context = new CGBitmapContext(IntPtr.Zero,
			//(int)width, (int)height, 8,
			//(int)(4 * width), CGColorSpace.CreateDeviceRGB(),
			//CGImageAlphaInfo.PremultipliedLast))
			//{

			//	RectangleF imageRect = new RectangleF(0, 0, width, height);

			//	if (originalImage.Orientation == UIImageOrientation.Left)
			//	{
			//		context.RotateCTM((nfloat)(Math.PI / 2f)); // 90 deg
			//		context.TranslateCTM(0, -height);

			//	}
			//	else if (originalImage.Orientation == UIImageOrientation.Right)
			//	{
			//		context.RotateCTM((nfloat)(-Math.PI / 2f)); // -90 deg
			//		context.TranslateCTM(-width, 0);

			//	}
			//	else if (originalImage.Orientation == UIImageOrientation.Up)
			//	{
			//		// NOTHING
			//	}
			//	else if (originalImage.Orientation == UIImageOrientation.Down)
			//	{
			//		context.RotateCTM((nfloat)(-Math.PI)); // -180 deg
			//		context.TranslateCTM(width, height);

			//	}

			//	// draw the image
			//	context.DrawImage(imageRect, originalImage.CGImage);

			//	UIKit.UIImage resizedImage = UIKit.UIImage.FromImage(context.ToImage());

			//	// save the image as a jpeg
			//	return resizedImage.AsJPEG().ToArray();
			//}


			using (CGImage imageRef = originalImage.CGImage)
			{
				CGImageAlphaInfo alphaInfo = imageRef.AlphaInfo;
				CGColorSpace colorSpaceInfo = CGColorSpace.CreateDeviceRGB();
				if (alphaInfo == CGImageAlphaInfo.None)
				{
					alphaInfo = CGImageAlphaInfo.NoneSkipLast;
				}

				width = Convert.ToInt32(imageRef.Width);
				height = Convert.ToInt32(imageRef.Height);


				if (height >= width)
				{
					width = (int)Math.Floor((double)width * ((double)sizeDesired / (double)height));
					height = (int)sizeDesired;
				}
				else
				{
					height = (int)Math.Floor((double)height * ((double)sizeDesired / (double)width));
					width = (int)sizeDesired;
				}


				CGBitmapContext bitmap;

				if (originalImage.Orientation == UIImageOrientation.Up || originalImage.Orientation == UIImageOrientation.Down)
				{
					bitmap = new CGBitmapContext(IntPtr.Zero, width, height, imageRef.BitsPerComponent, imageRef.BytesPerRow, colorSpaceInfo, alphaInfo);
				}
				else
				{
					bitmap = new CGBitmapContext(IntPtr.Zero, height, width, imageRef.BitsPerComponent, imageRef.BytesPerRow, colorSpaceInfo, alphaInfo);
				}

				switch (originalImage.Orientation)
				{
					case UIImageOrientation.Left:
						bitmap.RotateCTM((float)Math.PI / 2);
						bitmap.TranslateCTM(0, -height);
						break;
					case UIImageOrientation.Right:
						bitmap.RotateCTM(-((float)Math.PI / 2));
						bitmap.TranslateCTM(-width, 0);
						break;
					case UIImageOrientation.Up:
						break;
					case UIImageOrientation.Down:
						bitmap.TranslateCTM(width, height);
						bitmap.RotateCTM(-(float)Math.PI);
						break;
				}

				bitmap.DrawImage(new System.Drawing.Rectangle(0, 0, width, height), imageRef);

				UIKit.UIImage resizedImage = UIKit.UIImage.FromImage(bitmap.ToImage());

				// save the image as a jpeg
				return resizedImage.AsJPEG().ToArray();

			}
		}
	}
}