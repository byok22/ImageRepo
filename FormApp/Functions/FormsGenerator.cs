using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ApplicationsA.Functions;

namespace FormApp.Functions
{
    public static class FormsGenerator
    {
        //items for CheckedListBox 
        public static   ListBox.ObjectCollection getItemsForCheckedListBoXFromPath(string source)
        {
            FileOperations fileOperations = new FileOperations();

            List<string> listString;

            listString = fileOperations.getFilesFromPath(source);

            CheckedListBox.ObjectCollection items = new CheckedListBox.ObjectCollection(new CheckedListBox());

            foreach (var item in listString)
            {             
               
                items.Add(item);
            }
            return items;
        }
        public static ListBox.ObjectCollection getItemsFromPath(string source)
        {

             FileOperations fileOperations = new FileOperations();

            List<string> listString;

            listString = fileOperations.getFilesFromPath(source);

            ListBox.ObjectCollection items = new ListBox.ObjectCollection(new ListBox());

            foreach (var item in listString)
            {
                
                items.Add(item);
            }
            return items;
 
            
        }
    }
}
