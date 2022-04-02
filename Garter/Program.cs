using Garter.FeedProducts;
using System;

namespace Garter
{
    class Program
    {
        static void Main(string[] args)
        {
            ProcessRecords processRecords = new ProcessRecords();
            processRecords.ProcessAllRecords();
        }
    }
}
