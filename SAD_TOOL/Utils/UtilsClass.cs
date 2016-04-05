using SAD_TOOL.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SAD_TOOL.Utils
{
    public class UtilsClass
    {
        /// <summary>
        /// Da la lista de challenges de la carpeta donde esten
        /// </summary>
        /// <returns></returns>
        public static List<Challenge> getAllChallenge()
        {
            List<Challenge> list = null;
            string[] filePaths = namesFilesDir(Param.rutaDlls);

            if (filePaths != null && filePaths.Length != 0)
            {
                list = new List<Challenge>();

                foreach (string path in filePaths)
                {
                    if (path.Contains(Param.nameChallenge))
                    {
                        Challenge challenge = new Challenge();
                        challenge.NameDll = System.IO.Path.GetFileName(path);

                        list.Add(challenge);
                    }
                }
            }

            return list;
        }

        /// <summary>
        /// Informacion 1 challenge
        /// </summary>
        /// <param name="list"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static Challenge getChallengeByName(List<Challenge> list, string name)
        {
            Challenge result = null;

            foreach (Challenge cha in list)
            {
                if (cha.NameDll.Contains(name))
                {
                    result = cha;
                    break;
                }
            }

            return result;
        }

        /// <summary>
        /// Nombre archivos de una carpeta
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string[] namesFilesDir(String path)
        {
            string[] filePaths = null;

            try
            {
                if (Directory.Exists(path))
                {
                    filePaths = Directory.GetFiles(path);
                }
                else
                {
                    filePaths = null;
                }
            }
            catch (Exception)
            {
                filePaths = null;
            }

            return filePaths;
        }

        /// <summary>
        /// Crear directorio
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static bool createDir(String path)
        {
            bool ok = false;

            try
            {
                if (!Directory.Exists(path))
                {
                    System.IO.Directory.CreateDirectory(path);
                }

                ok = true;
            }
            catch (Exception)
            {
                ok = false;

            }

            return ok;
        }

        /// <summary>
        /// Generar tabla de challenges
        /// </summary>
        /// <param name="list"></param>
        /// <param name="_dgCars"></param>
        /// <returns></returns>
        public static DataGridView generateTable(List<Challenge> list, DataGridView _dgCars)
        {
            _dgCars.AutoGenerateColumns = false;

            //DataGridViewCheckBoxColumn column0 = new DataGridViewCheckBoxColumn();
            // column0.Width = 50;

            DataGridViewTextBoxColumn column1 = new DataGridViewTextBoxColumn();
            column1.Width = 200;
            column1.Name = "Name";
            column1.DataPropertyName = "Name";
            column1.HeaderText = "Name";
            column1.SortMode = DataGridViewColumnSortMode.Programmatic;

            DataGridViewTextBoxColumn column2 = new DataGridViewTextBoxColumn();
            column2.Width = 50;
            column2.Name = "Version";
            column2.DataPropertyName = "Version";
            column2.HeaderText = "Version";
            column2.SortMode = DataGridViewColumnSortMode.Programmatic;

            DataGridViewTextBoxColumn column3 = new DataGridViewTextBoxColumn();
            column3.Width = 255;
            column3.Name = "Description";
            column3.DataPropertyName = "Description";
            column3.HeaderText = "Description";
            column3.SortMode = DataGridViewColumnSortMode.Programmatic;

            //_dgCars.Columns.Add(column0);
            _dgCars.Columns.Add(column1);
            _dgCars.Columns.Add(column2);
            _dgCars.Columns.Add(column3);

            BindingList<Challenge> challenges = new BindingList<Challenge>();
            foreach (Challenge cha in list)
            {
                challenges.Add(cha);
            }

            _dgCars.DataSource = challenges;

            return _dgCars;
        }

        /// <summary>
        /// Generar parametros segun challenges selecionados
        /// </summary>
        /// <param name="pChaList"></param>
        /// <param name="pPanel"></param>
        /// <returns></returns>
        public static Panel generateParamPanel(FormWizard form, List<Challenge> pChaList)
        {
            int count = 0;

            Panel panel = form.groupBoxParam;

            ParamGlobal.GetInstance().listTextBox = new List<TextBox>();

            foreach (Challenge pCha in pChaList)
            {
                foreach (ParamList param in pCha.ParamList)
                {
                    if (param.type.Contains(Param.XML_FILE_ATTR_calendar))
                    {
                        Label label = new Label();
                        label.Name = pCha.NameDll + "Label" + count;
                        label.Text = param.name;
                        label.Location = new Point(6, 25 + (count * 25));

                        DateTimePicker date = new DateTimePicker();
                        date.Name = pCha.NameDll + "DateTimePicker" + count;
                        date.Location = new Point(label.Width + 10, 25 + (count * 25));

                        panel.Controls.Add(label);
                        panel.Controls.Add(date);
                    }
                    else if (param.type.Contains(Param.XML_FILE_ATTR_file))
                    {
                        Label label = new Label();
                        label.Name = pCha.NameDll + "Label" + count;
                        label.Text = param.name;
                        label.Location = new Point(6, 25 + (count * 25));

                        TextBox texBox = new TextBox();
                        texBox.Name = pCha.NameDll + "TextBox" + count;
                        texBox.Text = "";
                        texBox.Location = new Point(label.Width + 10, 25 + (count * 25));
                        texBox.Width = 250;
                        texBox.Enabled = false;

                        Button button = new Button();
                        button.Name = pCha.NameDll + "Button" + count;
                        button.Text = "Search";
                        button.Location = new Point(label.Width + 10 + texBox.Width + 10, 25 + (count * 25));
                        button.Click += new EventHandler(button_Click);

                        panel.Controls.Add(label);
                        panel.Controls.Add(texBox);
                        panel.Controls.Add(button);

                        //ParamGlobal.GetInstance().textBox = texBox;
                        ParamGlobal.GetInstance().listTextBox.Add(texBox);
                    }
                    else if (param.type.Contains(Param.XML_FILE_ATTR_text))
                    {
                        Label label = new Label();
                        label.Name = pCha.NameDll + "Label" + count;
                        label.Text = param.name;
                        label.Location = new Point(6, 25 + (count * 25));

                        TextBox texBox = new TextBox();
                        texBox.Name = pCha.NameDll + "TextBox" + count;
                        texBox.Text = "";
                        texBox.Location = new Point(label.Width + 10, 25 + (count * 25));
                        texBox.Width = 400;
                        texBox.Enabled = true;

                        panel.Controls.Add(label);
                        panel.Controls.Add(texBox);
                    }

                    count++;
                }
            }

            return panel;
        }

        public static GroupBox generateParamPassPanel(PopUp form, Challenge pCha)
        {
            int count = 0;

            GroupBox panel = form.groupBoxParam;

            foreach (ParamList param in pCha.ParamList)
            {
                if (param.type.Contains(Param.XML_FILE_ATTR_text))
                {
                    Label label = new Label();
                    label.Name = "Label" + count;
                    label.Text = param.name;
                    label.Location = new Point(6, 25 + (count * 25));

                    TextBox texBox = new TextBox();
                    texBox.Name = "TextBox" + count;
                    texBox.Text = "";
                    texBox.Location = new Point(label.Width + 10, 25 + (count * 25));
                    texBox.Width = 400;
                    texBox.Enabled = true;

                    panel.Controls.Add(label);
                    panel.Controls.Add(texBox);
                }

                count++;
            }

            return panel;
        }

        protected static void button_Click(object sender, EventArgs e)
        {
            OpenFileDialog loader = new OpenFileDialog();
            DialogResult locRes = loader.ShowDialog();
            if (locRes == DialogResult.OK)
            {
                string nameButton = ((Button)sender).Name;
                nameButton = nameButton.Substring(0, nameButton.IndexOf("Button"));

                List<TextBox> listTexts = ParamGlobal.GetInstance().listTextBox;
                foreach (TextBox tex in listTexts)
                {
                    string nameTextBox = ((TextBox)tex).Name;
                    nameTextBox = nameTextBox.Substring(0, nameTextBox.IndexOf("TextBox"));

                    if (nameButton.Equals(nameTextBox))
                    {
                        FileManager fileManager = new FileManager();
                        fileManager.writeFile(loader.FileName, tex);

                        break;
                    }
                }
            }
        }

        /// <summary>
        /// Tratar respuesta challenge
        /// </summary>
        /// <param name="pUnmanagedStringArray"></param>
        /// <param name="StringCount"></param>
        /// <param name="ManagedStringArray"></param>
        public static void MarshalUnmananagedStrArray2ManagedStrArray(IntPtr pUnmanagedStringArray, int StringCount, out string[] ManagedStringArray)
        {
            IntPtr[] pIntPtrArray = new IntPtr[StringCount];
            ManagedStringArray = new string[StringCount];

            Marshal.Copy(pUnmanagedStringArray, pIntPtrArray, 0, StringCount);

            for (int i = 0; i < StringCount; i++)
            {
                ManagedStringArray[i] = Marshal.PtrToStringAnsi(pIntPtrArray[i]);
            }
        }

        /// <summary>
        /// Elimina file
        /// </summary>
        public static void removeFile(string pPath)
        {
            string folder = Path.GetDirectoryName(pPath);
            string name = Path.GetFileName(pPath);
            string ext = Path.GetExtension(pPath);

            string[] filePaths = UtilsClass.namesFilesDir(folder);
            if (filePaths != null && filePaths.Length != 0)
            {
                foreach (string path in filePaths)
                {
                    if (path.ToLower().Contains(name.ToLower()))
                    {
                        try
                        {
                            File.Delete(path);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("ERROR removeFile, " + path + ", men: " + ex.Message);
                        }
                    }
                }
            }

            File.Delete(Param.rutaLogTmp);
        }

        public static bool removeFileDes(string pPath)
        {
            bool result = false;

            string folder = Path.GetDirectoryName(pPath);
            string name = Path.GetFileName(pPath);
            string ext = Path.GetExtension(pPath);

            string[] filePaths = UtilsClass.namesFilesDir(folder);
            if (filePaths != null && filePaths.Length != 0)
            {
                foreach (string path in filePaths)
                {
                    if (path.ToLower().Contains(name.ToLower()))
                    {
                        try
                        {
                            string nameOfFile = Path.GetFileName(path);

                            if (nameOfFile.Contains(name))
                            {
                                File.Delete(path);
                                result = true;
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("ERROR removeFileDes, " + path + ", men: " + ex.Message);
                            result = false;
                        }
                    }
                }
            }

            return result;
        }

        public static bool endAddAllowNoDenyFile(string folder, string name)
        {
            bool result = false;

            string[] filePaths = UtilsClass.namesFilesDir(folder);
            if (filePaths != null && filePaths.Length != 0)
            {
                foreach (string path in filePaths)
                {
                    if (path.ToLower().Contains(name.ToLower()))
                    {
                        try
                        {
                            string nameOfFile = Path.GetFileName(path);

                            if (nameOfFile.Contains(name))
                            {
                                FileManager filemanager = new FileManager();
                                filemanager.addAllowNoDenyFile(path);
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("ERROR endAddAllowNoDenyFile, " + path + ", men: " + ex.Message);
                            result = false;
                        }
                    }
                }
            }

            return result;
        }

        public static bool removeFileAux(string pPath)
        {
            bool result = false;

            string folder = Path.GetDirectoryName(pPath);
            string name = Path.GetFileName(pPath);
            string ext = Path.GetExtension(pPath);

            string[] filePaths = UtilsClass.namesFilesDir(folder);
            if (filePaths != null && filePaths.Length != 0)
            {
                foreach (string path in filePaths)
                {
                    if (path.ToLower().Contains(name.ToLower()))
                    {
                        try
                        {
                            File.Delete(path);
                            result = true;
                        }
                        catch (Exception e)
                        {
                            MessageBox.Show("Error Delete Challenge, Men: " + e.Message);
                        }
                    }
                }
            }

            return result;
        }

        public static void killProcess()
        {
            try
            {
                foreach (Process Proc in Process.GetProcesses())
                {
                    if (Proc.ProcessName.ToLower().Contains(Param.pro_EXCEL.ToLower()))
                    {
                        DateTime starTimeProcess = Proc.StartTime;
                        if (starTimeProcess >= ParamGlobal.GetInstance().startApp)
                        {
                            Proc.Kill();
                        }
                    }
                    else if (Proc.ProcessName.ToLower().Contains(Param.pro_WORLD.ToLower()))
                    {
                        DateTime starTimeProcess = Proc.StartTime;
                        if (starTimeProcess >= ParamGlobal.GetInstance().startApp)
                        {
                            Proc.Kill();
                        }
                    }
                    else if (Proc.ProcessName.ToLower().Contains(Param.pro_EDIT.ToLower()))
                    {
                        DateTime starTimeProcess = Proc.StartTime;
                        if (starTimeProcess >= ParamGlobal.GetInstance().startApp)
                        {
                            Proc.Kill();
                        }
                    }
                    else if (Proc.ProcessName.ToLower().Contains(Param.pro_ACROBAT.ToLower()))
                    {
                        DateTime starTimeProcess = Proc.StartTime;
                        if (starTimeProcess >= ParamGlobal.GetInstance().startApp)
                        {
                            Proc.Kill();
                        }
                    }
                    else if (Proc.ProcessName.ToLower().Contains(Param.pro_WINAPI.ToLower()))
                    {
                        DateTime starTimeProcess = Proc.StartTime;
                        if (starTimeProcess >= ParamGlobal.GetInstance().startApp)
                        {
                            Proc.Kill();
                        }
                    }
                    else if (Proc.ProcessName.ToLower().Contains(Param.pro_NOTE.ToLower()))
                    {
                        DateTime starTimeProcess = Proc.StartTime;
                        if (starTimeProcess >= ParamGlobal.GetInstance().startApp)
                        {
                            Proc.Kill();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR killProcess, cerrar proceso, men: " + ex.Message);
            }
        }

        public static void waitApp(int millisecondsToWait)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            while (true)
            {
                if (stopwatch.ElapsedMilliseconds >= millisecondsToWait)
                {
                    break;
                }
            }
        }

        public static void endAllApp()
        {
            waitApp(500);
            killProcess();

            try
            {
                int count = 0;
                bool ok = false;
                while (!ok && count < 3)
                {
                    waitApp(500);
                    ok = UtilsClass.removeFileDes(Param.rutaWinTmp + Param.tmpName);
                    count = count + 1;
                }

                count = 0;
                ok = false;
                while (!ok && count < 3)
                {
                    waitApp(500);
                    ok = UtilsClass.removeFileDes(Application.StartupPath + "\\" + Param.desName);
                    count = count + 1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR endAllApp, borrado, men: " + ex.Message);
            }

            waitApp(500);
        }

        public static bool fileExist(string pPath)
        {
            return File.Exists(pPath);
        }
        public static bool waitProcess(DateTime startApp, string fileSadExt)
        {
            bool exist = false;
            string processAux = "";

            if (fileSadExt.Contains(Param.ext_Doc))
            {
                processAux = Param.pro_WORLD;
            }
            else if (fileSadExt.Contains(Param.ext_Txt))
            {
                processAux = Param.pro_EDIT;
            }
            else if (fileSadExt.Contains(Param.ext_Pdf))
            {
                processAux = Param.pro_ACROBAT;
            }
            else if (fileSadExt.Contains(Param.ext_Xls))
            {
                processAux = Param.pro_EXCEL;
            }

            if (!processAux.Equals(""))
            {
                foreach (Process Proc in Process.GetProcesses())
                {
                    if (Proc.ProcessName.ToLower().Contains(processAux.ToLower()))
                    {
                        DateTime starTimeProcess = Proc.StartTime;
                        if (starTimeProcess >= startApp)
                        {
                            exist = true;
                            break;
                        }
                    }
                }
            }

            return exist;
        }

        public static bool copyInitFiles()
        {
            bool result = true;

            string source = Application.StartupPath + Param.rutaDLL_TOOL + Param.nameSADCipherLibraryDll;
            string target = Param.rutaSystem32 + Param.nameSADCipherLibraryDll;
            if (!File.Exists(target))
            {
                if (!UtilsClass.copyFile(source, target))
                {
                    result = false;
                }
            }

            source = Application.StartupPath + Param.rutaDLL_TOOL + Param.nameChallengeFechaDll;
            target = Param.rutaSystem32 + Param.nameChallengeFechaDll;
            if (!File.Exists(target))
            {
                if (!UtilsClass.copyFile(source, target))
                {
                    result = false;
                }
            }

            source = Application.StartupPath + Param.rutaDLL_TOOL + Param.nameChallengePassDll;
            target = Param.rutaSystem32 + Param.nameChallengePassDll;
            if (!File.Exists(target))
            {
                if (!UtilsClass.copyFile(source, target))
                {
                    result = false;
                }
            }

            source = Application.StartupPath + Param.rutaDLL_TOOL + Param.nameChallengeIntraDll;
            target = Param.rutaSystem32 + Param.nameChallengeIntraDll;
            if (!File.Exists(target))
            {
                if (!UtilsClass.copyFile(source, target))
                {
                    result = false;
                }
            }

            source = Application.StartupPath + Param.rutaDLL_TOOL + Param.nameChallengeFingerprintDLL;
            target = Param.rutaSystem32 + Param.nameChallengeFingerprintDLL;
            if (!File.Exists(target))
            {
                if (!UtilsClass.copyFile(source, target))
                {
                    result = false;
                }
            }

            if (!result)
            {
                target = Param.rutaSystem32 + Param.nameSADCipherLibraryDll;
                removeFile(target);

                target = Param.rutaSystem32 + Param.nameChallengeFechaDll;
                removeFile(target);

                target = Param.rutaSystem32 + Param.nameChallengePassDll;
                removeFile(target);

                target = Param.rutaSystem32 + Param.nameChallengeIntraDll;
                removeFile(target);

                target = Param.rutaSystem32 + Param.nameChallengeFingerprintDLL;
                removeFile(target);
            }

            return result;
        }

        public static bool copyFile(string sourcePath, string targetPath)
        {
            bool resutl = false;

            try
            {
                File.Copy(sourcePath, targetPath, true);

                FileManager filemanager = new FileManager();
                filemanager.addPermissionFile(targetPath);

                resutl = true;
            }
            catch (Exception ex)
            {
                resutl = false;
            }

            return resutl;
        }

        /// <summary>
        /// Filas selecionadas en tabla
        /// </summary>
        /// <returns></returns>
        public static List<Challenge> treatCellClick(DataGridView dataGridViewChallenges)
        {
            List<Challenge> list = new List<Challenge>();

            DataGridViewColumnCollection columns = dataGridViewChallenges.Columns;

            string nameChallenge = "";

            DataGridViewSelectedRowCollection selectedRows = dataGridViewChallenges.SelectedRows;

            foreach (DataGridViewRow row in selectedRows)
            {
                for (int i = 0; i < columns.Count; i++)
                {
                    string name = columns[i].Name;
                    DataGridViewCellCollection cells = row.Cells;

                    if (name.ToLower().Contains("Name".ToLower()))
                    {
                        nameChallenge = (string)cells[i].Value;
                        Challenge aux = UtilsClass.getChallengeByName(ParamGlobal.GetInstance().listChallenges, nameChallenge);

                        list.Add(aux);

                        break;
                    }
                }
            }

            return list;
        }

        public static List<Challenge> treatCellClickCheck(DataGridView dataGridViewChallenges)
        {
            List<Challenge> list = new List<Challenge>();

            DataGridViewColumnCollection columns = dataGridViewChallenges.Columns;
            DataGridViewRowCollection rows = dataGridViewChallenges.Rows;

            string nameChallenge = "";

            foreach (DataGridViewRow row in rows)
            {
                bool check = false;

                for (int i = 0; i < columns.Count; i++)
                {
                    string name = columns[i].Name;
                    DataGridViewCellCollection cells = row.Cells;

                    if (name.ToLower().Contains("Column0".ToLower()))
                    {
                        try
                        {
                            check = (bool)cells[i].Value;
                        }
                        catch (Exception)
                        {
                            check = false;
                        }

                    }

                    if (name.ToLower().Contains("Name".ToLower()))
                    {
                        if (check)
                        {
                            nameChallenge = (string)cells[i].Value;
                            Challenge aux = UtilsClass.getChallengeByName(ParamGlobal.GetInstance().listChallenges, nameChallenge);

                            list.Add(aux);

                            break;
                        }
                    }
                }
            }

            return list;
        }
    }
}
