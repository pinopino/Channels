# Channels (Push based Streams)

Channels has been renamed to Pipelines (System.IO.Pipelines) and has moved to corefxlab https://github.com/dotnet/corefxlab.

由于之前的项目用的是 .net core早期的项目格式，这在当前最新的vs2019中已经无法加载成功了。于是新建这个分支用来将之前的`project.json`迁移到`csproj`以便可以观察学习。

另外，之前的实现中用到了很多不稳定的api（比如`System.Buffers`之流），因此迁移后的项目目前是无法编译通过的（大概还有20多个错误没有解决），不过也还好并不会影响对大意的理解。
