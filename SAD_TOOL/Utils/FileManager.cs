using SAD_TOOL.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.AccessControl;
using System.Security.Permissions;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Management;
using System.Management.Instrumentation;

namespace SAD_TOOL.Utils
{
    public class FileManager
    {
        public TextBox textBox;
        public FileInfo fi;
        public ThreadStart delegadoOpenTCP;
        public Thread hiloOpenTCP;
        public List<String> listAccounts;
        public byte[] descifradoByteArray;

        /// <summary>
        /// Carga fichero SAD
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="textBox"></param>
        public void loadFileSad(string filePath, TextBox textBox)
        {
            this.textBox = textBox;

            if (System.IO.Path.GetExtension(filePath).Equals(Param.extSad))
            {
                string fileSad = System.IO.File.ReadAllText(filePath);
                this.textBox.Text += "\r\n" + "File OK: " + filePath + "\r\n";

                //Recuperar size
                int fileSadSize = ReadXML.getSizeFromSad(fileSad);
                this.textBox.Text += "\r\n" + "File size: " + fileSadSize + "\r\n";

                //Recuperar extension
                string fileSadExt = ReadXML.getExtFromSad(fileSad);
                this.textBox.Text += "\r\n" + "File ext: " + fileSadExt + "\r\n";

                //Recuperar name
                string fileSadName = ReadXML.getNameFromSad(fileSad);
                this.textBox.Text += "\r\n" + "File name: " + fileSadExt + "\r\n";

                //Challenges del sad
                string base64CipheredFile = ReadXML.getBase64CipheredFile(fileSad);
                List<Challenge> listCha = ReadXML.getChallengesFromSad(fileSad);
                this.textBox.Text += "\r\n" + "Recover file B64/Challenges" + "\r\n";

                //Mirar si es pass
                listCha = DllManager.isPass(listCha);

                //Si nulo algo mal en pass
                if (listCha == null)
                {
                    MessageBox.Show("Not its posible continue.");
                }
                else
                {
                    //Bytes archivo
                    this.descifradoByteArray = new byte[fileSadSize];
                    this.descifradoByteArray = System.Convert.FromBase64String(base64CipheredFile);
                    this.textBox.Text += "\r\n" + "Recover file bytes: " + this.descifradoByteArray.Length + "\r\n";

                    //Por cada challenge miramos si pasa el challenge
                    List<string> challAgent = DllManager.executeAll(listCha);
                    this.textBox.Text += "\r\n" + "Challenges execute" + "\r\n";

                    if (challAgent.Count != 0)
                    {
                        SADCipherLibraryDll cipher = new SADCipherLibraryDll();

                        string key256String = cipher.getMyKeyDll(challAgent.Count, challAgent.ToArray());
                        this.textBox.Text += "\r\n" + "Key ok" + "\r\n";

                        //copiamos archivo
                        byte[] fileOk = new byte[fileSadSize];
                        IntPtr descifradoPtr = cipher.RC4DecipherDll(this.descifradoByteArray, fileSadSize, key256String);
                        Marshal.Copy(descifradoPtr, fileOk, 0, fileSadSize);
                        this.textBox.Text += "\r\n" + "Key/File ok: " + fileOk.Length + "\r\n";

                        //Ponemos este archivo en temporal de windos
                        String rutaTmeWin = Param.rutaWinTmp + Param.tmpName + "." + fileSadExt;
                        if (UtilsClass.fileExist(rutaTmeWin))
                        {
                            UtilsClass.killProcess();
                            this.fileSadExit(rutaTmeWin);
                            UtilsClass.removeFile(rutaTmeWin);
                        }

                        FileManager fileMana = new FileManager();
                        bool okWrite = fileMana.byteArrayToFileStream(rutaTmeWin, fileOk);

                        UtilsClass.removeFileDes(Application.StartupPath + "\\" + Param.desName);

                        if (okWrite)
                        {
                            Thread.Sleep(500);

                            this.textBox.Text += "\r\n" + "Open ... " + rutaTmeWin + "\r\n";

                            //Abrir editor hookeado
                            if (this.openFile(rutaTmeWin, fileSadExt))
                            {
                                this.textBox.Text += "\r\n" + "Open file Ok" + "\r\n";

                                Thread.Sleep(2000);

                                //Para asegurar que el proceso lanzo el editor
                                int count = 0;
                                bool existProcess = UtilsClass.waitProcess(ParamGlobal.GetInstance().startApp, fileSadExt);
                                while (!existProcess && count < 5)
                                {
                                    count++;
                                    Thread.Sleep(500);
                                    existProcess = UtilsClass.waitProcess(ParamGlobal.GetInstance().startApp, fileSadExt);
                                }

                                if (!existProcess)
                                {
                                    UtilsClass.removeFileDes(rutaTmeWin);

                                    this.textBox.Text += "\r\n" + "ERROR open editor" + "\r\n";
                                }
                                else
                                {
                                    Thread.Sleep(2000);

                                    //Dar permisos
                                    if (this.startPermissions(rutaTmeWin))
                                    {
                                        this.textBox.Text += "\r\n" + "Permissins file Ok" + "\r\n";
                                    }
                                    else
                                    {
                                        this.textBox.Text += "\r\n" + "ERROR Permissins file" + "\r\n";
                                    }
                                }                           
                            }
                            else
                            {
                                UtilsClass.removeFileDes(rutaTmeWin);

                                this.textBox.Text += "\r\n" + "ERROR open file" + "\r\n";
                            }
                        }
                        else
                        {
                            this.textBox.Text += "\r\n" + "ERROR File not found" + "\r\n";
                        }

                        cipher.freeLibraryDll();
                    }
                    else
                    {
                        this.textBox.Text += "\r\n" + "ERROR Challenges no execute" + "\r\n";
                    }
                }
            }
            else
            {
                this.textBox.Text += "\r\n" + "ERROR Unrecognized File" + "\r\n";
            }

            this.textBox.SelectionStart = this.textBox.Text.Length;
            this.textBox.ScrollToCaret();
        }

