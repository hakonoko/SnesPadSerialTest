using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

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

class ControllerView : GroupBox {
    private const int GRID_SIZE = 22;
    private static readonly Font FONT = new Font("MS UI Gothic", 16, GraphicsUnit.Pixel);

    private Label AddLabel(float x, float y, float width, string text) {
        Label label = new Label();
        label.Location = new Point((int)(GRID_SIZE * x), (int)(GRID_SIZE * y));
        label.Size = new Size((int)(GRID_SIZE * width), GRID_SIZE);
        label.Text = text;
        Controls.Add(label);
        return label;
    }

    private Label packetNoLabel, buttonLabel;
    private Label leftTriggerLabel, rightTriggerLabel;
    private Label lxLabel, lyLabel, rxLabel, ryLabel;

    public ControllerView() {
        SuspendLayout();
        Font = FONT;
        base.Size = new Size(GRID_SIZE * 9, (int)(GRID_SIZE * 6.5f));
        packetNoLabel = AddLabel(0.5f, 1, 8, "パケット: -");
        buttonLabel = AddLabel(0.5f, 2, 8, "ボタン: -");
        leftTriggerLabel = AddLabel(0.5f, 3, 4, "LT: -");
        rightTriggerLabel = AddLabel(4.5f, 3, 4, "RT: -");
        lxLabel = AddLabel(0.5f, 4, 4, "LX: -");
        lyLabel = AddLabel(0.5f, 5, 4, "LY: -");
        rxLabel = AddLabel(4.5f, 4, 4, "RX: -");
        ryLabel = AddLabel(4.5f, 5, 4, "RY: -");
        ResumeLayout();
    }

    public new Size Size {
        get {
            return base.Size;
        }
    }

    private uint _Status = 0;
    private string _Text = "";
    private XInputWrapper.XINPUT_STATE _XInputState = new XInputWrapper.XINPUT_STATE();

    private string StatusMessage {
        get {
            if (_Status == XInputWrapper.ERROR_SUCCESS)
                return "";
            else if (_Status == XInputWrapper.ERROR_DEVICE_NOT_CONNECTED)
                return " (切断)";
            else
                return string.Format(" (エラー: 0x{0:x})", _Status);
        }
    }

    public new string Text {
        get {
            return base.Text;
        }
        set {
            _Text = value;
            base.Text = _Text + StatusMessage;
        }
    }

    public uint Status {
        get {
            return _Status;
        }
        set {
            _Status = value;
            base.Text = _Text + StatusMessage;
        }
    }

    public XInputWrapper.XINPUT_STATE XInputState {
        get {
            return _XInputState;
        }
        set {
            _XInputState = value;
            packetNoLabel.Text = string.Format("パケット: {0}", value.dwPacketNumber);
            buttonLabel.Text = string.Format("ボタン: 0x{0:x}", value.Gamepad.wButtons);
            leftTriggerLabel.Text = string.Format("LT: {0}", value.Gamepad.bLeftTrigger);
            rightTriggerLabel.Text = string.Format("RT: {0}", value.Gamepad.bRightTrigger);
            lxLabel.Text = string.Format("LX: {0}", value.Gamepad.sThumbLX);
            lyLabel.Text = string.Format("LY: {0}", value.Gamepad.sThumbLY);
            rxLabel.Text = string.Format("RX: {0}", value.Gamepad.sThumbRX);
            ryLabel.Text = string.Format("RY: {0}", value.Gamepad.sThumbRY);
        }
    }
}

class XInputTest : Form {
    //public static void Main() {
    //    Application.EnableVisualStyles();
    //    Application.SetCompatibleTextRenderingDefault(false);
    //    Application.Run(new XInputTest());
    //}

    private ControllerView[] cvs = new ControllerView[4];
    private Timer connectionCheckTimer, statusCheckTimer;

    public XInputTest() {
        Text = "XInput テスト";
        cvs[0] = new ControllerView();
        cvs[0].Text = "0";
        cvs[0].Location = new Point(10, 10);
        Controls.Add(cvs[0]);
        cvs[1] = new ControllerView();
        cvs[1].Text = "1";
        cvs[1].Location = new Point(20 + cvs[0].Width, 10);
        Controls.Add(cvs[1]);
        cvs[2] = new ControllerView();
        cvs[2].Text = "2";
        cvs[2].Location = new Point(10, 20 + cvs[0].Height);
        Controls.Add(cvs[2]);
        cvs[3] = new ControllerView();
        cvs[3].Text = "3";
        cvs[3].Location = new Point(20 + cvs[0].Width, 20 + cvs[0].Height);
        Controls.Add(cvs[3]);
        FormBorderStyle = FormBorderStyle.FixedSingle;
        MaximizeBox = false;
        ClientSize = new Size(cvs[0].Width * 2 + 30, cvs[0].Height * 2 + 30);

        Shown += ShownHandler;
    }

    private void ShownHandler(object sender, EventArgs e) {
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
        for (uint i = 0; i < 4; i++) {
            if (cvs[i].Status != XInputWrapper.ERROR_SUCCESS) {
                XInputWrapper.XINPUT_STATE state = new XInputWrapper.XINPUT_STATE();
                uint status = XInputWrapper.XInputGetState(i, ref state);
                cvs[i].Status = status;
                if (status == XInputWrapper.ERROR_SUCCESS)
                    cvs[i].XInputState = state;
            }
        }
    }

    private void StatusCheck(object sender, EventArgs e) {
        for (uint i = 0; i < 4; i++) {
            if (cvs[i].Status == XInputWrapper.ERROR_SUCCESS) {
                XInputWrapper.XINPUT_STATE state = new XInputWrapper.XINPUT_STATE();
                uint status = XInputWrapper.XInputGetState(i, ref state);
                cvs[i].Status = status;
                if (status == XInputWrapper.ERROR_SUCCESS)
                    cvs[i].XInputState = state;
            }
        }
    }
}
