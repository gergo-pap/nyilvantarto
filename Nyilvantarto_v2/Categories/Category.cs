using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Nyilvantarto_v2.Categories
{
    abstract class Category
    {
        public abstract string relativePath { get; }
        public abstract string categoryPrettyName { get; }
        public abstract string sqlTableName { get; }
        public abstract string row1Spec { get; }
        public abstract string row2Spec { get; }
        public abstract Panel panelFeltolt { get; set; }
        public abstract TextBox textBoxFileNameFeltolt { get; set; }

        protected abstract List<string> customSqlColumns { get; }

        private List<string> commonSqlColumns => new List<string>
        {
            "Id",
            "Tanuló neve",
            "Anyja neve",
        };

        public List<string> sqlColumns => commonSqlColumns.Concat(customSqlColumns).ToList();
        
        public void OpenFile(string rowId)
        {
            //MessageBox.Show($"rel path: {relativePath} rowid:{rowId}, sqltablename: {sqlTableName}");
            Controll.SearchFileInFileExplorer(relativePath, rowId, sqlTableName);
        }
    }
}
