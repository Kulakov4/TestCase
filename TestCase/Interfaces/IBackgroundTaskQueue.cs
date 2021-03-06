using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace TestCase.Interfaces
{
    public interface IBackgroundTaskQueue
    {
        // поставить в очередь
        void QueueBackgroundWorkItem(Func<CancellationToken, Task> workItem);

        // Взять из очереди
        Task<Func<CancellationToken, Task>> DequeueAsync(
            CancellationToken cancellationToken);

    }
}
