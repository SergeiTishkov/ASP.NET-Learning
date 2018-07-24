using System;
using System.IO;
using AutoMailSenderApp.Abstractions;

namespace AutoMailSenderApp.Infrastructure
{
    public class FileManipulator : IFileManipulator
    {
        /// <summary>
        /// Verify whether folder at a specific address exists or not and if not, creates it.
        /// </summary>
        /// <param name="fullPath">Specific address of a new folder.</param>
        public void CreateFolder(string fullPath)
        {
            if (!Directory.Exists(fullPath))
            {
                Directory.CreateDirectory(fullPath);
            }
        }

        /// <summary>
        /// Deletes file at a specific address.
        /// </summary>
        /// <param name="fullPath">Specific address of deleting file.</param>
        public void DeleteFile(string fullPath)
        {
            File.Delete(fullPath);
        }

        /// <summary>
        /// Moves invalid or not available attachment to newPathFolder folder with renaiming as $"Invalid attachment {DateTime.Now.ToString("ffff.ss.mm.HH.dd.MMM.yyyy")}.txt".
        /// </summary>
        /// <param name="oldFullPath">Current full path of attachment.</param>
        /// <param name="newPathFolder">New folder of invalid attachment</param>
        public void MoveInvalidAttachment(string oldFullPath, string newPathFolder)
        {
            string newPath = Path.Combine(newPathFolder, $"Invalid attachment {DateTime.Now.ToString("ffff.ss.mm.HH.dd.MMM.yyyy")}.txt");
            File.Move(oldFullPath, newPath);
        }
    }
}
