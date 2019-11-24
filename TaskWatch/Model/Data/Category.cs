using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskWatch.Model.Data
{
    public struct Category
    {
        public string name;
        public string description;

        public Category(string name, string description)
        {
            this.name = name;
            this.description = description;
        }

        /// <summary>
        /// Equalsオーバーライド category同士の比較のみ変更
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>カテゴリ名が同一の場合、true</returns>
        public override bool Equals(object obj)
        {
            if(this.GetType() == obj.GetType())
            {
                Category category = (Category)obj;
                return category.name == this.name;
            }

            return base.Equals(obj);
        }
    }
}
