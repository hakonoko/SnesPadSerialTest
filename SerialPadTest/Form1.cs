﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.AxHost;

namespace SerialPadTest {

    public partial class Form1 : Form {
        // Clock Cycle: 1  2  3  4  5  6  7  8  9  10 11 12 13 14 15 16
        // Button     : B  Y  Se St U  D  L  R  A  X  L  R  0  0  0  0 
        // Bit        : 15 14 13 12 11 10 9  8  7  6  5  4  3  2  1  0

        internal enum Button {
            B = 15,
            Y = 14,
            Select = 13,
            Start = 12,
            Up = 11,
            Down = 10,
            Left = 9,
            Right = 8,
            A = 7,
            X = 6,
            L = 5,
            R = 4,
            None = -1,
        }

        internal readonly string[] buttonNames = { "B", "Y", "SELECT", "START", "UP", "DOWN", "LEFT", "RIGHT", "A", "X", "L", "R", "0", "0", "0", "0" };

        SerialPort serialPort = new SerialPort();

        ushort buttons = 0;

        private XInputWrapper.XINPUT_STATE _XInputState = new XInputWrapper.XINPUT_STATE();
        private Timer connectionCheckTimer, statusCheckTimer;

        public Form1() {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e) {
            LoadSerialPortList();
            //Shown += ShownHandler;
        }

        //private void ShownHandler(object sender, EventArgs e) {
        //    connectionCheckTimer = new Timer();
        //    connectionCheckTimer.Interval = 1000;
        //    connectionCheckTimer.Tick += ConnectionCheck;
        //    connectionCheckTimer.Start();
        //    statusCheckTimer = new Timer();
        //    statusCheckTimer.Interval = 15;
        //    statusCheckTimer.Tick += StatusCheck;
        //    statusCheckTimer.Start();
        //}

        //private void ConnectionCheck(object sender, EventArgs e) {
        //    for (uint i = 0; i < 4; i++) {
        //        if (cvs[i].Status != XInputWrapper.ERROR_SUCCESS) {
        //            XInputWrapper.XINPUT_STATE state = new XInputWrapper.XINPUT_STATE();
        //            uint status = XInputWrapper.XInputGetState(i, ref state);
        //            cvs[i].Status = status;
        //            if (status == XInputWrapper.ERROR_SUCCESS)
        //                cvs[i].XInputState = state;
        //        }
        //    }
        //}

        //private void StatusCheck(object sender, EventArgs e) {
        //    for (uint i = 0; i < 4; i++) {
        //        if (cvs[i].Status == XInputWrapper.ERROR_SUCCESS) {
        //            XInputWrapper.XINPUT_STATE state = new XInputWrapper.XINPUT_STATE();
        //            uint status = XInputWrapper.XInputGetState(i, ref state);
        //            cvs[i].Status = status;
        //            if (status == XInputWrapper.ERROR_SUCCESS)
        //                cvs[i].XInputState = state;
        //        }
        //    }
        //}

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

                //DebugWriteLine($"Serial \"{serialPort.PortName}\" Connected.");
                buttons = 0;

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

        private void SetButtonToggle(Button button) {
            if (button == Button.None)
                return;

            int bit = (int)button;

            int data = (~buttons >> bit) & 1;
            ushort mask = (ushort)~(0x0000 | (1 << bit));

            buttons &= mask;
            buttons |= (ushort)(data << bit);

            SerialWriteButtons(buttons);
        }

        //private void SetButton(int bit, bool isPush) {
        //    if (isPush) {
        //        buttons |= (ushort)(1 << bit);
        //    } else {
        //        ushort mask = (ushort)~(0x0000 | (1 << bit));
        //        buttons &= mask;
        //    }

        //    SerialWriteButtons(buttons);
        //}
        
