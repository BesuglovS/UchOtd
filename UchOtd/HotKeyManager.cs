using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace UchOtd
{
    [Flags]
    public enum KeyModifiers
    {
        Alt = 1,
        Control = 2,        
        Shift = 4,
        Windows = 8,
        NoRepeat = 0x4000
    }

    public class HotKeyManager
    {
        [DllImport("user32")]
        private static extern bool RegisterHotKey(IntPtr hWnd, int id, uint fsModifiers, uint vk);

        [DllImport("user32")]
        private static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        private static int _id;
        public static event EventHandler<HotKeyEventArgs> HotKeyPressed;
        private static readonly MessageWindow Wnd = new MessageWindow();

        public static int RegisterHotKey(Keys key, uint modifiers)
        {
            int id = System.Threading.Interlocked.Increment(ref _id);
            RegisterHotKey(Wnd.Handle, id, modifiers, (uint)key);
            return id;
        }

        public static bool UnregisterHotKey(int id)
        {
            return UnregisterHotKey(Wnd.Handle, id);
        }

        protected static void OnHotKeyPressed(HotKeyEventArgs e)
        {
            if (HotKeyPressed != null)
            {
                HotKeyPressed(null, e);
            }
        }
        
        private class MessageWindow : Form
        {
            protected override void WndProc(ref Message m)
            {
                if (m.Msg == WmHotkey)
                {
                    var e = new HotKeyEventArgs(m.LParam);
                    OnHotKeyPressed(e);
                }

                base.WndProc(ref m);
            }

            private const int WmHotkey = 0x312;
        }        
    }


    public class HotKeyEventArgs : EventArgs
    {
        public readonly Keys Key;
        public readonly KeyModifiers Modifiers;

        public HotKeyEventArgs(Keys key, KeyModifiers modifiers)
        {
            Key = key;
            Modifiers = modifiers;
        }

        public HotKeyEventArgs(IntPtr hotKeyParam)
        {
            var param = (uint)hotKeyParam.ToInt64();
            Key = (Keys)((param & 0xffff0000) >> 16);
            Modifiers = (KeyModifiers)(param & 0x0000ffff);
        }
    }    
}