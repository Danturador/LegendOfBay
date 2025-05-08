using System.Collections.Generic;
using System.Linq;
using _ProjectFiles.Spawner.Models;
using UnityEngine;
using Door = _ProjectFiles.FeaturesLevel.LevelElements.IneractiveObjects.Scripts.KeysAndDoors.Door;

namespace _ProjectFiles.Spawner
{
    public class DoorsHolder : MonoBehaviour
    {
        [SerializeField] private List<Door> doors;
        public List<Door> Spawners => doors;

        public void Init()
        {
            doors = GetComponentsInChildren<Door>().ToList();
        }

        public void UpdateDoorsState(DoorsHolderData doorsHolderData)
        {
            for(int i = 0; i < doors.Count; i++)
            {
                doors[i].SetState(doorsHolderData.doorsData[i].isOpened);
            }
        }
        
        public List<DoorData> GetDoorsData()
        {
            List<DoorData> doorsData = new List<DoorData>();
            foreach (var door in doors)
            {
                doorsData.Add(new DoorData(door.IsDoorsOpened));
            }

            return doorsData;
        }
    }
}