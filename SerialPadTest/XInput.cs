using System;
using System.Drawing;
using System.IO.Ports;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using static XInputWrapper;

class XInputWrapper {
    public const uint ERROR_SUCCESS = 0;
    public const uint ERROR_DEVICE_NOT_CONNECTED = 0x48f;

    [StructLayout(LayoutKind.Sequential)]
    public struct XINPUT_GAMEPAD {
        public ushort wButtons;
        public byte bLeftTrigger;
        public byte bRightTrigger;
        public short sThumbLX;
        public short sThumbLY;
        public short sThumbRX;
        public short sThumbRY;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct XINPUT_STATE {
        public uint dwPacketNumber;
        public XINPUT_GAMEPAD Gamepad;
    }

    [DllImport("Xinput1_4.dll")]
    public static extern uint XInputGetState(uint dwUserIndex, ref XINPUT_STATE pState);
}

namespace SerialPadTest {
    public class XInput {
        private Timer connectionCheckTimer, statusCheckTimer;

        private XInputWrapper.XINPUT_STATE _XInputState = new XInputWrapper.XINPUT_STATE();
        private uint _Status = 0;

        public Label XInputConnectionLabel;

        private XInputWrapper.XINPUT_STATE XInputState {
            get => _XInputState;
            set {
                _XInputState = value;
                for (int i = 0; i < 16; i++) {
                    
                    Input.SetButtonToggle((Input.Button)((value.Gamepad.wButtons >> i) & 0x1));

                }
            }
        }

        public XInput() {
            ShownHandler();
        }

        private void ShownHandler() {
            connectionCheckTimer = new Timer();
            connectionCheckTimer.Interval = 1000;
            connectionCheckTimer.Tick += ConnectionCheck;
            connectionCheckTimer.Start();
            statusCheckTimer = new Timer();
            statusCheckTimer.Interval = 15;
            statusCheckTimer.Tick += StatusCheck;
            statusCheckTimer.Start();
        }

        private void ConnectionCheck(object sender, EventArgs e) {
            if (_Status != XInputWrapper.ERROR_SUCCESS) {
                XInputWrapper.XINPUT_STATE state = new XInputWrapper.XINPUT_STATE();
                uint status = XInputWrapper.XInputGetState(0, ref state);
                _Status = status;
                if (status == XInputWrapper.ERROR_SUCCESS) {
                    _XInputState = state;
                    XInputConnectionLabel.Text = "Connected!";
                }
            } else {
                XInputConnectionLabel.Text = "Disconnected.";
            }
        }

        private void StatusCheck(object sender, EventArgs e) {
            if (_Status == XInputWrapper.ERROR_SUCCESS) {
                XInputWrapper.XINPUT_STATE state = new XInputWrapper.XINPUT_STATE();
                uint status = XInputWrapper.XInputGetState(0, ref state);
                _Status = status;
                if (status == XInputWrapper.ERROR_SUCCESS)
                    _XInputState = state;
            }
        }
    }
}