using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MarblesTD.UnityCore.Systems.Game.Saving
{
    public class SaveButton : MonoBehaviour
    {
        [SerializeField] TMP_InputField inputField;
        [SerializeField] Button createSaveButton;
        [SerializeField] Button deleteSaveButton;
        [SerializeField] TMP_Text createSaveText;

        public event Action<string, bool> CreateSaveClicked;
        public event Action<string> DeleteSaveClicked;

        bool _containsSave;

        void Awake()
        {
            createSaveButton.onClick.AddListener(() => CreateSaveClicked?.Invoke(inputField.text, _containsSave));
            deleteSaveButton.onClick.AddListener(() => DeleteSaveClicked?.Invoke(inputField.text));
        }
        
        public void ShowLoadedSave(string fileMame)
        {
            _containsSave = true;
            inputField.text = fileMame;
            inputField.interactable = false;
            createSaveButton.interactable = true;
            createSaveText.text = "Otwórz";
            deleteSaveButton.gameObject.SetActive(true);
        }

        public void ShowEmptySave()
        {
            _containsSave = false;
            inputField.text = string.Empty;
            inputField.interactable = true;
            inputField.characterValidation = TMP_InputField.CharacterValidation.Alphanumeric;
            inputField.onValueChanged.RemoveAllListeners();
            inputField.onValueChanged.AddListener(OnInputFieldChanged);
            createSaveButton.interactable = false;
            createSaveText.text = "Zapisz";
            deleteSaveButton.gameObject.SetActive(false);
        }

        void OnInputFieldChanged(string text)
        {
            createSaveButton.interactable = !string.IsNullOrEmpty(text);
        }
    }
}