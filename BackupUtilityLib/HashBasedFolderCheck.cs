using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BackupUtilityLib
{
    public class HashBasedFolderCheck
    {
        private string _sourceDir;
        private string sourceName;

        public HashBasedFolderCheck(string sourceDir)
        {
            _sourceDir = sourceDir;
            sourceName = sourceDir.Split('\\').Last();
        }

        #region properties
        public bool ChangedState
        {
            get { return this.CheckIfChanged(_sourceDir); }
        }

        public string CurrentHash
        {
            get;
            set;
        }
        public string SourceName
        {
            get { return this.sourceName; }
            set { this.sourceName = value; }
        }

        public string SourceDir
        {
            get { return this._sourceDir; }
            set { this._sourceDir = value; }
        }
        public int BackupCount
        {
            get;
            set;
        }
        #endregion

        #region methods
        /// <summary>
        /// Stvara hash svih fileova u folderu
        /// </summary>
        private string ComputeFolderHash(string dir)
        {
            string hash = "";
            DirectoryInfo directory = new DirectoryInfo(dir);
            if (directory.Exists == false)                                   //ako source ne postoji
            {
                throw new DirectoryNotFoundException("Source directory does not exist or could not be found: " + dir);
            }
            FileInfo[] content = directory.GetFiles();                      //vraca file listu trenutnog dir-a
            foreach (FileInfo file in content)
            {
                string path = Path.Combine(dir, file.Name);                 //dodajemo npr 'file1.txt' na kraj string-a path koji pointa na dir. trenutno
                using (var md5 = MD5.Create())
                {
                    using (var stream = File.OpenRead(path))
                    {
                        hash += BitConverter.ToString(md5.ComputeHash(stream)).Replace(" - ", string.Empty);                //konkateniramo hash od file-a konvertan u string na pocetni string 'hash'
                        hash += file.Name;                                  //osjetljivost na promjenu filenamea
                    }
                }
            }
            DirectoryInfo[] subDirectories = directory.GetDirectories();    //vraca subdirektorije trenutnog dir-a
            foreach (DirectoryInfo dirinfo in subDirectories)               //nece uc ako nema subdir
            {
                string path = Path.Combine(dir, dirinfo.Name);
                hash += ComputeFolderHash(path);                            //rekurzivni poziv ove metode za svaki subdirektorij, isto se konkatenira na 'hash' varijablu koja potencijalno postaje ogromna
                hash += dirinfo.Name;                                       //osjetljivost na promjenu imena foldera
            }
            using (var md5 = MD5.Create())
            {
                hash = BitConverter.ToString(md5.ComputeHash(Encoding.UTF8.GetBytes(hash))).Replace("-", string.Empty);     //hashamo 'hash' string (konkatenaciju stringova hasheva svih fileova direktorija i subdirektorija) da bismo dobili mnogo kraci digest
            }
            return hash;
        }

        /// <summary>
        /// provjerava je li folder mijenjan preko hash-a
        /// </summary>
        private bool CheckIfChanged(string dir)
        {
            if (CurrentHash == ComputeFolderHash(dir)) return false;
            else
                return true;
        }

        /// <summary>
        /// obavlja se nakon Backup-a
        /// </summary>
        public void UpdateHash()
        {
            CurrentHash = ComputeFolderHash(_sourceDir);
            BackupStateManager.StoreFolderHash(this);
        }
        #endregion
    }
}