        private void SerialWriteButtons(ushort buttons) {
            byte high = (byte)((buttons & 0xFF00) >> 8);
            byte low = (byte)(buttons & 0x00FF);

            if(serialPort.IsOpen)
                serialPort.Write(new byte[] { high, low }, 0, 2);
                                            
            DebugWriteLine("Data: " + (high).ToString("X2") + (low).ToString("X2"));
            if (buttons > 0) {
                StringBuilder sb = new StringBuilder();
                sb.Append("Button: ");
                for (int i = 15; i >= 0; i--) {
                    if (((buttons >> i) & 0x0001) == 1) {
                        sb.Append(buttonNames[15 - i] + ", ");
                    }
                }
                sb.Remove(sb.Length - 2, 1);

                DebugWriteLine(sb.ToString());
            } else {
                DebugWriteLine("Button: None.");
            }
        }

        private void DebugWriteLine(string str) {
            if (!string.IsNullOrEmpty(str)) {
                richTextBox1.AppendText(str);
                richTextBox1.AppendText(Environment.NewLine);
                richTextBox1.ScrollToCaret();
                Debug.WriteLine(str);
            }
        }

        private void Button_UP_MouseDown(object sender, MouseEventArgs e) {
            SetButtonToggle(Button.Up);
        }

        private void Button_LEFT_MouseDown(object sender, MouseEventArgs e) {
            SetButtonToggle(Button.Left);
        }

        private void Button_RIGHT_MouseDown(object sender, MouseEventArgs e) {
            SetButtonToggle(Button.Right);
        }

        private void Button_DOWN_MouseDown(object sender, MouseEventArgs e) {
            SetButtonToggle(Button.Down);
        }

        private void Button_L_Click(object sender, MouseEventArgs e) {
            SetButtonToggle(Button.L);
        }

        private void Button_R_Click(object sender, MouseEventArgs e) {
            SetButtonToggle(Button.R);
        }

        private void Button_SELECT_Click(object sender, MouseEventArgs e) {
            SetButtonToggle(Button.Select);
        }

        private void Button_START_Click(object sender, MouseEventArgs e) {
            SetButtonToggle(Button.Start);
        }

        private void Button_X_Click(object sender, MouseEventArgs e) {
            SetButtonToggle(Button.X);
        }

        private void Button_Y_Click(object sender, MouseEventArgs e) {
            SetButtonToggle(Button.Y);
        }

        private void Button_A_Click(object sender, MouseEventArgs e) {
            SetButtonToggle(Button.A);
        }

        private void Button_B_Click(object sender, MouseEventArgs e) {
            SetButtonToggle(Button.B);
        }

        /// <summary>
        /// キーボードとボタンの対応表
        /// </summary>
        Dictionary<Keys, Button> keyCodeToButton = new Dictionary<Keys, Button>() {
            { Keys.W, Button.Up },
            { Keys.A, Button.Left },
            { Keys.S, Button.Down },
            { Keys.D, Button.Right },
            { Keys.T, Button.Select },
            { Keys.Y, Button.Start },
            { Keys.O, Button.X },
            { Keys.K, Button.Y },
            { Keys.L, Button.B },
            { Keys.Oemplus, Button.A },
            { Keys.E, Button.L },
            { Keys.I, Button.R },
        };
        Dictionary<Button, bool> buttonStatus = new Dictionary<Button, bool>();
        /// <summary>
        /// 押したキーがkeyCodeToButton辞書に登録されていれば、
        /// 対応したボタンを押したことにする
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_KeyDown(object sender, KeyEventArgs e) {
            Button button = Button.None;
            bool stat = false;
            keyCodeToButton.TryGetValue(e.KeyCode, out button);

            // 辞書に対応したキーが登録されているか？
            if(button != Button.None) {
                // buttonStatus辞書に押したキーが登録されているか？
                if (buttonStatus.TryGetValue(button, out stat)) {
                    if (stat) {
                        return;
                    } else {
                        buttonStatus[button] = true;
                    }
                } else {
                    // 登録されていなければ新しく追加
                    buttonStatus.Add(button, true);
                }
            }

            SetButtonToggle(button);
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e) {
            Button button = Button.None;
            keyCodeToButton.TryGetValue(e.KeyCode, out button);

            if (button != Button.None) {
                if (buttonStatus.ContainsKey(button)) {
                    buttonStatus[button] = false;
                }
            }

            SetButtonToggle(button);
        }
    }
}
