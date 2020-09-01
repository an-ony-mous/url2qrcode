using QRCoder;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;

namespace URL2QRCode {
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public MainWindow() {
            InitializeComponent();
        }

        /// <summary>
        /// QRコード生成処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnQRCode_Click(object sender, RoutedEventArgs e) {
            // 未入力の場合は何もしない
            if (string.IsNullOrEmpty(TxtURL.Text)) {
                return;
            }
            // QRコード生成処理
            ImgQRCode.Source = CreateQRCode(TxtURL.Text);
        }

        /// <summary>
        /// QRコード生成処理
        /// </summary>
        /// <param name="pURL"></param>
        /// <returns></returns>
        private BitmapFrame CreateQRCode(string pURL) {
            // 処理結果
            BitmapFrame eResult = null;
            // ジェネレーターオブジェクト生成
            var eQRCodeGenerator = new QRCodeGenerator();
            // QRコードオブジェクト生成
            var eQRCodeData = eQRCodeGenerator.CreateQrCode(pURL, QRCodeGenerator.ECCLevel.Q);
            var eQRCode = new QRCode(eQRCodeData);
            // Bitmap変換
            var eBitmapQrCode = eQRCode.GetGraphic(20);
            // リサイズ
            var eBitmap = new Bitmap(eBitmapQrCode, 350, 350);
            // QRコードオブジェクト破棄
            eQRCode.Dispose();
            // 画像の表示
            using (var eStream = new MemoryStream()) {
                eBitmap.Save(eStream, ImageFormat.Bmp);
                eStream.Seek(0, SeekOrigin.Begin);
                eResult = BitmapFrame.Create(eStream, BitmapCreateOptions.None, BitmapCacheOption.OnLoad);
            }
            return eResult;
        }
    }
}
