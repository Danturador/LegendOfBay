namespace _ProjectFiles.SaveSystem
{
    public interface IStorage
    {
        object Load(object defaultData = null, bool newGame = false);
        
        void Save(object saveData);
    }
}