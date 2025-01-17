namespace _ProjectFiles.SaveSystem
{
    public interface IStorage
    {
        object Load(object defaultData = null);
        
        void Save(object saveData);
    }
}