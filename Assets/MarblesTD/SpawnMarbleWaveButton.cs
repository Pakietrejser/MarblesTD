using System;
using System.Collections;
using System.Collections.Generic;
using MarblesTD.Core.Marbles;
using MarblesTD.Towers;
using MarblesTD.UnityCore;
using UnityEngine;
using UnityEngine.UI;

namespace MarblesTD
{
    [RequireComponent(typeof(Button))]
    public class SpawnMarbleWaveButton : MonoBehaviour
    {
        [SerializeField] private int marblesSpeed;
        [SerializeField] private int marblesHealth;
        [SerializeField] private int marblesAmount;
        [SerializeField] private float spawnDelay;
        [SerializeField] private GameObject marblePrefab;
        
        private Button spawnButton;

        private void Awake()
        {
            spawnButton = GetComponent<Button>();
            spawnButton.onClick.AddListener(() => StartCoroutine(SpawnWave()));
        }

        IEnumerator SpawnWave()
        {
            for (int i = 0; i < marblesAmount; i++)
            {
                var spawnPosition = Bootstrap.Instance.StartingPosition;
                var go = Instantiate(marblePrefab, spawnPosition);

                go.GetComponent<Rigidbody>().constraints &= ~RigidbodyConstraints.FreezePositionY;

                var view = go.GetComponent<IMarbleView>();
                Bootstrap.Instance.Marbles.Add(new Marble(view,
                    new Vector2(spawnPosition.transform.position.x, spawnPosition.transform.position.z), marblesHealth,
                    marblesSpeed));

                yield return new WaitForSeconds(spawnDelay);
            }
        }
    }
}