using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WiimoteLib;
using System.Drawing;
using System.Diagnostics;

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

        const int averageCount = 10;

        int weightArrayPointer = 0;
        float[] weightArrayX = new float[averageCount];
        float[] weightArrayY = new float[averageCount];

        Rectangle BWBRect;

        float WeigthMaxX = 10f;
        float WeigthMaxY = 10f;

        private Wiimote wiimote = new Wiimote();

        private WiimoteLabels wLabels;

        const int weightListLength = 100;

        System.Timers.Timer timer;
        int jumpTimeMilliSeconds = 1000;

        List<float> weightList = new List<float>();

        public WiimoteInput(WiimoteLabels wLabels) {
            this.wLabels = wLabels;

            BWBRect = wLabels.BWBRect.Bounds;
        }

        public void Connect() {
            wiimote.Connect();
            wiimote.WiimoteChanged += OnWiimoteChanged;

            wiimote.WiimoteState.BalanceBoardState.ZeroPoint.Reset = true;

            wiimote.SetLEDs(true,false,false,false);

            if (timer == null) {
                timer = new System.Timers.Timer();
                timer.Interval = jumpTimeMilliSeconds;
                timer.AutoReset = false;
                timer.Elapsed += (sender, e) => {
                    Input.SetButton(Input.Button.B, false);
                    Debug.WriteLine("Stop B");
                    timer.Stop();
                };
            }
        }

        public void Disconnect() {
            wiimote.Disconnect();

            if (timer != null) {
                timer.Dispose();
            }
        }

        public void ResetZeroPoint() {
            wiimote.WiimoteState.BalanceBoardState.ZeroPoint.Reset = true;

            WeigthMaxX = 10f;
            WeigthMaxY = 10f;
        }

        // 状態更新ハンドラ
        private void OnWiimoteChanged(object sender, WiimoteChangedEventArgs e) {
            // バランスボード
            switch (e.WiimoteState.ExtensionType) {
                case ExtensionType.BalanceBoard:
                BalanceBoardState bbs = e.WiimoteState.BalanceBoardState;

                // 各センサ
                Form1.Form.SetLabel(wLabels.TopLeft, Math.Max(0, bbs.SensorValuesKg.TopLeft).ToString("00.000"));
                Form1.Form.SetLabel(wLabels.TopRight, Math.Max(0, bbs.SensorValuesKg.TopRight).ToString("00.000"));
                Form1.Form.SetLabel(wLabels.BottomLeft, Math.Max(0, bbs.SensorValuesKg.BottomLeft).ToString("00.000"));
                Form1.Form.SetLabel(wLabels.BottomRight, Math.Max(0, bbs.SensorValuesKg.BottomRight).ToString("00.000"));

                // 体重
                Form1.Form.SetLabel(wLabels.Weight, Math.Max(0, bbs.WeightKg).ToString("00.000"));

                Calculation(bbs);

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


        private void Calculation(BalanceBoardState bbs) {
            int sizeX = wLabels.BWBPosition.Width / 2;
            int sizeY = wLabels.BWBPosition.Height / 2;

            int centerX = BWBRect.Width / 2;
            int centerY = BWBRect.Height / 2;

            var sensorsf = bbs.SensorValuesKg;

            float weightX = (sensorsf.TopRight + sensorsf.BottomRight) - (sensorsf.TopLeft + sensorsf.BottomLeft);
            float weightY = (sensorsf.TopLeft + sensorsf.TopRight) - (sensorsf.BottomRight + sensorsf.BottomLeft);

            WeigthMaxX = (WeigthMaxX < weightX) ? weightX : WeigthMaxX;
            WeigthMaxY = (WeigthMaxY < weightY) ? weightY : WeigthMaxY;

            weightArrayX.SetValue(weightX, weightArrayPointer);
            weightArrayY.SetValue(weightY, weightArrayPointer);

            // 今までの重心位置から平均を取る
            float averageX = weightArrayX.Average();
            float averageY = weightArrayY.Average();

            weightArrayPointer++;
            if(weightArrayPointer >= averageCount) {
                weightArrayPointer = 0;
            }

            // ボタン操作を行う
            SetButton(bbs.WeightKg, averageX, averageY, - WeigthMaxX / 2, WeigthMaxX / 2);

            // バランスWiiボードの重心を画面表示するための計算
            int positionX = (int)Map(averageX, -WeigthMaxX / 2, WeigthMaxX / 2, -centerX, centerX);
            int positionY = (int)Map(averageY, -WeigthMaxY / 2, WeigthMaxY / 2, -centerY, centerY);

            var location = new System.Drawing.Point() {
                X = BWBRect.Location.X - sizeX + centerX + Math.Min(Math.Max(positionX, -centerX + sizeX), centerX - sizeX),
                Y = BWBRect.Location.Y - sizeY + centerY - Math.Min(Math.Max(positionY, -centerY + sizeY), centerY - sizeY), //Yは下が+で上が-だった...
            };
            Form1.Form.SetPosition(wLabels.BWBPosition, location);
        }

        /// <summary>
        /// 体重、重心の位置でボタン操作を行う
        /// </summary>
        /// <param name="weight">体重</param>
        /// <param name="weightX">左右の重心位置</param>
        /// <param name="weightY">上下の重心位置</param>
        /// <param name="weightMaxX">左右の重心上限下限</param>
        /// <param name="weightMaxY">上下の重心上限下限</param>
        void SetButton(float weight, float weightX, float weightY, float weightMaxX, float weightMaxY) {
            var dirX = Math.Round(Map(weightX, -weightMaxX / 1f, weightMaxX / 1f, -1, 1));
            var dirY = Math.Round(Map(weightY, -weightMaxY / 1f, weightMaxY / 1f, -1, 1));

            weightList.Insert(0, weight);

            if(weightList.Count > weightListLength) {
                weightList.RemoveRange(weightListLength, weightList.Count - weightListLength);
            }

            //Form1.Form.DebugWriteLine(weightList.Count.ToString());

            if (weightList.Count > 70) {
                float nowWeight = weightList[0];  //現在の値と
                float prevWeight = weightList[10]; // 過去の値を取得

                if (nowWeight < 8 && prevWeight > 10 && !timer.Enabled) {
                    Form1.Form.DebugWriteLine($"now:{nowWeight.ToString("00.00")}, pre:{prevWeight.ToString("00.00")}");
                    Input.SetButton(Input.Button.B, true);
                    timer.Start();
                }
            }

            if (dirX > 0) {
                Input.SetButton(Input.Button.Right, false);
                Input.SetButton(Input.Button.Left, true);
            } else if (dirX < 0) {
                Input.SetButton(Input.Button.Right, true);
                Input.SetButton(Input.Button.Left, false);
            } else {
                Input.SetButton(Input.Button.Right, false);
                Input.SetButton(Input.Button.Left, false);
            }

            if (dirY < 0) {
                Input.SetButton(Input.Button.Up, false);
                Input.SetButton(Input.Button.Down, true);
            } else if (dirY > 0) {
                Input.SetButton(Input.Button.Up, true);
                Input.SetButton(Input.Button.Down, false);
            } else {
                Input.SetButton(Input.Button.Up, false);
                Input.SetButton(Input.Button.Down, false);
            }
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
