using SAD_TOOL.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SAD_TOOL
{
    public partial class Index : Form
    {
        public FileManager fileManager;

        public Index()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Inicio form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Index_Load(object sender, EventArgs e)
        {
            var Usuario = System.Security.Principal.WindowsIdentity.GetCurrent();
            var Grupos = new System.Security.Principal.WindowsPrincipal(Usuario);

            ParamGlobal.GetInstance().checkPer = true;

            if (!Environment.Is64BitOperatingSystem)
            {
                //var is64 = IntPtr.Size == 8;

                MessageBox.Show("A machine of 64 bits is necessary.");

                MessageBoxTemporal men = new MessageBoxTemporal();
                men.Start("Closing application ...", "Warning", 5, false);

                //EndAllApp
                UtilsClass.endAllApp();

                Application.ExitThread();
                Application.Exit();
            }
            else
            {
                if (!Grupos.IsInRole(System.Security.Principal.WindowsBuiltInRole.Administrator))
                {
                    MessageBox.Show("You need to run as administrator.");

                    MessageBoxTemporal men = new MessageBoxTemporal();
                    men.Start("Closing application ...", "Warning", 5, false);

                    //EndAllApp
                    UtilsClass.endAllApp();

                    Application.ExitThread();
                    Application.Exit();
                }
                else
                {
                    if (!UtilsClass.copyInitFiles())
                    {
                        MessageBox.Show("Could not start the application.");

                        MessageBoxTemporal men = new MessageBoxTemporal();
                        men.Start("Closing application ...", "Warning", 5, false);

                        //EndAllApp
                        UtilsClass.endAllApp();

                        Application.ExitThread();
                        Application.Exit();
                    }
                }
            }

            ParamGlobal.GetInstance().startApp = DateTime.Now;
        }

        /// <summary>
        /// Abrir archivo sad
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void newProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.endAllAppFile();

            OpenFileDialog loader = new OpenFileDialog();
            DialogResult locRes = loader.ShowDialog();
            if (locRes == DialogResult.OK)
            {
                if (fileManager == null)
                {
                    fileManager = new FileManager();
                }
                fileManager.loadFileSad(loader.FileName, this.textBox1);
            }
        }

        /// <summary>
        /// Importar challenge, dll
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void importNewChallengeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog loader = new OpenFileDialog();
            DialogResult locRes = loader.ShowDialog();
            if (locRes == DialogResult.OK)
            {
                if (fileManager == null)
                {
                    fileManager = new FileManager();
                }
                fileManager.loadFileDll(loader.FileName, this.textBox1);
            }
        }

        /// <summary>
        /// Generar .sad
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void generateProtectedFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.endAllAppFile();

            ParamGlobal.GetInstance().listChallenges = UtilsClass.getAllChallenge();

            if (ParamGlobal.GetInstance().listChallenges != null && ParamGlobal.GetInstance().listChallenges.Count != 0)
            {
                FormWizard wizardSad = new FormWizard();
                wizardSad.ShowDialog();
            }
            else
            {
                MessageBox.Show("Add dll please.");
            }
        }

        /// <summary>
        /// Exit
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Confirm Exit?", "Confirmation",
                                    MessageBoxButtons.YesNoCancel,
                                    MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                MessageBoxTemporal men = new MessageBoxTemporal();
                men.Start("Closing application ...", "Warning", 5, false);

                if (fileManager != null)
                {
                    fileManager.endAppExit();
                }

                //EndAllApp
                UtilsClass.endAllApp();

                Application.ExitThread();
                Application.Exit();
            }
            else if (result == DialogResult.No)
            {
            }
            else
            {
            }
        }

        /// <summary>
        /// Override the Close Form event
        /// Do something
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Index_FormClosing(Object sender, FormClosingEventArgs e)
        {
            //MessageBoxTemporal men = new MessageBoxTemporal();
            //men.Start("X- Closing application ...", "Warning", 5, false);

            //EndAllApp
            UtilsClass.endAllApp();
        }

        private void closeProtectedFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.endAllAppFile();
        }

        private void deleteChallengeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ParamGlobal.GetInstance().listChallenges = UtilsClass.getAllChallenge();

            FormDelete form = new FormDelete();
            form.ShowDialog();
        }

        public void endAllAppFile()
        {
            MessageBoxTemporal men = new MessageBoxTemporal();
            men.Start("Loading ...", "Warning", 3, false);

            if (fileManager != null)
            {
                fileManager.endAppExit();
            }

            //EndAllApp
            UtilsClass.endAllApp();
        }
    }
}
