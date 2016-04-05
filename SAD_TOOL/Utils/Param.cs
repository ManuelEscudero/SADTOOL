using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SAD_TOOL.Utils
{
    public static class Param
    {
        //Rutas
        public static string rutaWinTmp = "C:\\Windows\\Temp\\";
        public static string rutaLogTmp = Application.StartupPath + "\\logC.txt";
        public static string rutaSystem32 = "C:\\Windows\\System32\\";
        public static string rutaDlls = "C:\\Windows\\System32\\";
        public static string rutaFileExe_WinAPIOverride64 = Application.StartupPath + "\\APP_TOOL\\WinAPIOverride64.exe";
        public static string rutaFileExe_Pesnoop = Application.StartupPath + "\\APP_TOOL\\PESnoop.exe";
        public static string rutaFileExe_WINWORD = "WINWORD";
        public static string rutaFileExe_editplus = "C:\\Program Files (x86)\\EditPlus 3\\editplus.exe";
        public static string rutaFileExe_note = "C:\\Windows\\notepad.exe";
        public static string rutaFileExe_acrobat = "Acrobat";
        public static string rutaFileExe_reader = "AcroRd32";
        public static string rutaFileExe_excel = "EXCEL";
        public static string rutaFileDll_MHook_Def64 = Application.StartupPath + "\\DLL_TOOL\\MHook_Def264.dll";
        public static string rutaFileDll_MHook_Def32 = Application.StartupPath + "\\DLL_TOOL\\MHook_Def2.dll";
        public static string rutaFileDll_MHook_Def_XLSX_64 = Application.StartupPath + "\\DLL_TOOL\\MHook_Xlsx64.dll";
        public static string rutaFileDll_MHook_Def_XLSX_32 = Application.StartupPath + "\\DLL_TOOL\\MHook_Xlsx.dll";
        public static string rutaFileDll_MHook_Def_NOTE_64 = Application.StartupPath + "\\DLL_TOOL\\MHook_Note64.dll";
        public static string rutaFileDll_MHook_Def_NOTE_32 = Application.StartupPath + "\\DLL_TOOL\\MHook_Note.dll";

        public static string rutaDLL_TOOL = "\\DLL_TOOL\\";
        public static string rutaTMP_TOOL = "\\TMP_TOOL\\";

        public static string rutaFileExe_OFFICE = "Office";
        public static string rutaFileExe_ACROBAT = "Acrobat";

        public static string officex64 = "x64";

        public static string registryOffice = @"SOFTWARE\Microsoft\Office\";
        public static string registryOffice2 = @"SOFTWARE\Wow6432Node\Microsoft\Office\";
        public static string registryAdobe = @"SOFTWARE\Adobe\Adobe Acrobat\";
        public static string registryAdobe2 = @"SOFTWARE\Adobe\Acrobat Reader\";
        public static string registryAdobe3 = @"SOFTWARE\Wow6432Node\Adobe\Acrobat Reader\";

        public static string registryValueBitness = "Bitness";
        public static string registryValuePath = "Path";
        public static string registryValuePredeterminado = "(Predeterminado)";
        public static string registryValueInstallPath = "InstallPath";

        //Apps
        public static string rutaApp_Adobe = "Adobe";
        public static string rutaApp_AdobeAcrobat = "Adobe Acrobat";
        public static string rutaApp_Microsoft = "Microsoft";
        public static string rutaApp_Office = "Office";
        public static string rutaApp_Notepad = "Notepad";
        public static string rutaRegistry_Path = "Path";

        //Pruebas
        /*public static string rutaWinTmp = "C:\\Windows\\Temp\\";
        public static string rutaLogTmp = "C:\\Tmp\\logC.txt";
        public static string rutaDlls = Application.StartupPath + "\\Dlls\\";
        public static string rutaFileExe_WinAPIOverride64 = "C:\\Program Files (x86)\\winapioverride32_bin_6.5.1_Beta1\\WinAPIOverride64.exe";
        public static string rutaFileExe_WINWORD = "C:\\Program Files\\Microsoft Office\\Office14\\WINWORD.EXE";
        public static string rutaFileExe_editplus = "C:\\Program Files (x86)\\EditPlus 3\\editplus.exe";
        public static string rutaFileExe_note = "C:\\Windows\\notepad.exe";
        public static string rutaFileExe_acrobat = "C:\\Program Files (x86)\\Adobe\\Acrobat 10.0\\Acrobat\\Acrobat.exe";
        public static string rutaFileExe_excel = "C:\\Program Files\\Microsoft Office\\Office14\\EXCEL.EXE";
        public static string rutaFileDll_MHook_Def64 = "C:\\Users\\Manuel Escodero\\Documents\\Visual Studio 2012\\Projects\\Sad\\MHook_Def2\\Debug64\\MHook_Def264.dll";
        public static string rutaFileDll_MHook_Def_XLSX_64 = "C:\\Users\\Manuel Escodero\\Documents\\Visual Studio 2012\\Projects\\Sad\\MHook_Xlsx\\Debug64\\MHook_Xlsx64.dll";
        public static string rutaFileDll_MHook_Def32 = "C:\\Users\\Manuel Escodero\\Documents\\Visual Studio 2012\\Projects\\Sad\\MHook_Def2\\Debug\\MHook_Def2.dll";*/

        //Varios
        public static string tmpName = "sad";
        public static string desName = "descifrado";

        //Extensiones
        public static string extSad = ".sad";
        public static string extSadc = ".sadc";
        public static string extDll = ".dll";

        public static string ext_Doc = "doc";
        public static string ext_Txt = "txt";
        public static string ext_Pdf = "pdf";
        public static string ext_Xls = "xls";
        public static string ext_Xlsx = "xlsx";

        //Proecsos
        public static string pro_EXCEL = "EXCEL";
        public static string pro_WORLD = "WORD";
        public static string pro_EDIT = "PLUS";
        public static string pro_ACROBAT = "ACRO";
        public static string pro_WINAPI = "WINAPI";
        public static string pro_NOTE = "NOTE";

        //Tipo pagina wizard
        public const int pagSelectedCha = 0;
        public const int pagInfoCha = 1;
        public const int pagParamCha = 2;

        //Nombre Challenges
        public static string nameChallengeFecha = "Fecha";
        public static string nameChallengePass = "Pass";
        public static string nameChallengeIntra = "Intranet";
        public static string nameChallengeFechaDll = "ChallengeFechaDll.dll";
        public static string nameChallengePassDll = "ChallengePassDll.dll";
        public static string nameChallengeIntraDll = "ChallengeIntranetDLL.dll";
        public static string nameSADCipherLibraryDll = "SADCipherLibrary.dll";
        public static string nameChallengeFingerprintDLL = "ChallengeFingerprintDLL.dll";
        public static string nameChallenge = "Challenge";

        //XML sad
        public static string XML_HEAD = "<?xml version=\"1.0\" encoding=\"utf-16\"?>";
        public static string XML_HEAD_SAD = "\n<SAD>";
        public static string XML_PARAM_HEAD_SAD_HEADER = "\n\t<SAD_HEADER>";
        public static string XML_PARAM_HEAD_CIPHEREDFILE_BEGIN = "\n\t<CIPHEREDFILE ";
        public static string XML_PARAM_HEAD_CIPHEREDFILE_name = "name=\"";
        public static string XML_PARAM_HEAD_CIPHEREDFILE_type = "\" type=\"";
        public static string XML_PARAM_HEAD_CIPHEREDFILE_END = "\">\n\t\t";
        public static string XML_PARAM_HEAD_file_size = "\n\t\t<PARAM name=\"file_size\">";
        public static string XML_PARAM_HEAD_ext = "\n\t\t<PARAM name=\"ext\">";
        public static string XML_FOOTER_SAD = "\n</SAD>";
        public static string XML_PARAM_FOOTER_SAD_HEADER = "\n\t</SAD_HEADER>";
        public static string XML_PARAM_FOOTER_file_size = "</PARAM>";
        public static string XML_PARAM_FOOTER_ext = "</PARAM>";
        public static string XML_PARAM_FOOTER_CIPHEREDFILE = "\n\t</CIPHEREDFILE>";

        //XML Info
        public static string XML_INFO_PLUGIN_NAME = "PLUGIN_NAME";
        public static string XML_INFO_DESCRIPTION = "DESCRIPTION";
        public static string XML_INFO_RELEASE = "RELEASE";
        public static string XML_INFO_PARAM_LIST = "PARAM_LIST";
        public static string XML_INFO_ATTR_type = "type";
        public static string XML_INFO_ATTR_name = "name";

        public static string XML_FILE_SAD_HEADER = "SAD_HEADER";
        public static string XML_FILE_CIPHEREDFILE = "CIPHEREDFILE";
        public static string XML_FILE_CHALLENGE = "CHALLENGE";
        public static string XML_FILE_ATTR_file_size = "file_size";
        public static string XML_FILE_ATTR_ext = "ext";
        public static string XML_FILE_ATTR_name = "name";
        public static string XML_FILE_ATTR_type = "type";
        public static string XML_FILE_ATTR_calendar = "calendar";
        public static string XML_FILE_ATTR_file = "file";
        public static string XML_FILE_ATTR_text = "text";
        public static string XML_FILE_ATTR_pass = "pass";
    }

}
