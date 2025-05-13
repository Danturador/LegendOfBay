using System;
using System.Collections.Generic;
using UnityEngine;
using _ProjectFiles.SoundContainer;

namespace _ProjectFiles.SaveSystem
{
	[Serializable]
	public class GameData
	{
		public float PlayerHealth { get; private set; }
		public Vector3 Position { get; private set; }
		public bool HaveGrapplingHook { get; private set; }
		public SpawnersHolderData SpawnersHolderData { get; private set; }
		public DoorsHolderData DoorsHolderData { get; private set; }
		public KeysHolderData KeysHolderData { get; private set; }
		public List<string> Inventory { get; private set; }
		public int keysCollected { get; private set; }
		public SoundType currentAmbient { get; private set; }
        public byte[] MapTexture { get; private set; }

        public GameData()
        {
            PlayerHealth = 100f;
            Position = new Vector3(0, -0.68f, 0);
            HaveGrapplingHook = false;
            SpawnersHolderData = new SpawnersHolderData();
            DoorsHolderData = new DoorsHolderData();
            KeysHolderData = new KeysHolderData();
            Inventory = new List<string>();
			keysCollected = 0;
			currentAmbient = SoundType.AmbientStart;
			MapTexture = null;
        }

        public void SetPlayerHealth(float playerHealth) => PlayerHealth = playerHealth;
        public void SetPosition(Vector3 position) => Position = position;
        public void SetGrapplingHook(bool hasHook) => HaveGrapplingHook = hasHook;
        public void SetSpawners(SpawnersHolderData data) => SpawnersHolderData = data;
        public void SetDoors(DoorsHolderData data) => DoorsHolderData = data;
        public void SetKeys(KeysHolderData data) => KeysHolderData = data;
        public void SetInventory(List<string> data) => Inventory = data;
        public void SetCollectedKeyCount(int data) => keysCollected = data;
        public void SetCurrentAmbient(SoundType data) => currentAmbient = data;
        public void SetTexture(Texture2D sprite) => MapTexture = sprite.EncodeToPNG();
    }
}