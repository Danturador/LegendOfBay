using System;
using System.Collections.Generic;
using _GameAssets.Scripts.Spawner;
using _ProjectFiles.Spawner.Models;
using UnityEngine;

namespace _ProjectFiles.SaveSystem
{
    [Serializable]
    public class GameData
    {
        public float PlayerHealth { get; private set; }
        public Vector3 Position { get; private set; }
        public SpawnersHolderData SpawnersHolderData { get; private set; }
        public byte[] MapTexture { get; private set; }

        public GameData()
        {
            PlayerHealth = 100f;
            Position = Vector3.zero;
            SpawnersHolderData = new SpawnersHolderData();
            MapTexture = null;
        }

        public void SetPlayerHealth(float playerHealth) => PlayerHealth = playerHealth;
        public void SetPosition(Vector3 position) => Position = position;
        public void SetSpawners(SpawnersHolderData data) => SpawnersHolderData = data;
        public void SetTexture(Texture2D sprite) => MapTexture = sprite.EncodeToPNG();
    }
}