        public bool startPermissions(string rutaTmeWin)
        {
            bool result = false;

            try
            {
                this.fi = new FileInfo(rutaTmeWin);

                //Cambiar permisos del archivo
                this.changePermissions();

                this.textBox.Text += "\r\n" + "Check Permissions ... " + "\r\n";

                //Empezar a comprobar ese archivo
                this.delegadoOpenTCP = new ThreadStart(this.checkPermissions);
                this.hiloOpenTCP = new Thread(this.delegadoOpenTCP);
                this.hiloOpenTCP.Start();

                result = true;
            }
            catch (Exception)
            {
                result = false;
            }

            return result;
        }

        public bool openFile(string rutaTmeWin, string fileSadExt)
        {
            CommandManager com = new CommandManager();
            return com.launchWinApi(rutaTmeWin, fileSadExt);
        }

        /// <summary>
        /// Carga fichero Dll
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="textBox"></param>
        public void loadFileDll(string filePath, TextBox textBox)
        {
            this.textBox = textBox;

            if (System.IO.Path.GetExtension(filePath).Equals(Param.extDll))
            {
                if (UtilsClass.createDir(Param.rutaDlls))
                {
                    byte[] fileArray = System.IO.File.ReadAllBytes(filePath);

                    //Ponemos este archivo la arpeta de windos de las dlls
                    String rutaDll = Param.rutaDlls + System.IO.Path.GetFileName(filePath);
                    bool okWrite = this.byteArrayToFileStream(rutaDll, fileArray);

                    if (okWrite)
                    {
                        this.addPermissionFile(rutaDll);

                        this.textBox.Text += "\r\n" + "Challenge load " + filePath + "\r\n";
                    }
                    else
                    {
                        this.textBox.Text += "\r\n" + "ERROR File not found " + filePath + "\r\n";
                    }
                }
                else
                {
                    this.textBox.Text += "\r\n" + "ERROR Folder not found " + filePath + "\r\n";
                }
            }
            else
            {
                this.textBox.Text += "\r\n" + "ERROR Unrecognized File " + filePath + "\r\n";
            }

            this.textBox.SelectionStart = this.textBox.Text.Length;
            this.textBox.ScrollToCaret();
        }

        /// <summary>
        /// Finaliza hilo, permisos y borra archivo
        /// </summary>
        public void endApp()
        {
            try
            {
                this.hiloOpenTCP.Interrupt();
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR endApp interrupt, men: " + ex.Message);
            }

            try
            {
                //Quitar deny, poner allow
                this.addPermission();
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR endApp permission, men: " + ex.Message);
            }

            //EndAllApp
            UtilsClass.endAllApp();

            Application.ExitThread();
            Application.Exit();
        }

