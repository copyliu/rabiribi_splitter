using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace rabi_splitter_WPF
{
    public class MemoryHelper : IDisposable
    {
        [DllImport("kernel32.dll")]
        public static extern IntPtr OpenProcess(int dwDesiredAccess, bool bInheritHandle, int dwProcessId);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool CloseHandle(IntPtr hObject);

        [DllImport("kernel32.dll")]
        public static extern bool ReadProcessMemory(int hProcess,
            int lpBaseAddress, byte[] lpBuffer, int dwSize, ref int lpNumberOfBytesRead);

        private const int PROCESS_WM_READ = 0x0010;

        private readonly IntPtr processHandle;
        private readonly int processHandleInt;
        private readonly int baseAddress;

        public MemoryHelper(Process process)
        {
            processHandle = OpenProcess(PROCESS_WM_READ, false, process.Id);
            processHandleInt = (int)processHandle;
            baseAddress = process.MainModule.BaseAddress.ToInt32();
        }

        /*
        public static T GetMemoryValue<T>(Process process, int addr, bool baseaddr = true)
        {
            var memoryHelper = new MemoryHelper(process);
            return memoryHelper.GetMemoryValue<T>(addr, baseaddr);
        }
        */

        public T GetMemoryValue<T>(int addr, bool baseaddr = true)
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
                case TypeCode.Single:
                case TypeCode.UInt32:
                    datasize = 4;
                    break;

                case TypeCode.Double:
                case TypeCode.Int64:
                case TypeCode.UInt64:
                    datasize = 8;
                    break;

                default:
                    throw new Exception("not supported");
            }

            byte[] buffer = new byte[datasize];
            int bytesRead = 0;

            if (baseaddr)
            {
                ReadProcessMemory(processHandleInt, baseAddress + addr, buffer, datasize, ref bytesRead);
            }
            else
            {
                ReadProcessMemory(processHandleInt, addr, buffer, datasize, ref bytesRead);
            }

            switch (Type.GetTypeCode(typeof(T)))
            {

                case TypeCode.Boolean:
                    return (T)Convert.ChangeType(buffer[0] == 1, typeof(T));

                case TypeCode.Byte:
                case TypeCode.SByte:
                    return (T)Convert.ChangeType(buffer[0], typeof(T));
                case TypeCode.Char:
                    return (T)Convert.ChangeType((char)buffer[0], typeof(T));

                case TypeCode.Single:
                    return (T)Convert.ChangeType(BitConverter.ToSingle(buffer, 0), typeof(T));

                case TypeCode.Int16:
                    return (T)Convert.ChangeType(BitConverter.ToInt16(buffer, 0), typeof(T));
                case TypeCode.UInt16:
                    return (T)Convert.ChangeType(BitConverter.ToUInt16(buffer, 0), typeof(T));
                case TypeCode.Int32:
                    return (T)Convert.ChangeType(BitConverter.ToInt32(buffer, 0), typeof(T));
                case TypeCode.UInt32:
                    return (T)Convert.ChangeType(BitConverter.ToUInt32(buffer, 0), typeof(T));

                case TypeCode.Double:
                    return (T)Convert.ChangeType(BitConverter.ToDouble(buffer, 0), typeof(T));

                case TypeCode.Int64:
                    return (T)Convert.ChangeType(BitConverter.ToInt64(buffer, 0), typeof(T));
                case TypeCode.UInt64:
                    return (T)Convert.ChangeType(BitConverter.ToUInt64(buffer, 0), typeof(T));

                default:
                    throw new Exception("not supported");
            }

        }


        #region IDisposable Support
        private bool disposed = false;

        protected virtual void DisposeUnmanagedResources()
        {
            if (disposed) return;

            CloseHandle(processHandle);

            disposed = true;
        }

        ~MemoryHelper()
        {
            DisposeUnmanagedResources();
        }

        public void Dispose()
        {
            DisposeUnmanagedResources();
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}
