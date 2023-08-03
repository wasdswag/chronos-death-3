using System;
using UnityEngine;

namespace UIDrama
{
    public class Folder : MonoBehaviour, IProgram
    {
        [field: SerializeField] public GameObject[] UIDramaElements { get; set; }
        public bool IsRunning { get; set; }
        [SerializeField] private File[] innerFiles;
        private IFile [] _files;

        private void Awake()
        {
            _files = new IFile[innerFiles.Length];
            for (int i = 0; i < innerFiles.Length; i++)
                _files[i] = innerFiles[i].GetComponent<IFile>();
        }


        public void Run()
        {
            foreach (var uiObj in UIDramaElements)
                uiObj.SetActive(true);
            
        }

        public void Stop()
        {
            
        }
    }
}