using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ApplicationsA.Functions;
using Domain.Common;

namespace FormApp.Functions
{
    public static class FormsGenerator
    {
        //items for CheckedListBox 
        public static   ListBox.ObjectCollection GetItemsForCheckedListBoXFromPath(string source)
        {
            FileOperations fileOperations = new FileOperations();

            List<string> listString;

            listString = fileOperations.GetFilesFromPath(source);

            CheckedListBox.ObjectCollection items = new CheckedListBox.ObjectCollection(new CheckedListBox());

            foreach (var item in listString)
            {             
               
                items.Add(item);
            }
            return items;
        }
        public static ListBox.ObjectCollection GetItemsFromPath(string source)
        {

             FileOperations fileOperations = new FileOperations();

            List<string> listString;

            listString = fileOperations.GetFilesFromPath(source);

            ListBox.ObjectCollection items = new ListBox.ObjectCollection(new ListBox());

            foreach (var item in listString)
            {
                
                items.Add(item);
            }
            return items;
 
            
        }
        public static void setLabelTextColorSafe(Label label, string txt, Color? color = null)
        {
            try
            {
                if (label.InvokeRequired)
                {
                    label.Invoke(new Action(() => label.Text = txt));
                    if (color != null)
                    {
                        label.Invoke(new Action(() => label.BackColor = (Color)color));
                    }
                    return;
                }
                label.Text = txt;
                if (color != null)
                {
                    label.BackColor = (Color)color;
                }
            }
            catch(Exception ex)
            {
                LoggerImage.WriteLog(ex.Message, "FormsGenerator");
            }
           
        }
        public static void setTextboxTextColorSafe(TextBox label, string txt, Color? color = null)
        {
            try
            {
                if (label.InvokeRequired)
                {
                    label.Invoke(new Action(() => label.Text = txt));
                    if (color != null)
                    {
                        label.Invoke(new Action(() => label.BackColor = (Color)color));
                    }
                    return;
                }
                label.Text = txt;
                if (color != null)
                {
                    label.BackColor = (Color)color;
                }
            }
            catch(Exception ex)
            {
                LoggerImage.WriteLog(ex.Message, "FormsGenerator");
            }
           
        }
    }
}
