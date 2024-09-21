using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Windows.Forms;

/// <summary>
/// Xinput関連、この人のやつほぼ丸パクリだよ!ありがとう！
/// https://qiita.com/mikecat_mixc/items/7ea7bab63c93f1b2b04d
/// </summary>

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
        /// <summary>
        /// キーボードとボタンの対応表
        /// </summary>
        Dictionary<ushort, Input.Button> keyCodeToButton = new Dictionary<ushort, Input.Button>() {
            { 1, Input.Button.Up },
            { 4, Input.Button.Left },
            { 2, Input.Button.Down },
            { 8, Input.Button.Right },
            { 32, Input.Button.Select },
            { 16, Input.Button.Start },
            { 32768, Input.Button.X },
            { 16384, Input.Button.Y },
            { 4096, Input.Button.B },
            { 8192, Input.Button.A },
            { 256, Input.Button.L },
            { 512, Input.Button.R },
        };

        private Timer connectionCheckTimer, statusCheckTimer;

        private XInputWrapper.XINPUT_STATE _XInputState = new XInputWrapper.XINPUT_STATE();
        private uint _Status = 0;

        public Label XInputConnectionLabel;
        public Label XInputButtonLabel;

        private XInputWrapper.XINPUT_STATE XInputState {
            get => _XInputState;
            set {
                XInputButtonLabel.Text = value.Gamepad.wButtons.ToString("X4");

                if (_XInputState.Gamepad.wButtons != value.Gamepad.wButtons) {
                    _XInputState = value;

                    for (int i = 0; i < 16; i++) {
                        ushort btn = (ushort)(1 << i);
                        bool isPush = (value.Gamepad.wButtons & (1 << i)) > 0;
                        if (keyCodeToButton.TryGetValue(btn, out Input.Button button)) {
                            if(isPush)
                                Input.form.DebugWriteLine(Enum.GetName(typeof(Input.Button), button));

                            Input.SetButton(button, isPush);
                        }
                    }
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
                    XInputState = state;
                    XInputConnectionLabel.Text = "Connected!";
                }
            }
        }

        private void StatusCheck(object sender, EventArgs e) {
            if (_Status == XInputWrapper.ERROR_SUCCESS) {
                XInputWrapper.XINPUT_STATE state = new XInputWrapper.XINPUT_STATE();
                uint status = XInputWrapper.XInputGetState(0, ref state);
                _Status = status;
                if (status == XInputWrapper.ERROR_SUCCESS)
                    XInputState = state;
            }
        }
    }
}