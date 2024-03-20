namespace GameCore.Json
{
    public interface IJsonSerialization
    {
        void WriteDataToFile<T>(T dataObj, string fileName);
        void WriteDataToFile<T>(T[] dataObjArray, string fileName);
    }
}