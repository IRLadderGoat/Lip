using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace lip
{
    class Hotkey
    {
        /// <summary>
        /// Typical hotkey assigngments
        /// </summary>
        public enum FsModifiers
        {
            Alt = 0x0001,
            Control = 0x0002,
            Shift = 0x0004, // Changes!
            Window = 0x0008,
        }

        private IntPtr _hWnd;

        public Hotkey(IntPtr hWnd)
        {
            this._hWnd = hWnd;
        }

        public void RegisterHotKeys()
        {
            RegisterHotKey(_hWnd, 0, (int)FsModifiers.Control, Keys.M);
        }

        public void UnRegisterHotKeys()
        {
            UnregisterHotKey(_hWnd, 0);
        }

        #region WindowsAPI
        [DllImport("user32.dll")]
        private static extern bool RegisterHotKey(IntPtr hWnd, int id, int fsModifiers, Keys vk);

        [DllImport("user32.dll")]
        private static extern bool UnregisterHotKey(IntPtr hWnd, int id);
        #endregion

    }
}
