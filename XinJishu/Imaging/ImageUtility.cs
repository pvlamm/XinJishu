using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace XinJishu.Imaging
{
    public class ImageUtility : IDisposable
    {
        private Image _image { get; set; }
        public ImageUtility() { this._image = null; }
        public ImageUtility(String filename) {
            LoadFile(filename);
        }
        public ImageUtility(Stream file) {
            LoadFile(file);
        }

        public void LoadFile(String filename) {

            this._image = Image.FromFile(filename);

        }
        public void LoadFile(Stream file) {

            this._image = Image.FromStream(file);
        }

        public void RemoveMetadata()
        {
            if (this._image == null)
                return;

            foreach (var id in this._image.PropertyIdList)
                this._image.RemovePropertyItem(id);
        }

        /// <summary>
        /// Extracts list of metadata as string from a loaded image.
        /// This means that Integers, Byte[] and ASCII is returned as a simple string
        /// </summary>
        /// <returns>
        /// Id is 1: Base64 Encoded String
        /// Id is 2: UTF8 Decoded String
        /// Id is 3: BitConverter.ToUInt16, then to String
        /// Id is 4: BitConverter.ToUInt32, then to String
        /// Id is 5: [BitConverter.ToUInt32 => ToString] / [BitConverter.ToUInt32 => ToString], then to String
        /// Id is 6: Base64 Encoded String
        /// Id is 7: BitConverter.ToInt32, then to String
        /// Id is 10: BitConverter.ToInt32 => ToString / BitConverter.ToInt32 => ToString then to String
        /// Else : Convert to Base64 String, return
        /// </returns>
        public IEnumerable<String> GetMetadata()
        {
            if (this._image == null)
                yield return null;

            foreach (var item in this._image.PropertyItems)
            {
                switch (item.Id)
                {
                    case 1:
                        yield return Convert.ToBase64String(item.Value);
                        break;
                    case 2:
                        yield return System.Text.Encoding.UTF8.GetString(item.Value);
                        break;
                    case 3:
                        yield return BitConverter.ToUInt16(item.Value, 0).ToString();
                        break;
                    case 4:
                        yield return BitConverter.ToUInt32(item.Value, 0).ToString();
                        break;
                    case 5:
                        yield return (
                            BitConverter.ToUInt32(item.Value, 0).ToString() + "/" + BitConverter.ToUInt32(item.Value, 4).ToString()
                            ).ToString();
                        break;
                    case 6:
                        yield return Convert.ToBase64String(item.Value);
                        break;
                    case 7:
                        yield return BitConverter.ToInt32(item.Value, 0).ToString();
                        break;
                    case 10:
                        yield return (
                            BitConverter.ToInt32(item.Value, 0).ToString() + "/" + BitConverter.ToInt32(item.Value, 4).ToString()
                            ).ToString();
                        break;
                    default:
                        yield return Convert.ToBase64String(item.Value);
                        break;
                }
            }
        }

        public void Dispose()
        {
            this._image.Dispose();
        }
    }
}
