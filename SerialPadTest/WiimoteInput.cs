using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WiimoteLib;
using System.Drawing;

namespace SerialPadTest {
    public class WiimoteInput {
        public class WiimoteLabels {
            public Label Weight;
            public Label TopLeft;
            public Label TopRight;
            public Label BottomLeft;
            public Label BottomRight;
            public PictureBox BWBRect;
            public PictureBox BWBPosition;
        }

        Rectangle BWBRect;

        private Wiimote wiimote = new Wiimote();

        private WiimoteLabels wLabels;

        public WiimoteInput(WiimoteLabels wLabels) {
            this.wLabels = wLabels;

            BWBRect = wLabels.BWBRect.Bounds;
        }

        public void Connect() {
            wiimote.Connect();
            wiimote.WiimoteChanged += OnWiimoteChanged;

            wiimote.WiimoteState.BalanceBoardState.ZeroPoint.Reset = true;

            wiimote.SetLEDs(true,false,false,false);
        }

        public void Disconnect() {
            wiimote.Disconnect();
        }

        public void ResetZeroPoint() {
            wiimote.WiimoteState.BalanceBoardState.ZeroPoint.Reset = true;
        }

        // 状態更新ハンドラ
        private void OnWiimoteChanged(object sender, WiimoteChangedEventArgs e) {
            // バランスボード
            switch (e.WiimoteState.ExtensionType) {
                case ExtensionType.BalanceBoard:
                BalanceBoardState bbs = e.WiimoteState.BalanceBoardState;

                // 各センサ
                Form1.Form.SetLabel(wLabels.TopLeft, bbs.SensorValuesKg.TopLeft.ToString());
                Form1.Form.SetLabel(wLabels.TopRight, bbs.SensorValuesKg.TopRight.ToString());
                Form1.Form.SetLabel(wLabels.BottomLeft, bbs.SensorValuesKg.BottomLeft.ToString());
                Form1.Form.SetLabel(wLabels.BottomRight, bbs.SensorValuesKg.BottomRight.ToString());

                // 体重
                Form1.Form.SetLabel(wLabels.Weight, bbs.WeightKg.ToString());

                DrawPosition(bbs.SensorValuesKg);

                //// 歩行
                //if (this.prevCenterOfGravityX > 0) {
                //    if (bbs.CenterOfGravity.X < -5) {
                //        // 左へ重心移動
                //        this.passometer++;
                //        this.prevCenterOfGravityX = bbs.CenterOfGravity.X;
                //        lblPassometer.Text = this.passometer.ToString();

                //        // 向きの更新
                //        UpdateDirection(bbs);
                //        lblDirection.Text = this.direction.ToString();
                //    }
                //} else {
                //    if (bbs.CenterOfGravity.X > 5) {
                //        // 右へ重心移動
                //        this.passometer++;
                //        this.prevCenterOfGravityX = bbs.CenterOfGravity.X;
                //        lblPassometer.Text = this.passometer.ToString();

                //        // 向きの更新
                //        UpdateDirection(bbs);
                //        lblDirection.Text = this.direction.ToString();
                //    }
                //}
                break;
            }
        }

        private void DrawPosition(BalanceBoardSensorsF bbs) {
            int sizeX = wLabels.BWBPosition.Width / 2;
            int sizeY = wLabels.BWBPosition.Height / 2;

            int centerX = BWBRect.Width / 2;
            int centerY = BWBRect.Height / 2;

            float weightX = (bbs.TopRight + bbs.BottomRight) - (bbs.TopLeft + bbs.BottomLeft);
            float weightY = (bbs.TopLeft + bbs.TopRight) - (bbs.BottomRight + bbs.BottomLeft);

            int positionX = (int)Map(weightX, -20, 20, -centerX, centerX);
            int positionY = (int)Map(weightY, -20, 20, -centerY, centerY);

            var location = new System.Drawing.Point() {
                X = BWBRect.Location.X - sizeX + centerX + Math.Min(Math.Max(positionX, -centerX + sizeX), centerX - sizeX),
                Y = BWBRect.Location.Y - sizeY + centerY - Math.Min(Math.Max(positionY, -centerY + sizeY), centerY - sizeY), //Yは下が+で上が-だった...
            };
            Form1.Form.SetPosition(wLabels.BWBPosition, location);
        }

        /// <summary>
        /// 渡された数値をある範囲から別の範囲に変換
        /// </summary>
        /// <param name="value">変換する入力値</param>
        /// <param name="start1">現在の範囲の下限</param>
        /// <param name="stop1">現在の範囲の上限</param>
        /// <param name="start2">変換する範囲の下限</param>
        /// <param name="stop2">変換する範囲の上限</param>
        /// <returns>変換後の値</returns>
        float Map(float value, float start1, float stop1, float start2, float stop2) {
            return start2 + (stop2 - start2) * ((value - start1) / (stop1 - start1));
        }
    }
}
