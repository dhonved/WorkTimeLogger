using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Xml.Linq;
using System.Linq;
using System.Xml;
using System.IO;

namespace WorkTimeLogger.ViewModel.Services
{
    public static class LoggingService
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger("WorkTimeLogger");

        /// <summary>
        /// Logs the time spent property of every work item in a collection to a file.
        /// </summary>
        /// <param name="targetCollection">The collection those items are to be logged.</param>
        public static ObservableCollection<T> LogWorkItems<T>(ObservableCollection<T> targetCollection) where T : WorkItem
        {
            // Ensuring that the worktime log will only contain the work items once
            System.IO.File.WriteAllText(@"Logs/WorkTime.log." + DateTime.Now.ToString(@"yyyy-MM-dd"), string.Empty);

            log.Info("<WorkItems>");
            foreach (WorkItem wItem in targetCollection)
            {
                log.Info("\t<WorkItem id=\"" + wItem.WorkItemID + 
                    "\" summary=\"" + wItem.Name +
                    "\" timespent=\"" + (int)wItem.TimeSpent.TotalSeconds + "\" />");
            }
            log.Info("</WorkItems>");

            // Set return value
            return targetCollection;
        }

        /// <summary>
        /// Loads the already existing working time log data into the workitems in the workitem list.
        /// </summary>
        /// <param name="targetCollection">The collection where the items will be loaded.</param>
        /// <returns></returns>
        public static ObservableCollection<WorkItem> LoadWorkItems(ObservableCollection<WorkItem> targetCollection)
        {
            string FileName = @"Logs/WorkTime.log." + DateTime.Now.ToString(@"yyyy-MM-dd");

            if (File.Exists(FileName) && (new FileInfo(FileName).Length == 0))
            {
                targetCollection.Add(new WorkItem("workitem"));
            }
            else
            { 
                XmlDocument doc = new XmlDocument();
                doc.Load(FileName);
                XmlNodeList nodes = doc.GetElementsByTagName("WorkItem");

                foreach (XmlNode n in nodes)
                {
                    WorkItem wItem = new WorkItem("summary")
                    {
                        WorkItemID = Int32.Parse(n.Attributes["id"].Value),
                        Name = n.Attributes["summary"].Value,
                        TimeSpent = TimeSpan.FromSeconds(Int32.Parse(n.Attributes["timespent"].Value))
                    };
                
                    targetCollection.Add(wItem);
                }
            }

            return targetCollection;
        }
    }
}
