using System;
using System.Collections.Generic;

namespace Foundatio.Messaging {
    public class SharedMessageBusOptions : SharedOptions {
        /// <summary>
        /// The topic name
        /// </summary>
        public string Topic { get; set; } = "messages";

        /// <summary>
        /// Controls the maximum number of backplane messages that need to be queued and sent to subscribers.
        /// </summary>
        public int TaskQueueMaxItems { get; set; } = 10000;
        /// <summary>
        /// Controls the maximum number of threads that will process queued subscriber messages.
        /// </summary>
        public byte TaskQueueMaxDegreeOfParallelism { get; set; } = 4;

        /// <summary>
        /// Controls which types messages are mapped to.
        /// </summary>
        public Dictionary<string, Type> MessageTypeMappings { get; set; } = new Dictionary<string, Type>();
    }

    public class SharedMessageBusOptionsBuilder<TOptions, TBuilder> : SharedOptionsBuilder<TOptions, TBuilder>
        where TOptions : SharedMessageBusOptions, new()
        where TBuilder : SharedMessageBusOptionsBuilder<TOptions, TBuilder> {
        public TBuilder Topic(string topic) {
            if (string.IsNullOrEmpty(topic))
                throw new ArgumentNullException(nameof(topic));
            Target.Topic = topic;
            return (TBuilder)this;
        }

        public TBuilder TaskQueueMaxItems(int maxItems) {
            Target.TaskQueueMaxItems = maxItems;
            return (TBuilder)this;
        }

        public TBuilder TaskQueueMaxDegreeOfParallelism(byte maxDegree) {
            Target.TaskQueueMaxDegreeOfParallelism = maxDegree;
            return (TBuilder)this;
        }

        public TBuilder MapMessageType<T>(string name) {
            if (Target.MessageTypeMappings == null)
                Target.MessageTypeMappings = new Dictionary<string, Type>();
            
            Target.MessageTypeMappings[name] = typeof(T);
            return (TBuilder)this;
        }

        public TBuilder MapMessageTypeToClassName<T>() {
            if (Target.MessageTypeMappings == null)
                Target.MessageTypeMappings = new Dictionary<string, Type>();
            
            Target.MessageTypeMappings[typeof(T).Name] = typeof(T);
            return (TBuilder)this;
        }
    }
}