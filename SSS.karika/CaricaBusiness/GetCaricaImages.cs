using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;

namespace SSS.Karika.KarikaBusiness
{
    public class GetKarikaImages
    {
        public List<KarikaImage> lstKarikaImages(string directoryPath)
        {
            List<KarikaImage> _lstKarikaImages = new List<KarikaImage>();

            if (!Directory.Exists(directoryPath))
            {
                throw new DirectoryNotFoundException();
            }

            FileInfo[] lstFileInfos = new DirectoryInfo(directoryPath).GetFiles();

            foreach (FileInfo file in lstFileInfos)
            {
                if (file.Extension.ToUpper() == ".JPG" || file.Extension.ToUpper() == ".PNG")
                {
                    // Read File and Convert to KarikaImage
                    Image _tmpImg = Image.FromFile(file.FullName);
                    KarikaImage _tmpCarikaImage = GetKarikaImageFromImage(_tmpImg);
                    _tmpCarikaImage.Id = Convert.ToInt32(file.Name.Substring(0, 5));
                    _lstKarikaImages.Add(_tmpCarikaImage);
                }
            }

            return _lstKarikaImages;
        }

        public KarikaImage GetKarikaImageFromImage(Image image)
        {
            KarikaImage cImg = new KarikaImage();

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

        public KarikaImage GetKarikaImageFromFileInfo(FileInfo fileInfo)
        {
            KarikaImage cImg = new KarikaImage();
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

        public KarikaImage GetKarikaImageFromFilePath(String filePath)
        {
            KarikaImage cImg = new KarikaImage();
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