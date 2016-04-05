using SAD_TOOL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SAD_TOOL.Utils
{
    public class SADCipherLibraryDll
    {
        [DllImport("kernel32.dll")]
        static extern bool SetDllDirectory(string lpPathName);

        [DllImport("kernel32.dll")]
        static extern bool AddDllDirectory(string lpPathName);

        [DllImport("Kernel32.dll")]
        private static extern IntPtr LoadLibrary(string path);

        [DllImport("Kernel32.dll")]
        private static extern IntPtr LoadLibraryEx(string dllFilePath, IntPtr hFile, uint dwFlags);

        [DllImport("Kernel32.dll")]
        public extern static bool FreeLibrary(IntPtr dllPointer);

        [DllImport("Kernel32.dll")]
        private static extern IntPtr GetProcAddress(IntPtr hModule, string procName);

        private delegate IntPtr getMyKeyDelegate(int nchallenges, string[] returnofchallenges);
        private delegate IntPtr RC4CipherDelegate(string filePath, string key);
        private delegate IntPtr RC4DecipherDelegate(byte[] fileCiphered, int fileCipheredSize, string key);
        private delegate int getDocumentSizeDelegate();

        static IntPtr dllHandle;
        private const uint LOAD_WITH_ALTERED_SEARCH_PATH = 0x00000008;

        public SADCipherLibraryDll()
        {
            //this.LoadDll();
        }

        public void LoadDll()
        {
            this.SetAddDirectotryDll();

            dllHandle = IntPtr.Zero;

            dllHandle = LoadLibrary("SADCipherLibrary.dll");
            //MessageBox.Show("SadCipther - LoadLibrary: " + dllHandle);
        }

        public void SetAddDirectotryDll()
        {
            //bool setOk = SetDllDirectory(Param.rutaDlls + "SADCipherLibrary.dll");
            //MessageBox.Show("SadCipther - SetDllDirectory: " + setOk);

            //bool addOk = AddDllDirectory(Param.rutaDlls + "SADCipherLibrary.dll");
            //MessageBox.Show("SadCipther - AddDllDirectory: " + addOk);
        }

        public String getMyKeyDll(int nchallenges, string[] returnofchallenges)
        {
            string result = "";

            try
            {
                this.LoadDll();

                IntPtr addr = GetProcAddress(dllHandle, "getMyKey");
                //MessageBox.Show("SadCipther - GetProcAddress: " + addr);

                var func = (getMyKeyDelegate)Marshal.GetDelegateForFunctionPointer(addr, typeof(getMyKeyDelegate));
                //var retval = func(nchallenges, returnofchallenges.ToArray());
                var retval = func(nchallenges, returnofchallenges);
                result = Marshal.PtrToStringAnsi(retval);
            }
            catch (Exception ex)
            {
                result = "";
                MessageBox.Show("SadCipther - ex: " + ex.Message);
            }

            return result;
        }

        public IntPtr RC4CipherDll(string filePath, string key)
        {
            IntPtr result = IntPtr.Zero;

            try
            {
                this.LoadDll();

                IntPtr addr = GetProcAddress(dllHandle, "RC4Cipher");
                //MessageBox.Show("SadCipther - GetProcAddress: " + addr);

                var func = (RC4CipherDelegate)Marshal.GetDelegateForFunctionPointer(addr, typeof(RC4CipherDelegate));
                var retval = func(filePath, key);
                result = retval;
            }
            catch (Exception ex)
            {
                result = IntPtr.Zero;
                MessageBox.Show("SadCipther - ex: " + ex.Message);
            }

            return result;
        }

        public IntPtr RC4DecipherDll(byte[] fileCiphered, int fileCipheredSize, string key)
        {
            IntPtr result = IntPtr.Zero;

            try
            {
                this.LoadDll();

                IntPtr addr = GetProcAddress(dllHandle, "RC4Decipher");
                //MessageBox.Show("SadCipther - GetProcAddress: " + addr);

                var func = (RC4DecipherDelegate)Marshal.GetDelegateForFunctionPointer(addr, typeof(RC4DecipherDelegate));
                var retval = func(fileCiphered, fileCipheredSize, key);
                result = retval;
            }
            catch (Exception ex)
            {
                result = IntPtr.Zero;
                MessageBox.Show("SadCipther - ex: " + ex.Message);
            }

            return result;
        }

        public int getDocumentSizeDll()
        {
            int result = 0;

            try
            {
                this.LoadDll();

                IntPtr addr = GetProcAddress(dllHandle, "getDocumentSize");
                //MessageBox.Show("SadCipther - GetProcAddress: " + addr);

                var func = (getDocumentSizeDelegate)Marshal.GetDelegateForFunctionPointer(addr, typeof(getDocumentSizeDelegate));
                var retval = func();
                result = retval;
            }
            catch (Exception ex)
            {
                result = 0;
                MessageBox.Show("SadCipther - ex: " + ex.Message);
            }

            return result;
        }

        public void freeLibraryDll()
        {
            FreeLibrary(dllHandle);
        }
    }
}
