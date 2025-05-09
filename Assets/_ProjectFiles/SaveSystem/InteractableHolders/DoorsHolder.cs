using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Door = _ProjectFiles.FeaturesLevel.LevelElements.IneractiveObjects.Scripts.KeysAndDoors.Door;

namespace _ProjectFiles.SaveSystem.InteractableHolders
{
    public class DoorsHolder : MonoBehaviour
    {
        [Inject] private SaveSystemController _saveSystem;
        [SerializeField] private List<Door> doors;
        
        public void Awake()
        {
            LoadDoorsState(_saveSystem.gameData.DoorsHolderData);
        }

        private void LoadDoorsState(DoorsHolderData doorsHolderData)
        {
            foreach (var data in doorsHolderData.doorsData)
            {
                var door = doors.Find(d => d.Id == data.id);
                door.SetState(data.isOpened);
                door.OnDoorOpened += () => _saveSystem.UpdateDoors(GetDoorsData());
            }
        }
        
        private DoorsHolderData GetDoorsData()
        {
            List<DoorData> doorsData = new List<DoorData>();
            foreach (var door in doors)
            {
                doorsData.Add(new DoorData(door.Id, door.IsDoorsOpened));
            }

            return new DoorsHolderData(doorsData);
        }
    }
}