using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace KFIRPG.editor {
    class AlreadyLockedException: Exception {
        public AlreadyLockedException() : base() { }
        public AlreadyLockedException(string target) : base(string.Format("{0} is locked!", target)) { }
    }

    class LockFileNotEmptyException: Exception {
        public LockFileNotEmptyException() : base() { }
        public LockFileNotEmptyException(string target) : base(string.Format("{0} is not empty!", target)) { }
    }


    class Locker: IDisposable {
        const string FILENAME = ".lock";

        string targetFolder = null;
        string TargetLockFile {
            get {
                return Path.Combine(targetFolder, FILENAME);
            }
        }

        public Locker() { }
        public Locker(string targetFolder) {
            this.targetFolder = targetFolder;
        }

        bool IsFileEmpty(string fileName) {
            using (FileStream s = File.OpenRead(fileName)) {
                s.Seek(0, SeekOrigin.End);
                return (s.Position == 0);
            }
        }

        public void Lock(string targetFolder) {
            Unlock();
            this.targetFolder = targetFolder;
            if (File.Exists(TargetLockFile)) throw new AlreadyLockedException(TargetLockFile);
            File.Create(TargetLockFile).Close();
        }

        public void Unlock() {
            if (targetFolder == null) return;
            if (!File.Exists(TargetLockFile)) return;
            if (!IsFileEmpty(TargetLockFile)) throw new LockFileNotEmptyException(TargetLockFile);
            File.Delete(TargetLockFile);
        }

        #region IDisposable Members

        public void Dispose() {
            this.Unlock();
        }

        #endregion
    }
}
