using SAD_TOOL.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SAD_TOOL.Utils
{
    public class DllManager
    {
        /// <summary>
        /// INICIO - Importaciones para DLLs
        /// </summary>

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

        /// <summary>
        /// FIN - Importaciones para DLLs
        /// </summary>

        /// <summary>
        /// Parametros clase
        /// </summary>
        public TextBox textBox;

        /// <summary>
        /// Parametros Dll
        /// </summary>
        static IntPtr dllHandle;
        private const uint LOAD_WITH_ALTERED_SEARCH_PATH = 0x00000008;

        private delegate IntPtr getInfoDelegate();
        private delegate IntPtr executeParamDelegate(string[] parametros, int nparams);
        private delegate IntPtr executeDelegate(string xml);

        /// <summary>
        /// Cargar un challenge
        /// </summary>
        public static void LoadDll(String nameChallenge)
        {
            dllHandle = IntPtr.Zero;

            dllHandle = LoadLibrary(nameChallenge);
            //MessageBox.Show(nameChallenge + " - LoadDll: " + dllHandle);
        }

        /// <summary>
        /// Info de un challenge
        /// </summary>
        /// <param name="nameChallenge"></param>
        /// <returns></returns>
        public static String getInfoDll(String nameChallenge)
        {
            string info = "";

            try
            {
                LoadDll(nameChallenge);

                IntPtr addr = GetProcAddress(dllHandle, "getInfo");
                //MessageBox.Show(nameChallenge + " - GetProcAddress: " + addr);

                var func = (getInfoDelegate)Marshal.GetDelegateForFunctionPointer(addr, typeof(getInfoDelegate));
                var retval = func();
                info = Marshal.PtrToStringAnsi(retval);
            }
            catch (Exception ex)
            {
                info = "";
                MessageBox.Show(nameChallenge + " - ex: " + ex.Message);
            }

            FreeLibrary(dllHandle);

            return info;
        }

        /// <summary>
        /// Execute con parametros
        /// </summary>
        /// <param name="pCha"></param>
        /// <param name="listControls"></param>
        /// <returns></returns>
        public static List<string> executeParamDll(Challenge pCha, List<Control> listControls)
        {
            List<string> paramsList = new List<string>();
            List<string> resultList = new List<string>();

            foreach (Control control in listControls)
            {
                if (control is DateTimePicker)
                {
                    if (((DateTimePicker)control).Name.ToLower().Contains(pCha.NameDll.ToLower()))
                    {
                        paramsList.Add(((DateTimePicker)control).Value.ToShortDateString());
                    }
                }
                else if (control is TextBox)
                {
                    if (((TextBox)control).Name.ToLower().Contains(pCha.NameDll.ToLower()))
                    {
                        bool file = true;

                        try
                        {
                            paramsList.Add(System.IO.File.ReadAllText(((TextBox)control).Text.ToString()));
                        }
                        catch (FileNotFoundException)
                        {
                            file = false;
                        }

                        if (!file)
                        {
                            paramsList.Add(((TextBox)control).Text);
                        }
                    }
                }
            }

            try
            {
                //Para que el challenge funcione correctamente
                if (paramsList.Count == 1)
                {
                    paramsList.Add("");
                }

                LoadDll(pCha.NameDll);

                string[] challengeFechaStrings = null;

                IntPtr addr = GetProcAddress(dllHandle, "executeParam");
                //MessageBox.Show(pCha.NameDll + " - GetProcAddress: " + addr);

                var func = (executeParamDelegate)Marshal.GetDelegateForFunctionPointer(addr, typeof(executeParamDelegate));
                var retval = func(paramsList.ToArray(), paramsList.Count);
                UtilsClass.MarshalUnmananagedStrArray2ManagedStrArray(retval, paramsList.Count, out challengeFechaStrings);

                resultList.Add(challengeFechaStrings[0]);
                resultList.Add(challengeFechaStrings[1]);
            }
            catch (Exception ex)
            {
                MessageBox.Show(pCha.NameDll + " - ex: " + ex.Message);
            }

            FreeLibrary(dllHandle);

            return resultList;
        }

        /// <summary>
        /// Execute
        /// </summary>
        /// <param name="pCha"></param>
        /// <returns></returns>
        public static string executeDll(Challenge pCha)
        {
            string result = "";

            try
            {
                LoadDll(pCha.NameDll);

                IntPtr addr = GetProcAddress(dllHandle, "execute");
                //MessageBox.Show(pCha.NameDll + " - GetProcAddress: " + addr);

                var func = (executeDelegate)Marshal.GetDelegateForFunctionPointer(addr, typeof(executeDelegate));
                var retval = func(pCha.ExecuteXml);
                result = Marshal.PtrToStringAnsi(retval);
            }
            catch (Exception ex)
            {
                MessageBox.Show(pCha.NameDll + " - ex: " + ex.Message);
            }

            FreeLibrary(dllHandle);

            return result;
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Llama a getinfo de cada challenge y lee el xml resultante
        /// </summary>
        /// <param name="pList"></param>
        /// <returns></returns>
        public static List<Challenge> fillChallengesInfo(List<Challenge> pList)
        {
            foreach (Challenge cha in pList)
            {
                if (cha.NameDll.Contains(Param.nameChallenge))
                {
                    try
                    {
                        String info = getInfoDll(cha.NameDll);

                        cha.InfoXML = info;

                        //Read xml info
                        ReadXML.readInfo(cha);
                    }
                    catch (Exception ex)
                    {
                        cha.InfoXML = "";
                    }
                }
            }

            return pList;
        }

        /// <summary>
        /// Llama a getinfo de un challenge y lee el xml resultante
        /// </summary>
        /// <param name="pList"></param>
        /// <returns></returns>
        public static Challenge fillChallengesInfo(Challenge cha)
        {
            if (cha.NameDll.Contains(Param.nameChallenge))
            {
                try
                {
                    String info = getInfoDll(cha.NameDll);

                    cha.InfoXML = info;

                    //Read xml info
                    ReadXML.readInfo(cha);
                }
                catch (Exception ex)
                {
                    cha.InfoXML = "";
                }
            }

            return cha;
        }

        /// <summary>
        /// Genera archivo sad segun la lista de challenges selecionados y los parametros creados, 
        /// llama a cada executeParam de cada challenge
        /// </summary>
        /// <param name="chaList"></param>
        /// <param name="listControls"></param>
        /// <param name="pPathFile"></param>
        /// <returns></returns>
        public static bool generateSad(List<Challenge> chaList, List<Control> listControls, String pPathFile)
        {
            string xml = Param.XML_HEAD;
            xml += Param.XML_HEAD_SAD;
            xml += Param.XML_PARAM_HEAD_SAD_HEADER;

            List<string> chaStringResult = new List<string>();
            string chaXmlResult = "";
            string key256String = "";

            bool result = false;

            //Generando respuestas de challenges
            foreach (Challenge cha in chaList)
            {
                if (cha.NameDll.Contains(Param.nameChallenge))
                {
                    try
                    {
                        List<string> listResult = executeParamDll(cha, listControls);

                        chaStringResult.Add(listResult[0]);
                        chaXmlResult += listResult[1];
                    }
                    catch (Exception ex)
                    {
                        result = false;
                    }
                }
                else
                {
                    result = false;
                }
            }

            if (chaStringResult.Count != 0)
            {
                try
                {
                    string ext = Path.GetFileName(pPathFile);
                    string name = Path.GetFileName(pPathFile);
                    name = name.Substring(0, name.IndexOf("."));
                    ext = ext.Substring(ext.IndexOf(".") + 1, ext.Length - (ext.IndexOf(".") + 1));

                    //Generando .sad
                    SADCipherLibraryDll cipehrDll = new SADCipherLibraryDll();
                    key256String = cipehrDll.getMyKeyDll(chaStringResult.Count, chaStringResult.ToArray());
                    IntPtr cifradoPtr = cipehrDll.RC4CipherDll(pPathFile, key256String);
                    int size = cipehrDll.getDocumentSizeDll();

                    xml += Param.XML_PARAM_HEAD_file_size + size + Param.XML_PARAM_FOOTER_file_size;
                    xml += Param.XML_PARAM_FOOTER_SAD_HEADER;

                    byte[] cifradoByteArray = new byte[size];
                    Marshal.Copy(cifradoPtr, cifradoByteArray, 0, size);
                    string cipheredFileBase64String = System.Convert.ToBase64String(cifradoByteArray);

                    string cipheredFileXmlString = Param.XML_PARAM_HEAD_CIPHEREDFILE_BEGIN;
                    cipheredFileXmlString += Param.XML_PARAM_HEAD_CIPHEREDFILE_name + name;
                    cipheredFileXmlString += Param.XML_PARAM_HEAD_CIPHEREDFILE_type + ext;
                    cipheredFileXmlString += Param.XML_PARAM_HEAD_CIPHEREDFILE_END + cipheredFileBase64String + Param.XML_PARAM_FOOTER_CIPHEREDFILE;

                    string fileSad = xml + chaXmlResult + cipheredFileXmlString + Param.XML_FOOTER_SAD;

                    //Guardar .SAD en base64
                    SaveFileDialog saver = new SaveFileDialog();
                    DialogResult LocRes = saver.ShowDialog();
                    if (LocRes == DialogResult.OK)
                    {
                        System.IO.File.WriteAllText(saver.FileName + Param.extSad, fileSad);

                        result = true;
                    }
                    else
                    {
                        result = false;
                    }
                }
                catch (Exception ex)
                {
                    result = false;
                }
            }
            else
            {
                result = false;
            }

            return result;
        }

        /// <summary>
        /// Executar challenge
        /// </summary>
        /// <param name="listCha"></param>
        /// <returns></returns>
        public static List<string> executeAll(List<Challenge> listCha)
        {
            List<string> challAgent = new List<string>();
            List<Challenge> listAllCha = UtilsClass.getAllChallenge();

            foreach (Challenge chaParam in listAllCha)
            {
                foreach (Challenge cha in listCha)
                {
                    if (chaParam.NameDll.ToLower().Contains(cha.Type.ToLower()))
                    {
                        cha.NameDll = chaParam.NameDll;
                        challAgent.Add(executeDll(cha));
                        break;
                    }
                }
            }

            return challAgent;
        }

        //Para challenge pass

        /// <summary>
        /// Mira si es de tipo pass, si es asi lanza popup
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static List<Challenge> isPass(List<Challenge> list)
        {
            foreach (Challenge cha in list)
            {
                if (cha.Type == Param.XML_FILE_ATTR_pass)
                {
                    Challenge chaAux = cha;
                    chaAux.Name = Param.nameChallengePass;
                    chaAux.NameDll = Param.nameChallengePassDll;
                    chaAux = executePass(chaAux);

                    if (chaAux == null)
                    {
                        list = null;
                    }
                    else
                    {
                        list.Remove(cha);
                        list.Add(chaAux);
                    }

                    break;
                }
            }

            return list;
        }

        public static Challenge executePass(Challenge pCha)
        {
            //Lanza popup
            PopUp popup = new PopUp(pCha);
            popup.ShowDialog();

            return popup.cha;
        }

        public static Challenge fillXMLPass(Challenge cha, List<Control> listControls)
        {
            if (cha.Type == Param.XML_FILE_ATTR_pass)
            {
                cha.ExecuteXml = generateXMLPass(listControls);
            }

            return cha;
        }

        public static string generateXMLPass(List<Control> listControls)
        {
            string result = "";

            result = "<CHALLENGE name=\"pass\">";

            foreach (Control control in listControls)
            {
                if (control is TextBox)
                {
                    result += "\n\t\t<PARAM>" + ((TextBox)control).Text + "</PARAM>";
                }
            }

            result += "\n\t</CHALLENGE>";

            return result;
        }
    }
}
