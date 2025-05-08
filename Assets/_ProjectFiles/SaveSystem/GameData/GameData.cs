using System;
using _ProjectFiles.Spawner.Models;
using UnityEngine;

namespace _ProjectFiles.SaveSystem
{
    [Serializable]
    public class GameData
    {
        public float PlayerHealth { get; private set; }
        public Vector3 Position { get; private set; }
        public int KeysAmount { get; private set; }
        public SpawnersHolderData SpawnersHolderData { get; private set; }
        public DoorsHolderData DoorsHolderData { get; private set; }
        public KeysHolderData KeysHolderData { get; private set; }
        public byte[] MapTexture { get; private set; }

        public GameData()
        {
            PlayerHealth = 100f;
            Position = Vector3.zero;
            KeysAmount = 0;
            SpawnersHolderData = new SpawnersHolderData();
            DoorsHolderData = new DoorsHolderData();
            KeysHolderData = new KeysHolderData();
            MapTexture = null;
        }

        public void SetPlayerHealth(float playerHealth) => PlayerHealth = playerHealth;
        public void SetPosition(Vector3 position) => Position = position;
        public void SetKeysAmount(int amount) => KeysAmount = amount;
        public void SetSpawners(SpawnersHolderData data) => SpawnersHolderData = data;
        public void SetDoors(DoorsHolderData data) => DoorsHolderData = data;
        public void SetKeys(KeysHolderData data) => KeysHolderData = data;
        public void SetTexture(Texture2D sprite) => MapTexture = sprite.EncodeToPNG();
    }
}