using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class Program
    {


        static void Main(string[] args)
        {

            Console.Title = "SaveFucker | a tool of decrypting MinecraftWorlds ";

            string arg_path = "";
            bool pass = true;

            while (true)
            {

                pass = true;

                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("请输入您存档所在路径(请确保存档路径中不含有任何中文及其字符)");
                Console.ForegroundColor = ConsoleColor.Gray;
                arg_path = Console.ReadLine();


                if (!Directory.Exists(arg_path))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("路径不存在,请重新输入");
                    Console.ForegroundColor = ConsoleColor.Gray;
                }
                else
                {
                    for (int i = 0; i < arg_path.Length; i++)
                    {
                        if (Regex.IsMatch(arg_path[i].ToString(), @"[\u4e00-\u9fbb]+"))
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("存档路径含有中文 请命名成英文 或 将存档保存到其他非中文的路径");
                            Console.ForegroundColor = ConsoleColor.Gray;
                            pass = false;
                            break;
                        }

                    }

                    if (pass)
                        break;



                }









            }



            Console.WriteLine(MCStudio.Utils.LevelDbEncryptHelper.DecryptRecord(arg_path));
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("解密操作结束\r\n按下回车键退出程序...");
            Console.ReadLine();

        }








    }
}
