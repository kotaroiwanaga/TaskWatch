using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TaskWatch.Model.Data;

namespace TaskWatch.Model.TaskRecord
{
    interface ITaskRecord
    {
        void Register(CategoryData categoryData, TaskInfoData taskInfoData, TimeRecordData timeRecordData);
    }
}
