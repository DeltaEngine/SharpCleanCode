using System;

namespace SimplisticTcpFiniteStateMachine.Tests
{
	public class TcpFiniteStateMachine
	{
		public static string TraverseStates(string[] events)
		{
			var stateMachine = new TcpFiniteStateMachine();
			foreach (var eventName in events)
				stateMachine.state = stateMachine.GetNextState(Enum.Parse<Event>(eventName));
			return stateMachine.state.ToString();
		}

		private State state = State.CLOSED;

		public enum State
		{
			CLOSED,
			LISTEN,
			SYN_SENT,
			SYN_RCVD,
			ESTABLISHED,
			CLOSE_WAIT,
			LAST_ACK,
			FIN_WAIT_1,
			FIN_WAIT_2,
			CLOSING,
			TIME_WAIT,
			ERROR
		}

		private State GetNextState(Event executeEvent) =>
			state switch
			{
				State.CLOSED => GetNextClosedState(executeEvent),
				State.LISTEN => GetNextListenState(executeEvent),
				State.SYN_SENT => GetNextSynSentState(executeEvent),
				State.SYN_RCVD => GetNextSynRcvdState(executeEvent),
				State.ESTABLISHED => GetNextEstablishedState(executeEvent),
				State.CLOSE_WAIT => GetNextCloseWaitState(executeEvent),
				State.LAST_ACK => GetNextLastAckState(executeEvent),
				State.FIN_WAIT_1 => GetNextFinWait1State(executeEvent),
				State.FIN_WAIT_2 => GetNextFinWait2State(executeEvent),
				State.CLOSING => GetNextClosingState(executeEvent),
				State.TIME_WAIT => GetNextTimeWaitState(executeEvent),
				_ => State.ERROR
			};

		public enum Event
		{
			APP_PASSIVE_OPEN,
			APP_ACTIVE_OPEN,
			APP_SEND,
			APP_CLOSE,
			APP_TIMEOUT,
			RCV_SYN,
			RCV_ACK,
			RCV_SYN_ACK,
			RCV_FIN,
			RCV_FIN_ACK
		}

		private static State GetNextClosedState(Event executeEvent) =>
			executeEvent switch
			{
				Event.APP_PASSIVE_OPEN => State.LISTEN,
				Event.APP_ACTIVE_OPEN => State.SYN_SENT,
				_ => State.ERROR
			};
		
		private static State GetNextListenState(Event executeEvent) =>
			executeEvent switch
			{
				Event.RCV_SYN => State.SYN_RCVD,
				Event.APP_SEND => State.SYN_SENT,
				Event.APP_CLOSE => State.CLOSED,
				_ => State.ERROR
			};

		private State GetNextSynSentState(Event executeEvent)
		{
			switch (executeEvent)
			{
			case Event.RCV_SYN:
				syncReceivedCounter++;
				return State.SYN_RCVD;
			case Event.RCV_SYN_ACK:
				return State.ESTABLISHED;
			case Event.APP_CLOSE:
				return State.CLOSED;
			default:
				return State.ERROR;
			}
		}

		// ReSharper disable once NotAccessedField.Local, just an example
		private int syncReceivedCounter;

		private static State GetNextSynRcvdState(Event executeEvent) =>
			executeEvent switch
			{
				Event.APP_CLOSE => State.FIN_WAIT_1,
				Event.RCV_ACK => State.ESTABLISHED,
				_ => State.ERROR
			};

		private static State GetNextEstablishedState(Event executeEvent) =>
			executeEvent switch
			{
				Event.APP_CLOSE => State.FIN_WAIT_1,
				Event.RCV_FIN => State.CLOSE_WAIT,
				_ => State.ERROR
			};

		private static State GetNextCloseWaitState(Event executeEvent) =>
			executeEvent switch
			{
				Event.APP_CLOSE => State.LAST_ACK,
				_ => State.ERROR
			};

		private static State GetNextLastAckState(Event executeEvent) =>
			executeEvent switch
			{
				Event.RCV_ACK => State.CLOSED,
				_ => State.ERROR
			};

		private static State GetNextFinWait1State(Event executeEvent) =>
			executeEvent switch
			{
				Event.RCV_FIN => State.CLOSING,
				Event.RCV_FIN_ACK => State.TIME_WAIT,
				Event.RCV_ACK => State.FIN_WAIT_2,
				_ => State.ERROR
			};

		private static State GetNextFinWait2State(Event executeEvent) =>
			executeEvent switch
			{
				Event.RCV_FIN => State.TIME_WAIT,
				_ => State.ERROR
			};

		private static State GetNextClosingState(Event executeEvent) =>
			executeEvent switch
			{
				Event.RCV_ACK => State.TIME_WAIT,
				_ => State.ERROR
			};

		private static State GetNextTimeWaitState(Event executeEvent) =>
			executeEvent switch
			{
				Event.APP_TIMEOUT => State.CLOSED,
				_ => State.ERROR
			};
	}
}