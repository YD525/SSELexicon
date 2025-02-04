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

namespace YDSkyrimToolR.ConvertManager
{
    public class ConvertHelper
    {

        public static string FileToBase64String(string FilePath)
        {
            FileStream fsForRead = new FileStream(FilePath, FileMode.Open);//文件路径
            string base64Str = "";
            try
            {
                //读写指针移到距开头10个字节处
                fsForRead.Seek(0, SeekOrigin.Begin);
                byte[] bs = new byte[fsForRead.Length];
                int log = Convert.ToInt32(fsForRead.Length);
                //从文件中读取10个字节放到数组bs中
                fsForRead.Read(bs, 0, log);
                base64Str = Convert.ToBase64String(bs);
                return base64Str;
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                Console.ReadLine();
                return base64Str;
            }
            finally
            {
                fsForRead.Close();
            }
        }
        public static bool Base64StringToFile(string base64String, string TargetPath,string TargetName)
        {
            bool opResult = false;
            try
            {
                if (!Directory.Exists(TargetPath))
                {
                    Directory.CreateDirectory(TargetPath);
                }

                string strbase64 = base64String.Trim().Substring(base64String.IndexOf(",") + 1);   //将‘，’以前的多余字符串删除
                MemoryStream stream = new MemoryStream(Convert.FromBase64String(strbase64));
                FileStream fs = new FileStream(TargetPath + "\\" + TargetName, FileMode.OpenOrCreate, FileAccess.Write);
                byte[] b = stream.ToArray();
                fs.Write(b, 0, b.Length);
                fs.Close();

                opResult = true;
            }
            catch (Exception e)
            {
              
            }
            return opResult;
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
        public static double GetRate(double A, double B)
        {
            double Value = (A / B);
            var T1 = Math.Round(Value, 2); 
            return  T1 * 100; 
        }
        public static double RoundDouble(double v, int x)
        {
            return ChinaRound(v, x);
        }
        public static double ChinaRound(double value, int decimals)
        {
            if (value < 0)
            {
                return Math.Round(value + 5 / Math.Pow(10, decimals + 1), decimals, MidpointRounding.AwayFromZero);
            }
            else
            {
                return Math.Round(value, decimals, MidpointRounding.AwayFromZero);
            }
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
        public static int MorningOrNoon(DateTime SetTime)
        {
            var GetTime = SetTime;
            if (GetTime.Hour > 10)
            {
                if (GetTime.Hour <= 11)
                {
                    return 1;
                }
                else
                if (GetTime.Hour <= 16)
                {
                    return 2;
                }
                else
                if (GetTime.Hour <= 20)
                {
                    return 3;
                }
                else
                {
                    return 0;
                }
            }
            else
            {
                return 0;
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
        public static string GetStringNoEmp(string Message)
        {
            return Message.Replace(" ", "").Replace("    ", "").Replace("　", "");
        }
        public static string ObjToStr(object Item)
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
            long Number = 0;
            if (Item == null == false)
            {
                long.TryParse(Item.ToString(), out Number);
            }
            return Number;
        }


    }
}
