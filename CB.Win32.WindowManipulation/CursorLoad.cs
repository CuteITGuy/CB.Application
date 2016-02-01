using System;
using System.Collections.Generic;
using CB.Win32.Cursors;


namespace CB.Win32.WindowManipulation
{
    internal class CursorLoad: IDisposable
    {
        #region Fields
        private IntPtr _cursor;
        private string _cursorFile;
        private readonly string _defaultCursor;
        private readonly Dictionary<CursorNames, IntPtr> _preCursors = new Dictionary<CursorNames, IntPtr>();
        #endregion


        #region  Constructors & Destructor
        /// <summary>
        ///     Creates a new instance of CursorLoad and sets default cursor file path.
        /// </summary>
        /// <param name="defaultCursor">The default cursor file path.</param>
        internal CursorLoad(string defaultCursor)
        {
            _defaultCursor = defaultCursor;
            LoadDefaultCursor();
        }
        #endregion


        #region  Properties & Indexers
        /// <summary>
        ///     Gets or sets a nullable relative or absolute path to the cursor file.
        ///     <para>If this value is null or empty, the default cursor will be used.</para>
        ///     <para>Throw Win32Exception if file not found or not a valid cursor.</para>
        /// </summary>
        public string CursorFile
        {
            get { return _cursorFile; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _cursor = Cursor.LoadCursor(value);
                }
                else
                {
                    DestroyCursor();
                    LoadDefaultCursor();
                }
                _cursorFile = value;
            }
        }
        #endregion


        #region Methods
        /// <summary>
        ///     Releases all resources used by the CursorLoad object.
        /// </summary>
        public void Dispose()
        {
            ResetCursor();
            DestroyCursor();
        }

        /// <summary>
        ///     Sets system cursor to the previous value.
        /// </summary>
        public void ResetCursor()
        {
            /*if (preCursor != IntPtr.Zero)
            {
                Cursor.SetSystemCursor(preCursor, CursorNames.Arrow, false);
            }*/
            foreach (var pair in _preCursors)
            {
                if (pair.Value != IntPtr.Zero)
                {
                    Cursor.TrySetSystemCursor(pair.Value, pair.Key, false);
                }
            }
        }

        /// <summary>
        ///     Sets system cursor to the special value.
        /// </summary>
        public void SetCursor()
        {
            //Cursor.SetSystemCursor(cursor, CursorNames.Arrow, out preCursor, false);
            foreach (CursorNames curName in Enum.GetValues(typeof(CursorNames)))
            {
                IntPtr preCursor;
                Cursor.TrySetSystemCursor(_cursor, curName, out preCursor, false);
                _preCursors[curName] = preCursor;
            }
        }
        #endregion


        #region Implementation
        private void DestroyCursor()
        {
            if (_cursor != IntPtr.Zero)
            {
                Cursor.DestroyCursor(_cursor);
                _cursor = IntPtr.Zero;
            }
        }

        private void LoadDefaultCursor()
        {
            _cursor = Cursor.LoadCursor(_defaultCursor);
        }
        #endregion
    }
}