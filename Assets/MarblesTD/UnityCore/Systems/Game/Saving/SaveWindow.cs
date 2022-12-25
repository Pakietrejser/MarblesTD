using System;
using System.IO;
using Cysharp.Threading.Tasks;
using MarblesTD.Core.Common.Automatons;
using MarblesTD.Core.Common.Requests;
using MarblesTD.Core.Common.Requests.List;
using MarblesTD.UnityCore.Common.RequestHandlers;
using Newtonsoft.Json;
using UnityEngine;
using Zenject;

namespace MarblesTD.UnityCore.Systems.Game.Saving
{
    public class SaveWindow : MonoRequestHandler<SaveGameRequest, bool>, IState
    {
        [Inject] Mediator Mediator { get; set; }
        [Inject] GameSettings GameSettings { get; set; }
        [Inject] MainMenu MainMenu { get; set; }
        
        [SerializeField] SaveButton[] saveButtons;

        string _currentSaveName;

        protected override async UniTask<bool> Execute(SaveGameRequest request)
        {
            return SaveJsonData(_currentSaveName, false);
        }
        
        public void Enter()
        {
            UpdateSaveButtons();
            
            foreach (var saveButton in saveButtons)
            {
                saveButton.CreateSaveClicked += HandleCreateSave;
                saveButton.DeleteSaveClicked += HandleDeleteSave;
            }

            Show();
        }
        
        public void Exit()
        {
            Hide();
            
            foreach (var saveButton in saveButtons)
            {
                saveButton.CreateSaveClicked -= HandleCreateSave;
                saveButton.DeleteSaveClicked -= HandleDeleteSave;
            }
        }

        void Show() => gameObject.SetActive(true);
        void Hide() => gameObject.SetActive(false);
        
        async void HandleCreateSave(string saveName, bool containsSave)
        {
            if (containsSave)
            {
                bool successful = LoadJsonData(saveName);
                if (successful)
                {
                    UpdateSaveButtons();
                    
                    Hide();
                    MainMenu.Show();
                }
                else
                {
                    await Mediator.SendAsync(new BinaryChoiceRequest("Old Save", "This save is no longer compatible with the build, remove it and create a new one."));
                }
            }
            else
            {
                SaveJsonData(saveName, true);
                UpdateSaveButtons();

                Hide();
                MainMenu.Show();
            }
        }
        
        async void HandleDeleteSave(string saveName)
        {
            bool proceed = await Mediator.SendAsync(new BinaryChoiceRequest("Delete Save File", "Are you sure you want to delete your save file? All progress will be lost."));
            if (!proceed) return;
            
            RemoveJsonData(saveName);
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
            if(!Directory.Exists(FileWriter.GetPartialPath()))
            {
                Directory.CreateDirectory(FileWriter.GetPartialPath());
            }
            
            string[] fileNames = Directory.GetFiles(FileWriter.GetPartialPath());

            for (var i = 0; i < fileNames.Length; i++)
            {
                fileNames[i] = fileNames[i].Replace(FileWriter.GetPartialPath(), "");
                
                // transform "\name.save" to "name"
                fileNames[i] = fileNames[i].Remove(0, 1);
                fileNames[i] = fileNames[i].Remove(fileNames[i].Length - 5);
            }

            return fileNames;
        }
        
        bool SaveJsonData(string fileName, bool freshSave)
        {
            var saveData = new SaveData();
            GameSettings.Save(saveData, freshSave);
            
            string saveDataString  = JsonConvert.SerializeObject(saveData, Formatting.Indented, new JsonSerializerSettings {TypeNameHandling = TypeNameHandling.Auto});
            if (!FileWriter.WriteToFile($"{fileName}.json", saveDataString))
            {
                _currentSaveName = fileName;
                return true;
            }
            
            return false;
        }
        
        bool LoadJsonData(string fileName)
        {
            if (!FileWriter.LoadFromFile($"{fileName}.json", out string json))
            {
                return false;
            }
            
            var saveData = JsonConvert.DeserializeObject<SaveData>(json, new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Auto});
            GameSettings.Load(saveData);
            
            _currentSaveName = fileName;
            return true;
        }
        
        static void RemoveJsonData(string fileName)
        {
            string filePath = FileWriter.GetFullPath(fileName);
            filePath = $"{Path.GetFullPath(filePath)}.json";
            
            if (File.Exists(filePath))
            { 
                File.Delete(filePath);
                return;
            }

            foreach (string file in Directory.GetFiles(FileWriter.GetPartialPath()))
            {
                if (!file.Contains(fileName)) continue;
                File.Delete(file);
                return;
            }

            throw new Exception($"Couldn't remove a file {fileName} from {FileWriter.GetPartialPath()}");
        }
    }
}