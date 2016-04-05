using SAD_TOOL.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SAD_TOOL
{
    static class Program
    {
        static Process checker;
        static Process main;
        static int mainProcessID;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            //Main App Process.
            if (args.Length == 0)
            {
                //Saves current process info to pass on command line.
                main = Process.GetCurrentProcess();
                mainProcessID = main.Id;

                //Initializes the helper process
                checker = new Process();
                checker.StartInfo.FileName = main.MainModule.FileName;
                checker.StartInfo.Arguments = mainProcessID.ToString();

                checker.EnableRaisingEvents = true;
                //Para depurar
                checker.Exited += new EventHandler(checker_Exited);

                //Launch the helper process.
                checker.Start();

                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Index());
            }
            else //On the helper Process
            {
                //MessageBox.Show("Task Manager 1, " + int.Parse(args[0]));

                main = Process.GetProcessById(int.Parse(args[0]));

                main.EnableRaisingEvents = true;
                //main.Exited += new EventHandler(main_Exited);

                while (!main.HasExited)
                {
                    Thread.Sleep(1000); //Wait 1 second. 
                }

                UtilsClass.waitApp(500);

                endAppMain();

                UtilsClass.waitApp(500);

                //Provide some time to process the main_Exited event. 
                //Thread.Sleep(2000);
            }

            /*Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Index());*/
        }

        //Double checks the user not closing the checker process.
        static void checker_Exited(object sender, EventArgs e)
        {
            //This only checks for the task manager process running. 
            //It does not make sure that the app has been closed by it. But close enough.
            //If you can think of a better way please let me know.
            if (Process.GetProcessesByName("taskmgr").Length != 0)
            {
                ParamGlobal.GetInstance().checkPer = false;

                UtilsClass.waitApp(500);

                endAppMain();

                UtilsClass.waitApp(500);

                //MessageBox.Show("Task Manager killed helper process.");

                //MessageBoxTemporal men = new MessageBoxTemporal();
                //men.Start("Task Manager killed helper process, closing ...", "Warning", 5, false);

                //If you like you could kill the main app here to. 
                main.Kill();
            }
        }

        //Only gets to run on the checker process. The other one should be dead. 
        //If it isn't then run Forrest, run!!
        static void main_Exited(object sender, EventArgs e)
        {
            //This only checks for the task manager process running. 
            //It does not make sure that the app has been closed by it. But close enough.
            //If you can think of a better way please let me know.
            if (Process.GetProcessesByName("taskmgr").Length != 0)
            {
                UtilsClass.waitApp(500);

                endAppMain();

                UtilsClass.waitApp(500);

                MessageBox.Show("Task Manager killed my app.");

                //MessageBoxTemporal men = new MessageBoxTemporal();
                //men.Start("Task Manager killed my app, closing ...", "Warning", 5, false);                               
            }
        }

        static void endAppMain()
        {
            try
            {
                UtilsClass.waitApp(1000);
                UtilsClass.killProcess();
            }
            catch (Exception ex)
            {
                MessageBoxTemporal men = new MessageBoxTemporal();
                men.Start("ERROR, endAppMain, killProcess, " + ex.Message, "Warning", 5, false);
            }

            try
            {
                UtilsClass.waitApp(1000);
                UtilsClass.endAddAllowNoDenyFile(Param.rutaWinTmp, Param.tmpName);
            }
            catch (Exception ex)
            {
                MessageBoxTemporal men = new MessageBoxTemporal();
                men.Start("ERROR, endAppMain, addAllow, " + ex.Message, "Warning", 5, false);
            }

            try
            {
                UtilsClass.waitApp(1000);

                int count = 0;
                bool ok = false;
                while (!ok && count < 3)
                {
                    UtilsClass.waitApp(500);
                    ok = UtilsClass.removeFileDes(Param.rutaWinTmp + Param.tmpName);
                    count = count + 1;
                }

                count = 0;
                ok = false;
                while (!ok && count < 3)
                {
                    UtilsClass.waitApp(500);
                    ok = UtilsClass.removeFileDes(Application.StartupPath + "\\" + Param.desName);
                    count = count + 1;
                }
            }
            catch (Exception ex)
            {
                MessageBoxTemporal men = new MessageBoxTemporal();
                men.Start("ERROR, borrado, " + ex.Message, "Warning", 5, false);
            }
        }
    }
}
