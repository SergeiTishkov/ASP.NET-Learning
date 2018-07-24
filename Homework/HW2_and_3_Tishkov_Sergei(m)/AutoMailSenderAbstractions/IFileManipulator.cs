namespace AutoMailSenderApp.Abstractions
{
    public interface IFileManipulator
    {
        void CreateFolder(string fullPath);

        void DeleteFile(string fullPath);

        void MoveInvalidAttachment(string oldFullPath, string newPathFolder);
    }
}
