using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Nyilvantarto_v2.Categories
{
    class ErettsegiTorzslap : Category
    {
        public override string relativePath => @"\Adatok\Érettségi\Törzslap";
        public override string categoryPrettyName => "Érettségi törzslap";
        public override string sqlTableName => "erettsegitorzslap";
        public override string row1Spec => "vizsgaEvVeg";
        public override string row2Spec => "vizsgaTavasz1Osz0";
        public override Panel panelFeltolt { get; set; }
        public override TextBox textBoxFileNameFeltolt { get; set; }

        protected override List<string> customSqlColumns => new List<string>
        {
            "Vizsga éve",
            "Vizsga Időszaka"
        };
    }
}
