using System;
using System.Threading.Tasks;
using Plugin.Media.Abstractions;

namespace PrescriptionFiller.Services
{
    public interface ICameraService
	{
		Task<Photo> TakePicture(System.Action<Photo> callback = null);
		Task<Photo> PickPicture();
	}

	public class Photo
	{
		public MediaFile Picture { get; set; }
	}
}
