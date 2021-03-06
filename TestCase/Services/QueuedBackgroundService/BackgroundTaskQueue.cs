using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TestCase.Interfaces;

namespace TestCase.Services.QueuedBackgroundService
{
    public class BackgroundTaskQueue : IBackgroundTaskQueue
    {
        // Потокобезопасная очередь
        private ConcurrentQueue<Func<CancellationToken, Task>> workItems =
            new ConcurrentQueue<Func<CancellationToken, Task>>();

        private SemaphoreSlim signal = new SemaphoreSlim(0);

        // получить задачу из очереди
        public async Task<Func<CancellationToken, Task>> DequeueAsync(CancellationToken cancellationToken)
        {
            await signal.WaitAsync(cancellationToken);
            workItems.TryDequeue(out var workItem);

            return workItem;
        }

        // поставить задачу в очередь
        public void QueueBackgroundWorkItem(Func<CancellationToken, Task> workItem)
        {
            if (workItem == null)
            {
                throw new ArgumentNullException(nameof(workItem));
            }

            // ставим в очередь
            workItems.Enqueue(workItem);
            signal.Release();
        }
    }
}
