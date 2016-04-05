using SAD_TOOL.Models;
using SAD_TOOL.Utils;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace SAD_TOOL
{
    public partial class FormWizard : Form
    {
        #region Form Wizard Generation And Functionality Code
        #region Windows Generated Code


        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormWizard));
            this.panel2 = new System.Windows.Forms.Panel();
            this.m_buttonBack = new System.Windows.Forms.Button();
            this.m_buttonFinish = new System.Windows.Forms.Button();
            this.m_buttonNext = new System.Windows.Forms.Button();
            this.m_buttonCancel = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.m_panelContainer = new System.Windows.Forms.Panel();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBoxSelected = new System.Windows.Forms.Panel();
            this.dataGridViewChallenges = new System.Windows.Forms.DataGridView();
            this.Column0 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBoxInfo = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxParam = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxInfo = new System.Windows.Forms.TextBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBoxParam = new System.Windows.Forms.Panel();
            this.textBoxExa = new System.Windows.Forms.TextBox();
            this.buttonExa = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.m_labelSubtitle = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.m_panelContainer.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBoxSelected.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewChallenges)).BeginInit();
            this.tabPage2.SuspendLayout();
            this.groupBoxInfo.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.m_buttonBack);
            this.panel2.Controls.Add(this.m_buttonFinish);
            this.panel2.Controls.Add(this.m_buttonNext);
            this.panel2.Controls.Add(this.m_buttonCancel);
            resources.ApplyResources(this.panel2, "panel2");
            this.panel2.Name = "panel2";
            // 
            // m_buttonBack
            // 
            resources.ApplyResources(this.m_buttonBack, "m_buttonBack");
            this.m_buttonBack.BackColor = System.Drawing.Color.LightSteelBlue;
            this.m_buttonBack.Name = "m_buttonBack";
            this.m_buttonBack.UseVisualStyleBackColor = false;
            this.m_buttonBack.Click += new System.EventHandler(this.m_buttonBack_Click);
            // 
            // m_buttonFinish
            // 
            resources.ApplyResources(this.m_buttonFinish, "m_buttonFinish");
            this.m_buttonFinish.BackColor = System.Drawing.Color.LightSteelBlue;
            this.m_buttonFinish.Name = "m_buttonFinish";
            this.m_buttonFinish.UseVisualStyleBackColor = false;
            this.m_buttonFinish.Click += new System.EventHandler(this.m_buttonFinish_Click);
            // 
            // m_buttonNext
            // 
            resources.ApplyResources(this.m_buttonNext, "m_buttonNext");
            this.m_buttonNext.BackColor = System.Drawing.Color.LightSteelBlue;
            this.m_buttonNext.Name = "m_buttonNext";
            this.m_buttonNext.UseVisualStyleBackColor = false;
            this.m_buttonNext.Click += new System.EventHandler(this.m_buttonNext_Click);
            // 
            // m_buttonCancel
            // 
            resources.ApplyResources(this.m_buttonCancel, "m_buttonCancel");
            this.m_buttonCancel.BackColor = System.Drawing.Color.LightSteelBlue;
            this.m_buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.m_buttonCancel.Name = "m_buttonCancel";
            this.m_buttonCancel.UseVisualStyleBackColor = false;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.tableLayoutPanel1, "tableLayoutPanel1");
            this.tableLayoutPanel1.Controls.Add(this.panel2, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.m_panelContainer, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            // 
            // m_panelContainer
            // 
            this.m_panelContainer.Controls.Add(this.tabControl);
            resources.ApplyResources(this.m_panelContainer, "m_panelContainer");
            this.m_panelContainer.Name = "m_panelContainer";
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabPage1);
            this.tabControl.Controls.Add(this.tabPage2);
            this.tabControl.Controls.Add(this.tabPage3);
            resources.ApplyResources(this.tabControl, "tabControl");
            this.tabControl.HotTrack = true;
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.label6);
            this.tabPage1.Controls.Add(this.groupBoxSelected);
            resources.ApplyResources(this.tabPage1, "tabPage1");
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            // 
            // groupBoxSelected
            // 
            this.groupBoxSelected.Controls.Add(this.dataGridViewChallenges);
            resources.ApplyResources(this.groupBoxSelected, "groupBoxSelected");
            this.groupBoxSelected.Name = "groupBoxSelected";
            // 
            // dataGridViewChallenges
            // 
            this.dataGridViewChallenges.AllowUserToAddRows = false;
            this.dataGridViewChallenges.AllowUserToDeleteRows = false;
            this.dataGridViewChallenges.AllowUserToResizeColumns = false;
            this.dataGridViewChallenges.AllowUserToResizeRows = false;
            resources.ApplyResources(this.dataGridViewChallenges, "dataGridViewChallenges");
            this.dataGridViewChallenges.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewChallenges.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column0});
            this.dataGridViewChallenges.MultiSelect = false;
            this.dataGridViewChallenges.Name = "dataGridViewChallenges";
            this.dataGridViewChallenges.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dataGridViewChallenges.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dataGridViewChallenges.ShowCellErrors = false;
            this.dataGridViewChallenges.ShowCellToolTips = false;
            this.dataGridViewChallenges.ShowEditingIcon = false;
            this.dataGridViewChallenges.ShowRowErrors = false;
            // 
            // Column0
            // 
            resources.ApplyResources(this.Column0, "Column0");
            this.Column0.Name = "Column0";
            this.Column0.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Column0.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.label5);
            this.tabPage2.Controls.Add(this.groupBoxInfo);
            resources.ApplyResources(this.tabPage2, "tabPage2");
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // groupBoxInfo
            // 
            this.groupBoxInfo.Controls.Add(this.label3);
            this.groupBoxInfo.Controls.Add(this.textBoxParam);
            this.groupBoxInfo.Controls.Add(this.label2);
            this.groupBoxInfo.Controls.Add(this.textBoxInfo);
            resources.ApplyResources(this.groupBoxInfo, "groupBoxInfo");
            this.groupBoxInfo.Name = "groupBoxInfo";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // textBoxParam
            // 
            resources.ApplyResources(this.textBoxParam, "textBoxParam");
            this.textBoxParam.Name = "textBoxParam";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // textBoxInfo
            // 
            resources.ApplyResources(this.textBoxInfo, "textBoxInfo");
            this.textBoxInfo.Name = "textBoxInfo";
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.label4);
            this.tabPage3.Controls.Add(this.groupBoxParam);
            this.tabPage3.Controls.Add(this.textBoxExa);
            this.tabPage3.Controls.Add(this.buttonExa);
            resources.ApplyResources(this.tabPage3, "tabPage3");
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // groupBoxParam
            // 
            resources.ApplyResources(this.groupBoxParam, "groupBoxParam");
            this.groupBoxParam.ForeColor = System.Drawing.SystemColors.ControlText;
            this.groupBoxParam.Name = "groupBoxParam";
            // 
            // textBoxExa
            // 
            resources.ApplyResources(this.textBoxExa, "textBoxExa");
            this.textBoxExa.Name = "textBoxExa";
            // 
            // buttonExa
            // 
            resources.ApplyResources(this.buttonExa, "buttonExa");
            this.buttonExa.Name = "buttonExa";
            this.buttonExa.UseVisualStyleBackColor = true;
            this.buttonExa.Click += new System.EventHandler(this.buttonExa_Click);
            // 
            // panel1
            // 
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Controls.Add(this.m_labelSubtitle);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Name = "panel1";
            // 
            // m_labelSubtitle
            // 
            resources.ApplyResources(this.m_labelSubtitle, "m_labelSubtitle");
            this.m_labelSubtitle.Name = "m_labelSubtitle";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // FormWizard
            // 
            this.AcceptButton = this.m_buttonFinish;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.m_buttonCancel;
            this.ControlBox = false;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "FormWizard";
            this.panel2.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.m_panelContainer.ResumeLayout(false);
            this.tabControl.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.groupBoxSelected.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewChallenges)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.groupBoxInfo.ResumeLayout(false);
            this.groupBoxInfo.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        public Panel panel2;

        #endregion
        public Button m_buttonBack;
        public Button m_buttonNext;
        public Button m_buttonCancel;
        public TableLayoutPanel tableLayoutPanel1;
        public Panel m_panelContainer;
        public TabControl tabControl;
        public TabPage tabPage1;
        private Panel panel1;
        private Label m_labelSubtitle;
        private Label label1;
        private TabPage tabPage2;
        public DataGridView dataGridViewChallenges;
        private Panel groupBoxSelected;
        private Panel groupBoxInfo;
        private TabPage tabPage3;
        private TextBox textBoxInfo;
        public Button m_buttonFinish;
        private Label label3;
        private TextBox textBoxParam;
        private Label label2;
        private TextBox textBoxExa;
        private Button buttonExa;
        private DataGridViewCheckBoxColumn Column0;
        public Panel groupBoxParam;
        private Label label4;
        private Label label5;
        private Label label6;




        #region Form Wizard Functionality

        int currentPanelIndex;

        private void SetCurrentTab(int currentIndex)
        {
            if (currentPanelIndex > -1) //remove all old controls back to tabcontrol
            {
                int count = m_panelContainer.Controls.Count;

                for (int i = count - 1; i >= 0; i--)
                {
                    Control control = m_panelContainer.Controls[i];
                    m_panelContainer.Controls.Remove(control);
                    tabControl.TabPages[currentPanelIndex].Controls.Add(control);
                }
            }

            {
                int count = tabControl.TabPages[currentIndex].Controls.Count;
                m_labelSubtitle.Text = (currentIndex + 1) + " : " + tabControl.TabPages[currentIndex].Text;
                for (int i = count - 1; i >= 0; i--)
                {
                    Control control = tabControl.TabPages[currentIndex].Controls[i];
                    tabControl.TabPages[currentIndex].Controls.Remove(control);
                    m_panelContainer.Controls.Add(control);
                }

                currentPanelIndex = currentIndex;
            }
        }

        private void m_buttonNext_Click(object sender, EventArgs e)
        {
            if (NextButtonClicked())
            {
                int currentIndex = currentPanelIndex;
                currentIndex++;
                if (!(currentIndex >= tabControl.TabCount))
                {
                    SetCurrentTab(currentIndex);
                    m_buttonBack.Enabled = true;

                    if (currentIndex == tabControl.TabCount - 1)
                    {
                        m_buttonNext.Enabled = false;
                        m_buttonFinish.Enabled = true;
                    }
                }
            }
        }

        private void m_buttonBack_Click(object sender, EventArgs e)
        {
            if (BackButtonClicked())
            {
                int currentIndex = currentPanelIndex;
                currentIndex--;
                if (currentIndex >= 0)
                {
                    SetCurrentTab(currentIndex);

                    m_buttonNext.Enabled = true;
                    m_buttonFinish.Enabled = false;

                    if (currentIndex == 0)
                        m_buttonBack.Enabled = false;
                }
            }
        }
        #endregion}
        #endregion

        private List<Challenge> chaClickList = null;

        public FormWizard()
        {
            InitializeComponent();
            this.m_panelContainer.Controls.Remove(tabControl);
            this.currentPanelIndex = -1;
            this.SetCurrentTab(0);

            this.LoadData();
        }

        /// <summary>
        /// Carga datos inicial
        /// </summary>
        private void LoadData()
        {
            this.chaClickList = new List<Challenge>();

            this.label6.Text = "Selected one challenge and click on Next ...";

            //Getinfos
            ParamGlobal.GetInstance().listChallenges = DllManager.fillChallengesInfo(ParamGlobal.GetInstance().listChallenges);

            //Generar tabla de dlls
            this.dataGridViewChallenges = UtilsClass.generateTable(ParamGlobal.GetInstance().listChallenges, this.dataGridViewChallenges);
        }

        /// <summary>
        /// Siguente wizard
        /// </summary>
        /// <returns></returns>
        bool NextButtonClicked()
        {
            switch (currentPanelIndex)
            {
                case Param.pagSelectedCha:
                    {
                        this.chaClickList = UtilsClass.treatCellClickCheck(this.dataGridViewChallenges);

                        if (this.chaClickList == null || this.chaClickList.Count == 0)
                        {
                            MessageBox.Show("Please, selected one Challenge.");
                            return false;
                        }
                        else
                        {
                            this.label5.Text = "Information to challenge, please read and click on Next ...";

                            //Geneate param infos
                            foreach (Challenge cha in this.chaClickList)
                            {
                                this.textBoxInfo.Text += cha.InfoXML + "\r\n" + "\r\n";

                                if (cha.ParamList != null && cha.ParamList.Count != 0)
                                {
                                    String aux = "";
                                    foreach (ParamList par in cha.ParamList)
                                    {
                                        aux = aux + par.type + " : " + par.name + " : " + par.value + "\r\n";
                                    }
                                    this.textBoxParam.Text += aux + "\r\n";
                                }
                            }
                        }
                    }
                    break;
                case Param.pagInfoCha:
                    {
                        if (this.chaClickList == null || this.chaClickList.Count == 0)
                        {
                            MessageBox.Show("Error Challenge List.");
                            return false;
                        }
                        else
                        {
                            this.textBoxExa.Text = "";

                            this.label4.Text = "Fill all param to challenge, click on Finish ...";

                            //Generarte param dinamic
                            this.groupBoxParam = UtilsClass.generateParamPanel(this, this.chaClickList);                            
                        }
                    }
                    break;
                default:
                    {
                    }
                    break;
            }

            return true;
        }

        /// <summary>
        /// Anterior wizard
        /// </summary>
        /// <returns></returns>
        bool BackButtonClicked()
        {
            switch (currentPanelIndex)
            {
                case 0:
                    { }
                    break;
                case 1:
                    { }
                    break;
                default:
                    { }
                    break;
            }
            return true;
        }

        /// <summary>
        /// Fin wizard
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void m_buttonFinish_Click(object sender, EventArgs e)
        {
            //Generar .sad

            if (this.chaClickList == null || this.chaClickList.Count == 0)
            {
                MessageBox.Show("Error Challenge List.");
            }
            else if (this.textBoxExa.Text.Equals(""))
            {
                MessageBox.Show("Please, select one file to source.");
            }
            else
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
                    if (DllManager.generateSad(this.chaClickList, listControls, this.textBoxExa.Text))
                    {
                        MessageBox.Show(".sad generate ok.");

                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("Error generate .sad.");
                    }
                }
                else
                {
                    MessageBox.Show("Fill all param please.");
                }
            }
        }        

        /// <summary>
        /// Examinar, archivo para crear el .sad
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonExa_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileSad = new OpenFileDialog();
            DialogResult dialogResultSad = openFileSad.ShowDialog();
            if (dialogResultSad == DialogResult.OK)
            {
                this.textBoxExa.Text = openFileSad.FileName;
            }
            else
            {
                this.textBoxExa.Text = "";
            }
        }
    }
}



