using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JabbR.WinRT.Infrastructure.Settings
{
    public class Settings
    {
        private static SettingFile _settingsFile;

        public Settings()
        {
            if (_settingsFile == null)
            {
                Settings.OpenSettingsFile();
            }
        }

        public string UserId
        {
            get
            {
                return _settingsFile.UserId;
            }

            set
            {
                if (_settingsFile.UserId != value)
                {
                    _settingsFile.UserId = value;
                }
            }
        }

        public void Save()
        {
            var storageHelper = new ObjectStorageHelper<SettingFile>(StorageType.Local);
            storageHelper.SaveASync(_settingsFile, "Settings");
        }

        private static void OpenSettingsFile()
        {
            if (_settingsFile == null)
            {
                var storageHelper = new ObjectStorageHelper<SettingFile>(StorageType.Local);
                var loaderTask = storageHelper.LoadASync("Settings");

                try
                {
                    loaderTask.Wait();
                    _settingsFile = loaderTask.Result;
                }
                catch (Exception) { }

                if (_settingsFile == null)
                {
                    _settingsFile = new SettingFile();
                    storageHelper.SaveASync(_settingsFile, "Settings");
                }
            }
        }
    }
}