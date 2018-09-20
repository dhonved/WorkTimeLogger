using System;

namespace WorkTimeLogger
{
    public class WorkItem : ObservableObject, ISequencedObject
    {
        #region Fields

        // Property variables
        private int p_WorkItemID;
        private string p_Name;
        private TimeSpan p_TimeSpent;

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public WorkItem()
        {
        }

        /// <summary>
        /// Parameterized constructor.
        /// </summary>
        /// <param name="itemName">The name of the work item.</param>
        public WorkItem(string itemName)
        {
            p_Name = itemName;
            p_TimeSpent = new TimeSpan(0, 0, 0);
        }

        /// <summary>
        /// Parameterized constructor.
        /// </summary>
        /// <param name="itemName">The name of the work item.</param>
        /// <param name="itemIndex">The sequential position of the item in a story list.</param>
        public WorkItem(string itemName, int itemIndex)
        {
            p_Name = itemName;
            p_WorkItemID = itemIndex;
            p_TimeSpent = new TimeSpan(0, 0, 0);
        }

        #endregion

        #region Properties

        /// <summary>
        /// The sequential position of this item in a list of items.
        /// </summary>
        public int WorkItemID
        {
            get { return p_WorkItemID; }

            set
            {
                p_WorkItemID = value;
                base.RaisePropertyChangedEvent("WorkItemID");
            }
        }

        /// <summary>
        /// The name of the work item.
        /// </summary>
        public string Name
        {
            get { return p_Name; }

            set
            {
                p_Name = value;
                base.RaisePropertyChangedEvent("Name");
            }
        }

        /// <summary>
        /// The time already spent on the work item.
        /// </summary>
        public TimeSpan TimeSpent
        {
            get { return p_TimeSpent; }

            set
            {
                p_TimeSpent = value;
                base.RaisePropertyChangedEvent("TimeSpent");
            }
        }

        #endregion

        #region Overrides

        /// <summary>
        /// Sets the item name as its ToString() value.
        /// </summary>
        /// <returns>The name of the item.</returns>
        public override string ToString()
        {
            return Name;
        }

        #endregion
    }
}
