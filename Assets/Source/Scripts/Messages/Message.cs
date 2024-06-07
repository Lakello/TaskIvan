namespace TaskIvan.Messages
{
	public class Message
	{
		public readonly MessageId Id;

		public Message(MessageId id) =>
			Id = id;
	}

	public class Message<TData> : Message
	{
		public readonly TData Data;

		public Message(MessageId id, TData data) : base(id) =>
			Data = data;
	}
}