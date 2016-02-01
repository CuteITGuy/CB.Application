using System;
using System.Linq;


namespace CB.Win32.Hooks
{
    public class HooksHandler: IDisposable
    {
        #region  Properties & Indexers
        public virtual HookBase this[HookTypes type]
        {
            get { return _hooks[(int)type + 1]; }
        }
        #endregion


        #region Methods
        public void Dispose()
        {
            InstallHooks(null);
        }

        public virtual void InstallHooks(params HookTypes[] types)
        {
            if (types == null)
                types = new HookTypes[0];
            var selectedHookTypes = types.Select(t => (int)t + 1).ToArray();
            foreach (var i in selectedHookTypes.Where(i => _hooks[i] != null))
            {
                _hooks[i].Enabled = true;
            }
            foreach (var i in _hooks.Select((h, i) => i).Where(i => _hooks[i] != null).Except(selectedHookTypes))
            {
                _hooks[i].Enabled = false;
            }
        }
        #endregion


        #region Implementation
        protected virtual void InitilizeHooks()
        {
            _hooks = new HookBase[_hookCount];
            _hooks[1] = new JournalRecordHook();
            _hooks[2] = new JournalPlaybackHook();
            _hooks[3] = new KeyboardHook();
            _hooks[8] = new MouseHook();
            _hooks[10] = new DebugHook();
            _hooks[14] = new KeyboardLLHook();
            _hooks[15] = new MouseLLHook();
        }
        #endregion


        #region Variables
        protected readonly int _hookCount;
        protected HookBase[] _hooks;
        #endregion


        #region Contructors & Destructors
        public HooksHandler()
        {
            _hookCount = Enum.GetValues(typeof(HookTypes)).Length;
            InitilizeHooks();
        }

        ~HooksHandler()
        {
            InstallHooks(null);
        }
        #endregion
    }
}