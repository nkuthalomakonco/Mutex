Imports System.Threading

Module Module1
    Private mut As Mutex = New Mutex()
    Private Const numIterations As Integer = 1
    Private Const numThreads As Integer = 3
    Sub Main()
        For index As Integer = 1 To numThreads
            Dim Thread As Thread = New Thread(AddressOf ThreadProc)
            Thread.Name = String.Format("Thread{0}", index)
            Thread.Start()
            'Console.WriteLine("Thread: {0}", index)
        Next

        'The main thread exits, but the application continues to
        'run until all foreground threads have exited.
        Console.ReadLine()
    End Sub
    Sub ThreadProc()
        For index As Integer = 1 To numIterations
            UseResource()
        Next
    End Sub
    'This method represents a resource that must be synchronized
    'so that only one thread at a time can enter.
    Sub UseResource()
        Console.WriteLine("{0} is requesting the mutex", Thread.CurrentThread.Name)
        mut.WaitOne()
        Console.WriteLine("{0} has entered the protected area", Thread.CurrentThread.Name)
        'Place code to access non-reentrant resources here.

        'Simulate some work.
        Thread.Sleep(3000)
        Console.WriteLine("{0} is leaving the protected area", Thread.CurrentThread.Name)
        'Release the Mutex.
        mut.ReleaseMutex()
        Console.WriteLine("{0} has released the mutex", Thread.CurrentThread.Name)
    End Sub

End Module
