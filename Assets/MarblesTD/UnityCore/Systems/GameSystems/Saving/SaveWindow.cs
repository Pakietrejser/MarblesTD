using System;
using System.IO;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using MarblesTD.Core.Common.Automatons;
using MarblesTD.Core.Common.Requests;
using MarblesTD.Core.Common.Requests.List;
using MarblesTD.UnityCore.Common.RequestHandlers;
using MarblesTD.UnityCore.Systems.MapSystems;
using MarblesTD.UnityCore.Systems.ScenarioSystems;
using Newtonsoft.Json;
using UnityEngine;
using Zenject;

namespace MarblesTD.UnityCore.Systems.GameSystems.Saving
{
    public class SaveWindow : MonoRequestHandler<SaveGameRequest, bool>, IState
    {
        [Inject] Mediator Mediator { get; set; }
        [Inject] GameSettings GameSettings { get; set; }
        [Inject] ScenarioSpawner ScenarioSpawner { get; set; }
        [Inject] MainMenu MainMenu { get; set; }
        [Inject] TowerControllerView TowerControllerView { get; set; }
        
        [SerializeField] CanvasGroup windowBox;
        [SerializeField] SaveButton[] saveButtons;

        string _currentSaveName;

        protected override async UniTask<bool> Execute(SaveGameRequest request)
        {
            return SaveJsonData(_currentSaveName, false);
        }

        void Awake()
        {
            foreach (var saveButton in saveButtons)
            {
                saveButton.CreateSaveClicked += HandleCreateSave;
                saveButton.DeleteSaveClicked += HandleDeleteSave;
            }
        }

        public void EnterState()
        {
            UpdateSaveButtons();
            Show();
        }

        public void ExitState()
        {
            windowBox.gameObject.SetActive(false);
        }

        void Show()
        {
            windowBox.transform.localPosition = Vector3.up * 1000;
            gameObject.SetActive(true);
            windowBox.transform.DOKill();
            windowBox.transform.DOLocalMoveY(50, 1.7f).SetEase(Ease.OutBounce);
        }

        async void Hide()
        {
            windowBox.interactable = false;
            windowBox.transform.DOKill();
            windowBox.transform.DOScale(Vector3.one * .01f, .2f);
            await UniTask.Delay(TimeSpan.FromSeconds(.2f));
            windowBox.gameObject.SetActive(false);
        }

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
                    await Mediator.SendAsync(new BinaryChoiceRequest("Stary Zapis", "Ten zapis gry już nie zadziała, proszę usuń go i stwórz nowy.", true));
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
            bool proceed = await Mediator.SendAsync(new BinaryChoiceRequest("Usuń Zapis", "Jesteś pewien że chcesz usunąć ten zapis gry? Stracisz postęp swoich misji."));
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
                fileNames[i] = fileNames[i].Remove(0, 1);
                fileNames[i] = fileNames[i].Remove(fileNames[i].Length - 5);
                // transforms "\name.save" to "name"
            }

            return fileNames;
        }
        
        bool SaveJsonData(string fileName, bool freshSave)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                Debug.Log("Trying to create an empty save.");
                return false;
            }
            
            var saveData = new SaveData();
            
            ((ISaveable) GameSettings).Save(saveData, freshSave);
            ((ISaveable) ScenarioSpawner).Save(saveData, freshSave);
            ((ISaveable) TowerControllerView).Save(saveData, freshSave);
            
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

            try
            {
                ((ISaveable) GameSettings).Load(saveData);
                ((ISaveable) ScenarioSpawner).Load(saveData);
                ((ISaveable) TowerControllerView).Load(saveData);
            }
            catch (Exception e)
            {
                return false;
            }
            
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