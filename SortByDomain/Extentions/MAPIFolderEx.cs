using Microsoft.Office.Interop.Outlook;
using SortByDomain.Helpers;
using System;

namespace SortByDomain.Extentions
{
    public class MAPIFolderEx : IDisposable
    {
        private const int FOLDER_NOT_FOUND = -2147221233;

        private (string entryId, string storeId) folderEntryAndStoreId;
        private MAPIFolder folder;

        public MAPIFolderEx(MAPIFolder folder)
        {
            this.folderEntryAndStoreId = (folder?.EntryID, folder?.StoreID);
            this.folder = folder;
        }

        public MAPIFolder Folder
        {
            get
            {
                return folder;
            }
        }

        public string GetName(ref NameSpace defaultNamespace)
        {
            try
            {
                return folder?.Name;
            }
            catch (System.Exception ex)
            {
                FunctionHelper.Log(ex.Message);

                //Try to reload the folder once, then error
                ReloadFolder(ref defaultNamespace);
            }

            return folder?.Name;
        }

        public MAPIFolder AddSubFolder(string name, ref NameSpace defaultNamespace)
        {
            try
            {
                return folder.Folders.Add(name);
            }
            catch (System.Exception ex)
            {
                FunctionHelper.Log(ex.Message);

                //Try to reload the folder once, then error
                ReloadFolder(ref defaultNamespace);
            }

            return folder.Folders.Add(name);
        }

        public bool GetSubFolderExists(object index, ref NameSpace defaultNamespace)
        {
            try
            {
                return folder.Folders[index] is MAPIFolder;
            }
            catch (System.Exception ex)
            {
                if (ex.HResult == FOLDER_NOT_FOUND)
                {
                    return false;
                }

                FunctionHelper.Log(ex.Message);

                //Try to reload the folder once, then error
                ReloadFolder(ref defaultNamespace);
            }

            try
            {
                return folder.Folders[index] is MAPIFolder;
            }
            catch (System.Exception ex)
            {
                if (ex.HResult == FOLDER_NOT_FOUND)
                {
                    return false;
                }

                FunctionHelper.Log(ex.Message);
            }

            return false;
        }

        public MAPIFolder GetSubFolder(object index, ref NameSpace defaultNamespace)
        {
            try
            {
                return folder.Folders[index];
            }
            catch (System.Exception ex)
            {
                FunctionHelper.Log(ex.Message);

                //Try to reload the folder once, then error
                ReloadFolder(ref defaultNamespace);
            }

            return folder.Folders[index];
        }

        public int GetSubFolderCount(ref NameSpace defaultNamespace)
        {
            try
            {
                return folder.Folders.Count;
            }
            catch (System.Exception ex)
            {
                FunctionHelper.Log(ex.Message);

                //Try to reload the folder once, then error
                ReloadFolder(ref defaultNamespace);
            }

            return folder.Folders.Count;
        }

        public dynamic GetItem(object index, ref NameSpace defaultNamespace)
        {
            try
            {
                return folder.Items[index];
            }
            catch (System.Exception ex)
            {
                FunctionHelper.Log(ex.Message);

                //Try to reload the folder once, then error
                ReloadFolder(ref defaultNamespace);
            }

            return folder.Items[index];
        }

        public int GetItemCount(ref NameSpace defaultNamespace)
        {
            try
            {
                return folder.Items.Count;
            }
            catch (System.Exception ex)
            {
                FunctionHelper.Log(ex.Message);

                //Try to reload the folder once, then error
                ReloadFolder(ref defaultNamespace);
            }

            return folder.Items.Count;
        }

        private void ReloadFolder(ref NameSpace defaultNamespace)
        {
            FunctionHelper.ConsumeFinalReleaseNullComObject(folder);
            folder = null;

            folder = defaultNamespace.GetFolderFromID(folderEntryAndStoreId.entryId, folderEntryAndStoreId.storeId);
        }

        public void Dispose()
        {
            FunctionHelper.ConsumeFinalReleaseNullComObject(folder);
        }
    }
}