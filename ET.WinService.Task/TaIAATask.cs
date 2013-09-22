using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

using ET.WinService.Core.Task;
using ET.TA.IAA;

namespace ET.WinService.Task
{
    public class TaIAATask:TriggerTimeTask
    {
        public TaIAATask(XElement xelem) : base(xelem) { }

        public override void Execute(object state)
        {
            try
            {
                TaIAAContext context = new TaIAAContext();
                context.Run();
            }
            catch (Exception ex)
            {
                log.Error("任务执行出错！", ex);
            }
        }
    }
}
