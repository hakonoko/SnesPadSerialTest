using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerialPadTest {
    public static class Input {
        public static Form1 form;

        public static SerialPort serialPort;

        // Clock Cycle: 1  2  3  4  5  6  7  8  9  10 11 12 13 14 15 16
        // Button     : B  Y  Se St U  D  L  R  A  X  L  R  0  0  0  0 
        // Bit        : 15 14 13 12 11 10 9  8  7  6  5  4  3  2  1  0

        public enum Button {
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
            None = 0,
        }

        public static readonly string[] buttonNames = { "B", "Y", "SELECT", "START", "UP", "DOWN", "LEFT", "RIGHT", "A", "X", "L", "R", "0", "0", "0", "0" };

        static ushort buttons = 0;
        static ushort prevbuttons = 0;

        public static void SetButtonToggle(Button button) {
            if (button == Button.None)
                return;

            int bit = (int)button;

            int data = (~buttons >> bit) & 1;
            ushort mask = (ushort)~(0x0000 | (1 << bit));

            buttons &= mask;
            buttons |= (ushort)(data << bit);

            SerialWriteButtons(buttons);
        }

        public static void SetButton(Button button, bool isPush) {
            if (button == Button.None)
                return;

            int bit = (int)button;

            if (isPush) {
                buttons |= (ushort)(1 << bit);
            } else {
                ushort mask = (ushort)~(0x0000 | (1 << bit));
                buttons &= mask;
            }

            SerialWriteButtons(buttons);
        }

        public static void SerialWriteButtons(ushort buttons) {
            if (prevbuttons != buttons) {
                prevbuttons = buttons;

                byte high = (byte)((buttons & 0xFF00) >> 8);
                byte low = (byte)(buttons & 0x00FF);

                if (serialPort.IsOpen)
                    serialPort.Write(new byte[] { high, low }, 0, 2);

                form.DebugWriteLine("Data: " + (high).ToString("X2") + (low).ToString("X2"));
                if (buttons > 0) {
                    StringBuilder sb = new StringBuilder();
                    sb.Append("Button: ");
                    for (int i = 15; i >= 0; i--) {
                        if (((buttons >> i) & 0x0001) == 1) {
                            sb.Append(buttonNames[15 - i] + ", ");
                        }
                    }
                    sb.Remove(sb.Length - 2, 1);

                    form.DebugWriteLine(sb.ToString());
                } else {
                    form.DebugWriteLine("Button: None.");
                }
            }
        }

        public static void ResetButton() {
            //form.DebugWriteLine($"Serial \"{serialPort.PortName}\" Connected.");
            buttons = 0;
        }

    }
}
