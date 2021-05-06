using System.Linq;
using NUnit.Framework;

namespace SimplisticTcpFiniteStateMachine.Tests
{
	public class TcpFiniteStateMachineTests : TcpFiniteStateMachine
	{
		[TestCase(State.SYN_SENT, Event.APP_ACTIVE_OPEN)]
		[TestCase(State.ERROR, Event.RCV_SYN, Event.RCV_SYN)]
		[TestCase(State.CLOSE_WAIT, Event.APP_ACTIVE_OPEN, Event.RCV_SYN_ACK, Event.RCV_FIN)]
		[TestCase(State.SYN_RCVD, Event.APP_ACTIVE_OPEN, Event.RCV_SYN)]
		[TestCase(State.CLOSED, Event.APP_ACTIVE_OPEN, Event.APP_CLOSE)]
		[TestCase(State.ERROR, Event.APP_ACTIVE_OPEN, Event.APP_ACTIVE_OPEN)]
		[TestCase(State.ERROR, Event.APP_ACTIVE_OPEN, Event.RCV_SYN_ACK, Event.RCV_SYN)]
		[TestCase(State.SYN_SENT, Event.APP_PASSIVE_OPEN, Event.APP_SEND)]
		[TestCase(State.CLOSED, Event.APP_PASSIVE_OPEN, Event.APP_CLOSE)]
		[TestCase(State.ERROR, Event.APP_PASSIVE_OPEN, Event.APP_PASSIVE_OPEN)]
		[TestCase(State.ESTABLISHED, Event.APP_PASSIVE_OPEN, Event.RCV_SYN, Event.RCV_ACK)]
		[TestCase(State.FIN_WAIT_1, Event.APP_PASSIVE_OPEN, Event.RCV_SYN, Event.APP_CLOSE)]
		[TestCase(State.ERROR, Event.APP_PASSIVE_OPEN, Event.RCV_SYN, Event.RCV_SYN)]
		[TestCase(State.LAST_ACK,
			 Event.APP_ACTIVE_OPEN, Event.RCV_SYN_ACK, Event.RCV_FIN, Event.APP_CLOSE )]
		[TestCase(State.ERROR,
			Event.APP_ACTIVE_OPEN, Event.RCV_SYN_ACK, Event.RCV_FIN, Event.RCV_FIN )]
		[TestCase(State.ERROR, Event.APP_PASSIVE_OPEN, Event.RCV_SYN, Event.RCV_ACK,
			Event.APP_CLOSE, Event.APP_SEND)]
		[TestCase(State.ERROR, Event.APP_ACTIVE_OPEN, Event.RCV_SYN_ACK, Event.APP_CLOSE,
			Event.RCV_FIN_ACK, Event.RCV_ACK)]
		[TestCase(State.CLOSED, Event.APP_ACTIVE_OPEN, Event.RCV_SYN_ACK, Event.RCV_FIN,
			Event.APP_CLOSE, Event.RCV_ACK)]
		[TestCase(State.ERROR, Event.APP_ACTIVE_OPEN, Event.RCV_SYN_ACK, Event.RCV_FIN,
			Event.APP_CLOSE, Event.RCV_SYN)]
		[TestCase(State.TIME_WAIT, Event.APP_ACTIVE_OPEN, Event.RCV_SYN_ACK, Event.APP_CLOSE,
			Event.RCV_ACK, Event.RCV_FIN)]
		[TestCase(State.ERROR, Event.APP_ACTIVE_OPEN, Event.RCV_SYN_ACK, Event.APP_CLOSE,
			Event.RCV_FIN, Event.RCV_FIN)]
		[TestCase(State.TIME_WAIT, Event.APP_ACTIVE_OPEN, Event.RCV_SYN_ACK, Event.APP_CLOSE,
			Event.RCV_FIN, Event.RCV_ACK)]
		[TestCase(State.ERROR, Event.APP_ACTIVE_OPEN, Event.RCV_SYN_ACK, Event.APP_CLOSE,
			Event.RCV_ACK, Event.RCV_ACK)]
		[TestCase(State.CLOSED, Event.APP_ACTIVE_OPEN, Event.RCV_SYN_ACK, Event.APP_CLOSE,
			Event.RCV_FIN_ACK, Event.APP_TIMEOUT)]
		public void SampleTests(State expected, params Event[] events) =>
			Assert.That(TraverseStates(events.Select(e => e.ToString()).ToArray()),
				Is.EqualTo(expected.ToString()));
	}
}