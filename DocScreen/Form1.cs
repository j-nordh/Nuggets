using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using UtilClasses.Extensions.Objects;
using UtilClasses.Extensions.Types;
namespace DocScreen
{
    public partial class Form1 : Form
    {
        Hotkey _key;
        ScreenshotSettingsForm _settingsForm;
        Image _img;
        [DllImport("user32.dll")]
        static extern IntPtr GetForegroundWindow();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            _key = new Hotkey(Keys.P, false, true, true, false);
            _key.Pressed += OnHotKey;

            var dests = typeof(Form1).Assembly.DefinedTypes.Implementing<IDestination>().Select(t => t.GenerateConstructor<IDestination>()()).ToList();
            foreach (var d in dests)
            {
                var btn = new Button()
                {
                    Text = d.Name,
                    Tag = d
                };
                btn.Click += DestinationButton_Clicked;

                flpDestinations.Controls.Add(btn);
            }
            _settingsForm = new ScreenshotSettingsForm();
            Hide();
        }

        private void DestinationButton_Clicked(object sender, EventArgs e)
        {
            var d = sender.As<Button>().Tag.As<IDestination>();
            var cfg = d.Config;
            var sc = new Screenshot()
            {
                Image = _img
            };
            if (!cfg.NeedsConfig)
            {
                d.Handle(sc);
                return;
            }
            if (!_settingsForm.LastDestination.Equals(d.Name))
                _settingsForm.Set(d.Name, cfg);

            if (_settingsForm.ShowDialog() != DialogResult.OK)
                return;
            _settingsForm.Get(sc);
            d.Handle(sc);
            Hide();
        }

        private void OnHotKey(object sender, HandledEventArgs e)
        {
            var sc = new ScreenCapture();
            _img = sc.CaptureWindow(GetForegroundWindow());
            Show();

        }

        protected override void OnClosing(CancelEventArgs e)
        {
            if (_key != null && _key.Registered)
                _key.Unregister();

            base.OnClosing(e);

        }

        private void flpDestinations_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
