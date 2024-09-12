using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.AxHost;
using static XInputWrapper;

namespace SerialPadTest {

    public partial class Form1 : Form {
        SerialPort serialPort = new SerialPort();

        private KeyboardInput kInput = new KeyboardInput();
        private XInput xInput = new XInput();

        public Form1() {
            InitializeComponent();

            Input.form = this;

            xInput.XInputConnectionLabel = XInputConnectionLabel;
        }

        private void Form1_Load(object sender, EventArgs e) {
            LoadSerialPortList();
        }

        private void LoadSerialPortList() {
            comboBoxSelectPort.Items.Clear();

            string[] ports = SerialPort.GetPortNames();
            foreach (var port in ports) {
                comboBoxSelectPort.Items.Add(port);
            }

            if (comboBoxSelectPort.Items.Count > 0) {
                comboBoxSelectPort.SelectedIndex = 0;
            }
        }

        private void Button_Connect_Click(object sender, EventArgs e) {
            object selectedObject = comboBoxSelectPort.SelectedItem;

            if (serialPort.IsOpen || comboBoxSelectPort.SelectedIndex == -1) {
                return;
            }

            try {
                serialPort.BaudRate = 115200;
                serialPort.Parity = Parity.None;
                serialPort.DataBits = 8;
                serialPort.StopBits = StopBits.One;
                serialPort.Handshake = Handshake.None;
                serialPort.PortName = comboBoxSelectPort.GetItemText(selectedObject);
                serialPort.WriteBufferSize = 2;
                serialPort.DataReceived += SerialPort_DataReceived;
                serialPort.Open();

                Input.ResetButton();

                DebugWriteLine($"Serial \"{serialPort.PortName}\" connect Success!");

            } catch {
                DebugWriteLine($"Serial \"{serialPort.PortName}\" connect failed.");
            }
        }

        private void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e) {
            var str = serialPort.ReadExisting();
            if (!string.IsNullOrEmpty(str)) {
                StringBuilder sb = new StringBuilder();
                sb.Append(str);
                if (InvokeRequired) {
                    this.Invoke(new Action<string>(this.DebugWriteLine), sb.ToString());

                }
            }

        }

        private void Button_Disconnect_Click(object sender, EventArgs e) {
            if (serialPort.IsOpen) {
                serialPort.Close();
                DebugWriteLine($"Serial \"{serialPort.PortName}\" Disconnected.");
            }
        }

        private void button_ReloadSeralPort_Click(object sender, EventArgs e) {
            LoadSerialPortList();
        }

        public void DebugWriteLine(string str) {
            if (!string.IsNullOrEmpty(str)) {
                richTextBox1.AppendText(str);
                richTextBox1.AppendText(Environment.NewLine);
                richTextBox1.ScrollToCaret();
                Debug.WriteLine(str);
            }
        }

        private void Button_UP_MouseDown(object sender, MouseEventArgs e) {
            Input.SetButtonToggle(Input.Button.Up);
        }

        private void Button_LEFT_MouseDown(object sender, MouseEventArgs e) {
            Input.SetButtonToggle(Input.Button.Left);
        }

        private void Button_RIGHT_MouseDown(object sender, MouseEventArgs e) {
            Input.SetButtonToggle(Input.Button.Right);
        }

        private void Button_DOWN_MouseDown(object sender, MouseEventArgs e) {
            Input.SetButtonToggle(Input.Button.Down);
        }

        private void Button_L_Click(object sender, MouseEventArgs e) {
            Input.SetButtonToggle(Input.Button.L);
        }

        private void Button_R_Click(object sender, MouseEventArgs e) {
            Input.SetButtonToggle(Input.Button.R);
        }

        private void Button_SELECT_Click(object sender, MouseEventArgs e) {
            Input.SetButtonToggle(Input.Button.Select);
        }

        private void Button_START_Click(object sender, MouseEventArgs e) {
            Input.SetButtonToggle(Input.Button.Start);
        }

        private void Button_X_Click(object sender, MouseEventArgs e) {
            Input.SetButtonToggle(Input.Button.X);
        }

        private void Button_Y_Click(object sender, MouseEventArgs e) {
            Input.SetButtonToggle(Input.Button.Y);
        }

        private void Button_A_Click(object sender, MouseEventArgs e) {
            Input.SetButtonToggle(Input.Button.A);
        }

        private void Button_B_Click(object sender, MouseEventArgs e) {
            Input.SetButtonToggle(Input.Button.B);
        }

        /// <summary>
        /// 押したキーがkeyCodeToButton辞書に登録されていれば、
        /// 対応したボタンを押したことにする
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_KeyDown(object sender, KeyEventArgs e) {
            kInput.KeyDown(sender, e);
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e) {
            kInput.KeyUp(sender, e);
        }
    }
}
