using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;

namespace CSTerm
{
    public partial class Settings : Form
    {
        public string SelectedPort {get;private set;}
        public string selectedBaud { get; private set; }

        public Settings()
        {
            InitializeComponent();

            cbBaud.Items.AddRange(new string[] { "9600", "115200" });
            cbCOM.Items.AddRange(SerialPort.GetPortNames());

            
        }

        public Settings(string port, int baud) : this()
        {
            SelectedPort = port;
            selectedBaud = baud.ToString();

            cbBaud.SelectedItem = selectedBaud;
            cbCOM.SelectedItem = SelectedPort;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            selectedBaud = cbBaud.SelectedItem.ToString();
            SelectedPort = cbCOM.SelectedItem.ToString();
            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