        /// <summary>
        /// Finaliza hilo, permisos y borra archivo
        /// </summary>
        public void endAppExit()
        {
            try
            {
                if (this.hiloOpenTCP != null)
                {
                    this.hiloOpenTCP.Interrupt();
                    this.hiloOpenTCP = null;
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show("ERROR al cerrar el hilo: " + ex.Message);
                Console.Write(ex.Message);
            }

            try
            {
                this.addPermission();
            }
            catch (Exception ex)
            {
                //MessageBox.Show("ERROR al dar permisos: " + ex.Message);
                Console.Write(ex.Message);
            }
        }

        /// <summary>
        /// Finaliza hilo, permisos y borra archivo
        /// </summary>
        public void fileSadExit(string pPath)
        {
            try
            {
                if (this.hiloOpenTCP != null)
                {
                    this.hiloOpenTCP.Interrupt();
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show("ERROR al cerrar el hilo: " + ex.Message);
                Console.Write(ex.Message);
            }

            try
            {
                this.addPermissionFile2(pPath);
            }
            catch (Exception ex)
            {
                //MessageBox.Show("ERROR al dar permisos: " + ex.Message);
                Console.Write(ex.Message);
            }
        }

        /// <summary>
        /// Cambia permisos del archivo
        /// </summary>
        /// <param name="pFile"></param>
        public void changePermissions()
        {
            try
            {
                this.removePermission();

                this.textBox.Text += "Done" + "\r\n";
            }
            catch (Exception e)
            {
                this.textBox.Text += e.Message + "\r\n";
            }
        }

        /// <summary>
        /// Check Permisos Hilo, recursivo
        /// </summary>
        public void checkPermissions()
        {
            try
            {
                //Primero comprobar si existe el fichero
                if (UtilsClass.fileExist(fi.DirectoryName + "\\" + fi.Name))
                {
                    //Comprobar permisos
                    if (this.checkPermissionDenyOnFileAll(fi.DirectoryName + "\\" + fi.Name))
                    {
                        UtilsClass.waitApp(100);

                        this.checkPermissions();
                    }
                    else
                    {
                        if (this.hiloOpenTCP != null)
                        {
                            if (ParamGlobal.GetInstance().checkPer)
                            {
                                MessageBoxTemporal men = new MessageBoxTemporal();
                                men.Start("Please do not touch the file permissions, the application will close.", "Warning", 5, false);
                            }

                            this.endApp();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBoxTemporal men = new MessageBoxTemporal();
                men.Start("Error - Please do not touch the file permissions, the application will close. Men: " + ex.Message, "Warning", 5, false);

                this.endApp();
            }
        }

        /// <summary>
        /// Eliminar permisos
        /// </summary>
        public void removePermission()
        {
            string fileName = fi.DirectoryName + "\\" + fi.Name;
            AuthorizationRuleCollection accessRules = this.getPermissionOnFile(fileName);
            foreach (FileSystemAccessRule rule in accessRules)
            {
                this.removePermissionRule(fileName, rule);
            }
        }

        /// <summary>
        /// Eliminar permisos a fichero
        /// </summary>
        public void removePermissionFile(string pFile)
        {
            string fileName = pFile;
            AuthorizationRuleCollection accessRules = this.getPermissionOnFile(fileName);
            foreach (FileSystemAccessRule rule in accessRules)
            {
                this.removePermissionRuleFile(fileName, rule);
            }
        }

        /// <summary>
        /// Da permisos
        /// </summary>
        public void addPermission()
        {
            string fileName = fi.DirectoryName + "\\" + fi.Name;
            AuthorizationRuleCollection accessRules = this.getPermissionOnFile(fileName);
            foreach (FileSystemAccessRule rule in accessRules)
            {
                this.addPermissionOnRule(fileName, rule);
            }
        }

        /// <summary>
        /// Da permisos a fichero
        /// </summary>
        /// <param name="pFile"></param>
        public void addPermissionFile(string pFile)
        {
            string fileName = pFile;
            AuthorizationRuleCollection accessRules = this.getPermissionOnFile(fileName);
            foreach (FileSystemAccessRule rule in accessRules)
            {
                this.addPermissionOnRuleFile(fileName, rule);
            }
        }

        /// <summary>
        /// Da permisos a fichero
        /// </summary>
        /// <param name="pFile"></param>
        public void addPermissionFile2(string pFile)
        {
            string fileName = pFile;
            AuthorizationRuleCollection accessRules = this.getPermissionOnFile(fileName);
            foreach (FileSystemAccessRule rule in accessRules)
            {
                this.addPermissionOnRuleNoDenyFile(fileName, rule);
            }
        }


        /// <summary>
        /// Da permisos a fichero y quita deny
        /// </summary>
        /// <param name="pFile"></param>
        public void addAllowNoDenyFile(string pFile)
        {
            string fileName = pFile;
            AuthorizationRuleCollection accessRules = this.getPermissionOnFile(fileName);
            foreach (FileSystemAccessRule rule in accessRules)
            {
                this.addPermissionOnRuleNoDenyFile(fileName, rule);
            }
        }

        /// <summary>
        /// Poner deny a permisos
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="account"></param>
        public void removePermissionRule(string fileName, FileSystemAccessRule rule)
        {
            String account = rule.IdentityReference.Translate(typeof(NTAccount)).Value;

            this.textBox.Text += "Remove " + rule.FileSystemRights + " Allow " + (RemoveFileSecurity(fileName, @account, rule.FileSystemRights, AccessControlType.Allow)) + ", file: " + fileName + ", user: " + account + "\r\n";

            this.textBox.Text += "Add " + rule.FileSystemRights + " Deny " + (AddFileSecurity(fileName, @account, FileSystemRights.FullControl, AccessControlType.Deny)) + ", file: " + fileName + ", user: " + account + "\r\n";
        }

        /// <summary>
        /// Poner deny a permisos a fichero
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="account"></param>
        public void removePermissionRuleFile(string fileName, FileSystemAccessRule rule)
        {
            String account = rule.IdentityReference.Translate(typeof(NTAccount)).Value;

            this.RemoveFileSecurity(fileName, @account, rule.FileSystemRights, AccessControlType.Allow);

            this.AddFileSecurity(fileName, @account, FileSystemRights.FullControl, AccessControlType.Deny);
        }

        /// <summary>
        /// Dar deny a permisos
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="rule"></param>
        public void addPermissionOnRule(string fileName, FileSystemAccessRule rule)
        {
            String account = rule.IdentityReference.Translate(typeof(NTAccount)).Value;

            this.AddFileSecurity(fileName, @account, rule.FileSystemRights, AccessControlType.Allow);

            this.RemoveFileSecurity(fileName, @account, rule.FileSystemRights, AccessControlType.Deny);
        }

        /// <summary>
        /// Dar permisos a fichero
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="rule"></param>
        public void addPermissionOnRuleFile(string fileName, FileSystemAccessRule rule)
        {
            String account = rule.IdentityReference.Translate(typeof(NTAccount)).Value;

            this.AddFileSecurity(fileName, @account, FileSystemRights.FullControl, AccessControlType.Allow);
        }

        /// <summary>
        /// Dar permisos a fichero quita deny
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="rule"></param>
        public void addPermissionOnRuleNoDenyFile(string fileName, FileSystemAccessRule rule)
        {
            String account = rule.IdentityReference.Translate(typeof(NTAccount)).Value;

            this.RemoveFileSecurity(fileName, @account, rule.FileSystemRights, AccessControlType.Deny);
            this.AddFileSecurity(fileName, @account, FileSystemRights.FullControl, AccessControlType.Allow);
        }

        /// <summary>
        /// Get coleccion de permisos de un fichero
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public AuthorizationRuleCollection getPermissionOnFile(string path)
        {
            AuthorizationRuleCollection accessRules = null;

            var accessControlList = Directory.GetAccessControl(path);

            if (accessControlList == null)
            {
                return accessRules;
            }

            accessRules = accessControlList.GetAccessRules(true, true, typeof(System.Security.Principal.SecurityIdentifier));

            return accessRules;
        }

        /// <summary>
        /// Check permisos de un fichero (Mix)
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public bool checkPermissionDenyOnFileMix(string path)
        {
            bool seguir = true;

            FileSecurity fs = File.GetAccessControl(path);
            AuthorizationRuleCollection arc =
                fs.GetAccessRules(true, true, typeof(NTAccount));

            //Sacar usuarios
            List<IdentityReference> listIdentidades = new List<IdentityReference>();
            foreach (FileSystemAccessRule fsar in arc)
            {
                if (listIdentidades.Count == 0)
                {
                    listIdentidades.Add(fsar.IdentityReference);
                }
                else
                {
                    bool existIdentity = false;
                    foreach (IdentityReference iden in listIdentidades)
                    {
                        if (iden.Value.Equals(fsar.IdentityReference.Value))
                        {
                            existIdentity = true;
                            break;
                        }
                    }

                    if (!existIdentity)
                    {
                        listIdentidades.Add(fsar.IdentityReference);
                    }
                }
            }

            //Comprobar reglas por cada usuario, si en algun usuario no esta deny salir
            foreach (IdentityReference iden in listIdentidades)
            {
                List<FileSystemAccessRule> listRulesByUser = new List<FileSystemAccessRule>();

                foreach (FileSystemAccessRule fsar in arc)
                {
                    if (iden.Value.Equals(fsar.IdentityReference.Value))
                    {
                        listRulesByUser.Add(fsar);
                    }
                }

                bool deny = false;
                foreach (FileSystemAccessRule fsarUser in listRulesByUser)
                {
                    String estado = fsarUser.AccessControlType.ToString();
                    if (estado.Equals("Deny"))
                    {
                        deny = true;
                        break;
                    }
                }

                if (!deny)
                {
                    seguir = false;
                }
            }

            return seguir;
        }

        /// <summary>
        /// Check permisos de un fichero (Jorge)
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public bool checkPermissionDenyOnFileSecurity(string path)
        {
            bool seguir = true;

            FileSecurity fs = File.GetAccessControl(path);
            AuthorizationRuleCollection arc =
                fs.GetAccessRules(true, true, typeof(NTAccount));

            int contador = 0;
            foreach (FileSystemAccessRule fsar in arc)
            {
                String regla = fsar.FileSystemRights.ToString();
                String estado = fsar.AccessControlType.ToString();
                if (estado != "Deny" && (contador < 3 || regla == "Write, Read"))
                {
                    seguir = false;
                    break;
                }
                contador++;
            }

            return seguir;
        }

        /// <summary>
        /// Mix all
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public bool checkPermissionDenyOnFileAll(string path)
        {
            bool seguir = true;

            /*bool checkJor = false;
            bool checkMix = false;
            bool checkMan = false;

            checkJor = this.checkPermissionDenyOnFileSecurity(fi.DirectoryName + "\\" + fi.Name);
            checkMix = this.checkPermissionDenyOnFileMix(fi.DirectoryName + "\\" + fi.Name);
            checkMan = this.checkPermissionDenyOnFileRules(fi.DirectoryName + "\\" + fi.Name);

            if (checkJor && checkMix && checkMan)
            {
                seguir = true;
            }
            else if (!checkJor && !checkMix && !checkMan)
            {
                seguir = false;
            }
            else if (!checkMan)
            {
                seguir = false;
            }
            else if ((!checkJor || !checkMix) && (checkMan))
            {
                seguir = true;
            }*/

            if (!this.checkPermissionDenyOnFileRules(fi.DirectoryName + "\\" + fi.Name))
            {
                seguir = false;
            }

            return seguir;
        }

        /// <summary>
        /// Check permisos de un fichero (Manuel)
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public bool checkPermissionDenyOnFileRules(string path)
        {
            bool seguir = true;

            var accessControlList = Directory.GetAccessControl(path);

            if (accessControlList == null)
            {
                seguir = false;
            }

            var accessRules = accessControlList.GetAccessRules(true, true, typeof(System.Security.Principal.SecurityIdentifier));

            if (accessRules == null)
            {
                seguir = false;
            }

            List<FileSystemAccessRule> listDeny = new List<FileSystemAccessRule>();
            List<FileSystemAccessRule> listAllow = new List<FileSystemAccessRule>();

            foreach (FileSystemAccessRule rule in accessRules)
            {
                if (rule.AccessControlType == AccessControlType.Deny)
                {
                    listDeny.Add(rule);
                }
                else
                {
                    listAllow.Add(rule);
                }
            }

            //Mirar porque a veces no esta en allow
            foreach (FileSystemAccessRule ruleAllow in listAllow)
            {
                bool isDeny = false;

                foreach (FileSystemAccessRule ruleDeny in listDeny)
                {
                    if (ruleAllow.FileSystemRights == ruleDeny.FileSystemRights && ruleAllow.IdentityReference == ruleDeny.IdentityReference)
                    {
                        isDeny = true;
                        break;
                    }
                }

                if (!isDeny)
                {
                    seguir = false;
                    break;
                }
            }

            return seguir;
        }

        /// <summary>
        /// Adds an ACL entry on the specified file for the specified account. 
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="account"></param>
        /// <param name="rights"></param>
        /// <param name="controlType"></param>
        /// <returns></returns>

        public String AddFileSecurity(string fileName, string account, FileSystemRights rights, AccessControlType controlType)
        {
            String result = "OK";

            try
            {
                // Get a FileSecurity object that represents the 
                // current security settings.
                FileSecurity fSecurity = File.GetAccessControl(fileName);

                // Add the FileSystemAccessRule to the security settings.
                fSecurity.AddAccessRule(new FileSystemAccessRule(account,
                    rights, controlType));

                // Set the new access settings.
                File.SetAccessControl(fileName, fSecurity);
            }
            catch (Exception e)
            {
                result = "ERROR";

                this.textBox.Text += "AddFileSecurity ERROR, Men: " + e.Message + "\r\n";
            }

            return result;
        }

        /// <summary>
        /// Removes an ACL entry on the specified file for the specified account. 
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="account"></param>
        /// <param name="rights"></param>
        /// <param name="controlType"></param>
        /// <returns></returns>
        public String RemoveFileSecurity(string fileName, string account, FileSystemRights rights, AccessControlType controlType)
        {
            String result = "OK";

            try
            {
                // Get a FileSecurity object that represents the 
                // current security settings.
                FileSecurity fSecurity = File.GetAccessControl(fileName);

                // Remove the FileSystemAccessRule from the security settings.
                fSecurity.RemoveAccessRule(new FileSystemAccessRule(account,
                    rights, controlType));

                // Set the new access settings.
                File.SetAccessControl(fileName, fSecurity);
            }
            catch (Exception e)
            {
                result = "ERROR";

                this.textBox.Text += "RemoveFileSecurity ERROR, Men: " + e.Message + "\r\n";
            }

            return result;
        }

        /// <summary>
        /// Pasa de bytes a FileStream
        /// </summary>
        /// <param name="pRutaTmeWin"></param>
        /// <param name="pDescifradoByteArray"></param>
        /// <returns></returns>
        public bool byteArrayToFileStream(String pRutaTmeWin, byte[] pDescifradoByteArray)
        {
            try
            {
                // Open file for reading
                System.IO.FileStream fileStream =
                   new System.IO.FileStream(pRutaTmeWin, System.IO.FileMode.Create,
                                            System.IO.FileAccess.Write);
                // Writes a block of bytes to this stream using data from
                // a byte array.
                fileStream.Write(pDescifradoByteArray, 0, pDescifradoByteArray.Length);

                // close file stream
                fileStream.Close();

                return true;
            }
            catch (Exception _Exception)
            {
                // Error
                Console.WriteLine("Exception caught in process: {0}",
                                  _Exception.ToString());
            }

            // error occured, return false
            return false;
        }

        public void writeFile(string fileSource, TextBox textBox)
        {
            if (System.IO.Path.GetExtension(fileSource).Equals(Param.extSadc))
            {
                if (UtilsClass.createDir(Param.rutaDlls))
                {
                    byte[] fileArray = System.IO.File.ReadAllBytes(fileSource);

                    //Ponemos este archivo la carpeta tmp
                    UtilsClass.createDir(Application.StartupPath + Param.rutaTMP_TOOL);
                    String rutaFile = Application.StartupPath + Param.rutaTMP_TOOL + System.IO.Path.GetFileName(fileSource);
                    bool okWrite = this.byteArrayToFileStream(rutaFile, fileArray);

                    if (okWrite)
                    {
                        textBox.Text += fileSource;
                    }
                    else
                    {
                        MessageBox.Show("ERROR File not found " + fileSource);
                    }
                }
                else
                {
                    MessageBox.Show("ERROR Folder not found " + fileSource);
                }
            }
            else
            {
                MessageBox.Show("ERROR Unrecognized File " + fileSource);
            }
        }
    }
}
