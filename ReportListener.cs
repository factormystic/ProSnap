using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace ProSnap
{
    public class ReportListener : TraceListener
    {
        public List<TraceMessage> Messages = new List<TraceMessage>();

        public override void Write(string message)
        {
            Write(message, string.Empty);
        }

        public override void Write(string message, string category)
        {
            var last = Messages.LastOrDefault();
            if (last != null && last.Category == category && !last.CompleteLine)
            {
                last.Append(message);
            }
            else
            {
                Messages.Add(new TraceMessage(false, message, category));
            }
        }

        public override void WriteLine(string message)
        {
            WriteLine(message, string.Empty);
        }

        public override void WriteLine(string message, string category)
        {
            Messages.Add(new TraceMessage(true, message, category));
        }

        public class TraceMessage
        {
            public DateTime Timestamp { get; private set; }
            public string Category { get; private set; }
            public string Message { get; private set; }

            public bool CompleteLine { get; private set; }

            public TraceMessage(bool completeLine, string message, string category = "")
            {
                Timestamp = DateTime.UtcNow;
                Message = message;
                Category = category;

                CompleteLine = completeLine;
            }

            public void Append(string message)
            {
                Message += message;
            }
        }
    }
}
