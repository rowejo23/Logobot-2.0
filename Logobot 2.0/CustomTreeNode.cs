using System;
using System.Windows.Forms;

namespace Logobot2_0
{
    class CustomTreeNode : TreeNode
    {
        private string uniqueKey;

        public string UniqueValueKey
        {
            get { return uniqueKey; }
            set { uniqueKey = value; }
        }
    }
}
