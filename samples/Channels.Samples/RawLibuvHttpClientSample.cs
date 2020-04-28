﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Channels.Networking.Libuv;
using Channels.Text.Primitives;

namespace Channels.Samples
{
    public class RawLibuvHttpClientSample
    {
        public static async Task Run()
        {
            var thread = new UvThread();
            var client = new UvTcpClient(thread, new IPEndPoint(IPAddress.Loopback, 5000));

            var consoleOutput = thread.ChannelFactory.MakeWriteableChannel(Console.OpenStandardOutput());

            var connection = await client.ConnectAsync();

            while (true)
            {
                var buffer = connection.Output.Alloc();

                buffer.WriteAsciiString("GET / HTTP/1.1");
                buffer.WriteAsciiString("\r\n\r\n");

                await buffer.FlushAsync();

                // Write the client output to the console
                await CopyCompletedAsync(connection.Input, consoleOutput);

                await Task.Delay(1000);
            }
        }
        private static async Task CopyCompletedAsync(IReadableChannel input, IWritableChannel channel)
        {
            var result = await input.ReadAsync();
            var inputBuffer = result.Buffer;

            while (true)
            {
                try
                {
                    if (inputBuffer.IsEmpty && result.IsCompleted)
                    {
                        return;
                    }

                    var buffer = channel.Alloc();

                    buffer.Append(inputBuffer);

                    await buffer.FlushAsync();
                }
                finally
                {
                    input.Advance(inputBuffer.End);
                }

                var awaiter = input.ReadAsync();

                if (!awaiter.IsCompleted)
                {
                    // No more data
                    break;
                }

                result = await input.ReadAsync();
                inputBuffer = result.Buffer;
            }
        }

    }
}
