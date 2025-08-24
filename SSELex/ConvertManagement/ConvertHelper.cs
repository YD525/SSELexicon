using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Media.Imaging;

// Copyright (C) 2025 YD525
// Licensed under the GNU GPLv3
// See LICENSE for details
//https://github.com/YD525/SSELexicon

namespace SSELex.ConvertManager
{
    public class ConvertHelper
    {
        public static string FileToBase64String(string FilePath)
        {
            FileStream NFileStream = new FileStream(FilePath, FileMode.Open);
            string Base64Str = "";
            try
            {
                NFileStream.Seek(0, SeekOrigin.Begin);
                byte[] Bytes = new byte[NFileStream.Length];
                int Log = Convert.ToInt32(NFileStream.Length);
                NFileStream.Read(Bytes, 0, Log);
                Base64Str = Convert.ToBase64String(Bytes);
                return Base64Str;
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                Console.ReadLine();
                return Base64Str;
            }
            finally
            {
                NFileStream.Close();
            }
        }

        public static byte[] Base64ToBytes(string ByteStr)
        {
            byte[] ImageBytes = Convert.FromBase64String(ByteStr);
            MemoryStream Memory = new MemoryStream(ImageBytes, 0, ImageBytes.Length);
            Memory.Write(ImageBytes, 0, ImageBytes.Length);

            return ImageBytes;
        }

        public static string UrlEnCode(string Msg)
        {
            return HttpUtility.UrlEncode(Msg);
        }

        public static string UrlDeCode(string Msg)
        {
            return HttpUtility.UrlDecode(Msg);
        }

        public static BitmapImage BitmapToBitmapImage(Bitmap bitmap)
        {
            Bitmap bitmapSource = new Bitmap(bitmap.Width, bitmap.Height);
            int i, j;
            for (i = 0; i < bitmap.Width; i++)
                for (j = 0; j < bitmap.Height; j++)
                {
                    Color pixelColor = bitmap.GetPixel(i, j);
                    Color newColor = Color.FromArgb(pixelColor.R, pixelColor.G, pixelColor.B);
                    bitmapSource.SetPixel(i, j, newColor);
                }
            MemoryStream ms = new MemoryStream();
            bitmapSource.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
            BitmapImage bitmapImage = new BitmapImage();
            bitmapImage.BeginInit();
            bitmapImage.StreamSource = new MemoryStream(ms.ToArray());
            bitmapImage.EndInit();

            return bitmapImage;
        }
        public static BitmapImage BytesToBitmapImage(byte[] Bytes)
        {
            if (Bytes == null) return new BitmapImage();

            BitmapImage NextImage = new BitmapImage();

            NextImage.Dispatcher.Invoke(new Action(() => {
                NextImage.BeginInit();
                NextImage.StreamSource = new MemoryStream(Bytes);
                NextImage.EndInit();
            }));
            return NextImage;
        }
        public static BitmapImage BytesToBitmapImageByMusic(byte[] Def,byte[] Bytes)
        {
            if (Bytes == null) return new BitmapImage();
            if (Bytes.Length == 0) Bytes = Def;

            BitmapImage NextImage = new BitmapImage();

            NextImage.Dispatcher.Invoke(new Action(() => {
                NextImage.BeginInit();
                NextImage.StreamSource = new MemoryStream(Bytes);
                NextImage.EndInit();
            }));
            return NextImage;
        }
        public static Bitmap BytesToBitmap(byte[] Bytes)
        {
            MemoryStream stream = null;
            try
            {
                stream = new MemoryStream(Bytes);
                return new Bitmap((Image)new Bitmap(stream));
            }
            catch (ArgumentNullException ex)
            {
                throw ex;
            }
            catch (ArgumentException ex)
            {
                throw ex;
            }
            finally
            {
                stream.Close();
            }
        }
        public static byte[] BitmapToBytes(Bitmap bitmap)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                bitmap.Save(stream, ImageFormat.Jpeg);
                byte[] data = new byte[stream.Length];
                stream.Seek(0, SeekOrigin.Begin);
                stream.Read(data, 0, Convert.ToInt32(stream.Length));
                return data;
            }

        }
        public static string StringDivision(string Message, string Left, string Right)
        {
            if (Message.Contains(Left) && Message.Contains(Right))
            {
                string GetLeftString = Message.Substring(Message.IndexOf(Left) + Left.Length);
                string GetRightString = GetLeftString.Substring(0, GetLeftString.IndexOf(Right));
                return GetRightString;
            }
            else
            {
                return string.Empty;
            }
        }
        public static string ObjToStr(object? Item)
        {
            string GetConvertStr = string.Empty;
            if (Item == null == false)
            {
                GetConvertStr = Item.ToString();
            }
            return GetConvertStr;
        }
        public static int ObjToInt(object Item)
        {
            int Number = -1;
            if (Item == null == false)
            {
                int.TryParse(Item.ToString(), out Number);
            }
            return Number;
        }
        public static double ObjToDouble(object Item)
        {
            double Number = -1;
            if (Item == null == false)
            {
                double.TryParse(Item.ToString(), out Number);
            }
            return Number;
        }
        public static bool ObjToBool(object Item)
        {
            bool Check = false;
            if (Item == null == false)
            {
                Boolean.TryParse(Item.ToString(), out Check);
            }
            return Check;
        }
        public static long ObjToLong(object Item)
        {
            long Number = -1;
            if (Item == null == false)
            {
                long.TryParse(Item.ToString(), out Number);
            }
            return Number;
        }
    }
}
