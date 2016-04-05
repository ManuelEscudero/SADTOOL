using Microsoft.Win32;
using SAD_TOOL.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SAD_TOOL.Utils
{
    public class CommandManager
    {

        public bool launchWinApi(string rutaTmeWin, string fileSadExt)
        {
            bool result = false;

            if (!rutaTmeWin.Equals(""))
            {
                try
                {
                    if (this.findInstalApp(Param.rutaFileExe_WinAPIOverride64, false))
                    {
                        string comando = "";

                        if (fileSadExt.Contains(Param.ext_Doc))
                        {
                            ProgramApp programApp = this.findInstalAppRegistry(Param.rutaFileExe_WINWORD, Param.registryOffice, Param.registryValuePath);

                            if (programApp != null)
                            {
                                if (programApp.is64)
                                {
                                    comando = this.getComando(programApp.Path, rutaTmeWin, Param.rutaFileDll_MHook_Def64);
                                }
                                else
                                {
                                    comando = this.getComando(programApp.Path, rutaTmeWin, Param.rutaFileDll_MHook_Def32);
                                }
                            }
                            else
                            {
                                MessageBox.Show("Please, instal Office in your PC.");
                            }
                        }
                        else if (fileSadExt.Contains(Param.ext_Xlsx))
                        {
                            ProgramApp programApp = this.findInstalAppRegistry(Param.rutaFileExe_excel, Param.registryOffice, Param.registryValuePath);

                            if (programApp != null)
                            {
                                if (programApp.is64)
                                {
                                    comando = this.getComando(programApp.Path, rutaTmeWin, Param.rutaFileDll_MHook_Def_XLSX_64);
                                }
                                else
                                {
                                    comando = this.getComando(programApp.Path, rutaTmeWin, Param.rutaFileDll_MHook_Def_XLSX_32);
                                }
                            }
                            else
                            {
                                MessageBox.Show("Please, instal Office in your PC.");
                            }
                        }
                        else if (fileSadExt.Contains(Param.ext_Xls))
                        {
                            ProgramApp programApp = this.findInstalAppRegistry(Param.rutaFileExe_excel, Param.registryOffice, Param.registryValuePath);

                            if (programApp != null)
                            {
                                if (programApp.is64)
                                {
                                    comando = this.getComando(programApp.Path, rutaTmeWin, Param.rutaFileDll_MHook_Def64);
                                }
                                else
                                {
                                    comando = this.getComando(programApp.Path, rutaTmeWin, Param.rutaFileDll_MHook_Def32);
                                }
                            }
                            else
                            {
                                MessageBox.Show("Please, instal Office in your PC.");
                            }
                        }
                        else if (fileSadExt.Contains(Param.ext_Pdf))
                        {
                            ProgramApp programApp = this.findInstalAppRegistry(Param.rutaFileExe_acrobat, Param.registryAdobe, Param.registryValueInstallPath);

                            if (programApp != null)
                            {
                                if (programApp.is64)
                                {
                                    comando = this.getComando(programApp.Path, rutaTmeWin, Param.rutaFileDll_MHook_Def64);
                                }
                                else
                                {
                                    comando = this.getComando(programApp.Path, rutaTmeWin, Param.rutaFileDll_MHook_Def32);
                                }
                            }
                            else
                            {
                                MessageBox.Show("Please, instal Adobe acrobat in your PC.");
                            }
                        }
                        else if (fileSadExt.Contains(Param.ext_Txt))
                        {
                            if (this.findInstalApp(Param.rutaFileExe_editplus, false))
                            {
                                comando = this.getComando(Param.rutaFileExe_editplus, rutaTmeWin, Param.rutaFileDll_MHook_Def32);
                            }
                            else
                            {
                                if (this.findInstalApp(Param.rutaFileExe_note, false))
                                {
                                    comando = this.getComando(Param.rutaFileExe_note, rutaTmeWin, Param.rutaFileDll_MHook_Def_NOTE_64);
                                }
                                else
                                {
                                    MessageBox.Show("Please, instal edit plus or note pad in your PC.");
                                }
                            }
                        }

                        if (!comando.Equals(""))
                        {
                            result = this.ExecuteCommandCmd(comando);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Error WinApi Install.");
                    }
                }
                catch (Exception)
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

        private bool findInstalApp(string pVisor, bool p64)
        {
            return File.Exists(pVisor);
        }

        public ProgramApp geAdobeKey(string app)
        {
            ProgramApp programApp = null;

            try
            {
                string registry_key = @"SOFTWARE\Wow6432Node\" + app + "\\";
                RegistryKey key = Registry.LocalMachine.OpenSubKey(registry_key);

                string[] subKeyNames = key.GetSubKeyNames();
                if (subKeyNames.Length != 0)
                {
                    List<RegistryKey> listRegistryKeys = new List<RegistryKey>();

                    foreach (string subkey_name in subKeyNames)
                    {
                        RegistryKey subkey = key.OpenSubKey(subkey_name);

                        if (subkey_name.Equals(Param.rutaApp_AdobeAcrobat))
                        {
                            string[] subKeyNames2 = subkey.GetSubKeyNames();
                            foreach (string subkey_name2 in subKeyNames2)
                            {
                                listRegistryKeys.Add(subkey.OpenSubKey(subkey_name2));
                            }

                            break;
                        }
                    }

                    foreach (RegistryKey registryKey in listRegistryKeys)
                    {
                        bool exist = false;

                        subKeyNames = registryKey.GetSubKeyNames();
                        foreach (string subkey_name in subKeyNames)
                        {
                            RegistryKey registryKeySub = registryKey.OpenSubKey(subkey_name);
                            var path = (string)registryKeySub.GetValue("Path");

                            if (path != null)
                            {
                                programApp = new ProgramApp();
                                programApp.Name = app;
                                programApp.Path = path;

                                exist = true;
                                break;
                            }
                        }

                        if (exist)
                        {
                            break;
                        }
                    }
                }
                else
                {
                    programApp = null;
                }
            }
            catch (Exception)
            {
                programApp = null;
            }

            return programApp;
        }


        public ProgramApp geMicrosoftKey(string app)
        {
            ProgramApp programApp = null;

            try
            {
                string registry_key = @"SOFTWARE\Wow6432Node\" + app + "\\";
                RegistryKey key = Registry.LocalMachine.OpenSubKey(registry_key);

                string[] subKeyNames = key.GetSubKeyNames();
                if (subKeyNames.Length != 0)
                {
                    List<RegistryKey> listRegistryKeys = new List<RegistryKey>();

                    foreach (string subkey_name in subKeyNames)
                    {
                        RegistryKey subkey = key.OpenSubKey(subkey_name);

                        if (subkey_name.Equals(Param.rutaApp_AdobeAcrobat))
                        {
                            string[] subKeyNames2 = subkey.GetSubKeyNames();
                            foreach (string subkey_name2 in subKeyNames2)
                            {
                                listRegistryKeys.Add(subkey.OpenSubKey(subkey_name2));
                            }

                            break;
                        }
                    }

                    foreach (RegistryKey registryKey in listRegistryKeys)
                    {
                        bool exist = false;

                        subKeyNames = registryKey.GetSubKeyNames();
                        foreach (string subkey_name in subKeyNames)
                        {
                            RegistryKey registryKeySub = registryKey.OpenSubKey(subkey_name);
                            var path = (string)registryKeySub.GetValue("Path");

                            if (path != null)
                            {
                                programApp = new ProgramApp();
                                programApp.Name = app;
                                programApp.Path = path;

                                exist = true;
                                break;
                            }
                        }

                        if (exist)
                        {
                            break;
                        }
                    }
                }
                else
                {
                    programApp = null;
                }
            }
            catch (Exception)
            {
                programApp = null;
            }

            return programApp;
        }

        /// <summary>
        /// Encontrar la ruta de una app en el registro 2
        /// </summary>
        /// <param name="pVisor"></param>
        /// <returns></returns>
        private ProgramApp findInstalAppRegistryAux(string pVisor)
        {
            ProgramApp result = null;
            List<ProgramApp> programs = new List<ProgramApp>();

            try
            {
                //Saco programas instalados                
                string registry_key = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall";
                using (Microsoft.Win32.RegistryKey key = Registry.LocalMachine.OpenSubKey(registry_key))
                {
                    foreach (string subkey_name in key.GetSubKeyNames())
                    {
                        using (RegistryKey subkey = key.OpenSubKey(subkey_name))
                        {
                            var name = (string)subkey.GetValue("DisplayName");
                            var pathInstall = (string)subkey.GetValue("InstallLocation");
                            if (!string.IsNullOrEmpty(name))
                            {
                                ProgramApp programApp = new ProgramApp();
                                programApp.Name = name;
                                programApp.Path = pathInstall;
                                programs.Add(programApp);
                            }
                        }
                    }
                }
            }
            catch (Exception) { }

            try
            {
                //Saco programas instalados
                string registry_key = @"SOFTWARE\Wow6432Node\Microsoft\Windows\CurrentVersion\Uninstall";
                using (Microsoft.Win32.RegistryKey key = Registry.LocalMachine.OpenSubKey(registry_key))
                {
                    foreach (string subkey_name in key.GetSubKeyNames())
                    {
                        using (RegistryKey subkey = key.OpenSubKey(subkey_name))
                        {
                            var name = (string)subkey.GetValue("DisplayName");
                            var pathInstall = (string)subkey.GetValue("InstallLocation");
                            if (!string.IsNullOrEmpty(name))
                            {
                                ProgramApp programApp = new ProgramApp();
                                programApp.Name = name;
                                programApp.Path = pathInstall;
                                programs.Add(programApp);
                            }
                        }
                    }
                }
            }
            catch (Exception) { }

            //De los programas instalados segun extension averiguar el path
            foreach (ProgramApp program in programs)
            {
                if (program.Path != null && !program.Path.Equals(""))
                {
                    if (pVisor.ToLower().Contains(Param.pro_WORLD.ToLower()) || pVisor.ToLower().Contains(Param.pro_EXCEL.ToLower()))
                    {
                        if (program.Name.ToLower().Contains("Microsoft Office".ToLower()))
                        {
                            int pos = program.Path.LastIndexOf("\\");
                            if (pos != 0 && program.Path.Length > 0 && pos >= program.Path.Length - 1)
                            {
                                program.Path = program.Path.Substring(0, program.Path.Length - 1);
                            }

                            if (program.Name.ToLower().Contains("2013".ToLower()))
                            {
                                program.Path = program.Path + "\\" + Param.rutaFileExe_OFFICE + "15" + "\\" + pVisor + ".exe";

                                result = program;
                                break;
                            }
                            else if (program.Name.ToLower().Contains("2010".ToLower()))
                            {
                                program.Path = program.Path + "\\" + Param.rutaFileExe_OFFICE + "14" + "\\" + pVisor + ".exe";

                                result = program;
                                break;
                            }
                            else if (program.Name.ToLower().Contains("2007".ToLower()))
                            {
                                program.Path = program.Path + "\\" + Param.rutaFileExe_OFFICE + "12" + "\\" + pVisor + ".exe";

                                result = program;
                                break;
                            }
                            else if (program.Name.ToLower().Contains("2003".ToLower()))
                            {
                                program.Path = program.Path + "\\" + Param.rutaFileExe_OFFICE + "11" + "\\" + pVisor + ".exe";

                                result = program;
                                break;
                            }
                        }
                    }
                    else if (pVisor.ToLower().Contains(Param.pro_ACROBAT.ToLower()))
                    {
                        if (program.Name.ToLower().Contains(pVisor.ToLower()))
                        {
                            int pos = program.Path.LastIndexOf("\\");
                            if (pos != 0 && pos >= program.Path.Length - 1)
                            {
                                program.Path = program.Path.Substring(0, program.Path.Length - 1);
                            }

                            program.Path = program.Path + "\\" + Param.rutaFileExe_ACROBAT + "\\" + pVisor + ".exe";

                            result = program;
                            break;
                        }
                    }
                    else if (program.Name.ToLower().Contains(pVisor.ToLower()))
                    {
                        int pos = program.Path.LastIndexOf("\\");
                        if (pos != 0 && pos >= program.Path.Length - 1)
                        {
                            program.Path = program.Path.Substring(0, program.Path.Length - 1);
                        }

                        program.Path = program.Path + "\\" + pVisor + ".exe";

                        result = program;
                        break;
                    }
                }
            }

            if (!UtilsClass.fileExist(result.Path))
            {
                result.Path = "";
            }

            return result;
        }

        /// <summary>
        /// Encontrar la ruta de una app en el registro
        /// </summary>
        /// <param name="pVisor"></param>
        /// <param name="pRegistry"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        private ProgramApp findInstalAppRegistry(string pVisor, string pRegistry, string value)
        {
            ProgramApp result = new ProgramApp();
            result.Name = pVisor;

            //Saco el path del programa
            result.Path = searchValueInRegistry(pVisor, pRegistry, value, "inicio");
            if (result.Path.Contains("inicio") || result.Path.Contains("fin"))
            {
                if (pVisor.Equals(Param.rutaFileExe_WINWORD) || pVisor.Equals(Param.rutaFileExe_excel))
                {
                    result.Path = searchValueInRegistry(pVisor, Param.registryOffice2, value, "inicio");
                }
                else if (pVisor.Equals(Param.rutaFileExe_acrobat))
                {
                    result.Path = searchValueInRegistry(Param.rutaFileExe_reader, Param.registryAdobe2, value, "inicio");
                    if (result.Path.Contains("inicio") || result.Path.Contains("fin"))
                    {
                        result.Path = searchValueInRegistry(Param.rutaFileExe_reader, Param.registryAdobe3, value, "inicio");
                    }
                }

                if (result.Path.Contains("inicio") || result.Path.Contains("fin"))
                {
                    ProgramApp resultAux = new ProgramApp();
                    resultAux = findInstalAppRegistryAux(pVisor);
                    result.Path = resultAux.Path;
                }
            }

            MessageBox.Show("Path: " + result.Path);

            //Saco si el office es de 64 o 32
            //result.is64 = isOffice64(pRegistry, Param.registryValueBitness, Param.officex64.ToLower(), false);
            result.is64 = isApp64(result.Path);

            return result;
        }

        private string findPathOffice()
        {
            return "";
        }

        /// <summary>
        /// Buscar un valor en un registro
        /// </summary>
        /// <param name="registry"></param>
        /// <param name="registrySubKey"></param>
        /// <param name="value"></param>
        /// <param name="is64"></param>
        /// <returns></returns>
        private string searchValueInRegistry(string pVisor, string registry, string registrySubKey, string returnValue)
        {
            try
            {
                if (returnValue.Equals("inicio"))
                {
                    Microsoft.Win32.RegistryKey key = Registry.LocalMachine.OpenSubKey(registry);

                    if (key != null)
                    {
                        string[] listSubKeys = key.GetSubKeyNames();
                        foreach (string subKey in listSubKeys)
                        {
                            if (returnValue.Equals("inicio"))
                            {
                                RegistryKey subkeyRegistry = key.OpenSubKey(subKey);

                                //Para adobe
                                if (subkeyRegistry.Name.Contains(registrySubKey))
                                {
                                    returnValue = (string)subkeyRegistry.GetValue("");

                                    returnValue = returnValue + "\\";
                                    returnValue = returnValue.Replace("\\\\", "\\");
                                    returnValue = returnValue + pVisor + ".exe";

                                    if (UtilsClass.fileExist(returnValue))
                                    {
                                        break;
                                    }
                                    else
                                    {
                                        returnValue = "inicio";
                                    }
                                }

                                var name = (string)subkeyRegistry.GetValue(registrySubKey);
                                if (name != null)
                                {
                                    returnValue = name;

                                    returnValue = returnValue + "\\";
                                    returnValue = returnValue.Replace("\\\\", "\\");
                                    returnValue = returnValue + pVisor + ".exe";

                                    if (UtilsClass.fileExist(returnValue))
                                    {
                                        break;
                                    }
                                    else
                                    {
                                        returnValue = "inicio";
                                    }
                                }

                                if (returnValue.Equals("inicio"))
                                {
                                    string regAux = registry + "\\";
                                    regAux = regAux.Replace("\\\\", "\\");
                                    regAux = regAux + subKey;

                                    returnValue = searchValueInRegistry(pVisor, regAux, registrySubKey, returnValue);
                                }
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                    else
                    {
                        returnValue = "fin";
                    }
                }
            }
            catch (Exception)
            {
                returnValue = "fin";
            }

            return returnValue;
        }

        /// <summary>
        ///  Para ver si la app es de 64 o 32
        /// </summary>
        /// <param name="registry"></param>
        /// <param name="registrySubKey"></param>
        /// <param name="value"></param>
        /// <param name="is64"></param>
        /// <returns></returns>
        private bool isApp64(string pPath)
        {
            bool is64 = false;

            try
            {
                //is64 = ExecuteCommandCmdString(getComandoIs64(pPath));
                is64 = searchApp64InString(pPath);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error isApp64: " + ex.Message);
                is64 = false;
            }

            return is64;
        }

        private bool searchApp64InString(string pPath)
        {
            bool is64 = false;

            byte[] pattern64 = new byte[] { 80, 69, 00, 00, 100, 134 };
            byte[] pattern86 = new byte[] { 80, 69, 00, 00, 76 };

            byte[] bytesFile = File.ReadAllBytes(pPath);

            List<int> positions64 = SearchBytePattern(pattern64, bytesFile);
            List<int> positions86 = SearchBytePattern(pattern86, bytesFile);
            if (positions64.Count == 1 && positions86.Count == 0)
            {
                is64 = true;
            }

            return is64;
        }

        static public List<int> SearchBytePattern(byte[] pattern, byte[] bytes)
        {
            List<int> positions = new List<int>();
            int patternLength = pattern.Length;
            int totalLength = bytes.Length;
            byte firstMatchByte = pattern[0];
            for (int i = 0; i < totalLength; i++)
            {
                if (firstMatchByte == bytes[i] && totalLength - i >= patternLength)
                {
                    byte[] match = new byte[patternLength];
                    Array.Copy(bytes, i, match, 0, patternLength);
                    if (match.SequenceEqual<byte>(pattern))
                    {
                        positions.Add(i);
                        i += patternLength - 1;
                    }
                }
            }
            return positions;
        }

        /// <summary>
        /// Para ver si el office es de 64 o 32
        /// </summary>
        /// <param name="registry"></param>
        /// <param name="registrySubKey"></param>
        /// <param name="value"></param>
        /// <param name="is64"></param>
        /// <returns></returns>
        private bool isOffice64(string registry, string registrySubKey, string value, bool is64)
        {
            try
            {
                if (!is64)
                {
                    Microsoft.Win32.RegistryKey key = Registry.LocalMachine.OpenSubKey(registry);

                    if (key != null)
                    {
                        string[] listSubKeys = key.GetSubKeyNames();
                        foreach (string subKey in listSubKeys)
                        {
                            if (!is64)
                            {
                                RegistryKey subkeyRegistry = key.OpenSubKey(subKey);
                                var name = (string)subkeyRegistry.GetValue(registrySubKey);
                                if (name != null)
                                {
                                    if (name.ToLower().Contains(value))
                                    {
                                        is64 = true;
                                        break;
                                    }
                                }

                                if (!is64)
                                {
                                    string regAux = registry + "\\";
                                    regAux = regAux.Replace("\\\\", "\\");
                                    regAux = regAux + subKey;

                                    is64 = isOffice64(regAux, registrySubKey, value, is64);
                                }
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                    else
                    {
                        is64 = false;
                    }
                }
            }
            catch (Exception)
            {
                is64 = false;
            }

            return is64;
        }

        private string getComandoIs64(string pPath)
        {
            //pesnoop "C:\Program Files (x86)\Microsoft Office\Office12\EXCEL.EXE" /pe_dh

            //string comando = "\"" + Param.rutaFileExe_Pesnoop + "\"" + " " + "\"" + pPath + "\"" + " /PE_DH";
            string comando = "pesnoop " + "\"" + pPath + "\"" + " /PE_DH";

            return comando;
        }

        /// <summary>
        /// Comando para el winapi
        /// </summary>
        /// <param name="pVisor"></param>
        /// <param name="rutaTmeWin"></param>
        /// <param name="pHook"></param>
        /// <returns></returns>
        private string getComando(string pVisor, string rutaTmeWin, string pHook)
        {
            string comando = "\"" + Param.rutaFileExe_WinAPIOverride64 + "\""
                                + " AppPath=\"" + pVisor
                                + "\"" + " AppCmdLine=\"" + rutaTmeWin + "\""
                                + " NoGUI"
                                + " OverridingDlls=\""
                                + pHook + "\"";

            //Pruebas Con interfaz winapi
            /*string comando = "\"" + Param.rutaFileExe_WinAPIOverride64 + "\""
                                           + " AppPath=\"" + pVisor
                                           + "\"" + " AppCmdLine=\"" + rutaTmeWin + "\""
                                           + " OverridingDlls=\""
                                           + pHook + "\"";*/

            //Pruebas sin hook
            /*string comando = "\"" + Param.rutaFileExe_WinAPIOverride64 + "\""
                                            + " AppPath=\"" + pVisor
                                            + "\"" + " AppCmdLine=\"" + rutaTmeWin + "\""
                                            + " NoGUI";*/

            //Pruebas sin winapi
            /*string comando = "\"" + pVisor + "\"" + rutaTmeWin;*/

            return comando;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="comando"></param>
        /// <returns></returns>
        private bool ExecuteCommandCmdString(string comando)
        {
            bool is64 = false;
            string result = "";

            try
            {
                System.Diagnostics.Process proc = new System.Diagnostics.Process();
                proc.EnableRaisingEvents = false;
                proc.StartInfo.FileName = Param.rutaFileExe_Pesnoop;
                proc.StartInfo.RedirectStandardInput = true;
                proc.StartInfo.RedirectStandardOutput = true;
                proc.StartInfo.CreateNoWindow = true;
                proc.StartInfo.UseShellExecute = false;
                proc.Start();
                proc.StandardInput.WriteLine(comando);

                result = proc.StandardOutput.ReadToEnd();

                proc.StandardInput.Flush();
                proc.StandardInput.Close();
                proc.Close();
            }
            catch (Exception)
            {
                result = "";
            }

            //TODO Mirar result

            return is64;
        }

        /// <summary>
        /// Ejecutar comando en cmd
        /// </summary>
        /// <param name="comando"></param>
        /// <returns></returns>
        private bool ExecuteCommandCmd(string comando)
        {
            bool result = true;

            try
            {
                System.Diagnostics.Process proc = new System.Diagnostics.Process();
                proc.EnableRaisingEvents = false;
                proc.StartInfo.FileName = "cmd";
                proc.StartInfo.RedirectStandardInput = true;
                proc.StartInfo.RedirectStandardOutput = true;
                proc.StartInfo.CreateNoWindow = true;
                proc.StartInfo.UseShellExecute = false;
                proc.Start();
                proc.StandardInput.WriteLine(comando);
                proc.StandardInput.Flush();
                proc.StandardInput.Close();
                proc.Close();
            }
            catch (Exception)
            {
                result = false;
            }

            return result;
        }
    }
}
