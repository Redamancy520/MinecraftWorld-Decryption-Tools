using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal
        class Program
    {

     

    static void Main(string[] args)
        
        {
            Console.Title = "SaveFucker | a tool of decrypting MinecraftWorlds ";

            string arg_path = "";

            while (true)
            {
                Console.WriteLine("请输入您存档所在路径:");
                arg_path = Console.ReadLine();
                if (Directory.Exists(arg_path))
                    break;
                Console.WriteLine("路径不存在,请重新输入");
            }
          
            Console.WriteLine(MCStudio.Utils.LevelDbEncryptHelper.DecryptRecord(arg_path));
            Console.WriteLine("解密操作结束\r\n按下回车键退出程序...");
            Console.ReadLine();

        }
    }
}
