using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BlazorUIComponents.Controls
{
    public class AsyncManualResetEvent
    {
        private volatile TaskCompletionSource<bool> m_tcs = new TaskCompletionSource<bool>();

        public Task WaitAsync() { return m_tcs.Task; }

        public void Set() { m_tcs.TrySetResult(true); }

        public bool IsReset => !m_tcs.Task.IsCompleted;

        public void Reset()
        {
            while (true)
            {
                var tcs = m_tcs;
                if (!tcs.Task.IsCompleted ||
                    Interlocked.CompareExchange(ref m_tcs, new TaskCompletionSource<bool>(), tcs) == tcs)
                    return;
            }
        }
    }

    public class AsyncManualResetEvent<T>
    {
        private volatile TaskCompletionSource<T> m_tcs = new TaskCompletionSource<T>();

        public Task<T> WaitAsync() { return m_tcs.Task; }

        public void Set(T TResult) { m_tcs.TrySetResult(TResult); }

        public bool IsReset => !m_tcs.Task.IsCompleted;

        public void Reset()
        {
            while (true)
            {
                var tcs = m_tcs;
                if (!tcs.Task.IsCompleted ||
                    Interlocked.CompareExchange(ref m_tcs, new TaskCompletionSource<T>(), tcs) == tcs)
                    return;
            }
        }
    }
}
