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

	/// <summary>
	/// Implements mutual exclusion on folders.
	/// It creates a .lock file inside the folder, and throws an exception
	/// when trying to lock an already locked folder (unless forced).
	/// </summary>
	class Locker: IDisposable {
		const string FILENAME = ".lock";

		string targetFolder = null;
		string TargetLockFile {
			get {
				return CustomTargetLockFile(this.targetFolder);
			}
		}

		string CustomTargetLockFile(string targetFolder) {
			return Path.Combine(targetFolder, FILENAME);
		}

		/// <summary>
		/// Creates an empty lock.
		/// </summary>
		public Locker() { }

		/// <summary>
		/// Creates a lock and immediately locks the target.
		/// </summary>
		/// <see cref="Lock"/>
		/// <param name="targetFolder">The folder to lock</param>
		public Locker(string targetFolder) {
			Lock(targetFolder);
		}

		bool IsFileEmpty(string fileName) {
			using (FileStream s = File.OpenRead(fileName)) {
				s.Seek(0, SeekOrigin.End);
				return (s.Position == 0);
			}
		}

		/// <summary>
		/// Returns the locked state of the folder.
		/// </summary>
		/// <param name="targetFolder">The folder to check</param>
		/// <returns>True, if the target is locked, otherwise false</returns>
		public bool IsLocked(string targetFolder) {
			if (targetFolder == null) throw new ArgumentNullException("targetFolder");
			if (File.Exists(CustomTargetLockFile(targetFolder)) &&
				IsFileEmpty(CustomTargetLockFile(targetFolder))) return true;
			return false;
		}

		/// <summary>
		/// Locks the target folder. An exception is thrown is the folder is already locked.
		/// Releases the lock on the currently locked folder.
		/// </summary>
		/// <param name="targetFolder">The folder to lock</param>
		public void Lock(string targetFolder) {
			Lock(targetFolder, false);
		}

		/// <summary>
		/// Locks the target folder. If the forced parameter is false, an exception is thrown
		/// if the folder is already locked. If it is true, the lock is "stolen".
		/// Releases the lock on the currently locked folder.
		/// </summary>
		/// <param name="targetFolder">The folder to lock</param>
		/// <param name="forced">Force the lock, even if the folder is already locked</param>
		public void Lock(string targetFolder, bool forced) {
			if (targetFolder == null) throw new ArgumentNullException("targetFolder");
			Unlock();
			if (!forced) {
				if (File.Exists(CustomTargetLockFile(targetFolder))) throw new AlreadyLockedException(CustomTargetLockFile(targetFolder));
			}
			this.targetFolder = targetFolder;
			File.Create(TargetLockFile).Close();
		}

		/// <summary>
		/// Releases the lock on the currently locked folder.
		/// </summary>
		public void Unlock() {
			if (targetFolder == null) return;
			if (!File.Exists(TargetLockFile)) return;
			if (!IsFileEmpty(TargetLockFile)) throw new LockFileNotEmptyException(TargetLockFile);
			File.Delete(TargetLockFile);
		}

		#region IDisposable Members

		/// <summary>
		/// Releases the locked on the currently locked folder.
		/// </summary>
		public void Dispose() {
			this.Unlock();
		}

		#endregion
	}
}
