using System.Collections.ObjectModel;
using System.Windows.Input;
using WorkTimeLogger.ViewModel.Services;
using WorkTimeLogger.ViewModel.Commands;

namespace WorkTimeLogger
{
    public class MainWindowViewModel : ViewModelBase
    {
        #region Fields

        // Property variables
        private ObservableCollection<WorkItem> p_WorkItemList;
        private int p_ItemCount;

        #endregion

        #region Constructor

        public MainWindowViewModel()
        {
            this.Initialize();
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
        /// The number of items in the workitem list.
        /// </summary>
        public int ItemCount
        {
            get { return p_ItemCount; }

            set
            {
                p_ItemCount = value;
                base.RaisePropertyChangedEvent("ItemCount");
            }
        }

        #endregion

        #region Event Handlers

        /// <summary>
        /// Updates the ItemCount Property when the WorkItemList collection changes.
        /// </summary>
        void OnWorkItemListChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            // Update item count
            this.ItemCount = this.WorkItemList.Count;

            // Resequence list
            SequencingService.SetCollectionSequence(this.WorkItemList);
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

            // Add items to the list
            p_WorkItemList.Add(new WorkItem("Macaroni"));
            p_WorkItemList.Add(new WorkItem("Shredded Wheat"));
            p_WorkItemList.Add(new WorkItem("Fish Filets"));
            p_WorkItemList.Add(new WorkItem("Hamburger Buns"));
            p_WorkItemList.Add(new WorkItem("Whipped Cream"));
            p_WorkItemList.Add(new WorkItem("Soft Drinks"));
            p_WorkItemList.Add(new WorkItem("Bread"));

            // Initialize list index
            this.WorkItemList = SequencingService.SetCollectionSequence(this.WorkItemList);

            // Update bindings
            base.RaisePropertyChangedEvent("WorkItemList");
        }

        #endregion
    }
}
