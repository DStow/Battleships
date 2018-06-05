using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BattleshipsServer
{
    public partial class frmServer : Form
    {
        private TCPServerLib.Server _server;
        private string _logDirectory = "";

        public frmServer()
        {
            InitializeComponent();
        }

        private void frmServer_Load(object sender, EventArgs e)
        {
            // Check if log folder exists
            _logDirectory = Application.StartupPath + "\\Log";

            if (Directory.Exists(_logDirectory) == false)
            {
                Directory.CreateDirectory(_logDirectory);
            }
        }

        private void btnStartServer_Click(object sender, EventArgs e)
        {
            // Check if port is a valid number?
            string sPort = txtPort.Text;

            int port = 0;
            if (int.TryParse(sPort, out port) == false)
            {
                MessageBox.Show("Please enter a valid number for the port!");
                return;
            }
            else if (port < 1000 || port > 9999)
            {
                MessageBox.Show("Please enter a port between 1000 and 9999!");
                return;
            }
            else
            {
                _server = new TCPServerLib.Server(port, ProcessIncomingMessage);
                _server.StartServer();

                WriteToLog("Server started on port: " + port);

                btnStopServer.Enabled = true;
                btnStartServer.Enabled = false;
            }
        }

        private void btnStopServer_Click(object sender, EventArgs e)
        {
            _server.CloseServer();

            WriteToLog("Server stopped");

            btnStopServer.Enabled = false;
            btnStartServer.Enabled = true;
        }

        private void WriteToLog(string message)
        {
            // Write to textbox and to log file
            string lowKeyMessage = DateTime.Now.ToString("HH:mm:ss.fff") + " - " + message + "\n\r";
            string fullMessage = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + " - " + message;

            // Write to GUI
            txtServerLog.Text = lowKeyMessage + "\r\n" + txtServerLog.Text;

            // Write to text file
            string logFile = _logDirectory + "\\Log " + DateTime.Now.ToString("yyyyMMdd") + ".txt";
            StreamWriter sw = new StreamWriter(logFile, true);
            sw.WriteLine(fullMessage);
            sw.Close();
        }

        private void frmServer_FormClosing(object sender, FormClosingEventArgs e)
        {
            _server.CloseServer();
        }


        // Deals with any message that is recieved by the server while it's running
        private string ProcessIncomingMessage(string message)
        {
            return "Recieved";
        }
    }
}
