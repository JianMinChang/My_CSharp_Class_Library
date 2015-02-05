using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace MyLibrary.CustomFile
{

    public class FileHandle
    {
        /// <summary>
        /// 是否成功
        /// </summary>
        public bool IsSuccess { private set; get; }

        /// <summary>
        /// 錯誤訊息
        /// </summary>
        public string ErrorMessage { private set; get; }

        /// <summary>
        /// 回復初始值
        /// </summary>
        private void ResetStatus()
        {
            this.IsSuccess = false;
            this.ErrorMessage = string.Empty;
        }


        public static bool IsExistDirectory(string DirectoryPath)
        {
            return Directory.Exists(DirectoryPath);
        }

        public static bool IsExistFile(string FilePath)
        {
            return File.Exists(FilePath);
        }

        public void CreateDirectory(string DirectoryPath)
        {

            ResetStatus();
            if (!IsExistDirectory(DirectoryPath))
            {
                //If Not Exist Directory ,then Create it.
                try
                {
                    Directory.CreateDirectory(DirectoryPath);
                    this.IsSuccess = true;
                }
                catch (Exception ex)
                {
                    this.IsSuccess = false;
                    this.ErrorMessage = ex.Message;
                }

            }
        }

        /// <summary>
        /// 循序讀取.txt檔案內容，每次一行
        /// </summary>
        /// <param name="FileNamePath">檔案路徑</param>
        /// <returns></returns>
        public IList<string> FileReadLineTxtContent(string FileNamePath)
        {
            ResetStatus();
            IList<string> FileContentOnLine = new List<string>();

            string extension = string.Empty;

            extension = Path.GetExtension(FileNamePath);

            if (File.Exists(FileNamePath))
            {
                if (extension.ToLower() == ".txt")
                {
                    try
                    {
                        using (StreamReader sr = new StreamReader(FileNamePath))
                        {
                            string line = string.Empty;
                            while ((line = sr.ReadLine()) != null)
                            {
                                FileContentOnLine.Add(line);
                            }
                        }
                        this.IsSuccess = true;
                    }
                    catch (Exception ex)
                    {
                        this.IsSuccess = false;
                        this.ErrorMessage = ex.Message;
                    }
                }
                else
                {
                    this.ErrorMessage = "File's extension not txt";
                }
            }
            else
            {
                this.ErrorMessage = "File Not Exist";
            }


            return FileContentOnLine;
        }

        /// <summary>
        /// 讀取.txt檔案內容
        /// </summary>
        /// <param name="FileNamePath">檔案路徑</param>
        /// <returns></returns>
        public string FileReadJsonTxtContent(string FileNamePath)
        {
            ResetStatus();
            string FileContentOnLine = string.Empty;

            string extension = string.Empty;

            extension = Path.GetExtension(FileNamePath);

            if (File.Exists(FileNamePath))
            {
                if (extension.ToLower() == ".txt")
                {
                    try
                    {
                        using (StreamReader sr = new StreamReader(FileNamePath))
                        {
                            FileContentOnLine = sr.ReadToEnd();
                        }
                        this.IsSuccess = true;
                    }
                    catch (Exception ex)
                    {
                        this.IsSuccess = false;
                        this.ErrorMessage = ex.Message;
                    }
                }
                else
                {
                    this.ErrorMessage = "File's extension not txt";
                }
            }
            else
            {
                this.ErrorMessage = "File Not Exist";
            }


            return FileContentOnLine;
        }



        /// <summary>
        /// 寫入Txt檔案,檔案存在接續寫入，不存在則建立並寫入
        /// </summary>
        /// <param name="FilePath">檔案路徑</param>
        /// <param name="FileContent">要寫入的內容</param>
        public void FileWriteTxt(string FilePath, string FileContent)
        {

            ResetStatus();
            try
            {
                File.AppendAllText(FilePath, FileContent, System.Text.Encoding.UTF8);
                this.IsSuccess = true;
            }
            catch (Exception ex)
            {
                this.IsSuccess = false;
                this.ErrorMessage = ex.Message;
            }

        }
    }
}
