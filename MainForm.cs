using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;
using System.Threading;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.IO;
using static System.Net.Mime.MediaTypeNames;

namespace CSTerm
{
    public partial class MainForm : Form
    {
        string SelectedPort;
        int SelectedBaud;
        List<string> lastText = new List<string>();
        int iLastText = 0;


        private SerialPort serialPort;
        private CancellationTokenSource cancellationTokenSource;

        private Thread waitingThread;
        private bool isListening = false; // Global control variable

        public MainForm()
        {
            InitializeComponent();

            SelectedBaud = Properties.Settings.Default.LastBaud;
            SelectedPort = Properties.Settings.Default.LastPort;

            txtRx.Font = new Font("Consolas", 10, FontStyle.Regular);
        }



        private void btnSettings_Click(object sender, EventArgs e)
        {
            using (Settings s = new Settings(SelectedPort, SelectedBaud))
            {
                var res = s.ShowDialog(this);
                if (res != DialogResult.Cancel)
                {
                    int.TryParse(s.selectedBaud, out SelectedBaud);
                    SelectedPort = s.SelectedPort;
                    Properties.Settings.Default.LastBaud = SelectedBaud;
                    Properties.Settings.Default.LastPort = SelectedPort;
                    Properties.Settings.Default.Save();
                }
            }
        }



        private void btnConnect_Click(object sender, EventArgs e)
        {

            if (isListening || waitingThread != null)
            {
                try
                {
                    try { if (waitingThread != null) waitingThread.Abort();  }
                    catch { }

                    waitingThread = null;
                    isListening = false;

                    // Cancel the task
                    if (cancellationTokenSource != null)
                    {
                        cancellationTokenSource.Cancel();
                    }

                    // Close the serial port
                    if (serialPort?.IsOpen == true)
                    {
                        serialPort.Close();
                    }

                    this.Invoke((MethodInvoker)delegate
                    {
                        btnConnect.Text = "Connect";
                    });


                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }

                return;
            }

            try
            {
                // Initialize SerialPort with specific settings
                if (waitingThread != null)
                {
                    try { waitingThread.Abort(); }
                    catch { }
                    waitingThread = null;
                }


                serialPort = new SerialPort(SelectedPort, SelectedBaud); // Change "COM3" and 9600 as needed
                serialPort.DataBits = 8;
                serialPort.Parity = Parity.None;
                serialPort.StopBits = StopBits.One;
                serialPort.Open();

                startActualListen();



            }
            catch (UnauthorizedAccessException ex)
            {
                waitingThread = new Thread(() =>
                {

                    while (!serialPort.IsOpen)
                    {
                        try
                        {
                            serialPort.Open();
                        }
                        catch { }
                        System.Threading.Thread.Sleep(500);
                    }
                    startActualListen();
                });
                waitingThread.Start();
                btnConnect.Text = $"Waiting for port {SelectedPort}...";

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }


        private void startActualListen()
        {
            isListening = true;

            // Start a background thread to listen for data
            cancellationTokenSource = new CancellationTokenSource();

            // Start listening for data in a background task
            Task listeningTask = Task.Run(() => ListenForData(cancellationTokenSource.Token), cancellationTokenSource.Token);
            isListening = true;
            this.Invoke((MethodInvoker)delegate
            {
                btnConnect.Text = $"{SelectedPort} at {SelectedBaud}, Click to Disconnect";
            });
        }

        private void ListenForData(CancellationToken token)
        {
            try
            {
                while (!token.IsCancellationRequested && serialPort.IsOpen)
                {
                    try
                    {
                        string data = serialPort.ReadLine(); // Adjust based on your input format
                        if (!string.IsNullOrEmpty(data))
                        {
                            // Update TextBox on the UI thread
                            this.Invoke((MethodInvoker)delegate
                            {
                                //string sanitizedText = data.Replace("\r\n", "\n").Replace("\n", "");
                                txtRx.AppendText(data.Replace("\r", "\r\n"));
                            });
                        }
                    }
                    catch (TimeoutException)
                    {
                        // Ignore timeout exceptions
                    }
                    catch (IOException)
                    {
                        // Handle the case when the serial port is closed while reading
                        if (!token.IsCancellationRequested)
                        {
                            this.Invoke((MethodInvoker)delegate
                            {
                                MessageBox.Show("Error: Serial port closed unexpectedly.");
                            });
                        }
                        break;
                    }
                }
            }
            catch (OperationCanceledException)
            {
                // Handle cancellation gracefully
            }
            catch (Exception ex)
            {
                this.Invoke((MethodInvoker)delegate
                {
                    MessageBox.Show($"Listening stopped unexpectedly: {ex.Message}");
                });
            }
        }




        private void txtTx_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
            {
                if (lastText.Count == 0) return; // No history to navigate

                if (iLastText == -1) // First press, start at the last entry
                {
                    iLastText = lastText.Count - 1;
                }
                else if (iLastText > 0) // Move backward in history
                {
                    iLastText--;
                }

                txtTx.Text = lastText[iLastText];
                txtTx.SelectionStart = txtTx.Text.Length; // Move cursor to end of text
                e.Handled = true; // Prevent default behavior
            }
            else if (e.KeyCode == Keys.Down)
            {
                if (lastText.Count == 0) return; // No history to navigate

                if (iLastText >= 0 && iLastText < lastText.Count - 1) // Move forward in history
                {
                    iLastText++;
                    txtTx.Text = lastText[iLastText];
                }
                else if (iLastText == lastText.Count - 1) // Reset to current input
                {
                    iLastText = -1;
                    txtTx.Clear();
                }

                txtTx.SelectionStart = txtTx.Text.Length; // Move cursor to end of text
                e.Handled = true; // Prevent default behavior
            }

            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // Prevent the ding sound on Enter
                if (serialPort != null && serialPort.IsOpen)
                {
                    try
                    {
                        string message = txtTx.Text.Trim();
                        lastText.Add(message);
                        iLastText=lastText.Count;
                        if (!string.IsNullOrEmpty(message))
                        {
                            serialPort.WriteLine(message); // Send the message
                            txtTx.Clear(); // Clear the textbox after sending
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error sending data: {ex.Message}");
                    }
                }
                else
                {
                    MessageBox.Show("Serial port is not open!");
                }
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtRx.Clear();
        }
    }
}
