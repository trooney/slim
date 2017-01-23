Slim
----

Slim is a simple url minification application. It's written in C# and uses the Mono framework.

Slim uses the following components:

- ASP MVC4
- TinyIoC
- sqlite-net
- AutoMapper

Deploy
---

Build your project in Xamarin Studio. Then run the `make build` task and rsync the results to your server.

```
make build
rsync --exclude=build/App_Data/slim.sqlite -a build/ username@example:/path/to/slimurl.org
```