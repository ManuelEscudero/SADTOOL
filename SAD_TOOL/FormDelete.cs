using SAD_TOOL.Models;
using SAD_TOOL.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SAD_TOOL
{
    public partial class FormDelete : Form
    {
        private List<Challenge> chaClickList = null;

        public FormDelete()
        {
            InitializeComponent();

            LoadData();
        }

        /// <summary>
        /// Carga datos inicial
        /// </summary>
        private void LoadData()
        {
            this.chaClickList = new List<Challenge>();

            //Getinfos
            ParamGlobal.GetInstance().listChallenges = DllManager.fillChallengesInfo(ParamGlobal.GetInstance().listChallenges);

            //Generar tabla de dlls
            this.dataGridViewChallenges = UtilsClass.generateTable(ParamGlobal.GetInstance().listChallenges, this.dataGridViewChallenges);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.chaClickList = UtilsClass.treatCellClickCheck(this.dataGridViewChallenges);

            foreach (Challenge cha in this.chaClickList)
            {
                FileManager filemanager = new FileManager();
                filemanager.addPermissionFile(Param.rutaDlls + cha.NameDll);

                if (UtilsClass.removeFileAux(Param.rutaDlls + cha.NameDll))
                {
                    ParamGlobal.GetInstance().listChallenges.Remove(cha);
                }
            }

            this.Hide();
        }
    }
}
