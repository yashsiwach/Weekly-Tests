using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;
using System.Threading.Tasks;
public class OrderProcessor
{
    Queue<int> queue = new Queue<int>();
    bool producerDone = false;
    object lockObj = new object();
    int processed = 0;

    /// <summary>
    /// Producer enqueues orders into a shared queue
    /// Three consumers dequeue and process orders concurrently
    /// Processing stops gracefully once producer finishes and queue is empty
    /// </summary>
    public async Task<int> ProcessAsync(int orderCount)
    {
        var producer = Task.Run(() =>
        {
            for (int i = 0; i < orderCount; i++)
                lock (lockObj) queue.Enqueue(i);
            producerDone = true;
        });

        Task[] consumers = new Task[3];
        for (int i = 0; i < 3; i++)
        {
            consumers[i] = Task.Run(async () =>
            {
                while (true)
                {
                    int? item = null;
                    lock (lockObj)
                    {
                        if (queue.Count > 0) item = queue.Dequeue();
                        else if (producerDone) break;
                    }
                    if (item != null)
                    {
                        await Task.Delay(10);
                        Interlocked.Increment(ref processed);
                    }
                }
            });
        }

        await Task.WhenAll(consumers);
        await producer;
        return processed;
    }
}


[TestFixture]
public class OrderProcessorTests
{
    [Test]
    public async Task ProcessesAllOrders()
    {
        var op = new OrderProcessor();
        var result = await op.ProcessAsync(8);
        Assert.That(8, Is.EqualTo(result));
    }
}
