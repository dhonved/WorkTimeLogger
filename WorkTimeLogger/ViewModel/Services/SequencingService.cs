﻿using System.Collections.ObjectModel;

namespace WorkTimeLogger.ViewModel.Services
{
    public static class SequencingService
    {
        /// <summary>
        /// Resets the sequential order of a collection.
        /// </summary>
        /// <param name="targetCollection">The collection to be re-indexed.</param>
        public static ObservableCollection<T> SetCollectionSequence<T>(ObservableCollection<T> targetCollection) where T : ISequencedObject
        {
            // Initialize
            var sequenceNumber = 1;

            // Resequence
            foreach (ISequencedObject sequencedObject in targetCollection)
            {
                sequencedObject.WorkItemID = sequenceNumber;
                sequenceNumber++;
            }

            // Set return value
            return targetCollection;
        }
    }
}