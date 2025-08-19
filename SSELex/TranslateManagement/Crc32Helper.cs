using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSELex.TranslateManagement
{
    public static class Crc32Helper
    {
        private static readonly uint[] Table;
        static Crc32Helper()
        {
            uint poly = 0xEDB88320;
            Table = new uint[256];
            for (uint i = 0; i < Table.Length; ++i)
            {
                uint temp = i;
                for (int j = 0; j < 8; ++j)
                {
                    if ((temp & 1) == 1)
                    {
                        temp = (temp >> 1) ^ poly;
                    }
                    else
                    {
                        temp >>= 1;
                    }
                }
                Table[i] = temp;
            }
        }
        public static string ComputeCrc32(string input)
        {
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(input);
            uint crc = 0xFFFFFFFF;

            foreach (byte b in bytes)
            {
                byte index = (byte)((crc & 0xFF) ^ b);
                crc = (crc >> 8) ^ Table[index];
            }

            crc ^= 0xFFFFFFFF;
            return crc.ToString("X8");
        }
    }
}
