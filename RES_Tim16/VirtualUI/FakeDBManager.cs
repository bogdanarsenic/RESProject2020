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

            if (DeltaExists(d.FileId) || d.FileId == "" ||d.Content=="" || d.LineRange=="")
                return false;
            else
                deltas.Add(d);
            return true;


        }

        public bool AddFile(Files f)
        {
            if (f.Id == null || f.Name == null || f.Extension == null)
            {
                throw new ArgumentNullException("Arguments can't be null");
            }

            if (f.Id.Length > 50 || f.Extension.Length > 10 || f.Name.Length > 50)
            {
                throw new ArgumentException("It's above maximum for databases");
            }

            if (FileExists(f.Id) || f.Id == "" || f.Name == "" || f.Extension == "")
                return false;
            else
                files.Add(f);
            return true;

        }

        public bool AddFileContent(FileContent fcontent)
        {
            if (fcontent.FileId == null || fcontent.Content == null)
            {
                throw new ArgumentNullException("Arguments can't be null");
            }

            if (fcontent.FileId.Length > 50 || fcontent.Content.Length > 500 || fcontent.Id.Length > 50)
            {
                throw new ArgumentException("It's above maximum for databases");
            }

            if (fcontent.FileId == "" || fcontent.Content == "")
                return false;
            else
                fileContents.Add(fcontent);
            return true;
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
            if (id == null)
            {
                throw new ArgumentNullException("Arguments can't be null");
            }

            Files file = files.FirstOrDefault(f => f.Id == id);
            if (file == null)
            {
                return false;
            }
            return true;
        }

        public string GetContent(string id)
        {
            if(id==null)
            {
                throw new ArgumentNullException("Arguments can't be null");
            }
            FileContent fileContent = fileContents.FirstOrDefault(fc => fc.FileId == id);
            if(fileContent==null)
            {
                return "INVALID";
            }
            return fileContent.Content;
        }


        public Delta GetDelta(string fileId)
        {
            Delta delta = deltas.FirstOrDefault(d => d.FileId == fileId);
            if (delta == null)
            {
                return null;
            }
            return delta;
        }


        public string GetFileContentId(string id)
        {
            if (id == null)
            {
                throw new ArgumentNullException("Arguments can't be null");
            }
            FileContent fileContent = fileContents.FirstOrDefault(fc => fc.FileId == id);
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
            deltas.Remove(delta);
            Delta deltanovo = new Delta();
            deltanovo.FileId = d.FileId;
            deltanovo.Content = d.Content;
            deltanovo.LineRange = d.LineRange;
            deltas.Add(deltanovo);
            return true;
        }

        public bool UpdateFileContent(FileContent fcontent)
        {

            if (fcontent.FileId == null || fcontent.Content == null || fcontent.Id == null)
            {
                throw new ArgumentNullException("Arguments can't be null");
            }

            if (fcontent.FileId.Length > 50 || fcontent.Content.Length > 500 || fcontent.Id.Length > 50)
            {
                throw new ArgumentException("It's above maximum for databases");
            }

            FileContent fContent = fileContents.FirstOrDefault(f => f.FileId == fcontent.FileId);
            if (fContent == null)
            {
                return false;
            }

            fileContents.Remove(fContent);
            FileContent fc = new FileContent();

            fc.Id = fcontent.Id;
            fc.FileId = fcontent.FileId;
            fc.Content = fcontent.Content;
            fileContents.Add(fcontent);
            return true;
        }
    }
}
