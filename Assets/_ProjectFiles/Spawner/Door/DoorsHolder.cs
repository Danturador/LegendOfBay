using System.Collections.Generic;
using System.Linq;
using _ProjectFiles.Spawner.Models;
using UnityEngine;

namespace _GameAssets.Scripts.Spawner.Door
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
                doors[i].isOpened = doorsHolderData.doorsData[i].isOpened;
            }
        }
        
        public List<DoorData> GetDoorsData()
        {
            List<DoorData> doorsData = new List<DoorData>();
            foreach (var door in doors)
            {
                doorsData.Add(new DoorData(door.isOpened));
            }

            return doorsData;
        }
    }
}