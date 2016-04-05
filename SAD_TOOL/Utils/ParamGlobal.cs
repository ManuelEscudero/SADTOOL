using SAD_TOOL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SAD_TOOL.Utils
{
    public class ParamGlobal
    {
        public List<Challenge> listChallenges { get; set; }
         public List<TextBox> listTextBox { get; set; }
        public bool checkPer { get; set; }
        public string visorType { get; set; }
        public DateTime startApp { get; set; }

        private static ParamGlobal instance = null;

        private ParamGlobal()
        {

        }

        public static ParamGlobal GetInstance()
        {
            if (instance == null)
                instance = new ParamGlobal();

            return instance;
        }
    }
}
