using System;
using System.IO;
using Serilog.Events;
using Serilog.Formatting;
using Serilog.Formatting.Json;

namespace Backend.Shared.Logging
{
    public class LogsJsonFormatter : ITextFormatter
    {
        private readonly JsonValueFormatter _valueFormatter;
        private static readonly string[] LevelMap = { "vrb", "dbg", "inf", "wrn", "err", "ftl" };

        /// <summary>
        /// Construct a <see cref="LogsJsonFormatter"/>, optionally supplying a formatter for
        /// <see cref="LogEventPropertyValue"/>s on the event.
        /// </summary>
        /// <param name="valueFormatter">A value formatter, or null.</param>
        public LogsJsonFormatter(JsonValueFormatter valueFormatter = null)
        {
            _valueFormatter = valueFormatter ?? new JsonValueFormatter(typeTagName: "$type");
        }

        /// <summary>
        /// Format the log event into the output. Subsequent events will be newline-delimited.
        /// </summary>
        /// <param name="logEvent">The event to format.</param>
        /// <param name="output">The output.</param>
        public void Format(LogEvent logEvent, TextWriter output)
        {
            FormatEvent(logEvent, output, _valueFormatter);
            output.WriteLine();
        }

        /// <summary>
        /// Format the log event into the output.
        /// </summary>
        /// <param name="logEvent">The event to format.</param>
        /// <param name="output">The output.</param>
        /// <param name="valueFormatter">A value formatter for <see cref="LogEventPropertyValue"/>s on the event.</param>
        public static void FormatEvent(LogEvent logEvent, TextWriter output, JsonValueFormatter valueFormatter)
        {
            if (logEvent == null) throw new ArgumentNullException(nameof(logEvent));
            if (output == null) throw new ArgumentNullException(nameof(output));
            if (valueFormatter == null) throw new ArgumentNullException(nameof(valueFormatter));

            // DateTime
            output.Write("{\"dt\":\"");
            output.Write(logEvent.Timestamp.UtcDateTime.ToString("O"));

            // Level
            output.Write("\",\"lv\":\"");
            output.Write(LevelMap[(int)logEvent.Level]);

            // Message Template
            output.Write("\",\"mt\":");
            JsonValueFormatter.WriteQuotedJsonString(logEvent.MessageTemplate.Text, output);

            // Exception
            if (logEvent.Exception != null)
            {
                output.Write(",\"ex\":");
                JsonValueFormatter.WriteQuotedJsonString(logEvent.Exception.ToString(), output);
            }

            // Other Properties
            foreach (var property in logEvent.Properties)
            {
                output.Write(',');
                JsonValueFormatter.WriteQuotedJsonString(property.Key, output);
                output.Write(':');
                valueFormatter.Format(property.Value, output);
            }

            output.Write('}');
        }
    }
}
