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
    public partial class PopUp : Form
    {
        public Challenge cha { get; set; }

        public PopUp(Challenge pCha)
        {
            InitializeComponent();

            this.cha = pCha;

            this.Load();
        }

        public void Load()
        {
            this.cha = DllManager.fillChallengesInfo(this.cha);

            //Generar los parametros que necesite segun xml
            this.groupBoxParam = UtilsClass.generateParamPassPanel(this, this.cha);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.cha = null;

            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool seguir = true;
            List<Control> listControls = new List<Control>();

            foreach (Control control in this.groupBoxParam.Controls)
            {
                if (control is DateTimePicker)
                {
                    if (((DateTimePicker)control).Value == null || ((DateTimePicker)control).Value.Equals(""))
                    {
                        seguir = false;
                        break;
                    }
                    else
                    {
                        listControls.Add(control);
                    }
                }
                else if (control is TextBox)
                {
                    if (((TextBox)control).Text == null || ((TextBox)control).Text.Equals(""))
                    {
                        seguir = false;
                        break;
                    }
                    else
                    {
                        listControls.Add(control);
                    }
                }
            }

            if (seguir)
            {
                //Generar xml
                this.cha = DllManager.fillXMLPass(this.cha, listControls);

                this.Hide();
            }
            else
            {
                MessageBox.Show("Fill all param please.");
            }
        }
    }
}
