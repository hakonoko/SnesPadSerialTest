using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SerialPadTest {
    public class KeyboardInput {
        /// <summary>
        /// キーボードとボタンの対応表
        /// </summary>
        Dictionary<Keys, Input.Button> keyCodeToButton = new Dictionary<Keys, Input.Button>() {
            { Keys.W, Input.Button.Up },
            { Keys.A, Input.Button.Left },
            { Keys.S, Input.Button.Down },
            { Keys.D, Input.Button.Right },
            { Keys.T, Input.Button.Select },
            { Keys.Y, Input.Button.Start },
            { Keys.O, Input.Button.X },
            { Keys.K, Input.Button.Y },
            { Keys.L, Input.Button.B },
            { Keys.Oemplus, Input.Button.A },
            { Keys.E, Input.Button.L },
            { Keys.I, Input.Button.R },
        };
        Dictionary<Input.Button, bool> buttonStatus = new Dictionary<Input.Button, bool>();

        public KeyboardInput() {
        }

        public void KeyDown(object sender, KeyEventArgs e) {

            Input.Button button = Input.Button.None;
            bool stat = false;
            keyCodeToButton.TryGetValue(e.KeyCode, out button);

            // 辞書に対応したキーが登録されているか？
            if (button != Input.Button.None) {
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

            Input.SetButtonToggle(button);
        }

        public void KeyUp(object sender, KeyEventArgs e) {
            Input.Button button = Input.Button.None;
            keyCodeToButton.TryGetValue(e.KeyCode, out button);

            if (button != Input.Button.None) {
                if (buttonStatus.ContainsKey(button)) {
                    buttonStatus[button] = false;
                }
            }

            Input.SetButtonToggle(button);
        }
    }
}
