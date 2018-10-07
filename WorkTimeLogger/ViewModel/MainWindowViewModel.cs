using System.Collections.ObjectModel;
using System.Windows.Input;
using WorkTimeLogger.ViewModel.Services;
using WorkTimeLogger.ViewModel.Commands;
using System.Windows.Threading;
using System;
using System.Reflection;

namespace WorkTimeLogger
{
    public class MainWindowViewModel : ViewModelBase
    {
        #region Fields

        // Property variables
        private ObservableCollection<WorkItem> p_WorkItemList;
        private TimeSpan p_TotalTimeSpent;
        private string p_StatusBarMessage;
        private string p_WindowTitle;

        public DispatcherTimer dispatcherTimer = null;

        private static readonly log4net.ILog log = log4net.LogManager.GetLogger("WorkTimeLogger");

        #endregion

        #region Constructor

        public MainWindowViewModel()
        {
            this.Initialize();

            //  DispatcherTimer setup
            dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(DispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
        }

        #endregion

        #region Command Properties

        /// <summary>
        /// Starts the counter for the currently selected item from the Workitem List.
        /// </summary>
        public ICommand StartWork { get; set; }

        /// <summary>
        /// Stops the counter for the item currently in progress from the Workitem List.
        /// </summary>
        public ICommand StopWork { get; set; }

        #endregion

        #region Data Properties

        /// <summary>
        /// A grocery list.
        /// </summary>
        public ObservableCollection<WorkItem> WorkItemList
        {
            get { return p_WorkItemList; }

            set
            {
                p_WorkItemList = value;
                base.RaisePropertyChangedEvent("WorkItemList");
            }
        }

        /// <summary>
        /// The currently-selected work item.
        /// </summary>
        public WorkItem SelectedItem { get; set; }

        /// <summary>
        /// The work item currently in progress
        /// </summary>
        public WorkItem ActiveItem { get; set; }

        /// <summary>
        /// The total time spent on work items while the app is running
        /// </summary>
        public TimeSpan TotalTimeSpent
        {
            get { return p_TotalTimeSpent; }

            set
            {
                p_TotalTimeSpent = value;
                base.RaisePropertyChangedEvent("TotalTimeSpent");
            }
        }

        /// <summary>
        /// The number of items in the workitem list.
        /// </summary>
        public string StatusBarMessage
        {
            get { return p_StatusBarMessage; }

            set
            {
                p_StatusBarMessage = value;
                base.RaisePropertyChangedEvent("StatusBarMessage");
            }
        }

        /// <summary>
        /// The title of the main window.
        /// </summary>
        public string WindowTitle
        {
            get { return p_WindowTitle; }

            set
            {
                p_WindowTitle = value;
                base.RaisePropertyChangedEvent("WindowTitle");
            }
        }

        #endregion

        #region Event Handlers

        /// <summary>
        /// Updates the ItemCount Property when the WorkItemList collection changes.
        /// </summary>
        void OnWorkItemListChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            
        }

        /// <summary>
        /// Updates the current seconds display 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DispatcherTimer_Tick(object sender, EventArgs e)
        {
            // Updating the active items time spent property
            ActiveItem.TimeSpent = ActiveItem.TimeSpent.Add(new TimeSpan(0, 0, 1));
            TotalTimeSpent = TotalTimeSpent.Add(new TimeSpan(0, 0, 1));
        }
        
        /// <summary>
        /// Triggers logging service to put time spent values into the log file
        /// </summary>
        public void TriggerLogging()
        {
            LoggingService.LogWorkItems(this.WorkItemList);
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Initializes this application.
        /// </summary>
        private void Initialize()
        {
            // Initialize commands
            this.StartWork = new StartCommand(this);
            this.StopWork = new StopCommand(this);

            // Create workitem list
            p_WorkItemList = new ObservableCollection<WorkItem>();

            // Subscribe to CollectionChanged event
            p_WorkItemList.CollectionChanged += OnWorkItemListChanged;

            // Loading work items from worktime log file
            LoggingService.LoadWorkItems(this.WorkItemList);

            // Calculating already spent time on work items
            foreach (WorkItem wItem in WorkItemList)
            {
                p_TotalTimeSpent += wItem.TimeSpent;
            }

            // Updating the window title
            WindowTitle = "WorkTimeLogger v" + Assembly.GetExecutingAssembly().GetName().Version;

            // Update bindings
            base.RaisePropertyChangedEvent("WorkItemList");
        }

        #endregion
    }
}
