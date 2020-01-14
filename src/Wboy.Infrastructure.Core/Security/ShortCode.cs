using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Wboy.Infrastructure.Core.Security
{
    /// <summary>
    /// 字符串生成短码
    /// Copyright (C) sumxiang
    /// </summary>
    public class ShortCode
    {
        /// <summary>
        /// 将字符串生成对应的6位短码字符串
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string GetShortCode(string str)
        {
            var codeArray = Encrypto(str);
            return codeArray[new Random().Next(0, codeArray.Length)];
        }
        public static string GetShortLength4Code(string str)
        {
            var codeArray = Encrypto(str, 4);
            return codeArray[new Random().Next(0, codeArray.Length)];
        }

        public static string[] Encrypto(string str, int length = 6)
        {
            string[] chars = new string[] {"a" , "b" , "c" , "d" , "e" , "f" , "g" , "h" ,
                "i" , "j" , "k" , "l" , "m" , "n" , "o" , "p" , "q" , "r" , "s" , "t" ,
                "u" , "v" , "w" , "x" , "y" , "z" , "0" , "1" , "2" , "3" , "4" , "5" ,
                "6" , "7" , "8" , "9" , "A" , "B" , "C" , "D" , "E" , "F" , "G" , "H" ,
                "I" , "J" , "K" , "L" , "M" , "N" , "O" , "P" , "Q" , "R" , "S" , "T" ,
                "U" , "V" , "W" , "X" , "Y" , "Z"};
            string value = StringMd5(str);
            //Debug.Write("MD5加密之后字符串: " + value + "\n字符串长度" + value.Length);

            string[] results = new string[value.Length / 8];
            for (int i = 0; i < value.Length / 8; i++)
            {
                //把加密字符按照8位一组16进制与0x3FFFFFFF进行位与运算
                string tempResult = value.Substring(i * 8, 8);
                long lHexLong = 0x3FFFFFFF & long.Parse(tempResult);
                string outchar = "";
                for (int j = 0; j < length; j++)
                {
                    //把得到的值与0x0000003D进行位与运算，取得字符数组chars索引
                    long index = 0x0000003D & lHexLong;
                    //把取得的字符相加
                    outchar += chars[(int)index];
                    //每次循环按位右移5位
                    lHexLong = lHexLong >> 5;
                }
                //把字符串存入对应索引的输出数组
                results[i] = outchar;
            }
            return results;
        }
        public static string StringMd5(string str)
        {
            //string pwd = "";
            //MD5 md5 = MD5.Create();
            //byte[] s = md5.ComputeHash(Encoding.UTF8.GetBytes(str));
            //for (int i = 0; i < s.Length; i++)
            //{
            //    pwd = pwd + s[i];
            //}
            //return pwd;//等效下面代码

            MD5 md5 = MD5.Create();
            byte[] s = md5.ComputeHash(Encoding.UTF8.GetBytes(str));
            return s.Aggregate("", (current, t) => current + t);
        }
    }
}
