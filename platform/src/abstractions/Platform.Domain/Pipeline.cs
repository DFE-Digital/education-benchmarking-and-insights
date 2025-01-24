namespace Platform.Domain;

//TODO: Consider converting these to enums
public static class Pipeline
{
    public static class RunType
    {
        public const string Default = "default";
        public const string Custom = "custom";
    }

    public static class JobType
    {
        public const string Default = "default";
        public const string ComparatorSet = "comparator-set";
        public const string CustomData = "custom-data";
    }

    public static class JobStatus
    {
        public const string Complete = "complete";
        public const string Pending = "pending";
        public const string Removed = "removed";
        public const string Failed = "failed";
    }

    public static class PayloadKind
    {
        public const string ComparatorSetPayload = nameof(ComparatorSetPayload);
        public const string CustomDataPayload = nameof(CustomDataPayload);
    }

    public static class Events
    {
        public const string PipelinePendingMessageReceived = nameof(PipelinePendingMessageReceived);
        public const string PipelinePendingMessageOrchestrated = nameof(PipelinePendingMessageOrchestrated);
        public const string PipelineStartDefaultMessageReceived = nameof(PipelineStartDefaultMessageReceived);
        public const string PipelineStartCustomMessageReceived = nameof(PipelineStartCustomMessageReceived);
        public const string PipelineFinishedMessageReceived = nameof(PipelineFinishedMessageReceived);
        public const string PipelineStatusReceived = nameof(PipelineStatusReceived);
    }
}