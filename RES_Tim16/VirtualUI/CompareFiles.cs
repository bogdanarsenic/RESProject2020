﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualUI.Controller;
using VirtualUI.Models;

namespace VirtualUI
{
    public class CompareFiles
    {
        IDeltaController dc;
        public string Content { get; set; }
        public string DatabaseContent { get; set; }
        public string FileId { get; set; }

        public Delta Compare(string content, string databaseContent, string fileId)
        {

            string[] newText = content.Split(new string[] { "\\n" }, StringSplitOptions.None);
            string[] previous = databaseContent.Split(new string[] { "\\n" }, StringSplitOptions.None);

            int lengthOfNewText = newText.Length;
            int lengthOfPreviousText = previous.Length;

            if (lengthOfNewText < lengthOfPreviousText)
            {
                lengthOfNewText = lengthOfPreviousText;
            }

            string deltaContent = "";
            string previousContent = "";
            bool change = false;
            int row = 0;

            dc = new DeltaController();

            Delta d = new Delta();

            for (int i = 0; i < lengthOfNewText; i++)
            {
                if (newText.Length > i)
                {
                    deltaContent = newText[i];
                }

                if (previous.Length > i)
                {
                    previousContent = previous[i];
                }

                if (deltaContent != previousContent)
                {
                    row = i;
                    d.LineRange += ++row + ",";
                    d.Content += deltaContent + "\n";
                    change = true;
                    deltaContent = "";
                }

            }


            return d;
        }
    }
}
