using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace rabiribi_splitter
{
    public  static  class MemoryHelper
    {
        [DllImport("kernel32.dll")]
        public static extern IntPtr OpenProcess(int dwDesiredAccess, bool bInheritHandle, int dwProcessId);

        [DllImport("kernel32.dll")]
        public static extern bool ReadProcessMemory(int hProcess,
            int lpBaseAddress, byte[] lpBuffer, int dwSize, ref int lpNumberOfBytesRead);

        private const int PROCESS_WM_READ = 0x0010;

        public static T GetMemoryValue<T>(Process process, int addr)
        {
            int datasize;
            switch (Type.GetTypeCode(typeof(T)))
            {

                case TypeCode.Boolean:
                case TypeCode.Byte:
                case TypeCode.Char:
                case TypeCode.SByte:
                    datasize = 1;
                    break;
                case TypeCode.Int16:
                case TypeCode.UInt16:
                    datasize = 2;
                    break;
                case TypeCode.Int32:

                case TypeCode.UInt32:
                    datasize = 4;
                    break;
                case TypeCode.Int64:
                case TypeCode.UInt64:
                    datasize = 8;
                    break;
                default:
                    throw new Exception("not supported");
            }

            byte[] buffer = new byte[datasize];
            int bytesRead = 0;
            IntPtr processHandle = OpenProcess(PROCESS_WM_READ, false, process.Id);
            ReadProcessMemory((int) processHandle, process.MainModule.BaseAddress.ToInt32() + addr, buffer,
                datasize, ref bytesRead);
            switch (Type.GetTypeCode(typeof(T)))
            {

                case TypeCode.Boolean:
                    return (T)Convert.ChangeType( buffer[0] == 1,typeof(T));

                case TypeCode.Byte:
                case TypeCode.SByte:
                    return (T)Convert.ChangeType(buffer[0] , typeof(T));
                case TypeCode.Char:
                    return (T)Convert.ChangeType((char)buffer[0], typeof(T));


                case TypeCode.Int16:
               
                    return (T)Convert.ChangeType(BitConverter.ToInt16(buffer,0), typeof(T));
                   
                case TypeCode.UInt16:
                    return (T)Convert.ChangeType(BitConverter.ToUInt16(buffer, 0), typeof(T));
                case TypeCode.Int32:
                    return (T)Convert.ChangeType(BitConverter.ToInt32(buffer, 0), typeof(T));
                case TypeCode.UInt32:
                    return (T)Convert.ChangeType(BitConverter.ToUInt32(buffer, 0), typeof(T));
                   
                case TypeCode.Int64:
                    return (T)Convert.ChangeType(BitConverter.ToInt64(buffer, 0), typeof(T));
                case TypeCode.UInt64:
                    return (T)Convert.ChangeType(BitConverter.ToUInt64(buffer, 0), typeof(T));
                    
                default:
                    throw new Exception("not supported");
            }

        }


    }
}
