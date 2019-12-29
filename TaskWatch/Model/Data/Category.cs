using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskWatch.Model.Data
{
    public class Category
    {
        public string name;
        public string description;
        public List<TaskInfo> taskInfos;

        public Category(string name)
        {
            this.name = name;
            this.description = "";
            this.taskInfos = new List<TaskInfo>();
        }

        public Category(string name, string description)
        {
            this.name = name;
            this.description = description;
            this.taskInfos = new List<TaskInfo>();
        }

        public Category(CategoryData categoryData)
        {
            this.name = categoryData.name;
            this.description = categoryData.description;
            this.taskInfos = new List<TaskInfo>();
        }

        public CategoryData ToStruct()
        {
            return new CategoryData(name, description);
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

    public struct CategoryData
    {
        public string name;
        public string description;

        public CategoryData(string name = "", string description = "")
        {
            this.name = name;
            this.description = description;
        }
    }
}
