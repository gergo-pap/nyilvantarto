using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nyilvantarto_v2.Categories
{
    abstract class Category
    {
        protected abstract string relativePath { get; }
        
        protected abstract string sqlTableName { get; }

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
            Controll.SearchFileInFileExplorer(relativePath, rowId, sqlTableName);
        }
    }
}
