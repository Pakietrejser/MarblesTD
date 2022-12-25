using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MarblesTD.UnityCore.Systems.Saving
{
    public class SaveButton : MonoBehaviour
    {
        [SerializeField] TMP_InputField inputField;
        [SerializeField] Button createSaveButton;
        [SerializeField] Button deleteSaveButton;

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
            deleteSaveButton.gameObject.SetActive(true);
        }

        public void ShowEmptySave()
        {
            _containsSave = false;
            inputField.text = string.Empty;
            inputField.interactable = true;
            inputField.characterValidation = TMP_InputField.CharacterValidation.Alphanumeric;
            inputField.onValueChanged.RemoveAllListeners();
            inputField.onValueChanged.AddListener(_ => createSaveButton.interactable = true);
            createSaveButton.interactable = false;
            deleteSaveButton.gameObject.SetActive(false);
        }
    }
}