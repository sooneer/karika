using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;

namespace SSS.Carica.CaricaBusiness
{
    public class GetCaricaImages
    {
        public List<CaricaImage> lstCaricaImages(string directoryPath)
        {
            List<CaricaImage> _lstCaricaImages = new List<CaricaImage>();

            if (!Directory.Exists(directoryPath))
            {
                throw new DirectoryNotFoundException();
            }

            FileInfo[] lstFileInfos = new DirectoryInfo(directoryPath).GetFiles();

            foreach (FileInfo file in lstFileInfos)
            {
                if (file.Extension.ToUpper() == ".JPG" || file.Extension.ToUpper() == ".PNG")
                {
                    // Read File and Convert to CaricaImage
                    Image _tmpImg = Image.FromFile(file.FullName);
                    CaricaImage _tmpCarikaImage = GetCaricaImageFromImage(_tmpImg);
                    _tmpCarikaImage.Id = Convert.ToInt32(file.Name.Substring(0, 5));
                    _lstCaricaImages.Add(_tmpCarikaImage);
                }
            }

            return _lstCaricaImages;
        }

        public CaricaImage GetCaricaImageFromImage(Image image)
        {
            CaricaImage cImg = new CaricaImage();

            if (image == null)
            {
                throw new FileNotFoundException();
            }

            // PropertyTagImageDescription	0x010E
            // PropertyTagTypeRational      0x9206

            const int _imgSubject = 40095;
            const int _imgDescription = 40092;

            cImg.Subject = ReadImageProperty(image, _imgSubject);
            cImg.Description = ReadImageProperty(image, _imgDescription);

            image.Dispose();
            return cImg;
        }

        public CaricaImage GetCaricaImageFromFileInfo(FileInfo fileInfo)
        {
            CaricaImage cImg = new CaricaImage();
            Image image = Image.FromFile(fileInfo.FullName);
            if (image == null)
            {
                throw new FileNotFoundException();
            }

            // PropertyTagImageDescription	0x010E
            // PropertyTagTypeRational      0x9206

            const int _imgSubject = 40095;
            const int _imgDescription = 40092;

            cImg.Subject = ReadImageProperty(image, _imgSubject);
            cImg.Description = ReadImageProperty(image, _imgDescription);

            image.Dispose();
            return cImg;
        }

        public CaricaImage GetCaricaImageFromFilePath(String filePath)
        {
            CaricaImage cImg = new CaricaImage();
            FileInfo infoo = new FileInfo(HttpContext.Current.Server.MapPath(filePath));
            Image image = Image.FromFile(infoo.FullName);
            if (image == null)
            {
                throw new FileNotFoundException();
            }

            // PropertyTagImageDescription	0x010E
            // PropertyTagTypeRational      0x9206

            const int _imgSubject = 40095;
            const int _imgDescription = 40092;

            cImg.Subject = ReadImageProperty(image, _imgSubject);
            cImg.Description = ReadImageProperty(image, _imgDescription);
            cImg.Id =Convert.ToInt32(infoo.Name.Replace(".jpg",""));
            cImg.CreateDate = infoo.CreationTime;
            image.Dispose();
            return cImg;
        }

        public string ReadImageProperty(Image image, int propertyType)
        {
            string result = string.Empty;
            var props = image.PropertyItems;

            var imgProperty = props.FirstOrDefault(x => x.Id == propertyType);
            if (imgProperty != null)
            {
                var myObject = new System.Text.UnicodeEncoding();
                result = myObject.GetString(imgProperty.Value);
            }

            // For Test
            //for (int i = 0; i < props.Length; i++)
            //{
            //    var item = props[i];

            //    var myObject = new System.Text.UnicodeEncoding();
            //    result = myObject.GetString(item.Value);
            //}

            return result;
        }
    }
}