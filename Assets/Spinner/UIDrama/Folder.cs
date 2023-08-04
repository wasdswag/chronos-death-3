using System;
using System.Collections.Generic;
using UnityEngine;

namespace UIDrama
{
    public class Folder : Program
    {
        [SerializeField] private Transform boundA;
        [SerializeField] private Transform boundB;
        [SerializeField] private File[] innerFiles;
        [SerializeField] private List<FolderBoundaries> boundaries;
        private IFile [] _files;

        [SerializeField] private AudioSource audioSource;
        [SerializeField] private AudioClip moveSound;


        private void Awake()
        {
            _files = new IFile[innerFiles.Length];
            for (int i = 0; i < innerFiles.Length; i++)
            {
                _files[i] = innerFiles[i].GetComponent<IFile>();
                innerFiles[i].SetRootFolder(this);
            }
        }
        
        protected override void SetProgress(int percent)
        {
          
        }

        public string GetMissingFiles()
        {
            foreach (var file in _files)
            {
                if (!IsFileInside(file))
                    return file.Filename;
            }
            return null;
        }
        

        public bool IsFileInside(IFile file)
        {
            Vector2 first = boundA.position;
            Vector2 second = boundB.position;
            Vector2 toCheck = file.Position;

            if (first.RectContains(second, toCheck, 0.5f))
                return true;
            
            return false;
        }

        public bool HasThis(FolderBoundaries bound)
        {
            return boundaries.Contains(bound);
        }

        public void PlayMoveSound() => audioSource.PlayOneShot(moveSound);
        
        
    }
}