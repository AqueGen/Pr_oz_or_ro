using System.Diagnostics;

namespace Kapitalist.Services.Logging
{
    public static class TraceExtentions
    {
        /// <summary>
        /// Write an information trace message to a tracesource.
        /// </summary>
        /// <param name="trace">The trace source to write to</param>
        /// <param name="message">The informative message to write.</param>
        [Conditional("TRACE")]
        public static void TraceInformation(this ITrace trace, string message)
        {
            if (trace != null)
                trace.Source.TraceInformation(message);
            else
                System.Diagnostics.Trace.TraceInformation(message);
        }

        /// <summary>
        /// Write an information trace message to a tracesource.
        /// </summary>
        /// <param name="trace">The trace source to write to</param>
        /// <param name="format">The format of the string</param>
        /// <param name="args">The arguments to format the string</param>
        [Conditional("TRACE")]
        public static void TraceInformation(this ITrace trace, string format, params object[] args)
        {
            if (trace != null)
                trace.Source.TraceInformation(format, args);
            else
                System.Diagnostics.Trace.TraceInformation(format, args);
        }

        /// <summary>
        /// Write a warning trace message to a tracesource.
        /// </summary>
        /// <param name="trace">The trace source to write to</param>
        /// <param name="message">The informative message to write.</param>
        [Conditional("TRACE")]
        public static void TraceWarning(this ITrace trace, string message)
        {
            if (trace != null)
                trace.Source.TraceEvent(TraceEventType.Warning, 0, message);
            else
                System.Diagnostics.Trace.TraceWarning(message);
        }

        /// <summary>
        /// Write a warning trace message to a tracesource.
        /// </summary>
        /// <param name="trace">The trace source to write to</param>
        /// <param name="format">The format of the string</param>
        /// <param name="args">The arguments to format the string</param>
        [Conditional("TRACE")]
        public static void TraceWarning(this ITrace trace, string format, params object[] args)
        {
            if (trace != null)
                trace.Source.TraceEvent(TraceEventType.Warning, 0, format, args);
            else
                System.Diagnostics.Trace.TraceWarning(format, args);
        }

        /// <summary>
        /// Write a error trace message to a tracesource.
        /// </summary>
        /// <param name="traceSource">The trace source to write to</param>
        /// <param name="message">The informative message to write.</param>
        [Conditional("TRACE")]
        public static void TraceError(this ITrace trace, string message)
        {
            if (trace != null)
                trace.Source.TraceEvent(TraceEventType.Error, 0, message);
            else
                System.Diagnostics.Trace.TraceError(message);
        }

        /// <summary>
        /// Write a error trace message to a tracesource.
        /// </summary>
        /// <param name="traceSource">The trace source to write to</param>
        /// <param name="format">The format of the string</param>
        /// <param name="args">The arguments to format the string</param>
        [Conditional("TRACE")]
        public static void TraceError(this ITrace trace, string format, params object[] args)
        {
            if (trace != null)
                trace.Source.TraceEvent(TraceEventType.Error, 0, format, args);
            else
                System.Diagnostics.Trace.TraceError(format, args);
        }

        [Conditional("TRACE")]
        public static void TraceEvent(this ITrace trace, int id, string message)
        {
            if (trace != null)
                trace.Source.TraceEvent(TraceEventType.Information, id, message);
            else
                System.Diagnostics.Trace.TraceInformation(message);
        }

        [Conditional("TRACE")]
        public static void TraceEvent(this ITrace trace, int id, string format, params object[] args)
        {
            if (trace != null)
                trace.Source.TraceEvent(TraceEventType.Information, id, format, args);
            else
                System.Diagnostics.Trace.TraceInformation(format, args);
        }
    }
}