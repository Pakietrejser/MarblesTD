using System;
using System.IO;
using MarblesTD.Core.Common.Requests;
using MarblesTD.Core.Common.Requests.List;
using UnityEngine;
using Zenject;

namespace MarblesTD.UnityCore.Systems.Game.Saving
{
    public class SaveManagerView : MonoBehaviour
    {
        [Inject] Mediator mediator;
        
        [SerializeField] SerializationManager serializationManager;
        [SerializeField] SaveButton[] saveButtons;

        void Start()
        {
            UpdateSaveButtons();
            
            foreach (var saveButton in saveButtons)
            {
                saveButton.CreateSaveClicked -= HandleCreateSave;
                saveButton.DeleteSaveClicked -= HandleDeleteSave;
            }
        }
        
        async void HandleCreateSave(string saveName, bool containsSave)
        {
            if (containsSave)
            {
                bool successful = serializationManager.LoadJsonData(saveName);
                if (successful)
                {
                    UpdateSaveButtons();
                    // gameManagerOverlay.ChangeScreen(mainMenu);
                }
                else
                {
                    await mediator.SendAsync(new BinaryChoiceRequest("Old Save", "This save is no longer compatible with the build, remove it and create a new one."));
                }
            }
            else
            {
                serializationManager.SaveJsonData(saveName);
                UpdateSaveButtons();
                
                // gameManagerOverlay.ChangeScreen(mainMenu);
            }
        }
        
        async void HandleDeleteSave(string saveName)
        {
            bool proceed = await mediator.SendAsync(new BinaryChoiceRequest("Delete Save File", "Are you sure you want to delete your save file? All progress will be lost."));
            if (!proceed) return;
            
            serializationManager.RemoveJsonData(saveName);
            UpdateSaveButtons();
        }
        
        void UpdateSaveButtons()
        {
            string[] fileNames = LoadSaveFileNames();
            int fileCount = fileNames.Length;
            int maxFileCount = saveButtons.Length;
            
            if (fileCount > maxFileCount) Debug.LogError($"Too many save files! {fileCount}/{maxFileCount}");

            var i = 0;
            int allowedCount = Math.Min(fileCount, maxFileCount);
            for (; i < allowedCount; i++)
            {
                saveButtons[i].ShowLoadedSave(fileNames[i]);
            }
            for (; i < maxFileCount; i++)
            {
                saveButtons[i].ShowEmptySave();
            }
        }
        
        static string[] LoadSaveFileNames()
        {
            if(!Directory.Exists(FileManager.GetPartialPath()))
            {
                Directory.CreateDirectory(FileManager.GetPartialPath());
            }
            
            string[] fileNames = Directory.GetFiles(FileManager.GetPartialPath());

            for (var i = 0; i < fileNames.Length; i++)
            {
                fileNames[i] = fileNames[i].Replace(FileManager.GetPartialPath(), "");
                
                // transform "\name.save" to "name"
                fileNames[i] = fileNames[i].Remove(0, 1);
                fileNames[i] = fileNames[i].Remove(fileNames[i].Length - 5);
            }

            return fileNames;
        }
    }
}