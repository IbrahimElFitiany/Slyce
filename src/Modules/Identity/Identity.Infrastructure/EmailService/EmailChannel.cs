using Identity.Application.Interfaces.EmailService;
using System.Threading.Channels;

namespace Identity.Infrastructure.EmailService
{
    internal sealed class EmailChannel
    {
        private readonly Channel<EmailMessage> _channel = Channel.CreateBounded<EmailMessage>(
            new BoundedChannelOptions(1000)
            {
                FullMode = BoundedChannelFullMode.Wait 
            });

        internal ChannelWriter<EmailMessage> Writer => _channel.Writer;
        internal ChannelReader<EmailMessage> Reader => _channel.Reader;
    }
}