﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualUI.Models;

namespace VirtualUI
{
    public class FakeDBManager : Access.IDBManager
    {
        static FakeDBManager instance;

        public static FakeDBManager Instance
        {
            get
            {
                if (instance == null)
                    return new FakeDBManager();
                else
                    return instance;
            }
        }

        private List<Delta> deltas = new List<Delta>();
        private List<Files> files = new List<Files>();
        private List<FileContent> fileContents = new List<FileContent>();
        
        public bool AddDelta(Delta d)
        {
            if (d.FileId == null || d.Content == null || d.LineRange == null)
            {
                throw new ArgumentNullException("Arguments can't be null");
            }

            if (d.FileId.Length > 50 || d.Content.Length > 500 || d.LineRange.Length > 50)
            {
                throw new ArgumentException("It's above maximum for databases");
            }

            if (DeltaExists(d.FileId) || d.FileId == "")
                return false;
            else
                deltas.Add(d);
            return true;


        }

        public bool AddFile(Files f)
        {
            try
            {
                files.Add(f);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool AddFileContent(FileContent fcontent)
        {
            try
            {
                fileContents.Add(fcontent);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool DeltaExists(string id)
        {
            if(id==null)
            {
                throw new ArgumentNullException("Arguments can't be null");
            }
            

            Delta delta = deltas.FirstOrDefault(d => d.FileId == id);
            if(delta==null)
            {
                return false;
            }
            return true;
        }


        public bool FileExists(string id)
        {
            Files file = files.FirstOrDefault(f => f.Id == id);
            if (file == null)
            {
                return false;
            }
            return true;
        }

        public string GetContent(string id)
        {
            FileContent fileContent = fileContents.FirstOrDefault(fc => fc.FileId == id);
            if(fileContent==null)
            {
                return "INVALID";
            }
            return fileContent.Content;
        }


        public string GetFileContentId(string fileId)
        {
            FileContent fileContent = fileContents.FirstOrDefault(fc => fc.FileId == fileId);
            if (fileContent == null)
            {
                return "INVALID";
            }
            return fileContent.Id;
        }

        public bool UpdateDelta(Delta d)
        {

            if (d.FileId == null || d.Content == null || d.LineRange == null)
            {
                throw new ArgumentNullException("Arguments can't be null");
            }

            if (d.FileId.Length > 50 || d.Content.Length > 500 || d.LineRange.Length > 50)
            {
                throw new ArgumentException("It's above maximum for databases");
            }

            Delta delta = deltas.FirstOrDefault(de => de.FileId == d.FileId);

            if (delta == null)
            {
                return false;
            }
            delta.FileId = d.FileId;
            delta.Content = d.Content;
            delta.LineRange = d.LineRange;
            return true;
        }

        public bool UpdateFileContent(FileContent fcontent)
        {
            FileContent fContent = fileContents.FirstOrDefault(f => f.Id == fcontent.Id);
            if (fContent == null)
            {
                return false;
            }
            fContent.Id = fcontent.Id;
            fContent.FileId = fcontent.FileId;
            fContent.Content = fcontent.Content;
            return true;
        }
    }
}